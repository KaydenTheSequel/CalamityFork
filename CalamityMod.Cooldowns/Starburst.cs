using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class Starburst : CooldownHandler
{
	private Color TextBorderColor;

	public float CompletionPercentage => MathF.Round(instance.Completion * 100f);

	private bool IsEmpty => CompletionPercentage == 100f;

	private float TextXOffset => -5.4f;

	private Vector2 TextPosition
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(TextXOffset, 15f);
		}
	}

	private Color TextColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(Color.LightSkyBlue, Color.White, (MathF.Sin(Main.GlobalTimeWrappedHourly) + 1f) * 0.5f);
		}
	}

	public new static string ID => "Starburst";

	public override bool CanTickDown => false;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override bool ShouldDisplay => instance.player.Calamity().StratusStarburstResetTimer > 0;

	public override string Texture => "CalamityMod/Cooldowns/" + ID;

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			if (!IsEmpty)
			{
				return Color.Lerp(Color.SkyBlue, Color.DeepSkyBlue, instance.Completion);
			}
			return Color.DimGray;
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			if (!IsEmpty)
			{
				return Color.Lerp(Color.SkyBlue, Color.DeepSkyBlue, instance.Completion);
			}
			return Color.DimGray;
		}
	}

	public override void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		base.DrawExpanded(spriteBatch, position, opacity, scale);
		int value = CalamityPlayer.MaxStratusStarburst - instance.timeLeft;
		float Xoffset = ((value <= 9) ? (-5f) : ((value > 99) ? (-12.5f) : (-10f)));
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, value.ToString(), position + new Vector2(Xoffset, 4f) * scale, TextColor, TextBorderColor, scale);
	}

	public override void DrawCompact(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		base.DrawCompact(spriteBatch, position, opacity, scale);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, MathHelper.Min((float)instance.player.Calamity().StratusStarburst, (float)CalamityPlayer.MaxStratusStarburst).ToString(), position + TextPosition, TextColor, TextBorderColor);
	}

	public Starburst()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		TextBorderColor = Color.DarkSlateBlue;
		base._002Ector();
	}
}
