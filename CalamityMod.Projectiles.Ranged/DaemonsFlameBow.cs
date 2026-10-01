using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DaemonsFlameBow : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<DaemonsFlame>();

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 116;
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
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0.7f, 0.5f);
		Player player = Main.player[base.Projectile.owner];
		float pi = 0f;
		Vector2 playerRotate = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		if (base.Projectile.spriteDirection == -1)
		{
			pi = (float)Math.PI;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.ai[0]++;
		int fireSpeed = 0;
		if (base.Projectile.ai[0] >= 30f)
		{
			fireSpeed++;
		}
		if (base.Projectile.ai[0] >= 60f)
		{
			fireSpeed++;
		}
		if (base.Projectile.ai[0] >= 90f)
		{
			fireSpeed++;
		}
		if (base.Projectile.ai[0] >= 120f)
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
				Vector2 shootDirection = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerRotate;
				if (player.gravDir == -1f)
				{
					shootDirection.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerRotate.Y;
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
				for (int i = 0; i < 4; i++)
				{
					Vector2 randNormalize = Vector2.Normalize(base.Projectile.velocity) * scaleFactor11 * (0.6f + Main.rand.NextFloat() * 0.8f);
					if (float.IsNaN(randNormalize.X) || float.IsNaN(randNormalize.Y))
					{
						randNormalize = -Vector2.UnitY;
					}
					Vector2 projRandomPos = playerRotate + Utils.RandomVector2(Main.rand, -15f, 15f);
					int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projRandomPos.X, projRandomPos.Y, randNormalize.X, randNormalize.Y, ammoType, weaponDamage2, weaponKnockback2, base.Projectile.owner);
					Main.projectile[proj].noDropItem = true;
				}
				Vector2 daemonNormalize = Vector2.Normalize(base.Projectile.velocity) * scaleFactor11 * (0.6f + Main.rand.NextFloat() * 0.8f);
				if (float.IsNaN(daemonNormalize.X) || float.IsNaN(daemonNormalize.Y))
				{
					daemonNormalize = -Vector2.UnitY;
				}
				float speedY = daemonNormalize.Y;
				Vector2 daemonRandomPos = playerRotate + Utils.RandomVector2(Main.rand, -15f, 15f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), daemonRandomPos.X, daemonRandomPos.Y, daemonNormalize.X, daemonNormalize.Y, ModContent.ProjectileType<DaemonsFlameArrow>(), weaponDamage2, weaponKnockback2, base.Projectile.owner, 0f, speedY);
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
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(27f, 58f);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/DaemonsFlameBowGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
