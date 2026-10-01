using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AegisFlame : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.1f;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.5f;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y && base.Projectile.velocity.Y > 1f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y * -0.5f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 5f)
		{
			base.Projectile.ai[0] = 5f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
				if ((double)base.Projectile.velocity.X > -0.01 && (double)base.Projectile.velocity.X < 0.01)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.2f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		int goldFlameDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, new Color(255, Main.DiscoG, 53));
		Main.dust[goldFlameDust].position.X -= 2f;
		Main.dust[goldFlameDust].position.Y += 2f;
		Main.dust[goldFlameDust].scale += (float)Main.rand.Next(50) * 0.01f;
		Main.dust[goldFlameDust].noGravity = true;
		Main.dust[goldFlameDust].velocity.Y -= 2f;
		if (Main.rand.NextBool())
		{
			int goldFlameDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, 0f, 0f, 100, new Color(255, Main.DiscoG, 53));
			Main.dust[goldFlameDust2].position.X -= 2f;
			Main.dust[goldFlameDust2].position.Y += 2f;
			Main.dust[goldFlameDust2].scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
			Main.dust[goldFlameDust2].noGravity = true;
			Dust obj = Main.dust[goldFlameDust2];
			obj.velocity *= 0.1f;
		}
		if ((double)base.Projectile.velocity.Y < 0.25 && (double)base.Projectile.velocity.Y > 0.15)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.8f;
		}
		base.Projectile.rotation = (0f - base.Projectile.velocity.X) * 0.05f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<AegisBlast2>(), (int)((double)hit.Damage * 0.75), hit.Knockback, Main.myPlayer);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
