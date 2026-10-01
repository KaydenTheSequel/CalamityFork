using CalamityMod.NPCs.DesertScourge;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class DesertScourgeMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<DesertScourgeHead>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("DesertScourge");

	public override int VanillaMusic => 5;

	public override int OtherworldMusic => 81;

	public override int[] AdditionalNPCs => new int[2]
	{
		ModContent.NPCType<DesertScourgeBody>(),
		ModContent.NPCType<DesertScourgeTail>()
	};
}
