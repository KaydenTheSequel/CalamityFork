using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Aerospec;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AerospecHat" })]
public class AerospecHeadMagic : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 30;

	public static float MagicDamageBoost = 0.1f;

	public static float SetBonusManaCostReduction = 0.08f;

	public static float SetBonusMoveSpeedBoost = 0.05f;

	public static int SetBonusMagicCritBoost = 5;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, MagicDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.defense = 3;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<AerospecBreastplate>())
		{
			return legs.type == ModContent.ItemType<AerospecLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusManaCostReduction.ToPercent(), SetBonusMoveSpeedBoost.ToPercent(), AerospecBreastplate.SetBonusHurtDamageThreshold);
		player.Calamity().aeroSet = true;
		player.noFallDmg = true;
		player.moveSpeed += SetBonusMoveSpeedBoost;
		player.manaCost -= SetBonusManaCostReduction;
		player.GetCritChance<MagicDamageClass>() += SetBonusMagicCritBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.statManaMax2 += MaxManaBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(5).AddIngredient(824, 3).AddIngredient(320)
			.AddTile(16)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<AerospecBreastplate>())
			.Register();
	}
}
