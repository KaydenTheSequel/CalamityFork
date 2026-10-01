using CalamityMod.Items.Placeables.Furniture.Trophies;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

[LegacyName(new string[] { "KnowledgeOcean" })]
public class LoreAbyss : LoreItem
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 7;
		base.Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LeviathanTrophy>().AddTile(101).Register();
		CreateRecipe().AddIngredient<AnahitaTrophy>().AddTile(101).Register();
	}
}
