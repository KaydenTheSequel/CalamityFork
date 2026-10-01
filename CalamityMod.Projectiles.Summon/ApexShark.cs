using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ApexShark : ModProjectile, ILocalizedModType, IModType
{
	private int HitCooldown;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		if (HitCooldown > 0)
		{
			HitCooldown--;
		}
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.localAI[0] == 0f)
		{
			int constant = 36;
			for (int i = 0; i < constant; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (constant / 2 - 1)) * ((float)Math.PI * 2f) / (float)constant) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int ancientDust = Dust.NewDust(val + faceDirection, 0, 0, 85, faceDirection.X * 1.75f, faceDirection.Y * 1.75f, 100, default(Color), 1.1f);
				Main.dust[ancientDust].noGravity = true;
				Main.dust[ancientDust].velocity = faceDirection;
			}
			base.Projectile.localAI[0]++;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 7)
		{
			base.Projectile.frame = 0;
		}
		float maxTargetDist = 1200f;
		bool num = base.Projectile.type == ModContent.ProjectileType<ApexShark>();
		CalamityPlayer modPlayer = player.Calamity();
		player.AddBuff(ModContent.BuffType<AncientMineralSharkBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.apexShark = false;
			}
			if (modPlayer.apexShark)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.MinionAntiClump();
		bool decelerate = false;
		if (base.Projectile.ai[0] == 2f)
		{
			base.Projectile.ai[1]++;
			base.Projectile.extraUpdates = 1;
			if (base.Projectile.ai[1] > 60f)
			{
				base.Projectile.ai[1] = 1f;
				base.Projectile.ai[0] = 0f;
				base.Projectile.extraUpdates = 0;
				base.Projectile.numUpdates = 0;
				base.Projectile.netUpdate = true;
			}
			else
			{
				decelerate = true;
			}
		}
		if (decelerate)
		{
			return;
		}
		Vector2 projPos = base.Projectile.position;
		bool canAttack = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!canAttack && targetDist < maxTargetDist)
				{
					maxTargetDist = targetDist;
					projPos = npc.Center;
					canAttack = true;
				}
			}
		}
		if (!canAttack)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC nPC2 = enumerator.Current;
				if (nPC2.CanBeChasedBy(base.Projectile))
				{
					float targetDist2 = Vector2.Distance(nPC2.Center, base.Projectile.Center);
					if (!canAttack && targetDist2 < maxTargetDist)
					{
						maxTargetDist = targetDist2;
						projPos = nPC2.Center;
						canAttack = true;
					}
				}
			}
		}
		float separationAnxietyRange = 1500f;
		if (canAttack)
		{
			separationAnxietyRange = 2200f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > separationAnxietyRange)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (canAttack && base.Projectile.ai[0] == 0f)
		{
			Vector2 projDirection = projPos - base.Projectile.Center;
			float num2 = ((Vector2)(ref projDirection)).Length();
			((Vector2)(ref projDirection)).Normalize();
			if (num2 > 200f)
			{
				float scaleFactor2 = 13f;
				projDirection *= scaleFactor2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + projDirection) / 41f;
			}
			else
			{
				projDirection *= -6f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + projDirection) / 41f;
			}
		}
		else
		{
			bool isReturning = false;
			if (!isReturning)
			{
				isReturning = base.Projectile.ai[0] == 1f;
			}
			float returnSpeed = 10f;
			if (isReturning)
			{
				returnSpeed = 21f;
			}
			Vector2 center2 = base.Projectile.Center;
			Vector2 playerDirection = player.Center - center2 + new Vector2(0f, -60f);
			float num3 = ((Vector2)(ref playerDirection)).Length();
			if (num3 > 200f && returnSpeed < 8f)
			{
				returnSpeed = 1f;
			}
			if (((num3 < 150f) & isReturning) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num3 > 2000f)
			{
				base.Projectile.position.X = Main.player[base.Projectile.owner].Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = Main.player[base.Projectile.owner].Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (num3 > 70f)
			{
				((Vector2)(ref playerDirection)).Normalize();
				playerDirection *= returnSpeed;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + playerDirection) / 41f;
			}
			else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
			{
				base.Projectile.velocity.X = -0.15f;
				base.Projectile.velocity.Y = -0.05f;
			}
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 4);
		}
		if (base.Projectile.ai[1] > 60f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] == 0f && ((base.Projectile.ai[1] == 0f) & canAttack) && maxTargetDist < 500f)
		{
			base.Projectile.ai[1]++;
			if (Main.myPlayer == base.Projectile.owner)
			{
				base.Projectile.ai[0] = 2f;
				Vector2 npcCenter = projPos - base.Projectile.Center;
				((Vector2)(ref npcCenter)).Normalize();
				base.Projectile.velocity = npcCenter * 13f;
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return HitCooldown == 0;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 90);
		}
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 90);
		SoundEngine.PlaySound(SoundID.Item70 with
		{
			Volume = SoundID.Item70.Volume * 0.5f
		}, base.Projectile.Center);
		base.Projectile.velocity = Utils.RotatedBy(new Vector2(0f, 5f), (double)(base.Projectile.velocity.ToRotation() + 1.5f), default(Vector2));
		int sand = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SandExplosion>(), (int)((float)base.Projectile.damage * 0.7f), (int)(base.Projectile.knockBack * 0.7f), base.Projectile.owner);
		Main.projectile[sand].Center = base.Projectile.Center;
		Main.projectile[sand].DamageType = DamageClass.Summon;
		base.Projectile.netUpdate = true;
		HitCooldown = 20;
	}
}
