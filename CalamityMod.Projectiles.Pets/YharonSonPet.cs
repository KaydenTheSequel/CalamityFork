using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class YharonSonPet : ModProjectile, ILocalizedModType, IModType
{
	private static int xFrameAmt = 3;

	private static int yFrameAmt = 16;

	public int frameX;

	public int frameY;

	public new string LocalizationCategory => "Projectiles.Pets";

	public Player player => Main.player[base.Projectile.owner];

	public int CurrentFrame
	{
		get
		{
			return frameX * yFrameAmt + frameY;
		}
		set
		{
			frameX = value / yFrameAmt;
			frameY = value % yFrameAmt;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(1, 1, int.MaxValue).WithOffset(-55f, 2f).WithSpriteDirection(-1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 104;
		base.Projectile.height = 82;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.frameCounter <= 1)
		{
			return false;
		}
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = value.Size() / new Vector2((float)xFrameAmt, (float)yFrameAmt) * 0.5f;
		Rectangle frame = value.Frame(xFrameAmt, yFrameAmt, frameX, frameY);
		Main.EntitySpriteDraw(effects: (SpriteEffects)(base.Projectile.spriteDirection != 1), texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, color: lightColor, rotation: base.Projectile.rotation, origin: origin, scale: base.Projectile.scale);
		return false;
	}

	private bool CurrentlySitting()
	{
		if (CurrentFrame != 0 && CurrentFrame != 3 && CurrentFrame != 4)
		{
			return CurrentFrame >= 19;
		}
		return true;
	}

	private bool CurrentlyFlying()
	{
		if (CurrentFrame != 1 && CurrentFrame != 2)
		{
			if (CurrentFrame >= 5)
			{
				return CurrentFrame <= 18;
			}
			return false;
		}
		return true;
	}

	private void SittingFrames()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 6 == 0)
		{
			if (CurrentlyFlying())
			{
				CurrentFrame = 3;
			}
			else if (CurrentFrame == 45)
			{
				CurrentFrame = 0;
			}
			else if (CurrentFrame != 0)
			{
				CurrentFrame++;
			}
			if (CurrentFrame == 5)
			{
				CurrentFrame = 0;
			}
			if (Main.rand.NextBool(200) && CurrentFrame == 0)
			{
				CurrentFrame = 46;
			}
			if (Main.rand.NextBool(1500) && CurrentFrame == 0)
			{
				CurrentFrame = 19;
			}
			if (frameX >= xFrameAmt)
			{
				CurrentFrame = 0;
			}
		}
	}

	private void FlyingFrames()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 6 == 0)
		{
			if (CurrentlySitting())
			{
				CurrentFrame = 1;
			}
			else
			{
				CurrentFrame++;
			}
			if (CurrentFrame == 3)
			{
				CurrentFrame = 5;
			}
			if (CurrentFrame > 18)
			{
				CurrentFrame = 5;
			}
			if (frameX >= xFrameAmt)
			{
				CurrentFrame = 0;
			}
		}
	}

	private void PetDefaults()
	{
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.yharonPet = false;
		}
		if (modPlayer.yharonPet)
		{
			base.Projectile.timeLeft = 2;
		}
	}

	public override void AI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		PetDefaults();
		if (player.rocketDelay2 > 0)
		{
			base.Projectile.ai[0] = 1f;
		}
		float xDist = player.Center.X - base.Projectile.Center.X;
		float yDist = player.Center.Y - base.Projectile.Center.Y;
		Vector2 playerVector = default(Vector2);
		((Vector2)(ref playerVector))._002Ector(xDist, yDist);
		float playerDist = ((Vector2)(ref playerVector)).Length();
		if (playerDist > 2000f)
		{
			base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
			base.Projectile.netUpdate = true;
		}
		else if (playerDist > (CurrentlySitting() ? 300f : 200f))
		{
			if (yDist > 0f && base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y = 0f;
			}
			if (yDist < 0f && base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y = 0f;
			}
			base.Projectile.ai[0] = 1f;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			FlyingPetAI();
		}
		else
		{
			SitDownAttempt();
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		DontSitInMidair();
	}

	private void DontSitInMidair()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (!CurrentlySitting())
		{
			return;
		}
		bool noSolidGround = true;
		for (int i = (int)base.Projectile.BottomLeft.X / 16; i < (int)base.Projectile.BottomRight.X / 16; i++)
		{
			if (CalamityUtils.ParanoidTileRetrieval(i, (int)(base.Projectile.Bottom.Y / 16f)).IsTileSolidGround())
			{
				noSolidGround = false;
				break;
			}
		}
		if (noSolidGround)
		{
			base.Projectile.ai[0] = 1f;
		}
	}

	private void FlyingPetAI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		float passiveMvtFloat = 0.2f;
		base.Projectile.tileCollide = false;
		float range = 200f;
		float xDist = player.Center.X - base.Projectile.Center.X;
		float yDist = player.Center.Y - base.Projectile.Center.Y;
		yDist += Main.rand.NextFloat(-10f, 20f);
		xDist += Main.rand.NextFloat(-10f, 20f);
		xDist += 60f * (0f - (float)player.direction);
		yDist -= 60f;
		Vector2 playerVector = default(Vector2);
		((Vector2)(ref playerVector))._002Ector(xDist, yDist);
		float playerDist = ((Vector2)(ref playerVector)).Length();
		float returnSpeed = 12f;
		if (playerDist < range && player.velocity.Y == 0f && base.Projectile.Bottom.Y <= player.Bottom.Y && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.ai[0] = 0f;
			if (base.Projectile.velocity.Y < -6f)
			{
				base.Projectile.velocity.Y = -6f;
			}
		}
		if (playerDist < 60f)
		{
			playerVector.X = base.Projectile.velocity.X;
			playerVector.Y = base.Projectile.velocity.Y;
		}
		else
		{
			playerDist = returnSpeed / playerDist;
			playerVector.X *= playerDist;
			playerVector.Y *= playerDist;
		}
		if (base.Projectile.velocity.X < playerVector.X)
		{
			base.Projectile.velocity.X += passiveMvtFloat;
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X += passiveMvtFloat * 1.5f;
			}
		}
		if (base.Projectile.velocity.X > playerVector.X)
		{
			base.Projectile.velocity.X -= passiveMvtFloat;
			if (base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X -= passiveMvtFloat * 1.5f;
			}
		}
		if (base.Projectile.velocity.Y < playerVector.Y)
		{
			base.Projectile.velocity.Y += passiveMvtFloat;
			if (base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y += passiveMvtFloat * 1.5f;
			}
		}
		if (base.Projectile.velocity.Y > playerVector.Y)
		{
			base.Projectile.velocity.Y -= passiveMvtFloat;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y -= passiveMvtFloat * 1.5f;
			}
		}
		if (base.Projectile.velocity.X >= 0.5f)
		{
			base.Projectile.direction = -1;
		}
		else if (base.Projectile.velocity.X < -0.5f)
		{
			base.Projectile.direction = 1;
		}
		else if (player.Center.X < base.Projectile.Center.X)
		{
			base.Projectile.direction = 1;
		}
		else if (player.Center.X > base.Projectile.Center.X)
		{
			base.Projectile.direction = -1;
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.075f;
		FlyingFrames();
	}

	private void SitDownAttempt()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.tileCollide = true;
		bool tooRight = false;
		bool tooLeft = false;
		bool abovePlayer = false;
		float xAdjust = 0.08f;
		float xVelMax = 6.5f;
		if (player.Center.X < base.Projectile.Center.X - 85f)
		{
			tooRight = true;
		}
		else if (player.Center.X > base.Projectile.Center.X + 85f)
		{
			tooLeft = true;
		}
		if (tooRight)
		{
			if (base.Projectile.velocity.X > -3.5f)
			{
				base.Projectile.velocity.X -= xAdjust;
			}
			else
			{
				base.Projectile.velocity.X -= xAdjust * 0.25f;
			}
		}
		else if (tooLeft)
		{
			if (base.Projectile.velocity.X < 3.5f)
			{
				base.Projectile.velocity.X += xAdjust;
			}
			else
			{
				base.Projectile.velocity.X += xAdjust * 0.25f;
			}
		}
		else
		{
			base.Projectile.velocity.X *= 0.9f;
			if (base.Projectile.velocity.X >= 0f - xAdjust && base.Projectile.velocity.X <= xAdjust)
			{
				base.Projectile.velocity.X = 0f;
			}
		}
		base.Projectile.velocity.X *= 0.95f;
		if (base.Projectile.velocity.X > -0.1f && base.Projectile.velocity.X < 0.1f)
		{
			base.Projectile.velocity.X = 0f;
		}
		if (player.Bottom.Y - 8f > base.Projectile.Bottom.Y)
		{
			abovePlayer = true;
		}
		Collision.StepUp(ref base.Projectile.position, ref base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ref base.Projectile.stepSpeed, ref base.Projectile.gfxOffY);
		if (base.Projectile.velocity.Y == 0f && !abovePlayer && base.Projectile.velocity.X != 0f)
		{
			int i = (int)base.Projectile.Center.X / 16;
			int j = (int)base.Projectile.Center.Y / 16 + 1;
			if (tooRight)
			{
				i--;
			}
			if (tooLeft)
			{
				i++;
			}
			WorldGen.SolidTile(i, j);
		}
		if (base.Projectile.velocity.X > xVelMax)
		{
			base.Projectile.velocity.X = xVelMax;
		}
		if (base.Projectile.velocity.X < 0f - xVelMax)
		{
			base.Projectile.velocity.X = 0f - xVelMax;
		}
		bool sitting = base.Projectile.position.X == base.Projectile.oldPosition.X;
		if ((base.Projectile.velocity.Y == 0f) & sitting)
		{
			if (player.Center.X < base.Projectile.Center.X)
			{
				base.Projectile.direction = 1;
			}
			else if (player.Center.X > base.Projectile.Center.X)
			{
				base.Projectile.direction = -1;
			}
			base.Projectile.rotation = 0f;
			SittingFrames();
			return;
		}
		if (base.Projectile.velocity.X >= 0.5f)
		{
			base.Projectile.direction = -1;
		}
		else if (base.Projectile.velocity.X < -0.5f)
		{
			base.Projectile.direction = 1;
		}
		else if (player.Center.X < base.Projectile.Center.X)
		{
			base.Projectile.direction = 1;
		}
		else if (player.Center.X > base.Projectile.Center.X)
		{
			base.Projectile.direction = -1;
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.075f;
		FlyingFrames();
	}
}
