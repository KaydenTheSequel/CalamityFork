using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Silva;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "SilvaHelmet" })]
public class SilvaHeadSummon : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 2;

	public static float SummonDamageBoost = 0.3f;

	public static int SetBonusMinionSlotBoost = 3;

	public static float SetBonusSummonDamageBoost = 0.4f;

	public static int CrystalDamage = 380;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	internal static string SilvaCrystalEntitySourceContext => "SetBonus_Calamity_SilvaSummon";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 24;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 18;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<SilvaArmor>())
		{
			return legs.type == ModContent.ItemType<SilvaLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.silvaSet = true;
		calamityPlayer.silvaSummon = true;
		calamityPlayer.WearingPostMLSummonerSet = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent(), SilvaArmor.SetBonusRegenBoost.ToRegenPerSecond(), SilvaArmor.AccelerationBoost.ToPercent(), SilvaArmor.ReviveDuration.FramesToSeconds(), (SilvaArmor.ReviveCooldown / 60).FramesToSeconds());
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
		CreateRecipe().AddIngredient<PlantyMush>(6).AddIngredient<EffulgentFeather>(5).AddIngredient<AscendantSpiritEssence>(2)
			.AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<SilvaArmor>())
			.Register();
	}
}
