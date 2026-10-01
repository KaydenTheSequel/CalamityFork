using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class WulfrumBobber : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = 61;
		base.Projectile.bobber = true;
	}
}
