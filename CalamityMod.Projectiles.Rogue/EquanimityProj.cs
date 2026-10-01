using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class EquanimityProj : ModProjectile, ILocalizedModType, IModType
{
	public static int ChargeupTime = 10;

	public static int Lifetime = 500;

	public bool swapType;

	public CalamityUtils.CurveSegment pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);

	public CalamityUtils.CurveSegment throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Equanimity";

	public float OverallProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ThrowProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ChargeProgress => 1f - (float)(base.Projectile.timeLeft - Lifetime) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Returning => ref base.Projectile.ai[0];

	public ref float Bouncing => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 46;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool ShouldUpdatePosition()
	{
		return ChargeProgress >= 1f;
	}

	public override bool? CanDamage()
	{
		if (ChargeProgress < 1f)
		{
			return false;
		}
		if (Returning == 1f)
		{
			return false;
		}
		return base.CanDamage();
	}

	internal float ArmAnticipationMovement()
	{
		return CalamityUtils.PiecewiseAnimation(ChargeProgress, pullback, throwout);
	}

	public override void AI()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center;
		if (ChargeProgress < 1f)
		{
			float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
			Owner.heldProj = base.Projectile.whoAmI;
			Projectile projectile = base.Projectile;
			Vector2 mountedCenter = Owner.MountedCenter;
			Vector2 unitY = Vector2.UnitY;
			double radians = armRotation * Owner.gravDir;
			center = default(Vector2);
			projectile.Center = mountedCenter + unitY.RotatedBy(radians, center) * -40f * Owner.gravDir;
			base.Projectile.rotation = (-(float)Math.PI / 2f + armRotation) * Owner.gravDir;
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
			base.Projectile.tileCollide = false;
			return;
		}
		if (base.Projectile.timeLeft == Lifetime)
		{
			SoundEngine.PlaySound(in SoundID.DD2_GoblinBomberThrow, base.Projectile.Center);
			base.Projectile.Center = Owner.MountedCenter + base.Projectile.velocity * 12f;
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 17.5f;
		}
		base.Projectile.rotation += ((float)Math.PI / 16f + (float)Math.PI / 8f * Math.Clamp(ThrowProgress * 2f, 0f, 1f)) * (float)Math.Sign(base.Projectile.velocity.X);
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.Center);
			base.Projectile.soundDelay = 8;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 2f && Bouncing == 0f)
		{
			Returning = 1f;
			base.Projectile.numHits = 0;
		}
		if (Returning == 0f && Bouncing == 0f && ((Vector2)(ref base.Projectile.velocity)).Length() > 2f && base.Projectile.timeLeft < 455 + ChargeupTime)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.88f;
		}
		if (Returning == 1f && ((Vector2)(ref base.Projectile.velocity)).Length() < 20f)
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 1.1f;
		}
		for (int i = 0; i < 2; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center + ((float)i * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 14f, Main.rand.NextBool() ? 91 : 109, ((float)i * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2() * 3f).noGravity = true;
		}
		if (Returning == 1f)
		{
			base.Projectile.velocity = ((Vector2)(ref base.Projectile.velocity)).Length() * (Owner.MountedCenter - base.Projectile.Center).SafeNormalize(Vector2.One);
			center = base.Projectile.Center - Owner.MountedCenter;
			if (((Vector2)(ref center)).Length() < 24f)
			{
				base.Projectile.Kill();
			}
			if (base.Projectile.numHits >= 7)
			{
				base.Projectile.Kill();
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a86: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0980: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(31, 30);
		if (!base.Projectile.Calamity().stealthStrike)
		{
			Main.rand.NextFloat((float)Math.PI * 2f);
			Vector2 shootVelocity = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.8f, 1.6f);
			if (!swapType)
			{
				for (int s = 0; s < 2; s++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity.RotatedByRandom(100.0), ModContent.ProjectileType<EquanimityLightShard>(), (int)((float)base.Projectile.damage * 0.4f), 0f, base.Projectile.owner);
				}
			}
			if (swapType)
			{
				for (int i = 0; i < 2; i++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity.RotatedByRandom(100.0), ModContent.ProjectileType<EquanimityDarkShard>(), (int)((float)base.Projectile.damage * 0.4f), 0f, base.Projectile.owner);
				}
			}
			swapType = !swapType;
		}
		else
		{
			Main.rand.NextFloat((float)Math.PI * 2f);
			Vector2 shootVelocity2 = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.8f, 1.6f);
			if (!swapType)
			{
				SoundStyle style = DeadSunsWind.Explosion with
				{
					Volume = 0.4f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int j = 0; j < 3; j++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Indigo, "CalamityMod/Particles/LargeBloom", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.3f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/FlameExplosion2", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.04f, 25, UseAdditiveBlend: false, 0.85f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Black, "CalamityMod/Particles/FlameExplosion2", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.07f, 25, UseAdditiveBlend: false, 0.85f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Indigo * 0.55f, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, 0.08f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				for (int k = 0; k < 13; k++)
				{
					Vector2 randVel = Utils.RotatedByRandom(new Vector2(7f, 7f), 100.0) * Main.rand.NextFloat(0.8f, 1.6f);
					GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + randVel, randVel, Color.Black, Main.rand.Next(15, 26), Main.rand.NextFloat(0.3f, 0.5f), 0.8f));
				}
				float blastSize = 90f;
				float minMultiplier = 0.25f;
				int hitsToMinMult = 8;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult).DamageType = RogueDamageClass.Instance;
				for (int l = 0; l < 2; l++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity2.RotatedByRandom(100.0), ModContent.ProjectileType<EquanimityLightShard>(), (int)((float)base.Projectile.damage * 0.8f), 0f, base.Projectile.owner, 1f, 1f);
				}
			}
			if (swapType)
			{
				SoundStyle style = LunicEye.UseSound with
				{
					Volume = 0.7f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int m = 0; m < 3; m++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.LightPink, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.3f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.WhiteSmoke, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.07f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.MediumVioletRed, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.07f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.LightPink, "CalamityMod/Particles/DetailedExplosion", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.3f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.LightPink, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.04f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.09f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				for (int n = 0; n < 10; n++)
				{
					GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.3f, 1f), Color.White, Color.Orchid, 0.9f, 20, 2f, 2.2f));
				}
				for (int num = 0; num < 10; num++)
				{
					Vector2 velocity = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
					float colorRando = Main.rand.NextFloat(0f, 1f);
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + velocity, velocity, affectedByGravity: false, 11, Main.rand.NextFloat(0.009f, 0.005f), Color.Lerp(Color.DarkOrchid, Color.IndianRed, colorRando), new Vector2(2.2f, 0.9f), quickShrink: true));
				}
				float blastSize2 = 90f;
				float minMultiplier2 = 0.25f;
				int hitsToMinMult2 = 8;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner, blastSize2, minMultiplier2, hitsToMinMult2).DamageType = RogueDamageClass.Instance;
				for (int num2 = 0; num2 < 2; num2++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity2.RotatedByRandom(100.0), ModContent.ProjectileType<EquanimityDarkShard>(), (int)((float)base.Projectile.damage * 0.8f), 0f, base.Projectile.owner, 1f, 1f);
				}
			}
			swapType = !swapType;
		}
		if (base.Projectile.numHits > 4)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.3f;
			Returning = 1f;
			return;
		}
		NPC newTarget = null;
		float closestNPCDistance = 10000f;
		float targettingDistance = 900f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n2 = enumerator.Current;
			if (n2.whoAmI != target.whoAmI && n2.CanBeChasedBy(base.Projectile))
			{
				Vector2 val = base.Projectile.Center - n2.Center;
				float potentialNewDistance = ((Vector2)(ref val)).Length();
				if (potentialNewDistance < targettingDistance && potentialNewDistance < closestNPCDistance)
				{
					closestNPCDistance = potentialNewDistance;
					newTarget = n2;
					base.Projectile.velocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, newTarget, 30f, 3);
				}
			}
		}
		if (newTarget == null)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.3f;
			Returning = 1f;
		}
		else
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.85f);
			Bouncing = 2f;
		}
	}
}
