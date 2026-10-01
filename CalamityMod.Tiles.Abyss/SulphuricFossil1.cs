using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss;

public class SulphuricFossil1 : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(113, 90, 71), CalamityUtils.GetText("Tiles.Fossil"));
		base.DustType = 75;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
