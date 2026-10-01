using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class EGloveSpark : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Generic;
	}

	public override void AI()
	{
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.05f;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.3f;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y && base.Projectile.velocity.Y > 0.5f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y * -0.3f;
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
		int sparky = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 206, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
		Main.dust[sparky].position.X -= 2f;
		Main.dust[sparky].position.Y += 2f;
		Main.dust[sparky].scale += (float)Main.rand.Next(50) * 0.01f;
		Main.dust[sparky].noGravity = true;
		Main.dust[sparky].velocity.Y -= 2f;
		if (Main.rand.NextBool())
		{
			int sparky2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 206, 0f, 0f, 100, new Color(Main.rand.Next(20, 100), 204, 250));
			Main.dust[sparky2].position.X -= 2f;
			Main.dust[sparky2].position.Y += 2f;
			Main.dust[sparky2].scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
			Main.dust[sparky2].noGravity = true;
			Dust obj = Main.dust[sparky2];
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

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
