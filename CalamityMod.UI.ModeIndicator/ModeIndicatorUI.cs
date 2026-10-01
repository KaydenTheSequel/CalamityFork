using System;
using System.Linq;
using System.Text.RegularExpressions;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.Packets;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CalamityMod.UI.ModeIndicator;

public class ModeIndicatorUI
{
	private static int GlowFadeAnimLength = 20;

	public static int GlowFadeTime = 0;

	internal const int LockAnimLength = 15;

	private static int lockClickTime = 0;

	private static bool previousLockStatus = false;

	internal const float MaxIconHoverScaleBoost = 0.3f;

	internal const float IconHoverScaleIncrement = 0.05f;

	internal const float IconHoverScaleDecrement = 0.030000001f;

	private static float iconHoverScaleBoost = 0f;

	private static bool previouslyHoveringMainIcon = false;

	private static bool menuOpen = false;

	private static int menuOpenTransitionTime = 0;

	private static DifficultyMode previouslyHoveredMode = null;

	private static CalamityUtils.CurveSegment lockGrow = new CalamityUtils.CurveSegment(CalamityUtils.SineOutEasing, 0f, 1f, 0.4f);

	private static CalamityUtils.CurveSegment lockShrink = new CalamityUtils.CurveSegment(CalamityUtils.SineInEasing, 0.6f, 1.4f, -0.4f);

