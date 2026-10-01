using CalamityMod.NPCs;
using CalamityMod.NPCs.Leviathan;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class LeviathanMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<Leviathan>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("Leviathan");

	public override int VanillaMusic => 13;

	public override int OtherworldMusic => 80;

	public override int[] AdditionalNPCs => new int[1] { ModContent.NPCType<Anahita>() };

	public override bool AdditionalCheck()
	{
		return CalamityGlobalNPC.LeviAndAna != -1;
	}
}
