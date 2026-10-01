using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "MeldiateBar" })]
public class MeldConstruct : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.width = 15;
		base.Item.height = 12;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 42);
		base.Item.rare = 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient<MeldBlob>(6).AddIngredient<StarblightSoot>(3).AddTile(412)
			.Register();
	}
}
