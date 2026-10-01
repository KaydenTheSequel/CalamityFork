using CalamityMod.Dusts;
using CalamityMod.Items.Fishing.AstralCatches;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Astral;

public class AstralCrateTile : ModTile
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
		AddMapEntry(new Color(47, 66, 90), CalamityUtils.GetItemName<AstralCrate>());
		base.DustType = ModContent.DustType<AstralBlue>();
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		if (Main.rand.NextBool())
		{
			type = ModContent.DustType<AstralOrange>();
		}
		else
		{
			type = ModContent.DustType<AstralBlue>();
		}
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
