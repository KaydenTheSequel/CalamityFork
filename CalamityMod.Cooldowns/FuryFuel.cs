using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Cooldowns;

public class FuryFuel : CooldownHandler
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
			return Color.White;
		}
	}

	public new static string ID => "FuryFuel";

	public override bool CanTickDown => false;

	public override LocalizedText DisplayName => CalamityUtils.GetText("UI.Cooldowns." + ID);

	public override bool ShouldDisplay => instance.player.HeldItem.type == ModContent.ItemType<PristineFury>();

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
				return Color.Lerp(Color.Purple, Color.SlateGray, instance.Completion);
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
				return Color.Lerp(Color.Orchid, Color.SlateGray, instance.Completion);
			}
			return Color.DimGray;
		}
	}

	public override void DrawExpanded(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		base.DrawExpanded(spriteBatch, position, opacity, scale);
	}

	public override void DrawCompact(SpriteBatch spriteBatch, Vector2 position, float opacity, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		base.DrawCompact(spriteBatch, position, opacity, scale);
	}

	public FuryFuel()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		TextBorderColor = Color.Violet;
		base._002Ector();
	}
}
