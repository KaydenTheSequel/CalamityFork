using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.SnowRuffian;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class SnowRuffianGreaves : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.05f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 3;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2503, 15).AddIngredient(225, 5).AddIngredient(5070)
			.AddTile(16)
			.Register();
	}
}
