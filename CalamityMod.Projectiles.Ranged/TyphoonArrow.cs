using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TyphoonArrow : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 1;
		base.Projectile.aiStyle = 1;
		base.Projectile.timeLeft = 600;
		base.AIType = 1;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<TyphoonBubble>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
	}
}
