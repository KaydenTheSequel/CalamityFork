using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TauCannonBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	private const string LaserTexturePath = "CalamityMod/ExtraTextures/Lasers/TauCannonBeam";

	public Color color1;

	public Color color2;

	private SlotId BigBeamSoundSlot;

	public int time;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override float Lifetime => IsStage3Laser ? 90 : 25;

	public override float MaxScale
	{
		get
		{
			if (!IsStage3Laser)
			{
				return 1f;
			}
			return 4f;
		}
	}

	public override float MaxLaserLength => 2000f;

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/TauCannonBeamStart", (AssetRequestMode)2).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/TauCannonBeamMiddle", (AssetRequestMode)2).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/TauCannonBeamEnd", (AssetRequestMode)2).Value;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private Projectile Holdout => Main.projectile[(int)base.Projectile.ai[1]];

	private bool IsStage3Laser => base.Projectile.ai[2] == 1f;

	private Player Owner { get; set; }

	public override void SetDefaults()
	{
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 90);
		if (base.Projectile.numHits > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * (IsStage3Laser ? 1f : 0.75f));
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.LaserLength, IsStage3Laser ? 70f : 10f, ref _);
	}

	public override void UpdateLaserMotion()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Holdout.velocity;
		base.Projectile.rotation = Holdout.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void AttachToSomething()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if (Owner != null && Owner.active && !Owner.dead && !Owner.CCed && !Owner.noItems && Owner.ownedProjectileCounts[ModContent.ProjectileType<TauCannonHoldout>()] != 0 && Holdout.ModProjectile<TauCannonHoldout>() != null)
		{
			base.Projectile.Center = Holdout.ModProjectile<TauCannonHoldout>().GunTipPosition + Holdout.velocity * (IsStage3Laser ? 20f : 5f);
		}
	}

	public override void ExtraBehavior()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null)
		{
			Player player = (Owner = Main.player[base.Projectile.owner]);
		}
		base.Projectile.localNPCHitCooldown = (IsStage3Laser ? 4 : (-1));
		float Lifet = Lifetime - (float)time;
		if (!SoundEngine.TryGetActiveSound(BigBeamSoundSlot, out ActiveSound sound))
		{
			SoundStyle style = TauCannonHoldout.BigBeamSound with
			{
				Volume = 0.01f,
				IsLooped = true
			};
			BigBeamSoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		else
		{
			sound.Position = base.Projectile.Center;
			sound.Volume = Utils.GetLerpValue(0f, 20f, Lifet, clamped: true) * 100f;
			sound.Pitch = (1f - 1f * Utils.GetLerpValue(0f, 20f, Lifet, clamped: true)) * -1f;
		}
		Vector2 effectsPosition = Vector2.Lerp(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.LaserLength, Main.rand.NextFloat());
		Vector2 position = effectsPosition + Main.rand.NextVector2Circular(40f * (IsStage3Laser ? 1.2f : 0.8f), 40f * (IsStage3Laser ? 1.2f : 0.8f));
		Vector2? velocity = base.Projectile.velocity * Main.rand.NextFloat(5f, 40f);
		float scale = Main.rand.NextFloat(0.8f, 1.1f);
		Dust dust = Dust.NewDustPerfect(position, 278, velocity, 0, default(Color), scale);
		dust.noGravity = true;
		dust.color = (Main.rand.NextBool(3) ? color2 : color1);
		for (int i = 0; i < 2; i++)
		{
			GeneralParticleHandler.SpawnParticle(new LineParticle(effectsPosition + Main.rand.NextVector2Circular(50f * (IsStage3Laser ? 1.2f : 0.8f), 50f * (IsStage3Laser ? 1.2f : 0.8f)), base.Projectile.velocity * Main.rand.NextFloat(20f, 90f), affectedByGravity: false, 18, 0.8f, Main.rand.NextBool(3) ? color2 : color1));
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(BigBeamSoundSlot, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	public TauCannonBeam()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		color1 = Color.MediumTurquoise;
		color2 = Color.Coral;
		base._002Ector();
	}
}
