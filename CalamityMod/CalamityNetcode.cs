using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Packets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod;

public class CalamityNetcode : ModSystem
{
	private static List<CalamityPacket> _PacketHandlers = new List<CalamityPacket>();

	internal static ushort RegisterHandler(CalamityPacket handler)
	{
		ushort result = (ushort)_PacketHandlers.Count;
		_PacketHandlers.Add(handler);
		return result;
	}

	internal static void WriteHandlerNetID(BinaryWriter packet, ushort netID)
	{
		if (_PacketHandlers.Count > 256)
		{
			packet.Write(netID);
		}
		else
		{
			packet.Write((byte)netID);
		}
	}

	internal static ushort ReadHandlerNetID(BinaryReader packet)
	{
		if (_PacketHandlers.Count > 256)
		{
			return packet.ReadUInt16();
		}
		return packet.ReadByte();
	}

	public override void OnModUnload()
	{
		_PacketHandlers = null;
	}

	public static void HandlePacket(Mod mod, BinaryReader reader, int whoAmI)
	{
		try
		{
			ushort netID = ReadHandlerNetID(reader);
			CalamityPacket packetHandler = _PacketHandlers[netID];
			if (packetHandler != null)
			{
				packetHandler.HandlePacket(reader, whoAmI);
				return;
			}
			CalamityMod.Log.Error((object)$"Failed to parse Calamity packet: No Calamity packet exists with ID {netID}.");
			throw new Exception("Failed to parse Calamity packet: Invalid Calamity packet ID.");
		}
		catch (Exception ex)
		{
			if (ex is EndOfStreamException eose)
			{
				CalamityMod.Log.Error((object)"Failed to parse Calamity packet: Packet was too short, missing data, or otherwise corrupt.", (Exception)eose);
				return;
			}
			if (ex is ObjectDisposedException ode)
			{
				CalamityMod.Log.Error((object)"Failed to parse Calamity packet: Packet reader disposed or destroyed.", (Exception)ode);
				return;
			}
			if (ex is IOException ioe)
			{
				CalamityMod.Log.Error((object)"Failed to parse Calamity packet: An unknown I/O error occurred.", (Exception)ioe);
				return;
			}
			throw;
		}
	}

	public static void SyncWorld()
	{
		if (Main.dedServ)
		{
			NetMessage.SendData(7);
		}
	}

	public static void SyncNPC(NPC npcToSync, int toClient = -1, int ignoreClient = -1)
	{
		if (Main.dedServ && npcToSync != null)
		{
			int npcWhoAmI = npcToSync.whoAmI;
			if (npcWhoAmI >= 0 && npcWhoAmI < Main.maxNPCs)
			{
				NetMessage.SendData(23, toClient, ignoreClient, null, npcWhoAmI);
			}
		}
	}

	public static void SyncNPC(int npcWhoAmI, int toClient = -1, int ignoreClient = -1)
	{
		if (Main.dedServ && npcWhoAmI >= 0 && npcWhoAmI < Main.maxNPCs)
		{
			NetMessage.SendData(23, toClient, ignoreClient, null, npcWhoAmI);
		}
	}

	public static void NewNPC_ClientSide(Vector2 spawnPosition, int npcType, Player player)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 0)
		{
			NPC.NewNPC(new EntitySource_WorldEvent(), (int)spawnPosition.X, (int)spawnPosition.Y, npcType, 0, 0f, 0f, 0f, 0f, player.whoAmI);
		}
		else if (Main.netMode == 1)
		{
			SpawnNPCOnPlayerPacket.Send(player, (int)spawnPosition.X, (int)spawnPosition.Y, npcType);
		}
	}
}
