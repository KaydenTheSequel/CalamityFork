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

public class AndromedaDeathRay : ModProjectile, ILocalizedModType, IModType
{
	public const int TrueTimeLeft = 25;

	private const float maximumLength = 2000f;

	public float AngularMultiplier;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 25;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
		writer.Write(AngularMultiplier);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		AngularMultiplier = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		Projectile owner = Main.projectile[(int)base.Projectile.ai[0]];
		Player player = Main.player[base.Projectile.owner];
		if (owner.type != ModContent.ProjectileType<GiantIbanRobotOfDoom>() || !owner.active)
		{
			base.Projectile.Kill();
		}
		if (owner.active)
		{
			base.Projectile.Center = player.Center + new Vector2((owner.spriteDirection == 1) ? 48f : 22f, player.gravDir * -28f);
			if (player.Calamity().andromedaState == AndromedaPlayerState.SmallRobot)
			{
				base.Projectile.Center = player.Center + new Vector2((owner.spriteDirection == 1) ? 24f : 2f, 0f);
			}
		}
		float laserSize = 0.6f + (float)Math.Sin(base.Projectile.localAI[0] / 7f) * 0.025f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= 25f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / 25f) * 5f * laserSize;
		if (base.Projectile.scale > laserSize)
		{
			base.Projectile.scale = laserSize;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		Color newColor;
		if (!Main.dedServ)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 center = base.Projectile.Center;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(center, 133, null, 0, newColor);
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(2f, 3.5f) + Main.player[base.Projectile.owner].velocity * 1.5f;
				dust.fadeIn = Main.rand.NextFloat(0.6f, 0.85f);
				if (player.Calamity().andromedaState == AndromedaPlayerState.SmallRobot)
				{
					dust.fadeIn = 0.05f;
					dust.scale = 0.8f;
				}
				dust.noGravity = true;
			}
		}
		Vector2 center2 = base.Projectile.Center;
		float[] samples = new float[3];
		float determinedLength = 0f;
		Collision.LaserScan(center2, base.Projectile.velocity, (float)base.Projectile.width * base.Projectile.scale, 2000f, samples);
		for (int j = 0; j < samples.Length; j++)
		{
			determinedLength += samples[j];
		}
		determinedLength /= (float)samples.Length;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], determinedLength, 0.5f);
		Vector2 beamEndPosiiton = base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1];
		if (Collision.SolidCollision(beamEndPosiiton, 16, 16) && !Main.dedServ)
		{
			if (AngularMultiplier == 0f)
			{
				AngularMultiplier = Main.rand.NextFloat(1f, 4f);
			}
			float angle = base.Projectile.localAI[0] / 18f;
			float x = (float)Math.Sin(angle * AngularMultiplier) * (float)Math.Cos(angle);
			float y = (float)Math.Cos(angle * AngularMultiplier) * (float)Math.Sin(angle);
			Vector2 velocity = default(Vector2);
			((Vector2)(ref velocity))._002Ector(x * 4.5f, y * 2f);
			for (int k = 0; k < 3; k++)
			{
				Vector2 position = beamEndPosiiton + angle.ToRotationVector2() * 8f;
				newColor = default(Color);
				Dust dust2 = Dust.NewDustPerfect(position, 133, null, 0, newColor);
				dust2.velocity = velocity.RotatedBy((float)Math.PI * 2f / 3f * (float)k);
				dust2.scale = (float)Math.Cos(angle) + 1.2f;
				dust2.noGravity = true;
			}
		}
		newColor = Color.SkyBlue;
		DelegateMethods.v3_1 = ((Color)(ref newColor)).ToVector3();
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
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D laserTailTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AndromedaDeathrayBegin", (AssetRequestMode)1).Value;
		Texture2D laserBodyTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AndromedaDeathrayMid", (AssetRequestMode)1).Value;
		Texture2D laserHeadTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AndromedaDeathrayEnd", (AssetRequestMode)1).Value;
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
			((Rectangle)(ref sourceRectangle))._002Ector(0, 0, laserBodyTexture.Width, laserBodyTexture.Height);
			while (laserLengthDelta + 1f < laserLength)
			{
				if (laserLength - laserLengthDelta < (float)sourceRectangle.Height)
				{
					sourceRectangle.Height = (int)(laserLength - laserLengthDelta);
				}
				Main.EntitySpriteDraw(laserBodyTexture, centerDelta - Main.screenPosition, sourceRectangle, drawColor, base.Projectile.rotation, new Vector2((float)sourceRectangle.Width / 2f, 0f), base.Projectile.scale, (SpriteEffects)0);
				laserLengthDelta += (float)sourceRectangle.Height * base.Projectile.scale;
				centerDelta += base.Projectile.velocity * (float)sourceRectangle.Height * base.Projectile.scale;
				sourceRectangle.Y += 16;
				if (sourceRectangle.Y + sourceRectangle.Height > laserBodyTexture.Height)
				{
					sourceRectangle.Y = 0;
				}
			}
		}
		Main.EntitySpriteDraw(laserHeadTexture, centerDelta - Main.screenPosition, null, drawColor, base.Projectile.rotation, laserHeadTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
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
}
