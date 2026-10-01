using System;
using System.Collections.Generic;
using CalamityMod.Cooldowns;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.UI;

public class CooldownRackUI
{
	public static int MaxLargeIcons = 10;

	public const float CompactXSpacing = 28f;

	public const float ExpandedXScaling = 46f;

	public static bool DebugFullDisplay = false;

	public static float DebugForceCompletion = 0f;

	public static bool CompactIcons
	{
		get
		{
			if (CalamityClientConfig.Instance.CooldownDisplay == CooldownDisplayOptions.Compact)
			{
				return true;
			}
			return Main.LocalPlayer.GetDisplayedCooldowns().Count > MaxLargeIcons;
		}
	}

	public static Vector2 Spacing
	{
		get
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			if (!CompactIcons)
			{
				return Vector2.UnitX * 46f;
			}
			return Vector2.UnitX * 28f;
		}
	}

	public static Vector2 BaseDrawPosition
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(32f, 100f) + Spacing / 2f + Vector2.UnitY * 50f * MathF.Ceiling((float)Main.LocalPlayer.CountBuffs() / 11f);
		}
	}

	public static void Draw(SpriteBatch spriteBatch)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		if (Main.gameMenu || Main.playerInventory || CalamityClientConfig.Instance.CooldownDisplay == CooldownDisplayOptions.Hidden)
		{
			return;
		}
		IList<CooldownInstance> cooldownsToDraw = Main.LocalPlayer.GetDisplayedCooldowns();
		if (cooldownsToDraw.Count == 0)
		{
			return;
		}
		float uiScale = 1f;
		Vector2 displayPosition = BaseDrawPosition;
		int rectangleSide = (int)Math.Floor(CompactIcons ? (24f * uiScale) : (52f * uiScale));
		Rectangle iconRectangle = default(Rectangle);
		((Rectangle)(ref iconRectangle))._002Ector((int)displayPosition.X - rectangleSide / 2, (int)displayPosition.Y - rectangleSide / 2, rectangleSide, rectangleSide);
		Rectangle mouse = default(Rectangle);
		((Rectangle)(ref mouse))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 8, 8);
		string mouseHover = "";
		float iconOpacityScale = (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.1f + 0.6f;
		Vector2 mouseCenter = ((Rectangle)(ref mouse)).Center.ToVector2();
		float opacity = MathHelper.Clamp((float)Math.Sin(Main.GlobalTimeWrappedHourly % (float)Math.PI) * 2f, 0f, 1f) * 0.1f + 0.9f;
		foreach (CooldownInstance item in cooldownsToDraw)
		{
			CooldownHandler handler = item.handler;
			float iconOpacity = iconOpacityScale;
			iconOpacity += 0.3f * (1f - MathHelper.Clamp(Vector2.Distance(mouseCenter, ((Rectangle)(ref iconRectangle)).Center.ToVector2()), 0f, 80f) / 80f);
			if (((Rectangle)(ref iconRectangle)).Intersects(mouse))
			{
				mouseHover = handler.DisplayName.ToString();
				iconOpacity = opacity;
			}
			if (DebugFullDisplay)
			{
				iconOpacity = 1f;
			}
			if (CompactIcons)
			{
				handler.DrawCompact(spriteBatch, displayPosition, iconOpacity, uiScale);
			}
			else
			{
				handler.DrawExpanded(spriteBatch, displayPosition, iconOpacity, uiScale);
			}
			displayPosition += Spacing;
			iconRectangle.X += (int)Spacing.X;
		}
		if (mouseHover != "")
		{
			Main.LocalPlayer.mouseInterface = true;
			Main.instance.MouseText(mouseHover, 0, 0);
		}
	}
}
