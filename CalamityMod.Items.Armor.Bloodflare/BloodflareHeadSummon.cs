using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Bloodflare;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "BloodflareHelmet" })]
public class BloodflareHeadSummon : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.3f;

	public static int SetBonusMinionSlotBoost = 2;

	public static float SetBonusSummonDamageBoost = 0.3f;

	public static int MineDamage = 3750;

	public static int MineCooldown = 900;

	public static int DefenseBoostBelowHealthThreshold = 20;

	public static float DefenseBoostHealthThreshold = 0.5f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	internal static string GhostMineEntitySourceContext => "SetBonus_Calamity_BloodflareSummon";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 12;
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
		calamityPlayer.bloodflareSummon = true;
		calamityPlayer.WearingPostMLSummonerSet = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent(), DefenseBoostBelowHealthThreshold, DefenseBoostHealthThreshold.ToPercent());
		player.crimsonRegen = true;
		player.maxMinions += SetBonusMinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(11).AddIngredient<RuinousSoul>(2).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<BloodflareHeadRogue>())
			.Register();
	}
}
