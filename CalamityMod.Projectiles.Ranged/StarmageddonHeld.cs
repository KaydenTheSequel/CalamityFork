using System;
using System.IO;
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

public class StarmageddonHeld : ModProjectile
{
	private bool starHasBeenFired;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Starmageddon>();

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 166;
		base.Projectile.height = 62;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(starHasBeenFired);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		starHasBeenFired = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.8f, 0.1f, 1f);
		Player player = Main.player[base.Projectile.owner];
		int projectileType = ModContent.ProjectileType<StarmageddonBinaryStarCenter>();
		bool num = player.ownedProjectileCounts[projectileType] == 0;
		if (num && starHasBeenFired)
		{
			base.Projectile.frame = 0;
			starHasBeenFired = false;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 7;
		}
		bool num2 = num && base.Projectile.frame == 7;
		if (player.CantUseHoldout())
		{
			base.Projectile.Kill();
		}
		Vector2 playerPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		if (num2 && Main.myPlayer == base.Projectile.owner && !player.CantUseHoldout())
		{
			SoundEngine.PlaySound(in SoundID.Item92, base.Projectile.Center);
			float shootSpeed = 12f;
			int damage = player.GetWeaponDamage(player.HeldItem);
			float knockBack = player.HeldItem.knockBack;
			base.Projectile.velocity = Main.screenPosition - playerPosition;
			base.Projectile.velocity.X += Main.mouseX;
			base.Projectile.velocity.Y += Main.mouseY;
			if (player.gravDir == -1f)
			{
				base.Projectile.velocity.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerPosition.Y;
			}
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Vector2 dustSpawnPosition = base.Projectile.Center + Vector2.UnitY * -12f + base.Projectile.velocity * 40f;
			Vector2 starSpawnPosition = base.Projectile.Center + Vector2.UnitY * -12f + base.Projectile.velocity * 80f;
			int dustPerSpray = 50;
			for (int h = 0; h < 2; h++)
			{
				bool top = h == 0;
				for (int i = 0; i < dustPerSpray; i++)
				{
					bool useAltDust = i % 2 == 0;
					int dustID = (top ? (useAltDust ? 31 : 6) : (useAltDust ? 160 : 229));
					float num3 = (float)i * 0.2f;
					float angle = (top ? (-0.12f) : 0.12f);
					Vector2 dustVel = Utils.RotatedBy(new Vector2(num3, 0f), (double)(base.Projectile.velocity * shootSpeed).ToRotation(), default(Vector2));
					dustVel = dustVel.RotatedBy(angle);
					float scale = 1.8f - (float)i * 0.01f;
					int idx = Dust.NewDust(dustSpawnPosition, 1, 1, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale);
					Main.dust[idx].noGravity = true;
					Main.dust[idx].position = dustSpawnPosition;
				}
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), starSpawnPosition, base.Projectile.velocity * shootSpeed, projectileType, damage, knockBack, base.Projectile.owner, Projectile.GetByUUID(base.Projectile.owner, base.Projectile.whoAmI));
			starHasBeenFired = true;
			base.Projectile.netUpdate = true;
		}
		if (starHasBeenFired)
		{
			base.Projectile.velocity = Main.screenPosition - playerPosition;
			base.Projectile.velocity.X += Main.mouseX;
			base.Projectile.velocity.Y += Main.mouseY;
			if (player.gravDir == -1f)
			{
				base.Projectile.velocity.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerPosition.Y;
			}
			((Vector2)(ref base.Projectile.velocity)).Normalize();
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Vector2 displayOffset = Utils.RotatedBy(new Vector2(27f, -10f * (float)base.Projectile.direction), (double)base.Projectile.rotation, default(Vector2));
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

	public override void PostDraw(Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Vector2 origin = base.Projectile.Size / 2f;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/StarmageddonHeldGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
