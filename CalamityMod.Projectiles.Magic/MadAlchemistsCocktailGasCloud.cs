using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MadAlchemistsCocktailGasCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 9;
	}

	public override void AI()
	{
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] > 60f)
		{
			base.Projectile.ai[0] += 10f;
		}
		if (base.Projectile.ai[0] > 255f)
		{
			base.Projectile.Kill();
			base.Projectile.ai[0] = 255f;
		}
		base.Projectile.alpha = (int)(100.0 + (double)base.Projectile.ai[0] * 0.7);
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		base.Projectile.rotation += (float)base.Projectile.direction * 0.003f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.96f;
	}
}
