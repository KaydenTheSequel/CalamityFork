using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Daedalus;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "DaedalusHelm" })]
public class DaedalusHeadMelee : ModItem, ILocalizedModType, IModType
{
	public static float MeleeDamageBoost = 0.1f;

	public static int MeleeCritBoost = 10;

	public static float MeleeSpeedBoost = 0.1f;

	public static int SetBonusAggroBoost = 500;

	public static int ReflectCooldownMin = CalamityUtils.SecondsToFrames(20);

	public static int ReflectCooldownMax = CalamityUtils.SecondsToFrames(90);

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBoost.ToPercent(), MeleeSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 21;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<DaedalusBreastplate>())
		{
			return legs.type == ModContent.ItemType<DaedalusLeggings>();
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
		player.setBonus = this.GetLocalizedValue("SetBonus");
		player.Calamity().daedalusReflect = true;
		player.aggro += SetBonusAggroBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MeleeDamageClass>() += MeleeDamageBoost;
		player.GetCritChance<MeleeDamageClass>() += MeleeCritBoost;
		player.GetAttackSpeed<MeleeDamageClass>() += MeleeSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(7).AddIngredient<EssenceofEleum>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<DaedalusHeadMagic>())
			.Register();
	}
}
