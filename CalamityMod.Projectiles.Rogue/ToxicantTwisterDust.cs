using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ToxicantTwisterDust : ModProjectile, ILocalizedModType, IModType
{
	public NPC targetedNPC;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (base.Projectile.ai[1] % 2f == 0f && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-2f, -1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 5, 0.04f, Color.Lerp(Color.Green, Color.Chartreuse, 0.8f) * 0.65f, new Vector2(1f, 0.3f), quickShrink: true, glow: false, 1.5f));
		}
		base.Projectile.rotation += 0.2f;
		targetedNPC = ((base.Projectile.ai[1] > 90f) ? base.Projectile.Center.ClosestNPCAt(1200f) : null);
		float moveSpeed = Utils.GetLerpValue(200f, 0f, base.Projectile.timeLeft) * 0.5f;
		if (targetedNPC == null)
		{
			Vector2 moveToMouse = (Owner.ClampedMouseWorld() + Utils.RotatedBy(new Vector2(0f, -250f), (double)(base.Projectile.rotation * 0.2f), default(Vector2)).RotatedBy(MathHelper.ToRadians(90f) * base.Projectile.ai[2]) - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 8f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += moveToMouse * 0.7f;
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.9f;
			}
		}
		else
		{
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targetedNPC, ignoreTiles: true, moveSpeed, 13f, 0.98f);
			if (base.Projectile.ai[1] % 2f == 0f)
			{
				base.Projectile.timeLeft++;
			}
		}
		if (targetedNPC != null && targetedNPC.life <= 0)
		{
			targetedNPC = null;
		}
		base.Projectile.ai[1]++;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target != targetedNPC)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 180);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 40f, targetHitbox);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 2; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, 75, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.3f, 1.8f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.8f)).noGravity = true;
		}
	}
}
