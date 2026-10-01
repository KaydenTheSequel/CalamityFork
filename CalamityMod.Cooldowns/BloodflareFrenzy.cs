using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class BloodflareFrenzy : CooldownHandler
{
	public new static string ID => "BloodflareFrenzy";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/BloodflareFrenzy";

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(229, 171, 124);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(new Color(149, 127, 109), new Color(220, 101, 101), 1f - instance.Completion);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(new Color(149, 127, 109), new Color(220, 101, 101), 1f - instance.Completion);
		}
	}
}
