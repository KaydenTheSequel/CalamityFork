using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Events;
using CalamityMod.Items.SummonItems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class BloodwormNormal : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = 36;
		base.NPC.height = 16;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 5;
		base.NPC.knockBackResist = 0f;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.catchItem = (short)ModContent.ItemType<BloodwormItem>();
		base.NPC.dontTakeDamageFromHostiles = true;
		base.NPC.rarity = 4;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SulphurousSeaBiome>().Type };
	}

	public override void AI()
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.collideY)
		{
			if (base.NPC.ai[0] == 0f)
			{
				base.NPC.ai[0] = Main.rand.NextBool().ToDirectionInt();
				base.NPC.netUpdate = true;
			}
			if (base.NPC.collideX)
			{
				base.NPC.ai[0] *= -1f;
			}
		}
		float xSpeed = 3f;
		base.NPC.velocity.X = xSpeed * base.NPC.ai[0];
		base.NPC.spriteDirection = (int)base.NPC.ai[0];
		bool flee = false;
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (!player.dead && Vector2.Distance(player.Center, base.NPC.Center) <= 220f)
			{
				flee = true;
				break;
			}
		}
		int timeBeforeFlee = 60;
		if (flee && base.NPC.ai[1] < (float)timeBeforeFlee)
		{
			base.NPC.ai[1]++;
		}
		if (base.NPC.ai[1] == (float)timeBeforeFlee && Main.netMode != 1)
		{
			base.NPC.position.Y += 16f;
			base.NPC.Transform(ModContent.NPCType<BloodwormFleeing>());
			base.NPC.netUpdate = true;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Bloodworm")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y >= Main.npcFrameCount[base.Type] * frameHeight)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.Calamity().ZoneSulphur || AcidRainEvent.AcidRainEventIsOngoing || !NPC.downedMoonlord)
		{
			return 0f;
		}
		int bloodwormAmt = NPC.CountNPCS(base.NPC.type);
		float spawnMult = ((bloodwormAmt > 5) ? 1f : ((float)(0.16 * Math.Pow(5 - bloodwormAmt, 2.0)) + 1f));
		float num = (DownedBossSystem.downedBoomerDuke ? 0.1f : (AcidRainEvent.OldDukeHasBeenEncountered ? 0.4f : 0.2f));
		float luck = spawnInfo.Player.luck;
		if (luck > 0f && Main.rand.NextFloat() < luck)
		{
			spawnMult *= Main.rand.NextFloat(1f, 2f);
		}
		if (luck < 0f && Main.rand.NextFloat() < 0f - luck)
		{
			spawnMult *= Main.rand.NextFloat(0.5f, 1f);
		}
		return num * spawnMult;
	}
}
