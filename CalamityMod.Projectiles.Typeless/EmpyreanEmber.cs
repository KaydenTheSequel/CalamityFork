using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class EmpyreanEmber : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
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
				base.Projectile.velocity.X *= 0.97f;
				if (base.Projectile.velocity.X > -0.01f && base.Projectile.velocity.X < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.2f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		if (Main.rand.NextBool(3))
		{
			int ourpleDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 62, 0f, 0f, 100, default(Color), 0.75f);
			Dust obj = Main.dust[ourpleDust];
			obj.position.X -= 2f;
			obj.position.Y += 2f;
			obj.scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
			obj.noGravity = true;
			obj.velocity *= 0.1f;
		}
		else
		{
			int ourpleDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 62, 0f, 0f, 100, default(Color), 0.75f);
			Dust obj2 = Main.dust[ourpleDust2];
			obj2.position.X -= 2f;
			obj2.position.Y += 2f;
			obj2.scale += (float)Main.rand.Next(50) * 0.01f;
			obj2.noGravity = true;
			obj2.velocity.Y -= 2f;
		}
		if (base.Projectile.velocity.Y < 0.25f && base.Projectile.velocity.Y > 0.15f)
		{
			base.Projectile.velocity.X *= 0.8f;
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
