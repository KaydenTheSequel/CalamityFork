using CalamityMod.Dusts.WaterSplash;
using CalamityMod.Gores.WaterDroplet;
using CalamityMod.Systems.Graphic.LiquidSystem;
using CalamityMod.Tiles.Abyss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class BasaltGullyWater : ModWaterStyle, IWaterStyleModifyColor, IWaterStyleModifyLight
{
	public static int Type;

	private readonly Vector3 WaterGlowColor;

	public override void SetStaticDefaults()
	{
		Type = base.Slot;
	}

	public void ModifyLight(in Tile tile, int i, int j, ref float r, ref float g, ref float b)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Vector3 outputColor = default(Vector3);
		((Vector3)(ref outputColor))._002Ector(r, g, b);
		if (!(outputColor == Vector3.One) && !(outputColor == new Vector3(0.25f, 0.25f, 0.25f)) && !(outputColor == new Vector3(0.5f, 0.5f, 0.5f)))
		{
			if (tile.TileType != RustyChestTile.TileType)
			{
				WaterStyleCommon.ModifySunkenSeaWaterLight(i, j, WaterGlowColor, ref outputColor.X, ref outputColor.Y, ref outputColor.Z);
			}
			r = outputColor.X;
			g = outputColor.Y;
			b = outputColor.Z;
		}
	}

	public void ModifyColor(in Tile tile, int x, int y, ref VertexColors liquidColor, bool isSlope)
	{
		WaterStyleCommon.ModifyTransparentWaterColor(x, y, ref liquidColor, isSlope);
	}

	public override int ChooseWaterfallStyle()
	{
		return ModContent.Find<ModWaterfallStyle>("CalamityMod/BasaltGullyWaterflow").Slot;
	}

	public override int GetSplashDust()
	{
		return ModContent.DustType<BasaltGullySplash>();
	}

	public override int GetDropletGore()
	{
		return ModContent.GoreType<BasaltGullyWaterDroplet>();
	}

	public override Color BiomeHairColor()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.WhiteSmoke;
	}

	public BasaltGullyWater()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Color val = new Color(144, 174, 200);
		WaterGlowColor = ((Color)(ref val)).ToVector3();
		base._002Ector();
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
