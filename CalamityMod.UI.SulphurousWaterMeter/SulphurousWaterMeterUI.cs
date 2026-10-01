using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.SulphurousWaterMeter;

public class SulphurousWaterMeterUI : ModSystem
{
	internal const float DefaultPosX = 50.104603f;

	internal const float DefaultPosY = 58.05169f;

	private const float MouseDragEpsilon = 0.05f;

	private static Vector2? dragOffset;

	private static Texture2D edgeTexture;

	private static Texture2D barTexture;

	public override void OnModLoad()
	{
		edgeTexture = ModContent.Request<Texture2D>("CalamityMod/UI/SulphurousWaterMeter/SulphWaterMeter", (AssetRequestMode)1).Value;
		barTexture = ModContent.Request<Texture2D>("CalamityMod/UI/SulphurousWaterMeter/SulphWaterBar", (AssetRequestMode)1).Value;
		Reset();
	}

	public override void Unload()
	{
		Reset();
		edgeTexture = (barTexture = null);
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
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Invalid comparison between Unknown and I4
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 screenRatioPosition = default(Vector2);
		((Vector2)(ref screenRatioPosition))._002Ector(CalamityClientConfig.Instance.SulphuricWaterMeterPosX, CalamityClientConfig.Instance.SulphuricWaterMeterPosY);
		if (screenRatioPosition.X < 0f || screenRatioPosition.X > 100f)
		{
			screenRatioPosition.X = 50.104603f;
		}
		if (screenRatioPosition.Y < 0f || screenRatioPosition.Y > 100f)
		{
			screenRatioPosition.Y = 58.05169f;
		}
		float uiScale = Main.UIScale;
		Vector2 screenPos = screenRatioPosition;
		screenPos.X = (int)(screenPos.X * 0.01f * (float)Main.screenWidth);
		screenPos.Y = (int)(screenPos.Y * 0.01f * (float)Main.screenHeight);
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.SulphWaterUIOpacity > 0f && (modPlayer.ZoneSulphur || player.Calamity().ZoneAbyssLayer1))
		{
			DrawWaterBar(spriteBatch, modPlayer, screenPos);
		}
		else
		{
			bool changed = false;
			if (CalamityClientConfig.Instance.SulphuricWaterMeterPosX != screenRatioPosition.X)
			{
				CalamityClientConfig.Instance.SulphuricWaterMeterPosX = screenRatioPosition.X;
				changed = true;
			}
			if (CalamityClientConfig.Instance.SulphuricWaterMeterPosY != screenRatioPosition.Y)
			{
				CalamityClientConfig.Instance.SulphuricWaterMeterPosY = screenRatioPosition.Y;
				changed = true;
			}
			if (changed)
			{
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
		Rectangle mouseHitbox = default(Rectangle);
		((Rectangle)(ref mouseHitbox))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 8, 8);
		Rectangle waterBarArea = Utils.CenteredRectangle(screenPos, edgeTexture.Size() * uiScale);
		MouseState ms = Mouse.GetState();
		Vector2 mousePos = Main.MouseScreen;
		if (((Rectangle)(ref waterBarArea)).Intersects(mouseHitbox))
		{
			if (!CalamityClientConfig.Instance.MeterPosLock)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			if (modPlayer.SulphWaterPoisoningLevel > 0f && modPlayer.SulphWaterUIOpacity >= 0f)
			{
				string poisonText = (modPlayer.SulphWaterPoisoningLevel * 100f).ToString("n2");
				string textToDisplay = CalamityUtils.GetTextValue("UI.SulphurousWater") + ": " + poisonText + "/100";
				Main.instance.MouseText(textToDisplay, 0, 0);
				modPlayer.SulphWaterUIOpacity = MathHelper.Lerp(modPlayer.SulphWaterUIOpacity, 0.25f, 0.035f);
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
				CalamityClientConfig.Instance.SulphuricWaterMeterPosX = newScreenRatioPosition.X;
				CalamityClientConfig.Instance.SulphuricWaterMeterPosY = newScreenRatioPosition.Y;
			}
			if (dragOffset.HasValue && (int)((MouseState)(ref ms)).LeftButton == 0)
			{
				dragOffset = null;
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
		else
		{
			modPlayer.SulphWaterUIOpacity = MathHelper.Clamp(modPlayer.SulphWaterUIOpacity + (float)(modPlayer.SulphWaterPoisoningLevel > 0f).ToDirectionInt() * 0.06f, 0f, 1f);
		}
	}

	private static void DrawWaterBar(SpriteBatch spriteBatch, CalamityPlayer modPlayer, Vector2 screenPos)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		float uiScale = Main.UIScale;
		float offset = (float)(edgeTexture.Width - barTexture.Width) * 0.5f;
		spriteBatch.Draw(edgeTexture, screenPos, (Rectangle?)null, Color.White * modPlayer.SulphWaterUIOpacity, 0f, edgeTexture.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		float completionRatio = MathHelper.Clamp(modPlayer.SulphWaterPoisoningLevel, 0f, 1f);
		Rectangle barRectangle = default(Rectangle);
		((Rectangle)(ref barRectangle))._002Ector(0, 0, (int)((float)barTexture.Width * completionRatio), barTexture.Width);
		spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0f), (Rectangle?)barRectangle, Color.White * modPlayer.SulphWaterUIOpacity, 0f, edgeTexture.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
	}
}
