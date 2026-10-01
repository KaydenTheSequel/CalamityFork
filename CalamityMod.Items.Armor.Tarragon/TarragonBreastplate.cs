using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Tarragon;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class TarragonBreastplate : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.12f;

	public static int CritBoost = 8;

	public static int RegenBoost = 4;

	public static float DamageReductionBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost, RegenBoost.ToRegenPerSecond(), DamageReductionBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.defense = 36;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.lifeRegen += RegenBoost;
		player.endurance += DamageReductionBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(15).AddIngredient<DivineGeode>(18).AddTile(134)
			.Register();
	}
}
