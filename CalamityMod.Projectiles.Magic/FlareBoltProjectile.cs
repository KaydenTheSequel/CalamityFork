using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class FlareBoltProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int wallBounces;

	public float fadeIn;

	public bool launch;

	public Color bColor;

	public int launchTime;

	public Vector2 endPoint;

	public ref float time => ref base.Projectile.ai[0];

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 500;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref bColor)).ToVector3());
		Vector2 mouse = Owner.Calamity().mouseWorld;
		fadeIn = Utils.GetLerpValue(0f, (float)Owner.itemAnimationMax * 0.5f * (float)base.Projectile.MaxUpdates, time, clamped: true);
		float velFade = Utils.GetLerpValue(1.5f, 6.5f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		Vector2 velocity = Owner.Center.DirectionTo(mouse) * 8f;
		base.Projectile.scale = fadeIn * 1.5f;
		if (fadeIn < 1f)
		{
			base.Projectile.Center = Owner.Center + velocity.SafeNormalize(Vector2.UnitX) * 48f;
		}
		else
		{
			if (launch)
			{
				base.Projectile.tileCollide = true;
				Vector2 staticSpeed = Owner.Center.DirectionTo(mouse) * base.Projectile.Center.Distance(Owner.ClampedMouseWorld()) * 0.0165f;
				base.Projectile.velocity = staticSpeed;
				endPoint = Owner.ClampedMouseWorld();
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HalleysInfernoShoot");
				style.Volume = 0.45f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i < 20; i++)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1.5f, 3.5f));
					dust.noGravity = false;
					dust.scale = Main.rand.NextFloat(0.85f, 1.4f);
					dust.color = bColor;
					dust.noLightEmittence = true;
					if (i % 3 == 0)
					{
						float variance = Main.rand.NextFloat(-0.6f, 0.6f);
						float fxScale = Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance);
						Vector2 fxVelocity = base.Projectile.velocity.RotatedBy(variance) * Main.rand.NextFloat(0.6f, 1f) * (1f - Math.Abs(variance));
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, fxVelocity * 1.3f, "CalamityMod/Particles/FireTypeParticle", affectedByGravity: false, 22, fxScale, Color.Lerp(bColor, Color.Red, 0.5f), new Vector2(1.8f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f));
					}
				}
				launch = false;
			}
			if (Main.rand.NextBool(8) && launchTime < 56)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), -base.Projectile.velocity * 0.9f, "CalamityMod/Particles/FireTypeParticle", affectedByGravity: false, 32, 1.15f, Color.Lerp(bColor, Color.Red, 0.5f), new Vector2(0.8f, 1f)));
			}
			if (Main.rand.NextBool(6))
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), ModContent.DustType<LightDust>(), -base.Projectile.velocity * 0.5f);
				dust2.noGravity = false;
				dust2.scale = Main.rand.NextFloat(0.85f, 1.4f);
				dust2.color = bColor;
				dust2.noLightEmittence = true;
			}
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity * 0.3f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 11, 0.3f, bColor * 0.9f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.7f * velFade));
		}
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
		time++;
		if (!launch)
		{
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, endPoint, 0.035f);
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
			launchTime++;
			if (launchTime >= Owner.itemAnimationMax + 28)
			{
				base.Projectile.Kill();
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		target.AddBuff(24, 90);
		float minMult = 0.1f;
		int hitsToMinMult = 5;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FireImplosion>(), (int)((float)base.Projectile.damage * 0.75f), base.Projectile.knockBack, base.Projectile.owner);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		Color backglowColor = bColor;
		((Color)(ref backglowColor)).A = 0;
		projectile.DrawProjectileWithBackglow(backglowColor, Color.White, 2f * fadeIn, null, null, (SpriteEffects)0);
		return false;
	}

	public FlareBoltProjectile()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		launch = true;
		bColor = Color.OrangeRed;
		base._002Ector();
	}
}
