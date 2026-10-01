using CalamityMod.NPCs.Polterghast;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class PolterghastMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override int NPCType => ModContent.NPCType<Polterghast>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("Polterghast");

	public override int VanillaMusic => 24;

	public override int OtherworldMusic => 85;

	public override bool AdditionalCheck()
	{
		return !Main.zenithWorld;
	}
}
