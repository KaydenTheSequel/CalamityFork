using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class UltimaBowProjectile : ModProjectile
{
	public const float PositioningOffset = 35f;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Ultima>();

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/Ultima";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 82;
		base.Projectile.height = 114;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.direction = (Math.Cos(base.Projectile.velocity.ToRotation()) > 0.0).ToDirectionInt();
		AttemptToFireProjectiles(player);
		AttachToPlayer(player);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)(base.Projectile.spriteDirection == -1).ToInt() * (float)Math.PI;
		Time++;
	}

	public void AttemptToFireProjectiles(Player player)
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		if (player.CantUseHoldout() || !player.HasAmmo(player.HeldItem))
		{
			base.Projectile.Kill();
		}
		else if (base.Projectile.owner == Main.myPlayer && Time % (float)player.HeldItem.useTime == 0f)
		{
			int type = 1;
			float shotSpeed = player.HeldItem.shootSpeed;
			int damage = player.GetWeaponDamage(player.HeldItem);
			float knockBack = player.HeldItem.knockBack;
			player.PickAmmo(player.HeldItem, out type, out shotSpeed, out damage, out knockBack, out var _);
			if (player.HeldItem.UseSound.HasValue)
			{
				SoundEngine.PlaySound(player.HeldItem.UseSound.GetValueOrDefault(), base.Projectile.Center);
			}
			type = ModContent.ProjectileType<UltimaBolt>();
			float shootLaserChance = Utils.GetLerpValue(147f, 420f, Time, clamped: true);
			Vector2 shotPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
			shotPosition += base.Projectile.velocity.ToRotation().ToRotationVector2().RotatedByRandom(MathHelper.ToRadians(40f))
				.RotatedBy(-0.25f * (float)base.Projectile.spriteDirection) * 42f;
			base.Projectile.velocity = player.SafeDirectionTo(Main.MouseWorld);
			Vector2 shotVelocity = base.Projectile.velocity * shotSpeed;
			if (Main.rand.NextFloat() <= shootLaserChance)
			{
				type = ModContent.ProjectileType<UltimaRay>();
				shotVelocity = shotVelocity.RotatedByRandom(0.029999999329447746);
			}
			if (Time >= 294f && Main.rand.NextBool(6))
			{
				float offsetAngle = Main.rand.NextFloat(0.2f, 0.5f) * (float)Main.rand.NextBool().ToDirectionInt();
				Vector2 sparkVelocity = base.Projectile.SafeDirectionTo(Main.MouseWorld, Vector2.UnitY).RotatedByRandom(0.5) * 13f;
				sparkVelocity = sparkVelocity.RotatedBy(offsetAngle);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shotPosition, sparkVelocity, ModContent.ProjectileType<UltimaSpark>(), damage / 3, knockBack, base.Projectile.owner);
			}
			knockBack = player.GetWeaponKnockback(player.HeldItem, knockBack);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), shotPosition, shotVelocity, type, damage, knockBack, base.Projectile.owner);
			base.Projectile.netUpdate = true;
		}
	}

	public void AttachToPlayer(Player player)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) + base.Projectile.velocity.ToRotation().ToRotationVector2() * 35f;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = (player.itemAnimation = 2);
		player.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
