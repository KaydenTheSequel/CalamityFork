using System;
using System.IO;
using System.Linq;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SeraphimBeamLarge : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public int OwnerIndex
	{
		get
		{
			return (int)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 1000f;

	public override float Lifetime => 50f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			Color goldenrod = Color.Goldenrod;
			Color c2 = Color.Orange;
			Color color = Color.Lerp(goldenrod, c2, (float)base.Projectile.identity % 5f / 5f) * 1.1f;
			((Color)(ref color)).A = 25;
			return color;
		}
	}

	public override Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Transparent;
		}
	}

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/SeraphimBeamLarge", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/SeraphimBeamLargeMiddle", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/SeraphimBeamLargeEnd", (AssetRequestMode)1).Value;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
		writer.Write(base.Projectile.scale);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		base.Projectile.scale = reader.ReadSingle();
	}

	public override void AttachToSomething()
	{
	}

	public override void UpdateLaserMotion()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override float DetermineLaserLength()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		float[] sampledLengths = new float[10];
		Collision.LaserScan(base.Projectile.Center, base.Projectile.velocity, (float)base.Projectile.width * base.Projectile.scale, MaxLaserLength, sampledLengths);
		float newLaserLength = sampledLengths.Average();
		if (Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			return MaxLaserLength;
		}
		return newLaserLength;
	}

	public override void PostAI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.frameCounter == 0)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, base.Projectile.Center);
		}
		base.Projectile.frameCounter++;
		if ((float)base.Projectile.frameCounter % 5f == 4f)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.damage = Math.Max(1, (int)((double)base.Projectile.damage * 0.3));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Color beamColor = LaserOverlayColor;
		Rectangle startFrameArea = LaserBeginTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Rectangle middleFrameArea = LaserMiddleTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Rectangle endFrameArea = LaserEndTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 centerOnLaser = base.Projectile.Center + base.Projectile.velocity * base.Projectile.scale * 116f;
		Main.EntitySpriteDraw(LaserBeginTexture, centerOnLaser - Main.screenPosition, startFrameArea, beamColor, base.Projectile.rotation, LaserBeginTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		float laserBodyLength = base.LaserLength + (float)middleFrameArea.Height;
		if (laserBodyLength > 0f)
		{
			float laserOffset = (float)middleFrameArea.Height * base.Projectile.scale;
			float incrementalBodyLength = 0f;
			while (incrementalBodyLength + 1f < laserBodyLength)
			{
				centerOnLaser += base.Projectile.velocity * laserOffset;
				incrementalBodyLength += laserOffset;
				Main.EntitySpriteDraw(LaserMiddleTexture, centerOnLaser - Main.screenPosition, middleFrameArea, beamColor, base.Projectile.rotation, LaserMiddleTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		Vector2 laserEndCenter = centerOnLaser + base.Projectile.velocity * (float)endFrameArea.Height * base.Projectile.scale - Main.screenPosition;
		Main.EntitySpriteDraw(LaserEndTexture, laserEndCenter, endFrameArea, beamColor, base.Projectile.rotation, LaserEndTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
