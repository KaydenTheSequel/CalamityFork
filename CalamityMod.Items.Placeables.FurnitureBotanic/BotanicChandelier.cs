using CalamityMod.Tiles.FurnitureBotanic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureBotanic;

public class BotanicChandelier : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureBotanic.BotanicChandelier>());
		base.Item.value = Item.sellPrice(0, 0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBrick>(4).AddIngredient(8, 4).AddIngredient(85)
			.AddTile(304)
			.Register();
	}
}
