using CalamityMod.Items.Placeables.FurnitureWulfrum;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class AnodizedWulfrumMetal : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 0, 10);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StormlionMandible>().AddIngredient<WulfrumMetalScrap>().AddTile(18)
			.Register();
		CreateRecipe().AddIngredient<AnodizedWulfrumPlatform>(2).DisableDecraft().Register();
	}
}
