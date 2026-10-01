using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class EidolonSnail : ModProjectile, ILocalizedModType, IModType
{
	private int playerStill;

	private bool idleAnimation;

	private bool fly;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, 4, 5).WithOffset(-18f, 0f).WithSpriteDirection(1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 54;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/EidolonSnail", (AssetRequestMode)2).Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY + 2f), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), lightColor, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/EidolonSnailGlow", (AssetRequestMode)2).Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY + 2f), (Rectangle?)new Rectangle(0, frameHeight, texture.Width, height), Color.White, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, spriteEffects, 0f);
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
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af1: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.eidolonSnailPet = false;
		}
		if (modPlayer.eidolonSnailPet)
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
			if (base.Projectile.velocity.Y == 0f && ((HoleBelow() && playerDistance > 150f) || (playerDistance > 150f && base.Projectile.position.X == base.Projectile.oldPosition.X)))
			{
				base.Projectile.velocity.Y = -8f;
			}
			base.Projectile.velocity.Y += 0.35f;
			if (base.Projectile.velocity.Y > 15f)
			{
				base.Projectile.velocity.Y = 15f;
			}
			if (playerDistance > 520f)
			{
				fly = true;
				base.Projectile.velocity.X = 0f;
				base.Projectile.velocity.Y = 0f;
			}
			if (playerDistance > 100f)
			{
				if (player.position.X - base.Projectile.position.X > 0f)
				{
					base.Projectile.velocity.X += 0.12f;
					if (base.Projectile.velocity.X > 6f)
					{
						base.Projectile.velocity.X = 6f;
					}
				}
				else
				{
					base.Projectile.velocity.X -= 0.12f;
					if (base.Projectile.velocity.X < -6f)
					{
						base.Projectile.velocity.X = -6f;
					}
				}
			}
			if (playerDistance < 100f && base.Projectile.velocity.X != 0f)
			{
				if (base.Projectile.velocity.X > 0.8f)
				{
					base.Projectile.velocity.X -= 0.25f;
				}
				else if (base.Projectile.velocity.X < -0.8f)
				{
					base.Projectile.velocity.X += 0.25f;
				}
				else if (base.Projectile.velocity.X < 0.8f && base.Projectile.velocity.X > -0.8f)
				{
					base.Projectile.velocity.X = 0f;
				}
			}
			if (playerDistance < 70f)
			{
				base.Projectile.velocity.X *= 0.5f;
			}
			if (base.Projectile.position.X == base.Projectile.oldPosition.X && base.Projectile.position.Y == base.Projectile.oldPosition.Y && base.Projectile.velocity.X == 0f)
			{
				if (Main.rand.NextBool(200) && !idleAnimation)
				{
					idleAnimation = true;
				}
				if (idleAnimation)
				{
					base.Projectile.frameCounter++;
					if (base.Projectile.frameCounter > 8)
					{
						base.Projectile.frame++;
						base.Projectile.frameCounter = 0;
					}
					if (base.Projectile.frame > 4)
					{
						base.Projectile.frame = 0;
						idleAnimation = false;
					}
				}
				else
				{
					base.Projectile.frame = 0;
					base.Projectile.frameCounter = 0;
				}
			}
			else if (base.Projectile.velocity.Y > 0.3f && base.Projectile.position.Y != base.Projectile.oldPosition.Y)
			{
				base.Projectile.frame = 3;
				base.Projectile.frameCounter = 0;
			}
			else if (base.Projectile.velocity.X != 0f)
			{
				if (base.Projectile.frame < 4)
				{
					base.Projectile.frame = 4;
				}
				base.Projectile.frameCounter++;
				if ((float)base.Projectile.frameCounter > 7f - ((base.Projectile.velocity.X > 0f) ? base.Projectile.velocity.X : (0f - base.Projectile.velocity.X)))
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame > 8)
				{
					base.Projectile.frame = 4;
				}
			}
			if (base.Projectile.velocity.X > 0.8f)
			{
				base.Projectile.spriteDirection = 1;
			}
			else if (base.Projectile.velocity.X < -0.8f)
			{
				base.Projectile.spriteDirection = -1;
			}
		}
		else
		{
			if (!fly)
			{
				return;
			}
			float flySpeed = 0.5f;
			base.Projectile.tileCollide = false;
			Vector2 flyDirection = base.Projectile.Center;
			float horiPos = Main.player[base.Projectile.owner].position.X + (float)(Main.player[base.Projectile.owner].width / 2) - flyDirection.X;
			float vertiPos = Main.player[base.Projectile.owner].position.Y + (float)(Main.player[base.Projectile.owner].height / 2) - flyDirection.Y;
			vertiPos += (float)Main.rand.Next(-10, 21);
			horiPos += (float)Main.rand.Next(-10, 21);
			horiPos += 60f * (0f - (float)player.direction);
			vertiPos -= 60f;
			float playerDistance2 = (float)Math.Sqrt(horiPos * horiPos + vertiPos * vertiPos);
			if (playerDistance2 > 1200f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (playerDistance2 < 100f)
			{
				flySpeed = 0.5f;
				if (player.velocity.Y == 0f)
				{
					playerStill++;
				}
				else
				{
					playerStill = 0;
				}
				if (playerStill > 10 && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
				{
					fly = false;
					Projectile projectile = base.Projectile;
					projectile.velocity *= 0.2f;
					base.Projectile.tileCollide = true;
				}
			}
			if (playerDistance2 < 50f)
			{
				if (Math.Abs(base.Projectile.velocity.X) > 2f || Math.Abs(base.Projectile.velocity.Y) > 2f)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.9f;
				}
				flySpeed = 0.02f;
			}
			else
			{
				if (playerDistance2 < 100f)
				{
					flySpeed = 0.35f;
				}
				if (playerDistance2 > 300f)
				{
					flySpeed = 1f;
				}
				playerDistance2 = 18f / playerDistance2;
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
			base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
			if (base.Projectile.Center.X < Main.player[base.Projectile.owner].Center.X)
			{
				base.Projectile.spriteDirection = 1;
			}
			else if (base.Projectile.Center.X > Main.player[base.Projectile.owner].Center.X)
			{
				base.Projectile.spriteDirection = -1;
			}
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 4)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 11)
			{
				base.Projectile.frame = 9;
			}
		}
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
				if (Main.tile[x, y].HasTile && (Main.tile[x - 1, y].HasTile || Main.tile[x + 1, y].HasTile))
				{
					return false;
				}
			}
		}
		return true;
	}
}
