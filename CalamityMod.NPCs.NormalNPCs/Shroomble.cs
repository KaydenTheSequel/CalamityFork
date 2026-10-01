using System;
using CalamityMod.Items.Critters;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class Shroomble : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.chaseable = false;
		base.NPC.damage = 0;
		base.NPC.width = 28;
		base.NPC.height = 24;
		base.NPC.lifeMax = 25;
		base.NPC.aiStyle = 7;
		base.AIType = 299;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.DrawOffsetY = -2f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ShroombleBanner>();
		base.NPC.catchItem = (short)ModContent.ItemType<ShroombleItem>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void AI()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1 && Main.BestiaryTracker.Kills.GetKillCount(base.NPC) <= 0)
		{
			Main.BestiaryTracker.Kills.RegisterKill(base.NPC);
		}
		bool walking = base.NPC.ai[0] == 1f;
		int jumpChance = (walking ? 600 : 100);
		if (base.NPC.velocity.Y == 0f && Main.rand.NextBool(jumpChance) && base.NPC.ai[2] <= 0f)
		{
			SoundEngine.PlaySound(in HarvestStaffMinion.JumpSound, base.NPC.Center);
			base.NPC.velocity.Y = Main.rand.NextFloat(-6f, -4f);
			base.NPC.ai[2] = (walking ? 180 : 90);
			base.NPC.ai[1] += 60f;
		}
		if (!walking && Main.rand.NextBool(200) && base.NPC.velocity.Y == 0f)
		{
			base.NPC.direction *= -1;
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.ai[2]--;
		}
		if (base.NPC.ai[1] > 300f)
		{
			base.NPC.ai[1] = 300f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Shroomble")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.ZonePurity)
		{
			return 0f;
		}
		return (Main.remixWorld ? SpawnCondition.Cavern.Chance : SpawnCondition.OverworldDay.Chance) * 0.1f;
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
			base.NPC.frameCounter += (base.NPC.IsABestiaryIconDummy ? 0.6f : (Math.Abs(base.NPC.velocity.X) * 0.5f));
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
			base.NPC.frame.Y = frameHeight * (Main.npcFrameCount[base.Type] - 1);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(5);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 55, (float)hit.HitDirection * 0.5f, -0.5f, 0, default(Color), 0.5f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 55, (float)hit.HitDirection * 0.5f, -0.5f, 0, default(Color), 0.5f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("Shroomble").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("Shroomble2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("Shroomble3").Type);
			}
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			return new Color(Main.DiscoR, Main.DiscoB, Main.DiscoG, (int)((Color)(ref drawColor)).A) * base.NPC.Opacity;
		}
		return null;
	}
}
