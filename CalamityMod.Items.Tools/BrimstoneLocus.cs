using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class BrimstoneLocus : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = ModContent.RarityType<CalamityRed>();
		base.Item.value = 0;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AshesofAnnihilation>().AddIngredient<BloodstoneCore>(3).Register();
	}
}
