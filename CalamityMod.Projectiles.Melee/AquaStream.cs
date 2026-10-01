using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AquaStream : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.scale = 1f;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 150;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 90;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
	}

	public override void AI()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.X += base.Projectile.ai[0];
		base.Projectile.velocity.Y += base.Projectile.ai[1];
		Dust.NewDustPerfect(base.Projectile.Center, 33, (Vector2?)new Vector2(0f, 0f), 0, new Color(0, 142, 255), 1.5f).noGravity = true;
	}
}
