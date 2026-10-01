using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class CannonLaserbeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public const int LifetimeConst = 75;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public Projectile OwnerProjectile => CalamityUtils.FindProjectileByIdentity((int)base.Projectile.ai[1], base.Projectile.owner);

	public override float MaxScale => 0.5f + (float)Math.Cos(Main.GlobalTimeWrappedHourly * 10f) * 0.07f;

	public override float MaxLaserLength => 1560f;

	public override float Lifetime => 75f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override Color LightCastColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return LaserOverlayColor;
		}
	}

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/ThanatosBeamStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ThanatosBeamMiddle", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ThanatosBeamEnd", (AssetRequestMode)1).Value;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 38);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.scale = 0.5f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.hide = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.Calamity().UpdatePriority = 1f;
	}

	public override float DetermineLaserLength()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		float[] samples = new float[4];
		Collision.LaserScan(base.Projectile.Center, base.Projectile.velocity, (float)base.Projectile.width * base.Projectile.scale, MaxLaserLength, samples);
		return samples.Average();
	}

	public override void UpdateLaserMotion()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		if (OwnerProjectile == null)
		{
			base.Projectile.Kill();
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		}
	}

	public override void AttachToSomething()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		Projectile ownerProjectile = OwnerProjectile;
		if (ownerProjectile == null)
		{
			base.Projectile.Kill();
			return;
		}
		float attachmentOffset = (float)ownerProjectile.width * ownerProjectile.scale * 0.75f;
		base.Projectile.Center = ownerProjectile.Center + ownerProjectile.rotation.ToRotationVector2() * attachmentOffset - Owner.velocity;
		base.Projectile.rotation = ownerProjectile.rotation;
		base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2();
		base.Projectile.frame = base.Projectile.frameCounter++ / 5 % Main.projFrames[base.Type];
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero || base.Projectile.localAI[0] < 2f)
		{
			return false;
		}
		Color beamColor = LaserOverlayColor;
		Rectangle startFrameArea = LaserBeginTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Rectangle middleFrameArea = LaserMiddleTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Rectangle endFrameArea = LaserEndTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(LaserBeginTexture, base.Projectile.Center - Main.screenPosition, startFrameArea, beamColor, base.Projectile.rotation, startFrameArea.Size() * new Vector2(0.5f, 1f), base.Projectile.scale, (SpriteEffects)0);
		float laserBodyLength = base.LaserLength + (float)middleFrameArea.Height;
		Vector2 centerOnLaser = base.Projectile.Center;
		if (laserBodyLength > 0f)
		{
			float laserOffset = (float)middleFrameArea.Height * base.Projectile.scale;
			float incrementalBodyLength = 0f;
			while (incrementalBodyLength + 1f < laserBodyLength)
			{
				Main.EntitySpriteDraw(LaserMiddleTexture, centerOnLaser - Main.screenPosition, middleFrameArea, beamColor, base.Projectile.rotation, middleFrameArea.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
				incrementalBodyLength += laserOffset;
				centerOnLaser += base.Projectile.velocity * laserOffset;
				middleFrameArea.Y += LaserMiddleTexture.Height / Main.projFrames[base.Type];
				if (middleFrameArea.Y + middleFrameArea.Height > LaserMiddleTexture.Height)
				{
					middleFrameArea.Y = 0;
				}
			}
		}
		Vector2 laserEndCenter = centerOnLaser - Main.screenPosition;
		Main.EntitySpriteDraw(LaserEndTexture, laserEndCenter, endFrameArea, beamColor, base.Projectile.rotation, endFrameArea.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
