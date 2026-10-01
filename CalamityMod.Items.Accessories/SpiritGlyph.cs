using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "SpiritGenerator" })]
public class SpiritGlyph : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public static int RegenBoost = 2;

	public static int DefenseBoost = 3;

	public static float SummonDamageBoost = 0.1f;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 30;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().sGlyph = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(182, 5).AddIngredient(173, 15).AddTile(16)
			.Register();
	}
}
