using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "SulphurousSandWallSafe" })]
public class SulphurousSandWall : ModWall
{
	public override string Texture => "CalamityMod/Walls/SulphurousSandWall";

	public override void SetStaticDefaults()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		base.DustType = 32;
		AddMapEntry(new Color(84, 71, 46));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
