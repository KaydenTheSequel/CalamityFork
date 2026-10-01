using CalamityMod.Tiles.FurnitureBotanic;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureBotanic;

public class BotanicPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureBotanic.BotanicPlatform>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<UelibloomBrick>().Register();
	}
}
