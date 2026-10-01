using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CrystalPiercerShard : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.rotation += (float)base.Projectile.direction * 0.05f;
		if (!initialized)
		{
			base.Projectile.scale = Main.rand.NextFloat(0.85f, 1.15f);
			initialized = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}
}
