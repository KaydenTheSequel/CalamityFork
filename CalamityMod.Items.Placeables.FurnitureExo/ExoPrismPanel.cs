using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureExo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureExo;

public class ExoPrismPanel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<ExoPrismPanelTile>());
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Placeables/FurnitureExo/ExoPrismPanel_Glow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe(400).AddIngredient(170, 400).AddIngredient<ExoPrism>().AddTile<DraedonsForge>()
			.Register();
		CreateRecipe().AddIngredient<ExoPrismPlatform>(2).DisableDecraft().Register();
		CreateRecipe().AddIngredient<ExoPrismPanelWallItem>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
