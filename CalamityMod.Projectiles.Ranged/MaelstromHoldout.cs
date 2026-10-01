using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MaelstromHoldout : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<TheMaelstrom>();

	private Player Owner => Main.player[base.Projectile.owner];

	private ref float CurrentChargingFrames => ref base.Projectile.ai[0];

	private ref float FramesToLoadNextArrow => ref base.Projectile.localAI[0];

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/TheMaelstrom";

	public override void SetDefaults()
	{
		base.Projectile.width = 78;
		base.Projectile.height = 137;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
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
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		Vector2 shootPosition = armPosition + base.Projectile.velocity * (float)base.Projectile.width * 0.5f;
		if (Owner.CantUseHoldout() || !Owner.HasAmmo(Owner.HeldItem))
		{
			base.Projectile.Kill();
			return;
		}
		if (FramesToLoadNextArrow == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
			FramesToLoadNextArrow = Owner.HeldItem.useAnimation;
		}
		CurrentChargingFrames++;
		if (CurrentChargingFrames % FramesToLoadNextArrow == FramesToLoadNextArrow - 1f)
		{
			ShootProjectiles(shootPosition);
		}
		UpdateProjectileHeldVariables(armPosition);
		ManipulatePlayerVariables();
	}

	public void ShootProjectiles(Vector2 shootPosition)
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 16; i++)
			{
				int sparkLifetime = Main.rand.Next(22, 36);
				float sparkScale = Main.rand.NextFloat(1f, 1.3f);
				Color sparkColor = Color.Lerp(Color.Cyan, Color.AliceBlue, Main.rand.NextFloat(0.35f));
				Vector2 sparkVelocity = ((float)Math.PI * 2f * (float)i / 16f + Main.rand.NextFloat(0.09f)).ToRotationVector2() * Main.rand.NextFloat(6f, 14f);
				sparkVelocity.Y -= Owner.gravDir * 4f;
				GeneralParticleHandler.SpawnParticle(new SparkParticle(shootPosition, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
			}
		}
		SoundEngine.PlaySound(in SoundID.Item66, base.Projectile.Center);
		SoundEngine.PlaySound(in SoundID.Item96, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			Item heldItem = Owner.HeldItem;
			int arrowDamage = ((heldItem != null) ? Owner.GetWeaponDamage(heldItem) : 0);
			float shootSpeed = heldItem.shootSpeed;
			float knockback = heldItem.knockBack;
			int projectileType = 0;
			Owner.PickAmmo(heldItem, out projectileType, out shootSpeed, out arrowDamage, out knockback, out var _);
			projectileType = ModContent.ProjectileType<TheMaelstromShark>();
			knockback = Owner.GetWeaponKnockback(heldItem, knockback);
			Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * shootSpeed;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shootPosition, shootVelocity, projectileType, arrowDamage, knockback, base.Projectile.owner);
		}
	}

	private void UpdateProjectileHeldVariables(Vector2 armPosition)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
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
			float aimInterpolant = Utils.GetLerpValue(5f, 25f, base.Projectile.Distance(Main.MouseWorld), clamped: true);
			Vector2 oldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(Main.MouseWorld), aimInterpolant);
			if (base.Projectile.velocity != oldVelocity)
			{
				base.Projectile.ForceNetUpdate();
			}
		}
		base.Projectile.position = armPosition - base.Projectile.Size * 0.5f + base.Projectile.velocity * 24f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
	}

	private void ManipulatePlayerVariables()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
