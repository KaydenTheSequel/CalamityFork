using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "AlchemicalFlask" })]
public class AlchemicalDecanter : ModItem, ILocalizedModType, IModType
{
	public static float PlagueReduction = 0.5f;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(PlagueReduction.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().alchFlask = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient(1132).AddIngredient(2431, 8)
			.AddIngredient<PlagueCellCanister>(15)
			.AddTile(134)
			.Register();
	}
}
