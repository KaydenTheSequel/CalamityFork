using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "ChaoticBrickWall" })]
public class ScoriaBrickWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		AddMapEntry(new Color(255, 0, 0));
		base.DustType = 105;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
