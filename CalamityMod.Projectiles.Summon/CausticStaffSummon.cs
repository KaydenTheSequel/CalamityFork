using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CausticStaffSummon : ModProjectile, ILocalizedModType, IModType
{
	public bool initialized;

	private float debuffToInflict;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07de: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0925: Unknown result type (might be due to invalid IL or missing references)
		//IL_092a: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09db: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!initialized)
		{
			int dustAmt = 36;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(dustIndex - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int dusty = Dust.NewDust(val + faceDirection, 0, 0, 6, faceDirection.X * 1f, faceDirection.Y * 1f, 100, default(Color), 1.1f);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].noLight = true;
				Main.dust[dusty].velocity = faceDirection;
			}
			initialized = true;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<CausticStaffSummon>();
		player.AddBuff(ModContent.BuffType<CausticStaffBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.causticDragon = false;
			}
			if (modPlayer.causticDragon)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.MinionAntiClump();
		base.Projectile.tileCollide = base.Projectile.ai[0] != 1f;
		Vector2 targetPos = base.Projectile.position;
		float maxRange = 900f;
		bool foundEnemy = false;
		NPC target = base.Projectile.OwnerMinionAttackTargetNPC;
		if (target != null && target.CanBeChasedBy(base.Projectile))
		{
			float targetDist = Vector2.Distance(target.Center, base.Projectile.Center);
			if (!foundEnemy && targetDist < maxRange && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, target.position, target.width, target.height))
			{
				maxRange = targetDist;
				targetPos = target.Center;
				foundEnemy = true;
				_ = target.whoAmI;
			}
		}
		if (!foundEnemy)
		{
			for (int index = 0; index < Main.maxNPCs; index++)
			{
				NPC npc = Main.npc[index];
				if (npc.CanBeChasedBy(base.Projectile))
				{
					float targetDist2 = Vector2.Distance(npc.Center, base.Projectile.Center);
					if (!foundEnemy && targetDist2 < maxRange && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
					{
						maxRange = targetDist2;
						targetPos = npc.Center;
						foundEnemy = true;
					}
				}
			}
		}
		float maxDistanceFromPlayer = 1300f;
		if (foundEnemy)
		{
			maxDistanceFromPlayer = 1600f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > maxDistanceFromPlayer && base.Projectile.ai[0] != 1f)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (foundEnemy && base.Projectile.ai[0] == 0f)
		{
			Vector2 homeInVector = targetPos - base.Projectile.Center;
			float targetDist3 = ((Vector2)(ref homeInVector)).Length();
			((Vector2)(ref homeInVector)).Normalize();
			if (targetDist3 > 200f)
			{
				float velocity = 6f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + homeInVector * velocity) / 41f;
			}
			else if (targetDist3 < 150f)
			{
				float velocity2 = -4f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + homeInVector * velocity2) / 41f;
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.97f;
			}
		}
		else
		{
			if (!Collision.CanHitLine(base.Projectile.Center, 1, 1, player.Center, 1, 1))
			{
				base.Projectile.ai[0] = 1f;
			}
			float speed = 9f;
			if (base.Projectile.ai[0] == 1f)
			{
				speed = 22f;
			}
			Vector2 playerPos = player.Center - base.Projectile.Center;
			base.Projectile.netUpdate = true;
			int minionPosition = 1;
			for (int i = 0; i < base.Projectile.whoAmI; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj.active && proj.owner == base.Projectile.owner && proj.minion && proj.type == base.Projectile.type)
				{
					minionPosition++;
				}
			}
			playerPos.X -= 10f * (float)player.direction;
			playerPos.X -= (float)minionPosition * 40f * (float)player.direction;
			playerPos.Y -= 10f;
			float playerDist = ((Vector2)(ref playerPos)).Length();
			if (playerDist > 200f && speed < 15f)
			{
				speed = 15f;
			}
			if (playerDist < 100f && base.Projectile.ai[0] == 1f && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (playerDist > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)base.Projectile.width / 2f;
				base.Projectile.position.Y = player.Center.Y - (float)base.Projectile.width / 2f;
				base.Projectile.netUpdate = true;
			}
			if (playerDist > 10f)
			{
				((Vector2)(ref playerPos)).Normalize();
				if (playerDist < 50f)
				{
					speed /= 2f;
				}
				base.Projectile.velocity = (base.Projectile.velocity * 20f + playerPos * speed) / 21f;
			}
			else
			{
				base.Projectile.direction = player.direction;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.9f;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (Main.rand.NextBool(6))
		{
			int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[fire];
			obj.velocity *= 0.3f;
			Main.dust[fire].noGravity = true;
			Main.dust[fire].noLight = true;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = -1);
		}
		else if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = 1);
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 4);
			if (Main.rand.NextBool(3))
			{
				base.Projectile.ai[1]++;
			}
		}
		if (base.Projectile.ai[1] > 90f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] != 0f || !foundEnemy)
		{
			return;
		}
		Vector2 targetVec = targetPos - base.Projectile.Center;
		if (targetVec.X > 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = -1);
		}
		else if (targetVec.X < 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = 1);
		}
		if (base.Projectile.ai[1] != 0f)
		{
			return;
		}
		base.Projectile.ai[1]++;
		if (Main.myPlayer == base.Projectile.owner)
		{
			float speedMult = 16f;
			((Vector2)(ref targetVec)).Normalize();
			targetVec *= speedMult;
			int spike = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, targetVec, ModContent.ProjectileType<CausticStaffProjectile>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, debuffToInflict);
			debuffToInflict++;
			if (debuffToInflict >= 5f)
			{
				debuffToInflict = 0f;
			}
			Main.projectile[spike].netUpdate = true;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
