using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "ElementalQuiver" })]
public class PlanebreakersPouch : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage<RangedDamageClass>() += 0.15f;
		player.GetCritChance<RangedDamageClass>() += 5f;
		player.magicQuiver = true;
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.deadshotBrooch = true;
		calamityPlayer.ammoCost *= 0.8f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips.IntegrateHotkey(CalamityKeybinds.AmmoCycleHotkey);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyQuiver").AddIngredient<DeadshotBrooch>().AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
