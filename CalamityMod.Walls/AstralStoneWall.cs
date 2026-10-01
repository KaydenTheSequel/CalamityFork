using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "AstralStoneWallSafe" })]
public class AstralStoneWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 27;
		Main.wallHouse[base.Type] = true;
		WallID.Sets.Conversion.Stone[base.Type] = true;
		AddMapEntry(new Color(15, 26, 31));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
