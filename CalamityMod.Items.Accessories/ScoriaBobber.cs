using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

internal class ScoriaBobber : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override string Texture => "CalamityMod/Projectiles/Typeless/RiftReelerBobber";

	public override void SetDefaults()
	{
		base.Item.width = 9;
		base.Item.height = 9;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.accFishingBobber = true;
		player.accLavaFishing = true;
		player.Calamity().SelectedFishingMinigame = CalamityPlayer.FishingMinigames.ScoriaBobber;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(5139).AddIngredient<ScoriaBar>(5).AddTile(134)
			.Register();
	}
}
