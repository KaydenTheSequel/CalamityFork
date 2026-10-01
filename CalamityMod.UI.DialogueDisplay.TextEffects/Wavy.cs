using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.UI.DialogueDisplay.TextEffects;

public class Wavy : TextEffect
{
	private const float StandardAmp = 6f;

	private const float StandardFreq = 1.5f;

	private const float StandardOffsetFactor = (float)Math.PI / 160f;

	public override Vector2 ModifyPos(Vector2 pos, DialogueCharacterData data, float[] args)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		float amp = 6f;
		if (args.Length != 0)
		{
			amp = args[0];
		}
		float freq = 1.5f;
		if (args.Length > 1)
		{
			freq = args[1];
		}
		float indexFactor = (float)Math.PI / 160f;
		if (args.Length > 2)
		{
			indexFactor = args[2];
		}
		float sineWave = (float)Math.Sin(Main.GlobalTimeWrappedHourly * freq + data.TextPosition.X * indexFactor) * amp;
		return pos + Vector2.UnitY * sineWave;
	}
}
