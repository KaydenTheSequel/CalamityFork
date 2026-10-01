using CalamityMod.ExtraJumps;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Statigel;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "StatigelHelm" })]
public class StatigelHeadMelee : ModItem, ILocalizedModType, IModType
{
	public static float MeleeDamageBoost = 0.1f;

	public static float MeleeSpeedBoost = 0.1f;

	public static int MeleeCritBoost = 7;

	public static int SetBonusAggroBoost = 400;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBoost.ToPercent(), MeleeCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.defense = 9;
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
		player.setBonus = this.GetLocalization("SetBonus").Format(StatigelArmor.SetBonusJumpSpeedBoost.ToJumpSpeedPercent());
		player.Calamity().statigelSet = true;
		player.GetJumpState<StatigelJump>().Enable();
		Player.jumpHeight += (int)(StatigelArmor.SetBonusJumpHeightPercentBoost * 15f);
		player.jumpSpeedBoost += StatigelArmor.SetBonusJumpSpeedBoost;
		player.aggro += SetBonusAggroBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MeleeDamageClass>() += MeleeDamageBoost;
		player.GetCritChance<MeleeDamageClass>() += MeleeCritBoost;
		player.GetAttackSpeed<MeleeDamageClass>() += MeleeSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PurifiedGel>(5).AddIngredient<BlightedGel>(5).AddTile(220)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<StatigelHeadMagic>())
			.Register();
	}
}
