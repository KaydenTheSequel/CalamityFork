using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Umbraphile;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class UmbraphileBoots : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.09f;

	public static int RogueCritBoost = 6;

	public static float MoveSpeedBoost = 0.2f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 14;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SolarVeil>(14).AddIngredient(1225, 11).AddTile(134)
			.Register();
	}
}
