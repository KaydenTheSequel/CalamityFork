using CalamityMod.NPCs;
using CalamityMod.NPCs.Providence;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class ProvidenceMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<Providence>();

	public static int ProvidenceTrack => CalamityMod.Instance.GetMusicFromMusicMod("Providence").Value;

	public static int SilenceTrack => MusicLoader.GetMusicSlot(CalamityMod.Instance, "Sounds/Music/Silence");

	public override int? MusicModMusic => (ProvidenceSpawnState() < 180f && ProvUtils.StandardAI()) ? SilenceTrack : ProvidenceTrack;

	public override int VanillaMusic => 38;

	public override int OtherworldMusic => 84;

	public override void SpecialVisuals(Player player, bool isActive)
	{
		if (ProvidenceSpawnState() == 180f && ProvUtils.StandardAI())
		{
			Main.musicFade[ProvidenceTrack] = 1f;
		}
	}

	public static float ProvidenceSpawnState()
	{
		int provIndex = CalamityGlobalNPC.holyBoss;
		if (provIndex < 0 || provIndex >= Main.maxNPCs || !Main.npc[provIndex].active)
		{
			return -1f;
		}
		return Main.npc[provIndex].Calamity().newAI[3];
	}
}
