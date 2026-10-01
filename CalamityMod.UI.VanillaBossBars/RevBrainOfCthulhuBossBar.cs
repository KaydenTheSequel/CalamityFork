using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

internal class RevBrainOfCthulhuBossBar : ModBossBar
{
	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[266]];
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
		shield = 0f;
		shieldMax = 0f;
		if (NPC.AnyNPCs(267))
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC creeper = enumerator.Current;
				if (creeper.type == 267)
				{
					shieldMax = creeper.lifeMax * BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath();
					shield += creeper.life;
				}
			}
		}
		return true;
	}
}
