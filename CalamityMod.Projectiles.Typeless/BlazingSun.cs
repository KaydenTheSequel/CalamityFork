using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BlazingSun : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 130;
		base.Projectile.height = 130;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 0;
		base.Projectile.timeLeft = 30;
	}

	public override void AI()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 18)
		{
			base.Projectile.scale -= 0.05f;
			if (base.Projectile.alpha < 255)
			{
				base.Projectile.alpha += 10;
			}
			if (base.Projectile.alpha > 255)
			{
				base.Projectile.alpha = 255;
			}
		}
		if (base.Projectile.timeLeft >= 30)
		{
			base.Projectile.scale += 0.6f;
		}
		base.Projectile.rotation += 0.025f;
		if (base.Projectile.timeLeft >= 18)
		{
			Lighting.AddLight(base.Projectile.Center, new Vector3(240f, 185f, 7f) * (1f / 85f));
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
