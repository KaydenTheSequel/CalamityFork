using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public readonly struct RenderTargetScope : IDisposable
{
	private readonly GraphicsDevice graphicsDevice;

	private readonly RenderTargetBinding[] previous;

	public RenderTargetScope(RenderTarget2D target, bool preserveContents = true, Color? clearColor = null)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		ArgumentNullException.ThrowIfNull(target, "target");
		graphicsDevice = ((GraphicsResource)target).GraphicsDevice;
		previous = graphicsDevice.GetRenderTargets();
		if (preserveContents)
		{
			RenderTargetPreserver.PreserveBindings(previous);
		}
		graphicsDevice.SetRenderTarget(target);
		if (clearColor.HasValue)
		{
			graphicsDevice.Clear(clearColor.Value);
		}
	}

	public void Dispose()
	{
		graphicsDevice.SetRenderTargets(previous);
	}
}
