using CalamityMod.NPCs;
using CalamityMod.NPCs.DevourerofGods;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class DevourerofGodsPhase1MusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<DevourerofGodsHead>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("DevourerofGodsPhase1");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;

	public override int[] AdditionalNPCs => new int[2]
	{
		ModContent.NPCType<DevourerofGodsBody>(),
		ModContent.NPCType<DevourerofGodsTail>()
	};

	public override bool AdditionalCheck()
	{
		if (CalamityGlobalNPC.DoGHead != -1)
		{
			return CalamityGlobalNPC.DoGP2 == -1;
		}
		return false;
	}
}
