using CalamityMod.ExtraJumps;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Statigel;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "StatigelHeadgear" })]
public class StatigelHeadRanged : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.1f;

	public static int RangedCritBoost = 7;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), RangedCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.defense = 7;
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
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PurifiedGel>(5).AddIngredient<BlightedGel>(5).AddTile(220)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<StatigelHeadMagic>())
			.Register();
	}
}
