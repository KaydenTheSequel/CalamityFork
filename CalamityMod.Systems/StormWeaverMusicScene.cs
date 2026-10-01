using CalamityMod.NPCs.StormWeaver;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class StormWeaverMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<StormWeaverHead>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("StormWeaver");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;

	public override int[] AdditionalNPCs => new int[2]
	{
		ModContent.NPCType<StormWeaverBody>(),
		ModContent.NPCType<StormWeaverTail>()
	};
}
