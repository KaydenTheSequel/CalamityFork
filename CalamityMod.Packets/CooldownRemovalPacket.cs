using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class CooldownRemovalPacket : CalamityPacket
{
	public static CooldownRemovalPacket Instance { get; private set; }

	public static void Send(CalamityPlayer player, ushort[] netIDsToRemove, int toClient = -1, int ignoreClient = -1)
	{
		if (player != null)
		{
			ModPacket packet = Instance.CreateBasePacket();
			packet.WriteWhoAmI(player);
			packet.Write(netIDsToRemove.Length);
			for (int i = 0; i < netIDsToRemove.Length; i++)
			{
				packet.Write(netIDsToRemove[i]);
			}
			packet.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityPlayer player = packet.ReadCalamityPlayer();
		int count = packet.ReadInt32();
		ushort[] netIDsToRemove = new ushort[count];
		for (int i = 0; i < count; i++)
		{
			netIDsToRemove[i] = packet.ReadUInt16();
		}
		if (player == null)
		{
			return;
		}
		if (Main.netMode == 1)
		{
			ushort[] array = netIDsToRemove;
			foreach (ushort netID in array)
			{
				player.cooldowns.Remove(CooldownRegistry.registry[netID].ID);
			}
		}
		else if (Main.dedServ)
		{
			Send(player, netIDsToRemove, -1, sender);
		}
	}
}
