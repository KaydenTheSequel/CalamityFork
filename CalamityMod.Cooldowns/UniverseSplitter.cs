using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class UniverseSplitter : CooldownHandler
{
	public new static string ID => "UniverseSplitter";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/UniverseSplitter";

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
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			return CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.3f % 1f, new Color(236, 202, 255), new Color(192, 245, 255), new Color(255, 194, 205));
		}
	}
}
