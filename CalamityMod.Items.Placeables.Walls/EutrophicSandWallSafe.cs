using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Walls;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Walls;

public class EutrophicSandWallSafe : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 400;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<EutrophicSandWall>();
	}

	public override void SetDefaults()
	{
		base.Item.width = 12;
		base.Item.height = 12;
		base.Item.maxStack = 9999;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.useAnimation = 15;
		base.Item.useTime = 7;
		base.Item.useStyle = 1;
		base.Item.consumable = true;
		base.Item.createWall = ModContent.WallType<global::CalamityMod.Walls.EutrophicSandWallSafe>();
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient<EutrophicSand>().AddTile(18).Register();
	}
}
