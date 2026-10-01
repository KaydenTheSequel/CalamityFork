using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class ChaosState : CooldownHandler
{
	public string skinTexture;

	public Color outlineColor;

	public Color cooldownColorStart;

	public Color cooldownColorEnd;

	public new static string ID => "ChaosState";

	public override bool ShouldDisplay
	{
		get
		{
			if (CalamityClientConfig.Instance.VanillaCooldownDisplay)
			{
				return instance.player.chaosState;
			}
			return false;
		}
	}

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/ChaosState" + skinTexture;

	public override Color OutlineColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return outlineColor;
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(cooldownColorStart, cooldownColorEnd, 1f - instance.Completion);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(cooldownColorStart, cooldownColorEnd, 1f - instance.Completion);
		}
	}

	public override SoundStyle? EndSound => new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/ChaosStateOver");

	public ChaosState()
		: this("")
	{
	}

	public ChaosState(string skin)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		if (!(skin == "spectralveil"))
		{
			if (skin == "normalityrelocator")
			{
				skinTexture = "NR";
				outlineColor = new Color(129, 239, 246);
				cooldownColorStart = new Color(134, 143, 151);
				cooldownColorEnd = new Color(129, 239, 246);
			}
			else
			{
				skinTexture = "";
				outlineColor = new Color(246, 116, 181);
				cooldownColorStart = new Color(223, 58, 140);
				cooldownColorEnd = new Color(255, 179, 218);
			}
		}
		else
		{
			skinTexture = "Veil";
			outlineColor = new Color(138, 120, 222);
			cooldownColorStart = new Color(46, 46, 134);
			cooldownColorEnd = new Color(81, 90, 156);
		}
	}
}
