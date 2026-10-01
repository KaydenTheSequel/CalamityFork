using System.IO;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class TETurretPacket : CalamityPacket
{
	public static TETurretPacket Instance { get; private set; }

	public static void Send(TEBaseTurret turret, int toClient = -1, int ignoreClient = -1)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (turret != null)
		{
			ModPacket packet = Instance.CreateBasePacket();
			packet.WriteTileEntityID(turret);
			packet.Write(turret.FiringTime);
			packet.Write(turret.Angle);
			packet.WriteVector2(turret.TargetPos);
			turret.WriteExtraTurretData(packet);
			packet.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		TEBaseTurret turret = packet.ReadTileEntity<TEBaseTurret>();
		int firingTime = packet.ReadInt32();
		float angle = packet.ReadSingle();
		Vector2 targetVec = packet.ReadVector2();
		if (turret != null)
		{
			turret.FiringTime = firingTime;
			turret.Angle = angle;
			turret.TargetPos = targetVec;
			turret.ReadExtraTurretData(packet);
		}
		else
		{
			packet.ReadBytes(16);
		}
	}
}
