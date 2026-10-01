using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CosmicRainbowFront : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "Terraria/Images/Projectile_251";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 210;
		base.Projectile.scale = 1.25f;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, base.Projectile.velocity.X * 0.001f, base.Projectile.velocity.Y * 0.001f, ModContent.ProjectileType<CosmicRainbowTrail>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		float velocityMult = 39.75f / ((Vector2)(ref base.Projectile.velocity)).Length();
		float xVel = base.Projectile.velocity.X * velocityMult;
		float yVel = base.Projectile.velocity.Y * velocityMult;
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
