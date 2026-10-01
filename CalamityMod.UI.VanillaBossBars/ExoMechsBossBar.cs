using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

public class ExoMechsBossBar : ModBossBar
{
	public bool HideAres;

	public bool HideArtemis;

	public bool HideThanatos;

	public bool AllBossesSpawned;

	public NPC FalseNPCSegment;

	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		if (HideArtemis && HideThanatos && !HideAres)
		{
			return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AresBody>()]];
		}
		if (HideAres && HideThanatos && !HideArtemis)
		{
			return ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Artemis/ArtemisHead", (AssetRequestMode)2);
		}
		if (HideAres && HideArtemis && !HideThanatos)
		{
			return ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosNormalHead", (AssetRequestMode)2);
		}
		if (!HideAres)
		{
			return TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AresBody>()]];
		}
		if (!HideArtemis)
		{
			return ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Artemis/ArtemisHead", (AssetRequestMode)2);
		}
		return ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosNormalHead", (AssetRequestMode)2);
	}

	public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
	{
		ValidateAllMechs(ref info);
		NPC target = Main.npc[info.npcIndexToAimAt];
		if (!target.active && !FindMechsAgain(ref info))
		{
			AllBossesSpawned = false;
			return false;
		}
		life = target.life;
		lifeMax = target.lifeMax;
		if (NPC.AnyNPCs(ModContent.NPCType<AresBody>()) && NPC.AnyNPCs(ModContent.NPCType<Artemis>()) && NPC.AnyNPCs(ModContent.NPCType<ThanatosHead>()))
		{
			AllBossesSpawned = true;
		}
		if (AllBossesSpawned)
		{
			FalseNPCSegment = new NPC();
			FalseNPCSegment.SetDefaults(ModContent.NPCType<AresBody>(), target.GetMatchingSpawnParams());
			lifeMax = FalseNPCSegment.lifeMax;
			FalseNPCSegment.SetDefaults(ModContent.NPCType<Artemis>(), target.GetMatchingSpawnParams());
			lifeMax += FalseNPCSegment.lifeMax;
			FalseNPCSegment.SetDefaults(ModContent.NPCType<ThanatosHead>(), target.GetMatchingSpawnParams());
			lifeMax += FalseNPCSegment.lifeMax;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC ecco = enumerator.Current;
				if (ecco.type == ModContent.NPCType<AresBody>() && target.type != ModContent.NPCType<AresBody>())
				{
					life += ecco.life;
				}
				if (ecco.type == ModContent.NPCType<Artemis>() && target.type != ModContent.NPCType<Artemis>())
				{
					life += ecco.life;
				}
				if (ecco.type == ModContent.NPCType<ThanatosHead>() && target.type != ModContent.NPCType<ThanatosHead>())
				{
					life += ecco.life;
				}
			}
		}
		return true;
	}

	public void ValidateAllMechs(ref BigProgressBarInfo info)
	{
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (target.type == ModContent.NPCType<AresBody>())
			{
				HideAres = target.Opacity < 0.5f;
			}
			if (target.type == ModContent.NPCType<Artemis>())
			{
				HideArtemis = target.Opacity < 0.5f;
			}
			if (target.type == ModContent.NPCType<ThanatosHead>())
			{
				HideThanatos = target.Opacity < 0.5f;
			}
		}
		if (!NPC.AnyNPCs(ModContent.NPCType<AresBody>()))
		{
			HideAres = true;
		}
		if (!NPC.AnyNPCs(ModContent.NPCType<Artemis>()))
		{
			HideArtemis = true;
		}
		if (!NPC.AnyNPCs(ModContent.NPCType<ThanatosHead>()))
		{
			HideThanatos = true;
		}
	}

	public bool FindMechsAgain(ref BigProgressBarInfo info)
	{
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			NPC target = Main.npc[i];
			if (target.active)
			{
				if (target.type == ModContent.NPCType<AresBody>() && !HideAres)
				{
					info.npcIndexToAimAt = i;
					return true;
				}
				if (target.type == ModContent.NPCType<Artemis>() && !HideArtemis)
				{
					info.npcIndexToAimAt = i;
					return true;
				}
				if (target.type == ModContent.NPCType<ThanatosHead>() && !HideThanatos)
				{
					info.npcIndexToAimAt = i;
					return true;
				}
				if (target.type == ModContent.NPCType<AresBody>() || target.type == ModContent.NPCType<Artemis>() || target.type == ModContent.NPCType<ThanatosHead>())
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
		}
		return false;
	}
}
