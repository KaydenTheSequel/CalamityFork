using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class ExoMechsMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<Draedon>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("ExoMechs");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;

	public override int[] AdditionalNPCs => new int[7]
	{
		ModContent.NPCType<Apollo>(),
		ModContent.NPCType<AresBody>(),
		ModContent.NPCType<Artemis>(),
		ModContent.NPCType<ThanatosHead>(),
		ModContent.NPCType<ThanatosBody1>(),
		ModContent.NPCType<ThanatosBody2>(),
		ModContent.NPCType<ThanatosTail>()
	};

	public override bool AdditionalCheck()
	{
		return CalamityGlobalNPC.draedonAmbience == -1;
	}
}
