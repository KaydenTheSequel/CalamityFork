using CalamityMod.Walls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Tiles.SunkenSea;

public class UnsafeSeaPrismWall : MultiVariantModWall
{
	public override string Texture => "CalamityMod/Walls/SeaPrismWall";

	public override void SetStaticDefaults()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = false;
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

	public override bool Drop(int i, int j, ref int type)
	{
		return false;
	}
}
