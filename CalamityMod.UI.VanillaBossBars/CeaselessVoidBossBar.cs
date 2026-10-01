using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

public class CeaselessVoidBossBar : ModBossBar
{
	public NPC FalseNPCSegment;

	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<CeaselessVoid>()]];
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
		float lifePercent = Utils.Clamp(life / lifeMax, 0f, 1f);
		shield = 0f;
		shieldMax = 0f;
		int ExpectedBallsCounter = (((lifePercent <= 0.1f) ? 3 : ((lifePercent <= 0.4f) ? 2 : ((lifePercent <= 0.7f) ? 1 : 0))) + (CalamityWorld.death ? 6 : (CalamityWorld.revenge ? 5 : (Main.expertMode ? 4 : 3)))) * (Main.getGoodWorld ? 6 : 3) + 2;
		float RatioToCombust = 0.2f;
		if (NPC.AnyNPCs(ModContent.NPCType<DarkEnergy>()))
		{
			FalseNPCSegment = new NPC();
			FalseNPCSegment.SetDefaults(ModContent.NPCType<DarkEnergy>(), target.GetMatchingSpawnParams());
			shieldMax = (int)((float)(FalseNPCSegment.lifeMax * ExpectedBallsCounter) * (1f - RatioToCombust));
			shield -= (int)((float)(FalseNPCSegment.lifeMax * ExpectedBallsCounter) * RatioToCombust);
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC part = enumerator.Current;
				if (part.type == ModContent.NPCType<DarkEnergy>())
				{
					shield += part.life;
				}
			}
		}
		return true;
	}
}
