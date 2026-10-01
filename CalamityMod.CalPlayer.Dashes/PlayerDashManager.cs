using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public sealed class PlayerDashManager : ModSystem
{
	internal static Dictionary<string, PlayerDashEffect> DashIdentificationTable = new Dictionary<string, PlayerDashEffect>();

	public static bool FindByID(string id, out PlayerDashEffect dashEffect)
	{
		return DashIdentificationTable.TryGetValue(id, out dashEffect);
	}

	public static void TryAddDash(PlayerDashEffect dashEffect)
	{
		if (DashIdentificationTable != null && !dashEffect.GetType().IsAbstract)
		{
			string id = dashEffect.DashID;
			if (!DashIdentificationTable.ContainsKey(id) && !string.IsNullOrEmpty(id))
			{
				DashIdentificationTable[id] = dashEffect;
			}
		}
	}

	public override void Unload()
	{
		DashIdentificationTable = null;
	}
}
