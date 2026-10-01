using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Aerospec;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class AerospecLeggings : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.12f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.defense = 6;
	}

	public override void UpdateEquip(Player player)
	{
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(7).AddIngredient(824, 4).AddIngredient(320, 2)
			.AddTile(16)
			.Register();
	}
}
