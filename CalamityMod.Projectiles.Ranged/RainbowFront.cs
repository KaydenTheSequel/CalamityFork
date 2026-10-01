using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class RainbowFront : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "Terraria/Images/Projectile_251";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.scale = 1.25f;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] > 4f)
			{
				base.Projectile.localAI[0] = 3f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, base.Projectile.velocity.X * 0.001f, base.Projectile.velocity.Y * 0.001f, ModContent.ProjectileType<RainbowTrail>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			if (base.Projectile.timeLeft > 1200)
			{
				base.Projectile.timeLeft = 1200;
			}
		}
		float gravityControl = 1f;
		if (base.Projectile.velocity.Y < 0f)
		{
			gravityControl -= base.Projectile.velocity.Y / 3f;
		}
		base.Projectile.ai[0] += gravityControl;
		if (base.Projectile.ai[0] > 30f)
		{
			base.Projectile.velocity.Y += 0.5f;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.X *= 0.95f;
			}
			else
			{
				base.Projectile.velocity.X *= 1.05f;
			}
		}
		float x = base.Projectile.velocity.X;
		float y = base.Projectile.velocity.Y;
		float velocityMult = 15.95f * base.Projectile.scale / (float)Math.Sqrt((double)x * (double)x + (double)y * (double)y);
		float xVel = x * velocityMult;
		float yVel = y * velocityMult;
		base.Projectile.velocity.X = xVel;
		base.Projectile.velocity.Y = yVel;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.Transparent;
	}
}
