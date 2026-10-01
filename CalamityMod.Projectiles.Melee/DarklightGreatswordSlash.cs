using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DarklightGreatswordSlash : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/ExobeamSlash";

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 512;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 2;
		base.Projectile.Opacity = 1f;
		base.Projectile.timeLeft = 35;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.scale = 0.75f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 12;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Opacity = (float)base.Projectile.timeLeft / 35f;
		if (base.Projectile.timeLeft == 34)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(0.1f, 0.1f), 100.0), affectedByGravity: false, 12, Main.rand.NextFloat(0.03f, 0.05f), Main.rand.NextBool() ? Color.Pink : Color.Cyan, new Vector2(2f, 0.5f), quickShrink: true));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}

	public override bool ShouldUpdatePosition()
	{
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp((base.Projectile.ai[2] == 0f) ? Color.Cyan : Color.Pink, (base.Projectile.ai[2] == 0f) ? Color.DarkBlue : Color.DarkRed, (float)base.Projectile.identity / 7f % 1f) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
