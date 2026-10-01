using System;
using System.IO;
using CalamityMod.UI;
using Terraria;
using Terraria.ModLoader.IO;

namespace CalamityMod.Cooldowns;

public class CooldownInstance
{
	private const string NetIDSaveKey = "netID";

	private const string DurationSaveKey = "duration";

	private const string TimeLeftSaveKey = "timeLeft";

	internal ushort netID;

	public Player player;

	public int duration;

	public int timeLeft;

	public CooldownHandler handler;

	public float Completion
	{
		get
		{
			if (!CooldownRackUI.DebugFullDisplay)
			{
				if (duration == 0)
				{
					return 0f;
				}
				return (float)timeLeft / (float)duration;
			}
			return CooldownRackUI.DebugForceCompletion;
		}
	}

	public CooldownInstance(Player p, Cooldown cd, int dur)
	{
		netID = cd.netID;
		player = p;
		duration = dur;
		timeLeft = dur;
		handler = null;
		AssignHandler(cd);
	}

	public CooldownInstance(Player p, Cooldown cd, int dur, params object[] args)
	{
		netID = cd.netID;
		player = p;
		duration = dur;
		timeLeft = dur;
		handler = null;
		AssignHandler(cd, args);
	}

	internal CooldownInstance(Player p, string id, TagCompound tag)
	{
		netID = (ushort)tag.GetAsInt("netID");
		Cooldown cd = CooldownRegistry.Get(id);
		if (cd == null)
		{
			CalamityMod.Log.Warn((object)("Cooldown \"" + id + "\" loaded from NBT, but was not found. This cooldown will not be applied to the player."));
			return;
		}
		ushort registeredNetID = cd.netID;
		if (netID != registeredNetID)
		{
			CalamityMod.Log.Warn((object)$"Cooldown \"{id}\" loaded from NBT with discrepant netID {netID}. This cooldown was registered with netID {registeredNetID}");
			netID = registeredNetID;
		}
		player = p;
		duration = tag.GetAsInt("duration");
		timeLeft = tag.GetAsInt("timeLeft");
		AssignHandler(cd);
	}

	internal CooldownInstance(Player player, ushort netID, int duration, int timeLeft)
	{
		this.netID = netID;
		this.player = player;
		this.duration = duration;
		this.timeLeft = timeLeft;
		string id = CooldownRegistry.registry[netID].ID;
		AssignHandler(CooldownRegistry.Get(id));
	}

	internal CooldownInstance(BinaryReader reader)
	{
		netID = reader.ReadUInt16();
		byte playerIDByte = reader.ReadByte();
		player = Main.player[playerIDByte];
		duration = reader.ReadInt32();
		timeLeft = reader.ReadInt32();
		string id = CooldownRegistry.registry[netID].ID;
		AssignHandler(CooldownRegistry.Get(id));
	}

	internal void AssignHandler(Cooldown cd)
	{
		Type handlerT = cd.GetType().GenericTypeArguments[0];
		handler = Activator.CreateInstance(handlerT) as CooldownHandler;
		handler.instance = this;
	}

	internal void AssignHandler(Cooldown cd, params object[] args)
	{
		Type handlerT = cd.GetType().GenericTypeArguments[0];
		handler = Activator.CreateInstance(handlerT, args) as CooldownHandler;
		handler.instance = this;
	}

	internal TagCompound Save()
	{
		return new TagCompound
		{
			{
				"netID",
				(int)netID
			},
			{ "duration", duration },
			{ "timeLeft", timeLeft }
		};
	}

	internal void Write(BinaryWriter writer)
	{
		writer.Write(netID);
		byte playerIDByte = (byte)player.whoAmI;
		writer.Write(playerIDByte);
		writer.Write(duration);
		writer.Write(timeLeft);
	}
}
