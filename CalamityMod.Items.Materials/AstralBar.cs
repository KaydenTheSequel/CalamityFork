using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class AstralBar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 99;
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(base.Type, 1, 117, 1);
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(5, 12));
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.AstralBar>());
		base.Item.rare = 9;
		base.Item.value = Item.sellPrice(0, 1, 20);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StarblightSoot>(3).AddIngredient<AstralOre>(2).AddTile(412)
			.Register();
	}
}
