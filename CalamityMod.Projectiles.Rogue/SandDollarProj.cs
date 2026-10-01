using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SandDollarProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/SandDollar";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 28;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.aiStyle = 3;
		base.Projectile.timeLeft = 300;
		base.AIType = 272;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] += 0.1f;
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}
}
