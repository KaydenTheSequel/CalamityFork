using CalamityMod.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Ores;

public class HallowedOre : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 89;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Ores.HallowedOre>());
		base.Item.value = Item.sellPrice(0, 0, 12);
		base.Item.rare = 5;
	}

	public override void AddRecipes()
	{
		Recipe.Create(1225).AddIngredient<HallowedOre>(4).AddTile(133)
			.Register();
	}
}
