using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class CorruptionEffigy : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.1f;

	public static int CritBoost = 10;

	public static float DamageReductionLoss = 0.05f;

	public new string LocalizationCategory => "Items.Placeables";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(CritBoost, DamageReductionLoss.ToPercent());

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Furniture.CorruptionEffigy>());
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CrimsonEffigy>().AddTile(114).AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
	}
}
