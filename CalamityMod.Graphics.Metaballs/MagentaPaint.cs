using Microsoft.Xna.Framework;

namespace CalamityMod.Graphics.Metaballs;

public class MagentaPaint : PaintMetaball
{
	public override Color EdgeColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return Color.Magenta * 0.7f;
		}
	}
}
