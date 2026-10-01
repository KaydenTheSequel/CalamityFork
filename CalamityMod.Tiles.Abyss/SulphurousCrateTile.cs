using CalamityMod.Items.Fishing.SulphurCatches;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss;

[LegacyName(new string[] { "AbyssalCrateTile" })]
public class SulphurousCrateTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolidTop[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileTable[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(47, 79, 79), CalamityUtils.GetItemName<SulphurousCrate>());
		base.DustType = 33;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		if (Main.rand.NextBool())
		{
			type = 44;
		}
		else
		{
			type = 33;
		}
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
