using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

internal class NavystoneBobber : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override string Texture => "CalamityMod/Projectiles/Typeless/NavyBobber";

	public override void SetDefaults()
	{
		base.Item.width = 9;
		base.Item.height = 9;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.accFishingBobber = true;
		player.Calamity().SelectedFishingMinigame = CalamityPlayer.FishingMinigames.NavystoneBobber;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(5139).AddIngredient<PearlShard>().AddIngredient<Navystone>(5)
			.AddTile(16)
			.Register();
	}
}
