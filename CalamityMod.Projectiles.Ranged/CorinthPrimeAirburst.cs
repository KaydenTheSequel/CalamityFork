using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class CorinthPrimeAirburst : BaseMassiveExplosionProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override int Lifetime => 60;

	public override bool UsesScreenshake => base.Projectile.damage > 1;

	public override float GetScreenshakePower(float pulseCompletionRatio)
	{
		return CalamityUtils.Convert01To010(pulseCompletionRatio) * 16f;
	}

	public override Color GetCurrentExplosionColor(float pulseCompletionRatio)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Blue * 1.6f, Color.Cyan, MathHelper.Clamp(pulseCompletionRatio * 2.2f, 0f, 1f));
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void PostAI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0f, 0.3f);
	}
}
