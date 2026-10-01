using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DuststormCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Generic;
	}

	public override void AI()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.995f;
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 120f)
		{
			if (base.Projectile.alpha < 255)
			{
				base.Projectile.alpha += 5;
				if (base.Projectile.alpha > 255)
				{
					base.Projectile.alpha = 255;
				}
			}
			else if (base.Projectile.owner == Main.myPlayer)
			{
				base.Projectile.Kill();
			}
		}
		else if (base.Projectile.alpha > 80)
		{
			base.Projectile.alpha -= 30;
			if (base.Projectile.alpha < 80)
			{
				base.Projectile.alpha = 80;
			}
		}
	}
}
