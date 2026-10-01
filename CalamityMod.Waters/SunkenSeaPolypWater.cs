using CalamityMod.Dusts.WaterSplash;
using CalamityMod.Gores.WaterDroplet;
using CalamityMod.Systems.Graphic.LiquidSystem;
using CalamityMod.Tiles.Abyss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class SunkenSeaPolypWater : ModWaterStyle, IWaterStyleModifyLight
{
	private readonly Vector3 WaterGlowColor;

	public static int Type { get; private set; }

	public static ModWaterStyle Instance { get; private set; }

	public override void SetStaticDefaults()
	{
		Type = base.Slot;
		Instance = this;
	}

	public override void Unload()
	{
		Type = -1;
		Instance = null;
	}

	public void ModifyLight(in Tile tile, int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Vector3 outputColor = default(Vector3);
		((Vector3)(ref outputColor))._002Ector(r, g, b);
		if (tile.TileType != RustyChestTile.TileType)
		{
			WaterStyleCommon.ModifySunkenSeaWaterLight(i, j, WaterGlowColor, ref outputColor.X, ref outputColor.Y, ref outputColor.Z);
		}
		r = outputColor.X;
		g = outputColor.Y;
		b = outputColor.Z;
	}

	public override int ChooseWaterfallStyle()
	{
		return ModContent.Find<ModWaterfallStyle>("CalamityMod/SunkenSeaPolypWaterflow").Slot;
	}

	public override int GetSplashDust()
	{
		return ModContent.DustType<SunkenSeaPolypSplash>();
	}

	public override int GetDropletGore()
	{
		return ModContent.GoreType<SunkenSeaPolypWaterDroplet>();
	}

	public override Color BiomeHairColor()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.Coral;
	}

	public SunkenSeaPolypWater()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Color val = new Color(213, 185, 178);
		WaterGlowColor = ((Color)(ref val)).ToVector3();
		base._002Ector();
	}

	void IWaterStyleModifyLight.ModifyLight(in Tile tile, int x, int y, ref float r, ref float g, ref float b)
	{
		ModifyLight(in tile, x, y, ref r, ref g, ref b);
	}
}
