using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonSummoning;

public class CodebreakerUIScroller
{
	public bool IsBeingDragged { get; set; }

	public float PositionYInterpolant { get; set; }

	public void Draw(float top, float bottom, float x, float scale, float opacity)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		Color scrollerColor = Color.White;
		Texture2D scrollerTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/Scroller", (AssetRequestMode)2).Value;
		top -= (float)scrollerTexture.Height * scale * 0.5f;
		bottom += (float)scrollerTexture.Height * scale * 0.5f;
		Vector2 scrollerPosition = default(Vector2);
		((Vector2)(ref scrollerPosition))._002Ector(x, MathHelper.Lerp(top, bottom, PositionYInterpolant));
		Rectangle scrollerArea = Utils.CenteredRectangle(scrollerPosition, scrollerTexture.Size() * scale);
		Rectangle mouseArea = default(Rectangle);
		((Rectangle)(ref mouseArea))._002Ector((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 2, 2);
		if (Main.mouseLeft && ((Rectangle)(ref scrollerArea)).Intersects(mouseArea))
		{
			IsBeingDragged = true;
		}
		bool releaseDown = Main.mouseLeftRelease;
		if (IsBeingDragged & releaseDown)
		{
			IsBeingDragged = false;
		}
		if (IsBeingDragged)
		{
			PositionYInterpolant = Utils.GetLerpValue(top, bottom, Main.MouseScreen.Y, clamped: true);
		}
		if (IsBeingDragged)
		{
			scrollerColor = Color.DarkSlateGray;
		}
		Vector2 scrollerOrigin = scrollerTexture.Size() * 0.5f;
		Main.spriteBatch.Draw(scrollerTexture, scrollerPosition, (Rectangle?)null, scrollerColor * opacity, 0f, scrollerOrigin, scale, (SpriteEffects)0, 0f);
	}

	public void Reset()
	{
		IsBeingDragged = false;
		PositionYInterpolant = 0f;
	}
}
