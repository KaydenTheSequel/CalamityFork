using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class BrimlingPet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, 4, 5).WithOffset(-25f, 0f).WithSpriteDirection(-1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 62;
		base.Projectile.height = 60;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
	}

	public override void AI()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.brimling = false;
		}
		if (modPlayer.brimling)
		{
			base.Projectile.timeLeft = 4;
		}
		float flySpeed = 0.5f;
		base.Projectile.tileCollide = false;
		Vector2 flyDirection = base.Projectile.Center;
		float horiPos = Main.player[base.Projectile.owner].position.X + (float)(Main.player[base.Projectile.owner].width / 2) - flyDirection.X;
		float vertPos = Main.player[base.Projectile.owner].position.Y + (float)(Main.player[base.Projectile.owner].height / 2) - flyDirection.Y;
		vertPos += (float)Main.rand.Next(-10, 21);
		horiPos += (float)Main.rand.Next(-10, 21);
		horiPos += 60f * (0f - (float)Main.player[base.Projectile.owner].direction);
		vertPos -= 60f;
		float playerDistance = (float)Math.Sqrt(horiPos * horiPos + vertPos * vertPos);
		if (playerDistance > 1000f)
		{
			base.Projectile.position.X = base.Projectile.position.X + horiPos;
			base.Projectile.position.Y = base.Projectile.position.Y + vertPos;
			for (int k = 0; k < 10; k++)
			{
				Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 235, 0f, -1f);
			}
		}
		if (playerDistance < 100f && Main.player[base.Projectile.owner].velocity.Y == 0f && base.Projectile.position.Y + (float)base.Projectile.height <= Main.player[base.Projectile.owner].position.Y + (float)Main.player[base.Projectile.owner].height && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.ai[0] = 0f;
			if (base.Projectile.velocity.Y < -6f)
			{
				base.Projectile.velocity.Y = -6f;
			}
		}
		if (playerDistance < 50f)
		{
			if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.9f;
			}
			flySpeed = 0.01f;
		}
		else
		{
			if (playerDistance < 100f)
			{
				flySpeed = 0.1f;
			}
			if (playerDistance > 300f)
			{
				flySpeed = 1f;
			}
			playerDistance = 18f / playerDistance;
			horiPos *= playerDistance;
			vertPos *= playerDistance;
		}
		if (base.Projectile.velocity.X < horiPos)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X + flySpeed;
			if (flySpeed > 0.05f && base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + flySpeed;
			}
		}
		if (base.Projectile.velocity.X > horiPos)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X - flySpeed;
			if (flySpeed > 0.05f && base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - flySpeed;
			}
		}
		if (base.Projectile.velocity.Y < vertPos)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + flySpeed;
			if (flySpeed > 0.05f && base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + flySpeed * 2f;
			}
		}
		if (base.Projectile.velocity.Y > vertPos)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - flySpeed;
			if (flySpeed > 0.05f && base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - flySpeed * 2f;
			}
		}
		if ((double)base.Projectile.velocity.X > 0.25)
		{
			base.Projectile.direction = -1;
		}
		else if ((double)base.Projectile.velocity.X < -0.25)
		{
			base.Projectile.direction = 1;
		}
		Player projOwner = Main.player[base.Projectile.owner];
		base.Projectile.spriteDirection = -projOwner.direction;
		base.Projectile.rotation = base.Projectile.velocity.X * 0.03f;
		base.Projectile.frameCounter++;
		if (projOwner.statLife >= projOwner.statLifeMax2 / 4)
		{
			if (base.Projectile.frameCounter > 5)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 3)
			{
				base.Projectile.frame = 0;
			}
		}
		else
		{
			if (base.Projectile.frameCounter > 5)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 7)
			{
				base.Projectile.frame = 4;
			}
		}
	}
}
