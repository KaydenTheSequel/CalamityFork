using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Bloodflare;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "BloodflareHelm" })]
public class BloodflareHeadRogue : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.15f;

	public static int RogueCritBoost = 10;

	public static float MoveSpeedBoost = 0.05f;

	public static float SetBonusRogueStealth = 1.2f;

	public static int DefenseBoostAboveHealthThreshold = 30;

	public static float DefenseBoostHealthThreshold = 0.8f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 26;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<BloodflareBodyArmor>())
		{
			return legs.type == ModContent.ItemType<BloodflareCuisses>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadowSubtle = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.bloodflareSet = true;
		calamityPlayer.bloodflareThrowing = true;
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		calamityPlayer.wearingRogueArmor = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), DefenseBoostAboveHealthThreshold, DefenseBoostHealthThreshold.ToPercent());
		player.crimsonRegen = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(11).AddIngredient<RuinousSoul>(2).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BloodflareBodyArmor>())
			.Register();
	}
}
