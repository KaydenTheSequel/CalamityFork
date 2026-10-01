using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.ExtraTextures;

[Autoload(true, Side = ModSide.Client)]
public class ExtraTextureRefs : ModSystem
{
	public static Asset<Texture2D> DestroyerHeadGlowmask;

	public static Asset<Texture2D> DestroyerBodyGlowmask;

	public static Asset<Texture2D> DestroyerTailGlowmask;

	public static Asset<Texture2D> WallOfFleshEyeGlowmask;

	public static Asset<Texture2D> WallOfFleshDemonSickleTexture;

	public static Asset<Texture2D> FlyingCarpetVanilla;

	public static Asset<Texture2D> FlyingCarpetAuric;

	public static Asset<Texture2D> LuckIconGreater;

	public static Asset<Texture2D> LuckIconVanilla;

	public static Asset<Texture2D> LuckIconLesser;

	public static Asset<Texture2D> CircularSmear;

	public static Asset<Texture2D> CircularSmearFire1;

	public static Asset<Texture2D> CircularSmearFire2;

	public static Asset<Texture2D> CircularSmearFire3;

	public override void OnModLoad()
	{
		DestroyerHeadGlowmask = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBossGlowmasks/DestroyerHeadGlow", (AssetRequestMode)2);
		DestroyerBodyGlowmask = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBossGlowmasks/DestroyerBodyGlow", (AssetRequestMode)2);
		DestroyerTailGlowmask = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBossGlowmasks/DestroyerTailGlow", (AssetRequestMode)2);
		WallOfFleshEyeGlowmask = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBossGlowmasks/WallOfFleshEyeTelegraphGlow", (AssetRequestMode)2);
		WallOfFleshDemonSickleTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/ForbiddenOathbladeProjectile", (AssetRequestMode)2);
		FlyingCarpetVanilla = TextureAssets.FlyingCarpet;
		FlyingCarpetAuric = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/AuricCarpet", (AssetRequestMode)2);
		LuckIconGreater = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBuffs/LuckyGreater", (AssetRequestMode)2);
		LuckIconVanilla = TextureAssets.Buff[257];
		LuckIconLesser = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/VanillaBuffs/LuckyLesser", (AssetRequestMode)2);
		CircularSmear = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmear", (AssetRequestMode)2);
		CircularSmearFire1 = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearFire1", (AssetRequestMode)2);
		CircularSmearFire2 = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearFire2", (AssetRequestMode)2);
		CircularSmearFire3 = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearFire3", (AssetRequestMode)2);
	}

	public override void Unload()
	{
		if (!Main.dedServ)
		{
			TextureAssets.FlyingCarpet = FlyingCarpetVanilla;
			TextureAssets.Buff[257] = LuckIconVanilla;
		}
	}
}
