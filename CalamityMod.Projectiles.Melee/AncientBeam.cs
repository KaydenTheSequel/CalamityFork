using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AncientBeam : ModProjectile, ILocalizedModType, IModType
{
	private const float MaxTime = 40f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/FracturedArkGlow";

	public float Timer => 40f - (float)base.Projectile.timeLeft;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 40;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 34f * base.Projectile.scale;
		Vector2 start = -base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 8.5f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center + start, base.Projectile.Center + start + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * bladeLength, 24f, ref collisionPoint);
	}

	public override void AI()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		if ((float)base.Projectile.timeLeft < 35f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		base.Projectile.scale = 2.4f;
		base.Projectile.Opacity = 0.6f;
		Lighting.AddLight(base.Projectile.Center, 0.75f, 1f, 0.24f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1f - (float)Math.Pow(Timer / 40f, 3.0);
		if (Main.rand.NextBool(3))
		{
			int dustTrail = Dust.NewDust(base.Projectile.Center, 14, 14, 66, base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f, 150, new Color(Main.DiscoR, 100, 255), 1.2f);
			Main.dust[dustTrail].noGravity = true;
		}
		if (Main.rand.NextBool(3))
		{
			int dustType = Main.rand.Next(3);
			Dust.NewDust(base.Projectile.Center, 14, 14, dustType switch
			{
				1 => 57, 
				0 => 15, 
				_ => 58, 
			}, base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f, 150, default(Color), 1.3f);
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 1f)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 35)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_WitherBeastDeath, base.Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			Vector2 particleSpeed = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(1.2f, 2.3f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 40f, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.Cyan, 60, 1f, 1.5f, 3f, 0.02f));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = target.Hitbox.Size();
		Vector2 particleOrigin = ((((Vector2)(ref val)).Length() < 140f) ? target.Center : (base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 60f));
		for (int i = 0; i < 10; i++)
		{
			Vector2 particleSpeed = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(2.6f, 4f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(particleOrigin, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.Cyan, 60, 1f, 1.5f, 3f, 0.02f));
		}
	}
}
