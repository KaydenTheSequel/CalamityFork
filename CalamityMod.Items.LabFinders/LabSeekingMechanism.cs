using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.LabFinders;

[LegacyName(new string[] { "MysteriousMechanism" })]
public class LabSeekingMechanism : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.DraedonItems";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 26;
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = ModContent.RarityType<DarkOrange>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(4).AddIngredient<DubiousPlating>(4).AddRecipeGroup("IronBar", 10)
			.AddTile(16)
			.Register();
	}
}
