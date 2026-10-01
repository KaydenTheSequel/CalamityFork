using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class TarragonCloak : CooldownHandler
{
	public new static string ID => "TarragonCloak";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/TarragonCloak";

	public override Color OutlineColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(158, 204, 173);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(new Color(171, 106, 49), new Color(215, 182, 82), 1f - instance.Completion);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(new Color(171, 106, 49), new Color(215, 182, 82), 1f - instance.Completion);
		}
	}
}
