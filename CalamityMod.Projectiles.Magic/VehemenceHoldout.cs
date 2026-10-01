using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VehemenceHoldout : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Vehemence>();

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public ref float ChargeTime => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Items/Weapons/Magic/Vehemence";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 114);
		base.Projectile.friendly = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		UpdatePlayerVisuals();
		if ((float)base.Projectile.timeLeft > ChargeTime + 5f)
		{
			base.Projectile.timeLeft = (int)ChargeTime + 5;
		}
		if (Time == ChargeTime)
		{
			ShootBolt();
		}
		else if (Time < ChargeTime)
		{
			CreateChargeDust();
		}
		Time++;
		if (Main.mouseLeftRelease && Time >= 5f && Time < ChargeTime)
		{
			base.Projectile.Kill();
		}
	}

	private void UpdatePlayerVisuals()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		base.Projectile.rotation = base.Projectile.AngleTo(Main.MouseWorld);
		base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2();
		Projectile projectile = base.Projectile;
		projectile.Center += base.Projectile.rotation.ToRotationVector2() * 30f;
		base.Projectile.direction = (base.Projectile.spriteDirection = (Math.Cos(base.Projectile.rotation) > 0.0).ToDirectionInt());
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = CalamityUtils.WrapAngle90Degrees(base.Projectile.rotation);
		base.Projectile.rotation += (float)Math.PI / 4f;
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI / 2f;
		}
	}

	private void ShootBolt()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Item heldItem = Owner.HeldItem;
			Vector2 shootVelocity = base.Projectile.velocity * heldItem.shootSpeed;
			int damage = ((heldItem != null) ? Owner.GetWeaponDamage(heldItem) : 0);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity, ModContent.ProjectileType<VehemenceBolt>(), damage, heldItem.knockBack, base.Projectile.owner);
		}
	}

	private void CreateChargeDust()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			Vector2 spawnOffset = base.Projectile.velocity * 94f;
			for (int i = 0; i < 18; i++)
			{
				Dust brimstoneMagic = Dust.NewDustPerfect(base.Projectile.Center + spawnOffset + Main.rand.NextVector2CircularEdge(20f, 20f), 235);
				brimstoneMagic.velocity = (base.Projectile.Center + spawnOffset - brimstoneMagic.position).SafeNormalize(Vector2.Zero) * 0.3f + Owner.velocity;
				brimstoneMagic.velocity.Y -= 2f;
				brimstoneMagic.scale = 1.2f;
				brimstoneMagic.noGravity = true;
			}
		}
	}
}
