using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class WaterShotBuffer : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 1;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(Main.LocalPlayer.GetSource_FromThis(), base.Projectile.Center + new Vector2(3f, 0f), base.Projectile.velocity, ModContent.ProjectileType<WaterShot>(), base.Projectile.damage, base.Projectile.knockBack, Main.myPlayer);
			base.Projectile.Kill();
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
