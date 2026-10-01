using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HeavenlyGaleProj : BaseIdleHoldoutProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public bool OwnerCanShoot
	{
		get
		{
			if (base.Owner.HasAmmo(base.Owner.HeldItem) && !base.Owner.noItems)
			{
				return !base.Owner.CCed;
			}
			return false;
		}
	}

	public float StringReelbackInterpolant
	{
		get
		{
			int duration = base.Owner.HeldItem.useAnimation;
			float time = (float)duration - ShootDelay;
			float lerpValue = Utils.GetLerpValue(8f, 0f, time, clamped: true);
			float secondHalf = Utils.GetLerpValue(8f, (float)duration * 0.6f, time, clamped: true);
			return lerpValue + secondHalf;
		}
	}

	public float StringReelbackDistance => (float)base.Projectile.width * StringReelbackInterpolant * 0.3f;

	public Vector2 topStringOffset
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(-40f, -56f);
		}
	}

	public Vector2 bottomStringOffset
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(-40f, 46f);
		}
	}

	public float StringHalfHeight
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			return (Math.Abs(topStringOffset.Y) + Math.Abs(bottomStringOffset.Y)) * 0.5f;
		}
	}

	public float ChargeupInterpolant => Utils.GetLerpValue(32f, 300f, ChargeTimer, clamped: true);

	public ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	public ref float ChargeTimer => ref base.Projectile.ai[1];

	public ref float ShootDelay => ref base.Projectile.localAI[0];

	public override int AssociatedItemID => ModContent.ItemType<HeavenlyGale>();

	public override int IntendedProjectileType => ModContent.ProjectileType<HeavenlyGaleProj>();

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 176);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.MaxUpdates = 2;
	}

	public override void SafeAI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 armPosition = base.Owner.RotatedRelativePoint(base.Owner.MountedCenter, reverseRotation: true);
		Vector2 tipPosition = armPosition + base.Projectile.velocity * (float)base.Projectile.width * 0.45f;
		bool activatingShoot = ShootDelay <= 0f && Main.mouseLeft && !Main.mapFullscreen && !base.Owner.mouseInterface;
		if ((Main.myPlayer == base.Projectile.owner && OwnerCanShoot) & activatingShoot)
		{
			SoundEngine.PlaySound(in HeavenlyGale.FireSound, base.Projectile.Center);
			ShootDelay = base.Owner.HeldItem.useAnimation;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.damage = ((base.Owner.HeldItem != null) ? base.Owner.GetWeaponDamage(base.Owner.HeldItem) : 0);
		UpdateProjectileHeldVariables(armPosition);
		ManipulatePlayerVariables();
		if (ShootDelay > 0f && base.Projectile.FinalExtraUpdate())
		{
			float shootCompletionRatio = 1f - ShootDelay / ((float)base.Owner.HeldItem.useAnimation - 1f);
			float bowAngularOffset = (float)Math.Sin((float)Math.PI * 2f * shootCompletionRatio) * 0.4f;
			float damageFactor = Utils.Remap(ChargeTimer, 0f, 300f, 1f, 3.5f);
			if (ShootDelay % 4f == 0f)
			{
				Vector2 arrowDirection = base.Projectile.velocity.RotatedBy(bowAngularOffset);
				Color energyBoltColor = CalamityUtils.MulticolorLerp(shootCompletionRatio, CalamityUtils.ExoPalette);
				energyBoltColor = Color.Lerp(energyBoltColor, Color.White, 0.35f);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(tipPosition + arrowDirection * 16f, arrowDirection * 4.5f, 0.85f, energyBoltColor, 40, 1f, 5.4f, 4f, 0.08f));
				tipPosition = armPosition + arrowDirection * (float)base.Projectile.width * 0.45f;
				if (Main.myPlayer == base.Projectile.owner && base.Owner.HasAmmo(base.Owner.HeldItem))
				{
					Item heldItem = base.Owner.HeldItem;
					base.Owner.PickAmmo(heldItem, out var projectileType, out var shootSpeed, out var damage, out var knockback, out var _);
					damage = (int)((float)damage * damageFactor);
					projectileType = ModContent.ProjectileType<ExoCrystalArrow>();
					bool createLightning = ChargeTimer / 300f >= 0.8f;
					Vector2 arrowVelocity = arrowDirection * shootSpeed;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, arrowVelocity, projectileType, damage, knockback, base.Projectile.owner, createLightning.ToInt());
				}
			}
			ShootDelay--;
			if (ShootDelay <= 0f)
			{
				ChargeTimer = 0f;
			}
		}
		Color energyColor = Color.Orange;
		Vector2 verticalOffset = Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 8f;
		if (Math.Cos(base.Projectile.rotation) < 0.0)
		{
			verticalOffset *= -1f;
		}
		if (Main.rand.NextBool(4))
		{
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(tipPosition + verticalOffset, -Vector2.UnitY.RotatedByRandom(0.38999998569488525) * Main.rand.NextFloat(0.4f, 1.6f), 0.28f, energyColor, 25));
		}
		DelegateMethods.v3_1 = ((Color)(ref energyColor)).ToVector3();
		Utils.PlotTileLine(tipPosition - verticalOffset, tipPosition + verticalOffset, 10f, DelegateMethods.CastLightOpen);
		Lighting.AddLight(tipPosition, ((Color)(ref energyColor)).ToVector3());
		if (ShootDelay <= 0f)
		{
			ChargeTimer++;
		}
		if (ChargeTimer == 300f)
		{
			SoundStyle style = SoundID.Item158 with
			{
				Volume = 1.6f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i < 75; i++)
			{
				float num = (float)Math.PI * 2f * (float)i / 75f;
				float unitOffsetX = (float)Math.Pow(Math.Cos(num), 3.0);
				float unitOffsetY = (float)Math.Pow(Math.Sin(num), 3.0);
				Vector2 puffDustVelocity = new Vector2(unitOffsetX, unitOffsetY) * 5f;
				Dust dust = Dust.NewDustPerfect(tipPosition, 267, puffDustVelocity);
				dust.scale = 1.8f;
				dust.fadeIn = 0.5f;
				dust.color = CalamityUtils.MulticolorLerp((float)i / 75f, CalamityUtils.ExoPalette);
				dust.noGravity = true;
			}
			ChargeTimer++;
		}
	}

	public void UpdateProjectileHeldVariables(Vector2 armPosition)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			float aimInterpolant = Utils.GetLerpValue(10f, 40f, base.Projectile.Distance(Main.MouseWorld), clamped: true);
			Vector2 oldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(Main.MouseWorld), aimInterpolant);
			if (base.Projectile.velocity != oldVelocity)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
		base.Projectile.position = armPosition - base.Projectile.Size * 0.5f + base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 44f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
	}

	public void ManipulatePlayerVariables()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.ChangeDir(base.Projectile.direction);
		base.Owner.heldProj = base.Projectile.whoAmI;
		float frontArmRotation = (float)Math.Atan(StringHalfHeight / MathHelper.Max(StringReelbackDistance, 0.001f) * 0.5f);
		frontArmRotation = ((base.Owner.direction != -1) ? ((float)Math.PI / 2f - frontArmRotation) : (frontArmRotation + (float)Math.PI / 4f));
		frontArmRotation += base.Projectile.rotation + (float)Math.PI + (float)base.Owner.direction * ((float)Math.PI / 2f) + 0.12f;
		base.Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, frontArmRotation);
		base.Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Texture2D textureGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/HeavenlyGaleProjGlow", (AssetRequestMode)2).Value;
		Vector2 origin = texture.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 center = base.Projectile.Center;
		Vector2 unitX = Vector2.UnitX;
		double radians = base.Projectile.rotation + topStringOffset.ToRotation();
		Vector2 center2 = default(Vector2);
		Vector2 val = unitX.RotatedBy(radians, center2);
		center2 = topStringOffset;
		Vector2 topOfBow = center + val * ((Vector2)(ref center2)).Length();
		Vector2 center3 = base.Projectile.Center;
		Vector2 unitX2 = Vector2.UnitX;
		double radians2 = base.Projectile.rotation + bottomStringOffset.ToRotation();
		center2 = default(Vector2);
		Vector2 val2 = unitX2.RotatedBy(radians2, center2);
		center2 = bottomStringOffset;
		Vector2 bottomOfBow = center3 + val2 * ((Vector2)(ref center2)).Length();
		Vector2 endOfString = base.Projectile.Center - base.Projectile.rotation.ToRotationVector2() * (StringReelbackDistance + (1f - StringReelbackInterpolant) * 25f);
		float chargeOffset = ChargeupInterpolant * base.Projectile.scale * 3f;
		Color chargeColor = Color.Lerp(Color.Lime, Color.Cyan, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 7.1f) * 0.5f + 0.5f) * ChargeupInterpolant * 0.6f;
		((Color)(ref chargeColor)).A = 0;
		float rotation = base.Projectile.rotation;
		SpriteEffects direction = (SpriteEffects)0;
		if (Math.Cos(rotation) < 0.0)
		{
			direction = (SpriteEffects)1;
			rotation += (float)Math.PI;
		}
		Color stringColor = default(Color);
		((Color)(ref stringColor))._002Ector(105, 239, 145);
		Main.spriteBatch.DrawLineBetter(topOfBow, endOfString, stringColor, 2f);
		Main.spriteBatch.DrawLineBetter(bottomOfBow, endOfString, stringColor, 2f);
		for (int i = 0; i < 8; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 8f).ToRotationVector2() * chargeOffset;
			Main.spriteBatch.Draw(texture, drawPosition + drawOffset, (Rectangle?)null, chargeColor, rotation, origin, base.Projectile.scale, direction, 0f);
		}
		Main.spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale, direction, 0f);
		Main.spriteBatch.Draw(textureGlow, drawPosition, (Rectangle?)null, Color.White, rotation, origin, base.Projectile.scale, direction, 0f);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
