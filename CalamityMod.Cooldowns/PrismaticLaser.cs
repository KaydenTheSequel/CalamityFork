using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class PrismaticLaser : CooldownHandler
{
	public new static string ID => "PrismaticLaser";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/PrismaticLaser";

	public override Color OutlineColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return rainbowMode;
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return rainbowMode;
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return rainbowMode;
		}
	}

	internal Color rainbowMode
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			return CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.3f % 1f, new Color(103, 244, 251), new Color(255, 167, 236), new Color(255, 225, 136));
		}
	}
}
