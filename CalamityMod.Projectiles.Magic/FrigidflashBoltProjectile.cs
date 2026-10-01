using System;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class FrigidflashBoltProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int wallBounces;

	public float fadeIn;

	public bool launch;

	public Color fireColor;

	public Color iceColor;

	public SlotId chargeSound;

	public int launchTime;

	public Vector2 endPoint;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float time => ref base.Projectile.ai[0];

	public bool bigMagic => base.Projectile.ai[1] == 5f;

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 5;
		base.Projectile.timeLeft = 480;
		base.Projectile.extraUpdates = 2;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_085b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Color lightColor = Color.Lerp(fireColor, iceColor, Utils.GetLerpValue(300f, 0f, base.Projectile.timeLeft, clamped: true));
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref lightColor)).ToVector3());
		Vector2 mouse = Owner.Calamity().mouseWorld;
		fadeIn = (launch ? Utils.GetLerpValue(0f, (float)Owner.itemAnimationMax * 0.5f * (float)base.Projectile.MaxUpdates, time, clamped: true) : 1f);
		Vector2 velocity = Owner.Center.DirectionTo(mouse) * 10f;
		base.Projectile.scale = fadeIn * (bigMagic ? 1.3f : 1f);
		if (fadeIn < 1f)
		{
			base.Projectile.Center = Owner.Center + velocity.SafeNormalize(Vector2.UnitX) * 48f;
			if (bigMagic && time % 7f == 0f)
			{
				if (time == 0f)
				{
					SoundStyle style = FrigidflashBolt.ChargeSound with
					{
						Volume = 0.6f,
						Pitch = 0.75f
					};
					chargeSound = SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				Vector2 vel = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 3f) * fadeIn;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + vel * 3f, ModContent.DustType<LightDust>(), vel);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.75f, 1.1f) * fadeIn;
				dust.color = (Main.rand.NextBool() ? iceColor : fireColor);
				dust.noLightEmittence = true;
			}
			if (SoundEngine.TryGetActiveSound(chargeSound, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
			{
				ChargeSound.Position = base.Projectile.Center;
			}
		}
		else
		{
			if (launch)
			{
				base.Projectile.tileCollide = true;
				Vector2 staticSpeed = Owner.Center.DirectionTo(mouse) * Owner.Center.Distance(Owner.ClampedMouseWorld()) * 0.01f;
				base.Projectile.velocity = (bigMagic ? staticSpeed : velocity);
				endPoint = Owner.ClampedMouseWorld();
				SoundEngine.PlaySound(FrigidflashBolt.UseSound with
				{
					Volume = 1f,
					Pitch = (bigMagic ? (-0.15f) : 0.15f)
				}, base.Projectile.Center);
				if (bigMagic)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HalleysInfernoShoot");
					style.Volume = 0.65f;
					style.Pitch = 0.45f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				for (int i = 0; i < (bigMagic ? 14 : 8); i++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(1.1f, 1.9f) * (float)((!bigMagic) ? 1 : 2));
					dust2.noGravity = false;
					dust2.scale = Main.rand.NextFloat(0.65f, 1f) * (bigMagic ? 1.5f : 1f);
					dust2.color = (Main.rand.NextBool() ? fireColor : iceColor);
					dust2.noLightEmittence = true;
					if (i % 2 == 0)
					{
						bool type = Main.rand.NextBool();
						float variance = Main.rand.NextFloat(-0.6f, 0.6f);
						float fxScale = (Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance)) * 0.4f;
						Vector2 fxVelocity = (base.Projectile.velocity * 2f).RotatedBy(variance) * Main.rand.NextFloat(0.6f, 1f) * (1f - Math.Abs(variance)) * (float)((!bigMagic) ? 1 : 2);
						GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center, fxVelocity * 1.3f, fxVelocity.RotatedBy(variance * 1.3f), "CalamityMod/Particles/FullStar", 22, fxScale, type ? fireColor : iceColor, new Vector2(2f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.3f, 0.2f));
					}
				}
				launch = false;
			}
			if (Main.rand.NextBool(bigMagic ? 3 : 8) && launchTime < 56)
			{
				bool type2 = Main.rand.NextBool();
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(12f, 12f), -base.Projectile.velocity * 0.3f, type2 ? "CalamityMod/Particles/FireTypeParticle" : "CalamityMod/Particles/IceTypeParticle", affectedByGravity: false, 32, 0.9f, type2 ? fireColor : iceColor, new Vector2(0.8f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, type2 ? 0.2f : 0f));
			}
			float velFade = Utils.GetLerpValue(1.5f, 6.5f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + (Vector2.One * 3f * base.Projectile.scale).RotatedBy(base.Projectile.rotation), -base.Projectile.velocity * 0.3f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 7, 0.2f, iceColor * 0.9f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f * velFade));
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - (Vector2.One * 3f * base.Projectile.scale).RotatedBy(base.Projectile.rotation), -base.Projectile.velocity * 0.3f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 7, 0.2f, fireColor * 0.9f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f * velFade));
		}
		if (bigMagic)
		{
			if (!launch)
			{
				base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, endPoint, 0.035f);
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.9f;
				launchTime++;
				if ((float)launchTime >= (float)Owner.itemAnimationMax * 2.5f + 28f)
				{
					base.Projectile.Kill();
				}
			}
		}
		else if (base.Projectile.timeLeft < 260)
		{
			base.Projectile.velocity.X *= 0.9711f;
			if (base.Projectile.velocity.Y < 15f)
			{
				base.Projectile.velocity.Y += 0.19f;
			}
			if (base.Projectile.velocity.Y < 5f)
			{
				base.Projectile.velocity.Y *= 0.977f;
			}
			wallBounces = 2;
		}
		else if (wallBounces > 1)
		{
			base.Projectile.timeLeft--;
		}
		base.Projectile.rotation += 0.7f * (float)base.Projectile.direction / (float)base.Projectile.MaxUpdates * base.Projectile.scale;
		time++;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (bigMagic)
		{
			base.Projectile.Kill();
			return false;
		}
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			base.Projectile.localNPCImmunity[i] = 0;
		}
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		smallMagicExplosion();
		if (wallBounces >= 2)
		{
			base.Projectile.Kill();
		}
		wallBounces++;
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		target.AddBuff(323, 60);
		target.AddBuff(324, 60);
		float minMult = 0.2f;
		int hitsToMinMult = 3;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		if (bigMagic)
		{
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FireImplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 5f).ArmorPenetration = 20;
			float rot = Main.rand.NextFloat(-2f, 2f);
			for (int i = 0; i < 6; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 6f).ToRotationVector2().RotatedBy(rot - MathHelper.ToRadians(90f)) * 8f;
				GeneralParticleHandler.SpawnParticle(new VelChangingSpark(base.Projectile.Center + velocity * 20f, -velocity * 0.2f, -velocity * 5f, "CalamityMod/Particles/IceTypeParticle", 55, 1.8f, Color.Lerp(iceColor, Color.White, 0.5f), new Vector2(1.1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, 0.15f, 0.01f));
			}
			for (int n = 0; n < 15; n++)
			{
				Vector2 vel = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(3f, 15f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + vel * 14f, ModContent.DustType<LightDust>(), -vel);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.2f, 1.7f);
				dust.color = iceColor;
				dust.noLightEmittence = true;
			}
		}
		else
		{
			if (wallBounces < 2)
			{
				smallMagicExplosion();
			}
			SoundEngine.PlaySound(in FrigidflashBolt.ProjDeathSound, base.Projectile.Center);
		}
	}

	public void smallMagicExplosion()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		float blastSize = 80f;
		float minMultiplier = 0.25f;
		int hitsToMinMult = 4;
		int debuff1 = 324;
		int debuff2 = 323;
		int debuffTime = 90;
		Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
		projectile.localAI[0] = debuff1;
		projectile.localAI[2] = debuff2;
		projectile.localAI[1] = debuffTime;
		projectile.timeLeft = 15;
		projectile.DamageType = DamageClass.Magic;
		SoundStyle style = SoundID.Item27 with
		{
			Volume = 0.5f,
			Pitch = 0.3f,
			MaxInstances = -1
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = SoundID.Item27 with
		{
			Volume = 0.5f,
			Pitch = -0.3f,
			MaxInstances = -1
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		float rot = Main.rand.NextFloat(-2f, 2f);
		for (int i = 0; i < 6; i++)
		{
			Vector2 velocity = ((float)Math.PI * 2f * (float)i / 6f).ToRotationVector2().RotatedBy(rot) * 6f;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + velocity * 3f, velocity * 0.5f, "CalamityMod/Particles/IceTypeParticle", affectedByGravity: false, 25, 1.3f, iceColor, new Vector2(1f, 1.8f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, -0.45f));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), velocity * 1.5f);
			dust.noGravity = true;
			dust.scale = 1.45f;
			dust.color = fireColor;
			dust.noLightEmittence = true;
		}
		for (int j = 0; j < 6; j++)
		{
			Vector2 velocity2 = ((float)Math.PI * 2f * (float)j / 6f).ToRotationVector2().RotatedBy(rot - MathHelper.ToRadians(90f)) * 6f;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + velocity2 * 6f, velocity2 * 0.5f, "CalamityMod/Particles/FireTypeParticle", affectedByGravity: false, 25, 1.5f, fireColor, new Vector2(1.8f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.25f));
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), velocity2 * 2f);
			dust2.noGravity = true;
			dust2.scale = 1.25f;
			dust2.color = iceColor;
			dust2.noLightEmittence = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Color drawColor = Color.Lerp(fireColor, iceColor, Utils.GetLerpValue(300f, 0f, base.Projectile.timeLeft, clamped: true));
		Projectile projectile = base.Projectile;
		Color backglowColor = drawColor;
		((Color)(ref backglowColor)).A = 0;
		projectile.DrawProjectileWithBackglow(backglowColor, Color.White, 3f * fadeIn, null, null, (SpriteEffects)0);
		return false;
	}

	public FrigidflashBoltProjectile()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		launch = true;
		fireColor = Color.OrangeRed;
		iceColor = Color.DeepSkyBlue;
		base._002Ector();
	}
}
