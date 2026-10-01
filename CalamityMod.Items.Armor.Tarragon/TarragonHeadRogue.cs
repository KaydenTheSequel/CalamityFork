using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Tarragon;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "TarragonHelmet" })]
public class TarragonHeadRogue : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.1f;

	public static int RogueCritBoost = 7;

	public static float MoveSpeedBoost = 0.05f;

	public static float SetBonusRogueStealth = 1.15f;

	public static int CritsToActivateImmunity = 50;

	public static int ImmunityDuration = CalamityUtils.SecondsToFrames(2.5f);

	public static int ImmunityCooldown = CalamityUtils.SecondsToFrames(25);

	public static float RogueDamageBoostWhileDebuffed = 0.1f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.defense = 24;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<TarragonBreastplate>())
		{
			return legs.type == ModContent.ItemType<TarragonLeggings>();
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
		calamityPlayer.tarraSet = true;
		calamityPlayer.tarraThrowing = true;
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		calamityPlayer.wearingRogueArmor = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), CritsToActivateImmunity, ImmunityDuration.FramesToSeconds(), ImmunityCooldown.FramesToSeconds(), RogueDamageBoostWhileDebuffed.ToPercent());
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(7).AddIngredient<DivineGeode>(6).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<TarragonBreastplate>())
			.Register();
	}
}
