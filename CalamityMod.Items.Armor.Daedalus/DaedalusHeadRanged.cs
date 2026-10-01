using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Daedalus;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "DaedalusHelmet" })]
public class DaedalusHeadRanged : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.13f;

	public static int RangedCritBoost = 7;

	public static float AmmoReduction = 0.8f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), RangedCritBoost, (1f - AmmoReduction).ToPercent());

	public static int ShardDamage => 30.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 9;
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
		player.Calamity().daedalusShard = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(7).AddIngredient<EssenceofEleum>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<DaedalusHeadMagic>())
			.Register();
	}
}
