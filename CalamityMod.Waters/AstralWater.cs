using CalamityMod.Dusts.WaterSplash;
using CalamityMod.Gores.WaterDroplet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class AstralWater : ModWaterStyle
{
	public static ModWaterStyle Instance { get; private set; }

	public static ModWaterfallStyle WaterfallStyle { get; private set; }

	public static int SplashDust { get; private set; }

	public static int DropletGore { get; private set; }

	public static Asset<Texture2D> RainTexture { get; private set; }

	public override void SetStaticDefaults()
	{
		Instance = this;
		WaterfallStyle = ModContent.Find<ModWaterfallStyle>("CalamityMod/AstralWaterflow");
		SplashDust = ModContent.DustType<AstralSplash>();
		DropletGore = ModContent.GoreType<AstralWaterDroplet>();
	}

	public override void Unload()
	{
		Instance = null;
		WaterfallStyle = null;
		SplashDust = 0;
		DropletGore = 0;
	}

	public override int ChooseWaterfallStyle()
	{
		return WaterfallStyle.Slot;
	}

	public override int GetSplashDust()
	{
		return SplashDust;
	}

	public override int GetDropletGore()
	{
		return DropletGore;
	}

	public override Asset<Texture2D> GetRainTexture()
	{
		return RainTexture ?? (RainTexture = ModContent.Request<Texture2D>("CalamityMod/Waters/AstralRain", (AssetRequestMode)2));
	}

	public override byte GetRainVariant()
	{
		return (byte)Main.rand.Next(3);
	}

	public override Color BiomeHairColor()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return new Color(93, 78, 107);
	}
}
