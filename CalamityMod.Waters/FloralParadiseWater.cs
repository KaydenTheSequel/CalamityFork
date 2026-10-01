using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Waters;

public class FloralParadiseWater : ModWaterStyle
{
	public static int Type { get; private set; }

	public static ModWaterStyle Instance { get; private set; }

	public override void SetStaticDefaults()
	{
		Type = base.Slot;
		Instance = this;
	}

	public override void Unload()
	{
		Type = -1;
		Instance = null;
	}

	public override int ChooseWaterfallStyle()
	{
		return ModContent.Find<ModWaterfallStyle>("CalamityMod/FloralParadiseWaterflow").Slot;
	}

	public override int GetSplashDust()
	{
		return 33;
	}

	public override int GetDropletGore()
	{
		return 713;
	}

	public override Color BiomeHairColor()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.PaleTurquoise;
	}
}
