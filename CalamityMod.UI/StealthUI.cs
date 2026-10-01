using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI;

[Autoload(true, Side = ModSide.Client)]
public sealed class StealthUI : ModSystem
{
	internal const float DefaultStealthPosX = 50.104603f;

	internal const float DefaultStealthPosY = 55.765408f;

	private const float MouseDragEpsilon = 0.05f;

	private static Vector2? dragOffset;

	private static Texture2D edgeTexture;

	private static Texture2D indicatorTexture;

	private static Texture2D barTexture;

	private static Texture2D fullBarTexture;

	public override void OnModLoad()
	{
		edgeTexture = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/StealthMeter", (AssetRequestMode)1).Value;
		indicatorTexture = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/StealthMeterStrikeIndicator", (AssetRequestMode)1).Value;
		barTexture = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/StealthMeterBar", (AssetRequestMode)1).Value;
		fullBarTexture = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/StealthMeterBarFull", (AssetRequestMode)1).Value;
		Reset();
	}

	public override void Unload()
	{
		Reset();
		edgeTexture = (indicatorTexture = (barTexture = (fullBarTexture = null)));
	}

	private static void Reset()
	{
		dragOffset = null;
	}

	public static void Draw(SpriteBatch spriteBatch, Player player)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Invalid comparison between Unknown and I4
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 screenRatioPosition = default(Vector2);
		((Vector2)(ref screenRatioPosition))._002Ector(CalamityClientConfig.Instance.StealthMeterPosX, CalamityClientConfig.Instance.StealthMeterPosY);
		if (screenRatioPosition.X < 0f || screenRatioPosition.X > 100f)
		{
			screenRatioPosition.X = 50.104603f;
		}
		if (screenRatioPosition.Y < 0f || screenRatioPosition.Y > 100f)
		{
			screenRatioPosition.Y = 55.765408f;
		}
		float uiScale = Main.UIScale;
		Vector2 screenPos = screenRatioPosition;
		screenPos.X = (int)(screenPos.X * 0.01f * (float)Main.screenWidth);
		screenPos.Y = (int)(screenPos.Y * 0.01f * (float)Main.screenHeight);
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.stealthUIAlpha > 0f && CalamityClientConfig.Instance.StealthMeter && modPlayer.rogueStealthMax > 0f && modPlayer.wearingRogueArmor)
		{
			DrawStealthBar(spriteBatch, modPlayer, screenPos);
		}
		else
		{
			bool changed = false;
			if (CalamityClientConfig.Instance.StealthMeterPosX != screenRatioPosition.X)
			{
				CalamityClientConfig.Instance.StealthMeterPosX = screenRatioPosition.X;
				changed = true;
			}
			if (CalamityClientConfig.Instance.StealthMeterPosY != screenRatioPosition.Y)
			{
				CalamityClientConfig.Instance.StealthMeterPosY = screenRatioPosition.Y;
				changed = true;
			}
			if (changed)
			{
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
		Rectangle mouseHitbox = default(Rectangle);
		((Rectangle)(ref mouseHitbox))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 8, 8);
		Rectangle stealthBar = Utils.CenteredRectangle(screenPos, edgeTexture.Size() * uiScale);
		MouseState ms = Mouse.GetState();
		Vector2 mousePos = Main.MouseScreen;
		if (!((Rectangle)(ref stealthBar)).Intersects(mouseHitbox))
		{
			return;
		}
		if (!CalamityClientConfig.Instance.MeterPosLock)
		{
			Main.LocalPlayer.mouseInterface = true;
		}
		if (modPlayer.rogueStealthMax > 0f && modPlayer.stealthUIAlpha >= 0.5f)
		{
			string stealthStr = (100f * modPlayer.rogueStealth).ToString("n2");
			string maxStealthStr = (100f * modPlayer.rogueStealthMax).ToString("n2");
			string textToDisplay = $"{CalamityUtils.GetTextValue("UI.Stealth")}: {stealthStr}/{maxStealthStr}\n";
			textToDisplay = (Main.keyState.PressingShift() ? (textToDisplay + CalamityUtils.GetTextValue("UI.StealthInfoText")) : (textToDisplay + CalamityUtils.GetTextValue("UI.StealthShiftText")));
			Main.instance.MouseText(textToDisplay, 0, 0);
			modPlayer.stealthUIAlpha = MathHelper.Lerp(modPlayer.stealthUIAlpha, 0.25f, 0.035f);
		}
		Vector2 newScreenRatioPosition = screenRatioPosition;
		if (!CalamityClientConfig.Instance.MeterPosLock && (int)((MouseState)(ref ms)).LeftButton == 1)
		{
			if (!dragOffset.HasValue)
			{
				dragOffset = mousePos - screenPos;
			}
			Vector2 newCorner = mousePos - dragOffset.GetValueOrDefault(Vector2.Zero);
			newScreenRatioPosition.X = 100f * newCorner.X / (float)Main.screenWidth;
			newScreenRatioPosition.Y = 100f * newCorner.Y / (float)Main.screenHeight;
		}
		Vector2 delta = newScreenRatioPosition - screenRatioPosition;
		if (Math.Abs(delta.X) >= 0.05f || Math.Abs(delta.Y) >= 0.05f)
		{
			CalamityClientConfig.Instance.StealthMeterPosX = newScreenRatioPosition.X;
			CalamityClientConfig.Instance.StealthMeterPosY = newScreenRatioPosition.Y;
		}
		if (dragOffset.HasValue && (int)((MouseState)(ref ms)).LeftButton == 0)
		{
			dragOffset = null;
			CalamityClientConfig.Instance.SaveChanges();
		}
	}

	private static void DrawStealthBar(SpriteBatch spriteBatch, CalamityPlayer modPlayer, Vector2 screenPos)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		float uiScale = Main.UIScale;
		float offset = (float)(edgeTexture.Width - barTexture.Width) * 0.5f;
		spriteBatch.Draw(edgeTexture, screenPos, (Rectangle?)null, Color.White * modPlayer.stealthUIAlpha, 0f, edgeTexture.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		if (modPlayer.StealthStrikeAvailable())
		{
			spriteBatch.Draw(indicatorTexture, screenPos, (Rectangle?)null, Color.White * modPlayer.stealthUIAlpha, 0f, indicatorTexture.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		}
		float completionRatio = ((modPlayer.rogueStealthMax <= 0f) ? 0f : (modPlayer.rogueStealth / modPlayer.rogueStealthMax));
		Rectangle barRectangle = default(Rectangle);
		((Rectangle)(ref barRectangle))._002Ector(0, 0, (int)((float)barTexture.Width * completionRatio), barTexture.Width);
		bool full = modPlayer.rogueStealthMax > 0f && modPlayer.rogueStealth >= modPlayer.rogueStealthMax;
		spriteBatch.Draw(full ? fullBarTexture : barTexture, screenPos + new Vector2(offset * uiScale, 0f), (Rectangle?)barRectangle, Color.White * modPlayer.stealthUIAlpha, 0f, indicatorTexture.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
	}
}
