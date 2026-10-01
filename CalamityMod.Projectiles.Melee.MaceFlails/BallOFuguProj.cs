using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.MaceFlails;

public class BallOFuguProj : BaseMaceFlailProjectile
{
	public static float MaxSpikeTime = 180f;

	public static float SpikeRate = 10f;

	public static Asset<Texture2D> ChargeGlow;

	public override int AssociatedItemID => ModContent.ItemType<BallOFugu>();

	public override float SpinHitboxRadius => 80f;

	public override float SpinVisualRadius => 48f;

	public override float LaunchSpeed => 20f;

	public override int LaunchLifespan => 20;

	public override float MaxDropRange => 640f;

	public override float MaxRetractSpeed => 24f;

	public override float RetractAcceleration => 3.6f;

	public static float SpikeDamage => 0.6f;

	public static float SpikeKnockback => 0.2f;

	public static Color SpikeColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(91, 62, 153);
		}
	}

	public ref float SpikeTimer => ref base.Projectile.ai[2];

	public override Action<Projectile> EffectBeforePullback => delegate
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		int num = (int)(MathHelper.Clamp(SpikeTimer, 0f, MaxSpikeTime) / SpikeRate);
		if (num > 0)
		{
			SoundEngine.PlaySound(in BallOFugu.BlowSound, base.Projectile.Center);
			for (int i = -4; i < 5; i++)
			{
				Vector2 squish = new Vector2(Main.rand.NextFloat(1f, 1.2f), Main.rand.NextFloat(0.6f, 0.75f)) * ((i % 2 == 0) ? 1f : 1.35f);
				float rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(30f * (float)i);
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.One, SpikeColor, "CalamityMod/Particles/BlastCone", squish, rotation, 1f, 0.5f, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int j = 0; j < num; j++)
			{
				Vector2 velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(MathHelper.ToRadians(105f)) * Main.rand.NextFloat(6f, 7.2f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<UrchinSpikeFugu>(), (int)((float)base.Projectile.damage * SpikeDamage), base.Projectile.knockBack * SpikeKnockback, base.Projectile.owner);
			}
		}
		SpikeTimer = 0f;
		base.Projectile.netUpdate = true;
	};

	public override void Load()
	{
		ChargeGlow = ModContent.Request<Texture2D>("CalamityMod/Particles/LargeBloom", (AssetRequestMode)2);
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 38);
		base.Projectile.ignoreWater = true;
		base.SetDefaults();
	}

	public override void SpinAI(float launchSpeed)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		SpikeTimer++;
		if (base.Projectile.soundDelay <= 0)
		{
			SoundStyle style = SoundID.Zombie35 with
			{
				Pitch = -0.4f,
				PitchVariance = 0.2f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.soundDelay = Main.rand.Next(48, 60);
		}
		if (base.Projectile.owner == Main.myPlayer && SpikeTimer > MaxSpikeTime + SpikeRate)
		{
			Vector2 velocity = base.Projectile.DirectionFrom(base.Owner.MountedCenter).SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(4.5f, 6.5f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<UrchinSpikeFugu>(), (int)((float)base.Projectile.damage * SpikeDamage), base.Projectile.knockBack * SpikeKnockback, base.Projectile.owner);
			SpikeTimer = MaxSpikeTime + (float)Main.rand.Next(5);
		}
		base.SpinAI(launchSpeed);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(20, 180);
		if (base.CurrentFlailState == FlailState.LaunchingForward)
		{
			base.StateTimer = LaunchLifespan;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (SpikeTimer > SpikeRate)
		{
			float power = Utils.GetLerpValue(0f, MaxSpikeTime, SpikeTimer, clamped: true);
			Main.EntitySpriteDraw(ChargeGlow.Value, base.Projectile.Center - Main.screenPosition, null, SpikeColor * (0.6f + 0.12f * power), 0f, ChargeGlow.Size() * 0.5f, 0.1f + 0.05f * power, (SpriteEffects)0);
		}
		return base.PreDraw(ref lightColor);
	}
}
