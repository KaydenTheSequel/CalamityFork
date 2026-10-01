using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class HallowedRune : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public static int RegenBoost = 3;

	public static int DefenseBoost = 5;

	public static float SummonDamageBoost = 0.1f;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().hallowedRune = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SpiritGlyph>().AddIngredient(1225, 18).AddIngredient(547, 5)
			.AddIngredient(548, 5)
			.AddIngredient(549, 5)
			.AddTile(134)
			.Register();
	}
}
