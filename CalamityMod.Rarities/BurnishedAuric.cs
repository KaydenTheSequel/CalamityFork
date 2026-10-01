using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CalamityMod.Rarities;

public class BurnishedAuric : ModRarity
{
	public static float MaxY;

	public static Color BloomClr;

	public static Color TextClr;

	private static float lastFlashTime;

	private static bool isFlashing;

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
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		float flashChance = 0.005f;
		float flashDuration = 0.2f;
		if ((float)Main.GameUpdateCount - lastFlashTime > flashDuration * 60f)
		{
			if (Main.rand.NextFloat() < flashChance)
			{
				isFlashing = true;
				lastFlashTime = Main.GameUpdateCount;
			}
			else
			{
				isFlashing = false;
			}
		}
		Vector2 fontSize = font.MeasureString(text);
		Vector2 center = fontSize / 2f;
		if (Item.expert)
		{
			textColor = Main.DiscoColor;
		}
		new Vector2((float)X + center.X, (float)Y + center.Y / 1.5f);
		((Color)(ref textColor)).A = 0;
		float pulsing = 1.5f + (float)Math.Sin(time * 5f);
		for (float f = 0f; f < (float)Math.PI * 2f; f += 0.79f)
		{
			ChatManager.DrawColorCodedString(spriteBatch, font, text, new Vector2((float)X, (float)Y) + Utils.RotatedBy(new Vector2(pulsing, 0f), (double)(f + time * 2f % ((float)Math.PI * 2f)), default(Vector2)), textColor * 0.5f, rotation, origin, baseScale);
			if (isFlashing && CalamityClientConfig.Instance.TextEffects)
			{
				origin += Main.rand.NextVector2Circular(2f, 1.2f);
			}
		}
		if (isFlashing)
		{
			((Color)(ref textColor))._002Ector(0, 183, 241, 50);
			((Color)(ref lightColor))._002Ector(14, 255, 255, 50);
		}
		((Color)(ref textColor)).A = byte.MaxValue;
		ChatManager.DrawColorCodedStringShadow(spriteBatch, font, text, new Vector2((float)X, (float)Y), textColor * 2f, rotation, origin, baseScale);
		ChatManager.DrawColorCodedString(spriteBatch, font, text, new Vector2((float)X, (float)Y), new Color(77, 0, 33), rotation, origin, baseScale);
		float shineWidth = 40f;
		float shineSpeed = 80f;
		float shinePos = time * shineSpeed % (fontSize.X + shineWidth);
		Vector2 basePos = default(Vector2);
		((Vector2)(ref basePos))._002Ector((float)X, (float)Y);
		if (isFlashing && CalamityClientConfig.Instance.TextEffects)
		{
			basePos += Main.rand.NextVector2Circular(3f, 10f);
		}
		float charOffsetX = 0f;
		if (!renderTextSparkles)
		{
			return;
		}
		for (int i = 0; i < text.Length; i++)
		{
			string c = text[i].ToString();
			Vector2 charSize = font.MeasureString(c);
			_ = charSize * 0.5f;
			Vector2 charPos = basePos + new Vector2(charOffsetX, 0f);
			float dist = Math.Abs(charPos.X + charSize.X * baseScale.X / 2f + 10.5f - ((float)X + shinePos - shineWidth * 0.15f));
			float intensity = 1f - MathHelper.Clamp(dist / shineWidth, 0f, 1f);
			if (intensity > 0f)
			{
				Color shineColor = (isFlashing ? (new Color(90, 207, 255) * intensity * 2f) : (new Color(254, 231, 117) * intensity * 1f));
				ChatManager.DrawColorCodedString(spriteBatch, font, c, charPos, shineColor, rotation, origin, baseScale);
			}
			charOffsetX += charSize.X - (float)text.Length * 0.0085f;
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

	static BurnishedAuric()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		MaxY = 4.5f;
		BloomClr = new Color(48, 33, 4, 0);
		TextClr = new Color(157, 110, 11, 50);
		lastFlashTime = 0f;
		isFlashing = false;
	}
}
