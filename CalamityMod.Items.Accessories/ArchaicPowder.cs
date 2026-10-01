using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Crags;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ArchaicPowder : ModItem, ILocalizedModType, IModType
{
	public static float MiningSpeedBoost = 0.25f;

	public static float TrapDamageReduction = 0.65f;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningSpeedBoost.ToPercent(), TrapDamageReduction.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 34;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.pickSpeed -= MiningSpeedBoost;
		player.Calamity().aPowder = true;
		player.Calamity().fallingBlockProtection = true;
		player.Calamity().trapProtection = true;
		if (player.chiselSpeed)
		{
			player.pickSpeed += 0.15f;
		}
		if (player.Calamity().aFossil)
		{
			player.pickSpeed += AncientFossil.MiningSpeedBoost;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AncientFossil>().AddIngredient(4056).AddIngredient<AncientBoneDust>(3)
			.AddIngredient<ScorchedBone>(10)
			.AddIngredient(154, 15)
			.AddTile(16)
			.Register();
	}
}
