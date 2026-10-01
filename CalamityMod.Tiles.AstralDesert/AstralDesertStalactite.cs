using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.AstralDesert;

public class AstralDesertStalactite : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		base.DustType = ModContent.DustType<AstralBasic>();
		AddMapEntry(new Color(79, 61, 97));
		base.SetStaticDefaults();
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		WorldGen.CheckTight(i, j);
		return false;
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameY <= 18 || tile.TileFrameY == 72)
		{
			offsetY = -2;
		}
		else if ((tile.TileFrameY >= 36 && tile.TileFrameY <= 54) || tile.TileFrameY == 90)
		{
			offsetY = 2;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 4);
	}

	public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		sightColor = Color.Cyan;
		return true;
	}
}
