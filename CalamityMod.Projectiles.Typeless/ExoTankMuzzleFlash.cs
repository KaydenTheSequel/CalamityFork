using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ExoTankMuzzleFlash : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
	}

	public override void AI()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 3 * Main.projFrames[base.Type])
		{
			base.Projectile.Kill();
		}
		base.Projectile.frame = base.Projectile.frameCounter / 3;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
