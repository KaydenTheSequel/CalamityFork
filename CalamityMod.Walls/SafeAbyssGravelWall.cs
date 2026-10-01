using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "AbyssGravelWallSafe" })]
public class SafeAbyssGravelWall : ModWall
{
	public override string Texture => "CalamityMod/Walls/AbyssGravelWall";

	public override void SetStaticDefaults()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		AddMapEntry(new Color(6, 10, 54));
		base.DustType = 33;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
