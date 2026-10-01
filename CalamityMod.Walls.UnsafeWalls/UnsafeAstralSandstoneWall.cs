using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

public class UnsafeAstralSandstoneWall : ModWall
{
	public override string Texture => "CalamityMod/Walls/AstralSandstoneWall";

	public override void SetStaticDefaults()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 27;
		WallID.Sets.Conversion.Sandstone[base.Type] = true;
		AddMapEntry(new Color(29, 38, 49));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
