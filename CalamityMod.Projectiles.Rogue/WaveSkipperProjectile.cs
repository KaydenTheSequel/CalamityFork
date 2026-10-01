using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[LegacyName(new string[] { "DuneHopperProjectile" })]
public class WaveSkipperProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/WaveSkipper";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 6;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 20f && base.Projectile.velocity.Y < 24f)
		{
			base.Projectile.velocity.Y += 0.4f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.penetrate--;
		if (base.Projectile.penetrate <= 0)
		{
			base.Projectile.Kill();
		}
		else if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, base.Projectile.Calamity().stealthStrike ? 4 : 2);
		return false;
	}
}
