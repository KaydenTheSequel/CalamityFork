using System.IO;
using CalamityMod.Items.Tools;
using Terraria;

namespace CalamityMod.Packets;

internal sealed class DeleteAllSuperDummiesPacket : CalamityPacket
{
	public static DeleteAllSuperDummiesPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		Instance.CreateBasePacket().Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		if (Main.dedServ)
		{
			SuperDummy.DeleteDummies();
		}
	}
}
