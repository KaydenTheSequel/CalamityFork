using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI;

public class EnchantmentMetersUI
{
	public static Vector2 DrawPosition
	{
		get
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			return Main.LocalPlayer.Center;
		}
	}

	public static void Draw(SpriteBatch spriteBatch, Player player)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		Item heldItem = player.HeldItem;
		if (heldItem != null && !heldItem.IsAir && player.Calamity().dischargingItemEnchant)
		{
			float dischargeFactor = heldItem.Calamity().DischargeExhaustionRatio;
			Texture2D barBorderTexture = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/EphemeralBarBorder", (AssetRequestMode)2).Value;
			Texture2D barTexture = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/EphemeralBar", (AssetRequestMode)2).Value;
			int barCutoff = (int)((float)barTexture.Height * (1f - dischargeFactor));
			Rectangle barFrame = default(Rectangle);
			((Rectangle)(ref barFrame))._002Ector(0, barCutoff, barTexture.Width, barTexture.Height - barCutoff);
			Vector2 barDrawPosition = player.Top - Vector2.UnitY * ((float)barTexture.Height * 0.5f + 40f) + Vector2.UnitY * player.gfxOffY - Main.screenPosition;
			Color barColor = Color.White * 0.6f;
			spriteBatch.Draw(barBorderTexture, barDrawPosition, (Rectangle?)null, barColor, 0f, barBorderTexture.Size() * 0.5f, 1f, (SpriteEffects)0, 0f);
			spriteBatch.Draw(barTexture, barDrawPosition, (Rectangle?)barFrame, barColor, 0f, barTexture.Size() * 0.5f, 1f, (SpriteEffects)2, 0f);
		}
	}
}
