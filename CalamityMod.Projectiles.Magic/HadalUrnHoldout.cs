using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class HadalUrnHoldout : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle UrnSound = new SoundStyle("CalamityMod/Sounds/Item/HadalUrnClose");

	public int manatimer;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<HadalUrn>();

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float rotoffset = (float)Math.PI / 2f;
		Vector2 playerpos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		bool shouldBeHeld = !player.CantUseHoldout();
		base.Projectile.damage = ((player.HeldItem != null) ? player.GetWeaponDamage(player.HeldItem) : 0);
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]--;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (shouldBeHeld)
			{
				float holdscale = player.HeldItem.shootSpeed * base.Projectile.scale;
				Vector2 playerpos2 = playerpos;
				Vector2 going = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerpos2;
				if (player.gravDir == -1f)
				{
					going.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerpos2.Y;
				}
				Vector2 normalizedgoing = Vector2.Normalize(going);
				if (float.IsNaN(normalizedgoing.X) || float.IsNaN(normalizedgoing.Y))
				{
					normalizedgoing = -Vector2.UnitY;
				}
				normalizedgoing *= holdscale;
				if (normalizedgoing.X != base.Projectile.velocity.X || normalizedgoing.Y != base.Projectile.velocity.Y)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = normalizedgoing * 0.55f;
				if (base.Projectile.ai[0] <= 0f)
				{
					if (player.CheckMana(player.HeldItem, -1, pay: true))
					{
						SoundEngine.PlaySound(in SoundID.Item111, base.Projectile.Center);
						int projcount = 3;
						for (int i = 0; i < projcount; i++)
						{
							int projType = Main.rand.Next(6);
							int projDamage = base.Projectile.damage;
							int spreadfactor = 45;
							int ai = 0;
							float speedscale = 18f;
							switch (projType)
							{
							case 0:
							case 1:
								projType = ModContent.ProjectileType<HadalUrnLamprey>();
								break;
							case 2:
							case 3:
								projType = ModContent.ProjectileType<HadalUrnStarfish>();
								break;
							case 4:
								projType = ModContent.ProjectileType<HadalUrnJellyfish>();
								speedscale *= 0.5f;
								ai = 30;
								break;
							default:
								projType = ModContent.ProjectileType<HadalUrnIsopod>();
								projDamage = (int)((float)projDamage * 1.75f);
								speedscale *= 1.5f;
								break;
							}
							Vector2 shotSpeed = Vector2.Normalize(base.Projectile.velocity) * speedscale;
							shotSpeed.X += (float)Main.rand.Next(-spreadfactor, spreadfactor + 1) * 0.05f;
							shotSpeed.Y += (float)Main.rand.Next(-spreadfactor, spreadfactor + 1) * 0.05f;
							if (float.IsNaN(shotSpeed.X) || float.IsNaN(shotSpeed.Y))
							{
								shotSpeed = -Vector2.UnitY;
							}
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shotSpeed, projType, projDamage, base.Projectile.knockBack, player.whoAmI, ai);
						}
						base.Projectile.ai[0] = player.HeldItem.useTime;
					}
					else
					{
						SoundEngine.PlaySound(in UrnSound, base.Projectile.Center);
						base.Projectile.Kill();
					}
				}
			}
			else
			{
				SoundEngine.PlaySound(in UrnSound, base.Projectile.Center);
				base.Projectile.Kill();
			}
		}
		base.Projectile.position = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) - base.Projectile.Size / 2f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + rotoffset;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)base.Projectile.direction, base.Projectile.velocity.X * (float)base.Projectile.direction);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
