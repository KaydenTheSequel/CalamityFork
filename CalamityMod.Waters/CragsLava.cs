using CalamityMod.Dusts.WaterSplash;
using CalamityMod.Gores.WaterDroplet;
using CalamityMod.Systems;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class CragsLava : ModLavaStyle
{
	public override string WaterfallTexture => "CalamityMod/Waters/CragsLavaflow";

	public override int GetSplashDust()
	{
		return ModContent.DustType<CragsLavaSplash>();
	}

	public override int GetDropletGore()
	{
		return ModContent.GoreType<CragsLavaDroplet>();
	}

	public override bool IsLavaActive()
	{
		if (!Main.LocalPlayer.Calamity().ZoneCalamity)
		{
			return Main.LocalPlayer.Calamity().BrimstoneLavaFountainCounter > 0;
		}
		return true;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.62f;
		g = 0.2625f;
		b = 0.245f;
	}

	public override void InflictDebuff(Player player, int onfireDuration)
	{
	}
}
