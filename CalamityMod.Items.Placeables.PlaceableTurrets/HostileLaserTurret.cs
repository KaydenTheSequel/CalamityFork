using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Tiles.DraedonStructures;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.PlaceableTurrets;

public class HostileLaserTurret : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override string Texture => "CalamityMod/Items/Placeables/PlaceableTurrets/LaserTurret";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.DraedonStructures.HostileLaserTurret>());
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = 5;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 2);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(14).AddIngredient<DubiousPlating>(20).AddIngredient<Cinderplate>(10)
			.AddIngredient<EssenceofSunlight>(12)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(2, out var condition), condition)
			.AddTile(134)
			.AddCondition(Condition.InGraveyard)
			.Register();
	}
}
