using CalamityMod.NPCs.ProfanedGuardians;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

public class ProfanedGuardianBossBar : ModBossBar
{
	public NPC FalseNPCSegment;

	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		if (NPC.AnyNPCs(ModContent.NPCType<ProfanedGuardianHealer>()))
		{
			return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<ProfanedGuardianHealer>()]];
		}
		if (NPC.AnyNPCs(ModContent.NPCType<ProfanedGuardianDefender>()))
		{
			return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<ProfanedGuardianDefender>()]];
		}
		return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<ProfanedGuardianCommander>()]];
	}

	public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
	{
		NPC target = Main.npc[info.npcIndexToAimAt];
		if (!target.active)
		{
			return false;
		}
		life = target.life;
		lifeMax = target.lifeMax;
		FalseNPCSegment = new NPC();
		FalseNPCSegment.SetDefaults(ModContent.NPCType<ProfanedGuardianDefender>(), target.GetMatchingSpawnParams());
		lifeMax += FalseNPCSegment.lifeMax;
		FalseNPCSegment.SetDefaults(ModContent.NPCType<ProfanedGuardianHealer>(), target.GetMatchingSpawnParams());
		lifeMax += FalseNPCSegment.lifeMax;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC guardian = enumerator.Current;
			if (guardian.type == ModContent.NPCType<ProfanedGuardianDefender>() || guardian.type == ModContent.NPCType<ProfanedGuardianHealer>())
			{
				life += guardian.life;
			}
		}
		return true;
	}
}
