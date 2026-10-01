using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VenusianFlame : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.rotation = (0f - base.Projectile.velocity.X) * 0.05f + (float)Math.PI / 2f;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		base.Projectile.ai[1]--;
		if (!initialized)
		{
			initialized = true;
			base.Projectile.frame = Main.rand.Next(Main.projFrames[base.Type]);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X *= -0.1f;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X *= -0.5f;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y && base.Projectile.velocity.Y > 1f)
		{
			base.Projectile.velocity.Y *= -0.5f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 5f)
		{
			base.Projectile.ai[0] = 5f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X *= 0.97f;
				if (base.Projectile.velocity.X > -0.01f && base.Projectile.velocity.X < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y += 0.2f;
		}
		if (base.Projectile.velocity.Y < 0.25f && base.Projectile.velocity.Y > 0.15f)
		{
			base.Projectile.velocity.X *= 0.8f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		if (base.Projectile.velocity.Y < 0.25f && base.Projectile.velocity.Y > 0.15f)
		{
			base.Projectile.velocity.X *= 0.8f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		if (Main.rand.NextBool(6))
		{
			int venusDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 55, 0f, 0f, 100);
			Dust obj = Main.dust[venusDust];
			obj.position.X -= 2f;
			obj.position.Y += 2f;
			obj.scale += (float)Main.rand.Next(50) * 0.01f;
			obj.noGravity = true;
			obj.velocity.Y -= 2f;
			obj.velocity += base.Projectile.velocity * 0.5f;
		}
		if (Main.rand.NextBool(14))
		{
			int venusDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 278, 0f, 0f, 0, default(Color), Main.rand.NextFloat(0.3f, 0.5f));
			Dust obj2 = Main.dust[venusDust2];
			obj2.position.X -= 2f;
			obj2.position.Y += 2f;
			obj2.noGravity = true;
			obj2.velocity *= 0.1f;
			obj2.velocity += base.Projectile.velocity * 0.5f;
			obj2.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Orange : Color.Coral, 0.7f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(189, 150);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.ai[1] = 10f;
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
