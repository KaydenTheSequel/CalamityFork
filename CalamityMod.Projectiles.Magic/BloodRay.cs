using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BloodRay : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 150;

	public const float MaxExponentialDamageBoost = 3f;

	public static readonly float ExponentialDamageBoost = (float)Math.Pow(3.0, 0.006666666828095913);

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public ref float InitialDamage => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 10;
		base.Projectile.extraUpdates = 100;
		base.Projectile.timeLeft = 150;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		if (InitialDamage == 0f)
		{
			InitialDamage = base.Projectile.damage;
			base.Projectile.netUpdate = true;
		}
		Time++;
		base.Projectile.damage = (int)((double)InitialDamage * Math.Pow(ExponentialDamageBoost, Time));
		if (Time >= 12f)
		{
			for (int i = 0; i < 2; i++)
			{
				int dustType = (Main.rand.NextBool(4) ? 182 : 235);
				Dust dust = Dust.NewDustPerfect(base.Projectile.position - base.Projectile.velocity * (float)i / 2f, dustType);
				dust.scale = Main.rand.NextFloat(0.96f, 1.04f) * MathHelper.Lerp(1f, 1.7f, Time / 150f);
				dust.noGravity = true;
				dust.velocity *= 0.1f;
			}
		}
	}
}
