using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Bloodflare;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class BloodflareBodyArmor : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.12f;

	public static int CritBoost = 8;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 36;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(16).AddIngredient<RuinousSoul>(4).AddTile(134)
			.Register();
	}
}
