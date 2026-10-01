using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolyExplosionBoom : BaseMassiveExplosionProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override int Lifetime => 60;

	public override bool UsesScreenshake => true;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override float GetScreenshakePower(float pulseCompletionRatio)
	{
		return CalamityUtils.Convert01To010(pulseCompletionRatio) * 16f;
	}

	public override Color GetCurrentExplosionColor(float pulseCompletionRatio)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp((Color)(Main.zenithWorld ? new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB) : (Main.IsItDay() ? Color.Orange : Color.BlueViolet)) * 1.6f, Color.White, MathHelper.Clamp(pulseCompletionRatio * 2.2f, 0f, 1f));
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
		base.MaxRadius = 3000f;
		Lighting.AddLight(base.Projectile.Center, 0.1f, 0.1f, 0.1f);
	}
}
