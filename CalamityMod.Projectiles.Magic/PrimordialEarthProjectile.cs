using System;
using System.Collections.Generic;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PrimordialEarthProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int rotDirection = 1;

	public float curve;

	public List<bool> buffList = new List<bool>(new bool[255]);

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/Magic/DeathValleyDusterProjectile";

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
		base.Projectile.extraUpdates = 9;
		base.Projectile.timeLeft = 132;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0f)
		{
			rotDirection = ((base.Projectile.ai[1] == 1f) ? 1 : (-1));
			base.Projectile.rotation = Main.rand.NextFloat(-20f, 20f);
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Gold;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		if (time < 200f)
		{
			curve = MathHelper.Lerp(curve, 0.04f, 0.004f);
		}
		else
		{
			curve = MathHelper.Lerp(curve, -0.05f, 0.004f);
		}
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy((base.Projectile.ai[1] == 1f) ? curve : (0f - curve));
		if (base.Projectile.ai[2] == 1f)
		{
			for (int playerIndex = 0; playerIndex < 255; playerIndex++)
			{
				Player player = Main.player[playerIndex];
				if (Vector2.Distance(player.Center, base.Projectile.Center) < (float)base.Projectile.width * 0.5f * base.Projectile.scale && !buffList[playerIndex])
				{
					buffList[playerIndex] = true;
					player.AddBuff(ModContent.BuffType<SandsWindBuff>(), 840);
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
		if (time < 80f)
		{
			base.Projectile.scale *= 1.004f;
		}
		if (time > 5f)
		{
			int chance = ((!Main.rand.NextBool(3)) ? 1 : 2);
			if (Main.rand.NextBool(chance))
			{
				for (int j = 0; j < 2; j++)
				{
					Vector2 position = base.Projectile.Center + ((float)j * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 20f * base.Projectile.scale;
					int type = (Main.rand.NextBool(6) ? 262 : 287);
					Vector2? velocity3 = ((float)j * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(((Vector2)(ref base.Projectile.velocity)).Length())).ToRotationVector2() * (float)((chance > 1) ? 7 : 3);
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, velocity3, 0, newColor);
					dust.noGravity = false;
					dust.scale = Main.rand.NextFloat(0.75f, 1.2f);
					dust.alpha = Main.rand.Next(100, 171);
					dust.velocity = dust.velocity.RotatedByRandom(0.30000001192092896);
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
			Vector2 position2 = base.Projectile.Center + Utils.RotatedByRandom(new Vector2(25f, 25f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f) * base.Projectile.scale;
			int type2 = (Main.rand.NextBool(8) ? 262 : 287);
			Vector2? velocity4 = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1.3f);
			newColor = default(Color);
			Dust dust2 = Dust.NewDustPerfect(position2, type2, velocity4, 0, newColor);
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(0.4f, 0.7f);
			dust2.alpha = 100;
		}
		base.Projectile.rotation += Main.rand.NextFloat(0.01f, 0.18f) * (float)base.Projectile.direction * (float)rotDirection;
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		float minMult = 0.25f;
		int hitsToMinMult = 6;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		if (base.Projectile.numHits == 0)
		{
			damageMult += 0.5f;
			for (int i = 0; i < 3; i++)
			{
				SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact with
				{
					Volume = 0.9f,
					Pitch = -0.4f + (float)i * 0.25f,
					MaxInstances = 6
				}, base.Projectile.Center);
			}
			for (int j = 0; j < 9; j++)
			{
				float range = Main.rand.NextFloat(-0.3f, 0.3f);
				float power = 1f - Math.Abs(range);
				Vector2 vel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(range) * Main.rand.NextFloat(35f, 40f) * power;
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, vel, Color.Peru, Color.PeachPuff, Main.rand.NextFloat(1.7f, 2.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
				for (int b = 0; b < 6; b++)
				{
					Color clr = Color.Lerp(Main.rand.NextBool() ? Color.Peru : Color.PeachPuff, Color.Black, Main.rand.NextFloat(0.25f, 0.45f));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, vel.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.4f, 0.9f) * power, "CalamityMod/Particles/SmallSmoke", affectedByGravity: false, Main.rand.Next(25, 36), base.Projectile.scale * Main.rand.NextFloat(0.08f, 0.14f) * 7f, clr, new Vector2(1f, Main.rand.NextFloat(0.2f, 2f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-4f, 4f), fadeIn: false, affectedByLight: true, 0f, 1f, 1f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.8f, 0.8f)));
				}
			}
		}
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 1f)
		{
			Main.player[base.Projectile.owner].SetScreenshake(5f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PrimordialEarthExplosion>(), (int)((float)base.Projectile.damage * 2.5f), 0f, base.Projectile.owner);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagicRockImpact");
			style.Volume = 0.75f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Gold * 0.55f, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 2.56f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Gold, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.1f, 0.6f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.05f, 0.45000002f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		for (int i = 0; i < 65; i++)
		{
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(6) ? 262 : 287, Utils.RotatedByRandom(new Vector2(7f, 7f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f));
				dust.noGravity = false;
				dust.scale = Main.rand.NextFloat(0.8f, 1.3f);
				if (dust.type == 262)
				{
					dust.noGravity = true;
					dust.fadeIn = 0.5f;
					dust.velocity *= 2f;
				}
				dust.alpha = 100;
			}
			else
			{
				Color clr = Color.Lerp(Main.rand.NextBool() ? Color.Peru : Color.PeachPuff, Color.Black, Main.rand.NextFloat(0.25f, 0.45f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, (Vector2.One * Main.rand.NextFloat(6f, 15f)).RotatedByRandom(6.2831854820251465), "CalamityMod/Particles/SmallSmoke", affectedByGravity: true, Main.rand.Next(20, 46), base.Projectile.scale * Main.rand.NextFloat(0.08f, 0.14f) * 8f, clr, new Vector2(1f, Main.rand.NextFloat(0.2f, 2f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-4f, 4f), fadeIn: false, affectedByLight: true, 0f, 1f, 1f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.8f, 0.8f)));
			}
		}
		for (int j = 0; j < 9; j++)
		{
			Vector2 randVel = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.8f, 1.6f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, Color.Peru, Main.rand.Next(25, 36), Main.rand.NextFloat(0.9f, 2.3f), 0.4f));
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, randVel * 0.8f, Color.Peru, Color.PeachPuff, Main.rand.NextFloat(0.4f, 1.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width * 0.5f * base.Projectile.scale, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 50)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.4f);
		}
		return true;
	}
}
