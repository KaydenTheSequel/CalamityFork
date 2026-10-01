using System.IO;
using CalamityMod.Projectiles.Boss.BrainOfCthulhu;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Packets.Entities;

internal class BrainIllusionHitPacket : CalamityPacket
{
	public static BrainIllusionHitPacket Instance { get; private set; }

	public static void Send(int illusionIndex, int foolIndex)
	{
		if (!Main.dedServ)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.Write((byte)illusionIndex);
			modPacket.Write((byte)foolIndex);
			modPacket.Send();
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		byte[] bytes = packet.ReadBytes(2);
		NPC illusion = Main.npc[bytes[0]];
		Player fool = Main.player[bytes[1]];
		if (Main.dedServ)
		{
			Projectile.NewProjectile(illusion.GetSource_FromThis(), illusion.Center, Vector2.Zero, ModContent.ProjectileType<TelekineticBlast>(), 50, 0.5f, -1, fool.whoAmI, 8f, illusion.whoAmI);
		}
		illusion.dontTakeDamage = true;
		illusion.netUpdate = true;
	}
}
