using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.UI.DialogueDisplay.DisplayEffects;

public class DisplayEffect
{
	public virtual bool FadeWhenTooFar => true;

	public virtual bool DespawnWithAttachedNPC => true;

	public virtual float FadeBuffer => 150f;

	public virtual float FadeDistance => 150f;

	public virtual float TimeToAppear => 30f;

	public virtual float TimeToDisappear => 30f;

	public virtual Vector2 TextOffsetFromStart(Vector2 startPos, Vector2 textSize)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return startPos + new Vector2((0f - textSize.X) / 2f, 0f - (textSize.Y + 40f));
	}

	public virtual void PreDraw(SpriteBatch spriteBatch, Vector2 textStart, Vector2 textSize, int textTimer, int switchTimer)
	{
	}

	public virtual void PostDraw(SpriteBatch spriteBatch, Vector2 textStart, Vector2 textSize, int textTimer, int switchTimer)
	{
	}

	public virtual Vector2 AppearPositioning(Vector2 startPos, Vector2 goalPos, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(startPos, goalPos, CalamityUtils.SineOutEasing(time / TimeToAppear, 1));
	}

	public virtual Color AppearColoring(Color goalColor, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return goalColor;
	}

	public virtual float AppearOpacity(float goalOpacity, float time, DialogueCharacterData charData)
	{
		return CalamityUtils.SineOutEasing(MathHelper.Clamp(time / 20f, 0f, 1f), 1);
	}

	public virtual float AppearRotation(float goalRotation, float time, DialogueCharacterData charData)
	{
		return goalRotation;
	}

	public virtual Vector2 AppearScale(Vector2 goalScale, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(Vector2.Zero, goalScale, CalamityUtils.CircOutEasing(time / TimeToAppear, 1));
	}

	public virtual Vector2 DisappearPositioning(Vector2 startPos, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return startPos;
	}

	public virtual Color DisappearColoring(Color startColor, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return startColor;
	}

	public virtual float DisappearOpacity(float startOpacity, float time, DialogueCharacterData charData)
	{
		return 1f - CalamityUtils.SineOutEasing(MathHelper.Clamp(time / (TimeToDisappear * 0.66f), 0f, 1f), 1);
	}

	public virtual float DisappearRotation(float startRotation, float time, DialogueCharacterData charData)
	{
		return startRotation;
	}

	public virtual Vector2 DisappearScale(Vector2 startScale, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(startScale, startScale * 1.5f, CalamityUtils.ExpOutEasing(time / TimeToDisappear, 1));
	}
}
