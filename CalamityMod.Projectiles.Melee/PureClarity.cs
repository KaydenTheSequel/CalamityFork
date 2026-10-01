using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PureClarity : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public Vector2 mousePos;

	public Vector2 aimPos;

	public bool doSwing = true;

	public bool postSwing;

	public int useAnimation;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override int AssignedItemID => ModContent.ItemType<BrokenBiomeBlade>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/BrokenBiomeBlade";

	public override float HitboxOutset => 50f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(48f, 48f);
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(-5f, 40f);
		}
	}

	public ref float SwingDir => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.scale = 1.25f;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void WhenSpawned()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		SwingDir = 1f;
		mousePos = Owner.Calamity().mouseWorld;
		aimPos = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
		useAnimation = Owner.itemAnimationMax;
		Owner.direction = ((!(mousePos.X < Owner.Center.X)) ? 1 : (-1));
		FlipAsSword = Owner.direction == -1;
	}

	public override void UseStyle()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		if (CanHit || postSwing)
		{
			mousePos = Owner.Center - aimPos;
		}
		else
		{
			mousePos = Owner.Calamity().mouseWorld;
		}
		if (!doSwing)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				base.Projectile.localNPCImmunity[i] = 0;
			}
			mousePos = Owner.Calamity().mouseWorld;
			aimPos = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			CanHit = false;
			Owner.direction = ((!(mousePos.X < Owner.Center.X)) ? 1 : (-1));
			FlipAsSword = Owner.direction == -1;
			doSwing = true;
		}
		else
		{
			if (!CanHit && !postSwing)
			{
				Owner.direction = ((!(mousePos.X < Owner.Center.X)) ? 1 : (-1));
			}
			else
			{
				Owner.direction = ((!((Owner.Center - aimPos).X < Owner.Center.X)) ? 1 : (-1));
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(65f), 0.1f);
			if (AnimationProgress < (float)(useAnimation / 3))
			{
				aimPos = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				CanHit = false;
				postSwing = false;
				if (AnimationProgress == 0f)
				{
					doSwing = false;
					SwingDir = 0f - SwingDir;
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * SwingDir * (float)Owner.direction), 0.2f);
			}
			else
			{
				float swingTime = AnimationProgress - (float)(useAnimation / 3);
				float swingTimeMax = useAnimation - useAnimation / 3;
				if (swingTime > (float)(int)(swingTimeMax * 0.2f) && swingTime < (float)(int)(swingTimeMax * 0.85f))
				{
					CanHit = true;
					Vector2 particleVel = Utils.RotatedBy(new Vector2(0f, 10f * (0f - SwingDir) * (float)Owner.direction), (double)(base.FinalRotation - (float)Math.PI / 4f), default(Vector2));
					Vector2 particlePos = Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(0, 80), 0f), (double)(base.FinalRotation - (float)Math.PI / 4f), default(Vector2));
					Color particleColor = (Owner.HeldItem.ModItem as BrokenBiomeBlade).mainAttunement.tooltipColor;
					if (Main.rand.NextBool())
					{
						GeneralParticleHandler.SpawnParticle(new GenericBloom(particlePos, particleVel, particleColor, 0.08f, 20));
					}
					else
					{
						GeneralParticleHandler.SpawnParticle(new GenericSparkle(particlePos, particleVel, particleColor, particleColor, 0.55f, 20));
					}
				}
				else
				{
					CanHit = false;
				}
				if (swingTime == (float)(int)(swingTimeMax * 0.4f))
				{
					SoundStyle style = SoundID.Item43 with
					{
						Volume = 0.65f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					Vector2 projVel = (Owner.Calamity().mouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX) * 14.5f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, projVel, ModContent.ProjectileType<PurityProjection>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI);
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(MathHelper.Lerp(150f * SwingDir * (float)Owner.direction, 120f * (0f - SwingDir) * (float)Owner.direction, CalamityUtils.ExpInOutEasing(swingTime / swingTimeMax, 1))), 0.2f);
				if (swingTime >= swingTimeMax)
				{
					doSwing = false;
				}
				if (swingTime < (float)(int)(swingTimeMax * 0.7f))
				{
					postSwing = true;
				}
			}
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override void ResetStyle()
	{
	}
}
