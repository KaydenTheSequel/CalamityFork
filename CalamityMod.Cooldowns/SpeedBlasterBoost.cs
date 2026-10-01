using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class SpeedBlasterBoost : CooldownHandler
{
	public new static string ID => "SpeedBlasterBoost";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/SpeedBlasterBoost";

	public override Color OutlineColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(207, 207, 207);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(235, 33, 130);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(39, 227, 208);
		}
	}
}
