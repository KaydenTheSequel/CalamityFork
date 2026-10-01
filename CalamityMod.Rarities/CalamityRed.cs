using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CalamityMod.Rarities;

public class CalamityRed : ModRarity
{
	public static float MaxY;

	public static Color BloomClr;

	public static Color TextClr;

	public override Color RarityColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return TextClr * 2f;
		}
	}

	public static void Draw(Item Item, SpriteBatch spriteBatch, string text, int X, int Y, Color textColor, Color lightColor, float rotation, Vector2 origin, Vector2 baseScale, float time, bool renderTextSparkles, DynamicSpriteFont font)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		Texture2D crystalTextGlow = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/UI/CrystalTextGlow", (AssetRequestMode)2).Value;
		_ = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/UI/CrystalTextSparkle", (AssetRequestMode)2).Value;
		Vector2 fontSize = font.MeasureString(text);
		Vector2 center = fontSize / 2f;
		if (Item.expert)
		{
			textColor = Main.DiscoColor;
		}
		Vector2 glowPosition = default(Vector2);
		((Vector2)(ref glowPosition))._002Ector((float)X + center.X, (float)Y + center.Y / 1.5f);
		((Color)(ref textColor)).A = 0;
		float pulsing = 10f + (float)Math.Sin(time * 20f);
		float baseScalePulse = 1.03f;
		float flameHeight = 5f;
		float distortionAmount = 2f;
		if (renderTextSparkles)
		{
			Vector2 offset = default(Vector2);
			Vector2 scale = default(Vector2);
			Color flameLayerColor = default(Color);
			for (float f = 0f; f < (float)Math.PI * 2f; f += 0.79f)
			{
				float angle = f + time * 2f % ((float)Math.PI * 2f);
				float distortion = (float)Math.Sin(((float)Y + f * 100f + time * 20f) * 0.05f) * distortionAmount;
				((Vector2)(ref offset))._002Ector((float)Math.Cos(angle) * pulsing * 0.4f + distortion, (0f - Math.Abs((float)Math.Sin(angle))) * flameHeight);
				float scaleVariation = 0.95f + 0.05f * (float)Math.Sin(time * 15f + f * 2f);
				((Vector2)(ref scale))._002Ector(baseScalePulse * scaleVariation);
				((Color)(ref flameLayerColor))._002Ector((int)((float)(int)((Color)(ref textColor)).R * 0.5f), (int)((float)(int)((Color)(ref textColor)).G * 0.5f), (int)((float)(int)((Color)(ref textColor)).B * 0.5f), (int)((float)(int)((Color)(ref textColor)).A * 0.5f));
				ChatManager.DrawColorCodedString(spriteBatch, font, text, new Vector2((float)X, (float)Y) + offset, flameLayerColor, rotation, origin, scale);
			}
		}
		ChatManager.DrawColorCodedString(spriteBatch, font, text, new Vector2((float)X, (float)Y), textColor, rotation, origin, new Vector2(baseScalePulse));
		((Color)(ref textColor)).A = byte.MaxValue;
		ChatManager.DrawColorCodedStringShadow(spriteBatch, font, text, new Vector2((float)X, (float)Y), textColor * 2f, rotation, origin, baseScale);
		ColorTool.Rainbowing(time * 4f - 0.9f);
		spriteBatch.Draw(crystalTextGlow, glowPosition, (Rectangle?)null, lightColor, rotation + (float)Math.PI / 2f, new Vector2(6f, 33f), new Vector2(1.6f, fontSize.X / (float)crystalTextGlow.Height * 1.2f), (SpriteEffects)0, 0f);
		ChatManager.DrawColorCodedString(spriteBatch, font, text, new Vector2((float)X, (float)Y), Color.Black, rotation, origin, baseScale);
	}

	public static void Draw(Item Item, string text, int X, int Y, float rotation, Vector2 origin, Vector2 baseScale, Color? textColor = null, Color? lightColor = null, bool? renderTextSparkles = null)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Draw(Item, Main.spriteBatch, text, X, Y, Colors.AlphaDarken((Color)(((_003F?)textColor) ?? TextClr)), (Color)(((_003F?)lightColor) ?? BloomClr), rotation, origin, baseScale, Main.GlobalTimeWrappedHourly, renderTextSparkles ?? CalamityClientConfig.Instance.TextEffects, FontAssets.MouseText.Value);
	}

	public static void Draw(Item Item, DrawableTooltipLine line)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Draw(Item, line.Text, line.X, line.Y, line.Rotation, line.Origin, line.BaseScale);
	}

	static CalamityRed()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		MaxY = 4.5f;
		BloomClr = new Color(180, 20, 75, 0);
		TextClr = new Color(242, 27, 27, 255);
	}
}
