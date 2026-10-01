using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Walls.UnsafeWalls;

public class UnsafeSulphurousSandWall : ModWall
{
	public override string Texture => "CalamityMod/Walls/SulphurousSandWall";

	public override void SetStaticDefaults()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 32;
		AddMapEntry(new Color(84, 71, 46));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
