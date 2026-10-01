using CalamityMod.Tiles.FurnitureBotanic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureBotanic;

public class BotanicBed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureBotanic.BotanicBed>());
		base.Item.value = Item.sellPrice(0, 0, 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBrick>(15).AddIngredient(225, 5).AddTile(304)
			.Register();
	}
}
