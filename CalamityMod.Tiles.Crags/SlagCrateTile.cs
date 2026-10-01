using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Crags;

public class SlagCrateTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolidTop[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileTable[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(189, 152, 43), CalamityUtils.GetItemName<SlagCrate>());
		base.DustType = 60;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
