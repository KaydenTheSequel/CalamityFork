using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class NukeOfBliss : ModProjectile, ILocalizedModType, IModType
{
	public int reachedPeakTime = 120;

	public int rainDownTimer = 150;

	public NPC targeted;

	public float fade = 1f;

	public new string LocalizationCategory => "Projectiles.Ranged";

	private ref float RocketID => ref base.Projectile.ai[0];

	public ref float time => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 64;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 700;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Vector2.Distance(Owner.Center, base.Projectile.Center);
		Vector2 mouse = Owner.ClampedMouseWorld();
		if (base.Projectile.Center.Y > mouse.Y && rainDownTimer <= 0)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + MathHelper.ToRadians(90f) * (float)base.Projectile.direction;
		if (time > (float)reachedPeakTime)
		{
			if (rainDownTimer > 1)
			{
				base.Projectile.Center = new Vector2(mouse.X, Owner.Center.Y) + new Vector2(0f, -600f);
			}
			if (targeted == null || rainDownTimer > 0)
			{
				targeted = ((rainDownTimer == 0) ? (base.Projectile.Center + base.Projectile.velocity * 4f) : mouse).ClosestNPCAt((rainDownTimer == 0) ? 350 : 250);
			}
			if (targeted != null && base.Projectile.Center.Y > targeted.Center.Y)
			{
				targeted = null;
			}
			if (rainDownTimer >= 80)
			{
				bool isClusterRocket = RocketID == 4445f || RocketID == 4446f;
				if (rainDownTimer % 15 == 0 && Main.myPlayer == base.Projectile.owner)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastShoot");
					style.Volume = 0.45f;
					style.Pitch = 0f - 0.3f * Utils.GetLerpValue(0f, 300f, rainDownTimer, clamped: true);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					for (int i = 0; i < (isClusterRocket ? 4 : 2); i++)
					{
						Vector2 variance = ((i % 2 == 0) ? 0.2f : 1f) * (new Vector2((float)(80 * ((!isClusterRocket) ? 1 : 3)), 0f) * Main.rand.NextFloat(-1f, 1f));
						Vector2 velocity = (((targeted != null) ? targeted.Center : Owner.Calamity().mouseWorld) - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(8f, 12f) + variance * 0.008f;
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + variance, velocity, ModContent.ProjectileType<BlissfulBombardierSplitProjectile>(), (int)((float)base.Projectile.damage * (isClusterRocket ? 0.15f : 0.3f)), base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0]);
					}
				}
			}
			if (rainDownTimer > 0)
			{
				rainDownTimer--;
			}
			if (rainDownTimer == 65)
			{
				for (int j = 0; j < 3; j++)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MissileNearing");
					style.Volume = 0.6f;
					style.Pitch = 0.4f;
					style.MaxInstances = 2;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			if (rainDownTimer == 1)
			{
				targeted = null;
				base.Projectile.extraUpdates = 10;
				base.Projectile.penetrate = 1;
				base.Projectile.velocity = (((targeted != null) ? targeted.Center : mouse) - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 15f;
			}
			if (rainDownTimer == 0)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 19, 1.7f, Color.Goldenrod));
				if (targeted != null && targeted.Center.Y > base.Projectile.Center.Y)
				{
					Vector2 moveToTrackingPos = (((targeted != null) ? targeted.Center : mouse) - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
					if (((Vector2)(ref base.Projectile.velocity)).Length() < 15f)
					{
						base.Projectile.velocity = base.Projectile.velocity * 0.98f + moveToTrackingPos * 2.5f;
					}
					else
					{
						Projectile projectile = base.Projectile;
						projectile.velocity *= 0.9f;
					}
				}
			}
		}
		else
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.995f;
		}
		fade = ((rainDownTimer > 0) ? Utils.GetLerpValue(reachedPeakTime, (float)reachedPeakTime * 0.7f, time, clamped: true) : 1f);
		if (fade > 0.2f)
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.6f), BlissfulBombardierHoldout.staticEffectsColor, Main.rand.Next(40, 61), Main.rand.NextFloat(0.3f, 0.6f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
		}
		if (RocketID == 4459f || RocketID == 4447f || RocketID == 4448f || RocketID == 4449f)
		{
			base.Projectile.ignoreWater = false;
			if (base.Projectile.wet)
			{
				base.Projectile.timeLeft = 1;
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 300);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		CalamityUtils.RocketBehaviorInfo rocketBehaviorInfo = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
		rocketBehaviorInfo.clusterProjectileID = 0;
		rocketBehaviorInfo.destructiveClusterProjectileID = 0;
		CalamityUtils.RocketBehaviorInfo info = rocketBehaviorInfo;
		if (RocketID == 4445f)
		{
			_ = 1;
		}
		else
			_ = RocketID == 4446f;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MineralMortarExplode");
		style.Volume = 0.9f;
		style.Pitch = 0.4f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = new SoundStyle("CalamityMod/Sounds/Item/BlazingCoreParry");
		style.Volume = 0.9f;
		style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int blastRadius = MathHelper.Clamp(base.Projectile.RocketBehavior(info), 3, 100);
		base.Projectile.ExpandHitboxBy((float)blastRadius);
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.5f);
		base.Projectile.penetrate = -1;
		base.Projectile.Damage();
		float blastRadiusVisual = (float)blastRadius * 0.5f;
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, BlissfulBombardierHoldout.staticEffectsColor, "CalamityMod/Particles/SoftRoundExplosion", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.07f * blastRadiusVisual, 19, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, BlissfulBombardierHoldout.effectsColor, "CalamityMod/Particles/SoftRoundExplosion", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.05f * blastRadiusVisual, 19, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 3; i++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/SmallBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.45f * blastRadiusVisual, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		for (int j = 0; j < 40; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>());
			dust.velocity = ((float)Math.PI * 2f * (float)j / 40f).ToRotationVector2() * 8.5f * ((j % 2 == 0) ? 0.88f : 1f) * blastRadiusVisual;
			dust.scale = Main.rand.NextFloat(0.3f, 0.6f) * (float)blastRadius * 0.3f * ((j % 2 == 0) ? 2.2f : 1.8f);
			dust.noGravity = true;
			dust.color = BlissfulBombardierHoldout.effectsColor;
			dust.noLightEmittence = true;
		}
		for (int k = 0; k < 25; k++)
		{
			if (k < 14)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * blastRadiusVisual * Main.rand.NextFloat(0.3f, 1f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 27, Main.rand.NextFloat(2.45f, 2.7f), Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f)), new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.35f, 0.4f)));
				continue;
			}
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 278);
			dust2.velocity = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * blastRadiusVisual * Main.rand.NextFloat(0.4f, 1f);
			dust2.scale = Main.rand.NextFloat(0.6f, 0.8f) * blastRadiusVisual * 0.2f * ((k % 2 == 0) ? 2.2f : 1.8f);
			dust2.noGravity = false;
			dust2.color = BlissfulBombardierHoldout.staticEffectsColor;
		}
		for (int l = 0; l < 15; l++)
		{
			Vector2 velocity = Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0) * blastRadiusVisual * Main.rand.NextFloat(0.4f, 1f);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, velocity, "CalamityMod/Projectiles/Boss/ProvidenceCrystal", affectedByGravity: false, 12, 0.15f * (float)blastRadius, Color.White, new Vector2(1.5f, 0.4f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.7f));
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.35f;
		int hitsToMinMult = 10;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult * ((rainDownTimer <= 0) ? 1f : 0.2f);
	}

	public override bool? CanDamage()
	{
		if (!(fade <= 0.2f))
		{
			return null;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/NukeOfBliss", (AssetRequestMode)2).Value;
		float fade2 = ((rainDownTimer > 0) ? Utils.GetLerpValue(reachedPeakTime, (float)reachedPeakTime * 0.7f, time) : 1f);
		Projectile projectile = base.Projectile;
		Color staticEffectsColor = BlissfulBombardierHoldout.staticEffectsColor;
		((Color)(ref staticEffectsColor)).A = 0;
		projectile.DrawProjectileWithBackglow(staticEffectsColor * fade2, lightColor * fade2, 6f * Utils.GetLerpValue(0f, reachedPeakTime, time, clamped: true), texture, null, (SpriteEffects)0);
		return false;
	}
}
