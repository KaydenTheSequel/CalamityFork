using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Bloodflare;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "BloodflareMask" })]
public class BloodflareHeadMelee : ModItem, ILocalizedModType, IModType
{
	public static float MeleeDamageBoost = 0.1f;

	public static int MeleeCritBoost = 5;

	public static float MeleeSpeedBoost = 0.18f;

	public static int SetBonusAggroBoost = 900;

	public static int HitsToActivateFrenzy = 15;

	public static float FrenzyMeleeDamageBoost = 0.25f;

	public static int FrenzyMeleeCritBoost = 25;

	public static float FrenzyContactDamageReduction = 0.5f;

	public static int FrenzyDuration = CalamityUtils.SecondsToFrames(5);

	public static int FrenzyCooldown = CalamityUtils.SecondsToFrames(30);

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBoost.ToPercent(), MeleeCritBoost, MeleeSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 44;
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
		calamityPlayer.bloodflareMelee = true;
		player.aggro += SetBonusAggroBoost;
		player.setBonus = this.GetLocalization("SetBonus").Format(HitsToActivateFrenzy, FrenzyDuration.FramesToSeconds(), FrenzyMeleeDamageBoost.ToPercent(), FrenzyContactDamageReduction.ToPercent(), FrenzyCooldown.FramesToSeconds());
		player.crimsonRegen = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MeleeDamageClass>() += MeleeDamageBoost;
		player.GetCritChance<MeleeDamageClass>() += MeleeCritBoost;
		player.GetAttackSpeed<MeleeDamageClass>() += MeleeSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(11).AddIngredient<RuinousSoul>(2).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BloodflareHeadMagic>())
			.Register();
	}
}
