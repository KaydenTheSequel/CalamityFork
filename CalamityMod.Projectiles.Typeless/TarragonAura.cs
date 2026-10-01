using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class TarragonAura : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 2;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.ai[0] < 0f || base.Projectile.ai[0] > 199f || base.Projectile.ai[0] == (float)target.whoAmI)
		{
			return null;
		}
		return false;
	}
}
