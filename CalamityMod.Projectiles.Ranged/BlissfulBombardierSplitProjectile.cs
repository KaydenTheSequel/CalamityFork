using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BlissfulBombardierSplitProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	private ref float RocketID => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 4;
		base.Projectile.timeLeft = 400;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (base.Projectile.Center.Y > Owner.ClampedMouseWorld().Y)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.005f;
		if (base.Projectile.timeLeft % 2 == 0 && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-2f, -1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 5, 0.06f, BlissfulBombardierHoldout.effectsColor * 0.65f, new Vector2(1f, 0.3f), quickShrink: true, glow: false, 1.5f));
			return;
		}
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), (base.Projectile.velocity * -4f).RotatedByRandom(0.2) * Main.rand.NextFloat(0.2f, 1f));
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(0.35f, 0.55f);
		dust.color = (Main.rand.NextBool(3) ? Color.Orange : Color.Goldenrod);
		dust.noLightEmittence = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			CalamityUtils.RocketBehaviorInfo rocketBehaviorInfo = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
			rocketBehaviorInfo.clusterProjectileID = 0;
			rocketBehaviorInfo.destructiveClusterProjectileID = 0;
			CalamityUtils.RocketBehaviorInfo info = rocketBehaviorInfo;
			if (RocketID == 4445f)
			{
				_ = 1;
			}
			else
				_ = RocketID == 4446f;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastImpact");
			style.Volume = 0.6f;
			style.PitchVariance = 0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			int blastRadius = (int)((float)MathHelper.Clamp(base.Projectile.RocketBehavior(info), 3, 100) * 0.5f);
			base.Projectile.ExpandHitboxBy((float)blastRadius);
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.5f);
			base.Projectile.penetrate = -1;
			base.Projectile.Damage();
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Orange, "CalamityMod/Particles/SoftRoundExplosion", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.025f * (float)blastRadius, 13, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, BlissfulBombardierHoldout.staticEffectsColor, "CalamityMod/Particles/SmallBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.22f * (float)blastRadius, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int i = 0; i < 8; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), (new Vector2(5f, 5f) * (float)blastRadius).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.85f, 2.45f) * (float)blastRadius * 0.08f;
				dust.color = Color.Goldenrod;
				dust.noLightEmittence = true;
			}
			for (int j = 0; j < 4; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(10f, 10f), base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f * Main.rand.NextFloat(0.2f, 1f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 15, Main.rand.NextFloat(1.35f, 1.6f), Main.rand.NextBool(4) ? Color.Khaki : BlissfulBombardierHoldout.effectsColor, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.45f, 0.55f)));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/BlissfulBombardierSplitProjectile", (AssetRequestMode)2).Value;
		Projectile projectile = base.Projectile;
		Color staticEffectsColor = BlissfulBombardierHoldout.staticEffectsColor;
		((Color)(ref staticEffectsColor)).A = 0;
		projectile.DrawProjectileWithBackglow(staticEffectsColor, lightColor, 3f, texture, null, (SpriteEffects)0);
		return false;
	}
}
