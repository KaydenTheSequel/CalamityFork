using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class FlakPet : ModProjectile, ILocalizedModType, IModType
{
	private int playerStill;

	private bool fly;

	private int idleTimer;

	private bool hiding;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(2, 4, 6).WithOffset(-7f, 0f).WithSpriteDirection(-1)
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
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.flakPet = false;
		}
		if (modPlayer.flakPet)
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
				if (!hiding)
				{
					idleTimer++;
					base.Projectile.frame = 2;
				}
				if (idleTimer > 300)
				{
					hiding = true;
					if (base.Projectile.frame > 0)
					{
						base.Projectile.frameCounter++;
						if (base.Projectile.frameCounter > 3)
						{
							base.Projectile.frame--;
							base.Projectile.frameCounter = 0;
						}
						if (base.Projectile.frame < 0)
						{
							base.Projectile.frame = 0;
						}
					}
				}
			}
			else if (base.Projectile.velocity.Y > 0.3f && base.Projectile.position.Y != base.Projectile.oldPosition.Y)
			{
				idleTimer = 0;
				hiding = false;
				base.Projectile.frame = 2;
				base.Projectile.frameCounter = 0;
			}
			else
			{
				idleTimer = 0;
				hiding = false;
				base.Projectile.frameCounter++;
				if (base.Projectile.frameCounter > 6)
				{
					base.Projectile.frame++;
					base.Projectile.frameCounter = 0;
				}
				if (base.Projectile.frame > 5)
				{
					base.Projectile.frame = 2;
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
			base.Projectile.frame = 6;
			hiding = false;
			idleTimer = 0;
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
