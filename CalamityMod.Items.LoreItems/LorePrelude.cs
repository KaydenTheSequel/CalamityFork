using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

[LegacyName(new string[] { "KnowledgeLunaticCultist" })]
public class LorePrelude : LoreItem
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 9;
		base.Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3357).AddTile(101).Register();
	}
}
