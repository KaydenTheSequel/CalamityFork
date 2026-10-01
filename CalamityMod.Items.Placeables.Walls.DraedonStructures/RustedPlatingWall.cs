using CalamityMod.Items.Placeables.DraedonStructures;
using CalamityMod.Walls.DraedonStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls.DraedonStructures;

public class RustedPlatingWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.DraedonStructures.RustedPlatingWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<RustedPlating>().AddTile(18).Register();
	}
}
