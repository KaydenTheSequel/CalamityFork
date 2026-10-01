using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class FallenPaladinsHammerProj : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/PwnagehammerSound")
	{
		Volume = 0.35f
	};

	public static readonly SoundStyle UseSoundFunny = new SoundStyle("CalamityMod/Sounds/Item/CalamityBell")
	{
		Volume = 1.5f
	};

	public static readonly SoundStyle RedHamSound = new SoundStyle("CalamityMod/Sounds/Item/FallenPaladinsHammerClone")
	{
		Volume = 0.6f
	};

	public int returnhammer;

	public int time;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/FallenPaladinsHammer";

	public ref int EmpoweredHammer => ref Main.player[base.Projectile.owner].Calamity().PHAThammer;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 62;
		base.Projectile.height = 62;
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
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.direction = (base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1)));
		base.Projectile.rotation += MathHelper.ToRadians(22.5f) * (float)base.Projectile.direction;
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
			base.Projectile.velocity.Y *= 0.926f;
			base.Projectile.velocity.X *= 0.811f;
			if (base.Projectile.velocity.X > -1.05f && ((base.Projectile.velocity.X < 1.05f) & (base.Projectile.velocity.Y > -1.05f)) && base.Projectile.velocity.Y < 1.05f)
			{
				returnhammer = 2;
			}
		}
		if (returnhammer == 2)
		{
			float num = FallenPaladinsHammer.Speed * 0.7f;
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
					if (EmpoweredHammer == 3)
					{
						SoundEngine.PlaySound(in RedHamSound, base.Projectile.Center);
						for (int i = 0; i < 20; i++)
						{
							Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 90);
							dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.800000011920929) * new Vector2(4f, 1.25f) * Main.rand.NextFloat(0.9f, 1f);
							dust.velocity = dust.velocity.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
							dust.velocity += base.Projectile.velocity * ((float)EmpoweredHammer * 0.1f);
							dust.noGravity = true;
							dust.scale = Main.rand.NextFloat(0.7f, 1.2f) + (float)EmpoweredHammer * 0.4f;
							Dust dust2 = DustExtensions.BetterCloneDust(dust);
							dust2.velocity = Main.rand.NextVector2Circular(3f, 3f);
							dust2.velocity += base.Projectile.velocity * ((float)EmpoweredHammer * 0.1f);
						}
						int hammer = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<FallenPaladinsHammerEcho>(), base.Projectile.damage * 2, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.ai[1]);
						Main.projectile[hammer].localAI[0] = Math.Sign(base.Projectile.velocity.X);
						Main.projectile[hammer].netUpdate = true;
						EmpoweredHammer = 0;
					}
					else
					{
						SoundStyle style = SoundID.DD2_BetsysWrathShot with
						{
							Volume = 0.4f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						for (int j = 0; j < 20; j++)
						{
							Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 90);
							dust3.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.800000011920929) * new Vector2(4f, 1.25f) * Main.rand.NextFloat(0.9f, 1f);
							dust3.velocity = dust3.velocity.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
							dust3.velocity += base.Projectile.velocity * ((float)EmpoweredHammer * 0.1f);
							dust3.noGravity = true;
							dust3.scale = Main.rand.NextFloat(0.7f, 1.2f) + (float)EmpoweredHammer * 0.4f;
							Dust dust4 = DustExtensions.BetterCloneDust(dust3);
							dust4.velocity = Main.rand.NextVector2Circular(3f, 3f);
							dust4.velocity += base.Projectile.velocity * ((float)EmpoweredHammer * 0.1f);
						}
					}
					base.Projectile.Kill();
				}
			}
		}
		if (Main.rand.NextBool())
		{
			Vector2 offset = Utils.RotatedByRandom(new Vector2(12f, 0f), MathHelper.ToRadians(360f));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(4f, 0f), (double)offset.ToRotation(), default(Vector2));
			Dust dust5 = Dust.NewDustPerfect(new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y) + offset, 267, (Vector2?)new Vector2(base.Projectile.velocity.X * 0.2f + velOffset.X, base.Projectile.velocity.Y * 0.2f + velOffset.Y), 0, default(Color), 0.7f);
			dust5.noGravity = true;
			dust5.color = Color.DarkRed;
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		if (returnhammer == 0)
		{
			base.Projectile.ai[1] = target.whoAmI;
			if (Main.zenithWorld)
			{
				SoundStyle style = UseSoundFunny with
				{
					Pitch = (float)EmpoweredHammer * 0.2f - 0.4f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			else
			{
				SoundStyle style = UseSound with
				{
					Pitch = (float)EmpoweredHammer * 0.2f - 0.4f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			returnhammer = 1;
		}
		float numberOfDusts = 30f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			Vector2 offset = Utils.RotatedBy(new Vector2(3.6f, 0f), (double)(rot * Main.rand.NextFloat(1.1f, 4.1f)), default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(3f, 0f), (double)(rot * Main.rand.NextFloat(1.1f, 4.1f)), default(Vector2));
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, (base.Projectile.numHits != 0) ? 90 : (Main.rand.NextBool() ? 90 : ModContent.DustType<LightDust>()), (Vector2?)new Vector2(velOffset.X, velOffset.Y), 0, default(Color), 1f);
			dust.noGravity = true;
			dust.velocity = velOffset * ((base.Projectile.numHits != 0) ? 1f : ((i % 3 == 0) ? 3f : 1.5f));
			dust.scale = Main.rand.NextFloat(1.3f, 2.2f);
			if (base.Projectile.numHits == 0)
			{
				dust.color = Color.Red;
			}
		}
		if (base.Projectile.numHits == 0)
		{
			SoundStyle style = SoundID.DD2_ExplosiveTrapExplode with
			{
				Volume = 0.5f,
				Pitch = 0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0f, ModContent.ProjectileType<FallenExplosionSmall>(), base.Projectile.damage / 3, base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 90);
		float minMult = 0.7f;
		int hitsToMinMult = 10;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
