using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class ParryCooldown : CooldownHandler
{
	public string skinTexture;

	public Color outlineColor;

	public Color cooldownColorStart;

	public Color cooldownColorEnd;

	public new static string ID => "ParryCooldown";

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

	public ParryCooldown()
		: this("")
	{
	}

	public ParryCooldown(string skin)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		switch (skin)
		{
		case "blazingcore":
			skinTexture = "BlazingCoreParry";
			outlineColor = new Color(255, 191, 73);
			cooldownColorStart = new Color(181, 136, 177);
			cooldownColorEnd = new Color(255, 194, 161);
			break;
		case "flamelickedshell":
			skinTexture = "FlameShellParry";
			outlineColor = new Color(211, 124, 93);
			cooldownColorStart = new Color(107, 6, 6);
			cooldownColorEnd = new Color(228, 78, 78);
			break;
		case "shieldoftheocean":
			skinTexture = "OceanShieldParry";
			outlineColor = Color.White;
			cooldownColorStart = new Color(233, 111, 165);
			cooldownColorEnd = new Color(105, 139, 148);
			break;
		default:
			skinTexture = "ParryCooldown";
			outlineColor = Color.White;
			cooldownColorStart = Color.CornflowerBlue;
			cooldownColorEnd = Color.White;
			break;
		}
	}
}
