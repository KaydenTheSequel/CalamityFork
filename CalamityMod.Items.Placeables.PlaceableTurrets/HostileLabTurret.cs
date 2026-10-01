using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.DraedonStructures;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.PlaceableTurrets;

public class HostileLabTurret : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override string Texture => "CalamityMod/Items/Placeables/PlaceableTurrets/LabTurret";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<DraedonLabTurret>());
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = 3;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(14).AddIngredient<DubiousPlating>(20).AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(1, out var condition), condition)
			.AddCondition(Condition.InGraveyard)
			.AddTile(16)
			.Register();
	}
}
