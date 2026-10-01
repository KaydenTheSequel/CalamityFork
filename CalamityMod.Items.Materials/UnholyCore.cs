using CalamityMod.Items.Placeables.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class UnholyCore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 90;
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 46;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 80);
		base.Item.rare = 5;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<InfernalSuevite>(4).AddIngredient(174, 4).AddTile(77)
			.Register();
	}
}
