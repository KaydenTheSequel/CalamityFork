using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ImpactRound : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	public static int Lifetime = 600;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/AMRShot";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.light = 0.5f;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 7;
		base.Projectile.scale = 1.18f;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.AIType = 242;
		base.Projectile.penetrate = 5;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized && base.Projectile.CountsAsClass<RangedDamageClass>())
		{
			initialized = true;
			if (!Main.dedServ)
			{
				SoundStyle style = CommonCalamitySounds.LargeWeaponFireSound with
				{
					Volume = CommonCalamitySounds.LargeWeaponFireSound.Volume * 0.45f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		if (base.Projectile.timeLeft == Lifetime - 4)
		{
			for (int i = 0; i <= 4; i++)
			{
				Vector2 spinninpoint = base.Projectile.velocity * 0.5f;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(scale: Main.rand.NextFloat(0.3f, 0.8f), velocity: spinninpoint.RotatedByRandom(0.44999998807907104) * Main.rand.NextFloat(0.4f, 0.95f), relativePosition: base.Projectile.Center, affectedByGravity: false, lifetime: 6, color: Main.rand.NextBool() ? Color.DarkOrange : Color.OrangeRed));
				float sparkScale2 = Main.rand.NextFloat(0.4f, 1f);
				Vector2 sparkvelocity2 = spinninpoint.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(1.1f, 3.1f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkvelocity2, affectedByGravity: false, 6, sparkScale2, Main.rand.NextBool() ? Color.DarkOrange : Color.OrangeRed));
			}
		}
		if (base.Projectile.timeLeft < Lifetime - 3 && base.Projectile.timeLeft > Lifetime - 150)
		{
			GeneralParticleHandler.SpawnParticle(new AltSparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 15, 1f, Color.OrangeRed * 0.1f));
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.Center, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.Center);
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Red, Color.OrangeRed, Utils.GetLerpValue(0f, 3f, 1f, clamped: true)), "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.05f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i <= 6; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 90 : 183, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.2f, 1.5f), 0, default(Color), Main.rand.NextFloat(1.6f, 2.2f));
			dust.noGravity = true;
			dust.fadeIn = 0.5f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		modifiers.CritDamage += 0.25f;
		for (int i = 0; i <= 2; i++)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, -base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(0.18f, 0.44f)) * Main.rand.NextFloat(0.4f, 2.5f), affectedByGravity: false, 8, 0.9f, Main.rand.NextBool() ? Color.Red : Color.OrangeRed));
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, -base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.18f, -0.44f)) * Main.rand.NextFloat(0.4f, 2.5f), affectedByGravity: false, 8, 0.9f, Main.rand.NextBool() ? Color.Red : Color.OrangeRed));
		}
		for (int j = 0; j <= 3; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool() ? 90 : 183, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.2f, 1.5f), 0, default(Color), Main.rand.NextFloat(1.6f, 2.2f));
			dust.noGravity = true;
			dust.fadeIn = 0.5f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return base.Projectile.timeLeft < 600;
	}
}
