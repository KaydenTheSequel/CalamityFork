using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class SCalPet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Type], 6).WithOffset(-12f, -18f).WithSpriteDirection(-1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
	}

	public override void AI()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (player.dead)
		{
			modPlayer.scalPet = false;
		}
		if (modPlayer.scalPet)
		{
			base.Projectile.timeLeft = 2;
		}
		float passiveMvtFloat = 0.5f;
		base.Projectile.tileCollide = false;
		int range = 300;
		Vector2 center = base.Projectile.Center;
		float distX = player.Center.X - center.X;
		float distY = player.Center.Y - center.Y;
		float playerDist = player.Distance(center);
		float returnSpeed = 18f;
		float maxDist = 2000f;
		bool num = playerDist > maxDist;
		if (playerDist < (float)range && Main.player[base.Projectile.owner].velocity.Y == 0f && base.Projectile.position.Y + (float)base.Projectile.height <= Main.player[base.Projectile.owner].position.Y + (float)Main.player[base.Projectile.owner].height && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.ai[0] = 0f;
			if (base.Projectile.velocity.Y < -6f)
			{
				base.Projectile.velocity.Y = -6f;
			}
		}
		if (playerDist < 150f)
		{
			if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.99f;
			}
			passiveMvtFloat = 0.01f;
			if (distX < -2f)
			{
				distX = -2f;
			}
			if (distX > 2f)
			{
				distX = 2f;
			}
			if (distY < -2f)
			{
				distY = -2f;
			}
			if (distY > 2f)
			{
				distY = 2f;
			}
		}
		else
		{
			if (playerDist > 300f)
			{
				passiveMvtFloat = 0.2f;
			}
			playerDist = returnSpeed / playerDist;
			distX *= playerDist;
			distY *= playerDist;
		}
		if (num)
		{
			base.Projectile.Center = Main.player[base.Projectile.owner].Center;
			base.Projectile.velocity = Vector2.Zero;
			if (Main.myPlayer == base.Projectile.owner)
			{
				base.Projectile.netUpdate = true;
			}
		}
		if (Math.Abs(distX) > Math.Abs(distY) || passiveMvtFloat == 0.05f)
		{
			if (base.Projectile.velocity.X < distX)
			{
				base.Projectile.velocity.X += passiveMvtFloat;
				if (passiveMvtFloat > 0.05f && base.Projectile.velocity.X < 0f)
				{
					base.Projectile.velocity.X += passiveMvtFloat;
				}
			}
			if (base.Projectile.velocity.X > distX)
			{
				base.Projectile.velocity.X -= passiveMvtFloat;
				if (passiveMvtFloat > 0.05f && base.Projectile.velocity.X > 0f)
				{
					base.Projectile.velocity.X -= passiveMvtFloat;
				}
			}
		}
		if (Math.Abs(distX) <= Math.Abs(distY) || passiveMvtFloat == 0.05f)
		{
			if (base.Projectile.velocity.Y < distY)
			{
				base.Projectile.velocity.Y += passiveMvtFloat;
				if (passiveMvtFloat > 0.05f && base.Projectile.velocity.Y < 0f)
				{
					base.Projectile.velocity.Y += passiveMvtFloat;
				}
			}
			if (base.Projectile.velocity.Y > distY)
			{
				base.Projectile.velocity.Y -= passiveMvtFloat;
				if (passiveMvtFloat > 0.05f && base.Projectile.velocity.Y > 0f)
				{
					base.Projectile.velocity.Y -= passiveMvtFloat;
				}
			}
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
	}
}
