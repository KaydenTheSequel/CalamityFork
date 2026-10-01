using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.DialogueDisplay.DisplayEffects;

public class BossText : DisplayEffect
{
	public override bool FadeWhenTooFar => false;

	public override float TimeToAppear => 20f;

	public override bool DespawnWithAttachedNPC => false;

	public override Vector2 TextOffsetFromStart(Vector2 startPos, Vector2 textSize)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = Main.LocalPlayer.Center;
		Vector2 halfSize = textSize * 0.5f;
		return center - halfSize + Vector2.UnitY * (textSize.Y + 54f);
	}

	private float OffsetAppearTime(float time, float ratio)
	{
		return MathHelper.Clamp((time - ratio * TimeToAppear / 6f) / (TimeToAppear / 6f), 0f, 1f);
	}

	public override Vector2 AppearPositioning(Vector2 startPos, Vector2 goalPos, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		(goalPos - startPos).SafeNormalize(-Vector2.UnitX);
		return Vector2.Lerp(goalPos - new Vector2(-1f, -1f) * 24f * charData.Scale, goalPos, CalamityUtils.SineOutEasing(time / TimeToAppear, 1));
	}

	public override float AppearOpacity(float goalOpacity, float time, DialogueCharacterData charData)
	{
		return CalamityUtils.SineOutEasing(time / TimeToAppear, 1);
	}

	public override Vector2 AppearScale(Vector2 goalScale, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(goalScale * 0.75f, goalScale, CalamityUtils.ExpOutEasing(time / TimeToAppear, 1));
	}

	private float OffsetDisappearTime(float time, float ratio)
	{
		return MathHelper.Clamp((time - ratio * TimeToDisappear / 2f) / (TimeToDisappear / 2f), 0f, 1f);
	}

	public override Vector2 DisappearPositioning(Vector2 startPos, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(startPos, startPos + Vector2.unitXVector * 12f * charData.Scale, CalamityUtils.SineOutEasing(OffsetDisappearTime(time, charData.CompletionRatio), 1));
	}

	public override float DisappearOpacity(float startOpacity, float time, DialogueCharacterData charData)
	{
		return 1f - CalamityUtils.SineOutEasing(OffsetDisappearTime(time, charData.CompletionRatio), 1);
	}

	public override Vector2 DisappearScale(Vector2 startScale, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(startScale, startScale * 0.75f, CalamityUtils.ExpOutEasing(OffsetDisappearTime(time, charData.CompletionRatio), 1));
	}

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
