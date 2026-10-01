using CalamityMod.Items.Materials;
using CalamityMod.Items.Potions.Alcohol;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class IVDripOnTheRocks : ModItem, ILocalizedModType, IModType
{
	public static readonly float DamageBoostMultiplier = 1.25f;

	public static readonly float DamageReductionMultiplier = 0.75f;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoostMultiplier.ToString("N2"), DamageReductionMultiplier.ToString("N2"));

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 60;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().ivDrip = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OldFashioned>().AddIngredient<LivingShard>(6).AddTile(134)
			.Register();
	}
}
