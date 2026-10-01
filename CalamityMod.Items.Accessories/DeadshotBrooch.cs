using System.Collections.Generic;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "DaedalusEmblem" })]
public class DeadshotBrooch : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 40;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().deadshotBrooch = true;
		player.Calamity().ammoCost *= 0.8f;
		player.GetDamage<RangedDamageClass>() += 0.12f;
		player.GetCritChance<RangedDamageClass>() += 7f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips.IntegrateHotkey(CalamityKeybinds.AmmoCycleHotkey);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(491).AddIngredient<CoreofCalamity>(2).AddTile(134)
			.Register();
	}
}
