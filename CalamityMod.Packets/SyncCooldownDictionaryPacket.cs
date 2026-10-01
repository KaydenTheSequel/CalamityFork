using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncCooldownDictionaryPacket : CalamityPacket
{
	public static SyncCooldownDictionaryPacket Instance { get; private set; }

	public static void Send(CalamityPlayer player, int toClient = -1, int ignoreClient = -1)
	{
		if (player != null)
		{
			IEnumerable<(ushort, int, int)> cooldowns = player.cooldowns.Values.Select((CooldownInstance cd) => (netID: cd.netID, duration: cd.duration, timeLeft: cd.timeLeft));
			Send(player, cooldowns, toClient, ignoreClient);
		}
	}

	public static void Send(CalamityPlayer player, IEnumerable<(ushort netID, int duration, int timeLeft)> cooldowns, int toClient = -1, int ignoreClient = -1)
	{
		if (player == null || cooldowns == null)
		{
			return;
		}
		ModPacket packet = Instance.CreateBasePacket();
		packet.WriteWhoAmI(player);
		packet.Write(player.cooldowns.Count);
		foreach (var cd in cooldowns)
		{
			packet.Write(cd.netID);
			packet.Write(cd.duration);
			packet.Write(cd.timeLeft);
		}
		packet.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		CalamityPlayer player = packet.ReadCalamityPlayer();
		int count = packet.ReadInt32();
		Dictionary<ushort, (ushort, int, int)> receivedCooldowns = new Dictionary<ushort, (ushort, int, int)>(count);
		for (int i = 0; i < count; i++)
		{
			ushort netID = packet.ReadUInt16();
			int duration = packet.ReadInt32();
			int timeLeft = packet.ReadInt32();
			receivedCooldowns[netID] = (netID, duration, timeLeft);
		}
		if (player == null)
		{
			return;
		}
		if (Main.netMode == 1)
		{
			HashSet<ushort> hashSet = new HashSet<ushort>();
			foreach (ushort item in player.cooldowns.Values.Select((CooldownInstance cd) => cd.netID))
			{
				hashSet.Add(item);
			}
			HashSet<ushort> localIDs = hashSet;
			hashSet = new HashSet<ushort>();
			foreach (ushort key in receivedCooldowns.Keys)
			{
				hashSet.Add(key);
			}
			HashSet<ushort> receivedIDs = hashSet;
			HashSet<ushort> hashSet2 = new HashSet<ushort>();
			hashSet2.UnionWith(localIDs);
			hashSet2.UnionWith(receivedIDs);
			{
				foreach (ushort netID2 in hashSet2)
				{
					bool existsLocally = localIDs.Contains(netID2);
					bool existsRemotely = receivedIDs.Contains(netID2);
					string id = CooldownRegistry.registry[netID2].ID;
					if (existsLocally && !existsRemotely)
					{
						player.cooldowns.Remove(id);
					}
					else if (existsRemotely && !existsLocally)
					{
						(ushort, int, int) cdToAdd = receivedCooldowns[netID2];
						player.cooldowns[id] = new CooldownInstance(player.Player, cdToAdd.Item1, cdToAdd.Item2, cdToAdd.Item3);
					}
					else if (existsLocally & existsRemotely)
					{
						CooldownInstance cooldownInstance = player.cooldowns[id];
						cooldownInstance.duration = receivedCooldowns[netID2].Item2;
						cooldownInstance.timeLeft = receivedCooldowns[netID2].Item3;
					}
				}
				return;
			}
		}
		if (Main.dedServ)
		{
			Send(player, receivedCooldowns.Values, -1, sender);
		}
	}
}
