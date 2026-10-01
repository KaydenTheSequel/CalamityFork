using Microsoft.Xna.Framework;

namespace CalamityMod.Graphics.Metaballs;

public class BluePaint : PaintMetaball
{
	public override Color EdgeColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return Color.Blue * 0.7f;
		}
	}
}
