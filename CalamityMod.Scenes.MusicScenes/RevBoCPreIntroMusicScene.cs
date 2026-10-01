using CalamityMod.NPCs;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Scenes.MusicScenes;

public class RevBoCPreIntroMusicScene : ModSceneEffect
{
	public override int Music
	{
		get
		{
			if (BrainOfCthulhuSystem.PreviousMusic < 0)
			{
				return 16;
			}
			return BrainOfCthulhuSystem.PreviousMusic;
		}
	}

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
		return revBrain.SpawnTime == 0f;
	}
}
