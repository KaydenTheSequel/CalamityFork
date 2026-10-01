using Microsoft.Xna.Framework;

namespace CalamityMod.Graphics.Metaballs;

public class CyanPaint : PaintMetaball
{
	public override Color EdgeColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return Color.Cyan * 0.7f;
		}
	}
}
