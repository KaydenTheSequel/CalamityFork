using System.Collections.Generic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AriesWrathConstellation : ModProjectile, ILocalizedModType, IModType
{
	public List<Particle> Particles;

	private const float ConstellationSwapTime = 15f;

	private Vector2 PreviousEnd;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Timer => ref base.Projectile.ai[0];

	private Vector2 AnchorStart
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Center;
		}
	}

	private Vector2 AnchorEnd
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Owner.ClampedMouseWorld();
		}
	}

	public Vector2 SizeVector
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			Vector2 val = (AnchorEnd - AnchorStart).SafeNormalize(Vector2.Zero);
			Vector2 val2 = AnchorEnd - AnchorStart;
			return val * MathHelper.Clamp(((Vector2)(ref val2)).Length(), 0f, (float)FourSeasonsGalaxia.AriesAttunement_Reach);
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 1;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 8);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.timeLeft = 20;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + SizeVector, 30f, ref collisionPoint);
	}

	public void BootlegSpawnParticle(Particle particle)
	{
		if (!Main.dedServ)
		{
			Particles.Add(particle);
			particle.Type = GeneralParticleHandler.particleIDsByTypes[particle.GetType()];
		}
	}

	public override void AI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		if (Particles == null)
		{
			Particles = new List<Particle>();
		}
		base.Projectile.Center = Owner.Center;
		if (Owner.channel)
		{
			base.Projectile.timeLeft = 20;
		}
		if (!Owner.active)
		{
			base.Projectile.Kill();
			return;
		}
		Vector2 center;
		if (Timer % 15f == 0f && base.Projectile.timeLeft >= 20)
		{
			Particles.Clear();
			PreviousEnd = Owner.ClampedMouseWorld() - Owner.Center;
			base.Projectile.ai[0] = 1f;
			Vector2 previousStar = base.Projectile.Center;
			Particle Star = new GenericSparkle(previousStar, Vector2.Zero, Color.White, Color.Plum, Main.rand.NextFloat(1f, 1.5f), 20, 0f, 3f);
			BootlegSpawnParticle(Star);
			Particle Line;
			for (float i = 0f + Main.rand.NextFloat(0.2f, 0.5f); i < 1f; i += Main.rand.NextFloat(0.2f, 0.5f))
			{
				float num = Main.rand.NextFloat(-50f, 50f);
				Vector2 sizeVector = SizeVector;
				center = default(Vector2);
				Vector2 offset = num * sizeVector.RotatedBy(1.5707963705062866, center).SafeNormalize(Vector2.Zero);
				Star = new GenericSparkle(base.Projectile.Center + SizeVector * i + offset, Vector2.Zero, Color.White, Color.Plum, Main.rand.NextFloat(1f, 1.5f), 20, 0f, 3f);
				BootlegSpawnParticle(Star);
				Line = new BloomLineVFX(previousStar, base.Projectile.Center + SizeVector * i + offset - previousStar, 0.8f, Color.MediumVioletRed * 0.75f, 20, capped: true, telegraph: true);
				BootlegSpawnParticle(Line);
				if (Main.rand.NextBool(3))
				{
					float num2 = Main.rand.NextFloat(-50f, 50f);
					Vector2 sizeVector2 = SizeVector;
					center = default(Vector2);
					offset = num2 * sizeVector2.RotatedBy(1.5707963705062866, center).SafeNormalize(Vector2.Zero);
					Star = new GenericSparkle(base.Projectile.Center + SizeVector * i + offset, Vector2.Zero, Color.White, Color.Plum, Main.rand.NextFloat(1f, 1.5f), 20, 0f, 3f);
					BootlegSpawnParticle(Star);
					Line = new BloomLineVFX(previousStar, base.Projectile.Center + SizeVector * i + offset - previousStar, 0.8f, Color.MediumVioletRed * 0.75f, 20, capped: true, telegraph: true);
					BootlegSpawnParticle(Line);
				}
				previousStar = base.Projectile.Center + SizeVector * i + offset;
			}
			Star = new GenericSparkle(base.Projectile.Center + SizeVector, Vector2.Zero, Color.White, Color.Plum, Main.rand.NextFloat(1f, 1.5f), 20, 0f, 3f);
			BootlegSpawnParticle(Star);
			Line = new BloomLineVFX(previousStar, base.Projectile.Center + SizeVector - previousStar, 0.8f, Color.MediumVioletRed * 0.75f, 20, capped: true);
			BootlegSpawnParticle(Line);
		}
		Vector2 moveDirection = Vector2.Zero;
		if (Timer > (float)base.Projectile.oldPos.Length)
		{
			moveDirection = base.Projectile.position - base.Projectile.oldPos[0];
		}
		foreach (Particle particle in Particles)
		{
			if (particle != null)
			{
				particle.Position += particle.Velocity + moveDirection;
				particle.Time++;
				particle.Update();
			}
		}
		Particles.RemoveAll((Particle particle2) => particle2.Time >= particle2.Lifetime && particle2.SetLifetime);
		Timer++;
		center = Owner.ClampedMouseWorld() - Owner.Center - PreviousEnd;
		if (((Vector2)(ref center)).Length() > 120f)
		{
			Timer = 15f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (Particles != null)
		{
			Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
			foreach (Particle particle in Particles)
			{
				particle.CustomDraw(Main.spriteBatch);
			}
			Main.spriteBatch.ExitShaderRegion();
		}
		return false;
	}

	public AriesWrathConstellation()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PreviousEnd = Vector2.Zero;
		base._002Ector();
	}
}
