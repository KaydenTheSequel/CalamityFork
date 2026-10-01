using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.AstralSnow;

public class AstralIceLargePiles : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoFail[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.addTile(base.Type);
		base.DustType = ModContent.DustType<AstralBasic>();
		AddMapEntry(new Color(79, 61, 97));
		base.SetStaticDefaults();
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
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
