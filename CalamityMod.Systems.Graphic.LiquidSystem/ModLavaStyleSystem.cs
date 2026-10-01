using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using CalamityMod.ILEditing;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Graphic.LiquidSystem;

[Autoload(true, Side = ModSide.Client)]
[Autoload(true, Side = ModSide.Client)]
[Autoload(true, Side = ModSide.Client)]
[Autoload(true, Side = ModSide.Client)]
public sealed class ModLavaStyleSystem : ModSystem
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static Action<ManipulatorContext> _003C0_003E__ApplyEdits;

		public static Action _003C1_003E__PrepareRT;

		public static Action<GameTime> _003C2_003E__UpdateRT;

		public static Action _003C3_003E__DisposeRT;

		public static Manipulator _003C4_003E__DrawNormalLiquidPatch;

		public static Manipulator _003C5_003E__DrawPartialLiquidPatch;

		public static Manipulator _003C6_003E__DrawOldWaterPatch;

		public static hook_DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects _003C7_003E__DrawWaterfall;

		public static hook_AddLight _003C8_003E__WaterfallAddLight;

		public static hook_StylizeColor _003C9_003E__WaterfallGlowmaskEditor;

		public static Manipulator _003C10_003E__LavaBubbleReplacer;

		public static Manipulator _003C11_003E__LavaDropletReplacer;

		public static Manipulator _003C12_003E__SplashEntityLava;

		public static Manipulator _003C13_003E__PlayerDebuffEdit;

		public static Func<int, int> _003C14_003E__GetSplashDustID;

		public static Func<int, int> _003C15_003E__GetDropletGoreID;

		public static Action<Player, int> _003C16_003E__InflictDebuff;
	}

	public static ModLavaStyle[] LavaStyles;

	public static Asset<Texture2D>[] Textures;

	public static Asset<Texture2D>[] BlockTextures;

	public static Asset<Texture2D>[] SlopeTextures;

	public static Asset<Texture2D>[] WaterfallTextures;

	public static float[] LavaAlpha;

	public static int LavaStyle;

	private static readonly MethodInfo Tex2DAssetGetter;

	public const int TextureWidth = 48;

	public const int TextureHeight = 1360;

	public const int BlockTextureWidth = 16;

	public const int BlockTextureHeight = 16;

	public const int SlopeTextureWidth = 72;

	public const int SlopeTextureHeight = 16;

	public const int WaterfallTextureWidth = 512;

	public const int WaterfallTextureHeight = 40;

	private static readonly BlendState ActualAdditive;

	public static bool Initialized { get; private set; }

	public static bool TextureArrayReady { get; private set; }

	public static ModLavaStyle CurrentLavaStyle => LavaStyles[LavaStyle];

	public static RenderTarget2D LavaRT { get; private set; }

	public static RenderTarget2D LavaBlockRT { get; private set; }

	public static RenderTarget2D LavaSlopeRT { get; private set; }

	public static RenderTarget2D LavaWaterfallRT { get; private set; }

	public override void ResizeArrays()
	{
		IEnumerable<ModLavaStyle> allStyles = ModLavaStyleLoader.AllStyles;
		int totalCount = ModLavaStyleLoader.TotalCount;
		Array.Resize(ref LavaStyles, totalCount);
		Array.Resize(ref Textures, totalCount);
		Array.Resize(ref BlockTextures, totalCount);
		Array.Resize(ref SlopeTextures, totalCount);
		Array.Resize(ref WaterfallTextures, totalCount);
		Array.Resize(ref LavaAlpha, totalCount);
		foreach (ModLavaStyle modLavaStyle in allStyles)
		{
			int slot = modLavaStyle.Slot;
			LavaStyles[slot] = modLavaStyle;
			Textures[slot] = ModContent.Request<Texture2D>(modLavaStyle.Texture, (AssetRequestMode)2);
			BlockTextures[slot] = ModContent.Request<Texture2D>(modLavaStyle.BlockTexture, (AssetRequestMode)2);
			SlopeTextures[slot] = ModContent.Request<Texture2D>(modLavaStyle.SlopeTexture, (AssetRequestMode)2);
			WaterfallTextures[slot] = ModContent.Request<Texture2D>(modLavaStyle.WaterfallTexture, (AssetRequestMode)2);
		}
		LavaAlpha[0] = 1f;
		Textures[0] = LiquidRenderer.Instance._liquidTextures[1];
		SlopeTextures[0] = TextureAssets.LiquidSlope[1];
		BlockTextures[0] = TextureAssets.Liquid[1];
		WaterfallTextures[0] = Main.instance.waterfallManager.waterfallTexture[1];
		TextureArrayReady = true;
	}

	public override void Load()
	{
		base.Load();
		if (ExternalMods.biomeLava == null)
		{
			ManipulatorManager.ApplyEdits += ApplyEdits;
			Main.QueueMainThreadAction(PrepareRT);
			Main.OnPreDraw += UpdateRT;
			Initialized = true;
		}
	}

	public override void OnModUnload()
	{
		if (Initialized)
		{
			Main.QueueMainThreadAction(DisposeRT);
			Main.OnPreDraw -= UpdateRT;
			Initialized = false;
		}
		TextureArrayReady = false;
	}

	public override void PreUpdatePlayers()
	{
		LavaStyle = 0;
		foreach (ModLavaStyle lavaStyle in ModLavaStyleLoader.AllStyles)
		{
			if (lavaStyle?.IsLavaActive() ?? false)
			{
				LavaStyle = lavaStyle.Slot;
			}
		}
		for (int type = 0; type < ModLavaStyleLoader.TotalCount; type++)
		{
			if (LavaStyle == type)
			{
				LavaAlpha[type] += 0.125f;
				if (LavaAlpha[type] > 1f)
				{
					LavaAlpha[type] = 1f;
				}
			}
			else
			{
				LavaAlpha[type] -= 0.125f;
				if (LavaAlpha[type] < 0f)
				{
					LavaAlpha[type] = 0f;
				}
			}
		}
	}

	public static void ModifyLightBlended(int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		Vector3 vanillaLavaLight = default(Vector3);
		((Vector3)(ref vanillaLavaLight))._002Ector(0.55f, 0.33f, 0.11f);
		Vector3 lavaEmit = ((LavaStyle == 0) ? vanillaLavaLight : Vector3.Zero);
		ModifyLightSetup(i, j, LavaStyle, ref lavaEmit.X, ref lavaEmit.Y, ref lavaEmit.Z);
		for (int styleIndex = 0; styleIndex < ModLavaStyleLoader.TotalCount; styleIndex++)
		{
			if (LavaAlpha[styleIndex] > 0f && styleIndex != LavaStyle)
			{
				Vector3 propagatingColor = ((styleIndex == 0) ? vanillaLavaLight : Vector3.Zero);
				ModifyLightSetup(i, j, styleIndex, ref propagatingColor.X, ref propagatingColor.Y, ref propagatingColor.Z);
				Vector3 activeColor = ((LavaStyle == 0) ? vanillaLavaLight : Vector3.Zero);
				ModifyLightSetup(i, j, LavaStyle, ref activeColor.Z, ref activeColor.Y, ref activeColor.Z);
				lavaEmit = Vector3.Lerp(propagatingColor, activeColor, LavaAlpha[LavaStyle]);
			}
		}
		if (lavaEmit.X != 0f || lavaEmit.Y != 0f || lavaEmit.Z != 0f)
		{
			float colorManipulator = (float)(270 - Main.mouseTextColor) / 900f;
			lavaEmit += Vector3.One * colorManipulator;
		}
		r = Math.Max(r, lavaEmit.X);
		g = Math.Max(g, lavaEmit.Y);
		b = Math.Max(b, lavaEmit.Z);
	}

	public static void ModifyColorBlended(int i, int j, ref VertexColors initialColor, bool isSlope)
	{
		VertexColors color = initialColor;
		DrawColorSetup(i, j, LavaStyle, ref color, isSlope);
		for (int styleIndex = 0; styleIndex < ModLavaStyleLoader.TotalCount; styleIndex++)
		{
			if (LavaAlpha[styleIndex] > 0f && styleIndex != LavaStyle)
			{
				VertexColors propagatingColor = initialColor;
				DrawColorSetup(i, j, styleIndex, ref propagatingColor, isSlope);
				VertexColors activeColor = initialColor;
				DrawColorSetup(i, j, LavaStyle, ref activeColor, isSlope);
				color = LerpColors(propagatingColor, activeColor, LavaAlpha[LavaStyle]);
			}
		}
		initialColor = color;
		static VertexColors LerpColors(VertexColors a, VertexColors b, float amt)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			return new VertexColors
			{
				TopLeftColor = Color.Lerp(a.TopLeftColor, b.TopLeftColor, amt),
				TopRightColor = Color.Lerp(a.TopRightColor, b.TopRightColor, amt),
				BottomLeftColor = Color.Lerp(a.BottomLeftColor, b.BottomLeftColor, amt),
				BottomRightColor = Color.Lerp(a.BottomRightColor, b.BottomRightColor, amt)
			};
		}
	}

	public static void ModifyLightSetup(int i, int j, int style, ref float r, ref float g, ref float b)
	{
		LavaStyles[style]?.ModifyLight(i, j, ref r, ref g, ref b);
	}

	public static void DrawColorSetup(int x, int y, int style, ref VertexColors liquidColor, bool isSlope = false)
	{
		LavaStyles[style]?.DrawColor(x, y, ref liquidColor, isSlope);
	}

	public static int GetDropletGoreID(int oldID = -1)
	{
		ModLavaStyle lavaStyle = CurrentLavaStyle;
		if (lavaStyle != null)
		{
			return lavaStyle.GetDropletGore();
		}
		if (oldID < 0)
		{
			return 716;
		}
		return oldID;
	}

	public static int GetSplashDustID(int oldID = -1)
	{
		ModLavaStyle lavaStyle = CurrentLavaStyle;
		if (lavaStyle != null)
		{
			return lavaStyle.GetSplashDust();
		}
		if (oldID < 0)
		{
			return 35;
		}
		return oldID;
	}

	public static void InflictDebuff(Player player, int onFireTime)
	{
		CurrentLavaStyle?.InflictDebuff(player, onFireTime);
	}

	private static void ApplyEdits(ManipulatorContext ctx)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		if (Tex2DAssetGetter == null)
		{
			CalamityMod.Log.ILFailure("ModLavaStyle", "Cannot find Getter for Asset<Texture2D>::Value");
			return;
		}
		object obj = _003C_003EO._003C4_003E__DrawNormalLiquidPatch;
		if (obj == null)
		{
			Manipulator val = DrawNormalLiquidPatch;
			_003C_003EO._003C4_003E__DrawNormalLiquidPatch = val;
			obj = (object)val;
		}
		IL_LiquidRenderer.DrawNormalLiquids += (Manipulator)obj;
		object obj2 = _003C_003EO._003C5_003E__DrawPartialLiquidPatch;
		if (obj2 == null)
		{
			Manipulator val2 = DrawPartialLiquidPatch;
			_003C_003EO._003C5_003E__DrawPartialLiquidPatch = val2;
			obj2 = (object)val2;
		}
		IL_TileDrawing.DrawPartialLiquid += (Manipulator)obj2;
		object obj3 = _003C_003EO._003C6_003E__DrawOldWaterPatch;
		if (obj3 == null)
		{
			Manipulator val3 = DrawOldWaterPatch;
			_003C_003EO._003C6_003E__DrawOldWaterPatch = val3;
			obj3 = (object)val3;
		}
		IL_Main.oldDrawWater += (Manipulator)obj3;
		object obj4 = _003C_003EO._003C7_003E__DrawWaterfall;
		if (obj4 == null)
		{
			hook_DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects val4 = DrawWaterfall;
			_003C_003EO._003C7_003E__DrawWaterfall = val4;
			obj4 = (object)val4;
		}
		On_WaterfallManager.DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects += (hook_DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects)obj4;
		object obj5 = _003C_003EO._003C8_003E__WaterfallAddLight;
		if (obj5 == null)
		{
			hook_AddLight val5 = WaterfallAddLight;
			_003C_003EO._003C8_003E__WaterfallAddLight = val5;
			obj5 = (object)val5;
		}
		On_WaterfallManager.AddLight += (hook_AddLight)obj5;
		object obj6 = _003C_003EO._003C9_003E__WaterfallGlowmaskEditor;
		if (obj6 == null)
		{
			hook_StylizeColor val6 = WaterfallGlowmaskEditor;
			_003C_003EO._003C9_003E__WaterfallGlowmaskEditor = val6;
			obj6 = (object)val6;
		}
		On_WaterfallManager.StylizeColor += (hook_StylizeColor)obj6;
		object obj7 = _003C_003EO._003C10_003E__LavaBubbleReplacer;
		if (obj7 == null)
		{
			Manipulator val7 = LavaBubbleReplacer;
			_003C_003EO._003C10_003E__LavaBubbleReplacer = val7;
			obj7 = (object)val7;
		}
		IL_LiquidRenderer.InternalPrepareDraw += (Manipulator)obj7;
		object obj8 = _003C_003EO._003C11_003E__LavaDropletReplacer;
		if (obj8 == null)
		{
			Manipulator val8 = LavaDropletReplacer;
			_003C_003EO._003C11_003E__LavaDropletReplacer = val8;
			obj8 = (object)val8;
		}
		IL_TileDrawing.EmitLiquidDrops += (Manipulator)obj8;
		object obj9 = _003C_003EO._003C12_003E__SplashEntityLava;
		if (obj9 == null)
		{
			Manipulator val9 = SplashEntityLava;
			_003C_003EO._003C12_003E__SplashEntityLava = val9;
			obj9 = (object)val9;
		}
		IL_NPC.Collision_WaterCollision += (Manipulator)obj9;
		object obj10 = _003C_003EO._003C12_003E__SplashEntityLava;
		if (obj10 == null)
		{
			Manipulator val10 = SplashEntityLava;
			_003C_003EO._003C12_003E__SplashEntityLava = val10;
			obj10 = (object)val10;
		}
		IL_Projectile.Update += (Manipulator)obj10;
		object obj11 = _003C_003EO._003C12_003E__SplashEntityLava;
		if (obj11 == null)
		{
			Manipulator val11 = SplashEntityLava;
			_003C_003EO._003C12_003E__SplashEntityLava = val11;
			obj11 = (object)val11;
		}
		IL_Item.MoveInWorld += (Manipulator)obj11;
		ManipulatorBatch playerUpdate = ctx.PlayerUpdate;
		object obj12 = _003C_003EO._003C12_003E__SplashEntityLava;
		if (obj12 == null)
		{
			Manipulator val12 = SplashEntityLava;
			_003C_003EO._003C12_003E__SplashEntityLava = val12;
			obj12 = (object)val12;
		}
		playerUpdate.Add((Manipulator)obj12);
		ManipulatorBatch playerUpdate2 = ctx.PlayerUpdate;
		object obj13 = _003C_003EO._003C13_003E__PlayerDebuffEdit;
		if (obj13 == null)
		{
			Manipulator val13 = PlayerDebuffEdit;
			_003C_003EO._003C13_003E__PlayerDebuffEdit = val13;
			obj13 = (object)val13;
		}
		playerUpdate2.Add((Manipulator)obj13);
	}

	private static void DrawNormalLiquidPatch(ILContext il)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!PatchNextGetTexture2DLdLoc(cursor, delegate(int textureIdxLocIdx, FieldReference textureArrayField)
		{
			cursor.EmitLdloc(textureIdxLocIdx);
			cursor.EmitDelegate<Func<Texture2D, int, Texture2D>>((Func<Texture2D, int, Texture2D>)((Texture2D origTex, int textureIdx) => (Texture2D)((textureIdx != 1) ? ((object)origTex) : ((object)LavaRT))));
		}))
		{
			CalamityMod.Log.ILFailure("ModLavaStyle::DrawNormalLiquid", "Unable to Locate Asset<Texture2D>::Value call");
		}
	}

	private static void DrawPartialLiquidPatch(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ILCursor val = new ILCursor(il);
		int patchedCount = 0;
		foreach (var (c, textureArgIdx, textureArrayField) in FindAllGetTexture2DLdArg(val))
		{
			_ = ((MemberReference)textureArrayField).Name;
			if (((MemberReference)textureArrayField).Name == "Liquid")
			{
				c.EmitLdarg(textureArgIdx);
				c.EmitDelegate<Func<Texture2D, int, Texture2D>>((Func<Texture2D, int, Texture2D>)((Texture2D origTex, int textureIdx) => (Texture2D)((textureIdx != 1) ? ((object)origTex) : ((object)LavaBlockRT))));
				patchedCount++;
			}
			else if (((MemberReference)textureArrayField).Name == "LiquidSlope")
			{
				c.EmitLdarg(textureArgIdx);
				c.EmitDelegate<Func<Texture2D, int, Texture2D>>((Func<Texture2D, int, Texture2D>)((Texture2D origTex, int textureIdx) => (Texture2D)((textureIdx != 1) ? ((object)origTex) : ((object)LavaSlopeRT))));
				patchedCount++;
			}
			else
			{
				CalamityMod.Log.ILFailure("ModLavaStyle::DrawPartialLiquids", "Texture Array We referencing is [" + ((MemberReference)textureArrayField).Name + "] Which is not intended. Skipping");
			}
		}
		if (patchedCount <= 0)
		{
			CalamityMod.Log.ILFailure("ModLavaStyle::DrawPartialLiquids", "Unable to patch any of the texture reference");
		}
		else if (patchedCount != 5)
		{
			CalamityMod.Log.Warn((object)$"We did patched into {patchedCount} entry which is unmatching with desired count ({5}) when designed this iledit. Please check if anything is broken, If not update the count accordingly to suppress this message!");
			CalamityMod.Log.Warn((object)"Location: ModLavaStyleSystem_ILEdit.cs :: DrawPartialLiquidPatch");
		}
	}

	private static void DrawOldWaterPatch(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ILCursor val = new ILCursor(il);
		int patchedCount = 0;
		foreach (var (c, textureLocIdx, textureArrayField) in FindAllGetTexture2DLdLoc(val))
		{
			_ = ((MemberReference)textureArrayField).Name;
			if (((MemberReference)textureArrayField).Name == "Liquid")
			{
				c.EmitLdloc(textureLocIdx);
				c.EmitDelegate<Func<Texture2D, int, Texture2D>>((Func<Texture2D, int, Texture2D>)((Texture2D origTex, int textureIdx) => (Texture2D)((textureIdx != 1) ? ((object)origTex) : ((object)LavaBlockRT))));
				patchedCount++;
			}
			else if (((MemberReference)textureArrayField).Name == "LiquidSlope")
			{
				c.EmitLdloc(textureLocIdx);
				c.EmitDelegate<Func<Texture2D, int, Texture2D>>((Func<Texture2D, int, Texture2D>)((Texture2D origTex, int textureIdx) => (Texture2D)((textureIdx != 1) ? ((object)origTex) : ((object)LavaSlopeRT))));
				patchedCount++;
			}
			else
			{
				CalamityMod.Log.ILFailure("ModLavaStyle::DrawOldWater", "Texture Array We referencing is [" + ((MemberReference)textureArrayField).Name + "] Which is not intended. Skipping");
			}
		}
		if (patchedCount <= 0)
		{
			CalamityMod.Log.ILFailure("ModLavaStyle::DrawOldWater", "Unable to patch any of the texture reference");
		}
		else if (patchedCount != 10)
		{
			CalamityMod.Log.Warn((object)$"We did patched into {patchedCount} entry which is unmatching with desired count ({10}) when designed this iledit. Please check if anything is broken, If not update the count accordingly to suppress this message!");
			CalamityMod.Log.Warn((object)"Patch Name: ModLavaStyle::DrawOldWater");
		}
	}

	private static void DrawWaterfall(orig_DrawWaterfall_int_int_int_float_Vector2_Rectangle_Color_SpriteEffects orig, WaterfallManager self, int waterfallType, int x, int y, float opacity, Vector2 position, Rectangle sourceRect, Color color, SpriteEffects effects)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (waterfallType == 1 && CurrentLavaStyle != null)
		{
			Color lightColor = Lighting.GetColor(x, y);
			Main.spriteBatch.Draw((Texture2D)(object)LavaWaterfallRT, position, (Rectangle?)sourceRect, lightColor * opacity, 0f, Vector2.Zero, 1f, effects, 0f);
		}
		else
		{
			orig.Invoke(self, waterfallType, x, y, opacity, position, sourceRect, color, effects);
		}
	}

	private static void WaterfallAddLight(orig_AddLight orig, int waterfallType, int x, int y)
	{
		if (waterfallType == 1)
		{
			ModLavaStyle lavaStyle = CurrentLavaStyle;
			if (lavaStyle != null)
			{
				float r = 0.55f;
				float g = 0.33f;
				float b = 0.11f;
				lavaStyle.ModifyLight(x, y, ref r, ref g, ref b);
				Lighting.AddLight(x, y, r, g, b);
				return;
			}
		}
		orig.Invoke(waterfallType, x, y);
	}

	private static Color WaterfallGlowmaskEditor(orig_StylizeColor orig, float alpha, int maxSteps, int waterfallType, int y, int s, Tile tileCache, Color aColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (waterfallType == 1)
		{
			ModLavaStyle currentLavaStyle = CurrentLavaStyle;
			if (currentLavaStyle == null || !currentLavaStyle.LavafallGlowmask())
			{
				return aColor;
			}
		}
		return orig.Invoke(alpha, maxSteps, waterfallType, y, s, tileCache, aColor);
	}

	private static void LavaBubbleReplacer(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[3]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 16),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 16),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 35)
		}))
		{
			CalamityMod.Log.ILFailure("Ambient lava bubble replacer", "Could not locate the bubble newdust parameters");
			return;
		}
		cursor.EmitDelegate<Func<int, int>>((Func<int, int>)GetSplashDustID);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[3]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 16),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 8),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 35)
		}))
		{
			CalamityMod.Log.ILFailure("Ambient lava bubble replacer", "Could not locate the surface bubble newdust parameters");
		}
		else
		{
			cursor.EmitDelegate<Func<int, int>>((Func<int, int>)GetSplashDustID);
		}
	}

	private static void LavaDropletReplacer(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[4]
		{
			(Instruction i) =>
			{
				int num = default(int);
				return ILPatternMatchingExt.MatchLdarg(i, ref num);
			},
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 374),
			(Instruction i) =>
			{
				ILLabel val = default(ILLabel);
				return ILPatternMatchingExt.MatchBneUn(i, ref val);
			},
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 716)
		}))
		{
			CalamityMod.Log.ILFailure("Ambient lava droplet replacer", "Could not locate the lava droplet newgore parameters");
		}
		else
		{
			cursor.EmitDelegate<Func<int, int>>((Func<int, int>)GetDropletGoreID);
		}
	}

	private static void SplashEntityLava(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[5]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Entity>(i, "width"),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 12),
			(Instruction i) => ILPatternMatchingExt.MatchAdd(i),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 24),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 35)
		}))
		{
			CalamityMod.Log.ILFailure("Entity Lava Splashing (Item, Projectile, NPC, Player)", "Could not locate the first lava bubble splashing");
			return;
		}
		cursor.EmitDelegate<Func<int, int>>((Func<int, int>)GetSplashDustID);
		if (!cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[5]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdfld<Entity>(i, "width"),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 12),
			(Instruction i) => ILPatternMatchingExt.MatchAdd(i),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 24),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 35)
		}))
		{
			CalamityMod.Log.ILFailure("Entity Lava Splashing (Item, Projectile, NPC, Player)", "Could not locate the second lava bubble splashing");
		}
		else
		{
			cursor.EmitDelegate<Func<int, int>>((Func<int, int>)GetSplashDustID);
		}
	}

	private static void PlayerDebuffEdit(ILContext il)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		int onFireTimeLocalIdx = 0;
		if (!cursor.TryGotoNext((MoveType)0, new Func<Instruction, bool>[6]
		{
			(Instruction i) => ILPatternMatchingExt.MatchLdarg0(i),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 24),
			(Instruction i) => ILPatternMatchingExt.MatchLdloc(i, ref onFireTimeLocalIdx),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 1),
			(Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 0),
			(Instruction i) => ILPatternMatchingExt.MatchCall<Player>(i, "AddBuff")
		}))
		{
			CalamityMod.Log.ILFailure("Player Update Lava Debuff", "Could not locate the infliction of the On Fire! debuff inside the Player Update code");
			return;
		}
		cursor.EmitLdarg0();
		cursor.EmitLdloc(onFireTimeLocalIdx);
		cursor.EmitDelegate<Action<Player, int>>((Action<Player, int>)InflictDebuff);
	}

	private static IEnumerable<(ILCursor c, int textureArgIdx, FieldReference textureArrayField)> FindAllGetTexture2DLdArg(ILCursor cursor)
	{
		int textureArgIdx = 0;
		FieldReference textureArrayField = null;
		foreach (ILCursor c in FindAll(cursor, (MoveType)2, (Instruction x) => ILPatternMatchingExt.MatchLdsfld(x, ref textureArrayField) || ILPatternMatchingExt.MatchLdfld(x, ref textureArrayField), (Instruction x) => ILPatternMatchingExt.MatchLdarg(x, ref textureArgIdx), (Instruction x) => ILPatternMatchingExt.MatchLdelemRef(x), (Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt(x, (MethodBase)Tex2DAssetGetter)))
		{
			yield return (c: c, textureArgIdx: textureArgIdx, textureArrayField: textureArrayField);
		}
	}

	private static IEnumerable<(ILCursor c, int textureLocIdx, FieldReference textureArrayField)> FindAllGetTexture2DLdLoc(ILCursor cursor)
	{
		int textureLocIdx = 0;
		FieldReference textureArrayField = null;
		foreach (ILCursor c in FindAll(cursor, (MoveType)2, (Instruction x) => ILPatternMatchingExt.MatchLdsfld(x, ref textureArrayField) || ILPatternMatchingExt.MatchLdfld(x, ref textureArrayField), (Instruction x) => ILPatternMatchingExt.MatchLdloc(x, ref textureLocIdx), (Instruction x) => ILPatternMatchingExt.MatchLdelemRef(x), (Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt(x, (MethodBase)Tex2DAssetGetter)))
		{
			yield return (c: c, textureLocIdx: textureLocIdx, textureArrayField: textureArrayField);
		}
	}

	private static IEnumerable<ILCursor> FindAll(ILCursor cursor, MoveType moveType, params Func<Instruction, bool>[] predicates)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		ILCursor c = cursor.Clone();
		while (c.TryGotoNext(moveType, predicates))
		{
			yield return c.Clone();
		}
	}

	private static bool PatchNextGetTexture2DLdArg(ILCursor cursor, Action<int, FieldReference> patcher)
	{
		int textureArgIdx = 0;
		FieldReference textureArrayField = null;
		if (cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[4]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdsfld(x, ref textureArrayField) || ILPatternMatchingExt.MatchLdfld(x, ref textureArrayField),
			(Instruction x) => ILPatternMatchingExt.MatchLdarg(x, ref textureArgIdx),
			(Instruction x) => ILPatternMatchingExt.MatchLdelemRef(x),
			(Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt(x, (MethodBase)Tex2DAssetGetter)
		}))
		{
			patcher(textureArgIdx, textureArrayField);
			return true;
		}
		return false;
	}

	private static bool PatchNextGetTexture2DLdLoc(ILCursor cursor, Action<int, FieldReference> patcher)
	{
		int textureLocalIdx = 0;
		FieldReference textureArrayField = null;
		if (cursor.TryGotoNext((MoveType)2, new Func<Instruction, bool>[4]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdsfld(x, ref textureArrayField) || ILPatternMatchingExt.MatchLdfld(x, ref textureArrayField),
			(Instruction x) => ILPatternMatchingExt.MatchLdloc(x, ref textureLocalIdx),
			(Instruction x) => ILPatternMatchingExt.MatchLdelemRef(x),
			(Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt(x, (MethodBase)Tex2DAssetGetter)
		}))
		{
			patcher(textureLocalIdx, textureArrayField);
			return true;
		}
		return false;
	}

	private static void PrepareRT()
	{
		LavaRT = CreateRT(48, 1360);
		LavaBlockRT = CreateRT(16, 16);
		LavaSlopeRT = CreateRT(72, 16);
		LavaWaterfallRT = CreateRT(512, 40);
	}

	private static RenderTarget2D CreateRT(int width, int height)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		return new RenderTarget2D(((Game)Main.instance).GraphicsDevice, width, height, false, (SurfaceFormat)0, (DepthFormat)0, 0, (RenderTargetUsage)1);
	}

	private static void DisposeRT()
	{
		RenderTarget2D lavaRT = LavaRT;
		if (lavaRT != null)
		{
			((GraphicsResource)lavaRT).Dispose();
		}
		RenderTarget2D lavaBlockRT = LavaBlockRT;
		if (lavaBlockRT != null)
		{
			((GraphicsResource)lavaBlockRT).Dispose();
		}
		RenderTarget2D lavaSlopeRT = LavaSlopeRT;
		if (lavaSlopeRT != null)
		{
			((GraphicsResource)lavaSlopeRT).Dispose();
		}
		RenderTarget2D lavaWaterfallRT = LavaWaterfallRT;
		if (lavaWaterfallRT != null)
		{
			((GraphicsResource)lavaWaterfallRT).Dispose();
		}
		LavaRT = null;
		LavaBlockRT = null;
		LavaSlopeRT = null;
		LavaWaterfallRT = null;
	}

	private static void UpdateRT(GameTime time)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if (!Initialized || !TextureArrayReady || Main.gameMenu)
		{
			return;
		}
		using (LavaRT.Scope(preserveContents: true, Color.Transparent))
		{
			Begin();
			DrawTextures(Textures);
			End();
		}
		using (LavaBlockRT.Scope(preserveContents: true, Color.Transparent))
		{
			Begin();
			DrawTextures(BlockTextures);
			End();
		}
		using (LavaSlopeRT.Scope(preserveContents: true, Color.Transparent))
		{
			Begin();
			DrawTextures(SlopeTextures);
			End();
		}
		using (LavaWaterfallRT.Scope(preserveContents: true, Color.Transparent))
		{
			Begin();
			DrawTextures(WaterfallTextures);
			End();
		}
	}

	private static void Begin()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.Begin((SpriteSortMode)1, ActualAdditive, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Matrix.Identity);
	}

	private static void End()
	{
		Main.spriteBatch.End();
	}

	private static void DrawTextures(Asset<Texture2D>[] textures)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		int totalCount = ModLavaStyleLoader.TotalCount;
		for (int i = 0; i < totalCount; i++)
		{
			float alpha = LavaAlpha[i];
			if (!(alpha <= 0f))
			{
				Main.spriteBatch.Draw(textures[i].Value, Vector2.Zero, (Rectangle?)null, Color.White * alpha);
			}
		}
	}

	static ModLavaStyleSystem()
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		LavaStyles = new ModLavaStyle[ModLavaStyleLoader.VanillaCount];
		Textures = new Asset<Texture2D>[ModLavaStyleLoader.VanillaCount];
		BlockTextures = new Asset<Texture2D>[ModLavaStyleLoader.VanillaCount];
		SlopeTextures = new Asset<Texture2D>[ModLavaStyleLoader.VanillaCount];
		WaterfallTextures = new Asset<Texture2D>[ModLavaStyleLoader.VanillaCount];
		LavaAlpha = new float[ModLavaStyleLoader.VanillaCount];
		LavaStyle = 2;
		Initialized = false;
		TextureArrayReady = false;
		Tex2DAssetGetter = typeof(Asset<Texture2D>).GetProperty("Value").GetMethod;
		ActualAdditive = new BlendState
		{
			ColorSourceBlend = (Blend)0,
			AlphaSourceBlend = (Blend)0,
			ColorDestinationBlend = (Blend)0,
			AlphaDestinationBlend = (Blend)0
		};
	}
}
