using System;
using CalamityMod.Events;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.OldDuke;

public class OldDukeToothBall : ModNPC
{
	public static int ToothDamage = 55;

	public static int CloudDamage = 70;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 120;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 8000;
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 16000;
		}
		base.NPC.knockBackResist = 0.2f;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath11;
		base.NPC.chaseable = false;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
	}

	public override void AI()
	{
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.65f, 0.55f, 0f);
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		base.NPC.rotation += base.NPC.velocity.X * 0.05f;
		base.NPC.TargetClosest(faceTarget: false);
		Player player = Main.player[base.NPC.target];
		if (!player.active || player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead)
			{
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 600)
		{
			base.NPC.timeLeft = 600;
		}
		Vector2 vector = player.Center - base.NPC.Center;
		float cannonballMovementGateValue = 120f;
		float slowDownGateValue = cannonballMovementGateValue + 300f;
		float dieGateValue = slowDownGateValue + 60f;
		base.NPC.ai[3]++;
		if (((Vector2)(ref vector)).Length() < 40f || base.NPC.ai[3] >= dieGateValue)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			return;
		}
		if (base.NPC.ai[3] < cannonballMovementGateValue)
		{
			Vector2 finalCannonballVelocity = default(Vector2);
			((Vector2)(ref finalCannonballVelocity))._002Ector(base.NPC.ai[0], base.NPC.ai[1]);
			if (((Vector2)(ref base.NPC.velocity)).Length() < ((Vector2)(ref finalCannonballVelocity)).Length())
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 1.01f;
				if (((Vector2)(ref base.NPC.velocity)).Length() > ((Vector2)(ref finalCannonballVelocity)).Length())
				{
					((Vector2)(ref base.NPC.velocity)).Normalize();
					NPC nPC2 = base.NPC;
					nPC2.velocity *= ((Vector2)(ref finalCannonballVelocity)).Length();
				}
			}
			return;
		}
		if (base.NPC.ai[3] > slowDownGateValue)
		{
			NPC nPC3 = base.NPC;
			nPC3.velocity *= 0.95f;
			return;
		}
		float velocity = (death ? 14f : (revenge ? 13f : 12f));
		if (expertMode)
		{
			float speedUpMult = 0.005f;
			velocity += Vector2.Distance(player.Center, base.NPC.Center) * speedUpMult;
		}
		Vector2 toothBallDirection = default(Vector2);
		((Vector2)(ref toothBallDirection))._002Ector(base.NPC.Center.X + (float)(base.NPC.direction * 20), base.NPC.Center.Y + 6f);
		float targetXDist = player.position.X + (float)player.width * 0.5f - toothBallDirection.X;
		float targetYDist = player.Center.Y - toothBallDirection.Y;
		float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
		float toothBallSpeed = velocity / targetDistance;
		targetXDist *= toothBallSpeed;
		targetYDist *= toothBallSpeed;
		base.NPC.ai[2] -= Main.rand.Next(6);
		if (targetDistance < 300f || base.NPC.ai[2] > 0f)
		{
			if (targetDistance < 300f)
			{
				base.NPC.ai[2] = 100f;
			}
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			return;
		}
		float inertia = 50f;
		base.NPC.velocity.X = (base.NPC.velocity.X * inertia + targetXDist) / (inertia + 1f);
		base.NPC.velocity.Y = (base.NPC.velocity.Y * inertia + targetYDist) / (inertia + 1f);
		float toothBallAccel = 0.5f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && Vector2.Distance(base.NPC.Center, n.Center) < 48f)
			{
				if (base.NPC.position.X < n.position.X)
				{
					base.NPC.velocity.X -= toothBallAccel;
				}
				else
				{
					base.NPC.velocity.X += toothBallAccel;
				}
				if (base.NPC.position.Y < n.position.Y)
				{
					base.NPC.velocity.Y -= toothBallAccel;
				}
				else
				{
					base.NPC.velocity.Y += toothBallAccel;
				}
			}
		}
	}

	public override void OnKill()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 15; i++)
		{
			float sc = Main.rand.NextFloat(1f, 3f);
			Vector2 vel = Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f, 25f), 0f), 6.2831854820251465);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.NPC.Center, vel, "CalamityMod/Projectiles/Boss/OldDukeToothBallSpike", affectedByGravity: true, 40, sc + 0.5f, OldDuke.GlowColor, Vector2.One));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.NPC.Center, vel, "CalamityMod/Projectiles/Boss/OldDukeToothBallSpike", affectedByGravity: true, 40, sc, Color.White, Vector2.One, useAddativeBlend: false, glowCenter: false, 0f, fadeIn: false, affectedByLight: true));
		}
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(8) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
		if (Main.netMode != 1)
		{
			int totalProjectiles = (CalamityWorld.death ? 5 : (CalamityWorld.revenge ? 4 : 3));
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			int type = ModContent.ProjectileType<OldDukeToothBallSpike>();
			float velocity = 10f;
			double angleA = (double)radians * 0.5;
			double angleB = (double)MathHelper.ToRadians(90f) - angleA;
			float velocityX = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
			Vector2 spinningPoint = (Main.rand.NextBool() ? new Vector2(0f, 0f - velocity) : new Vector2(0f - velocityX, 0f - velocity));
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 toothSpikeRotation = spinningPoint.RotatedBy(radians * (float)k);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, toothSpikeRotation * 0.1f, type, ToothDamage, 0f, Main.myPlayer, toothSpikeRotation.X, toothSpikeRotation.Y);
			}
			if (Main.expertMode)
			{
				type = ModContent.ProjectileType<SandPoisonCloudOldDuke>();
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type, CloudDamage, 0f, Main.myPlayer);
			}
		}
		if (Main.zenithWorld && Main.netMode != 1)
		{
			int spawnX = base.NPC.width / 2;
			int type2 = ModContent.ProjectileType<OldDukeGore>();
			for (int j = 0; j < 2; j++)
			{
				Projectile.NewProjectile(base.NPC.GetSource_Death(), base.NPC.Center.X + (float)Main.rand.Next(-spawnX, spawnX), base.NPC.Center.Y, Main.rand.Next(-1, 2), Main.rand.Next(-6, -3), type2, OldDuke.GoreDamage, 0f, Main.myPlayer, 0f, 0f, 0f);
			}
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		cooldownSlot = 1;
		return true;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		float velDist = ((Vector2)(ref base.NPC.velocity)).Length();
		Vector2 vel = base.NPC.velocity;
		((Vector2)(ref vel)).Normalize();
		if (velDist > 5f && Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.NPC.Center + Utils.RotatedBy(new Vector2(Main.rand.NextFloat(5f, 30f), 0f), (double)Main.rand.NextFloat((float)Math.PI * 2f), default(Vector2)), base.NPC.velocity, affectedByGravity: false, 20, Main.rand.NextFloat(0.5f, 1.5f), OldDuke.GlowColor, fadeIn: true));
		}
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.NPC.Center, -(vel * 4f).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f)), OldDuke.GlowColor, Color.DarkSlateGray, Main.rand.NextFloat(0.5f, 1.5f), 150f));
		}
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		for (int i = 0; i < 360; i += 90)
		{
			Main.EntitySpriteDraw(tex.Value, base.NPC.Center + new Vector2(0f, 4f) - Main.screenPosition + Utils.RotatedBy(new Vector2(4f, 0f), (double)MathHelper.ToRadians((float)i), default(Vector2)), tex.Frame(), OldDuke.GlowColor, base.NPC.rotation, tex.Frame().Center(), base.NPC.scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(tex.Value, base.NPC.Center + new Vector2(0f, 4f) - Main.screenPosition + Utils.RotatedBy(new Vector2(8f, 0f), (double)MathHelper.ToRadians((float)i), default(Vector2)), tex.Frame(), OldDuke.GlowColor, base.NPC.rotation, tex.Frame().Center(), base.NPC.scale, (SpriteEffects)0);
		}
		return base.PreDraw(spriteBatch, screenPos, drawColor);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 15; i++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = (base.NPC.height = 96);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int j = 0; j < 15; j++)
		{
			int bloody = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[bloody];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[bloody].scale = 0.5f;
				Main.dust[bloody].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
			Main.dust[bloody].noGravity = true;
		}
		for (int l = 0; l < 30; l++)
		{
			int toxicDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, 0f, 0f, 100, default(Color), 3f);
			Main.dust[toxicDust].noGravity = true;
			Dust obj2 = Main.dust[toxicDust];
			obj2.velocity *= 5f;
			toxicDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[toxicDust];
			obj3.velocity *= 2f;
			Main.dust[toxicDust].noGravity = true;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("OldDukeToothBallGore").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("OldDukeToothBallGore2").Type, base.NPC.scale);
		}
	}
}
