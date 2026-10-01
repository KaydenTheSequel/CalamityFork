using System.IO;
using CalamityMod.TileEntities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal class TEUpdateCodebreakerConstituentsPacket : CalamityPacket
{
	public static TEUpdateCodebreakerConstituentsPacket Instance { get; private set; }

	public static void Send(TECodebreaker codeBreaker, int toClient = -1, int ignoreClient = -1)
	{
		if (codeBreaker != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			BitsByte containmentFlagWrapper = new BitsByte
			{
				[0] = codeBreaker.ContainsDecryptionComputer,
				[1] = codeBreaker.ContainsSensorArray,
				[2] = codeBreaker.ContainsAdvancedDisplay,
				[3] = codeBreaker.ContainsVoltageRegulationSystem,
				[4] = codeBreaker.ContainsCoolingCell
			};
			modPacket.WriteTileEntityID(codeBreaker);
			modPacket.Write(containmentFlagWrapper);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		TECodebreaker codeBreaker = packet.ReadTileEntity<TECodebreaker>();
		BitsByte containmentFlagWrapper = packet.ReadByte();
		bool containsDecryptionComputer = containmentFlagWrapper[0];
		bool containsSensorArray = containmentFlagWrapper[1];
		bool containsAdvancedDisplay = containmentFlagWrapper[2];
		bool containsVoltageRegulationSystem = containmentFlagWrapper[3];
		bool containsCoolingCell = containmentFlagWrapper[4];
		if (codeBreaker != null)
		{
			codeBreaker.ContainsDecryptionComputer = containsDecryptionComputer;
			codeBreaker.ContainsSensorArray = containsSensorArray;
			codeBreaker.ContainsAdvancedDisplay = containsAdvancedDisplay;
			codeBreaker.ContainsVoltageRegulationSystem = containsVoltageRegulationSystem;
			codeBreaker.ContainsCoolingCell = containsCoolingCell;
			if (Main.dedServ)
			{
				Send(codeBreaker, -1, sender);
			}
		}
	}
}
