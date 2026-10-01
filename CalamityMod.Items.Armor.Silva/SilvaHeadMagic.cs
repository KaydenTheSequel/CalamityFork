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
[LegacyName(new string[] { "SilvaMaskedCap" })]
public class SilvaHeadMagic : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 100;

	public static float ManaCostReduction = 0.19f;

	public static float MagicDamageBoost = 0.18f;

	public static int MagicCritBoost = 10;

	public static int BurstCooldown = CalamityUtils.SecondsToFrames(5);

	public static int BurstDamage = 800;

	public static double BurstDamageRatio = 0.6;

	public static int BurstDamageSoftcap = 1400;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), MagicDamageBoost.ToPercent(), MagicCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 24;
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
		calamityPlayer.silvaMage = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SilvaArmor.SetBonusRegenBoost.ToRegenPerSecond(), SilvaArmor.AccelerationBoost.ToPercent(), SilvaArmor.ReviveDuration.FramesToSeconds(), (SilvaArmor.ReviveCooldown / 60).FramesToSeconds());
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
		CreateRecipe().AddIngredient<PlantyMush>(6).AddIngredient<EffulgentFeather>(5).AddIngredient<AscendantSpiritEssence>(2)
			.AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<SilvaArmor>())
			.Register();
	}
}
