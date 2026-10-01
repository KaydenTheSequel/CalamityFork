using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Graphic.PixelationSystem;

public class PixelationManager : ModSystem
{
	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_BeforeAllTiles;

	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_BeforeSolidTiles;

	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_BeforeNPCs;

	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_AfterNPCs;

	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_BeforeProjectiles;

	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_AfterProjectiles;

	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_AfterPlayers;

	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_AfterDusts;

	private static Dictionary<BlendState, RenderTargetLease> PixelTargets_AfterEverything;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_BeforeAllTiles;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_BeforeSolidTiles;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_BeforeNPCs;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_AfterNPCs;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_BeforeProjectiles;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_AfterProjectiles;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_AfterPlayers;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_AfterDusts;

	private static List<PixelatedDrawer> ActivePixelatedDrawers_AfterEverything;

	internal const float PixelationResolution = 0.5f;

	internal static Matrix PixelationMatrix
	{
		get
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			return Main.GameViewMatrix.TransformationMatrix * Matrix.CreateScale(0.5f / Main.GameViewMatrix.Zoom.X, 0.5f / Main.GameViewMatrix.Zoom.Y, 1f) * Matrix.CreateTranslation(Main.GameViewMatrix.Translation.X * 0.5f, Main.GameViewMatrix.Translation.Y * 0.5f, 0f);
		}
	}

	public override void Load()
	{
		if (!Main.dedServ)
		{
			PixelTargets_BeforeAllTiles = new Dictionary<BlendState, RenderTargetLease>();
			PixelTargets_BeforeSolidTiles = new Dictionary<BlendState, RenderTargetLease>();
			PixelTargets_BeforeNPCs = new Dictionary<BlendState, RenderTargetLease>();
			PixelTargets_AfterNPCs = new Dictionary<BlendState, RenderTargetLease>();
			PixelTargets_BeforeProjectiles = new Dictionary<BlendState, RenderTargetLease>();
			PixelTargets_AfterProjectiles = new Dictionary<BlendState, RenderTargetLease>();
			PixelTargets_AfterPlayers = new Dictionary<BlendState, RenderTargetLease>();
			PixelTargets_AfterDusts = new Dictionary<BlendState, RenderTargetLease>();
			PixelTargets_AfterEverything = new Dictionary<BlendState, RenderTargetLease>();
			ActivePixelatedDrawers_BeforeAllTiles = new List<PixelatedDrawer>();
			ActivePixelatedDrawers_BeforeSolidTiles = new List<PixelatedDrawer>();
			ActivePixelatedDrawers_BeforeNPCs = new List<PixelatedDrawer>();
			ActivePixelatedDrawers_AfterNPCs = new List<PixelatedDrawer>();
			ActivePixelatedDrawers_BeforeProjectiles = new List<PixelatedDrawer>();
			ActivePixelatedDrawers_AfterProjectiles = new List<PixelatedDrawer>();
			ActivePixelatedDrawers_AfterPlayers = new List<PixelatedDrawer>();
			ActivePixelatedDrawers_AfterDusts = new List<PixelatedDrawer>();
			ActivePixelatedDrawers_AfterEverything = new List<PixelatedDrawer>();
			GeneralDrawLayerSystem.OnDrawLayerLate += DrawPixelatedTargets;
			GeneralDrawLayerSystem.OnPrepareDraw += PrepareTargets;
		}
	}

	public override void Unload()
	{
		if (!Main.dedServ)
		{
			PixelTargets_BeforeAllTiles = null;
			PixelTargets_BeforeSolidTiles = null;
			PixelTargets_BeforeNPCs = null;
			PixelTargets_AfterNPCs = null;
			PixelTargets_BeforeProjectiles = null;
			PixelTargets_AfterProjectiles = null;
			PixelTargets_AfterPlayers = null;
			PixelTargets_AfterDusts = null;
			PixelTargets_AfterEverything = null;
			ActivePixelatedDrawers_BeforeAllTiles = null;
			ActivePixelatedDrawers_BeforeSolidTiles = null;
			ActivePixelatedDrawers_BeforeNPCs = null;
			ActivePixelatedDrawers_AfterNPCs = null;
			ActivePixelatedDrawers_BeforeProjectiles = null;
			ActivePixelatedDrawers_AfterProjectiles = null;
			ActivePixelatedDrawers_AfterPlayers = null;
			ActivePixelatedDrawers_AfterDusts = null;
			ActivePixelatedDrawers_AfterEverything = null;
		}
	}

	public override void OnWorldUnload()
	{
		if (!Main.dedServ)
		{
			PixelTargets_BeforeAllTiles.Clear();
			PixelTargets_BeforeSolidTiles.Clear();
			PixelTargets_BeforeNPCs.Clear();
			PixelTargets_AfterNPCs.Clear();
			PixelTargets_BeforeProjectiles.Clear();
			PixelTargets_AfterProjectiles.Clear();
			PixelTargets_AfterPlayers.Clear();
			PixelTargets_AfterDusts.Clear();
			PixelTargets_AfterEverything.Clear();
			ActivePixelatedDrawers_BeforeAllTiles.Clear();
			ActivePixelatedDrawers_BeforeSolidTiles.Clear();
			ActivePixelatedDrawers_BeforeNPCs.Clear();
			ActivePixelatedDrawers_AfterNPCs.Clear();
			ActivePixelatedDrawers_BeforeProjectiles.Clear();
			ActivePixelatedDrawers_AfterProjectiles.Clear();
			ActivePixelatedDrawers_AfterPlayers.Clear();
			ActivePixelatedDrawers_AfterDusts.Clear();
			ActivePixelatedDrawers_AfterEverything.Clear();
		}
	}

	public static void AddPixelatedDrawer(Action<Matrix> drawAction, GeneralDrawLayer drawLayer, BlendState defaultBlendState = null)
	{
		if (!Main.dedServ && !Main.gameMenu)
		{
			BlendState blendState = defaultBlendState ?? BlendState.AlphaBlend;
			VerifyTargetExistence(drawLayer, blendState);
			PixelatedDrawer drawer = new PixelatedDrawer(drawAction, drawLayer, blendState);
			ReturnAssociatedDrawerCollection(drawLayer).Add(drawer);
		}
	}

	private static void PrepareTargets()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.gameMenu && !Main.dedServ)
		{
			DrawCollectionsToTarget(PixelTargets_BeforeAllTiles, PixelationMatrix, ActivePixelatedDrawers_BeforeAllTiles);
			DrawCollectionsToTarget(PixelTargets_BeforeSolidTiles, PixelationMatrix, ActivePixelatedDrawers_BeforeSolidTiles);
			DrawCollectionsToTarget(PixelTargets_BeforeNPCs, PixelationMatrix, ActivePixelatedDrawers_BeforeNPCs);
			DrawCollectionsToTarget(PixelTargets_AfterNPCs, PixelationMatrix, ActivePixelatedDrawers_AfterNPCs);
			DrawCollectionsToTarget(PixelTargets_BeforeProjectiles, PixelationMatrix, ActivePixelatedDrawers_BeforeProjectiles);
			DrawCollectionsToTarget(PixelTargets_AfterProjectiles, PixelationMatrix, ActivePixelatedDrawers_AfterProjectiles);
			DrawCollectionsToTarget(PixelTargets_AfterPlayers, PixelationMatrix, ActivePixelatedDrawers_AfterPlayers);
			DrawCollectionsToTarget(PixelTargets_AfterDusts, PixelationMatrix, ActivePixelatedDrawers_AfterDusts);
			DrawCollectionsToTarget(PixelTargets_AfterEverything, PixelationMatrix, ActivePixelatedDrawers_AfterEverything);
		}
	}

	private static void DrawCollectionsToTarget(Dictionary<BlendState, RenderTargetLease> targetCollection, Matrix pixelationMatrix, List<PixelatedDrawer> drawerCollection)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<BlendState, RenderTargetLease> blendStateTargetPair in targetCollection)
		{
			using (blendStateTargetPair.Value.Scope(preserveContents: true, Color.Transparent))
			{
				Main.spriteBatch.Begin((SpriteSortMode)0, blendStateTargetPair.Key, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, pixelationMatrix);
				List<PixelatedDrawer> drawersByBlendState = drawerCollection.Where((PixelatedDrawer d) => d.DefaultBlendState == blendStateTargetPair.Key).ToList();
				if (drawersByBlendState.Count > 0)
				{
					foreach (PixelatedDrawer item in drawersByBlendState)
					{
						item.DrawAction(pixelationMatrix);
					}
				}
				Main.spriteBatch.End();
			}
		}
		drawerCollection.Clear();
	}

	private static void DrawPixelatedTargets(GeneralDrawLayer drawLayer)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<BlendState, RenderTargetLease> keyValuePair in ReturnAssociatedTargetCollection(drawLayer))
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, keyValuePair.Key, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			float targetScale = 2f;
			Main.spriteBatch.Draw((Texture2D)(object)keyValuePair.Value.Target, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, targetScale, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
		}
	}

	private static List<PixelatedDrawer> ReturnAssociatedDrawerCollection(GeneralDrawLayer drawLayer)
	{
		return drawLayer switch
		{
			GeneralDrawLayer.BeforeAllTiles => ActivePixelatedDrawers_BeforeAllTiles, 
			GeneralDrawLayer.BeforeSolidTiles => ActivePixelatedDrawers_BeforeSolidTiles, 
			GeneralDrawLayer.BeforeNPCs => ActivePixelatedDrawers_BeforeNPCs, 
			GeneralDrawLayer.AfterNPCs => ActivePixelatedDrawers_AfterNPCs, 
			GeneralDrawLayer.BeforeProjectiles => ActivePixelatedDrawers_BeforeProjectiles, 
			GeneralDrawLayer.AfterProjectiles => ActivePixelatedDrawers_AfterProjectiles, 
			GeneralDrawLayer.AfterPlayers => ActivePixelatedDrawers_AfterPlayers, 
			GeneralDrawLayer.AfterDusts => ActivePixelatedDrawers_AfterDusts, 
			_ => ActivePixelatedDrawers_AfterEverything, 
		};
	}

	private static Dictionary<BlendState, RenderTargetLease> ReturnAssociatedTargetCollection(GeneralDrawLayer drawLayer)
	{
		return drawLayer switch
		{
			GeneralDrawLayer.BeforeAllTiles => PixelTargets_BeforeAllTiles, 
			GeneralDrawLayer.BeforeSolidTiles => PixelTargets_BeforeSolidTiles, 
			GeneralDrawLayer.BeforeNPCs => PixelTargets_BeforeNPCs, 
			GeneralDrawLayer.AfterNPCs => PixelTargets_AfterNPCs, 
			GeneralDrawLayer.BeforeProjectiles => PixelTargets_BeforeProjectiles, 
			GeneralDrawLayer.AfterProjectiles => PixelTargets_AfterProjectiles, 
			GeneralDrawLayer.AfterPlayers => PixelTargets_AfterPlayers, 
			GeneralDrawLayer.AfterDusts => PixelTargets_AfterDusts, 
			_ => PixelTargets_AfterEverything, 
		};
	}

	private static void VerifyTargetExistence(GeneralDrawLayer drawLayer, BlendState blendState)
	{
		switch (drawLayer)
		{
		case GeneralDrawLayer.BeforeAllTiles:
			if (PixelTargets_BeforeAllTiles.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_BeforeAllTiles[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		case GeneralDrawLayer.BeforeSolidTiles:
			if (PixelTargets_BeforeSolidTiles.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_BeforeSolidTiles[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		case GeneralDrawLayer.BeforeNPCs:
			if (PixelTargets_BeforeNPCs.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_BeforeNPCs[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		case GeneralDrawLayer.AfterNPCs:
			if (PixelTargets_AfterNPCs.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_AfterNPCs[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		case GeneralDrawLayer.BeforeProjectiles:
			if (PixelTargets_BeforeProjectiles.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_BeforeProjectiles[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		case GeneralDrawLayer.AfterProjectiles:
			if (PixelTargets_AfterProjectiles.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_AfterProjectiles[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		case GeneralDrawLayer.AfterPlayers:
			if (PixelTargets_AfterPlayers.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_AfterPlayers[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		case GeneralDrawLayer.AfterDusts:
			if (PixelTargets_AfterDusts.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_AfterDusts[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		case GeneralDrawLayer.AfterEverything:
			if (PixelTargets_AfterEverything.ContainsKey(blendState))
			{
				break;
			}
			Main.QueueMainThreadAction(delegate
			{
				PixelTargets_AfterEverything[blendState] = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int w, int h) => ((int)((float)w * 0.5f), (int)((float)h * 0.5f)));
			});
			break;
		}
	}
}
