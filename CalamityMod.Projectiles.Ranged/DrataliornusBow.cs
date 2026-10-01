using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DrataliornusBow : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Drataliornus>();

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/Drataliornus";

	public override void SetDefaults()
	{
		base.Projectile.width = 64;
		base.Projectile.height = 84;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1f, 0.6039216f, 0.22745098f);
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.ai[1]--;
		int usedAmmoItemId;
		if (base.Projectile.ai[0] >= 0f)
		{
			base.Projectile.ai[0]++;
			usedAmmoItemId = (int)base.Projectile.ai[0];
			switch (usedAmmoItemId)
			{
			case 36:
			case 72:
			case 108:
			case 144:
			case 180:
			case 216:
			case 252:
			case 288:
			case 324:
				base.Projectile.localAI[0]++;
				break;
			case 360:
			{
				base.Projectile.localAI[0]++;
				base.Projectile.ai[0] = -1f;
				for (int i = 0; i < 36; i++)
				{
					Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * 9f).RotatedBy((float)(i - 17) * ((float)Math.PI * 2f) / 36f) + player.Center;
					Vector2 faceDirection = val - player.Center;
					int dust = Dust.NewDust(val + faceDirection, 0, 0, 127, 0f, 0f, 0, default(Color), 4f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity = faceDirection;
				}
				break;
			}
			}
		}
		int baseUseTime = 38;
		int modifier = 3;
		bool timeToFire = false;
		if (base.Projectile.ai[1] <= 0f)
		{
			base.Projectile.ai[1] = (float)baseUseTime - (float)modifier * base.Projectile.localAI[0];
			timeToFire = true;
		}
		bool canFire = !player.CantUseHoldout() && player.HasAmmo(player.HeldItem);
		if ((base.Projectile.soundDelay <= 0) & canFire)
		{
			base.Projectile.soundDelay = baseUseTime - modifier * (int)base.Projectile.localAI[0];
			if (base.Projectile.ai[0] != 1f)
			{
				SoundEngine.PlaySound(in SoundID.Item5, base.Projectile.position);
			}
		}
		if (timeToFire && Main.myPlayer == base.Projectile.owner)
		{
			if (canFire)
			{
				int type = 1;
				float scaleFactor = 18f;
				int damage = player.GetWeaponDamage(player.HeldItem);
				float knockBack = player.HeldItem.knockBack;
				player.PickAmmo(player.HeldItem, out type, out scaleFactor, out damage, out knockBack, out usedAmmoItemId);
				type = ModContent.ProjectileType<DrataliornusFlame>();
				knockBack = player.GetWeaponKnockback(player.HeldItem, knockBack);
				Vector2 playerPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
				base.Projectile.velocity = Main.screenPosition - playerPosition;
				base.Projectile.velocity.X += Main.mouseX;
				base.Projectile.velocity.Y += Main.mouseY;
				if (player.gravDir == -1f)
				{
					base.Projectile.velocity.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerPosition.Y;
				}
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				float variation = (1f + base.Projectile.localAI[0]) * 3f;
				Vector2 position = playerPosition + Utils.RandomVector2(Main.rand, 0f - variation, variation);
				Vector2 speed = base.Projectile.velocity * scaleFactor * (0.6f + Main.rand.NextFloat() * 0.6f);
				float ai0 = 0f;
				if (base.Projectile.ai[0] < 0f)
				{
					if (Main.rand.NextBool(3))
					{
						ai0 = 2f;
						speed /= 2f;
					}
					else
					{
						ai0 = 1f;
					}
				}
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), position, speed, type, damage, knockBack, base.Projectile.owner, ai0);
				base.Projectile.netUpdate = true;
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Vector2 displayOffset = Utils.RotatedBy(new Vector2(32f, 0f), (double)base.Projectile.rotation, default(Vector2));
		base.Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) + displayOffset;
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
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
