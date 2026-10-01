using System.IO;
using CalamityMod.NPCs.TownNPCs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncAndroombaAIPacket : CalamityPacket
{
	public static SyncAndroombaAIPacket Instance { get; private set; }

	public static void Send(AndroombaFriendly roomba, int phase = -1, int toClient = -1, int ignoreClient = -1)
	{
		if (roomba != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(roomba);
			modPacket.Write((phase != -1) ? phase : ((int)roomba.NPC.ai[0]));
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		AndroombaFriendly roomba = packet.ReadModNPC<AndroombaFriendly>();
		int phase = packet.ReadInt32();
		if (roomba != null && Main.dedServ)
		{
			AndroombaFriendly.ChangeAI(roomba.NPC.whoAmI, phase);
		}
	}
}
