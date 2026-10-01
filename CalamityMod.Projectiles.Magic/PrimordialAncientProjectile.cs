using System;
using System.Collections.Generic;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PrimordialAncientProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int rotDirection = 1;

	public float curve = 0.02f;

	public bool goToCursor;

	public Vector2 mousePos;

	public float CenterX;

	public float CenterY;

	public List<bool> buffList = new List<bool>(new bool[255]);

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 140;
		base.Projectile.height = 140;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 8;
		base.Projectile.timeLeft = 430;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_0956: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0960: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0976: Unknown result type (might be due to invalid IL or missing references)
		//IL_0980: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_089a: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0f)
		{
			rotDirection = (Main.rand.NextBool() ? 1 : (-1));
			base.Projectile.rotation = Main.rand.NextFloat(-20f, 20f);
			base.Projectile.scale = 0.5f;
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Purple;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.3f);
		if (!goToCursor)
		{
			curve = MathHelper.Lerp(curve, 0f, 0.035f);
			Projectile projectile = base.Projectile;
			projectile.velocity *= Main.rand.NextFloat(0.985f, 0.995f);
			if (base.Projectile.ai[1] != 1f)
			{
				base.Projectile.velocity = base.Projectile.velocity.RotatedBy(curve * base.Projectile.localAI[0]);
			}
		}
		if (base.Projectile.ai[2] == 1f)
		{
			for (int playerIndex = 0; playerIndex < 255; playerIndex++)
			{
				Player player = Main.player[playerIndex];
				if (Vector2.Distance(player.Center, base.Projectile.Center) < (float)base.Projectile.width * 0.5f * base.Projectile.scale && !buffList[playerIndex])
				{
					buffList[playerIndex] = true;
					player.AddBuff(ModContent.BuffType<AeolianEarthBuff>(), 1080);
					int Dusts = 8;
					float radians = (float)Math.PI * 2f / (float)Dusts;
					Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
					for (int i = 0; i < Dusts; i++)
					{
						Vector2 dustVelocity = spinningPoint.RotatedBy(radians * (float)i) * 12.5f;
						GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, dustVelocity * 0.7f, affectedByGravity: false, 12, 0.009f, Color.Purple, new Vector2(3.5f, 1.3f), quickShrink: true));
						Vector2 center2 = player.Center;
						Vector2? velocity = dustVelocity.RotatedBy(MathHelper.ToRadians(22.5f));
						newColor = default(Color);
						Dust.NewDustPerfect(center2, 86, velocity, 0, newColor, 0.9f).noGravity = true;
						Vector2 center3 = player.Center;
						Vector2? velocity2 = dustVelocity.RotatedBy(MathHelper.ToRadians(22.5f)) * 0.4f;
						newColor = default(Color);
						Dust.NewDustPerfect(center3, 86, velocity2, 0, newColor, 1.2f).noGravity = true;
					}
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/RavagerPillarSummon");
					style.Volume = 0.65f;
					style.Pitch = 0.8f;
					SoundEngine.PlaySound(in style, player.Center);
				}
			}
		}
		if (base.Projectile.timeLeft % 2 == 0)
		{
			base.Projectile.frameCounter++;
		}
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
			base.Projectile.scale *= 1.007f;
		}
		if (time > 5f)
		{
			int chance = 5;
			if (Main.rand.NextBool(chance))
			{
				for (int j = 0; j < 2; j++)
				{
					Vector2 position = base.Projectile.Center + ((float)j * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 20f * base.Projectile.scale;
					int type = (Main.rand.NextBool(5) ? 86 : 287);
					Vector2? velocity3 = ((float)j * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(((Vector2)(ref base.Projectile.velocity)).Length())).ToRotationVector2() * (float)((chance > 1) ? 7 : 3);
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, velocity3, 0, newColor);
					dust.noGravity = false;
					dust.scale = Main.rand.NextFloat(0.75f, 1.2f);
					dust.alpha = Main.rand.Next(100, 171);
					dust.velocity = dust.velocity.RotatedByRandom(0.30000001192092896);
					if (dust.type == 86)
					{
						dust.noGravity = true;
					}
					if (chance > 1)
					{
						dust.noGravity = true;
					}
				}
			}
			if (time < 350f)
			{
				for (int k = 0; k < 2; k++)
				{
					Vector2 position2 = base.Projectile.Center + ((float)k * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 60f * base.Projectile.scale * Utils.GetLerpValue(350f, 250f, time, clamped: true);
					Vector2? velocity4 = ((float)k * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(((Vector2)(ref base.Projectile.velocity)).Length())).ToRotationVector2();
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(position2, 272, velocity4, 0, newColor);
					dust2.noGravity = true;
					dust2.scale = Main.rand.NextFloat(0.4f, 0.6f);
				}
			}
			if (time == 350f)
			{
				base.Projectile.velocity = Vector2.Zero;
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Purple, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.52f, 9, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.46f, 9, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagicRockImpact");
				style.Volume = 0.75f;
				style.Pitch = 0.35f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				mousePos = Main.player[base.Projectile.owner].ClampedMouseWorld();
				goToCursor = true;
			}
			if (goToCursor)
			{
				mousePos = Main.player[base.Projectile.owner].ClampedMouseWorld();
				if (time == 355f)
				{
					CenterX = base.Projectile.Center.X;
					CenterY = base.Projectile.Center.Y;
				}
				if (time > 360f && base.Projectile.timeLeft % 4 == 0 && base.Projectile.timeLeft > 15)
				{
					Vector2 trailVel = (new Vector2(CenterX, CenterY) - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 0.5f;
					float size = (float)base.Projectile.width * 0.5f * base.Projectile.scale;
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(size, size) - trailVel * 4f, trailVel, affectedByGravity: false, 11, 0.005f, Color.Purple, new Vector2(3.5f, 1.3f), quickShrink: true));
				}
				if (time > 360f && base.Projectile.timeLeft > 15)
				{
					float size2 = (float)base.Projectile.width * 0.03f * base.Projectile.scale;
					Vector2 position3 = base.Projectile.Center + Main.rand.NextVector2Circular(size2, size2);
					Vector2 trailVel2 = (new Vector2(CenterX, CenterY) - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 0.5f;
					Vector2? velocity5 = trailVel2;
					newColor = default(Color);
					Dust dust3 = Dust.NewDustPerfect(position3, 272, velocity5, 0, newColor);
					dust3.noGravity = true;
					dust3.scale = Main.rand.NextFloat(0.4f, 0.6f);
				}
				if (time > 355f)
				{
					base.Projectile.Center = new Vector2(MathHelper.Lerp(CenterX, mousePos.X, Utils.GetLerpValue(355f, 430f, time, clamped: true)), MathHelper.Lerp(CenterY, mousePos.Y, Utils.GetLerpValue(355f, 430f, time, clamped: true)));
				}
			}
			if (Main.rand.NextBool(6))
			{
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(25f, 25f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f) * base.Projectile.scale, (-base.Projectile.velocity * 0.2f).RotatedByRandom(0.20000000298023224) + Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * (float)((time > 96f) ? 1 : 0), Color.Peru, Color.PeachPuff, Main.rand.NextFloat(0.4f, 1.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
			}
			Vector2 position4 = base.Projectile.Center + Utils.RotatedByRandom(new Vector2(25f, 25f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f) * base.Projectile.scale;
			int type2 = (Main.rand.NextBool(5) ? 86 : 287);
			Vector2? velocity6 = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1.3f);
			newColor = default(Color);
			Dust dust4 = Dust.NewDustPerfect(position4, type2, velocity6, 0, newColor);
			dust4.noGravity = true;
			dust4.scale = Main.rand.NextFloat(0.4f, 0.7f);
			dust4.alpha = 100;
		}
		base.Projectile.rotation += Main.rand.NextFloat(0.1f * Utils.GetLerpValue(-100f, 360f, time)) * (float)base.Projectile.direction * base.Projectile.localAI[0];
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
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
					MaxInstances = 9
				}, base.Projectile.Center);
			}
			for (int j = 0; j < 6; j++)
			{
				float range = Main.rand.NextFloat(-0.3f, 0.3f);
				float power = 1f - Math.Abs(range);
				Vector2 vel = base.Projectile.Center.DirectionTo(target.Center).RotatedBy(range) * Main.rand.NextFloat(35f, 40f) * power;
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, vel, Color.Peru, Color.PeachPuff, Main.rand.NextFloat(1.7f, 2.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
				for (int b = 0; b < 5; b++)
				{
					Color clr = Color.Lerp(Main.rand.NextBool() ? Color.Peru : Color.PeachPuff, Color.Black, Main.rand.NextFloat(0.25f, 0.45f));
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, vel.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.4f, 0.9f) * power, "CalamityMod/Particles/SmallSmoke", affectedByGravity: false, Main.rand.Next(25, 36), base.Projectile.scale * Main.rand.NextFloat(0.08f, 0.14f) * 7f, clr, new Vector2(1f, Main.rand.NextFloat(0.2f, 2f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-4f, 4f), fadeIn: false, affectedByLight: true, 0f, 1f, 1f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.8f, 0.8f)));
				}
			}
		}
		modifiers.SourceDamage *= damageMult * 0.03f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Purple;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 2f);
		if (base.Projectile.ai[1] != 1f)
		{
			return;
		}
		Main.player[base.Projectile.owner].SetScreenshake(8f);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Purple, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.4f, 1.5f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.2f, 1.25f, 17, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PrimordialAncientExplosion>(), base.Projectile.damage, base.Projectile.knockBack * 1.5f, base.Projectile.owner);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MineralMortarExplode");
		style.Volume = 0.9f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = new SoundStyle("CalamityMod/Sounds/Item/EarthMeteor");
		style.Volume = 0.9f;
		style.Pitch = -0.3f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Purple, Color.White, 0.5f) * 0.55f, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 5.8879995f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Peru * 0.55f, "CalamityMod/Particles/SmokeExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.4f, 23, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.PeachPuff * 0.55f, "CalamityMod/Particles/SmokeExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.54f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 140; i++)
		{
			if (Main.rand.NextBool(5))
			{
				Vector2 center2 = base.Projectile.Center;
				int type = (Main.rand.NextBool(5) ? 86 : 287);
				Vector2? velocity = Utils.RotatedByRandom(new Vector2(21f, 21f), 100.0) * Main.rand.NextFloat(0.1f, 1.3f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor);
				dust.noGravity = false;
				dust.scale = Main.rand.NextFloat(0.8f, 1.3f);
				if (dust.type == 86)
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
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, (Vector2.One * Main.rand.NextFloat(9f, 33f)).RotatedByRandom(6.2831854820251465), "CalamityMod/Particles/SmallSmoke", affectedByGravity: true, Main.rand.Next(25, 56), base.Projectile.scale * Main.rand.NextFloat(0.08f, 0.14f) * 23f, clr, new Vector2(1f, Main.rand.NextFloat(0.2f, 2f)), useAddativeBlend: false, glowCenter: false, Main.rand.NextFloat(-4f, 4f), fadeIn: false, affectedByLight: true, 0f, 1f, 1f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.8f, 0.8f)));
			}
		}
		for (int j = 0; j < 30; j++)
		{
			Vector2 randVel = Utils.RotatedByRandom(new Vector2(30f, 30f), 100.0) * Main.rand.NextFloat(0.2f, 1.6f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, Color.Peru, Main.rand.Next(25, 36), Main.rand.NextFloat(0.9f, 2.3f), 0.4f));
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, randVel * 0.8f, Color.Peru, Color.PeachPuff, Main.rand.NextFloat(0.4f, 1.3f), 120f, Main.rand.NextFloat(0.03f, -0.03f)));
		}
		float numberOflines = 35f;
		float rotFactorlines = 360f / numberOflines;
		for (int e = 0; (float)e < numberOflines; e++)
		{
			float rot = MathHelper.ToRadians((float)e * rotFactorlines);
			Vector2 offset = (Vector2.UnitX * Main.rand.NextFloat(0.2f, 3.1f)).RotatedBy(rot + Main.rand.NextFloat(0.1f, 5.1f));
			Vector2 velOffset = (Vector2.UnitX * Main.rand.NextFloat(0.2f, 3.1f)).RotatedBy(rot + Main.rand.NextFloat(0.1f, 5.1f));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + offset, velOffset * Main.rand.NextFloat(15.5f, 25.5f), affectedByGravity: true, 80, Main.rand.NextFloat(0.5f, 1.3f), Color.Lerp(Color.White, Color.Purple, Main.rand.NextFloat(0.3f, 0.7f))));
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
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.7f);
		Texture2D rTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float randSize = Main.rand.NextFloat(0.8f, 1.2f);
		Color drawColor2 = Color.Purple;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color color = drawColor2;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(rTexture, position, null, color, base.Projectile.rotation, rTexture.Size() * 0.5f, 0.45f * Utils.GetLerpValue(0f, 25f, time, clamped: true) * randSize, (SpriteEffects)0);
		Vector2 position2 = base.Projectile.Center - Main.screenPosition;
		color = Color.White;
		((Color)(ref color)).A = 0;
		Main.EntitySpriteDraw(rTexture, position2, null, color, base.Projectile.rotation, rTexture.Size() * 0.5f, 0.3f * Utils.GetLerpValue(0f, 25f, time, clamped: true) * randSize, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 180);
	}
}
