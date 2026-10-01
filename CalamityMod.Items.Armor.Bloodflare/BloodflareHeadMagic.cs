using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Bloodflare;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "BloodflareHornedMask" })]
public class BloodflareHeadMagic : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 100;

	public static float ManaCostReduction = 0.17f;

	public static float MagicDamageBoost = 0.2f;

	public static int MagicCritBoost = 10;

	public static int GhostBoltCooldown = CalamityUtils.SecondsToFrames(1.67f);

	public static double GhostBoltDamageRatio = 1.3;

	public static int GhostBoltonDamageSoftcap = 2600;

	public static int BloodsplosionCooldown = CalamityUtils.SecondsToFrames(2);

	public static double BloodsplosionDamageRatio = 0.5;

	public static int BloodsplosionDamageSoftcap = 250;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), MagicDamageBoost.ToPercent(), MagicCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 18;
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
		calamityPlayer.bloodflareMage = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(GhostBoltCooldown.FramesToSeconds(), BloodsplosionCooldown.FramesToSeconds());
		player.crimsonRegen = true;
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
		CreateRecipe().AddIngredient<BloodstoneCore>(11).AddIngredient<RuinousSoul>(2).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BloodflareBodyArmor>())
			.Register();
	}
}
