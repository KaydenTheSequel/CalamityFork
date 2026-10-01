using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class DeepDiver : ModItem, ILocalizedModType, IModType
{
	public const int ShieldSlamDamage = 35;

	public const float ShieldSlamKnockback = 0.2f;

	public const int ShieldSlamIFrames = 16;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 28;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.deepDiver = true;
		calamityPlayer.DashID = DeepDiverDash.ID;
		player.dashType = 0;
		player.ignoreWater = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaPrism>(25).AddIngredient<MolluskHusk>(5).AddRecipeGroup("AnyCobaltBar", 10)
			.AddTile(16)
			.Register();
	}
}
