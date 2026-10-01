using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class RubicoPrimeMag : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public bool TouchedGrass;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/RubicoPrimeMag";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.aiStyle = 14;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 700;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		base.Projectile.extraUpdates = 0;
		Time++;
		_ = Main.player[base.Projectile.owner];
		if (!TouchedGrass)
		{
			base.Projectile.rotation += 0.5f * (float)base.Projectile.direction;
		}
		base.Projectile.velocity.Y -= 0.055f;
		base.Projectile.velocity.X *= 0.992f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = 0;
		TouchedGrass = true;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		return false;
	}
}
