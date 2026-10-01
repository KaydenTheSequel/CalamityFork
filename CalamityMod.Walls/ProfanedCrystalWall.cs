using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

public class ProfanedCrystalWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		Main.wallLargeFrames[base.Type] = 2;
		base.HitSound = SoundID.Shatter;
		AddMapEntry(new Color(125, 97, 123));
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 205, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
