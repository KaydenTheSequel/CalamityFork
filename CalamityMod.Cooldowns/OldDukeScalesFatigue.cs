using System;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class OldDukeScalesFatigue : CooldownHandler
{
	private Color TextBorderColor;

	public float CompletionPercentage => MathF.Round(100f - instance.Completion * 100f);

	private bool IsPlayerTired => instance.player.GetModPlayer<OldDukeScalesPlayer>().IsTired;

	private float TextXOffset => (CompletionPercentage > 99f) ? (-18) : ((CompletionPercentage > 9f) ? (-15) : (-12));

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
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			if (!IsPlayerTired)
			{
				return Color.White;
			}
			return Color.Red;
		}
	}

	public new static string ID => "OldDukeScalesFatigue";

	public override bool CanTickDown => false;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override bool ShouldDisplay
	{
		get
		{
			if (!instance.player.GetModPlayer<OldDukeScalesPlayer>().OldDukeScalesOn)
			{
				return IsPlayerTired;
			}
			return true;
		}
	}

	public override string Texture => "CalamityMod/Cooldowns/" + ID;

	public override Color CooldownStartColor
	{
		get
		{
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			if (!IsPlayerTired)
			{
				return Color.Lerp(Color.Green, Color.Red, instance.Completion);
			}
			return Color.Red;
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
			if (!IsPlayerTired)
			{
				return Color.Lerp(Color.Green, Color.Red, instance.Completion);
			}
			return Color.Red;
		}
	}

	public override void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		base.DrawExpanded(spriteBatch, position, opacity, scale);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, CompletionPercentage + "%", position + TextPosition, TextColor, TextBorderColor);
	}

	public override void DrawCompact(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		base.DrawCompact(spriteBatch, position, opacity, scale);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, CompletionPercentage + "%", position + TextPosition, TextColor, TextBorderColor);
	}

	public OldDukeScalesFatigue()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		TextBorderColor = Color.Black;
		base._002Ector();
	}
}
