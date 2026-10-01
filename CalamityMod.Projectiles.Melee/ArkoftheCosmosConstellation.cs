using System.Collections.Generic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ArkoftheCosmosConstellation : ModProjectile, ILocalizedModType, IModType
{
	private const float ConstellationSwapTime = 15f;

	public List<Particle> Particles;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public float Timer => base.Projectile.ai[0] - (float)base.Projectile.timeLeft;

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
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			Vector2 val = (AnchorEnd - AnchorStart).SafeNormalize(Vector2.Zero);
			Vector2 val2 = AnchorEnd - AnchorStart;
			return val * MathHelper.Clamp(((Vector2)(ref val2)).Length(), 0f, ArkoftheCosmos.MaxThrowReach);
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
		base.Projectile.localNPCHitCooldown = 15;
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
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		if (Particles == null)
		{
			Particles = new List<Particle>();
		}
		base.Projectile.Center = Owner.Center;
		if (!Owner.channel && base.Projectile.timeLeft > 20)
		{
			base.Projectile.timeLeft = 20;
		}
		if (!Owner.active)
		{
			base.Projectile.Kill();
			return;
		}
		if (Timer % 15f == 0f && base.Projectile.timeLeft >= 20)
		{
			Particles.Clear();
			float constellationColorHue = Main.rand.NextFloat();
			Color constellationColor = Main.hslToRgb(constellationColorHue, 1f, 0.8f);
			Vector2 previousStar = AnchorStart;
			Particle Star = new GenericSparkle(previousStar, Vector2.Zero, Color.White, Color.Plum, Main.rand.NextFloat(1f, 1.5f), 20, 0f, 3f);
			BootlegSpawnParticle(Star);
			Particle Line;
			for (float i = 0f + Main.rand.NextFloat(0.2f, 0.5f); i < 1f; i += Main.rand.NextFloat(0.2f, 0.5f))
			{
				constellationColorHue = (constellationColorHue + 0.16f) % 1f;
				constellationColor = Main.hslToRgb(constellationColorHue, 1f, 0.8f);
				Vector2 offset = Main.rand.NextFloat(-50f, 50f) * SizeVector.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero);
				Star = new GenericSparkle(AnchorStart + SizeVector * i + offset, Vector2.Zero, Color.White, constellationColor, Main.rand.NextFloat(1f, 1.5f), 20, 0f, 3f);
				BootlegSpawnParticle(Star);
				Line = new BloomLineVFX(previousStar, AnchorStart + SizeVector * i + offset - previousStar, 0.8f, constellationColor * 0.75f, 20, capped: true, telegraph: true);
				BootlegSpawnParticle(Line);
				if (Main.rand.NextBool(3))
				{
					constellationColorHue = (constellationColorHue + 0.16f) % 1f;
					constellationColor = Main.hslToRgb(constellationColorHue, 1f, 0.8f);
					offset = Main.rand.NextFloat(-50f, 50f) * SizeVector.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero);
					Star = new GenericSparkle(AnchorStart + SizeVector * i + offset, Vector2.Zero, Color.White, constellationColor, Main.rand.NextFloat(1f, 1.5f), 20, 0f, 3f);
					BootlegSpawnParticle(Star);
					Line = new BloomLineVFX(previousStar, AnchorStart + SizeVector * i + offset - previousStar, 0.8f, constellationColor, 20, capped: true, telegraph: true);
					BootlegSpawnParticle(Line);
				}
				previousStar = AnchorStart + SizeVector * i + offset;
			}
			constellationColorHue = (constellationColorHue + 0.16f) % 1f;
			constellationColor = Main.hslToRgb(constellationColorHue, 1f, 0.8f);
			Star = new GenericSparkle(AnchorStart + SizeVector, Vector2.Zero, Color.White, constellationColor, Main.rand.NextFloat(1f, 1.5f), 20, 0f, 3f);
			BootlegSpawnParticle(Star);
			Line = new BloomLineVFX(previousStar, AnchorStart + SizeVector - previousStar, 0.8f, constellationColor * 0.75f, 20, capped: true);
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
				particle.Color = Main.hslToRgb(Main.rgbToHsl(particle.Color).X + 0.02f, Main.rgbToHsl(particle.Color).Y, Main.rgbToHsl(particle.Color).Z);
			}
		}
		Particles.RemoveAll((Particle particle2) => particle2.Time >= particle2.Lifetime && particle2.SetLifetime);
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
}
