using Microsoft.Xna.Framework;

namespace CalamityMod.Graphics.Metaballs;

public class DragonsBreathBeamMetaball2 : DragonsBreathMetaball2
{
	private static float whiteSizeThreshold = 72f;

	public override Color EdgeColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return Color.OrangeRed * 1.2f;
		}
	}

	public override void DrawInstances()
	{
		DrawInstancesInternal(0.4f, whiteSizeThreshold);
	}
}
