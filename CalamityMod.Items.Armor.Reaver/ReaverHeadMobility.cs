using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Reaver;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "ReaverVisage" })]
public class ReaverHeadMobility : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.15f;

	public static float JumpSpeedBoost = 0.5f;

	public static float SetBonusFlightBoost = 0.1f;

	public static float SetBonusHookBoost = 0.5f;

	public static int SetBonusDashDelayReductionInterval = 3;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBoost.ToPercent(), JumpSpeedBoost.ToJumpSpeedPercent());

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 28;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 13;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<ReaverScaleMail>())
		{
			return legs.type == ModContent.ItemType<ReaverCuisses>();
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
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.reaverSpeed = true;
		calamityPlayer.wearingRogueArmor = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusFlightBoost.ToPercent(), SetBonusHookBoost.ToPercent(), (1f / (float)SetBonusDashDelayReductionInterval).ToPercent());
		player.noFallDmg = true;
		player.autoJump = true;
		if (player.miscCounter % SetBonusDashDelayReductionInterval == 1 && player.dashDelay > 0)
		{
			player.dashDelay--;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += MoveSpeedBoost;
		player.jumpSpeedBoost += JumpSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PerennialBar>(7).AddIngredient<LivingShard>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ReaverHeadExplore>())
			.Register();
	}
}
