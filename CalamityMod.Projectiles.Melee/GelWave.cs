using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GelWave : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 84;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 200;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Blue;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (Main.rand.NextBool(10))
		{
			Vector2 position = base.Projectile.position + base.Projectile.velocity;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			float speedX = base.Projectile.velocity.X * 0.5f;
			float speedY = base.Projectile.velocity.Y * 0.5f;
			newColor = default(Color);
			Dust.NewDust(position, width, height, 56, speedX, speedY, 0, newColor);
			Vector2 position2 = base.Projectile.position + base.Projectile.velocity;
			int width2 = base.Projectile.width;
			int height2 = base.Projectile.height;
			float speedX2 = base.Projectile.velocity.X * 0.5f;
			float speedY2 = base.Projectile.velocity.Y * 0.5f;
			newColor = default(Color);
			Dust.NewDust(position2, width2, height2, 73, speedX2, speedY2, 0, newColor);
		}
		if (base.Projectile.timeLeft <= 60)
		{
			base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 0f, 60f, 255f, 0f);
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.94f;
		}
		else if (base.Projectile.scale < 2f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.99f;
			base.Projectile.scale += 0.02f;
		}
		if (base.Projectile.timeLeft > 60)
		{
			Vector2 position3 = base.Projectile.Center - base.Projectile.velocity;
			Vector2? velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.4f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position3, 119, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = 1.2f;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.alpha != 0)
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(137, 300);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.1f;
		if (base.Projectile.numHits >= 3 && base.Projectile.timeLeft > 60)
		{
			base.Projectile.timeLeft = 60;
		}
	}
}
