using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HowlsHeartHowl : ModProjectile, ILocalizedModType, IModType
{
	public bool initialized;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 72;
		base.Projectile.height = 54;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
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
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!initialized)
		{
			int dustAmt = 36;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(dustIndex - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int dusty = Dust.NewDust(val + faceDirection, 0, 0, 191, faceDirection.X * 1f, faceDirection.Y * 1f, 100, default(Color), 1.1f);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].noLight = true;
				Main.dust[dusty].velocity = faceDirection;
			}
			initialized = true;
		}
		bool correctMinion = base.Projectile.type == ModContent.ProjectileType<HowlsHeartHowl>();
		if ((!modPlayer.howlsHeart && !modPlayer.howlsHeartVanity) || !player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (correctMinion)
		{
			if (player.dead)
			{
				modPlayer.howlTrio = false;
			}
			if (modPlayer.howlTrio)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.tileCollide = base.Projectile.ai[0] != 1f;
		Vector2 targetPos = base.Projectile.position;
		float maxRange = 900f;
		bool foundEnemy = false;
		int targetIndex = -1;
		NPC target = base.Projectile.OwnerMinionAttackTargetNPC;
		if (target != null && target.CanBeChasedBy(base.Projectile) && !modPlayer.howlsHeartVanity)
		{
			float targetDist = Vector2.Distance(target.Center, base.Projectile.Center);
			if (!foundEnemy && targetDist < maxRange && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, target.position, target.width, target.height))
			{
				maxRange = targetDist;
				targetPos = target.Center;
				foundEnemy = true;
				targetIndex = target.whoAmI;
			}
		}
		if (!foundEnemy && !modPlayer.howlsHeartVanity)
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
						targetIndex = index;
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
				if (proj.active && proj.owner == base.Projectile.owner && proj.type == base.Projectile.type)
				{
					minionPosition++;
				}
			}
			playerPos.X -= 10f * (float)player.direction;
			playerPos.X -= (float)minionPosition * 60f * (float)player.direction;
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
				base.Projectile.direction = -player.direction;
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
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1]++;
			if (Main.myPlayer == base.Projectile.owner)
			{
				float speedMult = 16f;
				((Vector2)(ref targetVec)).Normalize();
				targetVec *= speedMult;
				SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
				int fireball = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, targetVec, ModContent.ProjectileType<HowlsHeartFireball>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, targetIndex);
				Main.projectile[fireball].netUpdate = true;
				Main.projectile[fireball].frame = Main.rand.Next(4);
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
