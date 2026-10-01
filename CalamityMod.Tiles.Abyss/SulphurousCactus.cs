using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class SulphurousCactus : ModCactus
{
	public override void SetStaticDefaults()
	{
		base.GrowsOnTileId = new int[1] { ModContent.TileType<SulphurousSand>() };
	}

	public override Asset<Texture2D> GetTexture()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Abyss/SulphurousCactus", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetFruitTexture()
	{
		return null;
	}
}
