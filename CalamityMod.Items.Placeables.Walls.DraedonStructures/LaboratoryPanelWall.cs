using CalamityMod.Items.Placeables.DraedonStructures;
using CalamityMod.Walls.DraedonStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls.DraedonStructures;

public class LaboratoryPanelWall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableWall(ModContent.WallType<global::CalamityMod.Walls.DraedonStructures.LaboratoryPanelWall>());
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<LaboratoryPanels>().AddTile(18).Register();
	}
}
