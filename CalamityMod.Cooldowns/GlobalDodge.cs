using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class GlobalDodge : CooldownHandler
{
	public string skinTexture;

	public Color outlineColor;

	public Color cooldownColorStart;

	public Color cooldownColorEnd;

	public new static string ID => "GlobalDodge";

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

	public GlobalDodge()
		: this("")
	{
	}

	public GlobalDodge(string skin)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		if (!(skin == "abyssmirror"))
		{
			if (skin == "eclipsemirror")
			{
				skinTexture = "EclipseEvade";
				outlineColor = new Color(152, 206, 248);
				cooldownColorStart = new Color(255, 192, 71);
				cooldownColorEnd = new Color(255, 255, 151);
			}
			else
			{
				skinTexture = "GlobalDodge";
				outlineColor = Color.White;
				cooldownColorStart = Color.CornflowerBlue;
				cooldownColorEnd = Color.White;
			}
		}
		else
		{
			skinTexture = "AbyssEvade";
			outlineColor = new Color(125, 157, 149);
			cooldownColorStart = new Color(167, 147, 151);
			cooldownColorEnd = new Color(217, 209, 195);
		}
	}
}
