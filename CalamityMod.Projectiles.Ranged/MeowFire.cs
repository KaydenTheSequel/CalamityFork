using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MeowFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public static int Lifetime => 72;

	public bool PinkFire => base.Projectile.ai[0] == 1f;

	public ref float Time => ref base.Projectile.ai[1];

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
		base.Projectile.idStaticNPCHitCooldown = 8;
	}

	public override void AI()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time >= 6f)
		{
			base.Projectile.scale = 1.5f * Utils.GetLerpValue(6f, 30f, Time, clamped: true);
			float smokeRot = MathHelper.ToRadians(3f);
			Color smokeColor = Color.Lerp(PinkFire ? Color.DeepPink : Color.DodgerBlue, PinkFire ? Color.Pink : Color.PowderBlue, 0.5f + 0.3f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f));
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, smokeColor, 12, base.Projectile.scale * Main.rand.NextFloat(0.6f, 1.2f), 0.25f, smokeRot, glowing: true, 0f, required: true));
			if (Main.rand.NextBool(8))
			{
				Color glowColor = Color.Lerp(smokeColor, Color.White, 0.2f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, glowColor, 8, base.Projectile.scale * Main.rand.NextFloat(0.4f, 0.7f), 0.15f, smokeRot, glowing: true, 0.005f));
			}
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref smokeColor)).ToVector3() * base.Projectile.scale * 0.2f);
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 52f * base.Projectile.scale * 0.5f, targetHitbox);
	}
}
