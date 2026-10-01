using System.IO;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class CodebreakerSummonStuffPacket : CalamityPacket
{
	public static CodebreakerSummonStuffPacket Instance { get; private set; }

	public static void Send(int toClient = -1, int ignoreClient = -1)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		ModPacket modPacket = Instance.CreateBasePacket();
		modPacket.Write(CalamityWorld.DraedonSummonCountdown);
		modPacket.WriteVector2(CalamityWorld.DraedonSummonPosition);
		modPacket.Write(CalamityWorld.DraedonMechdusa);
		modPacket.Send(toClient, ignoreClient);
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		CalamityWorld.DraedonSummonCountdown = packet.ReadInt32();
		CalamityWorld.DraedonSummonPosition = packet.ReadVector2();
		CalamityWorld.DraedonMechdusa = packet.ReadBoolean();
	}
}
