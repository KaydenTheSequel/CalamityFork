using System.IO;
using CalamityMod.NPCs.TownNPCs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncAndroombaSolutionPacket : CalamityPacket
{
	public static SyncAndroombaSolutionPacket Instance { get; private set; }

	public static void Send(AndroombaFriendly roomba, int solType = -1, int toClient = -1, int ignoreClient = -1)
	{
		if (roomba != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(roomba);
			modPacket.Write((solType != -1) ? solType : ((int)roomba.NPC.ai[3]));
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		AndroombaFriendly roomba = packet.ReadModNPC<AndroombaFriendly>();
		int solution = packet.ReadInt32();
		if (roomba != null && Main.dedServ)
		{
			AndroombaFriendly.SwapSolution(roomba.NPC.whoAmI, solution);
		}
	}
}
