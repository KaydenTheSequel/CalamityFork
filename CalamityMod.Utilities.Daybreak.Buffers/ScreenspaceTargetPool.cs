using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Utilities.Daybreak.Buffers;

internal sealed class ScreenspaceTargetPool : RenderTargetPool
{
	public delegate(int Width, int Height) GetTargetSize(int backbufferWidth, int backbufferHeight, int offscreenTargetWidth, int offscreenTargetHeight);

	private readonly Dictionary<RenderTargetLease, GetTargetSize> cache = new Dictionary<RenderTargetLease, GetTargetSize>();

	private bool disposed;

	public new static ScreenspaceTargetPool Shared { get; } = new ScreenspaceTargetPool();

	private ScreenspaceTargetPool()
	{
	}

	public override RenderTargetLease Rent(GraphicsDevice device, int width, int height, RenderTargetDescriptor descriptor)
	{
		ArgumentNullException.ThrowIfNull(device, "device");
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0, "width");
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0, "height");
		ObjectDisposedException.ThrowIf(disposed, this);
		return Rent(device, (int _, int _, int _, int _) => (Width: width, Height: height), descriptor);
	}

	public RenderTargetLease Rent(GraphicsDevice device, RenderTargetDescriptor? descriptor = null)
	{
		return Rent(device, (int width, int height) => (width, height), descriptor);
	}

	public RenderTargetLease Rent(GraphicsDevice device, Func<int, int, (int, int)> targetSizeCallback, RenderTargetDescriptor? descriptor = null)
	{
		return Rent(device, (int width, int height, int _, int _) => ((int Width, int Height))targetSizeCallback(width, height), descriptor);
	}

	public RenderTargetLease Rent(GraphicsDevice device, GetTargetSize targetSizeCallback, RenderTargetDescriptor? descriptor = null)
	{
		ArgumentNullException.ThrowIfNull(device, "device");
		ArgumentNullException.ThrowIfNull(targetSizeCallback, "targetSizeCallback");
		ObjectDisposedException.ThrowIf(disposed, this);
		RenderTargetDescriptor valueOrDefault = descriptor.GetValueOrDefault();
		if (!descriptor.HasValue)
		{
			valueOrDefault = RenderTargetDescriptor.Default;
			descriptor = valueOrDefault;
		}
		GetTargetSizes(device, out var backbufferWidth, out var backbufferHeight, out var offscreenTargetWidth, out var offscreenTargetHeight);
		(int Width, int Height) tuple = targetSizeCallback(backbufferWidth, backbufferHeight, offscreenTargetWidth, offscreenTargetHeight);
		int width = tuple.Width;
		int height = tuple.Height;
		RenderTargetLease lease = new RenderTargetLease(descriptor.Value.Create(device, width, height), this);
		cache[lease] = targetSizeCallback;
		return lease;
	}

	public override void Return(RenderTargetLease lease)
	{
		ArgumentNullException.ThrowIfNull(lease, "lease");
		ObjectDisposedException.ThrowIf(disposed, this);
		if (cache.Remove(lease))
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
		foreach (RenderTargetLease key in cache.Keys)
		{
			((GraphicsResource)key.Target).Dispose();
		}
		cache.Clear();
	}

	private static void GetTargetSizes(GraphicsDevice device, out int backbufferWidth, out int backbufferHeight, out int offscreenTargetWidth, out int offscreenTargetHeight)
	{
		backbufferWidth = device.PresentationParameters.BackBufferWidth;
		backbufferHeight = device.PresentationParameters.BackBufferHeight;
		offscreenTargetWidth = ((Texture2D)Main.instance.tileTarget).Width;
		offscreenTargetHeight = ((Texture2D)Main.instance.tileTarget).Height;
	}

	internal void ResizeCachedTargets(GraphicsDevice device)
	{
		GetTargetSizes(device, out var backbufferWidth, out var backbufferHeight, out var offscreenTargetWidth, out var offscreenTargetHeight);
		foreach (var (lease, getTargetSize2) in cache)
		{
			var (width, height) = getTargetSize2(backbufferWidth, backbufferHeight, offscreenTargetWidth, offscreenTargetHeight);
			if (((Texture2D)lease.Target).Width != width || ((Texture2D)lease.Target).Height != height)
			{
				((GraphicsResource)lease.Target).Dispose();
				lease.Target = RenderTargetDescriptor.From(lease.Target).Create(device, width, height);
			}
		}
	}
}
