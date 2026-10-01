using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

internal class ScrapBobber : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override string Texture => "CalamityMod/Projectiles/Typeless/WulfrumBobber";

	public override void SetDefaults()
	{
		base.Item.width = 9;
		base.Item.height = 9;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().SelectedFishingMinigame = CalamityPlayer.FishingMinigames.ScrapBobber;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(5).AddTile(16).Register();
	}
}
