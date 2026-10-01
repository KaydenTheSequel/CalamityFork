using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Tarragon;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "TarragonVisage" })]
public class TarragonHeadRanged : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.1f;

	public static int RangedCritBoost = 7;

	public static float AmmoReduction = 0.75f;

	public static int OnHitEffectCooldown = CalamityUtils.SecondsToFrames(1);

	public static float LeafDamageRatio = 0.25f;

	public static int LeafDamageSoftcap = 150;

	public static float EnergyDamageRatio = 0.33f;

	public static int EnergyDamageSoftcap = 200;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), RangedCritBoost, (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.defense = 28;
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
		calamityPlayer.tarraRanged = true;
		player.setBonus = this.GetLocalizedValue("SetBonus");
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(7).AddIngredient<DivineGeode>(6).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<TarragonHeadMagic>())
			.Register();
	}
}
