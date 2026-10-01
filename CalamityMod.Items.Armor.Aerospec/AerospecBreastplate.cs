using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Aerospec;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class AerospecBreastplate : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.03f;

	public static int CritBoost = 3;

	public static int SetBonusHurtDamageThreshold = 25;

	public static float SetBonusFallSpeed = 15f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	internal static string FeatherEntitySourceContext => "SetBonus_Calamity_Aerospec";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent());

	public static int SetBonusFeatherDamage => 15.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.defense = 7;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(11).AddIngredient(824, 8).AddIngredient(320, 2)
			.AddTile(16)
			.Register();
	}
}
