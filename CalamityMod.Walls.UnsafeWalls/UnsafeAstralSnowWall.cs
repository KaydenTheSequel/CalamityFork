using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

public class UnsafeAstralSnowWall : ModWall
{
	public override string Texture => "CalamityMod/Walls/AstralSnowWall";

	public override void SetStaticDefaults()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = ModContent.DustType<AstralBasic>();
		WallID.Sets.Conversion.Snow[base.Type] = true;
		AddMapEntry(new Color(135, 145, 149));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
