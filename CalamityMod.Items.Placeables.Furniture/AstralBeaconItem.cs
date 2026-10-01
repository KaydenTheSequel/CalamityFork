using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Items.Potions;
using CalamityMod.Tiles.Astral;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class AstralBeaconItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<AstralBeacon>());
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 9;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AureusCell>(5).AddIngredient<StarblightSoot>(20).AddIngredient<global::CalamityMod.Items.Placeables.Astral.AstralStone>(30)
			.AddTile(283)
			.Register();
	}
}
