using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StellarContemptHammer : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/PwnagehammerSound")
	{
		Volume = 0.35f
	};

	public static readonly SoundStyle RedHamSound = new SoundStyle("CalamityMod/Sounds/Item/StellarContemptClone")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle UseSoundFunny = new SoundStyle("CalamityMod/Sounds/Item/CalamityBell")
	{
		Volume = 1.5f
	};

	public int returnhammer;

	public int DustOnce = 1;

	public float rotatehammer = 15f;

	public int time;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/StellarContempt";

	public ref int EmpoweredHammer => ref Main.player[base.Projectile.owner].Calamity().StellarHammer;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 11;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 74;
		base.Projectile.height = 74;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 120;
	}

	public override void AI()
	{
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a66: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_076b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_0775: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_082e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.direction = (base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1)));
		base.Projectile.rotation += MathHelper.ToRadians(rotatehammer) * (float)base.Projectile.direction;
		if (EmpoweredHammer >= 5)
		{
			EmpoweredHammer = 0;
		}
		if (returnhammer == 0)
		{
			int falloffTime = 15;
			if (time > falloffTime)
			{
				base.Projectile.velocity.X *= 0.967f;
			}
			if (base.Projectile.velocity.Y < 15f && time > falloffTime)
			{
				base.Projectile.velocity.Y += 0.426f;
			}
			if (base.Projectile.velocity.Y < 5f)
			{
				base.Projectile.velocity.Y *= 0.98f;
			}
		}
		if (returnhammer == 1)
		{
			if (EmpoweredHammer == 4)
			{
				base.Projectile.velocity.X *= 0.281f;
				base.Projectile.velocity.Y -= 0.8f;
				rotatehammer++;
				if (base.Projectile.velocity.Y < -18f)
				{
					EmpoweredHammer = 0;
					returnhammer = 3;
				}
			}
			else
			{
				base.Projectile.velocity.Y *= 0.926f;
				base.Projectile.velocity.X *= 0.811f;
				if (base.Projectile.velocity.X > -1.05f && ((base.Projectile.velocity.X < 1.05f) & (base.Projectile.velocity.Y > -1.05f)) && base.Projectile.velocity.Y < 1.05f)
				{
					returnhammer = 2;
				}
			}
		}
		if (returnhammer == 2)
		{
			base.Projectile.extraUpdates = 2;
			float num = StellarContempt.Speed * 0.7f;
			float acceleration = 1.1f;
			Vector2 center = Main.player[base.Projectile.owner].Center;
			float xDist = center.X - base.Projectile.Center.X;
			float yDist = center.Y - base.Projectile.Center.Y;
			float dist = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
			dist = num / dist;
			xDist *= dist;
			yDist *= dist;
			if (base.Projectile.velocity.X < xDist)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + acceleration;
				if (base.Projectile.velocity.X < 0f && xDist > 0f)
				{
					base.Projectile.velocity.X += acceleration;
				}
			}
			else if (base.Projectile.velocity.X > xDist)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - acceleration;
				if (base.Projectile.velocity.X > 0f && xDist < 0f)
				{
					base.Projectile.velocity.X -= acceleration;
				}
			}
			if (base.Projectile.velocity.Y < yDist)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + acceleration;
				if (base.Projectile.velocity.Y < 0f && yDist > 0f)
				{
					base.Projectile.velocity.Y += acceleration;
				}
			}
			else if (base.Projectile.velocity.Y > yDist)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - acceleration;
				if (base.Projectile.velocity.Y > 0f && yDist < 0f)
				{
					base.Projectile.velocity.Y -= acceleration;
				}
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox))
				{
					EmpoweredHammer++;
					SoundStyle style = SoundID.DD2_BetsysWrathShot with
					{
						Volume = 0.4f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					for (int i = 0; i < 30; i++)
					{
						Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>());
						dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.800000011920929) * new Vector2(4f, 1.25f) * Main.rand.NextFloat(0.9f, 1f);
						dust.velocity = dust.velocity.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
						dust.velocity += base.Projectile.velocity * ((float)EmpoweredHammer * 0.04f);
						dust.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
						dust.noGravity = true;
						dust.scale = Main.rand.NextFloat(0.2f, 0.6f) * (float)EmpoweredHammer;
						Dust dust2 = DustExtensions.BetterCloneDust(dust);
						dust2.velocity = Main.rand.NextVector2Circular(3f, 3f);
						dust2.velocity += base.Projectile.velocity * ((float)EmpoweredHammer * 0.04f);
						dust2.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
					}
					base.Projectile.Kill();
				}
			}
		}
		if (returnhammer == 3)
		{
			if (base.Projectile.velocity.Y < 0f)
			{
				float fade = Utils.GetLerpValue(3f, -10f, base.Projectile.velocity.Y, clamped: true);
				float numberOfDusts = 2f;
				float rotFactor = 360f / numberOfDusts;
				for (int j = 0; (float)j < numberOfDusts; j++)
				{
					MathHelper.ToRadians((float)j * rotFactor);
					Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 250f, 0.04f);
					velOffset *= Main.rand.NextFloat(25f, 45f) * fade;
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + velOffset * 2.5f, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, affectedByGravity: false, 14, Main.rand.NextFloat(1.1f, 1.25f) - 0.5f * fade, Color.Turquoise));
					Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + velOffset * 2.5f, 278, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, 0, default(Color), Main.rand.NextFloat(0.4f, 0.6f));
					dust3.noGravity = true;
					dust3.color = Color.Turquoise;
					dust3.velocity += base.Projectile.velocity;
				}
				base.Projectile.velocity.Y += 0.6f;
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0f;
				SoundEngine.PlaySound(in RedHamSound, base.Projectile.Center);
				int hammer = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<StellarContemptEcho>(), base.Projectile.damage * 6, base.Projectile.knockBack * 1.5f, base.Projectile.owner, 0f, base.Projectile.ai[1]);
				Main.projectile[hammer].localAI[0] = Math.Sign(base.Projectile.velocity.X);
				Main.projectile[hammer].netUpdate = true;
				base.Projectile.Kill();
			}
		}
		if (Main.rand.NextBool(3))
		{
			Vector2 offset = Utils.RotatedByRandom(new Vector2(12f, 0f), MathHelper.ToRadians(360f));
			Vector2 velOffset2 = Utils.RotatedBy(new Vector2(4f, 0f), (double)offset.ToRotation(), default(Vector2));
			Dust dust4 = Dust.NewDustPerfect(new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y) + offset, ModContent.DustType<LightDust>(), (Vector2?)new Vector2(base.Projectile.velocity.X * 0.3f + velOffset2.X, base.Projectile.velocity.Y * 0.3f + velOffset2.Y), 0, default(Color), 1f);
			dust4.noGravity = true;
			dust4.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		if (returnhammer == 0)
		{
			base.Projectile.ai[1] = target.whoAmI;
			if (Main.zenithWorld)
			{
				SoundStyle style = UseSoundFunny with
				{
					Pitch = (float)EmpoweredHammer * 0.1f - 0.1f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			else
			{
				SoundStyle style = UseSound with
				{
					Pitch = (float)EmpoweredHammer * 0.1f - 0.1f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			if (EmpoweredHammer == 4)
			{
				base.Projectile.velocity.Y *= 0f;
				base.Projectile.velocity.X *= 0f;
			}
			else
			{
				SpawnFlares(target.Center, target.width, target.height);
			}
			returnhammer = 1;
		}
		float numberOfDusts = MathHelper.Clamp(40 - base.Projectile.numHits * 5, 6, 40);
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(4.8f, 0f), (double)(rot * Main.rand.NextFloat(1.1f, 4.1f)), default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(4f, 0f), (double)(rot * Main.rand.NextFloat(1.1f, 4.1f)), default(Vector2));
			if (i % 3 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + offset, velOffset * Main.rand.NextFloat(1f, 1.5f), "CalamityMod/Particles/Sparkle", affectedByGravity: false, 25, Main.rand.NextFloat(0.55f, 0.75f), Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise, new Vector2(1f, 2f), useAddativeBlend: true, glowCenter: true));
				continue;
			}
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, ModContent.DustType<LightDust>(), (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
			dust.noGravity = true;
			dust.velocity = velOffset * Main.rand.NextFloat(0.75f, 1f);
			dust.scale = Main.rand.NextFloat(0.9f, 1.6f);
			dust.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 90);
		float minMult = 0.7f;
		int hitsToMinMult = 10;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	private void SpawnFlares(Vector2 targetPos, int width, int height)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		SoundStyle style = SoundID.Item88 with
		{
			Volume = 0.4f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		base.Projectile.netUpdate = true;
		int numFlares = EmpoweredHammer + 1;
		int flareDamage = (int)(0.1f * (float)base.Projectile.damage);
		float flareKB = 4f;
		for (int i = 0; i < numFlares; i++)
		{
			float flareSpeed = Main.rand.NextFloat(9f, 13f);
			float xDist = Main.rand.NextFloat(80f, 320f) * (Main.rand.NextBool() ? (-1f) : 1f);
			float yDist = Main.rand.NextFloat(1200f, 1440f);
			Vector2 startPoint = targetPos + new Vector2(xDist, 0f - yDist);
			float xVariance = (float)width / 4f;
			if (xVariance < 8f)
			{
				xVariance = 8f;
			}
			float yVariance = (float)height / 4f;
			if (yVariance < 8f)
			{
				yVariance = 8f;
			}
			float xOffset = Main.rand.NextFloat(0f - xVariance, xVariance);
			float yOffset = Main.rand.NextFloat(0f - yVariance, yVariance);
			Vector2 velocity = targetPos + new Vector2(xOffset, yOffset) - startPoint;
			((Vector2)(ref velocity)).Normalize();
			velocity *= flareSpeed;
			float AI1 = Main.rand.Next(3);
			if (base.Projectile.owner == Main.myPlayer)
			{
				int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), startPoint, velocity, 645, flareDamage, flareKB, Main.myPlayer, 0f, AI1);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].DamageType = DamageClass.MeleeNoSpeed;
					Main.projectile[proj].tileCollide = false;
				}
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 3);
		return false;
	}
}
