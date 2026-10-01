using System;
using CalamityMod.Balancing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class LifeSteal : CooldownHandler
{
	public new static string ID => "LifeSteal";

	private float lifeStealCap
	{
		get
		{
			if (!Main.expertMode)
			{
				return BalancingConstants.LifeStealCap_Classic;
			}
			return BalancingConstants.LifeStealCap_Expert;
		}
	}

	public override bool ShouldDisplay => instance.player.lifeSteal < lifeStealCap;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override string Texture => "CalamityMod/Cooldowns/LifeSteal";

	public override bool CanTickDown => false;

	public override Color OutlineColor
	{
		get
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			if (!(instance.player.lifeSteal < 0f))
			{
				return new Color(255, 142, 165);
			}
			return new Color(255, 142, 165);
		}
	}

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			if (!(instance.player.lifeSteal < 0f))
			{
				return new Color(255, 181, 181);
			}
			return new Color(145, 59, 59);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return CooldownStartColor;
		}
	}

	private Color TextColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	private Color TextBorderColor
	{
		get
		{
			//IL_0004: Unknown result type (might be due to invalid IL or missing references)
			return new Color(40, 0, 0);
		}
	}

	public override void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		base.DrawExpanded(spriteBatch, position, opacity, scale);
		float value = instance.player.lifeSteal;
		bool num = value < 0f;
		float valueToMeasure = Math.Abs(value);
		float Xoffset = ((!(valueToMeasure > 9f)) ? (-5f) : ((valueToMeasure > 99f) ? (-12.5f) : (-10f)));
		if (num)
		{
			Xoffset -= 8f;
		}
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, value.ToString("#"), position + new Vector2(Xoffset, 8f) * scale, TextColor, TextBorderColor, scale);
	}

	public override void DrawCompact(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		base.DrawCompact(spriteBatch, position, opacity, scale);
		float value = instance.player.lifeSteal;
		bool num = value < 0f;
		float valueToMeasure = Math.Abs(value);
		float Xoffset = ((!(valueToMeasure > 9f)) ? (-5f) : ((valueToMeasure > 99f) ? (-12.5f) : (-10f)));
		if (num)
		{
			Xoffset -= 8f;
		}
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, value.ToString("#"), position + new Vector2(Xoffset, 8f) * scale, TextColor, TextBorderColor, scale);
	}
}
