using System;
using System.Collections.Generic;
using System.Text;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.Rippers;

[Autoload(true, Side = ModSide.Client)]
public sealed class RipperUI : ModSystem
{
	public const float DefaultRagePosX = 35.77406f;

	public const float DefaultRagePosY = 4.5761433f;

	public const float DefaultAdrenPosX = 35.77406f;

	public const float DefaultAdrenPosY = 8.846918f;

	private const float MouseDragEpsilon = 0.05f;

	private const int RageAnimFrameDelay = 6;

	private const int RageAnimFrames = 10;

	private const int AdrenAnimFrameDelay = 5;

	private const int AdrenAnimFrames = 10;

	private static Vector2? rageDragOffset;

	private static Vector2? adrenDragOffset;

	private static Vector2 pearlOffsetLeft;

	private static Vector2 pearlOffsetCenter;

	private static Vector2 pearlOffsetRight;

	private static int rageAnimFrame;

	private static int rageAnimTimer;

	private static int adrenAnimFrame;

	private static int adrenAnimTimer;

	private const int AdrenBarFrames = 12;

	private const int AdrenBarFullFrames = 6;

	private const int AdrenBarFrameDelay = 5;

	private static int adrenBarFrame;

	private static int adrenBarFullFrame;

	private static int adrenBarTimer;

	private static Texture2D rageBarTex;

	private static Texture2D rageBorderTex;

	private static Texture2D rageAnimTex;

	private static Texture2D mushroomPlasmaTex;

	private static Texture2D infernalBloodTex;

	private static Texture2D redLightningTex;

	private static Texture2D adrenBarTex;

	private static Texture2D adrenBorderTex;

	private static Texture2D adrenBorderTexFull;

	private static Texture2D adrenAnimTex;

	private static Texture2D draedonBarTex;

	private static Texture2D draedonAnimTex;

	private static Texture2D electrolyteGelTex;

	private static Texture2D starlightFuelTex;

	private static Texture2D ectoheartTex;

