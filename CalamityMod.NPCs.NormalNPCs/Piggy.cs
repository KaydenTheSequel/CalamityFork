using System;
using CalamityMod.Items.Critters;
using CalamityMod.Items.Placeables.Banners;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class Piggy : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
		NPCID.Sets.NormalGoldCritterBestiaryPriority.Add(base.Type);
	}

	public override void SetDefaults()
	{
		base.NPC.chaseable = false;
		base.NPC.damage = 0;
		base.NPC.width = 26;
		base.NPC.height = 26;
		base.NPC.lifeMax = 2000;
		base.NPC.aiStyle = 7;
		base.AIType = 299;
		base.NPC.knockBackResist = 0.99f;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<PiggyBanner>();
		base.NPC.catchItem = (short)ModContent.ItemType<PiggyItem>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void AI()
	{
		if (Main.netMode != 1 && Main.BestiaryTracker.Kills.GetKillCount(base.NPC) <= 0)
		{
			Main.BestiaryTracker.Kills.RegisterKill(base.NPC);
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Piggy")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSulphur || spawnInfo.Player.Calamity().ZoneSunkenSea)
		{
			return 0f;
		}
		return SpawnCondition.TownCritter.Chance * 0.005f;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.velocity.Y == 0f)
		{
			if (!base.NPC.IsABestiaryIconDummy)
			{
				if (base.NPC.direction == 1)
				{
					base.NPC.spriteDirection = -1;
				}
				if (base.NPC.direction == -1)
				{
					base.NPC.spriteDirection = 1;
				}
				if (base.NPC.velocity.X == 0f)
				{
					base.NPC.frame.Y = 0;
					base.NPC.frameCounter = 0.0;
					return;
				}
			}
			base.NPC.frameCounter += (base.NPC.IsABestiaryIconDummy ? 0.6f : (Math.Abs(base.NPC.velocity.X) * 0.25f));
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 12.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y / frameHeight >= Main.npcFrameCount[base.Type] - 1)
			{
				base.NPC.frame.Y = frameHeight;
			}
		}
		else
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = frameHeight * 2;
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(3532);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}
}
