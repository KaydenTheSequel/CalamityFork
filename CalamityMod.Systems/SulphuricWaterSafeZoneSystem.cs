using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class SulphuricWaterSafeZoneSystem : ModSystem
{
	public static Dictionary<Point, float> NearbySafeTiles { get; set; } = new Dictionary<Point, float>();

	public override void PreUpdateEntities()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		foreach (Point key2 in NearbySafeTiles.Keys)
		{
			NearbySafeTiles[key2] -= 0.06f;
		}
		foreach (Point key in NearbySafeTiles.Keys.Where(delegate(Point key2)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			return NearbySafeTiles[key2] <= 0f;
		}))
		{
			NearbySafeTiles.Remove(key);
		}
	}
}
