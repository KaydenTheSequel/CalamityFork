using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Systems.Graphic;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Primitives;

public class PrimitivePixelationSystem : ModSystem
{
	private static RenderTargetLease PixelationTarget_BeforeAllTiles;

	private static RenderTargetLease PixelationTarget_BeforeSolidTiles;

	private static RenderTargetLease PixelationTarget_BeforeNPCs;

	private static RenderTargetLease PixelationTarget_AfterNPCs;

	private static RenderTargetLease PixelationTarget_BeforeProjectiles;

	private static RenderTargetLease PixelationTarget_AfterProjectiles;

	private static RenderTargetLease PixelationTarget_AfterPlayers;

	private static RenderTargetLease PixelationTarget_AfterDusts;

	private static RenderTargetLease PixelationTarget_AfterEverything;

	public static bool CurrentlyRendering { get; private set; }

	public override void Load()
	{
		if (Main.dedServ)
		{
			return;
		}
		Main.QueueMainThreadAction(delegate
		{
			PixelationTarget_BeforeAllTiles = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
			PixelationTarget_BeforeSolidTiles = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
			PixelationTarget_BeforeNPCs = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
			PixelationTarget_AfterNPCs = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
			PixelationTarget_BeforeProjectiles = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
			PixelationTarget_AfterProjectiles = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
			PixelationTarget_AfterPlayers = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
			PixelationTarget_AfterDusts = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
			PixelationTarget_AfterEverything = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => (w / 2, h / 2));
		});
		GeneralDrawLayerSystem.OnDrawLayer += DrawTargetScaled;
		GeneralDrawLayerSystem.OnPrepareDraw += DrawToTargets;
	}

	public override void Unload()
	{
		if (!Main.dedServ)
		{
			GeneralDrawLayerSystem.OnDrawLayer -= DrawTargetScaled;
			GeneralDrawLayerSystem.OnPrepareDraw -= DrawToTargets;
		}
	}

	private void DrawToTargets()
	{
		if (Main.gameMenu)
		{
			return;
		}
		List<IPixelatedPrimitiveRenderer> beforeAllTiles = new List<IPixelatedPrimitiveRenderer>();
		List<IPixelatedPrimitiveRenderer> beforeSolidTiles = new List<IPixelatedPrimitiveRenderer>();
		List<IPixelatedPrimitiveRenderer> beforeNPCs = new List<IPixelatedPrimitiveRenderer>();
		List<IPixelatedPrimitiveRenderer> afterNPCs = new List<IPixelatedPrimitiveRenderer>();
		List<IPixelatedPrimitiveRenderer> beforeProjectiles = new List<IPixelatedPrimitiveRenderer>();
		List<IPixelatedPrimitiveRenderer> afterProjectiles = new List<IPixelatedPrimitiveRenderer>();
		List<IPixelatedPrimitiveRenderer> afterPlayers = new List<IPixelatedPrimitiveRenderer>();
		List<IPixelatedPrimitiveRenderer> afterDusts = new List<IPixelatedPrimitiveRenderer>();
		List<IPixelatedPrimitiveRenderer> afterEverything = new List<IPixelatedPrimitiveRenderer>();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile projectile = enumerator.Current;
			if (projectile.ModProjectile != null && projectile.ModProjectile is IPixelatedPrimitiveRenderer pixelPrimitiveProjectile)
			{
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.BeforeAllTiles))
				{
					beforeAllTiles.Add(pixelPrimitiveProjectile);
				}
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.BeforeSolidTiles))
				{
					beforeSolidTiles.Add(pixelPrimitiveProjectile);
				}
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.BeforeNPCs))
				{
					beforeNPCs.Add(pixelPrimitiveProjectile);
				}
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterNPCs))
				{
					afterNPCs.Add(pixelPrimitiveProjectile);
				}
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.BeforeProjectiles))
				{
					beforeProjectiles.Add(pixelPrimitiveProjectile);
				}
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterProjectiles))
				{
					afterProjectiles.Add(pixelPrimitiveProjectile);
				}
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterPlayers))
				{
					afterPlayers.Add(pixelPrimitiveProjectile);
				}
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterDusts))
				{
					afterDusts.Add(pixelPrimitiveProjectile);
				}
				if (pixelPrimitiveProjectile.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterEverything))
				{
					afterEverything.Add(pixelPrimitiveProjectile);
				}
			}
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			NPC npc = enumerator2.Current;
			if (npc.ModNPC != null && npc.ModNPC is IPixelatedPrimitiveRenderer pixelPrimitiveNPC)
			{
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.BeforeAllTiles))
				{
					beforeAllTiles.Add(pixelPrimitiveNPC);
				}
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.BeforeSolidTiles))
				{
					beforeSolidTiles.Add(pixelPrimitiveNPC);
				}
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.BeforeNPCs))
				{
					beforeNPCs.Add(pixelPrimitiveNPC);
				}
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterNPCs))
				{
					afterNPCs.Add(pixelPrimitiveNPC);
				}
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.BeforeProjectiles))
				{
					beforeProjectiles.Add(pixelPrimitiveNPC);
				}
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterProjectiles))
				{
					afterProjectiles.Add(pixelPrimitiveNPC);
				}
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterPlayers))
				{
					afterPlayers.Add(pixelPrimitiveNPC);
				}
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterDusts))
				{
					afterDusts.Add(pixelPrimitiveNPC);
				}
				if (pixelPrimitiveNPC.LayerToRenderTo.HasFlag(GeneralDrawLayer.AfterEverything))
				{
					afterEverything.Add(pixelPrimitiveNPC);
				}
			}
		}
		CurrentlyRendering = true;
		DrawPrimsToRenderTarget(PixelationTarget_BeforeAllTiles.Target, GeneralDrawLayer.BeforeAllTiles, beforeAllTiles);
		DrawPrimsToRenderTarget(PixelationTarget_BeforeSolidTiles.Target, GeneralDrawLayer.BeforeSolidTiles, beforeSolidTiles);
		DrawPrimsToRenderTarget(PixelationTarget_BeforeNPCs.Target, GeneralDrawLayer.BeforeNPCs, beforeNPCs);
		DrawPrimsToRenderTarget(PixelationTarget_AfterNPCs.Target, GeneralDrawLayer.AfterNPCs, afterNPCs);
		DrawPrimsToRenderTarget(PixelationTarget_BeforeProjectiles.Target, GeneralDrawLayer.BeforeProjectiles, beforeProjectiles);
		DrawPrimsToRenderTarget(PixelationTarget_AfterProjectiles.Target, GeneralDrawLayer.AfterProjectiles, afterProjectiles);
		DrawPrimsToRenderTarget(PixelationTarget_AfterPlayers.Target, GeneralDrawLayer.AfterPlayers, afterPlayers);
		DrawPrimsToRenderTarget(PixelationTarget_AfterDusts.Target, GeneralDrawLayer.AfterDusts, afterDusts);
		DrawPrimsToRenderTarget(PixelationTarget_AfterEverything.Target, GeneralDrawLayer.AfterEverything, afterEverything);
		CurrentlyRendering = false;
	}

	private static void DrawPrimsToRenderTarget(RenderTarget2D renderTarget, GeneralDrawLayer layer, List<IPixelatedPrimitiveRenderer> pixelPrimitives)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		using (renderTarget.Scope(preserveContents: true, Color.Transparent))
		{
			if (!pixelPrimitives.Any())
			{
				return;
			}
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null);
			foreach (IPixelatedPrimitiveRenderer pixelPrimitive in pixelPrimitives)
			{
				pixelPrimitive.RenderPixelatedPrimitives(Main.spriteBatch, layer);
			}
			Main.spriteBatch.End();
		}
	}

	private static void DrawTargetScaled(GeneralDrawLayer drawLayer)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Main.spriteBatch.Draw((Texture2D)(object)ReturnAssociatedRenderTarget(drawLayer), Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, 2f, (SpriteEffects)0, 0f);
		Main.spriteBatch.End();
	}

	private static RenderTarget2D ReturnAssociatedRenderTarget(GeneralDrawLayer drawLayer)
	{
		return (RenderTarget2D)(drawLayer switch
		{
			GeneralDrawLayer.BeforeAllTiles => PixelationTarget_BeforeAllTiles.Target, 
			GeneralDrawLayer.BeforeSolidTiles => PixelationTarget_BeforeSolidTiles.Target, 
			GeneralDrawLayer.BeforeNPCs => PixelationTarget_BeforeNPCs.Target, 
			GeneralDrawLayer.AfterNPCs => PixelationTarget_AfterNPCs.Target, 
			GeneralDrawLayer.BeforeProjectiles => PixelationTarget_BeforeProjectiles.Target, 
			GeneralDrawLayer.AfterProjectiles => PixelationTarget_AfterProjectiles.Target, 
			GeneralDrawLayer.AfterPlayers => PixelationTarget_AfterPlayers.Target, 
			GeneralDrawLayer.AfterDusts => PixelationTarget_AfterDusts.Target, 
			_ => PixelationTarget_AfterEverything.Target, 
		});
	}
}
