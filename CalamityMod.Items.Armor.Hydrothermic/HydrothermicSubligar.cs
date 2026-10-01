using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Hydrothermic;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
[LegacyName(new string[] { "AtaxiaSubligar" })]
public class HydrothermicSubligar : ModItem, ILocalizedModType, IModType
{
	public static int CritBoost = 9;

	public static float MoveSpeedBoost = 0.1f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.defense = 14;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(10).AddIngredient<EssenceofHavoc>(2).AddTile(134)
			.Register();
	}
}
