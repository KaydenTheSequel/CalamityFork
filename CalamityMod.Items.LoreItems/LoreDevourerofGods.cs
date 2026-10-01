using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Rarities;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

[LegacyName(new string[] { "KnowledgeDevourerofGods" })]
public class LoreDevourerofGods : LoreItem
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.consumable = false;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DevourerofGodsTrophy>().AddTile(101).Register();
	}
}
