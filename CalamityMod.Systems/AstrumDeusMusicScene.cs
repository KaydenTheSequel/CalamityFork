using CalamityMod.NPCs.AstrumDeus;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class AstrumDeusMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<AstrumDeusHead>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("AstrumDeus");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;

	public override int[] AdditionalNPCs => new int[2]
	{
		ModContent.NPCType<AstrumDeusBody>(),
		ModContent.NPCType<AstrumDeusTail>()
	};
}
