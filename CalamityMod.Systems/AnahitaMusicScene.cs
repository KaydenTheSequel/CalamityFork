using CalamityMod.NPCs;
using CalamityMod.NPCs.Leviathan;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class AnahitaMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<Anahita>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("Anahita");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;

	public override bool AdditionalCheck()
	{
		return CalamityGlobalNPC.LeviAndAna == -1;
	}
}
