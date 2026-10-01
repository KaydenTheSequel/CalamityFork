using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureAshen;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureAshen;

public class AshenAccentSlab : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override string Texture => "CalamityMod/Items/Placeables/FurnitureAshen/AshenSlab";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureAshen.AshenAccentSlab>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(50).AddIngredient<SmoothBrimstoneSlag>(50).AddTile<AshenAltar>().AddCondition(Condition.InGraveyard)
			.Register();
	}
}
