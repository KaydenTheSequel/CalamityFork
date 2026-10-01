using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ShadeFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public static int Lifetime => 60;

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 6);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 3;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 12;
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time >= 6f)
		{
			base.Projectile.scale = 1.5f * Utils.GetLerpValue(6f, 36f, Time, clamped: true);
			float smokeRot = MathHelper.ToRadians(3f);
			Color smokeColor = Color.Lerp(Color.BlueViolet, Color.Black, 0.7f + 0.2f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f));
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, smokeColor, 20, base.Projectile.scale * Main.rand.NextFloat(0.6f, 1.2f), 0.8f, smokeRot, glowing: false, 0f, required: true));
			if (Main.rand.NextBool(5))
			{
				Color glowColor = Color.Lerp(smokeColor, Color.MidnightBlue, 0.25f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, glowColor, 15, base.Projectile.scale * Main.rand.NextFloat(0.4f, 0.7f), 0.8f, smokeRot, glowing: true, 0.005f));
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 52f * base.Projectile.scale * 0.5f, targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrainRot>(), 1200);
	}
}
