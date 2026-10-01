using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GalaxySmasherHammer : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/PwnagehammerSound")
	{
		Volume = 0.35f
	};

	public static readonly SoundStyle RedHamSound = new SoundStyle("CalamityMod/Sounds/Item/GalaxySmasherClone")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle UseSoundFunny = new SoundStyle("CalamityMod/Sounds/Item/CalamityBell")
	{
		Volume = 1.5f
	};

	public int returnhammer;

	public float rotatehammer;

	public int PulseCooldown;

	public float EchoHammerPrep;

	public float WaitTimer;

	public int InPulse;

	public int time;

	public Color usedColor;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/GalaxySmasher";

	public ref int EmpoweredHammer => ref Main.player[base.Projectile.owner].Calamity().GalaxyHammer;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 86;
		base.Projectile.height = 72;
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
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_0956: Unknown result type (might be due to invalid IL or missing references)
		//IL_0978: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea1: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(player.Center, base.Projectile.Center);
		base.Projectile.rotation += MathHelper.ToRadians(rotatehammer * 2f) * (float)base.Projectile.direction;
		List<Color> eColors = new List<Color>
		{
			Color.Aqua,
			Color.Magenta
		};
		float rate = Main.GlobalTimeWrappedHourly * 12f;
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		usedColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (EmpoweredHammer >= 8)
		{
			EmpoweredHammer = 0;
		}
		if (returnhammer == 0)
		{
			int falloffTime = 18;
			if (time > falloffTime)
			{
				base.Projectile.velocity.X *= 0.96f;
			}
			if (base.Projectile.velocity.Y < 25f && time > falloffTime)
			{
				base.Projectile.velocity.Y += 0.54f;
			}
			if (base.Projectile.velocity.Y < 5f)
			{
				base.Projectile.velocity.Y *= 0.973f;
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
			if (WaitTimer == 0f)
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
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle hitbox = base.Projectile.Hitbox;
				if (((Rectangle)(ref hitbox)).Intersects(player.Hitbox) || WaitTimer > 0f)
				{
					if (EmpoweredHammer >= 7)
					{
						base.Projectile.extraUpdates = 1;
						if (WaitTimer == 0f)
						{
							base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 20f;
						}
						WaitTimer++;
						Projectile projectile = base.Projectile;
						projectile.velocity *= 0.97f;
						if (WaitTimer == 20f)
						{
							EmpoweredHammer = 0;
							returnhammer = 3;
						}
					}
					else
					{
						EmpoweredHammer++;
						SoundStyle style = SoundID.DD2_BetsysWrathShot with
						{
							Volume = 0.4f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						for (int i = 0; i < 30; i++)
						{
							Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 181);
							dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.800000011920929) * new Vector2(4f, 1.25f) * Main.rand.NextFloat(0.9f, 1f);
							dust.velocity = dust.velocity.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
							dust.velocity += base.Projectile.velocity * ((float)EmpoweredHammer * 0.04f);
							dust.noGravity = true;
							dust.scale = Main.rand.NextFloat(0.2f, 0.6f) * (float)EmpoweredHammer;
							Dust dust2 = DustExtensions.BetterCloneDust(dust);
							dust2.velocity = Main.rand.NextVector2Circular(3f, 3f);
							dust2.velocity += base.Projectile.velocity * ((float)EmpoweredHammer * 0.04f);
						}
						base.Projectile.Kill();
					}
				}
			}
		}
		if (returnhammer == 3)
		{
			if (InPulse == 0)
			{
				SoundEngine.PlaySound(in RedHamSound, base.Projectile.Center);
				InPulse = 1;
			}
			rotatehammer -= 0.09f;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.96f;
			if (EchoHammerPrep >= 5f && InPulse < 2 && time % 20 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, usedColor * 0.35f, "CalamityMod/Particles/HighResHollowCircleHardEdge", new Vector2(1f, 1f * Utils.GetLerpValue(40f, 20f, EchoHammerPrep)), 0f, 2f, 0f, (int)(40f * Utils.GetLerpValue(50f, 0f, EchoHammerPrep, clamped: true)), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			if (EchoHammerPrep >= 32f)
			{
				InPulse = 2;
			}
			if (EchoHammerPrep <= 30f)
			{
				float fade = Utils.GetLerpValue(40f, 0f, EchoHammerPrep, clamped: true);
				float numberOfDusts = 2f;
				float rotFactor = 360f / numberOfDusts;
				for (int j = 0; (float)j < numberOfDusts; j++)
				{
					MathHelper.ToRadians((float)j * rotFactor);
					Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 250f, 0.04f);
					velOffset *= Main.rand.NextFloat(45f, 65f) * fade;
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + velOffset * 2.5f, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, affectedByGravity: false, 14, Main.rand.NextFloat(1.1f, 1.25f) - 0.5f * fade, usedColor));
					Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + velOffset * 2.5f, 278, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, 0, default(Color), Main.rand.NextFloat(0.4f, 0.6f));
					dust3.noGravity = true;
					dust3.color = usedColor;
					GalaxyMetaball.SpawnParticle(base.Projectile.Center + velOffset * 1.5f, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, 50f * Main.rand.NextFloat(0.9f, 1.3f) * fade);
				}
			}
			if (EchoHammerPrep > 40f)
			{
				int hammer = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<GalaxySmasherEcho>(), base.Projectile.damage * 9, base.Projectile.knockBack * 2.5f, base.Projectile.owner, 0f, base.Projectile.ai[1]);
				Main.projectile[hammer].localAI[0] = Math.Sign(base.Projectile.velocity.X);
				Main.projectile[hammer].netUpdate = true;
				base.Projectile.Kill();
			}
			else
			{
				EchoHammerPrep += 0.275f;
			}
		}
		if (targetDist < 1400f)
		{
			if (Main.rand.NextBool(3) && returnhammer < 3)
			{
				Vector2 offset = Utils.RotatedByRandom(new Vector2(12f, 0f), MathHelper.ToRadians(360f));
				Vector2 velOffset2 = Utils.RotatedBy(new Vector2(4f, 0f), (double)offset.ToRotation(), default(Vector2));
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center + offset, 278, -base.Projectile.velocity * 0.2f + velOffset2, 100, default(Color), 0.7f);
				dust4.noGravity = true;
				dust4.color = (Main.rand.NextBool() ? Color.Magenta : Color.Aqua);
			}
			Vector2 offset2 = Utils.RotatedBy(new Vector2(0f, -30f), -0.39269909262657166, default(Vector2)).RotatedBy(base.Projectile.rotation);
			Vector2 velOffset3 = Utils.RotatedBy(new Vector2(0f, -5f), -0.7853981852531433, default(Vector2)).RotatedBy(base.Projectile.rotation).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.8f, 1.5f);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + offset2 + Main.rand.NextVector2Circular(6f, 6f), velOffset3 * (float)((InPulse <= 0) ? 1 : 3), "CalamityMod/Particles/BloomRing", affectedByGravity: false, Main.rand.Next(7, 16), Main.rand.NextFloat(0.25f, 0.5f) * ((InPulse > 0) ? 1.5f : 1f), usedColor * 0.35f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-10f, 10f)));
			for (int k = 0; k < ((InPulse > 0) ? 3 : 2); k++)
			{
				Vector2 offset3 = Utils.RotatedBy(new Vector2(0f, -30f), -0.39269909262657166, default(Vector2)).RotatedBy(base.Projectile.rotation);
				Vector2 velOffset4 = Utils.RotatedBy(new Vector2(0f, -5f), -0.7853981852531433, default(Vector2)).RotatedBy(base.Projectile.rotation) * Main.rand.NextFloat(0.4f, 1f);
				GalaxyMetaball.SpawnParticle(base.Projectile.Center + offset3 + Main.rand.NextVector2Circular(6f, 6f), velOffset4 * ((InPulse > 0) ? 3f : 0.2f), 40f * Main.rand.NextFloat(0.9f, 1.3f) * (float)((InPulse <= 0) ? 1 : 2));
			}
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		if (returnhammer == 0)
		{
			base.Projectile.ai[1] = target.whoAmI;
			if (Main.zenithWorld)
			{
				SoundStyle style = UseSoundFunny with
				{
					Pitch = (float)EmpoweredHammer * 0.05f - 0.05f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			else
			{
				SoundStyle style = UseSound with
				{
					Pitch = (float)EmpoweredHammer * 0.05f - 0.05f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			int FunSizeHamID = ModContent.ProjectileType<GalaxySmasherMini>();
			int FunSizeHamDamage = (int)(0.1f * (float)base.Projectile.damage);
			float FunSizeHamKB = 0.2f * base.Projectile.knockBack;
			if (base.Projectile.owner == Main.myPlayer)
			{
				float numberOfProj = EmpoweredHammer + 1;
				float rotFactor2 = 360f / numberOfProj;
				float randRot = Main.rand.NextFloat(-6f, 6f);
				for (int i = 1; (float)i < numberOfProj + 1f; i++)
				{
					float rot = MathHelper.ToRadians((float)i * rotFactor2);
					Utils.RotatedBy(new Vector2(5f, 0f), (double)rot, default(Vector2)).RotatedBy(randRot);
					Vector2 velOffset = Utils.RotatedBy(new Vector2(1f, 0f), (double)rot, default(Vector2)).RotatedBy(randRot);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, velOffset, FunSizeHamID, FunSizeHamDamage, FunSizeHamKB, base.Projectile.owner, EmpoweredHammer, (EmpoweredHammer % 2 != 0) ? 1 : (-1), target.whoAmI);
				}
			}
			returnhammer = 1;
		}
		float numberOfDusts = MathHelper.Clamp(30 - base.Projectile.numHits * 3, 6, 30);
		float rotFactor3 = 360f / numberOfDusts;
		for (int j = 0; (float)j < numberOfDusts; j++)
		{
			float rot2 = MathHelper.ToRadians((float)j * rotFactor3);
			Vector2 offset = Utils.RotatedBy(new Vector2(4.8f, 0f), (double)(rot2 * Main.rand.NextFloat(1.1f, 4.1f)), default(Vector2));
			Vector2 velOffset2 = Utils.RotatedBy(new Vector2(7f, 0f), (double)(rot2 * Main.rand.NextFloat(1.1f, 4.1f)), default(Vector2));
			if (j % 3 == 0)
			{
				GalaxyMetaball.SpawnParticle(base.Projectile.Center + offset, velOffset2 * Main.rand.NextFloat(1f, 1.2f), 60f * Main.rand.NextFloat(0.9f, 1.3f));
				continue;
			}
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, ModContent.DustType<LightDust>());
			dust.noGravity = true;
			dust.velocity = velOffset2 * Main.rand.NextFloat(0.45f, 1f);
			dust.scale = Main.rand.NextFloat(0.8f, 1.7f);
			dust.color = (Main.rand.NextBool(3) ? Color.Aqua : Color.Magenta);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
		float minMult = 0.7f;
		int hitsToMinMult = 10;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GalaxySmasher", (AssetRequestMode)2).Value;
		Asset<Texture2D> p = ModContent.Request<Texture2D>("CalamityMod/Particles/FlameExplosion2", (AssetRequestMode)2);
		Asset<Texture2D> p2 = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearSmokey", (AssetRequestMode)2);
		Asset<Texture2D> p3 = ModContent.Request<Texture2D>("CalamityMod/Particles/SmallBloom", (AssetRequestMode)2);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.5f, 3, texture, drawCentered: true, shrink: true);
		float shrinkLerp = Utils.GetLerpValue(40f, 35f, EchoHammerPrep, clamped: true);
		float growLerp = Utils.GetLerpValue(0f, 35f, EchoHammerPrep, clamped: true);
		float bonusScale = ((returnhammer != 3) ? 1f : ((EchoHammerPrep < 35f) ? (growLerp * 3f) : (shrinkLerp * 3f)));
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		float fade = ((returnhammer == 3) ? 1f : Utils.GetLerpValue(2f, 15f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true));
		Main.EntitySpriteDraw(p3.Value, generalDrawPos, null, Color.Black * fade, base.Projectile.rotation, p3.Size() * 0.5f, 0.6f * bonusScale, (SpriteEffects)0);
		for (int i = 0; i < 3; i++)
		{
			Main.EntitySpriteDraw(p.Value, generalDrawPos, null, Color.Indigo * 0.95f * fade, base.Projectile.rotation * Main.rand.NextFloat(1.2f, 1.3f) + (float)i * 0.2f, p.Size() * 0.5f, (0.05f + (float)i * 0.004f) * bonusScale, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(texture, generalDrawPos, null, lightColor, base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.direction < 0));
		Color val;
		if (returnhammer < 3)
		{
			Texture2D value = p2.Value;
			val = usedColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, generalDrawPos, null, val * 0.45f * fade, base.Projectile.rotation * Main.rand.NextFloat(1.4f, 1.45f), p2.Size() * 0.5f, 0.9f * Main.rand.NextFloat(0.9f, 1.1f), (SpriteEffects)0);
		}
		else
		{
			Projectile projectile = base.Projectile;
			val = usedColor;
			((Color)(ref val)).A = 0;
			projectile.DrawProjectileWithBackglow(val, Color.White, 6.5f * growLerp, texture, null, (SpriteEffects)(base.Projectile.direction < 0));
		}
		return false;
	}

	public GalaxySmasherHammer()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		rotatehammer = 10f;
		usedColor = Color.Aqua;
		base._002Ector();
	}
}
