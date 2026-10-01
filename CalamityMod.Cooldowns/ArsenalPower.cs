using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class ArsenalPower : CooldownHandler
{
	public new static string ID => "ArsenalPower";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/ArsenalPower";

	public override Color OutlineColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Sienna;
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.LightSlateGray;
		}
	}

	public override SoundStyle? EndSound
	{
		get
		{
			if (!Main.zenithWorld)
			{
				return new SoundStyle("CalamityMod/Sounds/Item/ArsenalOffCooldown");
			}
			return null;
		}
	}

	public override void Tick()
	{
		if (Main.zenithWorld)
		{
			instance.timeLeft = -1;
		}
	}
}
