using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.UI.DialogueDisplay.DisplayEffects;

public class BuiltEffect : DisplayEffect
{
	public override Vector2 AppearPositioning(Vector2 startPos, Vector2 goalPos, float time, DialogueCharacterData charData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(goalPos + Vector2.UnitX.RotatedBy(charData.Index) * 400f, goalPos, time / TimeToAppear);
	}

	public override float AppearRotation(float goalRotation, float time, DialogueCharacterData charData)
	{
		return MathHelper.Lerp(goalRotation + (float)Math.PI * 4f, goalRotation, time / TimeToAppear);
	}
}
