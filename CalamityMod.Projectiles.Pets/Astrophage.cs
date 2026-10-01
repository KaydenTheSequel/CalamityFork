using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class Astrophage : ModProjectile, ILocalizedModType, IModType
{
	private bool fly;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Type], 6).WithOffset(-10f, 0f).WithSpriteDirection(-1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 35;
		base.Projectile.height = 35;
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
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.astrophage = false;
		}
		if (modPlayer.astrophage)
		{
			base.Projectile.timeLeft = 2;
		}
		if (base.Projectile.position.X == base.Projectile.oldPosition.X && base.Projectile.position.Y == base.Projectile.oldPosition.Y && base.Projectile.velocity.X == 0f)
		{
			base.Projectile.frame = 0;
		}
		else if (base.Projectile.velocity.Y > 0.3f && base.Projectile.position.Y != base.Projectile.oldPosition.Y)
		{
			base.Projectile.frame = 1;
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
			if (base.Projectile.frame > 4)
			{
				base.Projectile.frame = 0;
			}
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
		}
		else if (fly)
		{
			base.Projectile.alpha += 15;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.position.X = player.position.X;
				base.Projectile.position.Y = player.position.Y;
				fly = false;
				base.Projectile.alpha = 0;
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
