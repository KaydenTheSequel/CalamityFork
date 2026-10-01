using System;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PrismaticBreakerHoldout : BaseGunHoldoutProjectile, ILocalizedModType, IModType
{
	public const float LaserChargeTime = 300f;

	public const float LaserAimLag = 0.94f;

	public const float LaserDamageMult = 2f;

	public const float LaserLifetime = 360f;

	public float StarTimer;

	public float StarFrequency = 47f;

	public override int AssociatedItemID => ModContent.ItemType<PrismaticBreaker>();

	public override float MaxOffsetLengthFromArm => 40f;

	public ref float Timer => ref base.Projectile.ai[0];

	public override void HoldoutAI()
	{
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.CantUseHoldout() && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<PrismaticMagicCircle>()))
		{
			base.Projectile.Kill();
		}
		if (Timer > 660f)
		{
			base.Projectile.Kill();
		}
		Timer++;
		if (Timer <= 300f)
		{
			StarTimer++;
			if (StarTimer >= StarFrequency && Main.myPlayer == base.Projectile.owner)
			{
				StarTimer = 0f;
				SoundStyle style = SoundID.Item43 with
				{
					Volume = 0.6f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i < 3; i++)
				{
					float clampedChargeTime = MathHelper.Clamp(Timer / 300f, 0f, 1f);
					float starOffset = MathHelper.Lerp((float)Math.PI / 6f, 0f, clampedChargeTime);
					Vector2 velocity = Vector2.Normalize(base.Projectile.velocity).RotatedByRandom(starOffset) * 17f;
					Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, velocity, ModContent.ProjectileType<PrismaticWave>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, Main.rand.Next(12)).scale = MathHelper.Lerp(0.75f, 1f, clampedChargeTime);
				}
				StarFrequency -= 4f;
			}
		}
		if (Timer == 200f)
		{
			SoundEngine.PlaySound(in CrystylCrusher.ChargeSound, base.Projectile.Center, (ActiveSound _) => new ProjectileAudioTracker(base.Projectile).IsActiveAndInGame());
		}
		if (Timer == 300f && Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity, ModContent.ProjectileType<PrismaticMagicCircle>(), (int)((float)base.Projectile.damage * 2f), base.Projectile.knockBack, base.Projectile.owner);
		}
	}

	public override void ManageHoldout()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 storedVelocity = base.Projectile.velocity;
		base.ManageHoldout();
		if (base.Owner.ownedProjectileCounts[ModContent.ProjectileType<PrismaticMagicCircle>()] > 0)
		{
			Vector2 aimVector = (Main.MouseWorld - base.Owner.RotatedRelativePoint(base.Owner.MountedCenter, reverseRotation: true)).SafeNormalize(Vector2.UnitY);
			aimVector = Vector2.Normalize(Vector2.Lerp(aimVector, Vector2.Normalize(storedVelocity), 0.94f));
			if (aimVector != storedVelocity)
			{
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity = aimVector;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(position: base.Projectile.Center - Main.screenPosition, rotation: base.Projectile.rotation + (float)Math.PI / 4f * (float)base.Projectile.spriteDirection + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) - ((base.Owner.gravDir == -1f) ? ((float)Math.PI / 2f * (float)base.Owner.direction) : 0f), origin: value.Size() * 0.5f, effects: (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f), texture: value, sourceRectangle: null, color: base.Projectile.GetAlpha(lightColor), scale: base.Projectile.scale * base.Owner.gravDir);
		return false;
	}
}
