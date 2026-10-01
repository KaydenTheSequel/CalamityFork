using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureSacrilegious;

public class OccultBrickTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		base.DustType = 8;
		AddMapEntry(new Color(63, 69, 71));
		base.HitSound = SoundID.Tink;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
