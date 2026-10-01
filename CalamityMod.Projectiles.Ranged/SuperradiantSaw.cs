using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Cooldowns;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SuperradiantSaw : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle TileCollideGFB = new SoundStyle("CalamityMod/Sounds/Custom/MetalPipeFalling");

	public int HitstopTimer;

	public bool Returning;

	public int ReturnTimer;

	public const int ReturnDelay = 90;

	public const int MaxBoltPairs = 7;

	public bool Empowered;

	public Particle SmallSlashSmear;

	public Particle LargeSlashSmear;

	public static Asset<Texture2D> SawOutline;

	public static Asset<Texture2D> SmallSlash;

	public static Asset<Texture2D> LargeSlash;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float SawLevel => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public ref float PierceBeforeReturn => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 46);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.rotation += MathHelper.ToRadians(6f + 18f * SawLevel);
		if (HitstopTimer > 0)
		{
			HitstopTimer--;
			if (HitstopTimer == 0)
			{
				base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 24f;
			}
		}
		Player Owner = Main.player[base.Projectile.owner];
		Empowered = Owner.HasCooldown(SuperradiantSawBoost.ID);
		if (Empowered && !Returning && Time > 30f)
		{
			float homingTurnSpeed = 0.2f;
			Vector2 mouse = Owner.ClampedMouseWorld();
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.SafeDirectionTo(mouse).ToRotation(), homingTurnSpeed).ToRotationVector2() * 24f;
		}
		if (ReturnTimer > 0 && ReturnTimer < 90)
		{
			ReturnTimer++;
			if (ReturnTimer == 90)
			{
				Returning = true;
			}
		}
		if (Returning)
		{
			base.Projectile.tileCollide = false;
			if (ReturnTimer < 90)
			{
				ReturnTimer = 90;
			}
			ReturnTimer++;
			if (ReturnTimer < 120)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.95f;
			}
			else
			{
				if (ReturnTimer == 120)
				{
					int boltCount = Math.Min(base.Projectile.numHits, 7) * 2;
					for (int b = 0; b < boltCount; b++)
					{
						Vector2 randBoltVelocity = Main.rand.NextVector2Unit() * 9f;
						if (Main.myPlayer == base.Projectile.owner)
						{
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, randBoltVelocity, ModContent.ProjectileType<SuperradiantBolt>(), (int)((float)base.Projectile.damage * 0.5f), 0f, Main.myPlayer);
						}
					}
					float sparkCount = 6f + 5f * SawLevel;
					for (float i = 0f; i < sparkCount; i++)
					{
						Vector2 velocity = Main.rand.NextVector2Unit() * (12f + 10f * SawLevel);
						float sparkScale = 1f + 0.25f * SawLevel;
						GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, velocity, Color.White, Color.Lime, sparkScale, 30, 0.1f, sparkScale, Main.rand.NextFloat(0f, 0.01f)));
					}
				}
				if (ReturnTimer % 9 == 0 && Empowered)
				{
					Vector2 randVelocity = -base.Projectile.velocity.RotatedByRandom(1.0471975803375244) * 0.5f;
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, randVelocity, ModContent.ProjectileType<SuperradiantBolt>(), (int)((float)base.Projectile.damage * 0.5f), 0f, Main.myPlayer);
					}
				}
				float returnSpeed = 14.400001f + 0.05f * (float)(ReturnTimer - 120);
				Vector2 ownerDist = Owner.Center - base.Projectile.Center;
				if (((Vector2)(ref ownerDist)).Length() > 3000f)
				{
					base.Projectile.Kill();
				}
				((Vector2)(ref ownerDist)).Normalize();
				ownerDist *= returnSpeed;
				if (base.Projectile.velocity.X < ownerDist.X)
				{
					base.Projectile.velocity.X = ownerDist.X;
				}
				else if (base.Projectile.velocity.X > ownerDist.X)
				{
					base.Projectile.velocity.X = ownerDist.X;
				}
				if (base.Projectile.velocity.Y < ownerDist.Y)
				{
					base.Projectile.velocity.Y = ownerDist.Y;
				}
				else if (base.Projectile.velocity.Y > ownerDist.Y)
				{
					base.Projectile.velocity.Y = ownerDist.Y;
				}
				if (Main.myPlayer == base.Projectile.owner)
				{
					Rectangle hitbox = base.Projectile.Hitbox;
					if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox))
					{
						base.Projectile.Kill();
					}
				}
			}
		}
		else if (Main.rand.NextBool())
		{
			Color dustColor = (Color)(Empowered ? Main.DiscoColor : new Color(Main.DiscoR, 255, 60));
			Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f, 150, dustColor, 1.2f).noGravity = true;
		}
		if (!Empowered)
		{
			return;
		}
		if (SawLevel >= 2f)
		{
			if (LargeSlashSmear == null)
			{
				LargeSlashSmear = new CircularSmearVFX(base.Projectile.Center, Color.Black, Time * (0f - base.Projectile.rotation), 1.35f);
				GeneralParticleHandler.SpawnParticle(LargeSlashSmear);
			}
			else
			{
				LargeSlashSmear.Rotation = 0f - base.Projectile.rotation;
				LargeSlashSmear.Time = 0;
				LargeSlashSmear.Position = base.Projectile.Center;
				LargeSlashSmear.Scale = 1.35f;
				LargeSlashSmear.Color = Main.hslToRgb(0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.6f) * 0.8f;
			}
		}
		if (SawLevel >= 1f)
		{
			if (SmallSlashSmear == null)
			{
				SmallSlashSmear = new CircularSmearVFX(base.Projectile.Center, Color.Black, base.Projectile.rotation, 0.8f);
				GeneralParticleHandler.SpawnParticle(SmallSlashSmear);
				return;
			}
			SmallSlashSmear.Rotation = base.Projectile.rotation;
			SmallSlashSmear.Time = 0;
			SmallSlashSmear.Position = base.Projectile.Center;
			SmallSlashSmear.Scale = 0.8f;
			SmallSlashSmear.Color = Main.hslToRgb(0.5f + 0.5f * MathF.Cos(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.6f) * 0.6f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		int sparkCount = 6 + 5 * (int)SawLevel;
		for (int s = 0; s < sparkCount; s++)
		{
			Vector2 sparkVelocity = default(Vector2);
			if (base.Projectile.velocity.X != oldVelocity.X && oldVelocity.X < 0f)
			{
				sparkVelocity = Vector2.UnitX * 6.5f;
			}
			else if (base.Projectile.velocity.X != oldVelocity.X && oldVelocity.X >= 0f)
			{
				sparkVelocity = Vector2.UnitX * -6.5f;
			}
			else if (base.Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y < 0f)
			{
				sparkVelocity = Vector2.UnitY * 6.5f;
			}
			else if (base.Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y >= 0f)
			{
				sparkVelocity = Vector2.UnitY * -6.5f;
			}
			Vector2 relativePosition = ((sparkVelocity.X > 0f) ? base.Projectile.Left : ((sparkVelocity.X < 0f) ? base.Projectile.Right : ((sparkVelocity.Y > 0f) ? base.Projectile.Top : base.Projectile.Bottom)));
			sparkVelocity = sparkVelocity.RotatedByRandom(1.5707963705062866) * (Main.rand.NextFloat(0.8f, 1.2f) + Main.rand.NextFloat(0.2f, 0.6f) * SawLevel);
			float scale = Main.rand.NextFloat(0.5f, 0.8f) + Main.rand.NextFloat(0.2f, 0.6f) * SawLevel;
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(relativePosition, sparkVelocity, affectedByGravity: false, 30, scale, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)));
		}
		SoundEngine.PlaySound(Main.zenithWorld ? TileCollideGFB : SoundID.Item178 with
		{
			Pitch = 0.1f * (float)base.Projectile.numHits
		}, base.Projectile.Center);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		if (PierceBeforeReturn > 0f)
		{
			PierceBeforeReturn--;
			base.Projectile.numHits++;
			if (PierceBeforeReturn <= 0f)
			{
				base.Projectile.localNPCHitCooldown = 20;
				Returning = true;
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Laceration>(), 180);
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 90);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice");
		style.Pitch = 0.1f * (float)base.Projectile.numHits;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int onHitSparkAmount = 7 + 10 * (int)SawLevel;
		for (int s = 0; s < onHitSparkAmount; s++)
		{
			Vector2 sparkVel = base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * (Main.rand.NextFloat(0.4f, 0.8f) + Main.rand.NextFloat(0.4f, 0.6f) * SawLevel);
			float sparkSize = 0.4f + Main.rand.NextFloat(0.3f, 0.6f) * SawLevel;
			Color sparkColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(target.Center, sparkVel, affectedByGravity: false, 30, sparkSize, sparkColor));
		}
		for (int sq = 0; sq < 7; sq++)
		{
			Vector2 squareVel = Main.rand.NextVector2CircularEdge(1f, 1f) * (Main.rand.NextFloat(10f, 16f) + 5f * SawLevel);
			float squareSize = 1.6f + Main.rand.NextFloat(1f, 1.6f) * SawLevel;
			Color squareColor = Main.hslToRgb(Main.rand.NextFloat(), 0.6f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new SquareParticle(target.Center, squareVel, affectedByGravity: true, 30, squareSize, squareColor));
		}
		if (!Returning && HitstopTimer == 0)
		{
			HitstopTimer = 5;
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) / 24f;
		}
		if (base.Projectile.numHits < 1)
		{
			ReturnTimer = 1;
		}
		if (PierceBeforeReturn > 0f)
		{
			PierceBeforeReturn--;
			if (PierceBeforeReturn <= 0f)
			{
				base.Projectile.localNPCHitCooldown = 20;
				Returning = true;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= (Returning ? 0.2f : (Empowered ? 0.75f : 1f));
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/CeramicImpact", 2), base.Projectile.Center);
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		if (SawLevel >= 2f)
		{
			((Rectangle)(ref hitbox)).Inflate(72, 72);
		}
		else if (SawLevel >= 1f)
		{
			((Rectangle)(ref hitbox)).Inflate(32, 32);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		if (LargeSlash == null)
		{
			LargeSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSawLargeSlash", (AssetRequestMode)2);
		}
		Texture2D largeSlashTexture = LargeSlash.Value;
		if (SmallSlash == null)
		{
			SmallSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSawSmallSlash", (AssetRequestMode)2);
		}
		Texture2D smallSlashTexture = SmallSlash.Value;
		Color slashColor = default(Color);
		((Color)(ref slashColor))._002Ector(200, 200, 200, 100);
		if (SawLevel >= 2f)
		{
			Main.EntitySpriteDraw(largeSlashTexture, base.Projectile.Center - Main.screenPosition, null, slashColor, 0f - base.Projectile.rotation, largeSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			if (Time % 4f == 0f)
			{
				Vector2 randomParticleOffset = default(Vector2);
				((Vector2)(ref randomParticleOffset))._002Ector(Main.rand.NextFloat((float)(-base.Projectile.width) * 1.75f, (float)base.Projectile.width * 1.75f), Main.rand.NextFloat((float)(-base.Projectile.width) * 1.75f, (float)base.Projectile.width * 1.75f));
				float randomParticleScale = Main.rand.NextFloat(0.65f, 0.95f);
				Color bloomColor = Color.Lerp(new Color(29, 120, 30), new Color(56, 255, 59), MathF.Abs(MathF.Sin(Time)));
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center + randomParticleOffset, base.Projectile.velocity, Main.rand.NextBool() ? Color.White : bloomColor, randomParticleScale, randomParticleScale, 4, fade: false));
			}
		}
		if (SawLevel >= 1f)
		{
			Main.EntitySpriteDraw(smallSlashTexture, base.Projectile.Center - Main.screenPosition, null, slashColor, base.Projectile.rotation, smallSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			if (Time % 4f == 0f)
			{
				Vector2 randomParticleOffset2 = default(Vector2);
				((Vector2)(ref randomParticleOffset2))._002Ector(Main.rand.NextFloat(-base.Projectile.width, base.Projectile.width), Main.rand.NextFloat(-base.Projectile.width, base.Projectile.width));
				float randomParticleScale2 = Main.rand.NextFloat(0.35f, 0.65f);
				Color bloomColor2 = Color.Lerp(new Color(29, 120, 30), new Color(56, 255, 59), MathF.Abs(MathF.Cos(Time)));
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center + randomParticleOffset2, base.Projectile.velocity, Main.rand.NextBool() ? Color.White : bloomColor2, randomParticleScale2, randomParticleScale2, 4, fade: false));
			}
		}
		Texture2D buzzsawTexture = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(buzzsawTexture, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, buzzsawTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
		if (Empowered)
		{
			if (SawOutline == null)
			{
				SawOutline = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSawOutline", (AssetRequestMode)2);
			}
			Texture2D outline = SawOutline.Value;
			Main.EntitySpriteDraw(outline, base.Projectile.Center - Main.screenPosition, null, Main.DiscoColor, base.Projectile.rotation, outline.Size() * 0.5f, 1f, (SpriteEffects)0);
		}
		if (!CalamityClientConfig.Instance.Afterimages)
		{
			return false;
		}
		for (int i = 1; i < base.Projectile.oldPos.Length; i++)
		{
			float afterimageRot = base.Projectile.oldRot[i];
			Vector2 drawPos = base.Projectile.oldPos[i] + buzzsawTexture.Size() * 0.5f - Main.screenPosition;
			float intensity = MathHelper.Lerp(0.1f, 0.6f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			Main.EntitySpriteDraw(buzzsawTexture, drawPos, null, lightColor * intensity, afterimageRot, buzzsawTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			if (SawLevel >= 2f)
			{
				Main.EntitySpriteDraw(largeSlashTexture, drawPos, null, slashColor * intensity, 0f - afterimageRot, largeSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			}
			if (SawLevel >= 1f)
			{
				Main.EntitySpriteDraw(smallSlashTexture, drawPos, null, slashColor * intensity, afterimageRot, smallSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			}
		}
		return false;
	}
}
