using System.Collections.Generic;
using CalamityMod.NPCs.Ravager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

public class RavagerBossBar : ModBossBar
{
	public NPC FalseNPCSegment;

	public List<int> RavagerParts = new List<int>
	{
		ModContent.NPCType<RavagerClawLeft>(),
		ModContent.NPCType<RavagerClawRight>(),
		ModContent.NPCType<RavagerHead>(),
		ModContent.NPCType<RavagerLegLeft>(),
		ModContent.NPCType<RavagerLegRight>()
	};

	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<RavagerBody>()]];
	}

	public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
	{
		NPC target = Main.npc[info.npcIndexToAimAt];
		if (!target.active && !FindRavagerBody(ref info))
		{
			return false;
		}
		life = target.life;
		lifeMax = target.lifeMax;
		foreach (int type in RavagerParts)
		{
			FalseNPCSegment = new NPC();
			FalseNPCSegment.SetDefaults(type, target.GetMatchingSpawnParams());
			lifeMax += FalseNPCSegment.lifeMax;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			NPC part = enumerator2.Current;
			if (RavagerParts.Contains(part.type))
			{
				life += part.life;
			}
		}
		return true;
	}

	public bool FindRavagerBody(ref BigProgressBarInfo info)
	{
		info.npcIndexToAimAt = NPC.FindFirstNPC(ModContent.NPCType<RavagerBody>());
		return info.npcIndexToAimAt != -1;
	}
}
