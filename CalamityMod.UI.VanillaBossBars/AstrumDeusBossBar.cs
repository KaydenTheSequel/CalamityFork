using System;
using CalamityMod.NPCs.AstrumDeus;
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

public class AstrumDeusBossBar : ModBossBar
{
	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AstrumDeusHead>()]];
	}

	public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
	{
		NPC target = Main.npc[info.npcIndexToAimAt];
		if (!target.active && !FindMoreWorms(ref info))
		{
			return false;
		}
		life = 0f;
		lifeMax = 0f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC worm = enumerator.Current;
			if (worm.type != target.type)
			{
				continue;
			}
			if (CalamityWorld.death)
			{
				life += worm.life;
				lifeMax += worm.lifeMax;
				continue;
			}
			if (life <= 0f)
			{
				life = worm.life;
			}
			else
			{
				life = Math.Min(life, worm.life);
			}
			lifeMax = worm.lifeMax;
		}
		return true;
	}

	public bool FindMoreWorms(ref BigProgressBarInfo info)
	{
		info.npcIndexToAimAt = NPC.FindFirstNPC(ModContent.NPCType<AstrumDeusHead>());
		return info.npcIndexToAimAt != -1;
	}
}
