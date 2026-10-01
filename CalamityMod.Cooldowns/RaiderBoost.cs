using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class RaiderBoost : CooldownHandler
{
	public string skinTexture;

	public Color outlineColor;

	public Color cooldownColorStart;

	public Color cooldownColorEnd;

	public new static string ID => "RaidersBoost";

	public override bool ShouldDisplay => true;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/" + skinTexture;

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

	public RaiderBoost()
		: this("")
	{
	}

	public RaiderBoost(string skin)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		if (skin == "Bloodfeast")
		{
			skinTexture = "VampiricTalismanBoost";
			outlineColor = new Color(143, 27, 27);
			cooldownColorStart = new Color(133, 5, 5);
			cooldownColorEnd = new Color(255, 0, 0);
		}
		else
		{
			skinTexture = "RaiderBoost";
			outlineColor = new Color(122, 97, 77);
			cooldownColorStart = new Color(168, 122, 86);
			cooldownColorEnd = new Color(74, 60, 49);
		}
	}
}
