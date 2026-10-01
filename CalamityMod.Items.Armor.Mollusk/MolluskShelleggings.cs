using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Mollusk;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class MolluskShelleggings : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.06f;

	public static int CritBoost = 4;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 12;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.Calamity().molluskLegs = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MolluskHusk>(10).AddIngredient<SeaPrism>(20).AddTile(16)
			.Register();
	}
}
