using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ProfanedEnergy : ModProjectile, ILocalizedModType, IModType
{
	private float count;

	public bool flapping;

	public NPC targeted;

	public int maxTargetDistance = 500;

	public int maxRechargeTime = 180;

	public int attacks;

	public int maxAttacks = 10;

	public int reactTimer;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/NPCs/NormalNPCs/ImpiousImmolator";

	public ref float attackTimer => ref base.Projectile.ai[0];

	public ref float attackCooldown => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0818: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		flapping = false;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			if (base.Projectile.frame == 2)
			{
				flapping = true;
			}
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (flapping)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity += -Vector2.UnitY * 4.6f;
		}
		else
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity += Vector2.UnitY * 0.2f;
		}
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile3 = Main.projectile[x];
			if (Vector2.Distance(base.Projectile.Center, projectile3.Center) <= 70f && projectile3.active && projectile3.type == base.Projectile.type && projectile3 != base.Projectile)
			{
				Projectile projectile4 = base.Projectile;
				projectile4.velocity += base.Projectile.Center.DirectionFrom(projectile3.Center) * 0.02f;
			}
		}
		float rate = Main.GlobalTimeWrappedHourly * 2f;
		List<Color> eColors = new List<Color>
		{
			Color.Gold,
			Color.Khaki
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color val = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		Color usedColor = Color.Lerp(val, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref usedColor)).ToVector3() * 0.8f);
		if (targeted == null)
		{
			Vector2 moveDir = base.Projectile.Center.DirectionTo(player.Center);
			if (flapping && player.Center.Distance(base.Projectile.Center) > 300f)
			{
				SoundStyle style = SoundID.DD2_WitherBeastCrystalImpact with
				{
					Volume = 0.3f,
					Pitch = 0.8f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Projectile projectile5 = base.Projectile;
				projectile5.velocity += (moveDir * Main.rand.NextFloat(3f, 5f)).RotatedByRandom(0.4000000059604645);
				for (int j = 0; j < 8; j++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(35f, 35f), ModContent.DustType<LightDust>());
					dust.velocity = -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(3f, 7f);
					dust.scale = Main.rand.NextFloat(0.5f, 0.7f);
					dust.noGravity = true;
					dust.color = Color.Orchid;
					dust.noLightEmittence = true;
				}
			}
			if (reactTimer < 600)
			{
				if (player.Center.Distance(base.Projectile.Center) < 120f)
				{
					reactTimer++;
				}
			}
			else
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SanctifiedSparkHappy");
				style.Volume = 0.5f;
				style.PitchVariance = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i < 3; i++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, (-Vector2.UnitY * Main.rand.NextFloat(3f, 5f)).RotatedByRandom(0.6000000238418579), "CalamityMod/Particles/HeartParticle", affectedByGravity: false, 75, Main.rand.NextFloat(1.3f, 1.8f), Color.Lerp(Color.Goldenrod, Color.OrangeRed, (float)i * 0.5f), Vector2.One, useAddativeBlend: true, glowCenter: true));
				}
				reactTimer = Main.rand.Next(0, 121);
			}
			if (player.HasMinionAttackTargetNPC)
			{
				targeted = Main.npc[player.MinionAttackTargetNPC];
				if (!targeted.CanBeChasedBy(base.Projectile) || base.Projectile.Center.Distance(targeted.Center) > (float)maxTargetDistance)
				{
					targeted = null;
				}
			}
			else
			{
				targeted = base.Projectile.Center.ClosestNPCAt(maxTargetDistance);
			}
			base.Projectile.spriteDirection = Math.Sign(0f - moveDir.X);
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(0f, 0.05f);
		}
		if (targeted != null && (base.Projectile.Center.Distance(targeted.Center) > (float)maxTargetDistance || attackCooldown > 0f || !targeted.active || targeted.life <= 0))
		{
			targeted = null;
		}
		if (targeted != null)
		{
			reactTimer = Main.rand.Next(0, 121);
			if (base.Projectile.owner == Main.myPlayer)
			{
				Vector2 shootVel = base.Projectile.Center.DirectionTo(targeted.Center) * 8f;
				base.Projectile.spriteDirection = Math.Sign(0f - shootVel.X);
				if (attackTimer <= 0f)
				{
					SoundEngine.PlaySound(in SoundID.Item73, base.Projectile.Center);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (shootVel * Main.rand.NextFloat(0.8f, 1.2f)).RotatedBy(0.75f * (float)((attacks % 2 != 0) ? 1 : (-1))).RotatedByRandom(0.4000000059604645), ModContent.ProjectileType<FlameBlast>(), base.Projectile.damage, 0f, base.Projectile.owner);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Goldenrod, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, Main.rand.NextFloat(0.75f, 1f), 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					attackTimer = 8f;
					attacks++;
				}
				else
				{
					attackTimer--;
				}
				if (attacks >= 18)
				{
					attackCooldown = maxRechargeTime;
					attacks = 0;
				}
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(shootVel.ToRotation() + ((base.Projectile.spriteDirection == -1) ? 0f : MathHelper.ToRadians(180f)), 0.07f);
			}
		}
		if (attackTimer > 0f)
		{
			attackTimer--;
		}
		if (attackCooldown > 0f)
		{
			int healShots = player.maxMinions / 4;
			if (healShots != 0 && attackCooldown != (float)maxRechargeTime && attackCooldown % (float)(maxRechargeTime / (healShots + 1)) == 0f)
			{
				Vector2 vel = (base.Projectile.Center - player.Center - player.velocity * 10f).SafeNormalize(Vector2.UnitX) * -10f;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vel, ModContent.ProjectileType<HolyLight>(), 0, base.Projectile.knockBack, base.Projectile.owner, 0f, 5f, 5f).extraUpdates = 2;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianDash");
				style.Volume = 0.7f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, new Color(54, 209, 54), "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 1.3f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			attackCooldown--;
		}
		Projectile projectile6 = base.Projectile;
		projectile6.velocity *= 0.97f;
		if (count == 0f)
		{
			reactTimer += Main.rand.Next(0, 121);
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
			for (int k = 0; k < 20; k++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>());
				dust2.velocity = ((float)Math.PI * 2f * (float)k / 20f).ToRotationVector2() * 15.5f * ((k % 2 == 0) ? 0.88f : 1f);
				dust2.scale = Main.rand.NextFloat(1.3f, 1.6f) * 0.8f * ((k % 2 == 0) ? 2.2f : 1.8f);
				dust2.noGravity = true;
				dust2.color = Color.Goldenrod;
				dust2.noLightEmittence = true;
			}
			count++;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
