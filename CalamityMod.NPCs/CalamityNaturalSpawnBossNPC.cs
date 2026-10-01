using System.Linq;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.NPCs.Astral;
using CalamityMod.NPCs.GreatSandShark;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.Polterghast;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public sealed class CalamityNaturalSpawnBossNPC : GlobalNPC
{
	private sealed class ClearWorldHook : ModSystem
	{
		public override void ClearWorld()
		{
			ghostKillCount = 0;
			sharkKillCount = 0;
		}
	}

	public static int ghostKillCount;

	public static int sharkKillCount;

	private static int[] _PolterghastTriggerNPCS;

	private static int[] _GreatSharkTriggerNPCS;

	public override bool InstancePerEntity => false;

	public override void SetStaticDefaults()
	{
		ghostKillCount = 0;
		sharkKillCount = 0;
		_PolterghastTriggerNPCS = new int[4]
		{
			ModContent.NPCType<PhantomSpirit>(),
			ModContent.NPCType<PhantomSpiritS>(),
			ModContent.NPCType<PhantomSpiritM>(),
			ModContent.NPCType<PhantomSpiritL>()
		};
		_GreatSharkTriggerNPCS = new int[4] { 542, 545, 543, 544 };
	}

	public override void Unload()
	{
		_PolterghastTriggerNPCS = null;
		_GreatSharkTriggerNPCS = null;
	}

	public override void OnKill(NPC npc)
	{
		CheckPolterghastCondition(npc);
		CheckGreatSandSharkCondition(npc);
	}

	private static void CheckPolterghastCondition(NPC slainedNPC)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if (DownedBossSystem.downedPolterghast || NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.Polterghast.Polterghast>()) || !_PolterghastTriggerNPCS.Contains(slainedNPC.type))
		{
			return;
		}
		ghostKillCount++;
		if (ghostKillCount == 10)
		{
			Color messageColor = Color.Cyan;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.GhostBossText2", messageColor);
		}
		else if (ghostKillCount == 20)
		{
			Color messageColor2 = Color.Cyan;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.GhostBossText3", messageColor2);
		}
		if (ghostKillCount >= 30 && Main.netMode != 1)
		{
			int lastPlayer = slainedNPC.lastInteraction;
			if (!Main.player[lastPlayer].active || Main.player[lastPlayer].dead)
			{
				lastPlayer = slainedNPC.FindClosestPlayer();
			}
			if (lastPlayer >= 0)
			{
				SoundEngine.PlaySound(in global::CalamityMod.NPCs.Polterghast.Polterghast.SpawnSound, Main.player[lastPlayer].Center);
				NPC.SpawnOnPlayer(lastPlayer, ModContent.NPCType<global::CalamityMod.NPCs.Polterghast.Polterghast>());
				ghostKillCount = 0;
			}
		}
	}

	private static void CheckGreatSandSharkCondition(NPC slainedNPC)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (!NPC.downedPlantBoss || NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.GreatSandShark.GreatSandShark>()))
		{
			return;
		}
		bool fusionFeeder = slainedNPC.type == ModContent.NPCType<FusionFeeder>() && Main.zenithWorld;
		if (!_GreatSharkTriggerNPCS.Contains(slainedNPC.type) && !fusionFeeder)
		{
			return;
		}
		sharkKillCount++;
		if (sharkKillCount == 4)
		{
			Color messageColor = Color.Goldenrod;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.SandSharkText", messageColor);
		}
		else if (sharkKillCount == 8)
		{
			Color messageColor2 = Color.Goldenrod;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.SandSharkText2", messageColor2);
		}
		if (sharkKillCount >= 10 && Main.netMode != 1)
		{
			if (!Main.LocalPlayer.dead && Main.LocalPlayer.active)
			{
				SoundEngine.PlaySound(in Mauler.RoarSound, Main.LocalPlayer.Center);
			}
			int lastPlayer = slainedNPC.lastInteraction;
			if (!Main.player[lastPlayer].active || Main.player[lastPlayer].dead)
			{
				lastPlayer = slainedNPC.FindClosestPlayer();
			}
			if (lastPlayer >= 0)
			{
				NPC.SpawnOnPlayer(lastPlayer, ModContent.NPCType<global::CalamityMod.NPCs.GreatSandShark.GreatSandShark>());
				sharkKillCount = -5;
			}
		}
	}
}
