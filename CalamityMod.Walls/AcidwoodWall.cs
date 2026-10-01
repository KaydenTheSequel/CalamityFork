using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Walls;

public class AcidwoodWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Main.wallHouse[base.Type] = true;
		base.HitSound = SoundID.Dig;
		AddMapEntry(new Color(96, 69, 39));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 7, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}
}
