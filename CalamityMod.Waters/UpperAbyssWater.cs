using CalamityMod.Dusts.WaterSplash;
using CalamityMod.Gores.WaterDroplet;
using CalamityMod.Systems.Graphic.LiquidSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class UpperAbyssWater : ModWaterStyle, IWaterStyleModifyColor
{
	public static ModWaterStyle Instance { get; private set; }

	public static ModWaterfallStyle WaterfallStyle { get; private set; }

	public static int SplashDust { get; private set; }

	public static int DropletGore { get; private set; }

	public override void SetStaticDefaults()
	{
		Instance = this;
		WaterfallStyle = ModContent.Find<ModWaterfallStyle>("CalamityMod/UpperAbyssWaterflow");
		SplashDust = ModContent.DustType<SunkenSeaBurrowsSplash>();
		DropletGore = ModContent.GoreType<SunkenSeaBurrowsWaterDroplet>();
	}

	public override void Unload()
	{
		Instance = null;
		WaterfallStyle = null;
		SplashDust = 0;
		DropletGore = 0;
	}

	public override int ChooseWaterfallStyle()
	{
		return WaterfallStyle.Slot;
	}

	public override int GetSplashDust()
	{
		return SplashDust;
	}

	public override int GetDropletGore()
	{
		return DropletGore;
	}

	public override Color BiomeHairColor()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return new Color(9, 69, 82);
	}

	public void ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		WaterStyleCommon.ModifyTransparentWaterColor(x, y, ref liquidColor, isSlope);
	}

	void IWaterStyleModifyColor.ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		ModifyColor(in tile, x, y, ref liquidColor, isSlope);
	}
}
