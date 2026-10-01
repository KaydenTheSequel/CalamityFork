using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonLogs;

public abstract class DraedonsLogGUI : PopupGUI
{
	public int Page;

	public int ArrowClickCooldown;

	public bool HoveringOverBook;

	public const int TextStartOffsetX = 40;

	public int TotalLinesPerPage => 16;

	public abstract int TotalPages { get; }

	public override void Update()
	{
		if (Active)
		{
			if (FadeTime < FadeTimeMax)
			{
				FadeTime++;
			}
		}
		else if (FadeTime > 0)
		{
			FadeTime--;
		}
		if (Main.mouseLeft && !HoveringOverBook && FadeTime >= 30)
		{
			Page = 0;
			Active = false;
		}
		if (ArrowClickCooldown > 0)
		{
			ArrowClickCooldown--;
		}
		HoveringOverBook = false;
	}

	public abstract string GetTextByPage();

	public abstract Texture2D GetTextureByPage();

	public override void Draw(SpriteBatch spriteBatch)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D pageTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogPage", (AssetRequestMode)2).Value;
		float xScale = MathHelper.Lerp(0.004f, 1f, (float)FadeTime / (float)FadeTimeMax);
		Vector2 scale = new Vector2(xScale, 1f) * new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / pageTexture.Size();
		scale.Y *= 1.5f;
		scale *= 0.5f;
		float xResolutionScale = (float)Main.screenWidth / 2560f;
		float yResolutionScale = (float)Main.screenHeight / 1440f;
		float bookScale = 0.5f;
		scale *= bookScale;
		float yPageTop = MathHelper.Lerp((float)(Main.screenHeight * 2), (float)Main.screenHeight * 0.25f, (float)FadeTime / (float)FadeTimeMax);
		Rectangle mouseRectangle = default(Rectangle);
		((Rectangle)(ref mouseRectangle))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 2, 2);
		float drawPositionX = (float)Main.screenWidth * 0.5f;
		Vector2 drawPosition = default(Vector2);
		((Vector2)(ref drawPosition))._002Ector(drawPositionX, yPageTop);
		Rectangle pageRectangle = default(Rectangle);
		((Rectangle)(ref pageRectangle))._002Ector((int)drawPosition.X - (int)((float)pageTexture.Width * scale.X), (int)yPageTop, (int)((float)pageTexture.Width * scale.X) * 2, (int)((float)pageTexture.Height * scale.Y));
		for (int i = 0; i < 2; i++)
		{
			spriteBatch.Draw(pageTexture, drawPosition, (Rectangle?)null, Color.White, 0f, new Vector2(((float)i == 0f) ? ((float)pageTexture.Width) : 0f, 0f), scale, (SpriteEffects)((i != 0) ? 1 : 0), 0f);
			if (!HoveringOverBook)
			{
				HoveringOverBook = ((Rectangle)(ref mouseRectangle)).Intersects(pageRectangle);
			}
		}
		if (FadeTime < FadeTimeMax - 4 || !Active)
		{
			return;
		}
		int textWidth = (int)(xScale * (float)pageTexture.Width) - 40;
		textWidth = (int)((float)textWidth * xResolutionScale);
		List<string> dialogLines = Utils.WordwrapString(GetTextByPage(), FontAssets.MouseText.Value, (int)((float)textWidth / xResolutionScale), 250, out var _).ToList();
		dialogLines.RemoveAll((string text) => string.IsNullOrEmpty(text));
		_ = string.Concat(dialogLines).Length;
		float yOffsetPerLine = 28f;
		if (dialogLines.Count > TotalLinesPerPage)
		{
			yOffsetPerLine *= (float)(string.Concat(dialogLines).Length / GetTextByPage().Length);
		}
		if (Page < 0)
		{
			Page = 0;
		}
		if (Page > TotalPages)
		{
			Page = TotalPages;
		}
		DrawArrows(spriteBatch, xResolutionScale, yResolutionScale, yPageTop + 506f * yResolutionScale, mouseRectangle);
		int textDrawPositionX = (int)((float)pageTexture.Width * xResolutionScale + 350f * xResolutionScale);
		int yScale = (int)(42f * yResolutionScale);
		int yScale2 = (int)(yOffsetPerLine * yResolutionScale);
		for (int i2 = 0; i2 < dialogLines.Count; i2++)
		{
			if (dialogLines[i2] != null)
			{
				int textDrawPositionY = yScale + i2 * yScale2 + (int)yPageTop;
				Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, dialogLines[i2], textDrawPositionX, textDrawPositionY, Color.DarkCyan, Color.Black, Vector2.Zero, xResolutionScale);
			}
		}
		DrawSpecialImage(spriteBatch, xResolutionScale, yResolutionScale, yPageTop - 70f * yResolutionScale);
	}

	public void DrawArrows(SpriteBatch spriteBatch, float xResolutionScale, float yResolutionScale, float yPageBottom, Rectangle mouseRectangle)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		float arrowScale = 0.6f;
		Texture2D arrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogArrow", (AssetRequestMode)2).Value;
		if (Page > 0)
		{
			Vector2 drawPosition = default(Vector2);
			((Vector2)(ref drawPosition))._002Ector((float)(Main.screenWidth / 2) - 80f, yPageBottom);
			Rectangle arrowRectangle = default(Rectangle);
			((Rectangle)(ref arrowRectangle))._002Ector((int)drawPosition.X, (int)drawPosition.Y, arrowTexture.Width, arrowTexture.Height);
			arrowRectangle.Width = (int)((float)arrowRectangle.Width * xResolutionScale * arrowScale);
			arrowRectangle.Height = (int)((float)arrowRectangle.Height * yResolutionScale * arrowScale);
			if (((Rectangle)(ref mouseRectangle)).Intersects(arrowRectangle))
			{
				arrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogArrowHover", (AssetRequestMode)2).Value;
				if (ArrowClickCooldown <= 0 && Main.mouseLeft)
				{
					Page--;
					ArrowClickCooldown = 8;
				}
				Main.blockMouse = true;
			}
			spriteBatch.Draw(arrowTexture, drawPosition, (Rectangle?)null, Color.White, 0f, Vector2.Zero, new Vector2(xResolutionScale, yResolutionScale) * arrowScale, (SpriteEffects)1, 0f);
		}
		if (Page >= TotalPages - 1)
		{
			return;
		}
		Vector2 drawPosition2 = default(Vector2);
		((Vector2)(ref drawPosition2))._002Ector((float)(Main.screenWidth / 2) + 40f, yPageBottom);
		Rectangle arrowRectangle2 = default(Rectangle);
		((Rectangle)(ref arrowRectangle2))._002Ector((int)drawPosition2.X, (int)drawPosition2.Y, arrowTexture.Width, arrowTexture.Height);
		arrowRectangle2.Width = (int)((float)arrowRectangle2.Width * xResolutionScale);
		arrowRectangle2.Height = (int)((float)arrowRectangle2.Height * yResolutionScale);
		if (((Rectangle)(ref mouseRectangle)).Intersects(arrowRectangle2))
		{
			arrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogArrowHover", (AssetRequestMode)2).Value;
			if (ArrowClickCooldown <= 0 && Main.mouseLeft)
			{
				Page++;
				ArrowClickCooldown = 8;
			}
			Main.blockMouse = true;
		}
		spriteBatch.Draw(arrowTexture, drawPosition2, (Rectangle?)null, Color.White, 0f, Vector2.Zero, new Vector2(xResolutionScale, yResolutionScale) * arrowScale, (SpriteEffects)0, 0f);
	}

	public void DrawSpecialImage(SpriteBatch spriteBatch, float xResolutionScale, float yResolutionScale, float yPageTop)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = GetTextureByPage();
		if (texture != null)
		{
			float yAspectRatio = (float)texture.Height / (float)texture.Width;
			Vector2 drawPosition = default(Vector2);
			drawPosition.X = Main.screenWidth / 2 + (int)(285f * xResolutionScale);
			drawPosition.Y = yPageTop;
			Vector2 scale = default(Vector2);
			((Vector2)(ref scale))._002Ector(360f * xResolutionScale, MathHelper.Clamp(360f * yAspectRatio, 330f, 400f) * yResolutionScale);
			scale /= texture.Size();
			drawPosition.Y += (float)texture.Height * 0.4f * scale.Y;
			spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, Color.White, 0f, new Vector2((float)texture.Width * 0.5f, 0f), scale, (SpriteEffects)0, 0f);
		}
	}
}
