using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Rarities;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

[LegacyName(new string[] { "KnowledgeExoMechs" })]
public class LoreExoMechs : LoreItem
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
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ArtemisTrophy>().AddTile(101).Register();
		CreateRecipe().AddIngredient<ApolloTrophy>().AddTile(101).Register();
		CreateRecipe().AddIngredient<ThanatosTrophy>().AddTile(101).Register();
		CreateRecipe().AddIngredient<AresTrophy>().AddTile(101).Register();
	}
}
