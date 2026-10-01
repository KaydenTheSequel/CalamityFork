using CalamityMod.Items.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss;

public class AbyssFossilTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(29, 37, 58), CalamityUtils.GetItemName<AbyssShellFossil>());
		base.DustType = 33;
		RegisterItemDrop(ModContent.ItemType<AbyssShellFossil>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<AbyssShellFossil>(), base.Type, default(int));
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
