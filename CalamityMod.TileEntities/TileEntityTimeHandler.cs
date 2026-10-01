using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.TileEntities;

public static class TileEntityTimeHandler
{
	public static void Update()
	{
		MultiplayerClientUpdateVisuals();
	}

	private static void MultiplayerClientUpdateVisuals()
	{
		if (Main.netMode != 1)
		{
			return;
		}
		int factoryType = ModContent.GetInstance<TEPowerCellFactory>().Type;
		int chargerType = ModContent.GetInstance<TEChargingStation>().Type;
		int codebreakerType = ModContent.GetInstance<TECodebreaker>().Type;
		Dictionary<int, TileEntity>.ValueCollection.Enumerator enumerator = TileEntity.ByID.Values.GetEnumerator();
		do
		{
			TileEntity te = enumerator.Current;
			if (te == null)
			{
				continue;
			}
			if (te.type == factoryType)
			{
				((TEPowerCellFactory)te).Time++;
			}
			else if (te.type == chargerType)
			{
				TEChargingStation charger = (TEChargingStation)te;
				if (charger.ClientChargingDust && charger.CanDoWork)
				{
					charger.ClientChargingDust = false;
					charger.SpawnChargingDust();
				}
			}
			else if (te is TEBaseTurret turret)
			{
				turret.UpdateClient();
				turret.UpdateAngle();
			}
			else if (te.type == codebreakerType)
			{
				((TECodebreaker)te).UpdateTime();
			}
		}
		while (enumerator.MoveNext());
	}
}
