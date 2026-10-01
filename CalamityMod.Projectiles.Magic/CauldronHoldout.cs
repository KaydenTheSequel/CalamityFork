using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class CauldronHoldout : ModProjectile, ILocalizedModType, IModType
{
	public static int FireRate = 60;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<TheCauldron>();

	public override string Texture => "CalamityMod/Items/Weapons/Magic/TheCauldron";

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 46;
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
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float rotoffset = (float)Math.PI / 4f;
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
				Vector2 going = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - base.Projectile.Center;
				if (player.gravDir == -1f)
				{
					going.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - base.Projectile.Center.Y;
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
						SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballShot, base.Projectile.Center);
						int projType = ModContent.ProjectileType<CauldronProj>();
						int projDamage = base.Projectile.damage;
						float speedscale = 18f;
						Vector2 shotSpeed = base.Projectile.velocity.SafeNormalize(-Vector2.UnitY) * speedscale;
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shotSpeed, projType, projDamage, base.Projectile.knockBack, player.whoAmI);
						for (int i = 0; i < 6; i++)
						{
							Vector2 burstSpeed = (base.Projectile.rotation - rotoffset).ToRotationVector2().RotatedByRandom(MathHelper.ToRadians(45f)) * Main.rand.NextFloat(8f, 14f);
							GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 5f, burstSpeed, Main.rand.NextFloat(0.2f, 0.3f), Color.Orange, Main.rand.Next(6, 11), 3f, 1.5f));
						}
						base.Projectile.ai[0] = FireRate;
					}
					else
					{
						base.Projectile.Kill();
					}
				}
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) - new Vector2((float)((player.direction == 1) ? 6 : 0), 30f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + rotoffset;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)base.Projectile.direction, base.Projectile.velocity.X * (float)base.Projectile.direction);
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI);
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(TheCauldron.Glow.Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
