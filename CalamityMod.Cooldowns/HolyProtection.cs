using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class HolyProtection : CooldownHandler
{
	public new static string ID => "HolyProtection";

	public override bool ShouldDisplay => CalamityClientConfig.Instance.VanillaCooldownDisplay;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/HolyProtection";

	public override Color OutlineColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Orange;
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(Color.Yellow, Color.White, 1f - instance.Completion);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(Color.Yellow, Color.White, 1f - instance.Completion);
		}
	}
}
