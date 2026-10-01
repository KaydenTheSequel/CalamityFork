using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonsArsenal;

[Autoload(true, Side = ModSide.Client)]
public sealed class VisUI : ModSystem
{
	internal const float DefaultChargePosX = 50.104603f;

	internal const float DefaultChargePosY = 58.05169f;

	private const float MouseDragEpsilon = 0.05f;

	private static Vector2? dragOffset;

	private static Texture2D edgeTexture;

	private static Texture2D barTexture;

	public override void OnModLoad()
	{
		edgeTexture = ModContent.Request<Texture2D>("CalamityMod/UI/UnstableCastersGauntlet/VisMeterBorder", (AssetRequestMode)1).Value;
		barTexture = ModContent.Request<Texture2D>("CalamityMod/UI/UnstableCastersGauntlet/VisMeter", (AssetRequestMode)1).Value;
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
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Invalid comparison between Unknown and I4
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
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
		if (heldItem.type != ModContent.ItemType<UnstableCastersGauntlet>())
		{
			Reset();
			SavePosition();
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		DrawChargeBar(spriteBatch, modPlayer.unstableCastersGauntletVis / 100f, screenPos);
		Rectangle mouseHitbox = default(Rectangle);
		((Rectangle)(ref mouseHitbox))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 8, 8);
		Rectangle chargeBar = Utils.CenteredRectangle(screenPos, edgeTexture.Size() * uiScale * 0.8f);
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
		string percentString = player.Calamity().unstableCastersGauntletVis.ToString("n2");
		Main.instance.MouseText(CalamityUtils.GetTextValue("UI.Vis") + ": " + percentString + "%", 0, 0);
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

	private static void DrawChargeBar(SpriteBatch spriteBatch, float chargeRatio, Vector2 screenPos)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		float uiScale = Main.UIScale;
		float offset = (float)(edgeTexture.Width - barTexture.Width) * 0.5f;
		spriteBatch.Draw(edgeTexture, screenPos, (Rectangle?)null, Color.White, 0f, edgeTexture.Size() * 0.5f, uiScale * 0.8f, (SpriteEffects)0, 0f);
		Rectangle barRectangle = default(Rectangle);
		((Rectangle)(ref barRectangle))._002Ector(0, 0, (int)((float)barTexture.Width * chargeRatio), barTexture.Width);
		spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0f), (Rectangle?)barRectangle, Color.White, 0f, edgeTexture.Size() * 0.5f, uiScale * 0.85f, (SpriteEffects)0, 0f);
	}
}
