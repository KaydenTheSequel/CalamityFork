using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class PanaceaCooldown : CooldownHandler
{
	public new static string ID => "Panacea";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/PanaceaCooldown";

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(122, 198, 255);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(0, 128, 255);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(0, 255, 255);
		}
	}

	public override SoundStyle? EndSound => new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/PotionSicknessOver");
}
