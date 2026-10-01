using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NebulousCataclysm_Held : ModProjectile
{
	private const int TotalXFrames = 2;

	private const int TotalYFrames = 8;

	private const int FrameTimer = 6;

	private const int ShootFrame = 3;

	public int frameX;

	public int frameY;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<NebulousCataclysm>();

	public int CurrentFrame
	{
		get
		{
			return frameX * 8 + frameY;
		}
		set
		{
			frameX = value / 8;
			frameY = value % 8;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 120;
		base.Projectile.height = 124;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 6 == 0)
		{
			CurrentFrame++;
			if (frameX >= 2)
			{
				CurrentFrame = 0;
			}
		}
		Lighting.AddLight(base.Projectile.Center, 1.25f, 0f, 0.2f);
		if (CurrentFrame != 3)
		{
			base.Projectile.ai[0] = 0f;
		}
		bool num = CurrentFrame == 3 && base.Projectile.ai[0] == 0f;
		bool ableToShoot = false;
		bool weaponInUse = !Owner.CantUseHoldout();
		Vector2 halvedSize = base.Projectile.Size / 2f;
		Vector2 staffOffset = halvedSize + new Vector2(24f, 24f);
		if (num)
		{
			base.Projectile.ai[0] = 1f;
			if (weaponInUse)
			{
				if (Owner.CheckMana(Owner.HeldItem.mana))
				{
					ableToShoot = true;
				}
				else
				{
					base.Projectile.Kill();
				}
				if (ableToShoot)
				{
					SoundEngine.PlaySound(in SoundID.Item117, base.Projectile.Center);
				}
			}
			else
			{
				base.Projectile.Kill();
			}
			if ((Main.myPlayer == base.Projectile.owner) & ableToShoot)
			{
				Owner.CheckMana(Owner.HeldItem.mana, pay: true);
				int projectileType = ModContent.ProjectileType<NebulaCloudCore>();
				float coreVelocity = 8f;
				int weaponDamage = Owner.GetWeaponDamage(Owner.HeldItem);
				float weaponKnockback = Owner.HeldItem.knockBack;
				if (weaponInUse)
				{
					weaponKnockback = Owner.GetWeaponKnockback(Owner.HeldItem, weaponKnockback);
					float scaleFactor = Owner.HeldItem.shootSpeed * base.Projectile.scale;
					Vector2 projectileSpawnPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
					Vector2 projectileDestination = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - projectileSpawnPosition;
					if (Owner.gravDir == -1f)
					{
						projectileDestination.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - projectileSpawnPosition.Y;
					}
					Vector2 velocity = Vector2.Normalize(projectileDestination);
					if (float.IsNaN(velocity.X) || float.IsNaN(velocity.Y))
					{
						velocity = -Vector2.UnitY;
					}
					velocity *= scaleFactor;
					if (velocity.X != base.Projectile.velocity.X || velocity.Y != base.Projectile.velocity.Y)
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.velocity = velocity * 0.5f;
					Vector2 projectileVelocity = Vector2.Normalize(base.Projectile.velocity) * coreVelocity;
					if (float.IsNaN(projectileVelocity.X) || float.IsNaN(projectileVelocity.Y))
					{
						projectileVelocity = -Vector2.UnitY;
					}
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projectileSpawnPosition + Vector2.Normalize(projectileVelocity) * ((Vector2)(ref staffOffset)).Length(), projectileVelocity, projectileType, weaponDamage, weaponKnockback, base.Projectile.owner);
				}
				else
				{
					base.Projectile.Kill();
				}
			}
		}
		base.Projectile.position = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true) - halvedSize + Vector2.Normalize(base.Projectile.velocity) * staffOffset;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI * 3f / 4f) : ((float)Math.PI / 4f));
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)base.Projectile.direction, base.Projectile.velocity.X * (float)base.Projectile.direction);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.frameCounter >= 5)
		{
			Texture2D value = TextureAssets.Projectile[base.Type].Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			Vector2 origin = value.Size() / new Vector2(2f, 8f) * 0.5f;
			Rectangle frame = value.Frame(2, 8, frameX, frameY);
			Main.EntitySpriteDraw(effects: (SpriteEffects)(base.Projectile.spriteDirection != 1), texture: value, position: position, sourceRectangle: frame, color: Color.White, rotation: base.Projectile.rotation, origin: origin, scale: base.Projectile.scale);
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
