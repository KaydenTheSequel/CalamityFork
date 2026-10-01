using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class AscendEffect : CooldownHandler
{
	public new static string ID => "AscendEffect";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/AscendEffect";

	public override Color OutlineColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(197, 165, 108);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(144, 84, 29);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Khaki;
		}
	}

	public override SoundStyle? EndSound => new SoundStyle("CalamityMod/Sounds/Item/AscendantOff");
}
