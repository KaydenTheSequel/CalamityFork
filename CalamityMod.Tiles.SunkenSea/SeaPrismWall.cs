using CalamityMod.Walls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Tiles.SunkenSea;

public class SeaPrismWall : MultiVariantModWall
{
	internal static FramedMaskTexture GlowMaskBlue;

	internal static FramedMaskTexture GlowMaskPurple;

	internal static FramedMaskTexture GlowMaskGreen;

	public override string Texture => "CalamityMod/Walls/SeaPrismWall";

	public override void SetStaticDefaults()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		GlowMaskBlue = new FramedMaskTexture("CalamityMod/Walls/SeaPrismWall_Blue", 36, 36);
		GlowMaskPurple = new FramedMaskTexture("CalamityMod/Walls/SeaPrismWall_Purple", 36, 36);
		GlowMaskGreen = new FramedMaskTexture("CalamityMod/Walls/SeaPrismWall_Green", 36, 36);
		Main.wallHouse[base.Type] = true;
		base.DustType = 108;
		AddMapEntry(new Color(27, 123, 131));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void PopulateWallVariant(int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = i % 8 * 468;
		frameYOffset = j % 8 * 180;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return false;
	}
}
