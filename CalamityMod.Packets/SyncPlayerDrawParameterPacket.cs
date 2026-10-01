using System;
using System.IO;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets;

internal sealed class SyncPlayerDrawParameterPacket : CalamityPacket
{
	public static SyncPlayerDrawParameterPacket Instance { get; set; }

	public static void Send(CalamityPlayer player, int toClient = -1, int ignoreClient = -1)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (player != null)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.WriteWhoAmI(player);
			modPacket.Write((Half)player.drawingParameters.RoverShieldCharge);
			modPacket.Write((Half)player.drawingParameters.LunicShieldCharge);
			modPacket.Write((Half)player.drawingParameters.ProfanedShieldCharge);
			modPacket.WriteRGB(player.drawingParameters.ProfanedShieldColor);
			modPacket.Write((Half)player.drawingParameters.SpongeShieldCharge);
			modPacket.Write((short)player.RoverDriveShieldDurability);
			modPacket.Write((short)player.LunicCorpsShieldDurability);
			modPacket.Write((short)player.pSoulShieldDurability);
			modPacket.Write((short)player.SpongeShieldDurability);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer player = packet.ReadCalamityPlayer();
		float roverCharge = (float)packet.ReadHalf();
		float lunicCharge = (float)packet.ReadHalf();
		float profanedCharge = (float)packet.ReadHalf();
		Color profanedColor = packet.ReadRGB();
		float spongeCharge = (float)packet.ReadHalf();
		short roverDurability = packet.ReadInt16();
		short lunicDurability = packet.ReadInt16();
		short pSoulDurability = packet.ReadInt16();
		short spongeDurability = packet.ReadInt16();
		if (player != null)
		{
			player.drawingParameters.RoverShieldCharge = roverCharge;
			player.drawingParameters.LunicShieldCharge = lunicCharge;
			player.drawingParameters.ProfanedShieldCharge = profanedCharge;
			player.drawingParameters.ProfanedShieldColor = profanedColor;
			player.drawingParameters.SpongeShieldCharge = spongeCharge;
			player.RoverDriveShieldDurability = roverDurability;
			player.LunicCorpsShieldDurability = lunicDurability;
			player.pSoulShieldDurability = pSoulDurability;
			player.SpongeShieldDurability = spongeDurability;
			if (Main.dedServ)
			{
				Send(player, -1, sender);
			}
		}
	}
}
