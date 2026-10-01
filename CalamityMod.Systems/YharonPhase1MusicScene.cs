using CalamityMod.NPCs;
using CalamityMod.NPCs.Yharon;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class YharonPhase1MusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<Yharon>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("YharonPhase1");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;

	public override bool AdditionalCheck()
	{
		if (CalamityGlobalNPC.yharon != -1)
		{
			return CalamityGlobalNPC.yharonP2 == -1;
		}
		return false;
	}
}
