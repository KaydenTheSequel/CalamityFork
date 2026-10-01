using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "AstralIceWallSafe" })]
public class AstralIceWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		base.DustType = 27;
		WallID.Sets.Conversion.Ice[base.Type] = true;
		AddMapEntry(new Color(83, 76, 92));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
