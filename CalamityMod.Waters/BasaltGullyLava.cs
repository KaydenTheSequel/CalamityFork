using CalamityMod.Dusts.WaterSplash;
using CalamityMod.Gores.WaterDroplet;
using CalamityMod.Systems;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class BasaltGullyLava : ModLavaStyle
{
	public override string WaterfallTexture => "CalamityMod/Waters/BasaltGullyLavaflow";

	public override int GetSplashDust()
	{
		return ModContent.DustType<BasaltGullyLavaSplash>();
	}

	public override int GetDropletGore()
	{
		return ModContent.GoreType<BasaltGullyLavaDroplet>();
	}

	public override bool IsLavaActive()
	{
		return Main.LocalPlayer.Calamity().ZoneBasaltGully;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.972549f;
		g = 0.28627452f;
		b = 0.28627452f;
	}
}
