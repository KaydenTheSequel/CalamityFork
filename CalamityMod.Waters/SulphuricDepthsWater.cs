using CalamityMod.Dusts.WaterSplash;
using CalamityMod.Gores.WaterDroplet;
using CalamityMod.Systems.Graphic.LiquidSystem;
using CalamityMod.Tiles.Abyss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class SulphuricDepthsWater : ModWaterStyle, IWaterStyleModifyColor, IWaterStyleModifyLight
{
	public static int Type { get; private set; }

	public static ModWaterStyle Instance { get; private set; }

	public static ModWaterfallStyle WaterfallStyle { get; private set; }

	public static int SplashDust { get; private set; }

	public static int DropletGore { get; private set; }

	public override void SetStaticDefaults()
	{
		Type = base.Slot;
		Instance = this;
		WaterfallStyle = ModContent.Find<ModWaterfallStyle>("CalamityMod/SulphuricDepthsWaterflow");
		SplashDust = ModContent.DustType<SulphuricDepthsSplash>();
		DropletGore = ModContent.GoreType<SulphuricDepthsWaterDroplet>();
	}

	public override void Unload()
	{
		Type = -1;
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
		return new Color(35, 117, 89);
	}

	public void ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		WaterStyleCommon.ModifyTransparentWaterColor(x, y, ref liquidColor, isSlope);
	}

	public void ModifyLight(in Tile tile, int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Vector3 outputColor = default(Vector3);
		((Vector3)(ref outputColor))._002Ector(r, g, b);
		if (tile.TileType != RustyChestTile.TileType)
		{
			Vector3 val = outputColor;
			Color mediumSeaGreen = Color.MediumSeaGreen;
			outputColor = Vector3.Lerp(val, ((Color)(ref mediumSeaGreen)).ToVector3(), 0.18f);
		}
		r = outputColor.X;
		g = outputColor.Y;
		b = outputColor.Z;
	}

	void IWaterStyleModifyColor.ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		ModifyColor(in tile, x, y, ref liquidColor, isSlope);
	}

	void IWaterStyleModifyLight.ModifyLight(in Tile tile, int x, int y, ref float r, ref float g, ref float b)
	{
		ModifyLight(in tile, x, y, ref r, ref g, ref b);
	}
}
