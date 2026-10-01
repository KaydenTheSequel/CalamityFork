using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RainbowPartyCannonProjectile : ModProjectile
{
	public const float ChargeDelay = 60f;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<RainbowPartyCannon>();

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 92;
		base.Projectile.height = 66;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		Item heldItem = Owner.HeldItem;
		base.Projectile.damage = ((heldItem != null) ? Owner.GetWeaponDamage(heldItem) : 0);
		UpdatePlayerVisuals(Owner.Center);
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
			return;
		}
		if (Time > 60f && Time % (float)heldItem.useTime == 0f)
		{
			ConsumeManaAndFireProjectile(heldItem);
		}
		else if (Time <= 60f && !Main.dedServ && Main.rand.NextBool(3))
		{
			Vector2 velocityDirection = base.Projectile.velocity.RotatedByRandom(0.10000000149011612);
			Vector2 shotOffset = velocityDirection * base.Projectile.Size * 0.5f;
			Dust.NewDustPerfect(base.Projectile.Center + shotOffset, Main.rand.Next(139, 143), velocityDirection * Main.rand.NextFloat(5f, 9f));
		}
		Time++;
		base.Projectile.timeLeft = 2;
	}

	public void ConsumeManaAndFireProjectile(Item heldItem)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.whoAmI != Main.myPlayer)
		{
			return;
		}
		if (!Owner.CheckMana(heldItem.mana, pay: true))
		{
			base.Projectile.Kill();
			return;
		}
		if (heldItem.UseSound.HasValue)
		{
			SoundEngine.PlaySound(heldItem.UseSound.GetValueOrDefault(), base.Projectile.Center);
		}
		Vector2 spawnPosition = base.Projectile.Center + base.Projectile.velocity * 150f;
		if (!Collision.CanHitLine(Owner.MountedCenter, 16, 16, spawnPosition, 16, 16))
		{
			spawnPosition = base.Projectile.Center + base.Projectile.velocity * 50f;
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, base.Projectile.velocity.SafeNormalize(Vector2.Zero) * heldItem.shootSpeed, ModContent.ProjectileType<RainbowComet>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI);
	}

	public void UpdatePlayerVisuals(Vector2 center)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = center;
		if (base.Projectile.velocity == Vector2.Zero || ((Vector2)(ref base.Projectile.velocity)).Length() != 1f)
		{
			base.Projectile.velocity = (Main.MouseWorld - center).SafeNormalize(Vector2.UnitX * (float)base.Projectile.direction);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.direction = (base.Projectile.spriteDirection = (Math.Cos(base.Projectile.rotation) > 0.0).ToDirectionInt());
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
