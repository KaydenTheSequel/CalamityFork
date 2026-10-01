using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class PissWater : ModWaterStyle
{
	public static ModWaterStyle Instance { get; private set; }

	public static ModWaterfallStyle WaterfallStyle { get; private set; }

	public static int SplashDust { get; private set; }

	public static int DropletGore { get; private set; }

	public override void SetStaticDefaults()
	{
		Instance = this;
		WaterfallStyle = ModContent.Find<ModWaterfallStyle>("CalamityMod/PissWaterflow");
		SplashDust = 102;
		DropletGore = 711;
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
		return 102;
	}

	public override int GetDropletGore()
	{
		return 711;
	}

	public override Color BiomeHairColor()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.Yellow;
	}
}
