using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGDeathBoom : BaseMassiveExplosionProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override int Lifetime => 180;

	public override bool UsesScreenshake => true;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override float GetScreenshakePower(float pulseCompletionRatio)
	{
		return CalamityUtils.Convert01To010(pulseCompletionRatio) * 28f;
	}

	public override Color GetCurrentExplosionColor(float pulseCompletionRatio)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Cyan, Color.Fuchsia, MathHelper.Clamp(pulseCompletionRatio * 1.75f, 0f, 1f));
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime;
		base.CooldownSlot = 1;
	}

	public override void PostAI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		base.MaxRadius = 4200f;
		Lighting.AddLight(base.Projectile.Center, 0.1f, 0.1f, 0.1f);
	}
}
