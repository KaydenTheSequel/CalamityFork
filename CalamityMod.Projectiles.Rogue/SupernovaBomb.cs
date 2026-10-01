using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SupernovaBomb : ModProjectile, ILocalizedModType, IModType
{
	public Color variedColor;

	public Color mainColor;

	public Color randomColor;

	public int colorTimer;

	public int time;

	public bool homing;

	public bool returning;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Supernova";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 106;
		base.Projectile.height = 112;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0998: Unknown result type (might be due to invalid IL or missing references)
		//IL_099d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		Color newColor = (randomColor = (Color)(Main.rand.Next(4) switch
		{
			0 => Color.Red, 
			1 => Color.MediumTurquoise, 
			2 => Color.Orange, 
			_ => Color.LawnGreen, 
		}));
		if (time == 0)
		{
			mainColor = randomColor;
		}
		if (time % 20 == 0)
		{
			newColor = (variedColor = (Color)(colorTimer switch
			{
				0 => Color.Red, 
				1 => Color.MediumTurquoise, 
				2 => Color.Orange, 
				_ => Color.LawnGreen, 
			}));
			colorTimer++;
			if (colorTimer >= 4)
			{
				colorTimer = 0;
			}
		}
		Vector2 visualDirection = Utils.RotatedBy(new Vector2(17f, -17f), (double)base.Projectile.rotation, default(Vector2));
		Vector2 rotatedVisualDirection = Utils.RotatedByRandom(new Vector2(17f, -17f), 0.5).RotatedBy(base.Projectile.rotation);
		GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center + visualDirection, visualDirection * Main.rand.NextFloat(0.01f, 0.3f), affectedByGravity: false, 5, Main.rand.NextFloat(0.8f, 1f), Color.White * 0.7f * Utils.GetLerpValue(10f, 45f, time, clamped: true)));
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + rotatedVisualDirection * 0.7f, rotatedVisualDirection * Main.rand.NextFloat(0.01f, 0.15f), affectedByGravity: false, 13, Main.rand.NextFloat(0.55f, 0.9f), randomColor * Utils.GetLerpValue(10f, 45f, time, clamped: true)));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + rotatedVisualDirection * 0.7f, rotatedVisualDirection * Main.rand.NextFloat(0.01f, 0.5f), affectedByGravity: false, 17, Main.rand.NextFloat(0.2f, 0.6f), Color.Lerp(Color.White, randomColor, 0.5f) * Utils.GetLerpValue(10f, 45f, time, clamped: true)));
		}
		mainColor = Color.Lerp(mainColor, variedColor, 0.07f);
		base.Projectile.scale = 0.4f;
		Vector2 center = base.Projectile.Center;
		newColor = Color.Lerp(Color.White, randomColor, 0.5f);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		if (!homing && !returning)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(base.Projectile.velocity.ToRotation() - (float)Math.PI * 3f / 4f, 0.2f);
			if (homing)
			{
				CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 2000f, MathHelper.Clamp(5f + (float)time * 0.1f, 10f, 17f), 5f);
			}
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(9f, 9f) - base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 12f, visualDirection * Main.rand.NextFloat(0.2f, 0.8f), affectedByGravity: false, 20, 1.5f, Color.Lerp(Color.White, randomColor, 0.5f) * Utils.GetLerpValue(100f, 170f, time, clamped: true)));
			}
			for (int i = 0; i < 2; i++)
			{
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(12f, 12f) - base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 12f;
				Vector2? velocity = visualDirection * Main.rand.NextFloat(0.05f, 0.7f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position, 303, velocity, 0, newColor);
				dust.scale = Main.rand.NextFloat(0.75f, 2.25f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.White : Color.Lerp(Color.White, randomColor, 0.1f));
				dust.alpha = 170;
			}
		}
		if (time >= 60 && !homing && !returning)
		{
			if (base.Projectile.Center.ClosestNPCAt(2000f) == null)
			{
				returning = true;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ApolloArtemisTargetSelection");
				style.Pitch = -0.7f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				return;
			}
			if (time >= 60 && time < 75 && base.Projectile.Calamity().stealthStrike)
			{
				SoundStyle lockon = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ApolloArtemisTargetSelection");
				if (time == 60)
				{
					SoundStyle style = lockon with
					{
						Pitch = -0.6f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.7f, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.95f, 0.2f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, randomColor * 0.7f, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.8f, 0.2f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				if (time == 67)
				{
					SoundStyle style = lockon with
					{
						Pitch = -0.4f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.7f, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.75f, 0.2f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, randomColor * 0.7f, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.6f, 0.2f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				if (time == 74)
				{
					SoundStyle style = lockon with
					{
						Pitch = -0.1f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.7f, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.65f, 0.2f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, randomColor * 0.7f, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.5f, 0.2f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
			}
			if (time == 60 && !base.Projectile.Calamity().stealthStrike)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ApolloArtemisTargetSelection");
				style.Pitch = -0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.7f, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.75f, 0.2f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, randomColor * 0.7f, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), 0f, 0.6f, 0.2f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			if (time > (base.Projectile.Calamity().stealthStrike ? 75 : 62))
			{
				base.Projectile.penetrate = 1;
				homing = true;
				base.Projectile.extraUpdates = 3;
			}
		}
		if (returning)
		{
			base.Projectile.penetrate = -1;
			float num = MathHelper.Clamp(3f + (float)time * 0.15f, 7f, 28f);
			float acceleration = 1.1f;
			Player owner = Main.player[base.Projectile.owner];
			Vector2 center2 = owner.Center;
			float xDist = center2.X - base.Projectile.Center.X;
			float yDist = center2.Y - base.Projectile.Center.Y;
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
			Rectangle hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(owner.Hitbox))
			{
				base.Projectile.Kill();
			}
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		if (!returning)
		{
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = (base.Projectile.height = 128);
			base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
			if (base.Projectile.owner == Main.myPlayer)
			{
				if (base.Projectile.Calamity().stealthStrike)
				{
					float pitchRange = Main.rand.NextFloat(-0.1f, 0.1f);
					SoundStyle style = Supernova.StealthChargeSound with
					{
						Pitch = pitchRange
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SupernovaStealthBoom>(), base.Projectile.damage, 0f, base.Projectile.owner, 0f, 0f, pitchRange);
				}
				else
				{
					SoundEngine.PlaySound(in Supernova.ExplosionSound, base.Projectile.Center);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SupernovaBoom>(), base.Projectile.damage, 0f, base.Projectile.owner);
				}
			}
		}
		else
		{
			for (int i = 0; i < 15; i++)
			{
				randomColor = (Color)(Main.rand.Next(4) switch
				{
					0 => Color.Red, 
					1 => Color.MediumTurquoise, 
					2 => Color.Orange, 
					_ => Color.LawnGreen, 
				});
				Vector2 vel = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 14f * Main.rand.NextFloat(0.05f, 1.2f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f), 278, vel);
				dust.scale = Main.rand.NextFloat(0.45f, 1.15f);
				dust.noGravity = true;
				dust.color = Color.Lerp(Color.White, randomColor, 0.3f);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 60);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (!homing)
		{
			modifiers.SourceDamage *= 0.01f;
		}
		else
		{
			modifiers.SourceDamage *= 0.05f;
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 5f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		Color auraColor = base.Projectile.GetAlpha(Color.Lerp(Color.White, randomColor, 0.3f)) * 0.25f;
		for (int i = 0; i < 7; i++)
		{
			Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/Supernova", (AssetRequestMode)2).Value;
			Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)i / 7f + Main.GlobalTimeWrappedHourly * 8f).ToRotationVector2();
			rotationalDrawOffset *= MathHelper.Lerp(3f, 5.25f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f);
			Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + rotationalDrawOffset, null, auraColor, base.Projectile.rotation, centerTexture.Size() * 0.5f, base.Projectile.scale * 1.1f, (SpriteEffects)0);
		}
		if (!homing)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.Lerp(Color.White, randomColor, 0.3f));
		}
		return true;
	}

	public SupernovaBomb()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		variedColor = Color.White;
		mainColor = Color.LawnGreen;
		randomColor = Color.White;
		base._002Ector();
	}
}
