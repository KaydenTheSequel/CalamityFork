using CalamityMod.Items.Materials;
using CalamityMod.Tiles.FurnitureWulfrum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureWulfrum;

public class WulfrumPlatform : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.FurnitureWulfrum.WulfrumPlatform>());
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Placeables/FurnitureWulfrum/WulfrumPlatform_Glow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient<WulfrumMetalScrap>().Register();
	}
}
