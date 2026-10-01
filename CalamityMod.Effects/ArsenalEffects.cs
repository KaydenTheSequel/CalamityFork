using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Effects;

public class ArsenalEffects
{
	public static int ArsenalDust;

	public static int ArsenalPlasmaDust;

	public static Color ArsenalPlasmaColor;

	public static int ArsenalLaserDust;

	public static Color ArsenalLaserColor;

	public static int ArsenalPulseDust;

	public static Color ArsenalPulseColor;

	public static int ArsenalElectricDust;

	public static Color ArsenalElectricColor;

	public static int ArsenalGaussDust;

	public static Color ArsenalGaussColor;

	static ArsenalEffects()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		ArsenalDust = 278;
		ArsenalPlasmaDust = ModContent.DustType<SquashDustTileTouch>();
		ArsenalPlasmaColor = new Color(154, 255, 0);
		ArsenalLaserDust = ModContent.DustType<DiamondDust>();
		ArsenalLaserColor = Color.Lerp(Color.Crimson, Color.Red, 0.45f);
		ArsenalPulseDust = ModContent.DustType<SquashDustHollow>();
		ArsenalPulseColor = Color.Lerp(Color.DarkOrchid, Color.Magenta, 0.35f);
		ArsenalElectricDust = ModContent.DustType<UnstableDust>();
		ArsenalElectricColor = Color.Lerp(Color.Aqua, Color.Aquamarine, 0.35f);
		ArsenalGaussDust = ModContent.DustType<SquareDust>();
		ArsenalGaussColor = Color.Lerp(Color.Yellow, Color.Goldenrod, 0.25f);
	}
}
