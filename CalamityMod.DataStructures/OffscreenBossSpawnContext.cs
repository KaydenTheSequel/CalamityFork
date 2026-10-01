using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.DataStructures;

public class OffscreenBossSpawnContext : BaseBossSpawnContext
{
	public override Vector2 DetermineSpawnPosition(Vector2 relativePosition)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		return relativePosition + Main.rand.NextVector2CircularEdge(Main.screenWidth, Main.screenHeight) * (float)Math.Sqrt(2.0) * 1.1f * 0.5f;
	}
}
