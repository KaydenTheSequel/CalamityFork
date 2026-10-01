using Microsoft.Xna.Framework;

namespace CalamityMod.Gores.WaterDroplet;

public class CragsLavaDroplet : LiquidDropletGore
{
	public override bool lavaDroplet => true;

	public override Vector3 lavaColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Vector3(2.5f, 1.1f, 0.1f);
		}
	}
}
