using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.UI.DialogueDisplay.TextEffects;

internal class Shaking : TextEffect
{
	private const float StandardAmp = 2f;

	public override Vector2 ModifyPos(Vector2 pos, DialogueCharacterData data, float[] args)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		float amp = 2f;
		if (args.Length != 0)
		{
			amp = args[0];
		}
		return pos + Main.rand.NextVector2Circular(amp, amp);
	}
}
