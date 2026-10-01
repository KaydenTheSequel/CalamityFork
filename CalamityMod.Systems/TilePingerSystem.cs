using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class TilePingerSystem : ModSystem
{
	private class GlobalPingableTile : GlobalTile
	{
		public override void DrawEffects(int i, int j, int type, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			IPingedTileEffect[] tileEffects = TilePingerSystem.tileEffects;
			foreach (IPingedTileEffect effect in tileEffects)
			{
				if (effect.Active && effect.ShouldRegisterTile(i, j))
				{
					int tileType = Main.tile[i, j].TileType;
					bool solid = true;
					if (Lighting.Mode == LightMode.Color)
					{
						solid = ((!TileID.Sets.DrawTileInSolidLayer[tileType].HasValue) ? Main.tileSolid[tileType] : TileID.Sets.DrawTileInSolidLayer[tileType].Value);
					}
					RegisterTileToDraw(new Point(i, j), effect, solid);
					effect.EditDrawData(i, j, ref drawData);
				}
			}
		}
	}

	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static hook_DrawTiles_GetLightOverride _003C0_003E__ForceSufficientLight;
	}

	private static Dictionary<IPingedTileEffect, List<Point>> pingedTiles = new Dictionary<IPingedTileEffect, List<Point>>();

	private static Dictionary<IPingedTileEffect, List<Point>> pingedNonSolidTiles = new Dictionary<IPingedTileEffect, List<Point>>();

	private static Dictionary<IPingedTileEffect, List<Point>> drawCache = new Dictionary<IPingedTileEffect, List<Point>>();

	private static Dictionary<string, IPingedTileEffect> tileEffectLookup = new Dictionary<string, IPingedTileEffect>();

	private static IPingedTileEffect[] tileEffects = Array.Empty<IPingedTileEffect>();

	public static void RegisterEffect(string name, IPingedTileEffect tileEffect)
	{
		tileEffectLookup[name] = tileEffect;
		tileEffects = tileEffectLookup.Values.ToArray();
	}

	public override void Load()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (!Main.dedServ)
		{
			object obj = _003C_003EO._003C0_003E__ForceSufficientLight;
			if (obj == null)
			{
				hook_DrawTiles_GetLightOverride val = ForceSufficientLight;
				_003C_003EO._003C0_003E__ForceSufficientLight = val;
				obj = (object)val;
			}
			On_TileDrawing.DrawTiles_GetLightOverride += (hook_DrawTiles_GetLightOverride)obj;
		}
	}

	public override void Unload()
	{
		drawCache = null;
		pingedTiles = null;
		pingedNonSolidTiles = null;
		tileEffectLookup = null;
		tileEffects = null;
	}

	private static Color ForceSufficientLight(orig_DrawTiles_GetLightOverride orig, TileDrawing self, int j, int i, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, Color tileLight)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Color returnColor = orig.Invoke(self, j, i, tileCache, typeCache, tileFrameX, tileFrameY, tileLight);
		IPingedTileEffect[] array = tileEffects;
		foreach (IPingedTileEffect effect in array)
		{
			if (effect.Active && effect.ShouldRegisterTile(i, j))
			{
				effect.ModifyTileLight(i, j, tileLight, ref returnColor);
			}
		}
		return returnColor;
	}

	public static bool AddPing(IPingedTileEffect effect, Vector2 position, Player pinger)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return effect.TryAddPing(position, pinger);
	}

	public static bool AddPing(string effectName, Vector2 position, Player pinger)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			return AddPing(tileEffectLookup[effectName], position, pinger);
		}
		return false;
	}

	public static void RegisterTileToDraw(Point tilePos, string effectName, bool solid = true)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		RegisterTileToDraw(tilePos, tileEffectLookup[effectName], solid);
	}

	public static void RegisterTileToDraw(Point tilePos, IPingedTileEffect effect, bool solid = true)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		if (solid || Lighting.Mode != LightMode.Color)
		{
			if (!pingedTiles.ContainsKey(effect))
			{
				pingedTiles.Add(effect, new List<Point>());
			}
			if (!pingedTiles[effect].Contains(tilePos))
			{
				pingedTiles[effect].Add(tilePos);
			}
		}
		else
		{
			if (!pingedNonSolidTiles.ContainsKey(effect))
			{
				pingedNonSolidTiles.Add(effect, new List<Point>());
			}
			if (!pingedNonSolidTiles[effect].Contains(tilePos))
			{
				pingedNonSolidTiles[effect].Add(tilePos);
			}
		}
	}

	public override void PostUpdateEverything()
	{
		if (!Main.dedServ && tileEffects != null)
		{
			IPingedTileEffect[] array = tileEffects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UpdateEffect();
			}
		}
	}

	public override void PostDrawTiles()
	{
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		if (pingedTiles == null || pingedTiles.Keys.Count + pingedNonSolidTiles.Count < 1)
		{
			return;
		}
		drawCache.Clear();
		foreach (IPingedTileEffect solidEffect in pingedTiles.Keys)
		{
			drawCache.Add(solidEffect, pingedTiles[solidEffect].ConvertAll((Converter<Point, Point>)delegate(Point position)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				return new Point(position.X, position.Y);
			}));
		}
		if (Lighting.Mode == LightMode.Color)
		{
			foreach (IPingedTileEffect nonSolidEffect in pingedNonSolidTiles.Keys)
			{
				List<Point> clonedList = pingedNonSolidTiles[nonSolidEffect].ConvertAll((Converter<Point, Point>)delegate(Point position)
				{
					//IL_0000: Unknown result type (might be due to invalid IL or missing references)
					//IL_0006: Unknown result type (might be due to invalid IL or missing references)
					//IL_000c: Unknown result type (might be due to invalid IL or missing references)
					return new Point(position.X, position.Y);
				});
				if (!drawCache.TryGetValue(nonSolidEffect, out var value))
				{
					drawCache.Add(nonSolidEffect, clonedList);
				}
				else
				{
					value.AddRange(clonedList);
				}
			}
		}
		foreach (IPingedTileEffect tileEffect in drawCache.Keys)
		{
			Effect effect = tileEffect.SetupEffect();
			Main.spriteBatch.Begin((SpriteSortMode)1, tileEffect.BlendState, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, effect, Main.GameViewMatrix.TransformationMatrix);
			foreach (Point tilePos in drawCache[tileEffect])
			{
				tileEffect.PerTileSetup(tilePos, ref effect);
				tileEffect.DrawTile(tilePos);
			}
			Main.spriteBatch.End();
		}
	}

	public static void ClearTiles()
	{
		ClearTiles(solid: true);
		ClearTiles(solid: false);
	}

	public static void ClearTiles(bool solid)
	{
		if (solid)
		{
			pingedTiles.Clear();
		}
		else
		{
			pingedNonSolidTiles.Clear();
		}
	}
}
