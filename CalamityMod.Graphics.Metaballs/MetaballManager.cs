using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Systems.Graphic;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public class MetaballManager : ModSystem
{
	internal static readonly List<Metaball> metaballs = new List<Metaball>();

	public override void Load()
	{
		GeneralDrawLayerSystem.OnPrepareDraw += PrepareMetaballTargets;
		GeneralDrawLayerSystem.OnDrawLayer += DrawMetaballs;
	}

	public override void OnModUnload()
	{
		Main.QueueMainThreadAction(delegate
		{
			foreach (Metaball metaball in metaballs)
			{
				metaball?.Dispose();
			}
		});
	}

	public override void OnWorldUnload()
	{
		foreach (Metaball metaball in metaballs)
		{
			metaball.ClearInstances();
		}
	}

	public override void PostUpdateEverything()
	{
		foreach (Metaball metaball in metaballs.Where((Metaball m) => m.AnythingToDraw))
		{
			if (metaball.IgnoreFPS)
			{
				metaball.Update();
			}
		}
	}

	private void PrepareMetaballTargets()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		IEnumerable<Metaball> activeMetaballs = metaballs.Where((Metaball m) => m.AnythingToDraw);
		if (!activeMetaballs.Any())
		{
			return;
		}
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, (Effect)null, Main.Transform);
		_ = ((Game)Main.instance).GraphicsDevice;
		Vector2 offset = default(Vector2);
		foreach (Metaball metaball in activeMetaballs)
		{
			if (!Main.gamePaused && !metaball.IgnoreFPS)
			{
				metaball.Update();
			}
			metaball.PrepareSpriteBatch(Main.spriteBatch);
			foreach (RenderTargetLease layerTarget in metaball.LayerTargets)
			{
				using (layerTarget.Scope(preserveContents: true, Color.Transparent))
				{
					((Vector2)(ref offset))._002Ector(-2f, -2f);
					Main.screenPosition += offset;
					metaball.DrawInstances();
					Main.screenPosition -= offset;
					Main.spriteBatch.End();
					Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, (Effect)null, Main.Transform);
				}
			}
		}
		Main.spriteBatch.End();
	}

	internal static bool AnyActiveMetaballsAtLayer(GeneralDrawLayer layerType)
	{
		return metaballs.Any((Metaball m) => m.AnythingToDraw && m.DrawLayer == layerType);
	}

	private static void DrawMetaballs(GeneralDrawLayer layerType)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise);
		Vector2 offset = default(Vector2);
		foreach (Metaball metaball in metaballs.Where((Metaball m) => m.DrawLayer == layerType && m.AnythingToDraw))
		{
			for (int i = 0; i < metaball.LayerTargets.Count; i++)
			{
				((Vector2)(ref offset))._002Ector(-2f, -2f);
				Main.screenPosition += offset;
				metaball.PrepareShaderForTarget(i);
				Main.screenPosition -= offset;
				Color metaballDraw = (Main.LocalPlayer.Calamity().trippy ? Main.DiscoColor : Color.White);
				Main.spriteBatch.Draw((Texture2D)(object)metaball.LayerTargets[i].Target, offset, metaballDraw);
				if (Main.LocalPlayer.Calamity().trippy)
				{
					Main.spriteBatch.Draw((Texture2D)(object)metaball.LayerTargets[i].Target, offset, (Rectangle?)null, metaballDraw, 0f, Vector2.Zero, 1f, (SpriteEffects)1, 0f);
					Main.spriteBatch.Draw((Texture2D)(object)metaball.LayerTargets[i].Target, offset, (Rectangle?)null, metaballDraw, 0f, Vector2.Zero, 1f, (SpriteEffects)2, 0f);
					Main.spriteBatch.Draw((Texture2D)(object)metaball.LayerTargets[i].Target, offset, (Rectangle?)null, metaballDraw, 0f, Vector2.Zero, 1f, (SpriteEffects)3, 0f);
				}
			}
		}
		Main.spriteBatch.End();
	}
}
