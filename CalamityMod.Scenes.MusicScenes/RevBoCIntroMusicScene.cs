using System;
using CalamityMod.NPCs;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Scenes.MusicScenes;

public class RevBoCIntroMusicScene : ModSceneEffect
{
	public override int Music => MusicLoader.GetMusicSlot(base.Mod, "Sounds/Music/Silence");

	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override bool IsSceneEffectActive(Player player)
	{
		if (!CalamityWorld.revenge)
		{
			return false;
		}
		if (NPC.crimsonBoss == -1)
		{
			return false;
		}
		NPC brain = Main.npc[NPC.crimsonBoss];
		if ((byte)brain.ai[0] > 1)
		{
			return false;
		}
		if (!brain.TryGetAIOverride<BrainOfCthulhuAI>(out var revBrain))
		{
			return false;
		}
		if (revBrain.SpawnTime != 0f)
		{
			return revBrain.Time - Math.Abs(revBrain.SpawnTime) < 420f;
		}
		return false;
	}
}
