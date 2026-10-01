using CalamityMod.NPCs.CalClone;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CalamitasCloneMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<CalamitasClone>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("CalamitasClone");

	public override int VanillaMusic => 12;

	public override int OtherworldMusic => 80;

	public override int[] AdditionalNPCs => new int[2]
	{
		ModContent.NPCType<Cataclysm>(),
		ModContent.NPCType<Catastrophe>()
	};
}
