using System.Collections.Generic;
using CalamityMod.NPCs.SlimeGod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

public class SlimeGodBossBar : ModBossBar
{
	public NPC FalseNPCSegment;

	public List<int> SlimeGodSlimes = new List<int>
	{
		ModContent.NPCType<CrimulanPaladin>(),
		ModContent.NPCType<EbonianPaladin>(),
		ModContent.NPCType<SplitCrimulanPaladin>(),
		ModContent.NPCType<SplitEbonianPaladin>()
	};

	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<SlimeGodCore>()]];
	}

	public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
	{
		NPC target = Main.npc[info.npcIndexToAimAt];
		if (!target.active)
		{
			return false;
		}
		life = 0f;
		lifeMax = 0f;
		FalseNPCSegment = new NPC();
		FalseNPCSegment.SetDefaults(ModContent.NPCType<CrimulanPaladin>(), target.GetMatchingSpawnParams());
		lifeMax += FalseNPCSegment.lifeMax;
		FalseNPCSegment.SetDefaults(ModContent.NPCType<EbonianPaladin>(), target.GetMatchingSpawnParams());
		lifeMax += FalseNPCSegment.lifeMax;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC part = enumerator.Current;
			if (SlimeGodSlimes.Contains(part.type))
			{
				life += part.life;
			}
		}
		return true;
	}
}
