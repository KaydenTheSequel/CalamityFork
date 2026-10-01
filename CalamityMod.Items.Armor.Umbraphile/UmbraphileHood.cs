using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Umbraphile;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class UmbraphileHood : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.08f;

	public static float RogueVelocityBoost = 0.1f;

	public static float SetBonusRogueStealth = 1.1f;

	public static double ExplosionDamageRatio = 0.2;

	public static int ExplosionDamageSoftcap = 50;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueVelocityBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 10;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<UmbraphileRegalia>())
		{
			return legs.type == ModContent.ItemType<UmbraphileBoots>();
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
		calamityPlayer.umbraphileSet = true;
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth());
		player.Calamity().wearingRogueArmor = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.Calamity().rogueVelocity += RogueVelocityBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SolarVeil>(12).AddIngredient(1225, 8).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<UmbraphileBoots>())
			.Register();
	}
}
