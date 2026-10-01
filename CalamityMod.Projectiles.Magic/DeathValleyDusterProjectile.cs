using System;
using System.Collections.Generic;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class DeathValleyDusterProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int rotDirection = 1;

	public List<bool> buffList = new List<bool>(new bool[255]);

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 64;
		base.Projectile.height = 64;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 7;
		base.Projectile.timeLeft = 400;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0f)
		{
			rotDirection = (Main.rand.NextBool() ? 1 : (-1));
			base.Projectile.rotation = Main.rand.NextFloat(-20f, 20f);
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Gold;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		if (base.Projectile.ai[2] == 1f)
		{
			for (int playerIndex = 0; playerIndex < 255; playerIndex++)
			{
				Player player = Main.player[playerIndex];
				if (Vector2.Distance(player.Center, base.Projectile.Center) < (float)base.Projectile.width * 0.5f * base.Projectile.scale && !buffList[playerIndex])
				{
					buffList[playerIndex] = true;
					player.AddBuff(ModContent.BuffType<SandsWindBuff>(), 720);
					int Dusts = 12;
					float radians = (float)Math.PI * 2f / (float)Dusts;
					Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
					for (int i = 0; i < Dusts; i++)
					{
						Vector2 dustVelocity = spinningPoint.RotatedBy(radians * (float)i) * 12.5f;
						Vector2 center2 = player.Center;
						Vector2? velocity = dustVelocity;
						newColor = default(Color);
						Dust.NewDustPerfect(center2, 262, velocity, 0, newColor, 0.9f).noGravity = true;
						Vector2 center3 = player.Center;
						Vector2? velocity2 = dustVelocity * 0.6f;
						newColor = default(Color);
						Dust.NewDustPerfect(center3, 262, velocity2, 0, newColor, 1.2f).noGravity = true;
					}
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerPillarSummon");
					style.Volume = 0.65f;
					style.Pitch = 0.8f;
					SoundEngine.PlaySound(in style, player.Center);
				}
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (time > 80f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.984f;
		}
		else
		{
			base.Projectile.scale *= 1.004f;
		}
		if (time > 5f)
		{
			int chance = ((time > 136f) ? 3 : ((!(base.Projectile.Opacity < 1f)) ? 1 : 5));
			if (Main.rand.NextBool(chance))
			{
				for (int j = 0; j < 2; j++)
				{
					Vector2 position = base.Projectile.Center + ((float)j * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 20f * base.Projectile.scale;
					int type = (Main.rand.NextBool(10) ? 262 : 287);
					Vector2? velocity3 = ((float)j * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(((Vector2)(ref base.Projectile.velocity)).Length())).ToRotationVector2() * (float)((chance > 1) ? 7 : 3);
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, velocity3, 0, newColor);
					dust.noGravity = false;
					dust.scale = Main.rand.NextFloat(0.75f, 1.2f);
					dust.alpha = Main.rand.Next(100, 171);
					dust.velocity = dust.velocity.RotatedByRandom(0.5);
					if (dust.type == 262)
					{
						dust.noGravity = true;
					}
					if (chance > 1)
					{
						dust.noGravity = true;
					}
				}
			}
			if (Main.rand.NextBool(4))
			{
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(25f, 25f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f) * base.Projectile.scale, (-base.Projectile.velocity * 0.2f).RotatedByRandom(0.20000000298023224) + Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * (float)((time > 96f) ? 1 : 0), Color.Peru, Color.PeachPuff, Main.rand.NextFloat(0.4f, 1.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
			}
			if (time < 136f)
			{
				for (int k = 0; k < 2; k++)
				{
					Vector2 position2 = base.Projectile.Center + Utils.RotatedByRandom(new Vector2(25f, 25f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f) * base.Projectile.scale;
					int type2 = (Main.rand.NextBool(8) ? 262 : 287);
					Vector2? velocity4 = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1.3f);
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(position2, type2, velocity4, 0, newColor);
					dust2.noGravity = true;
					dust2.scale = Main.rand.NextFloat(0.4f, 0.7f);
					dust2.alpha = 100;
				}
			}
		}
		base.Projectile.rotation += Main.rand.NextFloat(0.01f, 0.18f) * (float)base.Projectile.direction * base.Projectile.Opacity * (float)rotDirection;
		if (base.Projectile.timeLeft <= 45)
		{
			base.Projectile.extraUpdates = 1;
			base.Projectile.Opacity = MathHelper.Lerp(0f, 1f, (float)base.Projectile.timeLeft / 45f);
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		float minMult = 0.25f;
		int hitsToMinMult = 6;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		if (base.Projectile.numHits == 0)
		{
			Main.player[base.Projectile.owner].SetScreenshake(3.5f);
			damageMult += 0.5f;
			for (int i = 0; i < 3; i++)
			{
				SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact with
				{
					Volume = 0.9f,
					Pitch = -0.4f + (float)i * 0.25f,
					MaxInstances = 3
				}, base.Projectile.Center);
			}
			for (int j = 0; j < 14; j++)
			{
				float range = Main.rand.NextFloat(-0.5f, 0.5f);
				float power = 1f - Math.Abs(range);
				Vector2 vel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(range) * Main.rand.NextFloat(45f, 50f) * power;
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, vel, Color.Peru, Color.PeachPuff, Main.rand.NextFloat(1.7f, 2.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
				for (int b = 0; b < 5; b++)
				{
					Color clr = Color.Lerp(Main.rand.NextBool() ? Color.Peru : Color.PeachPuff, Color.Black, Main.rand.NextFloat(0.25f, 0.45f));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, vel.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.4f, 0.9f) * power, "CalamityMod/Particles/SmallSmoke", affectedByGravity: false, Main.rand.Next(15, 26), base.Projectile.scale * Main.rand.NextFloat(0.08f, 0.14f) * 7f, clr, new Vector2(1f, Main.rand.NextFloat(0.2f, 2f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-4f, 4f), fadeIn: false, affectedByLight: true, 0f, 1f, 1f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.8f, 0.8f)));
				}
			}
		}
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits >= 5 && base.Projectile.timeLeft > 45)
		{
			base.Projectile.timeLeft = 45;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.4f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 20; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(30f, 30f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f) * base.Projectile.scale, Main.rand.NextBool(8) ? 262 : 287, (base.Projectile.velocity * 4f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1.3f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.4f, 0.7f);
			dust.alpha = 100;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * 0.5f * base.Projectile.scale, targetHitbox);
	}

	public override bool? CanDamage()
	{
		return base.Projectile.Opacity == 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 50)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.4f * base.Projectile.Opacity);
		}
		return true;
	}
}
