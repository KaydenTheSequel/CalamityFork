using System.IO;
using System.Linq;
using CalamityMod.UI.DialogueDisplay;
using CalamityMod.UI.DialogueDisplay.DisplayEffects;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Packets;

internal sealed class StartDialogueDisplayPacket : CalamityPacket
{
	public enum EntityType : byte
	{
		NPC,
		Player,
		Projectile
	}

	public static StartDialogueDisplayPacket Instance { get; private set; }

	public static void Send(string name, bool progressDialogue, Vector2 position, int index, int uptime, DialogueDisplaySystem.DisplayEffectID effect, float wrapWidth, int toClient = -1, int ignoreClient = -1)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.Write(name);
			modPacket.WriteFlags(progressDialogue);
			modPacket.WritePackedVector2(position);
			modPacket.Write(index);
			modPacket.Write(uptime);
			modPacket.Write((byte)effect);
			modPacket.Write(wrapWidth);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public static void Send(string name, bool progressDialogue, EntityType type, int entity, int index, int uptime, DialogueDisplaySystem.DisplayEffectID effect, float wrapWidth, int toClient = -1, int ignoreClient = -1)
	{
		if (Main.dedServ)
		{
			ModPacket modPacket = Instance.CreateBasePacket();
			modPacket.Write(name);
			modPacket.WriteFlags(progressDialogue, b2: true);
			modPacket.Write((byte)type);
			modPacket.Write(entity);
			modPacket.Write(index);
			modPacket.Write(uptime);
			modPacket.Write((byte)effect);
			modPacket.Write(wrapWidth);
			modPacket.Send(toClient, ignoreClient);
		}
	}

	public override void HandlePacket(BinaryReader packet, int sender)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		string name = packet.ReadString();
		packet.ReadFlags(out var progressDialogue, out var hasEntity);
		int entity = -1;
		Vector2 pos = Vector2.zeroVector;
		EntityType type = EntityType.NPC;
		if (hasEntity)
		{
			type = (EntityType)packet.ReadByte();
			entity = packet.ReadInt32();
		}
		else
		{
			pos = packet.ReadPackedVector2();
		}
		int index = packet.ReadInt32();
		int uptime = packet.ReadInt32();
		byte effect = packet.ReadByte();
		float wrapWidth = packet.ReadSingle();
		if (Main.netMode == 1)
		{
			DisplayEffect de = DialogueDisplaySystem.GetEffect((DialogueDisplaySystem.DisplayEffectID)effect);
			if (hasEntity)
			{
				DialogueDisplaySystem.StartDialogueOnClient(name, type switch
				{
					EntityType.NPC => Main.npc[entity], 
					EntityType.Player => Main.player[entity], 
					EntityType.Projectile => Main.projectile.FirstOrDefault((Projectile p) => p.identity == entity), 
					_ => null, 
				}, index, uptime, progressDialogue, de, wrapWidth);
			}
			else
			{
				DialogueDisplaySystem.StartDialogueOnClient(name, pos, index, uptime, progressDialogue, de, wrapWidth);
			}
		}
	}
}
