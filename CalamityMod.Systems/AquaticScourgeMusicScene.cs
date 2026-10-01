using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.AquaticScourge;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class AquaticScourgeMusicScene : BaseMusicSceneEffect
{
	public override SceneEffectPriority Priority => SceneEffectPriority.BossMedium;

	public override int NPCType => ModContent.NPCType<AquaticScourgeHead>();

	public override int? MusicModMusic => CalamityMod.Instance.GetMusicFromMusicMod("AquaticScourge");

	public override int VanillaMusic => 12;

	public override int OtherworldMusic => 80;

	public override bool AdditionalCheck()
	{
		if (CalamityGlobalNPC.aquaticScourge == -1)
		{
			return false;
		}
		if (!Main.npc[CalamityGlobalNPC.aquaticScourge].justHit && !((double)Main.npc[CalamityGlobalNPC.aquaticScourge].life <= (double)Main.npc[CalamityGlobalNPC.aquaticScourge].lifeMax * 0.999) && !BossRushEvent.BossRushActive)
		{
			return Main.zenithWorld;
		}
		return true;
	}
}
