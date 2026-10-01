using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class PotionSickness : CooldownHandler
{
	public new static string ID => "PotionSickness";

	public override bool ShouldDisplay
	{
		get
		{
			if (CalamityClientConfig.Instance.VanillaCooldownDisplay)
			{
				return instance.player.potionDelay > 0;
			}
			return false;
		}
	}

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/PotionSickness";

	public override Color OutlineColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 142, 165);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(new Color(208, 234, 255), new Color(231, 3, 54), instance.Completion);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(new Color(208, 234, 255), new Color(231, 3, 54), instance.Completion);
		}
	}

	public override SoundStyle? EndSound => new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/PotionSicknessOver");
}
