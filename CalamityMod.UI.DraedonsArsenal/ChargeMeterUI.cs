using System;
using CalamityMod.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonsArsenal;

[Autoload(true, Side = ModSide.Client)]
public sealed class ChargeMeterUI : ModSystem
{
	internal const float DefaultChargePosX = 50.104603f;

	internal const float DefaultChargePosY = 58.05169f;

	private const float MouseDragEpsilon = 0.05f;

	private static Vector2? dragOffset;

	private static Texture2D edgeTexture;

	private static Texture2D barTexture;

	public override void OnModLoad()
	{
		edgeTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/ChargeMeterBorder", (AssetRequestMode)1).Value;
		barTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/ChargeMeter", (AssetRequestMode)1).Value;
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
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Invalid comparison between Unknown and I4
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 screenRatioPosition = new Vector2(CalamityClientConfig.Instance.ChargeMeterPosX, CalamityClientConfig.Instance.ChargeMeterPosY);
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
		Item heldItem = player.HeldItem;
		if (!CalamityClientConfig.Instance.ChargeMeter || heldItem == null || heldItem.IsAir)
		{
			Reset();
			SavePosition();
			return;
		}
		CalamityGlobalItem modItem = heldItem.Calamity();
		if (modItem == null || !modItem.UsesCharge)
		{
			Reset();
			SavePosition();
			return;
		}
		DrawChargeBar(spriteBatch, modItem, screenPos);
		Rectangle mouseHitbox = default(Rectangle);
		((Rectangle)(ref mouseHitbox))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 8, 8);
		Rectangle chargeBar = Utils.CenteredRectangle(screenPos, edgeTexture.Size() * uiScale);
		MouseState ms = Mouse.GetState();
		Vector2 mousePos = Main.MouseScreen;
		if (!((Rectangle)(ref chargeBar)).Intersects(mouseHitbox))
		{
			return;
		}
		if (!CalamityClientConfig.Instance.MeterPosLock)
		{
			Main.LocalPlayer.mouseInterface = true;
		}
		string percentString = (100f * modItem.ChargeRatio).ToString("n2");
		Main.instance.MouseText(CalamityUtils.GetTextValue("UI.Charge") + ": " + percentString + "%", 0, 0);
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
			CalamityClientConfig.Instance.ChargeMeterPosX = newScreenRatioPosition.X;
			CalamityClientConfig.Instance.ChargeMeterPosY = newScreenRatioPosition.Y;
		}
		if (dragOffset.HasValue && (int)((MouseState)(ref ms)).LeftButton == 0)
		{
			dragOffset = null;
			CalamityClientConfig.Instance.SaveChanges();
		}
		void SavePosition()
		{
			bool changed = false;
			if (CalamityClientConfig.Instance.ChargeMeterPosX != screenRatioPosition.X)
			{
				CalamityClientConfig.Instance.ChargeMeterPosX = screenRatioPosition.X;
				changed = true;
			}
			if (CalamityClientConfig.Instance.ChargeMeterPosY != screenRatioPosition.Y)
			{
				CalamityClientConfig.Instance.ChargeMeterPosY = screenRatioPosition.Y;
				changed = true;
			}
			if (changed)
			{
				CalamityClientConfig.Instance.SaveChanges();
			}
		}
	}

	private static void DrawChargeBar(SpriteBatch spriteBatch, CalamityGlobalItem modItem, Vector2 screenPos)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		float uiScale = Main.UIScale;
		float offset = (float)(edgeTexture.Width - barTexture.Width) * 0.5f;
		spriteBatch.Draw(edgeTexture, screenPos, (Rectangle?)null, Color.White, 0f, edgeTexture.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
		Rectangle barRectangle = default(Rectangle);
		((Rectangle)(ref barRectangle))._002Ector(0, 0, (int)((float)barTexture.Width * modItem.ChargeRatio), barTexture.Width);
		spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0f), (Rectangle?)barRectangle, Color.White, 0f, edgeTexture.Size() * 0.5f, uiScale, (SpriteEffects)0, 0f);
	}
}
