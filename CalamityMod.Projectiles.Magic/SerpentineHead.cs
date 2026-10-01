using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SerpentineHead : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = 5;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0.55f / 255f, (float)(255 - base.Projectile.alpha) * 0.55f / 255f);
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 40;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		int seaDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 68, 0f, 0f, 100, default(Color), 1.25f);
		Dust obj = Main.dust[seaDust];
		obj.velocity *= 0.3f;
		Main.dust[seaDust].position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
		Main.dust[seaDust].position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2) + (float)Main.rand.Next(-4, 5);
		Main.dust[seaDust].noGravity = true;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		int direction = base.Projectile.direction;
		base.Projectile.direction = (base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1)));
		if (direction != base.Projectile.direction)
		{
			base.Projectile.netUpdate = true;
		}
		float scaleClamp = MathHelper.Clamp(base.Projectile.localAI[0], 0f, 50f);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.scale = 1f + scaleClamp * 0.01f;
		base.Projectile.width = (base.Projectile.height = (int)(10f * base.Projectile.scale));
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 20f && base.Projectile.ai[0] < 40f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.3f;
		}
		else if (base.Projectile.ai[0] >= 40f && base.Projectile.ai[0] < 60f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.3f;
		}
		else if (base.Projectile.ai[0] >= 60f)
		{
			base.Projectile.ai[0] = 0f;
		}
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[0] <= 0f)
		{
			if (Main.player[base.Projectile.owner].channel)
			{
				float chaseMouseDist = 18f;
				Vector2 projDirection = base.Projectile.Center;
				float mouseX = (float)Main.mouseX + Main.screenPosition.X - projDirection.X;
				float mouseY = (float)Main.mouseY + Main.screenPosition.Y - projDirection.Y;
				if (Main.player[base.Projectile.owner].gravDir == -1f)
				{
					mouseY = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - projDirection.Y;
				}
				float mouseDist = (float)Math.Sqrt(mouseX * mouseX + mouseY * mouseY);
				mouseDist = (float)Math.Sqrt(mouseX * mouseX + mouseY * mouseY);
				if (mouseDist > chaseMouseDist)
				{
					mouseDist = chaseMouseDist / mouseDist;
					mouseX *= mouseDist;
					mouseY *= mouseDist;
					int num = (int)(mouseX * 1000f);
					int exaggeratedXVelocity = (int)(base.Projectile.velocity.X * 1000f);
					int yVelocity = (int)(mouseY * 1000f);
					int exaggeratedYVelocity = (int)(base.Projectile.velocity.Y * 1000f);
					if (num != exaggeratedXVelocity || yVelocity != exaggeratedYVelocity)
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.velocity.X = mouseX;
					base.Projectile.velocity.Y = mouseY;
				}
				else
				{
					int num2 = (int)(mouseX * 1000f);
					int exagXVel = (int)(base.Projectile.velocity.X * 1000f);
					int yVel = (int)(mouseY * 1000f);
					int exagYVel = (int)(base.Projectile.velocity.Y * 1000f);
					if (num2 != exagXVel || yVel != exagYVel)
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.velocity.X = mouseX;
					base.Projectile.velocity.Y = mouseY;
				}
			}
			else if (base.Projectile.ai[0] <= 0f)
			{
				base.Projectile.netUpdate = true;
				Vector2 faceDirection = base.Projectile.Center;
				float miceX = (float)Main.mouseX + Main.screenPosition.X - faceDirection.X;
				float miceY = (float)Main.mouseY + Main.screenPosition.Y - faceDirection.Y;
				if (Main.player[base.Projectile.owner].gravDir == -1f)
				{
					miceY = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - faceDirection.Y;
				}
				float miceDist = (float)Math.Sqrt(miceX * miceX + miceY * miceY);
				if (miceDist == 0f || base.Projectile.ai[0] < 0f)
				{
					((Vector2)(ref faceDirection))._002Ector(Main.player[base.Projectile.owner].position.X + (float)(Main.player[base.Projectile.owner].width / 2), Main.player[base.Projectile.owner].position.Y + (float)(Main.player[base.Projectile.owner].height / 2));
					miceX = base.Projectile.position.X + (float)base.Projectile.width * 0.5f - faceDirection.X;
					miceY = base.Projectile.position.Y + (float)base.Projectile.height * 0.5f - faceDirection.Y;
					miceDist = (float)Math.Sqrt(miceX * miceX + miceY * miceY);
				}
				miceDist = 12f / miceDist;
				miceX *= miceDist;
				miceY *= miceDist;
				base.Projectile.velocity.X = miceX;
				base.Projectile.velocity.Y = miceY;
				if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
				{
					base.Projectile.Kill();
				}
				base.Projectile.ai[0] = 1f;
			}
		}
		if (base.Projectile.velocity.X != 0f || base.Projectile.velocity.Y != 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int k = 0; k < 8; k++)
		{
			int seaDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 68, 0f, 0f, 100, default(Color), 1.25f);
			Dust obj = Main.dust[seaDust];
			obj.velocity *= 0.3f;
			Main.dust[seaDust].position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
			Main.dust[seaDust].position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2) + (float)Main.rand.Next(-4, 5);
			Main.dust[seaDust].noGravity = true;
		}
	}
}
