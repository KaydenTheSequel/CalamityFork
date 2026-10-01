using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class AncientFossil : ModItem, ILocalizedModType, IModType
{
	public static float MiningSpeedBoost = 0.1f;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.pickSpeed -= MiningSpeedBoost;
		player.Calamity().aFossil = true;
		player.Calamity().fallingBlockProtection = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnySiltBlock", 100).AddTile(17).Register();
	}
}
