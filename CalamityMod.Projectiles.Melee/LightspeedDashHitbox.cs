using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class LightspeedDashHitbox : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 84;
		base.Projectile.height = 64;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 18;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Main.player[base.Projectile.owner].Center;
		Main.player[base.Projectile.owner].velocity = base.Projectile.velocity;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		Owner.Center.DirectionTo(Owner.ClampedMouseWorld() + ((float)Math.PI).ToRotationVector2());
		SoundStyle style = CommonCalamitySounds.SwiftSliceSound with
		{
			Volume = CommonCalamitySounds.SwiftSliceSound.Volume * 0.33f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		CalamityPlayer calamityPlayer = Main.player[base.Projectile.owner].Calamity();
		calamityPlayer.elementalMastery += 30;
		calamityPlayer.elementalMastery = Math.Min(calamityPlayer.elementalMastery, Lightspeed.MaxEnergy);
		int points = 2;
		float radians = (float)Math.PI * 2f / (float)points;
		Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f)).RotatedByRandom(100.0);
		for (int k = 0; k < points; k++)
		{
			Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.44999998807907104);
			GeneralParticleHandler.SpawnParticle(new RainbowGlowSparkParticle(target.Center + velocity * 7.5f, velocity * 0.5f, affectedByGravity: false, 14, 0.07f, Color.Aqua, new Vector2(0.55f, 0.825f), quickShrink: true, glow: true, 1f, 0.05f));
		}
		for (int i = 0; i < 12; i++)
		{
			Vector2 particleSpeed = target.Center.SafeNormalize(Vector2.One).RotatedByRandom(18.84955596923828) * Main.rand.NextFloat(4f, 8f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(target.Center, particleSpeed, Main.rand.NextFloat(0.4f, 0.9f), Color.OrangeRed, 50, 2f, 2.5f, 3f, 0.06f));
		}
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 120);
	}
}
