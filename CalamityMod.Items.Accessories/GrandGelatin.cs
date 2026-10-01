using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class GrandGelatin : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.12f;

	public static float JumpSpeedBoost = 0.6f;

	public static int AuraLifetime = 1800;

	public static int AuraRegenBoost = 4;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBoost.ToPercent(), AuraLifetime.FramesToSeconds(), AuraRegenBoost.ToRegenPerSecond());

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 52;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().GrandGelatin = true;
		player.moveSpeed += MoveSpeedBoost;
		player.jumpSpeedBoost += JumpSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CleansingJelly>().AddIngredient<LifeJelly>().AddIngredient<VitalJelly>()
			.AddIngredient(520, 2)
			.AddIngredient(521, 2)
			.AddTile(16)
			.Register();
	}
}
