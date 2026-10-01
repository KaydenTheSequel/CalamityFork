using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseFlailProjectile : ModProjectile
{
	public Texture2D FlailTexture => TextureAssets.Projectile[base.Type].Value;

	public virtual Color SpecialDrawColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 200, 0);
		}
	}

	public virtual int ExudeDustType => 244;

	public virtual int WhipDustType => 246;

	public virtual int HandleHeight => 54;

	public virtual int BodyType1SectionHeight => 18;

	public virtual int BodyType2SectionHeight => 18;

	public virtual int BodyType1StartY => 36;

	public virtual int BodyType2StartY => 58;

	public virtual int TailStartY => 90;

	public virtual int TailHeight => 52;

	public virtual void Behavior()
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.localAI[1] > 0f)
		{
			base.Projectile.localAI[1]--;
		}
		base.Projectile.alpha -= 42;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = base.Projectile.velocity.ToRotation();
		}
		float direction = (base.Projectile.localAI[0].ToRotationVector2().X >= 0f).ToDirectionInt();
		if (base.Projectile.ai[1] <= 0f)
		{
			direction *= -1f;
		}
		Vector2 velocityAdditive = (direction * (base.Projectile.ai[0] / 30f * ((float)Math.PI * 2f) - (float)Math.PI / 2f)).ToRotationVector2();
		velocityAdditive.Y *= (float)Math.Sin(base.Projectile.ai[1]);
		if (base.Projectile.ai[1] <= 0f)
		{
			velocityAdditive.Y *= -1f;
		}
		velocityAdditive = velocityAdditive.RotatedBy(base.Projectile.localAI[0]);
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] < 30f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity += 48f * velocityAdditive;
		}
		else
		{
			base.Projectile.Kill();
		}
		base.Projectile.position = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) - base.Projectile.Size / 2f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		Vector2 centerDelta = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
		if (player.direction != 1)
		{
			centerDelta.X = (float)player.bodyFrame.Width - centerDelta.X;
		}
		if (player.gravDir != 1f)
		{
			centerDelta.Y = (float)player.bodyFrame.Height - centerDelta.Y;
		}
		if (player.heldProj == -1)
		{
			player.heldProj = base.Projectile.whoAmI;
		}
		centerDelta -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
		base.Projectile.Center = player.RotatedRelativePoint(player.position + centerDelta, reverseRotation: true) - base.Projectile.velocity;
		if (base.Projectile.alpha == 0)
		{
			GenerateDust();
		}
	}

	public virtual void ExtraBehavior()
	{
	}

	public override void AI()
	{
		Behavior();
		ExtraBehavior();
	}

	public virtual void GenerateDust()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position + base.Projectile.velocity * 2f, base.Projectile.width, base.Projectile.height, ExudeDustType, 0f, 0f, 100, SpecialDrawColor, 2f);
			dust.noGravity = true;
			dust.velocity *= 2f;
			dust.velocity += base.Projectile.localAI[0].ToRotationVector2();
			dust.fadeIn = 1.5f;
		}
		float counterMax = 18f;
		for (int counter = 0; (float)counter < counterMax; counter++)
		{
			if (Main.rand.NextBool(4))
			{
				Dust dust2 = Dust.NewDustDirect(base.Projectile.position + base.Projectile.velocity + base.Projectile.velocity * ((float)counter / counterMax), base.Projectile.width, base.Projectile.height, WhipDustType, 0f, 0f, 100, SpecialDrawColor);
				dust2.noGravity = true;
				dust2.fadeIn = 0.5f;
				dust2.velocity += base.Projectile.localAI[0].ToRotationVector2();
				dust2.noLight = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		DrawHandleSprite(in lightColor);
		Vector2 normalizedVelocity = Vector2.Normalize(base.Projectile.velocity);
		float speed = ((Vector2)(ref base.Projectile.velocity)).Length() + 16f - 40f * base.Projectile.scale;
		Vector2 bodyDrawPosition = base.Projectile.Center.Floor() + normalizedVelocity * base.Projectile.scale * 20f;
		DrawType2BodySprite(in speed, in normalizedVelocity, in lightColor, ref bodyDrawPosition);
		bodyDrawPosition = base.Projectile.Center.Floor() + normalizedVelocity * base.Projectile.scale * 20f;
		DrawType1BodySprite(in speed, in normalizedVelocity, in lightColor, ref bodyDrawPosition);
		Vector2 whipEndPosition = bodyDrawPosition;
		DrawWhipTail(in whipEndPosition, in lightColor);
		return false;
	}

	public void DrawHandleSprite(in Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		Rectangle handleFrame = default(Rectangle);
		((Rectangle)(ref handleFrame))._002Ector(0, 0, FlailTexture.Width, HandleHeight);
		Main.EntitySpriteDraw(FlailTexture, base.Projectile.Center.Floor() - Main.screenPosition + Vector2.UnitY * Main.player[base.Projectile.owner].gfxOffY, handleFrame, lightColor, base.Projectile.rotation + (float)Math.PI, handleFrame.Size() / 2f - Vector2.UnitY * 4f, base.Projectile.scale, (SpriteEffects)0);
	}

	public void DrawType1BodySprite(in float speed, in Vector2 normalizedVelocity, in Color lightColor, ref Vector2 bodyDrawPosition)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		Rectangle type1BodyFrame = default(Rectangle);
		((Rectangle)(ref type1BodyFrame))._002Ector(0, BodyType1StartY, FlailTexture.Width, BodyType1SectionHeight);
		int type1BodyDrawCount = ((speed < 100f) ? 22 : 9);
		if (!(speed > 0f))
		{
			return;
		}
		float speedRatio = speed / (float)type1BodyDrawCount;
		bodyDrawPosition += normalizedVelocity * speedRatio * 0.25f;
		for (int i = 0; i < type1BodyDrawCount; i++)
		{
			float drawPositionDeltaMult = speedRatio;
			if (i == 0)
			{
				drawPositionDeltaMult *= 0.75f;
			}
			Main.EntitySpriteDraw(FlailTexture, bodyDrawPosition - Main.screenPosition + Vector2.UnitY * Main.player[base.Projectile.owner].gfxOffY, type1BodyFrame, lightColor, base.Projectile.rotation + (float)Math.PI, new Vector2((float)(type1BodyFrame.Width / 2), 0f), base.Projectile.scale, (SpriteEffects)0);
			bodyDrawPosition += normalizedVelocity * drawPositionDeltaMult;
		}
	}

	public void DrawType2BodySprite(in float speed, in Vector2 normalizedVelocity, in Color lightColor, ref Vector2 bodyDrawPosition)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		Rectangle type2BodyFrame = default(Rectangle);
		((Rectangle)(ref type2BodyFrame))._002Ector(0, BodyType2StartY, FlailTexture.Width, BodyType2SectionHeight);
		if (!(speed > 0f))
		{
			return;
		}
		float counter = 0f;
		while (counter + 1f < speed)
		{
			if (speed - counter < (float)type2BodyFrame.Height)
			{
				type2BodyFrame.Height = (int)(speed - counter);
			}
			Main.EntitySpriteDraw(FlailTexture, bodyDrawPosition - Main.screenPosition + Vector2.UnitY * Main.player[base.Projectile.owner].gfxOffY, type2BodyFrame, lightColor, base.Projectile.rotation + (float)Math.PI, new Vector2((float)(type2BodyFrame.Width / 2), 0f), base.Projectile.scale, (SpriteEffects)0);
			counter += (float)type2BodyFrame.Height * base.Projectile.scale;
			bodyDrawPosition += normalizedVelocity * (float)type2BodyFrame.Height * base.Projectile.scale;
		}
	}

	public void DrawWhipTail(in Vector2 whipEndPosition, in Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		Rectangle tailFrame = default(Rectangle);
		((Rectangle)(ref tailFrame))._002Ector(0, TailStartY, FlailTexture.Width, TailHeight);
		Main.EntitySpriteDraw(FlailTexture, whipEndPosition - Main.screenPosition + Vector2.UnitY * Main.player[base.Projectile.owner].gfxOffY, tailFrame, lightColor, base.Projectile.rotation + (float)Math.PI, FlailTexture.Frame().Top(), base.Projectile.scale, (SpriteEffects)0);
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit, (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CutTiles);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float _ = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity, 16f * base.Projectile.scale, ref _))
		{
			return true;
		}
		return false;
	}
}
