using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class SupremeBaitTackleBoxFishingStation : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 52;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.fishingSkill += 50;
		player.accFishingLine = true;
		player.accTackleBox = true;
		player.accLavaFishing = true;
		player.Calamity().fishingStation = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(5064).AddIngredient(2676, 5).AddIngredient<MolluskHusk>(5)
			.AddTile(16)
			.Register();
	}
}
