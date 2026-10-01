using CalamityMod.Items.Placeables.FurnitureMonolith;
using CalamityMod.Tiles.Furniture.Monoliths;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Monoliths;

public class AstralProjector : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AstralProjectorTile>());
		base.Item.value = Item.sellPrice(0, 0, 1);
		base.Item.rare = 4;
		base.Item.accessory = true;
		base.Item.vanity = true;
	}

	public override void UpdateEquip(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().monolithAstralShader = 30;
		}
	}

	public override void UpdateVanity(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().monolithAstralShader = 30;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralMonolith>(15).AddTile(16).Register();
	}
}
