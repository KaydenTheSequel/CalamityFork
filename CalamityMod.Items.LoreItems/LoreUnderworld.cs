using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

[LegacyName(new string[] { "KnowledgeUnderworld" })]
public class LoreUnderworld : LoreItem
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 4;
		base.Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1365).AddTile(101).Register();
	}
}
