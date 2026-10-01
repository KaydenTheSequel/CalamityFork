using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

internal class RevMoonLordBossBar : ModBossBar
{
	private List<int> MoonLordPartIDs;

	private NPC _dummy;

	public override void SetStaticDefaults()
	{
		_dummy = new NPC();
	}

	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[396]];
	}

	public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
	{
		NPC target = Main.npc[info.npcIndexToAimAt];
		if ((!target.active || InBadAI(target)) && !TryFindingAnotherMoonLordPiece(ref info))
		{
			return false;
		}
		life = 0f;
		lifeMax = 0f;
		shield = 0f;
		shieldMax = 0f;
		NPCSpawnParams dummy = new NPCSpawnParams
		{
			strengthMultiplierOverride = target.strengthMultiplier,
			playerCountForMultiplayerDifficultyOverride = target.statsAreScaledForThisManyPlayers
		};
		_dummy.SetDefaults(398, dummy);
		lifeMax += _dummy.lifeMax;
		_dummy.SetDefaults(396, dummy);
		lifeMax += _dummy.lifeMax;
		_dummy.SetDefaults(397, dummy);
		lifeMax += _dummy.lifeMax * 2;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (MoonLordPartIDs.Contains(n.type) && !InBadAI(n))
			{
				life += n.life;
			}
		}
		return true;
	}

	private static bool InBadAI(NPC n)
	{
		if (n.type == 398 && (n.ai[0] == 2f || n.ai[0] == -1f || n.localAI[3] == 0f))
		{
			return true;
		}
		if (n.ai[0] == -2f || n.ai[0] == -3f || n.Calamity().newAI[0] == 1f)
		{
			return true;
		}
		return false;
	}

	private bool TryFindingAnotherMoonLordPiece(ref BigProgressBarInfo info)
	{
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (MoonLordPartIDs.Contains(n.type) && !InBadAI(n))
			{
				info.npcIndexToAimAt = n.whoAmI;
				return true;
			}
		}
		return false;
	}

	public RevMoonLordBossBar()
	{
		int num = 3;
		List<int> list = new List<int>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<int> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = 398;
		num2++;
		span[num2] = 396;
		span[num2 + 1] = 397;
		MoonLordPartIDs = list;
		base._002Ector();
	}
}
