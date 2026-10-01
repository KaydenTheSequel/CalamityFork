using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;

namespace CalamityMod.Cooldowns;

public class WarbanneroftheRighteousBuff : CooldownHandler
{
	public float CompletionPercentage => Utils.GetLerpValue(70f, 0f, instance.timeLeft);

	private bool IsEmpty => CompletionPercentage == 0f;

	private float TextXOffset
	{
		get
		{
			if (instance.timeLeft > 20)
			{
				return -18f;
			}
			return -11f;
		}
	}

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
			return Color.White;
		}
	}

	private Color TextBorderColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Black;
		}
	}

	public new static string ID => "WarbanneroftheRighteousBuff";

	public override bool CanTickDown => false;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override bool ShouldDisplay => instance.player.Calamity().WarbanneroftheRighteous;

	public override string Texture => "CalamityMod/Cooldowns/" + ID;

	public override Color CooldownStartColor
	{
		get
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			if (!IsEmpty)
			{
				return Color.Lerp(Color.SlateGray, Color.DarkGoldenrod, CompletionPercentage);
			}
			return Color.DimGray;
		}
	}

	public override Color CooldownEndColor
	{
		get
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			if (!IsEmpty)
			{
				return Color.Lerp(Color.SlateGray, Color.Gold, CompletionPercentage);
			}
			return Color.DimGray;
		}
	}

	public override void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		base.DrawExpanded(spriteBatch, position, opacity, scale);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, -instance.timeLeft + 15 + "%", position + TextPosition, TextColor, TextBorderColor);
	}

	public override void DrawCompact(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		base.DrawCompact(spriteBatch, position, opacity, scale);
		CalamityUtils.DrawBorderStringEightWay(spriteBatch, FontAssets.MouseText.Value, -instance.timeLeft + 15 + "%", position + TextPosition, TextColor, TextBorderColor);
	}
}
