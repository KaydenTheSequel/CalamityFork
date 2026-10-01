using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncNPCMotionDataToServerPacket : CalamityPacket
{
	public static SyncNPCMotionDataToServerPacket Instance { get; private set; }

	public static void Send(NPC npc, int toClient = -1, int ignoreClient = -1)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (npc != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(npc);
			modPacket.WriteVector2(npc.Center);
			modPacket.WriteVector2(npc.velocity);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = packet.ReadNPC();
		Vector2 center = packet.ReadVector2();
		Vector2 velocity = packet.ReadVector2();
		if (Main.dedServ && npc != null)
		{
			npc.Center = center;
			npc.velocity = velocity;
			NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
		}
	}
}
