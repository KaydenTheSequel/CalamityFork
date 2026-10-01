using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class ScarfCooldown : CooldownHandler
{
	public string skinTexture;

	public Color outlineColor;

	public Color cooldownColorStart;

	public Color cooldownColorEnd;

	public new static string ID => "ScarfCooldown";

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
			return cooldownColorStart;
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return cooldownColorEnd;
		}
	}

	public ScarfCooldown()
		: this("")
	{
	}

	public ScarfCooldown(string skin)
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		if (skin == "evasionscarf")
		{
			skinTexture = "EvasionScarf";
			outlineColor = Color.Lerp(new Color(255, 194, 150), new Color(255, 160, 150), (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.5f + 0.5f);
			cooldownColorStart = new Color(132, 23, 32);
			cooldownColorEnd = new Color(164, 52, 45);
		}
		else
		{
			skinTexture = "CounterScarf";
			outlineColor = Color.Lerp(new Color(255, 115, 178), new Color(255, 76, 76), (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.5f + 0.5f);
			cooldownColorStart = new Color(194, 75, 97);
			cooldownColorEnd = new Color(255, 76, 76);
		}
	}
}
