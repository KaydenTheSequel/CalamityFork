using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Graphic.LiquidSystem;

[Autoload(true, Side = ModSide.Client)]
public sealed class SpecialLiquidDrawingSystem : ModSystem
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static hook_GetTileLight _003C0_003E__ApplyLiquidEmit;

		public static Manipulator _003C1_003E__LiquidDrawColorAndPostDraw;

		public static hook_DrawPartialLiquid _003C2_003E__LiquidSlopeDrawColors;

		public static Manipulator _003C3_003E__OldLiquidPostDraw;

		public static hook_DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects _003C4_003E__ModifyWaterfallColor;
	}

	public override void OnModLoad()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		object obj = _003C_003EO._003C0_003E__ApplyLiquidEmit;
		if (obj == null)
		{
			hook_GetTileLight val = ApplyLiquidEmit;
			_003C_003EO._003C0_003E__ApplyLiquidEmit = val;
			obj = (object)val;
		}
		On_TileLightScanner.GetTileLight += (hook_GetTileLight)obj;
		object obj2 = _003C_003EO._003C1_003E__LiquidDrawColorAndPostDraw;
		if (obj2 == null)
		{
			Manipulator val2 = LiquidDrawColorAndPostDraw;
			_003C_003EO._003C1_003E__LiquidDrawColorAndPostDraw = val2;
			obj2 = (object)val2;
		}
		IL_LiquidRenderer.DrawNormalLiquids += (Manipulator)obj2;
		object obj3 = _003C_003EO._003C2_003E__LiquidSlopeDrawColors;
		if (obj3 == null)
		{
			hook_DrawPartialLiquid val3 = LiquidSlopeDrawColors;
			_003C_003EO._003C2_003E__LiquidSlopeDrawColors = val3;
			obj3 = (object)val3;
		}
		On_TileDrawing.DrawPartialLiquid += (hook_DrawPartialLiquid)obj3;
		object obj4 = _003C_003EO._003C3_003E__OldLiquidPostDraw;
		if (obj4 == null)
		{
			Manipulator val4 = OldLiquidPostDraw;
			_003C_003EO._003C3_003E__OldLiquidPostDraw = val4;
			obj4 = (object)val4;
		}
		IL_Main.oldDrawWater += (Manipulator)obj4;
		object obj5 = _003C_003EO._003C4_003E__ModifyWaterfallColor;
		if (obj5 == null)
		{
			hook_DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects val5 = ModifyWaterfallColor;
			_003C_003EO._003C4_003E__ModifyWaterfallColor = val5;
			obj5 = (object)val5;
		}
		On_WaterfallManager.DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects += (hook_DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects)obj5;
	}

	private static void ModifyEmit(Tile tile, int x, int y, ref Vector3 lightColor)
	{
		if (!tile.HasTile && tile.LiquidAmount > 0)
		{
			if (tile.LiquidType == 0 && GetModWaterStyle(Main.waterStyle) is IWaterStyleModifyLight waterStyle)
			{
				float R = 0f;
				float G = 0f;
				float B = 0f;
				waterStyle.ModifyLight(in tile, x, y, ref R, ref G, ref B);
				lightColor.X = Math.Max(lightColor.X, R);
				lightColor.Y = Math.Max(lightColor.Y, G);
				lightColor.Z = Math.Max(lightColor.Z, B);
			}
			else if (tile.LiquidType == 1 && ModLavaStyleSystem.Initialized)
			{
				ModLavaStyleSystem.ModifyLightBlended(x, y, ref lightColor.X, ref lightColor.Y, ref lightColor.Z);
			}
		}
	}

	private static void ModifyColor(int x, int y, int liquidStyle, ref VertexColors initialColor, bool isSlope = false)
	{
		if (GetModWaterStyle(liquidStyle) is IWaterStyleModifyColor waterStyle)
		{
			waterStyle.ModifyColor(Main.tile[x, y], x, y, ref initialColor, isSlope);
		}
		else if (liquidStyle == 1 && ModLavaStyleSystem.Initialized)
		{
			ModLavaStyleSystem.ModifyColorBlended(x, y, ref initialColor, isSlope);
		}
	}

	private static void PostDrawEffect(int x, int y, int liquidStyle)
	{
		if (GetModWaterStyle(liquidStyle) is IWaterStylePostDrawEffect waterStyle)
		{
			waterStyle.PostDrawEffect(Main.tile[x, y], x, y);
		}
	}

	private static void ApplyLiquidEmit(orig_GetTileLight orig, TileLightScanner self, int x, int y, out Vector3 outputColor)
	{
		orig.Invoke(self, x, y, ref outputColor);
		ModifyEmit(Main.tile[x, y], x, y, ref outputColor);
	}

	private static void LiquidDrawColorAndPostDraw(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		FieldInfo typeField = typeof(LiquidRenderer.LiquidDrawCache).GetField("Type");
		if (typeField == null)
		{
			CalamityMod.Log.ILFailure("Liquid Draw Colors", "Could not find FieldInfo for LiquidDrawCache.Type");
			return;
		}
		MethodInfo lightingGetCornerColorsMethod = typeof(Lighting).GetMethod("GetCornerColors");
		if (lightingGetCornerColorsMethod == null)
		{
			CalamityMod.Log.ILFailure("Liquid Draw Colors", "Could not find MethodInfo for GetCornerColors");
			return;
		}
		MethodInfo mainDrawTileInWaterMethod = typeof(Main).GetMethod("DrawTileInWater");
		if (mainDrawTileInWaterMethod == null)
		{
			CalamityMod.Log.ILFailure("Liquid Draw Colors", "Could not find FieldInfo for DrawTileInWater");
			return;
		}
		int liquidStyleLocalIdx = 0;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[3]
		{
			(Instruction c) =>
			{
				int num = default(int);
				return ILPatternMatchingExt.MatchLdloc(c, ref num);
			},
			(Instruction c) => ILPatternMatchingExt.MatchLdfld(c, typeField),
			(Instruction c) => ILPatternMatchingExt.MatchStloc(c, ref liquidStyleLocalIdx)
		}))
		{
			CalamityMod.Log.ILFailure("Liquid Draw Colors", "Could not locate the local index for Liquid Type (Style)");
			return;
		}
		int vertexColorLocalIdx = 0;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[3]
		{
			(Instruction c) => ILPatternMatchingExt.MatchLdloca(c, ref vertexColorLocalIdx),
			(Instruction c) =>
			{
				float num = default(float);
				return ILPatternMatchingExt.MatchLdcR4(c, ref num);
			},
			(Instruction c) => ILPatternMatchingExt.MatchCallOrCallvirt(c, (MethodBase)lightingGetCornerColorsMethod)
		}))
		{
			CalamityMod.Log.ILFailure("Liquid Draw Colors", "Could not locate the local index for VertexColors");
			return;
		}
		int xLocalIdx = 0;
		int yLocalIdx = 0;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[4]
		{
			(Instruction c) =>
			{
				int num = default(int);
				return ILPatternMatchingExt.MatchLdarg(c, ref num);
			},
			(Instruction c) => ILPatternMatchingExt.MatchLdloc(c, ref xLocalIdx),
			(Instruction c) => ILPatternMatchingExt.MatchLdloc(c, ref yLocalIdx),
			(Instruction c) => ILPatternMatchingExt.MatchCallOrCallvirt(c, (MethodBase)mainDrawTileInWaterMethod)
		}))
		{
			CalamityMod.Log.ILFailure("Liquid Draw Colors", "Could not locate the liquid vertex colors for drawing");
			return;
		}
		cursor.EmitLdloc(liquidStyleLocalIdx);
		cursor.EmitLdloc(xLocalIdx);
		cursor.EmitLdloc(yLocalIdx);
		cursor.EmitLdloca(vertexColorLocalIdx);
		cursor.EmitDelegate<_003C_003EA_007B00000200_007D<int, int, int, VertexColors>>((_003C_003EA_007B00000200_007D<int, int, int, VertexColors>)delegate(int liquidStyle, int x, int y, ref VertexColors initialColor)
		{
			ModifyColor(x, y, liquidStyle, ref initialColor);
		});
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[1]
		{
			(Instruction c) => ILPatternMatchingExt.MatchCallOrCallvirt<TileBatch>(c, "Draw")
		}))
		{
			CalamityMod.Log.ILFailure("Liquid Draw Colors", "Could not locate TileBatch.Draw call");
			return;
		}
		cursor.EmitLdloc(liquidStyleLocalIdx);
		cursor.EmitLdloc(xLocalIdx);
		cursor.EmitLdloc(yLocalIdx);
		cursor.EmitDelegate<Action<int, int, int>>((Action<int, int, int>)delegate(int liquidStyle, int x, int y)
		{
			PostDrawEffect(x, y, liquidStyle);
		});
	}

	private static void LiquidSlopeDrawColors(orig_DrawPartialLiquid orig, TileDrawing self, bool behindBlocks, Tile tileCache, ref Vector2 position, ref Rectangle liquidSize, int liquidType, ref VertexColors colors)
	{
		tileCache.TilePos(out var x, out var y);
		ushort type = tileCache.TileType;
		bool isFullblock = type == 0 || (!TileID.Sets.BlocksWaterDrawingBehindSelf[type] & behindBlocks);
		ModifyColor(x, y, liquidType, ref colors, !isFullblock);
		orig.Invoke(self, behindBlocks, tileCache, ref position, ref liquidSize, liquidType, ref colors);
	}

	private static void OldLiquidPostDraw(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		VariableDefinition isDrawnVarDef = new VariableDefinition(cursor.Context.Import(typeof(bool)));
		il.Body.Variables.Add(isDrawnVarDef);
		cursor.EmitLdcI4(0);
		cursor.EmitStloc((VariableReference)(object)isDrawnVarDef);
		int xLocalIdx = 0;
		int yLocalIdx = 0;
		ILLabel endLoopLabal = null;
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[5]
		{
			(Instruction c) => ILPatternMatchingExt.MatchBrfalse(c, ref endLoopLabal),
			(Instruction c) => ILPatternMatchingExt.MatchLdloc(c, ref xLocalIdx),
			(Instruction c) => ILPatternMatchingExt.MatchLdloc(c, ref yLocalIdx),
			(Instruction c) => ILPatternMatchingExt.MatchCallOrCallvirt<Lighting>(c, "GetColor"),
			(Instruction c) =>
			{
				int num = default(int);
				return ILPatternMatchingExt.MatchStloc(c, ref num);
			}
		}))
		{
			CalamityMod.Log.ILFailure("Old Liquid PostDraw", "Could not locate Lighting.GetColor call");
			return;
		}
		cursor.EmitLdcI4(1);
		cursor.EmitStloc((VariableReference)(object)isDrawnVarDef);
		cursor.GotoLabel(endLoopLabal, (MoveType)1, false);
		cursor.EmitLdloc((VariableReference)(object)isDrawnVarDef);
		cursor.EmitLdloc(xLocalIdx);
		cursor.EmitLdloc(yLocalIdx);
		cursor.EmitLdarg(1);
		cursor.EmitLdarg(2);
		cursor.EmitDelegate<Action<bool, int, int, bool, int>>((Action<bool, int, int, bool, int>)delegate(bool isDrawn, int x, int y, bool isBackground, int waterStyle)
		{
			if (!(!isDrawn | isBackground) && Main.waterStyle == waterStyle && Main.tile[x, y].LiquidType == 0)
			{
				PostDrawEffect(x, y, waterStyle);
			}
		});
		cursor.EmitLdcI4(0);
		cursor.EmitStloc((VariableReference)(object)isDrawnVarDef);
	}

	private static void ModifyWaterfallColor(orig_DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects orig, WaterfallManager self, int waterfallType, int x, int y, float opacity, Vector2 position, Rectangle sourceRect, Color color, SpriteEffects effects)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (GetModWaterfallStyle(waterfallType) is IWaterfallStyleModifyColor style)
		{
			Tile tile = Main.tile[x, y];
			Texture2D texture = self.waterfallTexture[waterfallType].Value;
			Lighting.GetCornerColors(x, y, out var vertices);
			style.ModifyColor(in tile, x, y, ref vertices);
			Main.tileBatch.Draw(texture, position, sourceRect, vertices, Vector2.Zero, 1f, effects);
		}
		else
		{
			orig.Invoke(self, waterfallType, x, y, opacity, position, sourceRect, color, effects);
		}
	}

	private static ModWaterStyle GetModWaterStyle(int type)
	{
		return LoaderManager.Get<WaterStylesLoader>().Get(type);
	}

	private static ModWaterfallStyle GetModWaterfallStyle(int type)
	{
		return LoaderManager.Get<WaterFallStylesLoader>().Get(type);
	}
}
