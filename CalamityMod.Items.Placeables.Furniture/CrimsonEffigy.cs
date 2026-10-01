using CalamityMod.Tiles.Furniture;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class CrimsonEffigy : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.15f;

	public static int DefenseBoost = 10;

	public static float MaxHealthLossPercent = 0.1f;

	public new string LocalizationCategory => "Items.Placeables";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), DefenseBoost, MaxHealthLossPercent.ToPercent());

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Furniture.CrimsonEffigy>());
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CorruptionEffigy>().AddTile(114).AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
	}
}
