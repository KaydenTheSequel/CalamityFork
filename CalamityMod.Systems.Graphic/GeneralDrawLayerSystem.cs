using System;
using System.Runtime.CompilerServices;
using CalamityMod.Enums;
using CalamityMod.Utilities.Daybreak;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Graphic;

internal sealed class GeneralDrawLayerSystem : ModSystem
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static hook_CheckMonoliths _003C0_003E__CheckMonoliths;

		public static hook_DrawBackgroundBlackFill _003C1_003E__GeneralDrawLayer_DrawToLayer_BeforeAllTiles;

		public static hook_DoDraw_Tiles_Solid _003C2_003E__GeneralDrawLayer_DrawToLayer_BeforeSolidTiles;

		public static hook_DoDraw_DrawNPCsOverTiles _003C3_003E__GeneralDrawLayer_DrawToLayer_NPCs;

		public static hook_DrawProjectiles _003C4_003E__GeneralDrawLayer_DrawToLayer_Projectiles;

		public static hook_DrawPlayers_AfterProjectiles _003C5_003E__GeneralDrawLayer_DrawToLayer_AfterPlayers;

		public static hook_DrawDust _003C6_003E__GeneralDrawLayer_DrawToLayer_AfterDusts;

		public static hook_DrawInfernoRings _003C7_003E__GeneralDrawLayer_DrawToLayer_AfterEverything;
	}

	public static event Action OnPrepareDraw;

	public static event Action<GeneralDrawLayer> OnDrawLayer;

	public static event Action<GeneralDrawLayer> OnDrawLayerLate;

	public static event Action OnBeforeAllTiles;

	public static event Action OnBeforeSolidTiles;

	public static event Action OnBeforeNPCs;

	public static event Action OnAfterNPCs;

	public static event Action OnBeforeProjectiles;

	public static event Action OnAfterProjectiles;

	public static event Action OnAfterPlayers;

	public static event Action OnAfterDusts;

	public static event Action OnAfterEverything;

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
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		object obj = _003C_003EO._003C0_003E__CheckMonoliths;
		if (obj == null)
		{
			hook_CheckMonoliths val = CheckMonoliths;
			_003C_003EO._003C0_003E__CheckMonoliths = val;
			obj = (object)val;
		}
		On_Main.CheckMonoliths += (hook_CheckMonoliths)obj;
		object obj2 = _003C_003EO._003C1_003E__GeneralDrawLayer_DrawToLayer_BeforeAllTiles;
		if (obj2 == null)
		{
			hook_DrawBackgroundBlackFill val2 = GeneralDrawLayer_DrawToLayer_BeforeAllTiles;
			_003C_003EO._003C1_003E__GeneralDrawLayer_DrawToLayer_BeforeAllTiles = val2;
			obj2 = (object)val2;
		}
		On_Main.DrawBackgroundBlackFill += (hook_DrawBackgroundBlackFill)obj2;
		object obj3 = _003C_003EO._003C2_003E__GeneralDrawLayer_DrawToLayer_BeforeSolidTiles;
		if (obj3 == null)
		{
			hook_DoDraw_Tiles_Solid val3 = GeneralDrawLayer_DrawToLayer_BeforeSolidTiles;
			_003C_003EO._003C2_003E__GeneralDrawLayer_DrawToLayer_BeforeSolidTiles = val3;
			obj3 = (object)val3;
		}
		On_Main.DoDraw_Tiles_Solid += (hook_DoDraw_Tiles_Solid)obj3;
		object obj4 = _003C_003EO._003C3_003E__GeneralDrawLayer_DrawToLayer_NPCs;
		if (obj4 == null)
		{
			hook_DoDraw_DrawNPCsOverTiles val4 = GeneralDrawLayer_DrawToLayer_NPCs;
			_003C_003EO._003C3_003E__GeneralDrawLayer_DrawToLayer_NPCs = val4;
			obj4 = (object)val4;
		}
		On_Main.DoDraw_DrawNPCsOverTiles += (hook_DoDraw_DrawNPCsOverTiles)obj4;
		object obj5 = _003C_003EO._003C4_003E__GeneralDrawLayer_DrawToLayer_Projectiles;
		if (obj5 == null)
		{
			hook_DrawProjectiles val5 = GeneralDrawLayer_DrawToLayer_Projectiles;
			_003C_003EO._003C4_003E__GeneralDrawLayer_DrawToLayer_Projectiles = val5;
			obj5 = (object)val5;
		}
		On_Main.DrawProjectiles += (hook_DrawProjectiles)obj5;
		object obj6 = _003C_003EO._003C5_003E__GeneralDrawLayer_DrawToLayer_AfterPlayers;
		if (obj6 == null)
		{
			hook_DrawPlayers_AfterProjectiles val6 = GeneralDrawLayer_DrawToLayer_AfterPlayers;
			_003C_003EO._003C5_003E__GeneralDrawLayer_DrawToLayer_AfterPlayers = val6;
			obj6 = (object)val6;
		}
		On_Main.DrawPlayers_AfterProjectiles += (hook_DrawPlayers_AfterProjectiles)obj6;
		object obj7 = _003C_003EO._003C6_003E__GeneralDrawLayer_DrawToLayer_AfterDusts;
		if (obj7 == null)
		{
			hook_DrawDust val7 = GeneralDrawLayer_DrawToLayer_AfterDusts;
			_003C_003EO._003C6_003E__GeneralDrawLayer_DrawToLayer_AfterDusts = val7;
			obj7 = (object)val7;
		}
		On_Main.DrawDust += (hook_DrawDust)obj7;
		object obj8 = _003C_003EO._003C7_003E__GeneralDrawLayer_DrawToLayer_AfterEverything;
		if (obj8 == null)
		{
			hook_DrawInfernoRings val8 = GeneralDrawLayer_DrawToLayer_AfterEverything;
			_003C_003EO._003C7_003E__GeneralDrawLayer_DrawToLayer_AfterEverything = val8;
			obj8 = (object)val8;
		}
		On_Main.DrawInfernoRings += (hook_DrawInfernoRings)obj8;
	}

	public override void Unload()
	{
		OnPrepareDraw = null;
		OnDrawLayer = null;
		OnDrawLayerLate = null;
		OnBeforeAllTiles = null;
		OnBeforeSolidTiles = null;
		OnBeforeNPCs = null;
		OnAfterNPCs = null;
		OnBeforeProjectiles = null;
		OnAfterProjectiles = null;
		OnAfterPlayers = null;
		OnAfterDusts = null;
		OnAfterEverything = null;
	}

	private static void CheckMonoliths(orig_CheckMonoliths orig)
	{
		orig.Invoke();
		OnPrepareDraw?.Invoke();
	}

	private static void GeneralDrawLayer_DrawForLayer(GeneralDrawLayer drawLayer)
	{
		OnDrawLayer?.Invoke(drawLayer);
		OnDrawLayerLate?.Invoke(drawLayer);
		((Action)(drawLayer switch
		{
			GeneralDrawLayer.BeforeAllTiles => OnBeforeAllTiles, 
			GeneralDrawLayer.BeforeSolidTiles => OnBeforeSolidTiles, 
			GeneralDrawLayer.BeforeNPCs => OnBeforeNPCs, 
			GeneralDrawLayer.AfterNPCs => OnAfterNPCs, 
			GeneralDrawLayer.BeforeProjectiles => OnBeforeProjectiles, 
			GeneralDrawLayer.AfterProjectiles => OnAfterProjectiles, 
			GeneralDrawLayer.AfterPlayers => OnAfterPlayers, 
			GeneralDrawLayer.AfterDusts => OnAfterDusts, 
			GeneralDrawLayer.AfterEverything => OnAfterEverything, 
			_ => null, 
		}))?.Invoke();
	}

	private static void GeneralDrawLayer_DrawToLayer_BeforeAllTiles(orig_DrawBackgroundBlackFill orig, Main self)
	{
		Main.spriteBatch.End(out var ss);
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.BeforeAllTiles);
		Main.spriteBatch.Begin(in ss);
		orig.Invoke(self);
	}

	private static void GeneralDrawLayer_DrawToLayer_BeforeSolidTiles(orig_DoDraw_Tiles_Solid orig, Main self)
	{
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.BeforeSolidTiles);
		orig.Invoke(self);
	}

	private static void GeneralDrawLayer_DrawToLayer_NPCs(orig_DoDraw_DrawNPCsOverTiles orig, Main self)
	{
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.BeforeNPCs);
		orig.Invoke(self);
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.AfterNPCs);
	}

	private static void GeneralDrawLayer_DrawToLayer_Projectiles(orig_DrawProjectiles orig, Main self)
	{
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.BeforeProjectiles);
		orig.Invoke(self);
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.AfterProjectiles);
	}

	private static void GeneralDrawLayer_DrawToLayer_AfterPlayers(orig_DrawPlayers_AfterProjectiles orig, Main self)
	{
		orig.Invoke(self);
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.AfterPlayers);
	}

	private static void GeneralDrawLayer_DrawToLayer_AfterDusts(orig_DrawDust orig, Main self)
	{
		orig.Invoke(self);
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.AfterDusts);
	}

	private static void GeneralDrawLayer_DrawToLayer_AfterEverything(orig_DrawInfernoRings orig, Main self)
	{
		orig.Invoke(self);
		Main.spriteBatch.End(out var ss);
		GeneralDrawLayer_DrawForLayer(GeneralDrawLayer.AfterEverything);
		Main.spriteBatch.Begin(in ss);
	}
}
