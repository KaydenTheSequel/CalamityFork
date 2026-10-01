using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class CooldownAdditionPacket : CalamityPacket
{
	public static CooldownAdditionPacket Instance { get; private set; }

	public static void Send(CalamityPlayer player, CooldownInstance cd, int toClient = -1, int ignoreClient = -1)
	{
		Send(player, cd.netID, cd.duration, cd.timeLeft, toClient, ignoreClient);
	}

	public static void Send(CalamityPlayer player, ushort netID, int duration, int timeLeft, int toClient = -1, int ignoreClient = -1)
	{
		if (player != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(player);
			modPacket.Write(netID);
			modPacket.Write(duration);
			modPacket.Write(timeLeft);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityPlayer player = packet.ReadCalamityPlayer();
		ushort netID = packet.ReadUInt16();
		int duration = packet.ReadInt32();
		int timeLeft = packet.ReadInt32();
		if (player != null)
		{
			if (Main.netMode == 1)
			{
				CooldownInstance instance = new CooldownInstance(player.Player, netID, duration, timeLeft);
				string id = CooldownRegistry.registry[instance.netID].ID;
				player.cooldowns[id] = instance;
			}
			else if (Main.dedServ)
			{
				Send(player, netID, duration, timeLeft, -1, sender);
			}
		}
	}
}
