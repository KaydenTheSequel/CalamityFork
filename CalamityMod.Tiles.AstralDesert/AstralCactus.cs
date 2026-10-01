using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.AstralDesert;

public class AstralCactus : GlowMaskCactus
{
	public override void SetStaticDefaults()
	{
		base.GrowsOnTileId = new int[1] { ModContent.TileType<AstralSand>() };
	}

	public override Asset<Texture2D> GetTexture()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/AstralDesert/AstralCactus", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetGlowTexture()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/AstralDesert/AstralCactusGlow", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetFruitTexture()
	{
		return null;
	}

	public override Asset<Texture2D> GetFruitGlowTexture()
	{
		return null;
	}

	public override Color GetGlowColor(int i, int j)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
