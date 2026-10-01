using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Rarities;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

[LegacyName(new string[] { "KnowledgeSentinels" })]
public class LoreCeaselessVoid : LoreItem
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
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CeaselessVoidTrophy>().AddTile(101).Register();
	}
}
