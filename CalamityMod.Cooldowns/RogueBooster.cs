using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class RogueBooster : CooldownHandler
{
	public string skinTexture;

	public Color outlineColor;

	public Color cooldownColorStart;

	public Color cooldownColorEnd;

	public new static string ID => "RogueBooster";

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

	public RogueBooster()
		: this("")
	{
	}

	public RogueBooster(string skin)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		if (skin == "birb")
		{
			skinTexture = "BlunderBooster";
			outlineColor = new Color(210, 180, 100);
			cooldownColorStart = new Color(90, 67, 76);
			cooldownColorEnd = new Color(255, 83, 145);
		}
		else
		{
			skinTexture = "PlaguedFuelPack";
			outlineColor = new Color(130, 190, 64);
			cooldownColorStart = new Color(230, 64, 64);
			cooldownColorEnd = new Color(209, 248, 62);
		}
	}
}
