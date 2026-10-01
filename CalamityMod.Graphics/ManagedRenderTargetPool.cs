using System;
using System.Collections.Generic;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Graphics;

internal sealed class ManagedRenderTargetPool : RenderTargetPool
{
	private static readonly List<ManagedRenderTarget> managedTargets = new List<ManagedRenderTarget>();

	public new static ManagedRenderTargetPool Shared { get; } = new ManagedRenderTargetPool();

	public override RenderTargetLease Rent(GraphicsDevice device, int width, int height, RenderTargetDescriptor descriptor)
	{
		throw new InvalidOperationException("ManagedRenderTargetPool does not support directly leasing targets");
	}

	public RenderTargetLease Register(ManagedRenderTarget managedTarget)
	{
		managedTargets.Add(managedTarget);
		return new RenderTargetLease(managedTarget.CreationCondition(Main.screenWidth, Main.screenHeight), this);
	}

	public override void Return(RenderTargetLease lease)
	{
		((GraphicsResource)lease.Target).Dispose();
	}

	public void Return(ManagedRenderTarget managedRt, bool preserveCollection)
	{
		if (!preserveCollection)
		{
			managedTargets.Remove(managedRt);
		}
		Return(managedRt.lease);
	}

	public override void Dispose()
	{
		foreach (ManagedRenderTarget managedTarget in managedTargets)
		{
			managedTarget.Dispose(preserveCollection: true);
		}
		managedTargets.Clear();
	}

	internal void ResizeTargets()
	{
		foreach (ManagedRenderTarget target in managedTargets)
		{
			if (target.ShouldResetUponScreenResize)
			{
				((GraphicsResource)target.lease.Target).Dispose();
				target.lease.Target = target.CreationCondition(Main.screenWidth, Main.screenHeight);
			}
		}
	}
}
