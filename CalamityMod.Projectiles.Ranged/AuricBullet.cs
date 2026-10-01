using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AuricBullet : ModProjectile, ILocalizedModType, IModType
{
	public int Heat = 2;

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/Item/AuricBulletHit")
	{
		Volume = 0.7f
	};

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 9;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 15;
		base.Projectile.tileCollide = false;
		base.Projectile.ArmorPenetration = 50;
		base.Projectile.alpha = 255;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, 0, lightColor);
		return false;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		Vector3 DustLight = default(Vector3);
		((Vector3)(ref DustLight))._002Ector(0.255f, 0.23f, 0f);
		Lighting.AddLight(base.Projectile.Center, DustLight * 2f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		base.Projectile.spriteDirection = base.Projectile.direction;
		if (base.Projectile.timeLeft < 595)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 250f, 7.5f, 10f);
		}
		if (num < 1400f && base.Projectile.timeLeft < 599 && base.Projectile.timeLeft % 2 == 0)
		{
			if (base.Projectile.timeLeft < 585)
			{
				Heat = 5;
			}
			if (base.Projectile.timeLeft < 580)
			{
				Heat = 10;
			}
			if (base.Projectile.timeLeft < 575)
			{
				Heat = 20;
			}
			int positionVariation = ((base.Projectile.timeLeft < 590) ? 17 : 7);
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center + Main.rand.NextVector2Circular(positionVariation, positionVariation), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 1.1f), affectedByGravity: false, 4, 1.45f, (!Main.rand.NextBool(Heat)) ? ((base.Projectile.timeLeft > 590) ? Color.Red : Color.DarkGoldenrod) : ((base.Projectile.timeLeft < 570) ? Color.Goldenrod : Color.OrangeRed)));
		}
		if (Main.rand.NextBool(7))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), Main.rand.NextBool() ? 311 : 292, -base.Projectile.velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.1f, 0.6f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.7f, 1.1f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AuricRebuke>(), 240);
		SoundStyle style = HitSound with
		{
			PitchVariance = 0.15f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.Gold, Color.Cyan, Main.rand.NextFloat(1.8f, 2.5f), 5, Main.rand.NextFloat(-0.01f, 0.01f), 1.68f));
		Vector2 bloodSpawnPosition = target.Center + Main.rand.NextVector2Circular(target.width, target.height) * 0.04f;
		Vector2 spinninpoint = (base.Projectile.Center - bloodSpawnPosition).SafeNormalize(Vector2.UnitY);
		int sparkLifetime = Main.rand.Next(9, 12);
		float sparkScale = Main.rand.NextFloat(0.9f, 1.3f) * 0.85f;
		Color sparkColor = Color.Lerp(Color.DarkGoldenrod, Color.Gold, Main.rand.NextFloat(0.7f));
		Vector2 sparkVelocity = spinninpoint.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(22f, 45f);
		sparkVelocity.Y -= 6f;
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
		}
		for (int i = 0; i <= 6; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 226, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.1f, 2.9f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.3f, 0.9f);
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (!target.CanBeChasedBy())
		{
			return false;
		}
		return null;
	}
}
