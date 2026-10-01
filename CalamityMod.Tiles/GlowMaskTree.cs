using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public abstract class GlowMaskTree : ModTree
{
	public abstract Asset<Texture2D> GetGlowTexture();

	public abstract Asset<Texture2D> GetTopGlowTextures();

	public abstract Asset<Texture2D> GetBranchGlowTextures();

	public abstract Color GetGlowColor(int i, int j);
}
