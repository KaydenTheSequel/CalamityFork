using System.IO;
using CalamityMod.World;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class ExoMechSelectionPacket : CalamityPacket
{
	public static ExoMechSelectionPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write((int)CalamityWorld.DraedonMechToSummon);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityWorld.DraedonMechToSummon = (ExoMech)packet.ReadInt32();
	}
}
