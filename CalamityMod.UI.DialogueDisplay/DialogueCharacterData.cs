using Microsoft.Xna.Framework;

namespace CalamityMod.UI.DialogueDisplay;

public class DialogueCharacterData
{
	public int Timer;

	public int Index;

	public int TextLength;

	public int LineNumber;

	public Vector2 TextPosition;

	public Vector2 DrawPosition;

	public Rectangle Frame;

	public Color DrawColor;

	public float Rotation;

	public Vector2 Scale;

	public float CompletionRatio => (float)Index / (float)TextLength;

	public DialogueCharacterData(int index, int textLength, int lineNumber)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		Index = index;
		TextLength = textLength;
		LineNumber = lineNumber;
		TextPosition = Vector2.Zero;
		base._002Ector();
	}

	internal void SetDrawInfo(Vector2 drawPos, Rectangle frame, Color color, float rotation, Vector2 scale)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		DrawPosition = drawPos;
		Frame = frame;
		DrawColor = color;
		Rotation = rotation;
		Scale = scale;
	}
}
