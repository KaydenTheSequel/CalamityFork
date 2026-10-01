using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class RelicOfResilienceCooldown : CooldownHandler
{
	public new static string ID => "RelicOfResilienceCooldown";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/RelicOfResilienceCooldown";

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 191, 73);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return new Color(122, 66, 59);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(165, 103, 87);
		}
	}
}
