using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.DialogueDisplay.DisplayEffects;

public class WhisperingPearlEffects : DisplayEffect
{
	public override void PreDraw(SpriteBatch spriteBatch, Vector2 textStart, Vector2 textSize, int textTimer, int switchTimer)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		if (textTimer >= 0)
		{
			float Opacity = 1f;
			if ((float)textTimer < 30f)
			{
				Opacity = MathHelper.Lerp(0f, 1f, CalamityUtils.CircOutEasing((float)textTimer / 30f, 1));
			}
			if (switchTimer > 0)
			{
				Opacity *= 1f - CalamityUtils.CircOutEasing((float)switchTimer / 60f, 1);
			}
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Particles/SmallBloom", (AssetRequestMode)2).Value;
			spriteBatch.Draw(tex, textStart + textSize * 0.5f - Main.screenPosition, (Rectangle?)null, Color.Black * 0.6f * Opacity, 0f, tex.Size() * 0.5f, new Vector2(textSize.X / 160f, textSize.Y / 120f), (SpriteEffects)0, 0f);
		}
	}
}
