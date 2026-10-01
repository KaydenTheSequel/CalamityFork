using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "VictideBar" })]
public class SeaRemains : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 60;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 24;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 36);
		base.Item.rare = 2;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PearlShard>(2).AddIngredient(275, 2).AddIngredient(2626, 2)
			.AddIngredient(2625, 2)
			.AddTile(17)
			.Register();
	}
}
