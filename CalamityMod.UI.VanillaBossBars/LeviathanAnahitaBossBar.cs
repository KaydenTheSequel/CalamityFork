using CalamityMod.NPCs.Leviathan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

public class LeviathanAnahitaBossBar : ModBossBar
{
	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		if (NPC.AnyNPCs(ModContent.NPCType<Leviathan>()))
		{
			return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Leviathan>()]];
		}
		return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Anahita>()]];
	}

	public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
	{
		NPC target = Main.npc[info.npcIndexToAimAt];
		if (!target.active && !FindTheRightFish(ref info))
		{
			return false;
		}
		life = target.life;
		lifeMax = target.lifeMax;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC wife = enumerator.Current;
			int targetCouple = ((target.type == ModContent.NPCType<Anahita>()) ? ModContent.NPCType<Leviathan>() : ModContent.NPCType<Anahita>());
			if (wife.type == targetCouple)
			{
				life += wife.life;
				lifeMax += wife.lifeMax;
			}
		}
		shield = 0f;
		shieldMax = 0f;
		if (target.type == ModContent.NPCType<Anahita>() && !NPC.AnyNPCs(ModContent.NPCType<Leviathan>()))
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				NPC part = enumerator2.Current;
				if (part.type == ModContent.NPCType<AnahitasIceShield>())
				{
					shield += part.life;
					shieldMax += part.lifeMax;
				}
			}
		}
		return true;
	}

	public bool FindTheRightFish(ref BigProgressBarInfo info)
	{
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (target.active && target.type == ModContent.NPCType<Anahita>())
			{
				info.npcIndexToAimAt = target.whoAmI;
				return true;
			}
			if (target.active && target.type == ModContent.NPCType<Leviathan>())
			{
				info.npcIndexToAimAt = target.whoAmI;
				return true;
			}
		}
		return false;
	}
}
