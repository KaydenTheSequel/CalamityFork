using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Reaver;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class ReaverCuisses : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.12f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 18;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PerennialBar>(10).AddIngredient<LivingShard>(2).AddTile(134)
			.Register();
	}
}
