using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.TitanHeart;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class TitanHeartMask : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.07f;

	public static float RogueVelocityBoost = 0.1f;

	public static int OnHitDebuffDuration = CalamityUtils.SecondsToFrames(2);

	public static float SetBonusRogueStealth = 1f;

	public static float StealthStrikeKnockbackMult = 2f;

	public static int ExplosionDamage = 40;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueVelocityBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.defense = 8;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<TitanHeartMantle>())
		{
			return legs.type == ModContent.ItemType<TitanHeartBoots>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), StealthStrikeKnockbackMult);
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.titanHeartSet = true;
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		calamityPlayer.wearingRogueArmor = true;
		player.noKnockback = true;
	}

	public override void UpdateEquip(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.titanHeartMask = true;
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		calamityPlayer.rogueVelocity += RogueVelocityBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralMonolith>(10).AddIngredient<global::CalamityMod.Items.Materials.TitanHeart>().AddTile(16)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<TitanHeartMantle>())
			.Register();
	}
}
