using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public abstract class RenderTargetPool : IDisposable
{
	private static readonly SharedRenderTargetPool shared = new SharedRenderTargetPool();

	private static readonly List<RenderTargetLease> leases_to_clear = new List<RenderTargetLease>();

	public static RenderTargetPool Shared => shared;

	public abstract RenderTargetLease Rent(GraphicsDevice device, int width, int height, RenderTargetDescriptor descriptor);

	public abstract void Return(RenderTargetLease lease);

	public abstract void Dispose();

	public static void ReturnNextFrame(RenderTargetLease lease)
	{
		leases_to_clear.Add(lease);
	}

	internal static void ClearPendingLeases()
	{
		foreach (RenderTargetLease item in leases_to_clear)
		{
			item.Dispose();
		}
		leases_to_clear.Clear();
	}
}
