using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Razorwind : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/TornadoProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 2;
		base.Projectile.penetrate = 3;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
	}

	public override void AI()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		base.Projectile.alpha -= 5;
		if (base.Projectile.alpha < 50)
		{
			base.Projectile.alpha = 50;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 4f)
		{
			for (int i = 0; i < 3; i++)
			{
				int blueDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 187, 0f, 0f, 100, new Color(53, Main.DiscoG, 255), 2f);
				Main.dust[blueDust].noGravity = true;
				Dust obj = Main.dust[blueDust];
				obj.velocity *= 0f;
			}
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 250f, 12f, 20f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 10; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 187, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 100, new Color(53, Main.DiscoG, 255));
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(53, Main.DiscoG, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
