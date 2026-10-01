using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Hydrothermic;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
[LegacyName(new string[] { "AtaxiaArmor" })]
public class HydrothermicArmor : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.13f;

	public static float InfernoHealthThreshold = 0.5f;

	public static int InfernoHitRate = 30;

	public static int InfernoDamage = 50;

	public static float InfernoRange = 300f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	internal static string VanitySmokeEntitySourceContext => "SetBonus_Calamity_Hydrothermic_Vanity";

	internal static string InfernoPotionEntitySourceContext => "SetBonus_Calamity_Hydrothermic_InfernoPotionBoost";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent());

	public static int BlazeDamage => 115.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 20;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.lavaImmune = true;
		player.buffImmune[24] = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(15).AddIngredient<EssenceofHavoc>(3).AddTile(134)
			.Register();
	}
}
