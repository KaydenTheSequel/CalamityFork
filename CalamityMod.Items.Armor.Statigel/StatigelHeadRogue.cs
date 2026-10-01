using CalamityMod.CalPlayer;
using CalamityMod.ExtraJumps;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Statigel;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "StatigelMask" })]
public class StatigelHeadRogue : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.1f;

	public static int RogueCritBoost = 7;

	public static float MoveSpeedBoost = 0.05f;

	public static float SetBonusRogueStealth = 0.9f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.defense = 6;
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
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), StatigelArmor.SetBonusJumpSpeedBoost.ToJumpSpeedPercent());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.statigelSet = true;
		player.GetJumpState<StatigelJump>().Enable();
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		calamityPlayer.wearingRogueArmor = true;
		Player.jumpHeight += (int)(StatigelArmor.SetBonusJumpHeightPercentBoost * 15f);
		player.jumpSpeedBoost += StatigelArmor.SetBonusJumpSpeedBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PurifiedGel>(5).AddIngredient<BlightedGel>(5).AddTile(220)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<StatigelArmor>())
			.Register();
	}
}
