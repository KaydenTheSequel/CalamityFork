using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BloodBoilerFire : ModProjectile, ILocalizedModType, IModType
{
	private bool playedSound;

	public int Time;

	public float particleSize = 15f;

	public Vector2 bloodCloudReturn;

	public bool improvedHeal;

	public bool setHomingVelocity;

	public float HomingVelocity = 0.18f;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 43;
		base.Projectile.height = 43;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 220)
		{
			particleSize += 0.5f;
		}
		else
		{
			particleSize--;
		}
		particleSize = MathHelper.Clamp(particleSize, 0f, 1000f);
		Time++;
		if (base.Projectile.timeLeft == 295)
		{
			int bloodLifetime = Main.rand.Next(22, 36);
			float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
			Color bloodColor = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : (Main.rand.NextBool() ? Color.Firebrick : Color.Red));
			float randomSpeedMultiplier = Main.rand.NextFloat(0.8f, 1.55f);
			Vector2 bloodVelocity = base.Projectile.velocity.RotatedByRandom(0.5) * randomSpeedMultiplier + new Vector2(0f, -3f);
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
		if (base.Projectile.timeLeft < 296)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.1f, "CalamityMod/Particles/PearlParticleGlow", affectedByGravity: false, 10, 0.05f * particleSize, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed, new Vector2(0.5f, 1f), useAddativeBlend: false));
			if (Main.rand.NextBool(8))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.1f, "CalamityMod/Particles/WaterFoam", affectedByGravity: false, 5, 0.01f * particleSize, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Red, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-10f, 10f)));
			}
			if (Main.rand.NextBool(6) && base.Projectile.ai[1] == 0f)
			{
				Color smokeColor = ((!ChildSafety.Disabled) ? Color.CornflowerBlue : (Main.rand.NextBool() ? Color.Firebrick : Color.Red));
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Main.rand.NextVector2Circular(5f + (float)Time * 0.3f, 5f + (float)Time * 0.3f), scale: Main.rand.NextFloat(0.5f, 1.6f), opacity: 170f + (float)(-Time) * 0.6f, velocity: Vector2.Zero, colorFire: smokeColor, colorFade: Color.Black, rotationSpeed: Main.rand.NextFloat(0.2f, -0.2f)));
			}
		}
		if (base.Projectile.ai[1] == 0f && Main.rand.NextBool(9))
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center + Main.rand.NextVector2Circular(7f + (float)Time * 0.4f, 7f + (float)Time * 0.4f), Vector2.Zero, (!ChildSafety.Disabled) ? Color.CornflowerBlue : (Main.rand.NextBool(3) ? Color.Red : Color.Firebrick), new Vector2(1f, 1f), 0f, Main.rand.NextFloat(0.02f, 0.07f) + (float)Time * 0.0006f, 0f, 35));
		}
		if (!playedSound)
		{
			SoundEngine.PlaySound(in SoundID.Item34, base.Projectile.position);
			playedSound = true;
		}
		Lighting.AddLight(base.Projectile.Center, 1f, 0f, 0f);
		if (base.Projectile.timeLeft == 235)
		{
			bloodCloudReturn = base.Projectile.velocity;
		}
		if (base.Projectile.timeLeft <= 235 && base.Projectile.ai[1] == 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.98f;
		}
		if (base.Projectile.timeLeft == 220)
		{
			base.Projectile.velocity = -bloodCloudReturn.RotatedBy((base.Projectile.ai[2] == 5f) ? (-0.7f) : 0.7f) * 1.2f;
			for (int i = 0; i <= 5; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 2f + Main.rand.NextVector2Circular(35f, 35f), (!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool(3) ? 60 : 296), base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(0.5f)) * Main.rand.NextFloat(1.7f, 3.2f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 2f);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 2f + Main.rand.NextVector2Circular(35f, 35f), (!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool(3) ? 60 : 296), base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.1f, 1.7f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(1.2f, 2f);
			}
		}
		if (base.Projectile.timeLeft == 209)
		{
			base.Projectile.ai[1] = 1f;
		}
		if (base.Projectile.ai[1] != 1f)
		{
			return;
		}
		if (!setHomingVelocity)
		{
			HomingVelocity = Main.rand.NextFloat(0.29f, 0.32f);
			setHomingVelocity = true;
		}
		base.Projectile.extraUpdates = 5;
		bool dustEffect = !Main.rand.NextBool(3);
		int dustColor = ((!ChildSafety.Disabled) ? 16 : (dustEffect ? 296 : 60));
		Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular((!dustEffect) ? 5 : 0, (!dustEffect) ? 5 : 0), dustColor);
		dust3.scale = (dustEffect ? Main.rand.NextFloat(1.1f, 1.45f) : Main.rand.NextFloat(0.9f, 1.2f));
		dust3.velocity = (dustEffect ? (base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.4f)) : (Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(0.7f, 1.2f)));
		dust3.alpha = 100;
		dust3.noLight = true;
		dust3.noGravity = true;
		Player player = Main.player[base.Projectile.owner];
		Vector2 playerCenter = player.Center;
		float xDist = playerCenter.X - base.Projectile.Center.X;
		float yDist = playerCenter.Y - base.Projectile.Center.Y;
		float dist = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
		if (dist > 3000f)
		{
			base.Projectile.Kill();
		}
		dist = 20f / dist;
		xDist *= dist;
		yDist *= dist;
		if (base.Projectile.velocity.X < xDist)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X + HomingVelocity;
			if (base.Projectile.velocity.X < 0f && xDist > 0f)
			{
				base.Projectile.velocity.X += HomingVelocity;
			}
		}
		else if (base.Projectile.velocity.X > xDist)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X - HomingVelocity;
			if (base.Projectile.velocity.X > 0f && xDist < 0f)
			{
				base.Projectile.velocity.X -= HomingVelocity;
			}
		}
		if (base.Projectile.velocity.Y < yDist)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + HomingVelocity;
			if (base.Projectile.velocity.Y < 0f && yDist > 0f)
			{
				base.Projectile.velocity.Y += HomingVelocity;
			}
		}
		else if (base.Projectile.velocity.Y > yDist)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - HomingVelocity;
			if (base.Projectile.velocity.Y > 0f && yDist < 0f)
			{
				base.Projectile.velocity.Y -= HomingVelocity;
			}
		}
		if (base.Projectile.timeLeft == 5)
		{
			base.Projectile.Center = playerCenter;
		}
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		Rectangle hitbox = base.Projectile.Hitbox;
		if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
		{
			if (improvedHeal)
			{
				BloodstoneHealOrb.Heal(player, 6);
			}
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 1200);
		target.AddBuff(ModContent.BuffType<Laceration>(), 1200);
		improvedHeal = true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.85f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}
}
