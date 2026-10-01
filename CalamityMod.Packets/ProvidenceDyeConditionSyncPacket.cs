using System.IO;
using CalamityMod.NPCs.Providence;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class ProvidenceDyeConditionSyncPacket : CalamityPacket
{
	public static ProvidenceDyeConditionSyncPacket Instance { get; private set; }

	public static void Send(Providence providence, int toClient = -1, int ignoreClient = -1)
	{
		if (providence != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(providence);
			modPacket.Write(providence.hasBeenGivenFullPower);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		Providence providence = packet.ReadModNPC<Providence>();
		bool hasBeenEnraged = packet.ReadBoolean();
		if (providence != null)
		{
			providence.hasBeenGivenFullPower = hasBeenEnraged;
		}
	}
}
