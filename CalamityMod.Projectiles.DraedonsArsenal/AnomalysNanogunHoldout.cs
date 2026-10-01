using System;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AnomalysNanogunHoldout : ModProjectile, ILocalizedModType, IModType
{
	public static Texture2D ScopeTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/AnomalysNanogunScope", (AssetRequestMode)1).Value;

	public const float GunLength = 64f;

	public const float MPFBAIType = 2f;

	public const int MPFBFireTimer = 59;

	public const float MPFBRecoil = (float)Math.PI * 3f / 4f;

	public const float MPFBScreenShakePower = 5f;

	public const float MPFBPushback = 15f;

	public const float MaxMPFBPropellableSpeed = 14f;

	public const int PlasmaChargeupTimer = 40;

	public const int PlasmaCooldownTimer = 24;

	public const int PlasmaShotCooldown = 15;

	public const int PlasmaShotCount = 5;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float AITimer => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public Vector2 MouseDistance
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Calamity().mouseWorld - Owner.MountedCenter;
		}
	}

	public ref float ScopeRotation => ref base.Projectile.localAI[0];

	public ref float ScopeScale => ref base.Projectile.localAI[1];

	public ref float TargetRecoil => ref base.Projectile.localAI[0];

	public bool UseMPFBAI => base.Projectile.ai[0] == 2f;

	public static int PlasmaFireTimer => 139;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 1);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Owner.Calamity().mouseWorldListener = true;
		base.Projectile.rotation = (Owner.Calamity().mouseWorld - Owner.MountedCenter).ToRotation();
		base.Projectile.Center = Owner.MountedCenter;
		Owner.heldProj = base.Projectile.whoAmI;
		if (UseMPFBAI)
		{
			UpdateMPFB();
		}
		else
		{
			UpdatePlasmaBeam();
		}
		AITimer++;
	}

	public void UpdateMPFB()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != 0f)
		{
			Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		}
		else
		{
			Owner.ChangeDir(1);
		}
		if (AITimer == 0f)
		{
			SoundEngine.PlaySound(in TheAnomalysNanogun.MPFBShotSFX, base.Projectile.Center);
			for (int i = 0; i < 3; i++)
			{
				int proj = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(Owner, Owner.HeldItem, -1), base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 64f, base.Projectile.velocity * (1.5f - (float)i * 0.24f), ModContent.ProjectileType<AnomalysNanogunMPFBDevastator>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				if (Main.projectile.IndexInRange(proj))
				{
					Main.projectile[proj].frame = Main.rand.Next(0, 4);
					Main.projectile[proj].frameCounter = Main.rand.Next(0, 3);
				}
			}
			Owner.SetScreenshake(5f);
			float playerSpeed = ((Vector2)(ref Owner.velocity)).Length();
			Vector2 pushback = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * -15f;
			Vector2 newPlayerVelocity = Owner.velocity + pushback;
			float newPlayerSpeed = ((Vector2)(ref newPlayerVelocity)).Length();
			if (playerSpeed < 14f || newPlayerSpeed < playerSpeed)
			{
				Owner.velocity = newPlayerVelocity;
			}
			else
			{
				Owner.velocity = newPlayerVelocity.SafeNormalize(Vector2.UnitX) * playerSpeed;
			}
			Vector2 direction = MouseDistance.SafeNormalize(Vector2.UnitX);
			float recoil = direction.RotatedBy(-2.042035f * (float)Owner.direction).ToRotation();
			TargetRecoil = recoil;
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(Owner.MountedCenter + direction * 64f, Vector2.Zero, Color.DeepSkyBlue, new Vector2(0.5f, 1f), direction.ToRotation(), 0.2f, 1f, 30));
		}
		else
		{
			if (AITimer < 22f)
			{
				float newRotation = UpdateAimPostShotRecoil(TargetRecoil.ToRotationVector2());
				Owner.itemRotation = newRotation;
			}
			else if (AITimer == 22f)
			{
				SoundEngine.PlaySound(in SoundID.Item149, base.Projectile.Center);
			}
			else if (59f - AITimer < 30f)
			{
				float newRotation2 = UpdateAimPostShotRecoil((TargetRecoil + 2.042035f * (float)Owner.direction).ToRotationVector2());
				Owner.itemRotation = newRotation2;
			}
			if (AITimer >= 59f)
			{
				base.Projectile.Kill();
			}
		}
	}

	private float UpdateAimPostShotRecoil(Vector2 target)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(target * (float)Owner.direction, Owner.itemRotation.ToRotationVector2(), 0.795f).ToRotation();
	}

	public void UpdatePlasmaBeam()
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		if (AITimer == 0f)
		{
			SoundEngine.PlaySound(in TheAnomalysNanogun.PlasmaChargeSFX, base.Projectile.Center);
			ScopeRotation = Main.rand.NextFloat((float)Math.PI / 2f, (float)Math.PI * 3f / 4f) * (float)Main.rand.NextBool().ToDirectionInt();
			ScopeScale = 2.5f;
		}
		Owner.itemRotation = base.Projectile.rotation;
		int dir = Math.Sign(base.Projectile.rotation.ToRotationVector2().X);
		Owner.ChangeDir((dir == 0) ? 1 : dir);
		Owner.itemRotation = ((Owner.Calamity().mouseWorld - Owner.MountedCenter) * (float)Owner.direction).ToRotation();
		if (AITimer - 40f > 0f)
		{
			if (AITimer % 15f == 0f && (AITimer - 40f) / 15f <= 5f)
			{
				Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(Owner, Owner.HeldItem, -1), base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 64f, base.Projectile.rotation.ToRotationVector2(), ModContent.ProjectileType<AnomalysNanogunPlasmaBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				SoundEngine.PlaySound(in TheAnomalysNanogun.PlasmaShotSFX, base.Projectile.Center);
			}
		}
		else
		{
			ScopeRotation = MathHelper.Lerp(ScopeRotation, 0f, 0.08f);
			ScopeScale -= 0.0625f;
		}
		if (AITimer >= (float)PlasmaFireTimer)
		{
			base.Projectile.Kill();
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (!UseMPFBAI && AITimer - 40f <= 0f)
		{
			Vector2 drawCenter = base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 64f - Main.screenPosition;
			Main.EntitySpriteDraw(ScopeTexture, drawCenter, null, Color.White, ScopeRotation, ScopeTexture.Bounds.Size() / 2f, ScopeScale, (SpriteEffects)0);
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		return false;
	}
}
