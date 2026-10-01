using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class CalamitasDefeatMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<SupremeCalamitas>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("CalamitasDefeat");

	public override int VanillaMusic => 2;

	public override int OtherworldMusic => 79;

	public override bool AdditionalCheck()
	{
		return CalamityGlobalNPC.SCalAcceptance != -1;
	}
}
