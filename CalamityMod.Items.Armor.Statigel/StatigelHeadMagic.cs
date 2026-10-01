using CalamityMod.ExtraJumps;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Statigel;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "StatigelCap" })]
public class StatigelHeadMagic : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 40;

	public static float ManaCostReduction = 0.1f;

	public static float MagicDamageBoost = 0.1f;

	public static int MagicCritBoost = 7;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), MagicDamageBoost.ToPercent(), MagicCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.defense = 5;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<StatigelArmor>())
		{
			return legs.type == ModContent.ItemType<StatigelGreaves>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = CalamityUtils.GetTextFromModItem<StatigelArmor>("CommonSetBonus").Format(StatigelArmor.SetBonusJumpSpeedBoost.ToJumpSpeedPercent());
		player.Calamity().statigelSet = true;
		player.GetJumpState<StatigelJump>().Enable();
		Player.jumpHeight += (int)(StatigelArmor.SetBonusJumpHeightPercentBoost * 15f);
		player.jumpSpeedBoost += StatigelArmor.SetBonusJumpSpeedBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += MagicCritBoost;
		player.manaCost -= ManaCostReduction;
		player.statManaMax2 += MaxManaBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PurifiedGel>(5).AddIngredient<BlightedGel>(5).AddTile(220)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<StatigelArmor>())
			.Register();
	}
}
