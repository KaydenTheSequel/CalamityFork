using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.FurnitureWulfrum;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum;

public class ChargedWulfrumEnergyBarrier : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.ChargedWulfrumEnergyBarrier>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(25).AddIngredient(170, 25).AddIngredient<EnergyCore>().AddTile(283)
			.Register();
		CreateRecipe().AddIngredient<ChargedWulfrumEnergyBarrierWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
