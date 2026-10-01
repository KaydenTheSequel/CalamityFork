using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.DialogueDisplay.DisplayEffects;

public class AlwaysOnScreen : DisplayEffect
{
	private Vector2 StartPosition;

	public override bool FadeWhenTooFar => false;

	public override Vector2 TextOffsetFromStart(Vector2 startPos, Vector2 textSize)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		StartPosition = startPos;
		Vector2 playerPos = Main.LocalPlayer.Center;
		Vector2 halfSize = textSize * 0.5f;
		Vector2 newPos = startPos - halfSize + Vector2.UnitY * (0f - (textSize.Y + 36f));
		Vector2 val = newPos.ToScreenPosition();
		Vector2 boundTopLeftScreen = default(Vector2);
		((Vector2)(ref boundTopLeftScreen))._002Ector((float)Main.screenWidth / 2f - (float)Main.screenWidth / 2.5f, (float)Main.screenHeight / 2f - (float)Main.screenHeight / 2.5f);
		if (val.X < boundTopLeftScreen.X)
		{
			newPos.X = playerPos.X - (float)Main.screenWidth / 2.5f;
		}
		if (val.Y < boundTopLeftScreen.Y)
		{
			newPos.Y = playerPos.Y - (float)Main.screenHeight / 2.5f;
		}
		if (newPos.X > playerPos.X + (float)Main.screenWidth / 2.5f - textSize.X)
		{
			newPos.X = playerPos.X + (float)Main.screenWidth / 2.5f - textSize.X;
		}
		if (newPos.Y > playerPos.Y + (float)Main.screenHeight / 2.5f - textSize.Y)
		{
			newPos.Y = playerPos.Y + (float)Main.screenHeight / 2.5f - textSize.Y;
		}
		return newPos;
	}

	public override void PreDraw(SpriteBatch spriteBatch, Vector2 textTopLeft, Vector2 textSize, int textTimer, int switchTimer)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/UI/DialogueDisplay/Assets/DialogueArrow", (AssetRequestMode)2).Value;
		Vector2 textCenter = textTopLeft + textSize * 0.5f;
		Vector2 toStart = (StartPosition - textCenter).SafeNormalize(-Vector2.UnitY) * 64f;
		spriteBatch.Draw(tex, textCenter + toStart - Main.screenPosition, (Rectangle?)null, Color.White, toStart.ToRotation(), tex.Size() * 0.5f, 1f, (SpriteEffects)0, 0f);
	}
}
