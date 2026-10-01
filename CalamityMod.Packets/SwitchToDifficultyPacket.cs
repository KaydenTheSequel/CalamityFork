using System.IO;
using System.Linq;
using CalamityMod.Systems;
using CalamityMod.UI.ModeIndicator;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SwitchToDifficultyPacket : CalamityPacket
{
	public static SwitchToDifficultyPacket Instance { get; private set; }

	public static void Send(DifficultyMode modeToSwitch, int toClient = -1, int ignoreClient = -1)
	{
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(modeToSwitch.FullName);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		string modeName = packet.ReadString();
		DifficultyMode difficulty = DifficultyModeSystem.Difficulties.SingleOrDefault((DifficultyMode diff) => diff.FullName.Equals(modeName));
		if (difficulty != null)
		{
			ModeIndicatorUI.SwitchToDifficulty(difficulty, broadcast: false);
			if (Main.dedServ)
			{
				Send(difficulty, -1, sender);
			}
			return;
		}
		CalamityMod.Log.Error((object)$"Packet: [{"SwitchToDifficultyPacket"}] has failed! Name: [{modeName}] is not a valid {"DifficultyMode"} name!");
	}
}
