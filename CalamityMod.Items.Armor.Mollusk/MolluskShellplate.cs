using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Mollusk;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class MolluskShellplate : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.06f;

	public static int CritBoost = 5;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 18;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.Calamity().molluskChest = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MolluskHusk>(15).AddIngredient<SeaPrism>(25).AddTile(16)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<MolluskShelleggings>())
			.Register();
	}
}
