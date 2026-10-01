using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.UI.DialogueDisplay.TextEffects;

public abstract class TextEffect
{
	public virtual Vector2 ModifyPos(Vector2 pos, DialogueCharacterData data, float[] args)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return pos;
	}

	public virtual float ModifyRot(float rot, DialogueCharacterData data, float[] args)
	{
		return rot;
	}

	public virtual Color ModifyColor(Color current, DialogueCharacterData data, float[] args)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return current;
	}

	public virtual Vector2 ModifyScale(Vector2 scale, DialogueCharacterData data, float[] args)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return scale;
	}

	public virtual void PreDraw(SpriteBatch spritebatch, Texture2D texture, DialogueCharacterData data)
	{
	}

	public virtual void PostDraw(SpriteBatch spritebatch, Texture2D texture, DialogueCharacterData data)
	{
	}
}
