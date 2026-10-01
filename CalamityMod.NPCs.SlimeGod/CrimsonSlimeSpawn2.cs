using System;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SlimeGod;

public class CrimsonSlimeSpawn2 : ModNPC
{
	public float spikeTimer = 60f;

	public static int SmallSpikeDamage = 11;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.CrimsonSlimeSpawn.DisplayName");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 1;
		base.NPC.damage = 28;
		base.NPC.width = 40;
		base.NPC.height = 30;
		base.NPC.defense = 6;
		base.NPC.lifeMax = (BossRushEvent.BossRushActive ? 12000 : 130);
		base.NPC.knockBackResist = 0.7f;
		base.NPC.Opacity = 0.8f;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<SplitCrimulanPaladin>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CrimsonSlimeSpawn2")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		int frameY = 1;
		if (!Main.dedServ)
		{
			if (TextureAssets.Npc[base.Type].Value == null)
			{
				return;
			}
			frameY = TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type];
		}
		int aiState = 0;
		if (base.NPC.aiAction == 0)
		{
			aiState = ((!(base.NPC.velocity.Y >= 0f)) ? 2 : ((base.NPC.velocity.Y <= 0f) ? ((base.NPC.velocity.X != 0f) ? 1 : 0) : 3));
		}
		else if (base.NPC.aiAction == 1)
		{
			aiState = 4;
		}
		base.NPC.frameCounter++;
		if (aiState > 0)
		{
			base.NPC.frameCounter++;
		}
		if (aiState == 4)
		{
			base.NPC.frameCounter++;
		}
		if (base.NPC.frameCounter >= 8.0)
		{
			base.NPC.frame.Y += frameY;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y >= frameY * Main.npcFrameCount[base.Type])
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override void AI()
	{
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		if (spikeTimer > 0f)
		{
			spikeTimer--;
		}
		int type = ModContent.ProjectileType<CrimsonSpike>();
		int damage = SmallSpikeDamage;
		if (Main.zenithWorld)
		{
			type = (Main.rand.NextBool() ? ModContent.ProjectileType<IchorShot>() : ModContent.ProjectileType<BloodGeyser>());
		}
		if (base.NPC.wet)
		{
			return;
		}
		Vector2 faceDirection = default(Vector2);
		((Vector2)(ref faceDirection))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
		float targetX = Main.player[base.NPC.target].position.X + (float)Main.player[base.NPC.target].width * 0.5f - faceDirection.X;
		float targetY = Main.player[base.NPC.target].position.Y - faceDirection.Y;
		float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
		if (Main.expertMode && targetDistance < 120f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && base.NPC.velocity.Y == 0f)
		{
			base.NPC.ai[0] = -40f;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
			}
			if (Main.netMode != 1 && spikeTimer == 0f)
			{
				int projcount = (Main.zenithWorld ? 12 : 5);
				Vector2 spikeDirection = default(Vector2);
				for (int n = 0; n < projcount; n++)
				{
					((Vector2)(ref spikeDirection))._002Ector((float)(n - 2), -4f);
					spikeDirection.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
					spikeDirection.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
					((Vector2)(ref spikeDirection)).Normalize();
					spikeDirection *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), faceDirection.X, faceDirection.Y, spikeDirection.X, spikeDirection.Y, type, damage, 0f, Main.myPlayer);
					spikeTimer = 30f;
				}
			}
		}
		else if (targetDistance < 360f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height) && base.NPC.velocity.Y == 0f)
		{
			base.NPC.ai[0] = -40f;
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
			}
			if (Main.netMode != 1 && spikeTimer == 0f)
			{
				targetY = Main.player[base.NPC.target].position.Y - faceDirection.Y - (float)Main.rand.Next(0, 200);
				targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
				targetDistance = 6.5f / targetDistance;
				targetX *= targetDistance;
				targetY *= targetDistance;
				spikeTimer = 50f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), faceDirection.X, faceDirection.Y, targetX, targetY, type, damage, 0f, Main.myPlayer);
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		Color dustColor = Color.Crimson;
		((Color)(ref dustColor)).A = 150;
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f, base.NPC.alpha, dustColor);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f, base.NPC.alpha, dustColor);
			}
		}
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(8) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}
}
