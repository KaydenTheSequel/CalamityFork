using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Systems.Graphic;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Renderers;

public class RendererManager : ModSystem
{
	public static List<BaseRenderer> Renderers { get; private set; } = new List<BaseRenderer>();

	public override void Load()
	{
		if (!Main.dedServ)
		{
			GeneralDrawLayerSystem.OnDrawLayer += DrawRendererAtLayer;
			GeneralDrawLayerSystem.OnPrepareDraw += DrawToTargets;
		}
	}

	public override void Unload()
	{
		if (!Main.dedServ)
		{
			Renderers.Clear();
		}
	}

	public override void PreUpdateEntities()
	{
		if (Main.dedServ)
		{
			return;
		}
		foreach (BaseRenderer renderer in Renderers)
		{
			renderer.PreUpdate();
		}
	}

	public override void PostUpdateEverything()
	{
		if (Main.dedServ)
		{
			return;
		}
		foreach (BaseRenderer renderer in Renderers)
		{
			renderer.PostUpdate();
		}
	}

	private static void DrawRendererAtLayer(GeneralDrawLayer drawLayer)
	{
		IEnumerable<BaseRenderer> renderers = Renderers.Where((BaseRenderer renderer) => renderer.ShouldDraw && renderer.Layer == drawLayer);
		if (!renderers.Any())
		{
			return;
		}
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise);
		foreach (BaseRenderer item in renderers)
		{
			item.DrawTarget(Main.spriteBatch);
		}
		Main.spriteBatch.End();
	}

	private void DrawToTargets()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		if (Main.gameMenu || Main.dedServ)
		{
			return;
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.Calamity().drawnAnyShieldThisFrame = false;
		}
		foreach (BaseRenderer renderer in Renderers)
		{
			if (!renderer.ShouldDraw)
			{
				continue;
			}
			using (Main.spriteBatch.Scope())
			{
				using (renderer.MainTarget.Scope(preserveContents: true, Color.Transparent))
				{
					Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
					renderer.DrawToTarget(Main.spriteBatch);
					Main.spriteBatch.End();
				}
			}
		}
	}
}
