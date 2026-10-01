using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI;

[Autoload(true, Side = ModSide.Client)]
public sealed class FlightBar : ModSystem
{
	internal const float DefaultFlightPosX = 40.9375f;

	internal const float DefaultFlightPosY = 7.2222223f;

	private const float MouseDragEpsilon = 0.05f;

	private const int FlightAnimFrameDelay = 5;

	private const int FlightAnimFrames = 17;

	private static int FlightAnimFrame = -1;

	private static int FlightAnimTimer = 0;

	private static Vector2? dragOffset = null;

	private static Texture2D borderTexture;

	private static Texture2D flightBarAnimTexture;

	private static Texture2D barTexture;

	private static Texture2D disabledBarTexture;

	private static Texture2D infiniteBarTexture;

	private static Texture2D limitedBarTexture;

	private static bool completedAnimation = false;

	public static List<int> infiniteFlightMounts = new List<int> { 7, 8, 44, 23, 12 };

	private static Texture2D GetApplicableBorder(CalamityPlayer modPlayer)
	{
		if (modPlayer.Player.equippedWings != null && modPlayer.Player.wingTimeMax == 0 && modPlayer.Player.mount.Active && modPlayer.Player.mount._data.flightTimeMax == 0)
		{
			return disabledBarTexture;
		}
		if ((modPlayer.infiniteFlight || RidingInfiniteFlightMount(modPlayer.Player)) && completedAnimation)
		{
			return infiniteBarTexture;
		}
		if (modPlayer.weakPetrification || modPlayer.icarusFolly || modPlayer.DoGExtremeGravity)
		{
			return limitedBarTexture;
		}
		return borderTexture;
	}

	private static object GetFlightTime(CalamityPlayer modPlayer)
	{
		Player player = modPlayer.Player;
		if (player.equippedWings != null && player.wingTimeMax == 0 && (!player.mount.Active || player.mount._data.flightTimeMax <= 0))
		{
			object result = 0;
		}
		if ((modPlayer.infiniteFlight || RidingInfiniteFlightMount(modPlayer.Player)) && completedAnimation)
		{
			return "∞";
		}
		bool ridingLimitedFlightMount = player.mount.Active && player.mount._data.flightTimeMax > 0;
		int num;
		int num2;
		if (player.carpet)
		{
			num = ((!player.canCarpet) ? 1 : 0);
			if (num != 0)
			{
				num2 = player.carpetTime;
				goto IL_00de;
			}
		}
		else
		{
			num = 0;
		}
		num2 = (ridingLimitedFlightMount ? (player.mount._flyTime + (int)((float)player.mount._data.fatigueMax - player.mount._fatigue)) : ((int)player.wingTime));
		goto IL_00de;
		IL_00de:
		int currentFlight = num2;
		int maxFlight = ((num != 0) ? 300 : (ridingLimitedFlightMount ? (player.mount._data.flightTimeMax + player.mount._data.fatigueMax) : player.wingTimeMax));
		return Math.Min(100f * (float)currentFlight / (float)maxFlight, 100f).ToString("0.00");
	}

	public override void OnModLoad()
	{
		borderTexture = ModContent.Request<Texture2D>("CalamityMod/UI/FlightBar/FlightBarBorder", (AssetRequestMode)1).Value;
		flightBarAnimTexture = ModContent.Request<Texture2D>("CalamityMod/UI/FlightBar/FlightBarAnim", (AssetRequestMode)1).Value;
		barTexture = ModContent.Request<Texture2D>("CalamityMod/UI/FlightBar/FlightBar", (AssetRequestMode)1).Value;
		disabledBarTexture = ModContent.Request<Texture2D>("CalamityMod/UI/FlightBar/FlightBarBorderDisabled", (AssetRequestMode)1).Value;
		infiniteBarTexture = ModContent.Request<Texture2D>("CalamityMod/UI/FlightBar/FlightBarBorderInfinite", (AssetRequestMode)1).Value;
		limitedBarTexture = ModContent.Request<Texture2D>("CalamityMod/UI/FlightBar/FlightBarBorderReduced", (AssetRequestMode)1).Value;
		Reset();
	}

