using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncNPCPosAndRotOnlyPacket : CalamityPacket
{
	public static SyncNPCPosAndRotOnlyPacket Instance { get; private set; }

	public static void Send(NPC npc, int toClient = -1, int ignoreClient = -1)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (npc != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(npc);
			modPacket.WriteVector2(npc.position);
			modPacket.Write((Half)npc.rotation);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = packet.ReadNPC();
		Vector2 position = packet.ReadVector2();
		float rotation = (float)packet.ReadHalf();
		if (npc != null)
		{
			npc.position = position;
			npc.rotation = rotation;
			if (Main.dedServ)
			{
				Send(npc, -1, sender);
			}
		}
	}
}
