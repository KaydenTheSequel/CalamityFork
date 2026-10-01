using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "AstralSnowWallSafe" })]
public class AstralSnowWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = ModContent.DustType<AstralBasic>();
		Main.wallHouse[base.Type] = true;
		WallID.Sets.Conversion.Snow[base.Type] = true;
		AddMapEntry(new Color(135, 145, 149));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
