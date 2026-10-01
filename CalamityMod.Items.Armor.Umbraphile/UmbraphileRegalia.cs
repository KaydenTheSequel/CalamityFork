using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Umbraphile;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class UmbraphileRegalia : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.1f;

	public static int RogueCritBoost = 10;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 24;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 18;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SolarVeil>(18).AddIngredient(1225, 15).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<UmbraphileBoots>())
			.Register();
	}
}