	public override void Unload()
	{
		Reset();
		borderTexture = (flightBarAnimTexture = (barTexture = (disabledBarTexture = (infiniteBarTexture = (limitedBarTexture = null)))));
		FlightAnimFrame = -1;
		FlightAnimTimer = 0;
		completedAnimation = false;
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
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Invalid comparison between Unknown and I4
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		Vector2 screenRatioPosition = default(Vector2);
		((Vector2)(ref screenRatioPosition))._002Ector(CalamityClientConfig.Instance.FlightBarPosX, CalamityClientConfig.Instance.FlightBarPosY);
		if (screenRatioPosition.X < 0f || screenRatioPosition.X > 100f)
		{
			screenRatioPosition.X = 40.9375f;
		}
		if (screenRatioPosition.Y < 0f || screenRatioPosition.Y > 100f)
		{
			screenRatioPosition.Y = 7.2222223f;
		}
		float uiScale = Main.UIScale;
		Vector2 screenPos = screenRatioPosition;
		screenPos.X = (int)(screenPos.X * 0.01f * (float)Main.screenWidth);
		screenPos.Y = (int)(screenPos.Y * 0.01f * (float)Main.screenHeight);
		CalamityPlayer modPlayer = player.Calamity();
		if (CalamityClientConfig.Instance.FlightBar && ((player.wingsLogic > 0 && player.wingTimeMax > 0) || (player.mount.Active && player.mount._data.flightTimeMax > 0) || (player.carpet && !player.canCarpet)))
		{
			DrawFlightBar(spriteBatch, modPlayer, screenPos);
			Rectangle mouseHitbox = default(Rectangle);
			((Rectangle)(ref mouseHitbox))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 8, 8);
			Rectangle flightBar = Utils.CenteredRectangle(screenPos, borderTexture.Size() * uiScale);
			MouseState ms = Mouse.GetState();
			Vector2 mousePos = Main.MouseScreen;
			if (!((Rectangle)(ref flightBar)).Intersects(mouseHitbox))
			{
				return;
			}
			if (!CalamityClientConfig.Instance.MeterPosLock)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			if ((modPlayer.Player.equippedWings != null && modPlayer.Player.wingTimeMax > 0) || (player.mount.Active && modPlayer.Player.mount._data.flightTimeMax > 0) || (player.carpet && !player.canCarpet))
			{
				string textToDisplay = CalamityUtils.GetText("UI.Flight").Format(GetFlightTime(modPlayer).ToString() + (modPlayer.infiniteFlight ? "" : "%"));
				Main.instance.MouseText(textToDisplay, 0, 0);
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
				CalamityClientConfig.Instance.FlightBarPosX = newScreenRatioPosition.X;
				CalamityClientConfig.Instance.FlightBarPosY = newScreenRatioPosition.Y;
			}
			if (dragOffset.HasValue && (int)((MouseState)(ref ms)).LeftButton == 0)
			{
				dragOffset = null;
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
		else
		{
			bool changed = false;
			if (CalamityClientConfig.Instance.FlightBarPosX != screenRatioPosition.X)
			{
				CalamityClientConfig.Instance.FlightBarPosX = screenRatioPosition.X;
				changed = true;
			}
			if (CalamityClientConfig.Instance.FlightBarPosY != screenRatioPosition.Y)
			{
				CalamityClientConfig.Instance.FlightBarPosY = screenRatioPosition.Y;
				changed = true;
			}
			if (changed)
			{
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
	}

	private static void DrawFlightBar(SpriteBatch spriteBatch, CalamityPlayer modPlayer, Vector2 screenPos)
	{
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		float uiScale = Main.UIScale;
		Player player = modPlayer.Player;
		float flightRatio = 1f;
		if (!modPlayer.infiniteFlight && !RidingInfiniteFlightMount(player))
		{
			flightRatio = ((player.carpet && !player.canCarpet) ? Math.Min((float)player.carpetTime / 300f, 1f) : ((player.mount.Active && player.mount._data.flightTimeMax > 0) ? Math.Min(((float)player.mount._flyTime + ((float)player.mount._data.fatigueMax - player.mount._fatigue)) / (float)(player.mount._data.flightTimeMax + player.mount._data.fatigueMax), 1f) : Math.Min(player.wingTime / (float)player.wingTimeMax, 1f)));
		}
		if (!completedAnimation && FlightAnimFrame == -1 && (modPlayer.infiniteFlight || RidingInfiniteFlightMount(modPlayer.Player)))
		{
			FlightAnimFrame++;
		}
		if (FlightAnimFrame > -1)
		{
			FlightAnimTimer++;
			if (FlightAnimTimer >= 5)
			{
				if (FlightAnimFrame >= 16)
				{
					FlightAnimFrame = -1;
					FlightAnimTimer = 0;
					completedAnimation = modPlayer.infiniteFlight || RidingInfiniteFlightMount(modPlayer.Player);
				}
				else
				{
					FlightAnimTimer = 0;
					FlightAnimFrame++;
				}
			}
		}
		Texture2D correctBorder = GetApplicableBorder(modPlayer);
		if (completedAnimation && !modPlayer.infiniteFlight && correctBorder != infiniteBarTexture)
		{
			completedAnimation = false;
		}
		float offset = (float)(correctBorder.Width - barTexture.Width) * 0.5f;
		spriteBatch.Draw(correctBorder, screenPos, (Rectangle?)null, Color.White, 0f, correctBorder.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		if (correctBorder != disabledBarTexture && correctBorder != infiniteBarTexture)
		{
			int correctHeight = ((correctBorder == limitedBarTexture) ? (barTexture.Height / 2) : barTexture.Height);
			Rectangle barRectangle = barTexture.Bounds;
			barRectangle.Height = (int)((float)correctHeight * flightRatio);
			Vector2 origin = correctBorder.Size() * 0.5f;
			origin.Y += 0.1f;
			Vector2 drawPos = screenPos - new Vector2(offset * uiScale, 12f * uiScale);
			spriteBatch.Draw(barTexture, drawPos, (Rectangle?)barRectangle, Color.White, MathHelper.ToRadians(180f), origin, uiScale, (SpriteEffects)0, 0f);
		}
		if (!completedAnimation && FlightAnimFrame >= 0)
		{
			Vector2 origin2 = default(Vector2);
			((Vector2)(ref origin2))._002Ector((float)correctBorder.Width * 0.5f, (float)(correctBorder.Height / 17) * 0.5f);
			float num = (float)(correctBorder.Width - flightBarAnimTexture.Width) / 2f;
			int frameHeight = flightBarAnimTexture.Height / 17 - 1;
			Vector2 sizeDiffOffset = new Vector2(num, -37f) * uiScale;
			Rectangle animCropRect = default(Rectangle);
			((Rectangle)(ref animCropRect))._002Ector(0, (frameHeight + 1) * FlightAnimFrame, flightBarAnimTexture.Width, frameHeight);
			spriteBatch.Draw(flightBarAnimTexture, screenPos + sizeDiffOffset, (Rectangle?)animCropRect, Color.White, 0f, origin2, uiScale, (SpriteEffects)0, 0f);
		}
	}

	private static bool RidingInfiniteFlightMount(Player player)
	{
		if (player.mount.Active && (player.mount._data.fatigueMax >= 2147483646 || infiniteFlightMounts.Contains(player.mount.Type)))
		{
			return true;
		}
		return false;
	}
}
