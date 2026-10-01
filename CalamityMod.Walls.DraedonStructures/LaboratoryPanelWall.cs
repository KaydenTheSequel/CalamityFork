using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Walls.DraedonStructures;

public class LaboratoryPanelWall : ModWall
{
	public override void SetStaticDefaults()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 1;
		Main.wallHouse[base.Type] = true;
		AddMapEntry(new Color(63, 57, 56));
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
