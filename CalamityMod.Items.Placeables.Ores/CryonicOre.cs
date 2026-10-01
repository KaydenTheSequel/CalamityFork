using CalamityMod.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Ores;

public class CryonicOre : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 90;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Ores.CryonicOre>());
		base.Item.value = Item.sellPrice(0, 0, 18);
		base.Item.rare = 5;
	}
}
