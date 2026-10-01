using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Tiles.DraedonStructures;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.PlaceableTurrets;

public class HostileWaterTurret : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override string Texture => "CalamityMod/Items/Placeables/PlaceableTurrets/WaterTurret";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.DraedonStructures.HostileWaterTurret>());
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = 3;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(14).AddIngredient<DubiousPlating>(20).AddIngredient<Navyplate>(10)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(1, out var condition), condition)
			.AddCondition(Condition.InGraveyard)
			.AddTile(16)
			.Register();
	}
}
