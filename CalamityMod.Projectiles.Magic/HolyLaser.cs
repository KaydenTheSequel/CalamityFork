using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class HolyLaser : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	private const string LaserTexturePath = "CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRay";

	public Color color1;

	public Color color2;

	public float extraRot;

	public Vector2 displace;

	public int time;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override float Lifetime => isBigLaser ? 60 : 30;

	public override float MaxScale
	{
		get
		{
			if (!isBigLaser)
			{
				return 0.8f;
			}
			return 1.5f;
		}
	}

	public override float MaxLaserLength => 1500f;

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayStart", (AssetRequestMode)2).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayMid", (AssetRequestMode)2).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ProvidenceHolyRayEnd", (AssetRequestMode)2).Value;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			Color white = Color.White;
			((Color)(ref white)).A = 0;
			return white;
		}
	}

	public override Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Transparent;
		}
	}

	private Projectile Holdout => Main.projectile[(int)base.Projectile.ai[1]];

	private bool isBigLaser => base.Projectile.ai[2] == 1f;

	private Player Owner { get; set; }

	public override void SetDefaults()
	{
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), isBigLaser ? 180 : 90);
		if (base.Projectile.numHits > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * (isBigLaser ? 0.55f : 0.6f));
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
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.LaserLength, isBigLaser ? 50f : 20f, ref _);
	}

	public override void UpdateLaserMotion()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		float dir = base.Projectile.ai[0];
		if (time == 0)
		{
			if (!isBigLaser)
			{
				extraRot = dir;
				base.Projectile.ai[0] = 0f;
			}
			else
			{
				extraRot = 1.2f * dir;
			}
		}
		base.Projectile.velocity = Holdout.velocity.RotatedBy(extraRot);
		if (!isBigLaser)
		{
			base.Projectile.rotation = Holdout.velocity.ToRotation() - (float)Math.PI / 2f + extraRot;
			extraRot *= 0.85f;
		}
		else
		{
			base.Projectile.rotation = Holdout.velocity.ToRotation() - (float)Math.PI / 2f + extraRot;
			extraRot -= 0.05f * dir;
		}
	}

	public override void AttachToSomething()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (Owner != null && Owner.active && !Owner.dead && !Owner.CCed && !Owner.noItems && Owner.ownedProjectileCounts[ModContent.ProjectileType<PurgeGuzzlerHoldout>()] != 0)
		{
			base.Projectile.Center = Holdout.ModProjectile<PurgeGuzzlerHoldout>().GunTipPosition;
		}
	}

	public override void ExtraBehavior()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null)
		{
			Player player = (Owner = Main.player[base.Projectile.owner]);
		}
		Vector2 randomLineEffectPosition = Vector2.Lerp(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.LaserLength, Main.rand.NextFloat()) + Main.rand.NextVector2Circular(7f * (isBigLaser ? 1.2f : 0.8f), 7f * (isBigLaser ? 1.2f : 0.8f));
		if (Main.rand.NextBool() || isBigLaser)
		{
			Vector2 position = randomLineEffectPosition;
			Vector2? velocity = base.Projectile.velocity * Main.rand.NextFloat(5f, 40f);
			float scale = Main.rand.NextFloat(0.8f, 1.1f);
			Dust dust = Dust.NewDustPerfect(position, 278, velocity, 0, default(Color), scale);
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool(3) ? color2 : color1);
		}
		for (int i = 0; i < (isBigLaser ? 4 : 2); i++)
		{
			randomLineEffectPosition = Vector2.Lerp(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.LaserLength, Main.rand.NextFloat()) + Main.rand.NextVector2Circular(7f, 7f);
			if (i % 2 == 0)
			{
				Vector2 position2 = randomLineEffectPosition;
				int type = ModContent.DustType<LightDust>();
				Vector2? velocity2 = base.Projectile.velocity * Main.rand.NextFloat(5f, 40f);
				float scale = Main.rand.NextFloat(0.8f, 1.1f) * (float)((!isBigLaser) ? 1 : 2);
				Dust dust2 = Dust.NewDustPerfect(position2, type, velocity2, 0, default(Color), scale);
				dust2.noGravity = true;
				dust2.color = (Main.rand.NextBool(3) ? color2 : color1);
				dust2.noLightEmittence = true;
			}
			else
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(randomLineEffectPosition, base.Projectile.velocity * Main.rand.NextFloat(1f, 10f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 17, Main.rand.NextFloat(1.15f, 1.3f), Color.Lerp(color1, color2, Main.rand.NextFloat()), new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.3f, 0.4f)));
			}
		}
		time++;
	}

	public HolyLaser()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		color1 = Color.Goldenrod;
		color2 = Color.Orange;
		displace = Vector2.Zero;
		base._002Ector();
	}
}
