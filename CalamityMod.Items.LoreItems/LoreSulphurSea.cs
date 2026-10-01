using CalamityMod.Items.Placeables.Furniture.Trophies;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

[LegacyName(new string[] { "KnowledgeSulphurSea" })]
public class LoreSulphurSea : LoreItem
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 5;
		base.Item.consumable = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AquaticScourgeTrophy>().AddTile(101).Register();
	}
}