	private static Vector2 PearlOffsetCenterLeft
	{
		get
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			return 0.5f * (pearlOffsetLeft + pearlOffsetCenter);
		}
	}

	private static Vector2 PearlOffsetCenterRight
	{
		get
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			return 0.5f * (pearlOffsetRight + pearlOffsetCenter);
		}
	}

	public override void OnModLoad()
	{
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		rageBarTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/RageBar", (AssetRequestMode)1).Value;
		rageBorderTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/RageBarBorder", (AssetRequestMode)1).Value;
		rageAnimTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/RageFullAnimation", (AssetRequestMode)1).Value;
		adrenBarTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/AdrenalineBar", (AssetRequestMode)1).Value;
		adrenBorderTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/AdrenalineBarBorder", (AssetRequestMode)1).Value;
		adrenBorderTexFull = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/AdrenalineBarBorderFull", (AssetRequestMode)1).Value;
		adrenAnimTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/AdrenalineFullAnimation", (AssetRequestMode)1).Value;
		draedonBarTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/DraedonAdrenalineBar", (AssetRequestMode)1).Value;
		draedonAnimTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/DraedonAdrenalineFullAnimation", (AssetRequestMode)1).Value;
		mushroomPlasmaTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/RageDisplay_MushroomPlasmaRoot", (AssetRequestMode)1).Value;
		infernalBloodTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/RageDisplay_InfernalBlood", (AssetRequestMode)1).Value;
		redLightningTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/RageDisplay_RedLightningContainer", (AssetRequestMode)1).Value;
		electrolyteGelTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/AdrenalineDisplay_ElectrolyteGelPack", (AssetRequestMode)1).Value;
		starlightFuelTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/AdrenalineDisplay_StarlightFuelCell", (AssetRequestMode)1).Value;
		ectoheartTex = ModContent.Request<Texture2D>("CalamityMod/UI/Rippers/AdrenalineDisplay_Ectoheart", (AssetRequestMode)1).Value;
		pearlOffsetLeft = new Vector2((float)rageBorderTex.Width * 0.3333f - 6f, (float)rageBorderTex.Height - 9f);
		pearlOffsetCenter = new Vector2((float)rageBorderTex.Width * 0.5f - 6f, (float)rageBorderTex.Height - 9f);
		pearlOffsetRight = new Vector2((float)rageBorderTex.Width * 0.6667f - 6f, (float)rageBorderTex.Height - 9f);
		Reset();
	}

	public override void Unload()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Reset();
		rageBarTex = (rageBorderTex = (rageAnimTex = null));
		adrenBarTex = (adrenBorderTex = (adrenBorderTexFull = (adrenAnimTex = (draedonBarTex = (draedonAnimTex = null)))));
		mushroomPlasmaTex = (infernalBloodTex = (redLightningTex = null));
		electrolyteGelTex = (starlightFuelTex = (ectoheartTex = null));
		pearlOffsetLeft = (pearlOffsetCenter = (pearlOffsetRight = Vector2.Zero));
	}

	internal static void Reset()
	{
		rageDragOffset = null;
		adrenDragOffset = null;
		rageAnimFrame = -1;
		rageAnimTimer = 0;
		adrenAnimFrame = -1;
		adrenAnimTimer = 0;
		adrenBarFrame = -1;
		adrenBarFullFrame = -1;
		adrenBarTimer = 0;
	}

	public static void Draw(SpriteBatch spriteBatch, Player player)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Invalid comparison between Unknown and I4
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Invalid comparison between Unknown and I4
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 rageScreenRatioPos = default(Vector2);
		((Vector2)(ref rageScreenRatioPos))._002Ector(CalamityClientConfig.Instance.RageMeterPosX, CalamityClientConfig.Instance.RageMeterPosY);
		if (rageScreenRatioPos.X < 0f || rageScreenRatioPos.X > 100f)
		{
			rageScreenRatioPos.X = 35.77406f;
		}
		if (rageScreenRatioPos.Y < 0f || rageScreenRatioPos.Y > 100f)
		{
			rageScreenRatioPos.Y = 4.5761433f;
		}
		Vector2 adrenScreenRatioPos = default(Vector2);
		((Vector2)(ref adrenScreenRatioPos))._002Ector(CalamityClientConfig.Instance.AdrenalineMeterPosX, CalamityClientConfig.Instance.AdrenalineMeterPosY);
		if (adrenScreenRatioPos.X < 0f || adrenScreenRatioPos.X > 100f)
		{
			adrenScreenRatioPos.X = 35.77406f;
		}
		if (adrenScreenRatioPos.Y < 0f || adrenScreenRatioPos.Y > 100f)
		{
			adrenScreenRatioPos.Y = 8.846918f;
		}
		Vector2 rageScreenPos = rageScreenRatioPos;
		rageScreenPos.X = (int)(rageScreenPos.X * 0.01f * (float)Main.screenWidth);
		rageScreenPos.Y = (int)(rageScreenPos.Y * 0.01f * (float)Main.screenHeight);
		Vector2 adrenScreenPos = adrenScreenRatioPos;
		adrenScreenPos.X = (int)(adrenScreenPos.X * 0.01f * (float)Main.screenWidth);
		adrenScreenPos.Y = (int)(adrenScreenPos.Y * 0.01f * (float)Main.screenHeight);
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.RageEnabled)
		{
			DrawRageBar(spriteBatch, modPlayer, rageScreenPos);
		}
		else
		{
			bool changed = false;
			if (CalamityClientConfig.Instance.RageMeterPosX != rageScreenRatioPos.X)
			{
				CalamityClientConfig.Instance.RageMeterPosX = rageScreenRatioPos.X;
				changed = true;
			}
			if (CalamityClientConfig.Instance.RageMeterPosY != rageScreenRatioPos.Y)
			{
				CalamityClientConfig.Instance.RageMeterPosY = rageScreenRatioPos.Y;
				changed = true;
			}
			if (changed)
			{
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
		if (modPlayer.AdrenalineEnabled)
		{
			DrawAdrenalineBar(spriteBatch, modPlayer, adrenScreenPos);
		}
		else
		{
			bool changed2 = false;
			if (CalamityClientConfig.Instance.AdrenalineMeterPosX != adrenScreenRatioPos.X)
			{
				CalamityClientConfig.Instance.AdrenalineMeterPosX = adrenScreenRatioPos.X;
				changed2 = true;
			}
			if (CalamityClientConfig.Instance.AdrenalineMeterPosY != adrenScreenRatioPos.Y)
			{
				CalamityClientConfig.Instance.AdrenalineMeterPosY = adrenScreenRatioPos.Y;
				changed2 = true;
			}
			if (changed2)
			{
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
		float uiScale = Main.UIScale;
		Rectangle mouseHitbox = default(Rectangle);
		((Rectangle)(ref mouseHitbox))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 8, 8);
		bool num = modPlayer.adrenaline >= modPlayer.adrenalineMax || modPlayer.adrenalineModeActive;
		Vector2 adrenSize = default(Vector2);
		((Vector2)(ref adrenSize))._002Ector((float)adrenBorderTex.Width, (float)(adrenBorderTex.Height / 12));
		if (num)
		{
			((Vector2)(ref adrenSize))._002Ector((float)adrenBorderTexFull.Width, (float)(adrenBorderTexFull.Height / 6));
		}
		Rectangle rageBar = Utils.CenteredRectangle(rageScreenPos, rageBorderTex.Size() * uiScale);
		Rectangle adrenBar = Utils.CenteredRectangle(adrenScreenPos, adrenSize * uiScale);
		bool num2 = ((Rectangle)(ref mouseHitbox)).Intersects(rageBar) && modPlayer.RageEnabled;
		bool adrenHover = ((Rectangle)(ref mouseHitbox)).Intersects(adrenBar) && modPlayer.AdrenalineEnabled;
		MouseState ms = Mouse.GetState();
		Vector2 mousePos = Main.MouseScreen;
		if (num2 && !adrenHover)
		{
			if (!CalamityClientConfig.Instance.MeterPosLock)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			string rageStr = MakeRipperPercentString(modPlayer.rage, modPlayer.rageMax);
			Main.instance.MouseText(CalamityUtils.GetTextValue("UI.Rage") + ": " + rageStr, 0, 0);
			Vector2 newScreenRatioPosition = rageScreenRatioPos;
			if (!CalamityClientConfig.Instance.MeterPosLock && (int)((MouseState)(ref ms)).LeftButton == 1)
			{
				if (!rageDragOffset.HasValue)
				{
					rageDragOffset = mousePos - rageScreenPos;
				}
				Vector2 newCorner = mousePos - rageDragOffset.GetValueOrDefault(Vector2.Zero);
				newScreenRatioPosition.X = 100f * newCorner.X / (float)Main.screenWidth;
				newScreenRatioPosition.Y = 100f * newCorner.Y / (float)Main.screenHeight;
			}
			Vector2 delta = newScreenRatioPosition - rageScreenRatioPos;
			if (Math.Abs(delta.X) >= 0.05f || Math.Abs(delta.Y) >= 0.05f)
			{
				CalamityClientConfig.Instance.RageMeterPosX = newScreenRatioPosition.X;
				CalamityClientConfig.Instance.RageMeterPosY = newScreenRatioPosition.Y;
			}
			if (rageDragOffset.HasValue && (int)((MouseState)(ref ms)).LeftButton == 0)
			{
				rageDragOffset = null;
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
		else
		{
			if (!adrenHover)
			{
				return;
			}
			if (!CalamityClientConfig.Instance.MeterPosLock)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			string adrenNameStr = CalamityUtils.GetTextValue("UI." + (modPlayer.draedonsHeart ? "Nanomachines" : "Adrenaline"));
			string adrenAmountStr = MakeRipperPercentString(modPlayer.adrenaline, modPlayer.adrenalineMax);
			Main.instance.MouseText(adrenNameStr + ": " + adrenAmountStr, 0, 0);
			Vector2 newScreenRatioPosition2 = adrenScreenRatioPos;
			if (!CalamityClientConfig.Instance.MeterPosLock && (int)((MouseState)(ref ms)).LeftButton == 1)
			{
				if (!adrenDragOffset.HasValue)
				{
					adrenDragOffset = mousePos - adrenScreenPos;
				}
				Vector2 newCorner2 = mousePos - adrenDragOffset.GetValueOrDefault(Vector2.Zero);
				newScreenRatioPosition2.X = 100f * newCorner2.X / (float)Main.screenWidth;
				newScreenRatioPosition2.Y = 100f * newCorner2.Y / (float)Main.screenHeight;
			}
			Vector2 delta2 = newScreenRatioPosition2 - adrenScreenRatioPos;
			if (Math.Abs(delta2.X) >= 0.05f || Math.Abs(delta2.Y) >= 0.05f)
			{
				CalamityClientConfig.Instance.AdrenalineMeterPosX = newScreenRatioPosition2.X;
				CalamityClientConfig.Instance.AdrenalineMeterPosY = newScreenRatioPosition2.Y;
			}
			if (adrenDragOffset.HasValue && (int)((MouseState)(ref ms)).LeftButton == 0)
			{
				adrenDragOffset = null;
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
	}

	private static void DrawRageBar(SpriteBatch spriteBatch, CalamityPlayer modPlayer, Vector2 screenPos)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		float uiScale = Main.UIScale;
		Vector2 shakeOffset = (modPlayer.rageModeActive ? GetShakeOffset() : Vector2.Zero);
		float rageRatio = modPlayer.rage / modPlayer.rageMax;
		if (rageRatio >= 1f && rageAnimFrame == -1)
		{
			rageAnimFrame = 0;
		}
		else if (rageRatio < 1f && rageAnimFrame == 10)
		{
			rageAnimFrame = -1;
		}
		bool animationActive = rageAnimFrame >= 0 && rageAnimFrame < 10;
		if (animationActive)
		{
			rageAnimTimer++;
			if (rageAnimTimer >= 6)
			{
				rageAnimTimer = 0;
				rageAnimFrame++;
			}
		}
		spriteBatch.Draw(rageBorderTex, screenPos + shakeOffset, (Rectangle?)null, Color.White, 0f, rageBorderTex.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		int barWidth = rageBarTex.Width;
		float offset = (float)(rageBorderTex.Width - rageBarTex.Width) * 0.5f;
		Rectangle cropRect = default(Rectangle);
		((Rectangle)(ref cropRect))._002Ector(0, 0, (int)((float)barWidth * rageRatio), rageBarTex.Height);
		spriteBatch.Draw(rageBarTex, screenPos + shakeOffset + new Vector2(offset * uiScale, 0f), (Rectangle?)cropRect, Color.White, 0f, rageBorderTex.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		IList<Texture2D> pearls = new List<Texture2D>(3);
		if (modPlayer.rageBoostOne)
		{
			pearls.Add(mushroomPlasmaTex);
		}
		if (modPlayer.rageBoostTwo)
		{
			pearls.Add(infernalBloodTex);
		}
		if (modPlayer.rageBoostThree)
		{
			pearls.Add(redLightningTex);
		}
		IList<Vector2> offsets = GetPearlOffsets(pearls.Count);
		for (int i = 0; i < pearls.Count; i++)
		{
			spriteBatch.Draw(pearls[i], screenPos + shakeOffset + offsets[i] * uiScale, (Rectangle?)null, Color.White, 0f, rageBorderTex.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		}
		if (animationActive)
		{
			float xOffset = (float)(rageBorderTex.Width - rageAnimTex.Width) / 2f;
			int frameHeight = rageAnimTex.Height / 10 - 1;
			float yOffset = (float)(rageBorderTex.Height - frameHeight) / 2f;
			Vector2 sizeDiffOffset = default(Vector2);
			((Vector2)(ref sizeDiffOffset))._002Ector(xOffset, yOffset);
			Rectangle animCropRect = default(Rectangle);
			((Rectangle)(ref animCropRect))._002Ector(0, (frameHeight + 1) * rageAnimFrame, rageAnimTex.Width, frameHeight);
			spriteBatch.Draw(rageAnimTex, screenPos + shakeOffset + sizeDiffOffset, (Rectangle?)animCropRect, Color.White, 0f, rageBorderTex.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		}
	}

	private static void DrawAdrenalineBar(SpriteBatch spriteBatch, CalamityPlayer modPlayer, Vector2 screenPos)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		bool draedonHeart = modPlayer.draedonsHeart;
		bool useFullTexture = modPlayer.adrenaline >= modPlayer.adrenalineMax || modPlayer.adrenalineModeActive;
		float uiScale = Main.UIScale;
		Vector2 shakeOffset = (modPlayer.adrenalineModeActive ? GetShakeOffset() : Vector2.Zero);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)adrenBorderTex.Width * 0.5f, (float)(adrenBorderTex.Height / 12) * 0.5f);
		if (useFullTexture)
		{
			((Vector2)(ref origin))._002Ector((float)adrenBorderTexFull.Width * 0.5f, (float)(adrenBorderTexFull.Height / 6) * 0.5f);
		}
		if (draedonHeart)
		{
			adrenBarTimer++;
			if (adrenBarTimer >= 5)
			{
				adrenBarTimer = 0;
				adrenBarFrame++;
				adrenBarFullFrame++;
				if (adrenBarFrame == 12)
				{
					adrenBarFrame = 1;
				}
				if (adrenBarFullFrame == 6)
				{
					adrenBarFullFrame = 1;
				}
			}
		}
		else
		{
			adrenBarTimer = 0;
			adrenBarFrame = 0;
			adrenBarFullFrame = 0;
		}
		float adrenRatio = modPlayer.adrenaline / modPlayer.adrenalineMax;
		if (adrenRatio >= 1f && adrenAnimFrame == -1)
		{
			adrenAnimFrame = 0;
		}
		else if (adrenRatio < 1f && adrenAnimFrame == 10)
		{
			adrenAnimFrame = -1;
		}
		bool animationActive = adrenAnimFrame >= 0 && adrenAnimFrame < 10;
		if (animationActive)
		{
			adrenAnimTimer++;
			if (adrenAnimTimer >= 5)
			{
				adrenAnimTimer = 0;
				adrenAnimFrame++;
			}
		}
		if (!useFullTexture)
		{
			int frameHeight = adrenBorderTex.Height / 12 - 1;
			Rectangle borderRect = default(Rectangle);
			((Rectangle)(ref borderRect))._002Ector(0, (frameHeight + 1) * adrenBarFrame, adrenBorderTex.Width, frameHeight);
			spriteBatch.Draw(adrenBorderTex, screenPos + shakeOffset, (Rectangle?)borderRect, Color.White, 0f, origin, uiScale, (SpriteEffects)0, 0f);
		}
		else
		{
			int frameHeight2 = adrenBorderTexFull.Height / 6 - 1;
			Rectangle borderRect2 = default(Rectangle);
			((Rectangle)(ref borderRect2))._002Ector(0, (frameHeight2 + 1) * adrenBarFullFrame, adrenBorderTexFull.Width, frameHeight2);
			spriteBatch.Draw(adrenBorderTexFull, screenPos + shakeOffset, (Rectangle?)borderRect2, Color.White, 0f, origin, uiScale, (SpriteEffects)0, 0f);
		}
		int barWidth = adrenBarTex.Width;
		float offset = (float)(adrenBorderTex.Width - adrenBarTex.Width) * 0.5f;
		Rectangle cropRect = default(Rectangle);
		((Rectangle)(ref cropRect))._002Ector(0, 0, (int)((float)barWidth * adrenRatio), adrenBarTex.Height);
		spriteBatch.Draw(draedonHeart ? draedonBarTex : adrenBarTex, screenPos + shakeOffset + new Vector2(offset * uiScale, 2f), (Rectangle?)cropRect, Color.White, 0f, origin, uiScale, (SpriteEffects)0, 0f);
		IList<Texture2D> pearls = new List<Texture2D>(3);
		if (modPlayer.adrenalineBoostOne)
		{
			pearls.Add(electrolyteGelTex);
		}
		if (modPlayer.adrenalineBoostTwo)
		{
			pearls.Add(starlightFuelTex);
		}
		if (modPlayer.adrenalineBoostThree)
		{
			pearls.Add(ectoheartTex);
		}
		IList<Vector2> offsets = GetPearlOffsets(pearls.Count);
		for (int i = 0; i < pearls.Count; i++)
		{
			spriteBatch.Draw(pearls[i], screenPos + shakeOffset + offsets[i] * uiScale + new Vector2(0f, 5f), (Rectangle?)null, Color.White, 0f, origin, uiScale, (SpriteEffects)0, 0f);
		}
		if (animationActive)
		{
			float animOffset = 5f;
			float xOffset = (float)(adrenBorderTex.Width - adrenAnimTex.Width) / 2f;
			int frameHeight3 = adrenAnimTex.Height / 10 - 1;
			float yOffset = (float)(adrenBorderTex.Height / 12 - frameHeight3) / 2f + animOffset;
			if (useFullTexture)
			{
				yOffset = (float)(adrenBorderTexFull.Height / 6 - frameHeight3) / 2f + animOffset;
			}
			Vector2 sizeDiffOffset = default(Vector2);
			((Vector2)(ref sizeDiffOffset))._002Ector(xOffset, yOffset);
			Rectangle animCropRect = default(Rectangle);
			((Rectangle)(ref animCropRect))._002Ector(0, (frameHeight3 + 1) * adrenAnimFrame, adrenAnimTex.Width, frameHeight3);
			spriteBatch.Draw(draedonHeart ? draedonAnimTex : adrenAnimTex, screenPos + shakeOffset + sizeDiffOffset, (Rectangle?)animCropRect, Color.White, 0f, origin, uiScale, (SpriteEffects)0, 0f);
		}
	}

	private static Vector2 GetShakeOffset()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		float shake = CalamityClientConfig.Instance.RipperMeterShake;
		float num = Main.rand.NextFloat(0f - shake, shake);
		float shakeY = Main.rand.NextFloat(0f - shake, shake);
		return new Vector2(num, shakeY);
	}

	private static IList<Vector2> GetPearlOffsets(int count)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		return count switch
		{
			1 => new List<Vector2> { pearlOffsetCenter }, 
			2 => new List<Vector2> { PearlOffsetCenterLeft, PearlOffsetCenterRight }, 
			3 => new List<Vector2> { pearlOffsetLeft, pearlOffsetCenter, pearlOffsetRight }, 
			_ => null, 
		};
	}

	private static string MakeRipperPercentString(float value, float maxValue)
	{
		string topTwoDecimalPlaces = value.ToString("n2");
		string bottomTwoDecimalPlaces = maxValue.ToString("n2");
		string percent = (100f * value / maxValue).ToString("0.00");
		StringBuilder stringBuilder = new StringBuilder(32);
		stringBuilder.Append(percent);
		stringBuilder.Append("% (");
		stringBuilder.Append(topTwoDecimalPlaces);
		stringBuilder.Append(" / ");
		stringBuilder.Append(bottomTwoDecimalPlaces);
		stringBuilder.Append(')');
		return stringBuilder.ToString();
	}

	static RipperUI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		rageDragOffset = null;
		adrenDragOffset = null;
		pearlOffsetLeft = Vector2.Zero;
		pearlOffsetCenter = Vector2.Zero;
		pearlOffsetRight = Vector2.Zero;
		rageAnimFrame = -1;
		rageAnimTimer = 0;
		adrenAnimFrame = -1;
		adrenAnimTimer = 0;
		adrenBarFrame = -1;
		adrenBarFullFrame = -1;
		adrenBarTimer = 0;
	}
}
