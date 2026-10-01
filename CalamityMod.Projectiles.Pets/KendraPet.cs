using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class KendraPet : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle BarkSound = new SoundStyle("CalamityMod/Sounds/Item/KendraBark");

	private int chosenIdle;

	private int idleTimer;

	private int idleBarkTimer;

	private int playerStill;

	private bool fly;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 31;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, 13, 6).WithOffset(-30f, 0f).WithSpriteDirection(-1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 46;
		base.Projectile.height = 46;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.tileCollide = true;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		Vector2 projCenter = base.Projectile.Center;
		Vector2 playerDirection = obj.Center - projCenter;
		float playerDistance = ((Vector2)(ref playerDirection)).Length();
		fallThrough = playerDistance > 200f;
		return true;
	}

	public override void AI()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.kendra = false;
		}
		if (modPlayer.kendra)
		{
			base.Projectile.timeLeft = 2;
		}
		_ = base.Projectile.position;
		if (!fly)
		{
			base.Projectile.rotation = 0f;
			Vector2 projCenter = base.Projectile.Center;
			Vector2 playerDirection = player.Center - projCenter;
			float playerDistance = ((Vector2)(ref playerDirection)).Length();
			if (base.Projectile.velocity.Y == 0f && (HoleBelow() || (playerDistance > 110f && base.Projectile.position.X == base.Projectile.oldPosition.X)))
			{
				base.Projectile.velocity.Y = -5f;
			}
			base.Projectile.velocity.Y += 0.2f;
			if (base.Projectile.velocity.Y > 7f)
			{
				base.Projectile.velocity.Y = 7f;
			}
			if (playerDistance > 600f)
			{
				fly = true;
				base.Projectile.velocity.X = 0f;
				base.Projectile.velocity.Y = 0f;
				base.Projectile.tileCollide = false;
			}
			if (playerDistance > 100f)
			{
				if (player.position.X - base.Projectile.position.X > 0f)
				{
					base.Projectile.velocity.X += 0.1f;
					if (base.Projectile.velocity.X > 7f)
					{
						base.Projectile.velocity.X = 7f;
					}
				}
				else
				{
					base.Projectile.velocity.X -= 0.1f;
					if (base.Projectile.velocity.X < -7f)
					{
						base.Projectile.velocity.X = -7f;
					}
				}
			}
			if (playerDistance < 100f && base.Projectile.velocity.X != 0f)
			{
				if (base.Projectile.velocity.X > 0.5f)
				{
					base.Projectile.velocity.X -= 0.15f;
				}
				else if (base.Projectile.velocity.X < -0.5f)
				{
					base.Projectile.velocity.X += 0.15f;
				}
				else if (base.Projectile.velocity.X < 0.5f && base.Projectile.velocity.X > -0.5f)
				{
					base.Projectile.velocity.X = 0f;
				}
			}
			if (base.Projectile.position.X == base.Projectile.oldPosition.X && base.Projectile.position.Y == base.Projectile.oldPosition.Y && base.Projectile.velocity.X == 0f)
			{
				base.Projectile.frameCounter++;
				switch (chosenIdle)
				{
				case 1:
					if (idleTimer == 0)
					{
						base.Projectile.frame = 0;
					}
					idleTimer++;
					if (base.Projectile.frameCounter > 6)
					{
						base.Projectile.frame++;
						base.Projectile.frameCounter = 0;
					}
					if (base.Projectile.frame > 4)
					{
						chosenIdle = 0;
					}
					if (base.Projectile.frame < 1)
					{
						base.Projectile.frame = 1;
					}
					break;
				case 2:
					if (idleTimer == 0)
					{
						base.Projectile.frame = 0;
					}
					idleTimer++;
					if (base.Projectile.frameCounter > 6)
					{
						base.Projectile.frame++;
						base.Projectile.frameCounter = 0;
					}
					if (base.Projectile.frame > 13)
					{
						chosenIdle = 0;
					}
					if (base.Projectile.frame < 5)
					{
						base.Projectile.frame = 5;
					}
					break;
				case 3:
					if (idleTimer == 0)
					{
						base.Projectile.frame = 0;
					}
					idleTimer++;
					if (base.Projectile.frameCounter > 6)
					{
						base.Projectile.frame++;
						base.Projectile.frameCounter = 0;
					}
					if (base.Projectile.frame > 24)
					{
						chosenIdle = 0;
					}
					if (base.Projectile.frame < 19)
					{
						base.Projectile.frame = 19;
					}
					break;
				}
				if (chosenIdle == 0)
				{
					base.Projectile.frame = 0;
					base.Projectile.frameCounter = 6;
					idleTimer++;
					if (idleTimer > 360)
					{
						chosenIdle = Main.rand.Next(1, 3);
						idleTimer = 0;
					}
					idleBarkTimer++;
					if (idleBarkTimer > 1080 && Main.rand.NextBool())
					{
						SoundEngine.PlaySound(in BarkSound, base.Projectile.position);
						chosenIdle = 3;
						idleBarkTimer = 0;
						idleTimer = 0;
					}
				}
			}
			else if (base.Projectile.velocity.Y > 0.3f && base.Projectile.position.Y != base.Projectile.oldPosition.Y)
			{
				base.Projectile.frame = 16;
				base.Projectile.frameCounter = 0;
			}
			else
			{
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 6)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame > 18)
				{
					base.Projectile.frame = 14;
				}
				if (base.Projectile.frame < 14)
				{
					base.Projectile.frame = 14;
				}
			}
		}
		else if (fly)
		{
			float flySpeed = 0.3f;
			base.Projectile.tileCollide = false;
			Vector2 flyDirection = base.Projectile.Center;
			float horiPos = Main.player[base.Projectile.owner].position.X + (float)(Main.player[base.Projectile.owner].width / 2) - flyDirection.X;
			float vertiPos = Main.player[base.Projectile.owner].position.Y + (float)(Main.player[base.Projectile.owner].height / 2) - flyDirection.Y;
			vertiPos += (float)Main.rand.Next(-10, 21);
			horiPos += (float)Main.rand.Next(-10, 21);
			horiPos += 60f * (0f - (float)Main.player[base.Projectile.owner].direction);
			vertiPos -= 60f;
			float playerDistance2 = (float)Math.Sqrt(horiPos * horiPos + vertiPos * vertiPos);
			if (playerDistance2 > 2000f)
			{
				base.Projectile.position.X = Main.player[base.Projectile.owner].Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = Main.player[base.Projectile.owner].Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (playerDistance2 < 100f)
			{
				flySpeed = 0.1f;
				if (player.velocity.Y == 0f)
				{
					playerStill++;
				}
				else
				{
					playerStill = 0;
				}
				if (playerStill > 60 && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
				{
					fly = false;
					base.Projectile.tileCollide = true;
				}
			}
			if (playerDistance2 < 50f)
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
				if (playerDistance2 < 100f)
				{
					flySpeed = 0.1f;
				}
				if (playerDistance2 > 300f)
				{
					flySpeed = 1f;
				}
				playerDistance2 = 18.5f / playerDistance2;
				horiPos *= playerDistance2;
				vertiPos *= playerDistance2;
			}
			if (base.Projectile.velocity.X <= horiPos)
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
			if (base.Projectile.velocity.Y <= vertiPos)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + flySpeed;
				if (flySpeed > 0.05f && base.Projectile.velocity.Y < 0f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y + flySpeed * 2f;
				}
			}
			if (base.Projectile.velocity.Y > vertiPos)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - flySpeed;
				if (flySpeed > 0.05f && base.Projectile.velocity.Y > 0f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y - flySpeed * 2f;
				}
			}
			base.Projectile.rotation = base.Projectile.velocity.X * 0.015f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 6)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 30)
			{
				base.Projectile.frame = 25;
			}
			if (base.Projectile.frame < 25)
			{
				base.Projectile.frame = 25;
			}
		}
		if (base.Projectile.velocity.X > 0.25f)
		{
			base.Projectile.spriteDirection = -1;
		}
		else if (base.Projectile.velocity.X < -0.25f)
		{
			base.Projectile.spriteDirection = 1;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	private bool HoleBelow()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		int tileWidth = 4;
		int tileX = (int)(base.Projectile.Center.X / 16f) - tileWidth;
		if (base.Projectile.velocity.X > 0f)
		{
			tileX += tileWidth;
		}
		int tileY = (int)((base.Projectile.position.Y + (float)base.Projectile.height) / 16f);
		for (int y = tileY; y < tileY + 2; y++)
		{
			for (int x = tileX; x < tileX + tileWidth; x++)
			{
				if (Main.tile[x, y].HasTile)
				{
					return false;
				}
			}
		}
		return true;
	}
}
