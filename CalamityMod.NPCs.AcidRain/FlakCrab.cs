using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class FlakCrab : ModNPC
{
	public const int TotalHitsNeededToDoDamage = 10;

	public Player Target => Main.player[base.NPC.target];

	public ref float ChasabilityTimer => ref base.NPC.ai[0];

	public ref float AcidShootTimer => ref base.NPC.ai[1];

	public ref float HopTimer => ref base.NPC.ai[2];

	public ref float HopCounter => ref base.NPC.ai[3];

	public ref float FleeCountdownTimer => ref base.NPC.localAI[0];

	public ref float TotalHits => ref base.NPC.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 28;
		base.NPC.height = 70;
		base.NPC.damage = 10;
		base.NPC.lifeMax = 300;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.lifeMax = 4200;
		}
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 4);
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit41;
		base.NPC.DeathSound = SoundID.DD2_WitherBeastDeath;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<FlakCrabBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.FlakCrab")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(FleeCountdownTimer);
		writer.Write(TotalHits);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		FleeCountdownTimer = reader.ReadSingle();
		TotalHits = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		ChasabilityTimer++;
		base.NPC.defense = ((TotalHits < 10f) ? 999999 : 20);
		if (base.NPC.justHit)
		{
			FleeCountdownTimer = 240f;
			base.NPC.netUpdate = true;
		}
		if (FleeCountdownTimer == 0f || TotalHits < 10f)
		{
			if (ChasabilityTimer < 300f)
			{
				base.NPC.chaseable = false;
				base.NPC.knockBackResist = 0f;
			}
			AcidShootTimer++;
			Player closestTargetToTop = Main.player[Player.FindClosest(base.NPC.Top, 0, 0)];
			if (Math.Abs(closestTargetToTop.Center.X - base.NPC.Center.X) < 320f && closestTargetToTop.Center.Y - base.NPC.Top.Y < -60f && AcidShootTimer >= (float)Main.rand.Next(90, 135))
			{
				ShootFlakAcidAtTarget(closestTargetToTop);
			}
			base.NPC.velocity.X *= 0.97f;
		}
		else
		{
			FleeCountdownTimer--;
			if (base.NPC.velocity.Y == 0f)
			{
				HopTimer++;
				base.NPC.knockBackResist = 0.6f;
				base.NPC.TargetClosest();
				base.NPC.velocity.X *= 0.85f;
				float hopRate = MathHelper.Lerp(25f, 10f, 1f - (float)base.NPC.life / (float)base.NPC.lifeMax);
				float lungeForwardSpeed = 10f;
				float jumpSpeed = 9f;
				if (Collision.CanHit(base.NPC.Center, 1, 1, Target.Center, 1, 1))
				{
					lungeForwardSpeed *= 1.5f;
				}
				if (Main.netMode != 1 && HopTimer > hopRate)
				{
					HopCounter++;
					if (HopCounter % 3f == 2f)
					{
						lungeForwardSpeed *= 1.5f;
					}
					HopTimer = 0f;
					base.NPC.velocity.Y -= jumpSpeed;
					base.NPC.velocity.X = lungeForwardSpeed * (float)(-base.NPC.direction);
					base.NPC.netUpdate = true;
				}
			}
			else
			{
				base.NPC.knockBackResist = 0.2f;
				base.NPC.velocity.X *= 0.995f;
			}
			base.NPC.chaseable = true;
		}
		if (ChasabilityTimer >= 300f && !base.NPC.chaseable)
		{
			base.NPC.chaseable = true;
		}
	}

	public void ShootFlakAcidAtTarget(Player closestTargetToTop)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1)
		{
			float speed = (DownedBossSystem.downedPolterghast ? 29f : 17f);
			speed *= Main.rand.NextFloat(0.8f, 1.2f);
			int damage = ((!Main.masterMode) ? ((!Main.expertMode) ? (DownedBossSystem.downedPolterghast ? 42 : 23) : (DownedBossSystem.downedPolterghast ? 32 : 18)) : (DownedBossSystem.downedPolterghast ? 27 : 15));
			Vector2 spawnPosition = base.NPC.Top + Vector2.UnitY * 6f;
			Vector2 shootVelocity = (closestTargetToTop.Center - spawnPosition).SafeNormalize(Vector2.UnitY).RotatedByRandom(0.25) * speed;
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition, shootVelocity, ModContent.ProjectileType<FlakAcid>(), damage, 2f);
			AcidShootTimer = 0f;
			base.NPC.netUpdate = true;
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		if (FleeCountdownTimer == 0f || TotalHits < 10f)
		{
			return false;
		}
		return null;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<FlakToxicannon>(), 10);
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(() => DownedBossSystem.downedPolterghast);
		mainRule.Add(ModContent.ItemType<CorrodedFossil>(), 15, 1, 3, !DownedBossSystem.downedPolterghast);
		mainRule.AddFail(ModContent.ItemType<CorrodedFossil>(), 3, 1, 3, DownedBossSystem.downedPolterghast);
	}

	public override void FindFrame(int frameHeight)
	{
		if (TotalHits < 10f && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frame.Y = 0;
		}
		else if (FleeCountdownTimer > 0f || base.NPC.IsABestiaryIconDummy)
		{
			if (base.NPC.frameCounter++ % 6.0 == 5.0)
			{
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type])
			{
				base.NPC.frame.Y = frameHeight * 3;
			}
			if (FleeCountdownTimer <= 8f && !base.NPC.IsABestiaryIconDummy)
			{
				base.NPC.frame.Y = frameHeight;
			}
		}
		else
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("FlakCrabGore1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("FlakCrabGore2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("FlakCrabGore3").Type);
			}
		}
		TotalHits++;
		base.NPC.netUpdate = true;
	}
}
