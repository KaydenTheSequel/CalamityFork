using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

internal sealed class SharedRenderTargetPool : RenderTargetPool
{
	private readonly record struct Key(int Width, int Height, RenderTargetDescriptor Descriptor)
	{
		public static Key From(RenderTarget2D target)
		{
			return new Key(((Texture2D)target).Width, ((Texture2D)target).Height, RenderTargetDescriptor.From(target));
		}
	}

	private sealed class Entry
	{
		public Stack<RenderTarget2D> Targets { get; } = new Stack<RenderTarget2D>();

		public DateTime LastUsed { get; set; } = DateTime.UtcNow;
	}

	private const int max_per_key = 4;

	private const int max_total_targets = 128;

	private static readonly TimeSpan max_idle_time = TimeSpan.FromSeconds(5.0);

	private static readonly TimeSpan minimum_trim_time = TimeSpan.FromSeconds(1.0);

	private readonly Dictionary<Key, Entry> cache = new Dictionary<Key, Entry>();

	private DateTime lastTrimmed = DateTime.UtcNow;

	private int totalCached;

	private bool disposed;

	public override RenderTargetLease Rent(GraphicsDevice device, int width, int height, RenderTargetDescriptor descriptor)
	{
		ArgumentNullException.ThrowIfNull(device, "device");
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0, "width");
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0, "height");
		ObjectDisposedException.ThrowIf(disposed, this);
		try
		{
			Key key = new Key(width, height, descriptor);
			if (!cache.TryGetValue(key, out var entry))
			{
				entry = (cache[key] = new Entry());
			}
			else
			{
				entry.LastUsed = DateTime.UtcNow;
			}
			RenderTarget2D target;
			if (entry.Targets.Count > 0)
			{
				target = entry.Targets.Pop();
				totalCached--;
			}
			else
			{
				target = descriptor.Create(device, width, height);
			}
			return new RenderTargetLease(target, this);
		}
		finally
		{
			TrimAged();
		}
	}

	public override void Return(RenderTargetLease lease)
	{
		ArgumentNullException.ThrowIfNull(lease, "lease");
		ObjectDisposedException.ThrowIf(disposed, this);
		Key key = Key.From(lease.Target);
		if (!cache.TryGetValue(key, out var entry))
		{
			entry = (cache[key] = new Entry());
		}
		else
		{
			entry.LastUsed = DateTime.UtcNow;
		}
		if (entry.Targets.Count < 4)
		{
			if (totalCached >= 128)
			{
				((GraphicsResource)lease.Target).Dispose();
				return;
			}
			entry.Targets.Push(lease.Target);
			totalCached++;
		}
		else
		{
			((GraphicsResource)lease.Target).Dispose();
		}
	}

	public override void Dispose()
	{
		if (!disposed)
		{
			Trim();
			disposed = true;
		}
	}

	private void Trim()
	{
		foreach (Entry entry in cache.Values)
		{
			while (entry.Targets.Count > 0)
			{
				((GraphicsResource)entry.Targets.Pop()).Dispose();
				totalCached--;
			}
		}
		cache.Clear();
	}

	private void TrimAged()
	{
		DateTime now = DateTime.UtcNow;
		if (now - lastTrimmed < minimum_trim_time)
		{
			return;
		}
		lastTrimmed = now;
		foreach (var (key2, entry2) in cache)
		{
			if (!(now - entry2.LastUsed <= max_idle_time))
			{
				while (entry2.Targets.Count > 0)
				{
					((GraphicsResource)entry2.Targets.Pop()).Dispose();
					totalCached--;
				}
				cache.Remove(key2);
			}
		}
	}
}
