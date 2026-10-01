using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

[LegacyName(new string[] { "SulphurousSandstoneWall" })]
public class UnsafeSulphurousSandstoneWall : ModWall
{
	public override string Texture => "CalamityMod/Walls/SulphurousSandstoneWall";

	public override void SetStaticDefaults()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 32;
		AddMapEntry(new Color(57, 45, 38));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
