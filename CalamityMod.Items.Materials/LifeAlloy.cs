using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "BarofLife" })]
public class LifeAlloy : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 95;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 24;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 3);
		base.Item.rare = 8;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>().AddIngredient<PerennialBar>().AddIngredient<ScoriaBar>()
			.AddTile(134)
			.Register();
	}
}
