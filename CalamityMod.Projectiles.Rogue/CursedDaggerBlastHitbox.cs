using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CursedDaggerBlastHitbox : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 200;
		base.Projectile.height = 200;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft % 30 == 0)
		{
			SoundStyle style = SoundID.DD2_SkyDragonsFurySwing with
			{
				Volume = 1.2f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
		}
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(200f, 200f), 75);
		dust.scale = Main.rand.NextFloat(0.8f, 1.1f);
		dust.noGravity = true;
		int sparkCount = Main.rand.Next(18);
		float offset = Main.rand.NextFloat((float)Math.PI * 2f);
		for (int i = 0; i < sparkCount; i++)
		{
			float angle = (float)i / (float)sparkCount * ((float)Math.PI * 2f) + offset;
			Vector2 relativePosition = base.Projectile.Center + angle.ToRotationVector2() * (float)Main.rand.Next(65, 200);
			int sparkLifetime = Main.rand.Next(10, 18);
			float sparkScale = Main.rand.NextFloat(0.8f, 1f) * 0.955f;
			Color sparkColor = Color.Lerp(Color.LawnGreen, Color.Green, Main.rand.NextFloat(0.7f));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(color: Color.Lerp(sparkColor, Color.LawnGreen, Main.rand.NextFloat()), relativePosition: relativePosition, velocity: (angle - (float)Math.PI / 2f * (float)base.Projectile.direction).ToRotationVector2() * Main.rand.NextFloat(2f, 5.5f), affectedByGravity: false, lifetime: sparkLifetime, scale: sparkScale));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(39, 600);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, base.Projectile.width, targetHitbox);
	}
}
