using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SlurperBobber : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = 61;
		base.Projectile.bobber = true;
	}

	public override bool PreDrawExtras()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0f, 0f);
		return true;
	}
}
