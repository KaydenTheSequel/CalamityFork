using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Daedalus;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "DaedalusHat" })]
public class DaedalusHeadMagic : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 80;

	public static float ManaCostReduction = 0.1f;

	public static float MagicDamageBoost = 0.13f;

	public static int MagicCritBoost = 7;

	public static int AbsorptionChanceDenominator = 10;

	public static float DamageAbsorptionPercent = 0.5f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), MagicDamageBoost.ToPercent(), MagicCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 5;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<DaedalusBreastplate>())
		{
			return legs.type == ModContent.ItemType<DaedalusLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadowSubtle = true;
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalization("SetBonus").Format(AbsorptionChanceDenominator.GetChanceFromDenominator(), DamageAbsorptionPercent.ToPercent());
		player.Calamity().daedalusAbsorb = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += MaxManaBoost;
		player.manaCost -= ManaCostReduction;
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += MagicCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(7).AddIngredient<EssenceofEleum>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<DaedalusBreastplate>())
			.Register();
	}
}
