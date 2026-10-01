using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class DaawnlightSpiritOriginExtraCrit : CooldownHandler
{
	private Color TextBorderColor;

	public new static string ID => "DSOCritCooldown";

	public override bool ShouldDisplay => instance.player.GetModPlayer<CalamityPlayer>().spiritOrigin;

	private float ExtraCritChance => Math.Min(instance.player.GetModPlayer<CalamityPlayer>().spiritOriginCritBoost, DaawnlightSpiritOrigin.CritHardCap);

	private float TextXOffset => (float)((ExtraCritChance > 99f) ? (-24) : ((ExtraCritChance > 9f) ? (-20) : (-16))) * TextScale;

	private Vector2 TextPosition
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(TextXOffset, 25f);
		}
	}

	private Color TextColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(Color.White, Color.Tomato, Utils.GetLerpValue(DaawnlightSpiritOrigin.CritDecayThreshold - 10, DaawnlightSpiritOrigin.CritDecayThreshold, instance.player.GetModPlayer<CalamityPlayer>().spiritOriginCritBoost));
		}
	}

	private float TextScale => Utils.Remap(ExtraCritChance, 0f, DaawnlightSpiritOrigin.CritDecayThreshold, 1f, 1.5f);

	public override bool CanTickDown => false;

	public override Color CooldownStartColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(Main.DiscoB, Main.DiscoR, Main.DiscoG);
		}
	}

	public override string Texture => "CalamityMod/Cooldowns/" + ID;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		base.DrawExpanded(spriteBatch, position, opacity, scale);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, $"+{ExtraCritChance}%", position + TextPosition, TextColor, TextBorderColor, TextScale);
	}

	public override void DrawCompact(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		base.DrawCompact(spriteBatch, position, opacity, scale);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, $"+{ExtraCritChance}%", position + TextPosition, TextColor, TextBorderColor, TextScale);
	}

	public DaawnlightSpiritOriginExtraCrit()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		TextBorderColor = Color.Black;
		base._002Ector();
	}
}
