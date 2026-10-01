using CalamityMod.Tiles.DraedonStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.DraedonStructures;

public class LaboratoryPipePlating : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.DraedonStructures.LaboratoryPipePlating>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<LaboratoryPlating>().AddIngredient<RustedPipes>().AddTile(16)
			.Register();
	}
}
