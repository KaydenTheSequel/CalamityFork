using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing;

internal class SkylineBobber : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override string Texture => "CalamityMod/Projectiles/Typeless/HeronBobber";

	public override void SetDefaults()
	{
		base.Item.width = 9;
		base.Item.height = 9;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.accFishingBobber = true;
		player.Calamity().SelectedFishingMinigame = CalamityPlayer.FishingMinigames.SkylineBobber;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(5139).AddIngredient<AerialiteBar>(5).AddIngredient(320)
			.AddTile(16)
			.Register();
	}
}
