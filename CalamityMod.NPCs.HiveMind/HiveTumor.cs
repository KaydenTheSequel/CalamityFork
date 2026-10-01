using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.HiveMind;

public class HiveTumor : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 0;
		base.NPC.width = 30;
		base.NPC.height = 30;
		base.NPC.defense = 0;
		base.NPC.lifeMax = (Main.zenithWorld ? 10 : 1000);
		base.NPC.knockBackResist = 0f;
		base.NPC.chaseable = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.rarity = 2;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().ProvidesProximityRage = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.HiveTumor")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		float timeToSpawn = 120f;
		if (!Main.zenithWorld || !NPC.AnyNPCs(ModContent.NPCType<HiveMind>()))
		{
			return;
		}
		base.NPC.ai[0]++;
		if (base.NPC.ai[0] >= timeToSpawn)
		{
			int spawnRandomizer = Main.rand.Next(0, 5);
			int type = 6;
			switch (spawnRandomizer)
			{
			case 2:
				type = 7;
				break;
			case 3:
				type = ModContent.NPCType<DankCreeper>();
				break;
			case 4:
				type = ModContent.NPCType<HiveBlob>();
				break;
			}
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, type);
			base.NPC.ai[0] = 0f;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player) || !spawnInfo.Player.ZoneCorrupt)
		{
			return 0f;
		}
		if (spawnInfo.Player.Calamity().disableHiveCystSpawns)
		{
			return 0f;
		}
		bool corrupt = TileID.Sets.Corrupt[spawnInfo.SpawnTileType] || spawnInfo.SpawnTileType == 22;
		if (spawnInfo.PlayerSafe || !corrupt)
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		if (NPC.AnyNPCs(ModContent.NPCType<HiveMind>()))
		{
			return 0f;
		}
		if (NPC.downedBoss2 && !DownedBossSystem.downedHiveMind)
		{
			return 1.5f;
		}
		if (!Main.hardMode)
		{
			return 0.5f;
		}
		return 0.05f;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = 2000;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, hit.HitDirection, -1f);
			}
			if (Main.netMode != 1 && (!NPC.AnyNPCs(ModContent.NPCType<HiveMind>()) || Main.zenithWorld))
			{
				Vector2 spawnAt = base.NPC.Bottom;
				NPC.NewNPC(base.NPC.GetSource_Death(), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<HiveMind>());
			}
		}
	}
}
