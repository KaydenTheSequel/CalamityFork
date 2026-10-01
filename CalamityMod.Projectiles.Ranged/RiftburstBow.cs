using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class RiftburstBow : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Riftburst>();

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/Riftburst";

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 82;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0.7f, 0.5f);
		Player player = Main.player[base.Projectile.owner];
		float pi = 0f;
		Vector2 playerRotation = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		if (base.Projectile.spriteDirection == -1)
		{
			pi = (float)Math.PI;
		}
		base.Projectile.ai[0]++;
		int fireSpeed = 0;
		if (base.Projectile.ai[0] >= 90f)
		{
			fireSpeed++;
		}
		if (base.Projectile.ai[0] >= 180f)
		{
			fireSpeed++;
		}
		if (base.Projectile.ai[0] >= 270f)
		{
			fireSpeed++;
		}
		int delayCompare = 24;
		int fireSpeedCompare = 2;
		base.Projectile.ai[1]--;
		bool fullSpeed = false;
		if (base.Projectile.ai[1] <= 0f)
		{
			base.Projectile.ai[1] = delayCompare - fireSpeedCompare * fireSpeed;
			fullSpeed = true;
			_ = (int)base.Projectile.ai[0] / (delayCompare - fireSpeedCompare * fireSpeed);
		}
		bool canUseItem = !player.CantUseHoldout() && player.HasAmmo(player.HeldItem);
		if (base.Projectile.localAI[0] > 0f)
		{
			base.Projectile.localAI[0]--;
		}
		if ((base.Projectile.soundDelay <= 0) & canUseItem)
		{
			base.Projectile.soundDelay = delayCompare - fireSpeedCompare * fireSpeed;
			if (base.Projectile.ai[0] != 1f)
			{
				SoundEngine.PlaySound(in SoundID.Item5, base.Projectile.position);
			}
			base.Projectile.localAI[0] = 12f;
		}
		player.phantasmTime = 2;
		if (fullSpeed && Main.myPlayer == base.Projectile.owner)
		{
			int ammoType = 1;
			float scaleFactor11 = 14f;
			int weaponDamage2 = player.GetWeaponDamage(player.HeldItem);
			float weaponKnockback2 = player.HeldItem.knockBack;
			if (canUseItem)
			{
				player.PickAmmo(player.HeldItem, out ammoType, out scaleFactor11, out weaponDamage2, out weaponKnockback2, out var _);
				weaponKnockback2 = player.GetWeaponKnockback(player.HeldItem, weaponKnockback2);
				float scaleFactor12 = player.HeldItem.shootSpeed * base.Projectile.scale;
				Vector2 shootDirection = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerRotation;
				if (player.gravDir == -1f)
				{
					shootDirection.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerRotation.Y;
				}
				Vector2 normalizeShoot = Vector2.Normalize(shootDirection);
				if (float.IsNaN(normalizeShoot.X) || float.IsNaN(normalizeShoot.Y))
				{
					normalizeShoot = -Vector2.UnitY;
				}
				normalizeShoot *= scaleFactor12;
				if (normalizeShoot.X != base.Projectile.velocity.X || normalizeShoot.Y != base.Projectile.velocity.Y)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = normalizeShoot * 0.55f;
				for (int i = 0; i < 5; i++)
				{
					Vector2 randNormalize = Vector2.Normalize(base.Projectile.velocity) * scaleFactor11 * (0.6f + Main.rand.NextFloat() * 0.8f);
					if (float.IsNaN(randNormalize.X) || float.IsNaN(randNormalize.Y))
					{
						randNormalize = -Vector2.UnitY;
					}
					Vector2 projRandomPos = playerRotation + Utils.RandomVector2(Main.rand, -15f, 15f);
					int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projRandomPos, randNormalize, ammoType, weaponDamage2, weaponKnockback2, base.Projectile.owner);
					Main.projectile[proj].noDropItem = true;
				}
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.position = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) - base.Projectile.Size / 2f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + pi;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)base.Projectile.direction, base.Projectile.velocity.X * (float)base.Projectile.direction);
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/RiftburstGlow", (AssetRequestMode)2).Value;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), base.Projectile.scale, spriteEffects);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