	private static CalamityUtils.CurveSegment lockBump = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.9f, 1f, -0.2f);

	private static CalamityUtils.CurveSegment barExpand = new CalamityUtils.CurveSegment(CalamityUtils.SineInOutEasing, 0f, 0f, 1f);

	private static CalamityUtils.CurveSegment barWidthExpand = new CalamityUtils.CurveSegment(CalamityUtils.SineInOutEasing, 0f, 0f, 1.2f);

	private static CalamityUtils.CurveSegment barWidthRetract = new CalamityUtils.CurveSegment(CalamityUtils.SineInEasing, 0.6f, 1.2f, -0.2f);

	public static Rectangle MouseScreenArea
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			return Utils.CenteredRectangle(Main.MouseScreen, Vector2.One * 2f);
		}
	}

	public static Vector2 FrameSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(74f, 74f);
		}
	}

	public static Rectangle MainClickArea
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			return Utils.CenteredRectangle(DrawCenter, FrameSize * MainIconScale);
		}
	}

	public static bool ClickingMouse
	{
		get
		{
			if (Main.mouseLeft)
			{
				return Main.mouseLeftRelease;
			}
			return false;
		}
	}

	public static Vector2 DrawCenter
	{
		get
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2((float)Main.screenWidth - 400f - WidthForTier(DifficultyModeSystem.MostAlternateDifficulties) * 0.5f, 82f) + FrameSize * 0.5f;
		}
	}

	public static float MainIconScale => 1f + iconHoverScaleBoost;

	internal static int MenuAnimLength
	{
		get
		{
			if (DifficultyModeSystem.MostAlternateDifficulties <= 1)
			{
				return 20;
			}
			return 30;
		}
	}

	internal static float LockShakeScale => CalamityUtils.PiecewiseAnimation((float)lockClickTime / 15f, lockGrow, lockShrink, lockBump);

	internal static float BarExpansionProgress
	{
		get
		{
			float animLength = ((DifficultyModeSystem.MostAlternateDifficulties == 1) ? ((float)MenuAnimLength) : ((float)(MenuAnimLength * 2) / 3f));
			float progress = (float)menuOpenTransitionTime / animLength;
			if (menuOpen)
			{
				progress = 1f - progress;
			}
			return CalamityUtils.PiecewiseAnimation(progress, barExpand);
		}
	}

	internal static float BarWidthExpansionProgress
	{
		get
		{
			float progress = Math.Max((float)menuOpenTransitionTime - (float)(MenuAnimLength * 2) / 3f, 0f) / ((float)MenuAnimLength / 3f);
			if (menuOpen)
			{
				progress = 1f - progress;
			}
			return CalamityUtils.PiecewiseAnimation(progress, barWidthExpand, barWidthRetract);
		}
	}

	public static float WidthForTier(int alts)
	{
		return (float)(alts - 1) * 76f;
	}

	private static void ClearVariables()
	{
		GlowFadeTime = 0;
		lockClickTime = 0;
		menuOpenTransitionTime = 0;
		previousLockStatus = false;
		iconHoverScaleBoost = 0f;
		menuOpen = false;
		previouslyHoveringMainIcon = false;
		previouslyHoveredMode = null;
	}

	public static void TickVariables()
	{
		if (lockClickTime > 0)
		{
			lockClickTime--;
		}
		if (menuOpenTransitionTime > 0)
		{
			menuOpenTransitionTime--;
		}
		if (iconHoverScaleBoost > 0f)
		{
			iconHoverScaleBoost -= 0.030000001f;
		}
		if (!menuOpen || menuOpenTransitionTime > 0)
		{
			previouslyHoveredMode = null;
		}
		if (GlowFadeTime > 0)
		{
			GlowFadeTime--;
		}
	}

	public static void Draw(SpriteBatch spriteBatch)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.playerInventory)
		{
			ClearVariables();
			return;
		}
		Texture2D indicatorTexture = DifficultyModeSystem.GetCurrentDifficulty.Texture.Value;
		GetDifficultyStatus(out var difficultyText);
		GetLockStatus(out var lockText, out var locked);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(MainClickArea))
		{
			if (!DifficultyModeSystem._hasCheckedItOutYet)
			{
				GlowFadeTime = GlowFadeAnimLength;
				DifficultyModeSystem._hasCheckedItOutYet = true;
			}
			if (!previouslyHoveringMainIcon)
			{
				previouslyHoveringMainIcon = true;
				SoundEngine.PlaySound(in SoundID.MenuTick);
			}
			if (iconHoverScaleBoost < 0.3f)
			{
				iconHoverScaleBoost = Math.Min(iconHoverScaleBoost + 0.05f + 0.030000001f, 0.3f);
				if (ClickingMouse && menuOpenTransitionTime == 0 && !locked)
				{
					SoundEngine.PlaySound(menuOpen ? SoundID.MenuClose : SoundID.MenuOpen);
					menuOpenTransitionTime = MenuAnimLength;
					menuOpen = !menuOpen;
				}
			}
		}
		else
		{
			previouslyHoveringMainIcon = false;
		}
		if (!DifficultyModeSystem._hasCheckedItOutYet || GlowFadeTime > 0)
		{
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
			Texture2D bloomTex = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/BloomFlare", (AssetRequestMode)2).Value;
			float opacity = ((!DifficultyModeSystem._hasCheckedItOutYet) ? 1f : (1f * (float)GlowFadeTime / (float)GlowFadeAnimLength));
			float scale = 0.4f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.05f;
			float rot = Main.GlobalTimeWrappedHourly * 0.5f;
			spriteBatch.Draw(bloomTex, DrawCenter, (Rectangle?)null, Color.Crimson * opacity, rot, new Vector2(123f, 124f), scale, (SpriteEffects)0, 0f);
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		}
		string extraDescText = string.Empty;
		if (menuOpenTransitionTime > 0 || menuOpen)
		{
			ManageHexIcons(spriteBatch, out extraDescText);
		}
		spriteBatch.Draw(indicatorTexture, DrawCenter, (Rectangle?)null, Color.White, 0f, indicatorTexture.Size() * 0.5f, MainIconScale, (SpriteEffects)0, 0f);
		if (locked)
		{
			DrawLock(spriteBatch);
		}
		if (difficultyText != LocalizedText.Empty || extraDescText != string.Empty)
		{
			string textToDisplay = difficultyText.ToString();
			if (difficultyText != LocalizedText.Empty)
			{
				if (lockText != LocalizedText.Empty)
				{
					textToDisplay = textToDisplay + "\n" + lockText.ToString();
				}
			}
			else
			{
				textToDisplay = extraDescText;
			}
			FontAssets.MouseText.Value.MeasureString(textToDisplay);
			int numLines = 1 + Regex.Matches(textToDisplay, "\n").Count;
			string heightCalculator = string.Concat(Enumerable.Repeat("mis nuevos los gatos \n", numLines));
			Vector2 regexedBoxSize = default(Vector2);
			((Vector2)(ref regexedBoxSize))._002Ector(ChatManager.GetStringSize(FontAssets.MouseText.Value, textToDisplay, Vector2.One).X, ChatManager.GetStringSize(FontAssets.MouseText.Value, heightCalculator, Vector2.One).Y);
			Vector2 textboxStart = new Vector2((float)Main.mouseX, (float)Main.mouseY) + Vector2.One * 14f;
			if (Main.ThickMouse)
			{
				textboxStart += Vector2.One * 6f;
			}
			if (!Main.mouseItem.IsAir)
			{
				textboxStart.X += 34f;
			}
			if (textboxStart.X + regexedBoxSize.X + 4f > (float)Main.screenWidth)
			{
				textboxStart.X = (float)Main.screenWidth - regexedBoxSize.X - 4f;
			}
			if (textboxStart.Y + regexedBoxSize.Y + 4f > (float)Main.screenHeight)
			{
				textboxStart.Y = (float)Main.screenHeight - regexedBoxSize.Y - 4f;
			}
			Utils.DrawInvBG(spriteBatch, new Rectangle((int)textboxStart.X - 10, (int)textboxStart.Y - 10, (int)regexedBoxSize.X + 20, (int)regexedBoxSize.Y + 16), new Color(50, 20, 35) * 0.925f);
			Main.LocalPlayer.mouseInterface = true;
			Main.instance.MouseText(textToDisplay, 0, 0);
		}
		TickVariables();
	}

	public static void GetDifficultyStatus(out LocalizedText text)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		text = LocalizedText.Empty;
		Rectangle mouseScreenArea = MouseScreenArea;
		if (!((Rectangle)(ref mouseScreenArea)).Intersects(MainClickArea))
		{
			return;
		}
		string modeToDisplay = ((Main.getGoodWorld && DifficultyModeSystem.Difficulties[0].FTWName != null) ? DifficultyModeSystem.Difficulties[0].FTWName.ToString() : DifficultyModeSystem.Difficulties[1].Name.ToString());
		bool anyActiveMode = Main.getGoodWorld;
		for (int i = 1; i < DifficultyModeSystem.Difficulties.Count; i++)
		{
			if (DifficultyModeSystem.GetCurrentDifficulty == DifficultyModeSystem.Difficulties[i])
			{
				modeToDisplay = ((Main.getGoodWorld && DifficultyModeSystem.Difficulties[i].FTWName != null) ? DifficultyModeSystem.Difficulties[i].FTWName.ToString() : DifficultyModeSystem.Difficulties[i].Name.ToString());
				anyActiveMode = true;
			}
		}
		string modeStr = CalamityUtils.GetText("UI.ModeAppend").Format(modeToDisplay);
		string activeText = CalamityUtils.GetTextValue("UI." + (anyActiveMode ? "Active" : "NotActive"));
		text = CalamityUtils.GetText("UI.DifficultyStatusText").WithFormatArgs(modeStr, activeText.ToLower());
	}

	public static void GetLockStatus(out LocalizedText text, out bool locked)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		locked = false;
		text = CalamityUtils.GetText("UI.DifficultyClickText");
		if (CalamityPlayer.areThereAnyDamnBosses || BossRushEvent.BossRushActive)
		{
			locked = true;
			text = CalamityUtils.GetText("UI.ChangingTheRules");
		}
		if (locked != previousLockStatus && lockClickTime == 0)
		{
			lockClickTime = 15;
		}
		previousLockStatus = locked;
		if (locked && menuOpen)
		{
			menuOpen = false;
			menuOpenTransitionTime = MenuAnimLength - menuOpenTransitionTime;
		}
		if (locked && ClickingMouse && lockClickTime == 0)
		{
			Rectangle mouseScreenArea = MouseScreenArea;
			if (((Rectangle)(ref mouseScreenArea)).Intersects(MainClickArea))
			{
				lockClickTime = 15;
				SoundEngine.PlaySound(in SoundID.MenuTick);
			}
		}
	}

	public static void ManageHexIcons(SpriteBatch spriteBatch, out string text)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		int tiers = DifficultyModeSystem.DifficultyTiers.Count();
		float barLength = (float)(90 * tiers) * BarExpansionProgress;
		float progress = (menuOpen ? (1f - (float)menuOpenTransitionTime / (float)MenuAnimLength) : ((float)menuOpenTransitionTime / (float)MenuAnimLength));
		Vector2 basePosition = DrawCenter + barLength / ((float)tiers + 1f) * Vector2.UnitY;
		text = string.Empty;
		bool modeHovered = false;
		Vector2 positionOffset = barLength / ((float)tiers + 1f) * Vector2.UnitY;
		float progressMult = 0.8f * progress;
		Color progressColor = Color.White * progress;
		for (int i = 0; i < tiers; i++)
		{
			int modesAtTier = DifficultyModeSystem.DifficultyTiers[i].Length;
			float width = WidthForTier(modesAtTier) * 0.5f;
			for (int j = 0; j < modesAtTier; j++)
			{
				DifficultyMode mode = DifficultyModeSystem.DifficultyTiers[i][j];
				Texture2D hexIcon = (mode.Enabled ? mode.Texture.Value : mode.TextureDisabled.Value);
				Vector2 hexIconSize = hexIcon.Size();
				Vector2 iconPosition = basePosition + positionOffset * (float)i;
				if (modesAtTier > 1)
				{
					iconPosition += Vector2.UnitX * MathHelper.Lerp(width * -1f, width, (float)j / (float)(modesAtTier - 1)) * BarWidthExpansionProgress;
				}
				Rectangle mouseScreenArea = MouseScreenArea;
				bool hovered = ((Rectangle)(ref mouseScreenArea)).Intersects(Utils.CenteredRectangle(iconPosition, hexIconSize));
				float usedOpacity = 0.85f;
				if (hovered)
				{
					usedOpacity = MathHelper.Lerp(usedOpacity, 1f, 0.7f);
				}
				if (mode == DifficultyModeSystem.GetCurrentDifficulty)
				{
					usedOpacity = 1f;
					Texture2D outlineTexture = mode.OutlineTexture.Value;
					spriteBatch.Draw(outlineTexture, iconPosition, (Rectangle?)null, mode.ChatTextColor * progressMult, 0f, outlineTexture.Size() * 0.5f, 1f, (SpriteEffects)0, 0f);
				}
				spriteBatch.Draw(hexIcon, iconPosition, (Rectangle?)null, progressColor * usedOpacity, 0f, hexIconSize * 0.5f, 1f, (SpriteEffects)0, 0f);
				if ((menuOpenTransitionTime == 0) & hovered)
				{
					if (previouslyHoveredMode != mode)
					{
						SoundEngine.PlaySound(in SoundID.MenuTick);
					}
					previouslyHoveredMode = mode;
					modeHovered = true;
					text = GetDifficultyText(mode);
					if (ClickingMouse)
					{
						SwitchToDifficulty(mode, broadcast: true);
					}
				}
			}
		}
		if (!modeHovered)
		{
			previouslyHoveredMode = null;
		}
	}

	public static void DrawLock(SpriteBatch spriteBatch)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D lockTexture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicatorLock", (AssetRequestMode)2).Value;
		float rotationShift = ((lockClickTime == 0) ? 0f : ((float)Math.Sin((1f - (float)lockClickTime / 15f) * ((float)Math.PI * 2f) * 2f) * 0.5f * ((float)lockClickTime / 15f)));
		spriteBatch.Draw(lockTexture, DrawCenter + Vector2.UnitY * 24f * MainIconScale, (Rectangle?)null, Color.White, 0f + rotationShift, lockTexture.Size() * 0.5f, LockShakeScale * MainIconScale, (SpriteEffects)0, 0f);
	}

	public static string GetDifficultyText(DifficultyMode mode)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		bool useFTWName = mode.FTWName != null && Main.getGoodWorld;
		LocalizedText preface = (useFTWName ? mode.FTWName : mode.Name);
		if (mode == DifficultyModeSystem.GetCurrentDifficulty)
		{
			preface = CalamityUtils.GetText("UI.CurrentlySelected").WithFormatArgs(useFTWName ? mode.FTWName.ToString() : mode.Name.ToString());
		}
		string text = "\n" + mode.ShortDescription.ToString();
		if (mode.ExpandedDescription != LocalizedText.Empty)
		{
			text = ((!Main.keyState.PressingShift()) ? (text + "\n" + CalamityUtils.GetTextValue("UI.DifficultyShiftText")) : (text + "\n" + mode.ExpandedDescription.ToString()));
		}
		return preface.ToString() + text;
	}

	public static void SwitchToDifficulty(DifficultyMode mode, bool broadcast)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		if (mode == DifficultyModeSystem.GetCurrentDifficulty)
		{
			return;
		}
		CalamityUtils.BroadcastFormattedText("Mods.CalamityMod.UI.DifficultySwitch", Color.White, (Main.getGoodWorld && DifficultyModeSystem.GetCurrentDifficulty.FTWTextColor.HasValue) ? DifficultyModeSystem.GetCurrentDifficulty.FTWTextColor.Value.Hex3() : DifficultyModeSystem.GetCurrentDifficulty.ChatTextColor.Hex3(), (Main.getGoodWorld && DifficultyModeSystem.GetCurrentDifficulty.FTWName != null) ? DifficultyModeSystem.GetCurrentDifficulty.FTWName : DifficultyModeSystem.GetCurrentDifficulty.Name, (Main.getGoodWorld && DifficultyModeSystem.GetCurrentDifficulty.FTWTextColor.HasValue) ? mode.FTWTextColor.Value.Hex3() : mode.ChatTextColor.Hex3(), (Main.getGoodWorld && DifficultyModeSystem.GetCurrentDifficulty.FTWName != null) ? mode.FTWName : mode.Name);
		DifficultyModeSystem._newGameModeID = mode.BackBoneGameModeID;
		for (int i = 0; i < DifficultyModeSystem.Difficulties.Count; i++)
		{
			if (DifficultyModeSystem.Difficulties[i]._difficultyTier >= mode._difficultyTier && DifficultyModeSystem.Difficulties[i] != mode && DifficultyModeSystem.Difficulties[i].Enabled)
			{
				DifficultyModeSystem.Difficulties[i].Enabled = false;
			}
		}
		for (int j = 0; j < mode._difficultyTier; j++)
		{
			if (DifficultyModeSystem.DifficultyTiers[j].Length == 1)
			{
				DifficultyModeSystem.DifficultyTiers[j][0].Enabled = true;
				continue;
			}
			for (int k = 0; k < DifficultyModeSystem.DifficultyTiers[j].Length; k++)
			{
				DifficultyModeSystem.DifficultyTiers[j][k].Enabled = false;
			}
			for (int l = 0; l < mode.FavoredDifficultyAtTier(j).Length; l++)
			{
				DifficultyModeSystem.DifficultyTiers[j][mode.FavoredDifficultyAtTier(j)[l]].Enabled = true;
			}
		}
		mode.Enabled = true;
		SoundEngine.PlaySound(mode.ActivationSound);
		if ((Main.netMode != 0) & broadcast)
		{
			SwitchToDifficultyPacket.Send(mode);
		}
		if (menuOpen)
		{
			menuOpen = false;
			menuOpenTransitionTime = MenuAnimLength;
		}
	}
}
