using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "AstralFossilWall" })]
public class CelestialRemainsWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = ModContent.DustType<AstralBasic>();
		Main.wallHouse[base.Type] = true;
		AddMapEntry(new Color(29, 38, 49));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
