using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class BloodstoneCore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 113;
	}

	public override void SetDefaults()
	{
		base.Item.width = 15;
		base.Item.height = 12;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<Bloodstone>(5).AddIngredient<BloodOrb>().AddIngredient<Necroplasm>()
			.AddTile(134)
			.Register();
	}
}
