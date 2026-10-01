using Microsoft.Xna.Framework;

namespace CalamityMod.Graphics.Metaballs;

public class DragonsBreathBeamMetaball : DragonsBreathMetaball
{
	private static float whiteSizeThreshold = 72f;

	public override Color EdgeColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Orange;
		}
	}

	public override void DrawInstances()
	{
		DrawInstancesInternal(0.4f, whiteSizeThreshold);
	}
}
