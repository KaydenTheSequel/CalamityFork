using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AureusBomber : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 1;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = 1;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		int dustType = Utils.SelectRandom<int>(Main.rand, ModContent.DustType<AstralBlue>(), ModContent.DustType<AstralOrange>());
		if (base.Projectile.localAI[0] == 0f)
		{
			int constant = 36;
			for (int i = 0; i < constant; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (constant / 2 - 1)) * ((float)Math.PI * 2f) / (float)constant) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int astralDust = Dust.NewDust(val + faceDirection, 0, 0, dustType, faceDirection.X * 1.5f, faceDirection.Y * 1.5f, 100, default(Color), 1.4f);
				Main.dust[astralDust].noGravity = true;
				Main.dust[astralDust].noLight = true;
				Main.dust[astralDust].velocity = faceDirection;
			}
			base.Projectile.localAI[0]++;
		}
		base.Projectile.MinionAntiClump();
		Vector2 idlePosition = player.Center;
		idlePosition.Y -= 48f;
		float minionPositionOffsetX = (10 + base.Projectile.minionPos * 40) * -player.direction;
		idlePosition.X += minionPositionOffsetX;
		Vector2 vectorToIdlePosition = idlePosition - base.Projectile.Center;
		float idleDistance = ((Vector2)(ref vectorToIdlePosition)).Length();
		if (Main.rand.NextBool(10))
		{
			int index = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100, Color.Transparent, 2f);
			Dust obj = Main.dust[index];
			obj.velocity *= 0.3f;
			Main.dust[index].noGravity = true;
			Main.dust[index].noLight = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 3)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = -1);
		}
		else if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = 1);
		}
		base.Projectile.tileCollide = true;
		if (base.Projectile.ai[0] == 1f)
		{
			base.Projectile.tileCollide = false;
		}
		Vector2 objectivePos = base.Projectile.position;
		float minDist = 400f;
		bool enemyFound = false;
		NPC targetedNPC = base.Projectile.OwnerMinionAttackTargetNPC;
		if (targetedNPC != null && targetedNPC.CanBeChasedBy(base.Projectile))
		{
			float distToEnemy = Vector2.Distance(targetedNPC.Center, base.Projectile.Center);
			if ((((double)Vector2.Distance(base.Projectile.Center, objectivePos) > (double)distToEnemy && (double)distToEnemy < (double)minDist) || !enemyFound) && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, targetedNPC.position, targetedNPC.width, targetedNPC.height))
			{
				minDist = distToEnemy;
				objectivePos = targetedNPC.Center;
				enemyFound = true;
			}
		}
		if (!enemyFound)
		{
			for (int j = 0; j < Main.npc.Length; j++)
			{
				NPC npc = Main.npc[j];
				if (npc.CanBeChasedBy(base.Projectile))
				{
					float distToEnemy2 = Vector2.Distance(npc.Center, base.Projectile.Center);
					if ((((double)Vector2.Distance(base.Projectile.Center, objectivePos) > (double)distToEnemy2 && (double)distToEnemy2 < (double)minDist) || !enemyFound) && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
					{
						minDist = distToEnemy2;
						objectivePos = npc.Center;
						enemyFound = true;
					}
				}
			}
		}
		int maxDistToEnemy = 500;
		if (enemyFound)
		{
			maxDistToEnemy = 1000;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > (float)maxDistToEnemy)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (enemyFound && base.Projectile.ai[0] == 0f)
		{
			float distX = objectivePos.X - base.Projectile.Center.X;
			float distY = objectivePos.Y - base.Projectile.Center.Y;
			float distToTarget = (float)Math.Sqrt(distX * distX + distY * distY);
			distToTarget = 15f / distToTarget;
			distX *= distToTarget;
			distY *= distToTarget;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 20f + distX) / 21f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 20f + distY) / 21f;
		}
		else
		{
			if (!Collision.CanHitLine(base.Projectile.Center, 1, 1, player.Center, 1, 1))
			{
				base.Projectile.ai[0] = 1f;
			}
			float speedToPlayer = 6f;
			if (base.Projectile.ai[0] == 1f)
			{
				speedToPlayer = 15f;
			}
			Vector2 center = base.Projectile.Center;
			Vector2 playerPos = player.Center - center;
			float distToPlayer = ((Vector2)(ref playerPos)).Length();
			if (distToPlayer > 200f && speedToPlayer < 9f)
			{
				speedToPlayer = 9f;
			}
			speedToPlayer *= 0.75f;
			if (distToPlayer < idleDistance && base.Projectile.ai[0] == 1f && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (distToPlayer > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.width / 2);
			}
			if (distToPlayer > 10f)
			{
				((Vector2)(ref playerPos)).Normalize();
				if (distToPlayer < 50f)
				{
					speedToPlayer /= 2f;
				}
				base.Projectile.velocity = (base.Projectile.velocity * 20f + playerPos * speedToPlayer) / 21f;
			}
			else
			{
				base.Projectile.direction = player.direction;
				base.Projectile.velocity = base.Projectile.velocity * 0.9f;
			}
		}
		if (base.Projectile.ai[0] == 0f && enemyFound)
		{
			if ((objectivePos - base.Projectile.Center).X > 0f)
			{
				base.Projectile.spriteDirection = (base.Projectile.direction = -1);
			}
			else if ((objectivePos - base.Projectile.Center).X < 0f)
			{
				base.Projectile.spriteDirection = (base.Projectile.direction = 1);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 150);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int j = 0; j < 2; j++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 50);
		}
		for (int k = 0; k < 20; k++)
		{
			int moreAstralDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[moreAstralDust].noGravity = true;
			Dust obj = Main.dust[moreAstralDust];
			obj.velocity *= 3f;
			moreAstralDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 50);
			Dust obj2 = Main.dust[moreAstralDust];
			obj2.velocity *= 2f;
			Main.dust[moreAstralDust].noGravity = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		int dye = Owner?.cMinion ?? 0;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, null, drawCentered: true, shrink: false, dye);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
	}
}
