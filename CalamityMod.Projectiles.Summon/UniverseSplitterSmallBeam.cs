using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class UniverseSplitterSmallBeam : ModProjectile, ILocalizedModType, IModType
{
	public const int FadeinTime = 25;

	public const int MovementTime = 60;

	public const int TimeLeft = 120;

	public const float TotalSweeps = 4f;

	public const float MaximumLength = 3000f;

	public const float LaserSize = 1.2f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 12000;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 120;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public static float CosineInterpolation(float start, float end, float time)
	{
		float cosineTime = (1f - (float)Math.Cos((float)Math.PI * time)) * 0.5f;
		return start * (1f - cosineTime) + end * cosineTime;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] <= 60f)
		{
			base.Projectile.velocity = (base.Projectile.ai[0] + (float)Math.Cos(base.Projectile.localAI[0] / 60f * 4f) * CosineInterpolation(1f, 0f, base.Projectile.localAI[0] / 60f) * ((float)Math.PI * 2f / 15f)).ToRotationVector2();
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] < 25f)
		{
			base.Projectile.scale = MathHelper.Lerp(0.01f, 1.2f, base.Projectile.localAI[0] / 25f);
		}
		if (base.Projectile.localAI[0] > 60f && base.Projectile.localAI[0] <= 70f)
		{
			base.Projectile.scale = MathHelper.Lerp(1.2f, 3.25f, (base.Projectile.localAI[0] - 60f) / 10f);
		}
		if (base.Projectile.localAI[0] > 105f)
		{
			base.Projectile.scale = MathHelper.Lerp(3.25f, 0.01f, (base.Projectile.localAI[0] - 105f) / 15f);
		}
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], 3000f, 0.5f);
		_ = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 6f);
		Color white = Color.White;
		DelegateMethods.v3_1 = ((Color)(ref white)).ToVector3();
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D laserTailTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UniverseSplitterSmallBeamBegin", (AssetRequestMode)1).Value;
		Texture2D laserBodyTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UniverseSplitterSmallBeamMid", (AssetRequestMode)1).Value;
		Texture2D laserHeadTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UniverseSplitterSmallBeamEnd", (AssetRequestMode)1).Value;
		float laserLength = base.Projectile.localAI[1];
		Color drawColor = new Color(1f, 1f, 1f) * 0.9f;
		Main.EntitySpriteDraw(laserTailTexture, base.Projectile.Center - Main.screenPosition, null, drawColor, base.Projectile.rotation, laserTailTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		laserLength -= (float)(laserTailTexture.Height / 2 + laserHeadTexture.Height) * base.Projectile.scale;
		Vector2 centerDelta = base.Projectile.Center;
		centerDelta += base.Projectile.velocity * base.Projectile.scale * (float)laserTailTexture.Height / 2f;
		if (laserLength > 0f)
		{
			float laserLengthDelta = 0f;
			Rectangle sourceRectangle = default(Rectangle);
			((Rectangle)(ref sourceRectangle))._002Ector(0, 14 * (base.Projectile.timeLeft / 3 % 5), laserBodyTexture.Width, 16);
			while (laserLengthDelta + 1f < laserLength)
			{
				if (laserLength - laserLengthDelta < (float)sourceRectangle.Height)
				{
					sourceRectangle.Height = (int)(laserLength - laserLengthDelta);
				}
				Main.EntitySpriteDraw(laserBodyTexture, centerDelta - Main.screenPosition, sourceRectangle, drawColor, base.Projectile.rotation, new Vector2((float)sourceRectangle.Width / 2f, 0f), base.Projectile.scale, (SpriteEffects)0);
				laserLengthDelta += (float)sourceRectangle.Height * base.Projectile.scale;
				centerDelta += base.Projectile.velocity * (float)sourceRectangle.Height * base.Projectile.scale;
				sourceRectangle.Y += 14;
				if (sourceRectangle.Y + sourceRectangle.Height > laserBodyTexture.Height)
				{
					sourceRectangle.Y = 0;
				}
			}
		}
		Main.EntitySpriteDraw(laserHeadTexture, centerDelta - Main.screenPosition, null, drawColor, base.Projectile.rotation, laserHeadTexture.Frame().Top(), base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CutTiles);
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
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float value = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], 22f * base.Projectile.scale, ref value))
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(144, 180);
	}
}
