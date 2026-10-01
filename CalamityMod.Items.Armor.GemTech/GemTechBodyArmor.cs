using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.GemTech;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class GemTechBodyArmor : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 32;
		base.Item.defense = 31;
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
		CreateRecipe().AddIngredient<ExoPrism>(16).AddIngredient<GalacticaSingularity>(5).AddIngredient<CoreofCalamity>(2)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
