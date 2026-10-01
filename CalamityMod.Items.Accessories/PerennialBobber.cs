using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

internal class PerennialBobber : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override string Texture => "CalamityMod/Projectiles/Typeless/FeralDoubleBobber";

	public override void SetDefaults()
	{
		base.Item.width = 9;
		base.Item.height = 9;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.accFishingBobber = true;
		player.fishingSkill += (int)((float)(player.statLifeMax2 - player.statLife) * 0.25f);
		player.Calamity().SelectedFishingMinigame = CalamityPlayer.FishingMinigames.PerennialBobber;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(5139).AddIngredient<PerennialBar>(5).AddTile(134)
			.Register();
	}
}
