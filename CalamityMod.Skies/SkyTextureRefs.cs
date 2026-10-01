using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

[Autoload(true, Side = ModSide.Client)]
internal sealed class SkyTextureRefs : ModSystem
{
	public static Asset<Texture2D> AstralSky;

	public static Asset<Texture2D> AstralSurfaceFront;

	public static Asset<Texture2D> AstralSurfaceFrontGlow;

	public static Asset<Texture2D> AstralSurfaceClose;

	public static Asset<Texture2D> AstralSurfaceCloseGlow;

	public static Asset<Texture2D> AstralSurfaceMiddle;

	public static Asset<Texture2D> AstralSurfaceMiddleGlow;

	public static Asset<Texture2D> AstralDesertSurfaceClose;

	public static Asset<Texture2D> AstralDesertSurfaceMiddle;

	public static Asset<Texture2D> AstralSnowSurfaceMiddle;

	public static Asset<Texture2D> SulphurSeaSky;

	public static Asset<Texture2D> SulphurSeaSkyFront;

	public static Asset<Texture2D> SulphurSeaSurface;

	public override void OnModLoad()
	{
		AstralSky = ModContent.Request<Texture2D>("CalamityMod/Skies/AstralSky", (AssetRequestMode)2);
		AstralSurfaceFront = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralSurfaceFront", (AssetRequestMode)2);
		AstralSurfaceFrontGlow = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralSurfaceFrontGlow", (AssetRequestMode)2);
		AstralSurfaceClose = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralSurfaceClose", (AssetRequestMode)2);
		AstralSurfaceCloseGlow = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralSurfaceCloseGlow", (AssetRequestMode)2);
		AstralSurfaceMiddle = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralSurfaceMiddle", (AssetRequestMode)2);
		AstralSurfaceMiddleGlow = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralSurfaceMiddleGlow", (AssetRequestMode)2);
		AstralDesertSurfaceClose = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralDesertSurfaceClose", (AssetRequestMode)2);
		AstralDesertSurfaceMiddle = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralDesertSurfaceMiddle", (AssetRequestMode)2);
		AstralSnowSurfaceMiddle = ModContent.Request<Texture2D>("CalamityMod/Backgrounds/AstralSnowSurfaceMiddle", (AssetRequestMode)2);
		SulphurSeaSky = ModContent.Request<Texture2D>("CalamityMod/Skies/SulphurSeaSky", (AssetRequestMode)2);
		SulphurSeaSkyFront = ModContent.Request<Texture2D>("CalamityMod/Skies/SulphurSeaSkyFront", (AssetRequestMode)2);
		SulphurSeaSurface = ModContent.Request<Texture2D>("CalamityMod/Skies/SulphurSeaSurface", (AssetRequestMode)2);
	}

	public override void Unload()
	{
		AstralSky = null;
		AstralSurfaceFront = null;
		AstralSurfaceFrontGlow = null;
		AstralSurfaceClose = null;
		AstralSurfaceCloseGlow = null;
		AstralSurfaceMiddle = null;
		AstralSurfaceMiddleGlow = null;
		AstralDesertSurfaceClose = null;
		AstralDesertSurfaceMiddle = null;
		AstralSnowSurfaceMiddle = null;
		SulphurSeaSky = null;
		SulphurSeaSkyFront = null;
		SulphurSeaSurface = null;
	}
}
