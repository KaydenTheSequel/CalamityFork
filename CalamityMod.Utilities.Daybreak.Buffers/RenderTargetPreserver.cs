using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public static class RenderTargetPreserver
{
	public static void PreserveBindings(RenderTargetBinding[] bindings)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < bindings.Length; i++)
		{
			RenderTargetBinding binding = bindings[i];
			Texture renderTarget = ((RenderTargetBinding)(ref binding)).RenderTarget;
			RenderTarget2D rt = (RenderTarget2D)(object)((renderTarget is RenderTarget2D) ? renderTarget : null);
			if (rt != null)
			{
				rt.RenderTargetUsage = (RenderTargetUsage)1;
			}
		}
	}

	public static RenderTargetBinding[] GetAndPreserveCurrentBindings()
	{
		RenderTargetBinding[] renderTargets = ((Game)Main.instance).GraphicsDevice.GetRenderTargets();
		PreserveBindings(renderTargets);
		return renderTargets;
	}
}
