using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Astral;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class AstralHelm : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.05f;

	public static int CritBoost = 15;

	public static int SetBonusMinionSlotBoost = 3;

	public static float SetBonusDamageBoost = 0.1f;

	public static int SetBonusCritBoost = 10;

	public static int StarRainCooldown = CalamityUtils.SecondsToFrames(1);

	public static int StarDamage = 120;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 9;
		base.Item.defense = 17;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<AstralBreastplate>())
		{
			return legs.type == ModContent.ItemType<AstralLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusDamageBoost.ToPercent(), StarRainCooldown.FramesToSeconds());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.astralStarRain = true;
		calamityPlayer.omniscience = true;
		player.maxMinions += SetBonusMinionSlotBoost;
		player.GetDamage<GenericDamageClass>() += SetBonusDamageBoost;
		player.GetCritChance<GenericDamageClass>() += SetBonusCritBoost;
		player.Calamity().wearingRogueArmor = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralBar>(8).AddIngredient(117, 6).AddTile(412)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<AstralBreastplate>())
			.Register();
	}
}
