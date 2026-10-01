using System.IO;
using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class DefenseDamageSyncPacket : CalamityPacket
{
	public static DefenseDamageSyncPacket Instance { get; private set; }

	public static void Send(CalamityPlayer playerToSync, int toClient = -1, int ignoreClient = -1)
	{
		if (playerToSync != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(playerToSync);
			modPacket.Write(playerToSync.totalDefenseDamage);
			modPacket.Write(playerToSync.defenseDamageRecoveryFrames);
			modPacket.Write(playerToSync.totalDefenseDamageRecoveryFrames);
			modPacket.Write(playerToSync.defenseDamageDelayFrames);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityPlayer player = packet.ReadCalamityPlayer();
		int totalDefDamage = packet.ReadInt32();
		int defDamageRecoverFrames = packet.ReadInt32();
		int totalDefDamageRecoverFrames = packet.ReadInt32();
		int defDamageDelayFrames = packet.ReadInt32();
		if (player != null)
		{
			player.totalDefenseDamage = totalDefDamage;
			player.defenseDamageRecoveryFrames = defDamageRecoverFrames;
			player.totalDefenseDamageRecoveryFrames = totalDefDamageRecoverFrames;
			player.defenseDamageDelayFrames = defDamageDelayFrames;
			if (Main.dedServ)
			{
				Send(player, -1, sender);
			}
		}
	}
}
