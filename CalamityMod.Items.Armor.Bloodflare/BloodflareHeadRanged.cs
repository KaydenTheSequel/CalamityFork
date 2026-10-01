using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Bloodflare;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "BloodflareHornedHelm" })]
public class BloodflareHeadRanged : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/BloodflareRangerActivation");

	public static float RangedDamageBoost = 0.1f;

	public static float AmmoReduction = 0.75f;

	public static int RangedCritBoost = 10;

	public static int SoulCooldown = CalamityUtils.SecondsToFrames(30);

	public static int SoulDamage = 300;

	public static int SoulAmount = 16;

	public static int BloodBombCooldown = CalamityUtils.SecondsToFrames(2.5f);

	public static double BloodBombDamageRatio = 0.8;

	public static int BloodBombDamageSoftcap = 120;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 30;
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
		calamityPlayer.bloodflareRanged = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(CalamityUtils.GetArmorSetBonusKey(), SoulCooldown.FramesToSeconds(), BloodBombCooldown.FramesToSeconds());
		player.crimsonRegen = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(11).AddIngredient<RuinousSoul>(2).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BloodflareHeadMagic>())
			.Register();
	}
}
