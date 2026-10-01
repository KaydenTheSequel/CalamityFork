using System;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Graphics;

[Obsolete("ManagedRenderTarget is obsolete; developers should use RenderTarget2Ds and RenderTargetLeases")]
public class ManagedRenderTarget : IDisposable
{
	public delegate RenderTarget2D RenderTargetCreationCondition(int screenWidth, int screenHeight);

	internal RenderTargetLease lease;

	public readonly RenderTargetCreationCondition CreationCondition;

	public readonly bool ShouldResetUponScreenResize;

	public readonly bool ShouldAutoDispose;

	public int TimeSinceLastAccessed;

	public bool WaitingForFirstInitialization => false;

	public bool IsUninitialized => false;

	public bool IsDisposed { get; private set; }

	public RenderTarget2D Target => lease.Target;

	public int Width => ((Texture2D)Target).Width;

	public int Height => ((Texture2D)Target).Height;

	public Vector2 Size
	{
		get
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2((float)Width, (float)Height);
		}
	}

	[Obsolete("use ScreenspaceTargetPool")]
	public static RenderTarget2D CreateScreenSizedTarget(int screenWidth, int screenHeight)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		return new RenderTarget2D(((Game)Main.instance).GraphicsDevice, screenWidth, screenHeight);
	}

	public ManagedRenderTarget(bool shouldResetUponScreenResize, RenderTargetCreationCondition creationCondition, bool shouldAutoDispose = true)
	{
		ShouldResetUponScreenResize = shouldResetUponScreenResize;
		CreationCondition = creationCondition;
		ShouldAutoDispose = shouldAutoDispose;
		lease = ManagedRenderTargetPool.Shared.Register(this);
	}

	[Obsolete("Use RenderTargetScope")]
	public void SwapTo(Color? flushColor = null)
	{
		Target.SwapTo(flushColor);
	}

	public void Dispose()
	{
		Dispose(preserveCollection: false);
	}

	public void Dispose(bool preserveCollection)
	{
		if (!IsDisposed)
		{
			IsDisposed = true;
			ManagedRenderTargetPool.Shared.Return(this, preserveCollection);
		}
	}

	public void Recreate(int screenWidth, int screenHeight)
	{
	}

	public static implicit operator RenderTarget2D(ManagedRenderTarget target)
	{
		return target.Target;
	}
}
