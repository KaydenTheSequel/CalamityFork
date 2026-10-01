using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "HardenedAstralSandWallSafe" })]
public class HardenedAstralSandWall : ModWall
{
	public override string Texture => "CalamityMod/Walls/HardenedAstralSandWall";

	public override void SetStaticDefaults()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 27;
		Main.wallHouse[base.Type] = true;
		WallID.Sets.Conversion.HardenedSand[base.Type] = true;
		AddMapEntry(new Color(10, 9, 21));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
