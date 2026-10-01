using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.GemTech;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class GemTechSchynbaulds : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 26;
		base.Item.defense = 24;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		GemTechHeadgear.ModifySetTooltips(this, tooltips);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ExoPrism>(12).AddIngredient<GalacticaSingularity>(4).AddIngredient<CoreofCalamity>(2)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
