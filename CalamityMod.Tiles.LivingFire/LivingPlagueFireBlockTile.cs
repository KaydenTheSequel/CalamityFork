using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.LivingFire;

public class LivingPlagueFireBlockTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		TileID.Sets.CanPlaceNextToNonSolidTile[base.Type] = true;
		base.DustType = 46;
		AddMapEntry(new Color(0, 171, 31));
		base.AnimationFrameHeight = 90;
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frame = Main.tileFrame[336];
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0f;
		g = 1f;
		b = 0.2f;
	}
}
