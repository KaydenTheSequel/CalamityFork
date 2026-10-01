using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public static class RenderTargetScopeExtensions
{
	public static RenderTargetScope Scope(this RenderTarget2D target, bool preserveContents = true, Color? clearColor = null)
	{
		return new RenderTargetScope(target, preserveContents, clearColor);
	}

	public static RenderTargetScope Scope(this RenderTargetLease target, bool preserveContents = true, Color? clearColor = null)
	{
		return new RenderTargetScope(target.Target, preserveContents, clearColor);
	}
}
