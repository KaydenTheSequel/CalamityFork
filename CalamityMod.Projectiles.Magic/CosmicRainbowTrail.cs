using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CosmicRainbowTrail : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 25;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "Terraria/Images/Projectile_251";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.alpha = 255;
		base.Projectile.light = 0.3f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 25;
		base.Projectile.ignoreWater = true;
		base.Projectile.scale = 1.25f;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 6;
	}

	public override void AI()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.spriteDirection = (base.Projectile.velocity.X <= 0f).ToDirectionInt();
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
			base.Projectile.localAI[0] = 1f;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		if (base.Projectile.timeLeft < 6)
		{
			base.Projectile.alpha = 255 - (int)(255f * (float)base.Projectile.timeLeft / 6f);
		}
		else if (base.Projectile.timeLeft > 19)
		{
			base.Projectile.alpha = 255 - (int)(255f * (float)(25 - base.Projectile.timeLeft) / 6f);
		}
		else
		{
			base.Projectile.alpha = 0;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 0);
	}
}
