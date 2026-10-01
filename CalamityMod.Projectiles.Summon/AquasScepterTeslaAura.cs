using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AquasScepterTeslaAura : ModProjectile, ILocalizedModType, IModType
{
	private static float TeslaAuraScale = 3f;

	public bool ableToHit = true;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public sealed override void SetDefaults()
	{
		base.Projectile.width = 216;
		base.Projectile.height = 216;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 27;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 28;
		base.Projectile.alpha = 0;
		base.Projectile.spriteDirection = ((!Main.rand.NextBool()) ? 1 : (-1));
	}

	public override bool? CanDamage()
	{
		if (!ableToHit)
		{
			return false;
		}
		return null;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * (1f - (float)base.Projectile.alpha / 255f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, TeslaAuraScale * 96f, targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.6f);
	}

	public override void AI()
	{
		base.Projectile.scale = TeslaAuraScale;
		base.Projectile.alpha += 11;
		base.Projectile.ai[0]++;
		base.Projectile.frameCounter++;
		base.Projectile.frame = (int)((float)base.Projectile.frameCounter / 5.4f);
		if (base.Projectile.ai[0] > 8f)
		{
			ableToHit = false;
		}
	}
}
