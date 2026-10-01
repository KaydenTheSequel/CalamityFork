using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class SilvaRevive : CooldownHandler
{
	public new static string ID => "SilvaRevive";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/SilvaRevive";

	public override Color OutlineColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(151, 211, 152);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(226, 188, 74);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(151, 211, 152);
		}
	}

	public override bool CanTickDown
	{
		get
		{
			if (!CalamityPlayer.areThereAnyDamnBosses)
			{
				return !CalamityPlayer.areThereAnyDamnEvents;
			}
			return false;
		}
	}
}
