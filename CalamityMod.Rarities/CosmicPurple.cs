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
using Terraria.Utilities;

namespace CalamityMod.Rarities;

public class CosmicPurple : ModRarity
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
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D crystalTextGlow = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/UI/CrystalTextGlow", (AssetRequestMode)2).Value;
		Texture2D sparkle = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/UI/CrystalTextSparkle", (AssetRequestMode)2).Value;
		Vector2 fontSize = font.MeasureString(text);
		Vector2 center = fontSize / 2f;
		Vector2 glowPosition = default(Vector2);
		((Vector2)(ref glowPosition))._002Ector((float)X + center.X, (float)Y + center.Y / 1.5f);
		((Color)(ref textColor)).A = 0;
		float pulsing = 2.5f + (float)Math.Sin(time * 5f);
		for (float f = 0f; f < (float)Math.PI * 2f; f += 0.79f)
		{
			ChatManager.DrawColorCodedString(spriteBatch, font, text, new Vector2((float)X, (float)Y) + Utils.RotatedBy(new Vector2(pulsing, 0f), (double)(f + time * 2f % ((float)Math.PI * 2f)), default(Vector2)), textColor * 0.5f, rotation, origin, baseScale);
		}
		((Color)(ref textColor)).A = byte.MaxValue;
		ChatManager.DrawColorCodedStringShadow(spriteBatch, font, text, new Vector2((float)X, (float)Y), textColor * 2f, rotation, origin, baseScale);
		ColorTool.Rainbowing(time * 4f - 0.9f);
		spriteBatch.Draw(crystalTextGlow, glowPosition, (Rectangle?)null, lightColor, rotation + (float)Math.PI / 2f, new Vector2(6f, 33f), new Vector2(1.6f, fontSize.X / (float)crystalTextGlow.Height * 1.2f), (SpriteEffects)0, 0f);
		ChatManager.DrawColorCodedString(spriteBatch, font, text, new Vector2((float)X, (float)Y), Color.Black, rotation, origin, baseScale);
		if (!renderTextSparkles)
		{
			return;
		}
		UnifiedRandom rand = new UnifiedRandom(Hash((int)(center.X + center.Y)));
		int sparkleCount = rand.Next((int)fontSize.X / 7, (int)fontSize.X / 5) + 1;
		Color color2 = lightColor;
		((Color)(ref color2)).A = 0;
		Vector2 sparkleOrigin = default(Vector2);
		((Vector2)(ref sparkleOrigin))._002Ector(15f, 15f);
		Vector2 v = default(Vector2);
		for (int i = 0; i < sparkleCount; i++)
		{
			((Vector2)(ref v))._002Ector(rand.NextFloat(fontSize.X), rand.NextFloat(fontSize.Y * 0.6f) + 1f);
			float lifeTime = Main.GlobalTimeWrappedHourly * 4f + rand.NextFloat((float)Math.PI * 2f);
			lifeTime %= (float)Math.PI * 2f;
			if (!(lifeTime > (float)Math.PI * 2f))
			{
				float sinValue = (float)Math.Sin(lifeTime);
				Color white = new Color(200 + ((Color)(ref lightColor)).R / 20, 200 + ((Color)(ref lightColor)).G / 20, 200 + ((Color)(ref lightColor)).B / 20, 255) * sinValue;
				float sparkleRotationSpeed = Main.rand.NextFloat(0.8f, 1.5f);
				float sparkleRotation = time * sparkleRotationSpeed;
				spriteBatch.Draw(sparkle, new Vector2((float)X, (float)Y - lifeTime * MaxY + 3f) + v, (Rectangle?)null, white, sparkleRotation, sparkleOrigin, lifeTime / ((float)Math.PI * 2f) * 0.3f, (SpriteEffects)0, 0f);
				spriteBatch.Draw(sparkle, new Vector2((float)X, (float)Y - lifeTime * MaxY + 2f) + v, (Rectangle?)null, white * 0.5f, sparkleRotation, sparkleOrigin, lifeTime / ((float)Math.PI * 2f), (SpriteEffects)0, 0f);
				float scale2 = (float)Math.Sin(lifeTime / ((float)Math.PI / 2f)) + 1f;
				float scale3 = lifeTime / ((float)Math.PI * 2f);
				scale2 *= 0.2f;
				scale3 *= 0.15f;
				spriteBatch.Draw(sparkle, new Vector2((float)X, (float)Y - lifeTime * MaxY + 2f) + v, (Rectangle?)null, color2 * sinValue, sparkleRotation, sparkleOrigin, new Vector2(scale3, scale3) * 1.5f, (SpriteEffects)0, 0f);
				spriteBatch.Draw(sparkle, new Vector2((float)X, (float)Y - lifeTime * MaxY + 2f) + v, (Rectangle?)null, color2 * sinValue, sparkleRotation, sparkleOrigin, new Vector2(scale2, scale3), (SpriteEffects)0, 0f);
				spriteBatch.Draw(sparkle, new Vector2((float)X, (float)Y - lifeTime * MaxY + 2f) + v, (Rectangle?)null, color2 * sinValue, sparkleRotation, sparkleOrigin, new Vector2(scale3, scale2), (SpriteEffects)0, 0f);
			}
		}
		static int Hash(int x)
		{
			x ^= x >> 16;
			x *= 2146121005;
			x ^= x >> 15;
			x *= -2073254261;
			x ^= x >> 16;
			return x;
		}
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

	static CosmicPurple()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		MaxY = 4.5f;
		BloomClr = new Color(65, 38, 87, 0);
		TextClr = new Color(103, 66, 138, 255);
	}
}
