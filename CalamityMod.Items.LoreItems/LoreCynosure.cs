using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems;

public class LoreCynosure : LoreItem
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
		base.Item.rare = ModContent.RarityType<CalamityRed>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ShadowspecBar>().AddTile(101).Register();
	}
}
