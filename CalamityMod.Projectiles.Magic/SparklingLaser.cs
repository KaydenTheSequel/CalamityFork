using System;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class SparklingLaser : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public bool playedSound;

	public const int ChargeupTime = 50;

	private const float AimResponsiveness = 0.8f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override Color LightCastColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(204, 204, 255);
		}
	}

	public override float Lifetime => 18000f;

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 1600f;

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/SparklingLaserBegin", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/SparklingLaserMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/SparklingLaserEnd", (AssetRequestMode)1).Value;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Projectile.type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 18000;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 20;
	}

	public override void DetermineScale()
	{
		base.Projectile.scale = ((base.Time < 50f) ? 0f : MaxScale);
	}

	public override float DetermineLaserLength()
	{
		return DetermineLaserLength_CollideWithTiles();
	}

	public override bool PreAI()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 rrp = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
			UpdateAim(rrp);
			base.Projectile.direction = ((Main.MouseWorld.X > Owner.Center.X) ? 1 : (-1));
			base.Projectile.netUpdate = true;
		}
		int dir = base.Projectile.direction;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.Center = Owner.Center + base.Projectile.velocity * 56f;
		base.Projectile.timeLeft = 18000;
		Owner.ChangeDir(dir);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = ((base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * (float)(-Owner.direction)).ToRotation();
		if (!Owner.channel)
		{
			base.Projectile.Kill();
			return false;
		}
		if (Owner.miscCounter % 10 == 0 && !Owner.CheckMana(Owner.HeldItem, -1, pay: true))
		{
			base.Projectile.Kill();
			return false;
		}
		if (base.Time < 50f)
		{
			int dustCount = (int)(base.Time / 20f);
			Vector2 spawnPos = base.Projectile.Center;
			for (int k = 0; k < dustCount + 1; k++)
			{
				Dust dust = Dust.NewDustDirect(spawnPos, 1, 1, 226, base.Projectile.velocity.X / 2f, base.Projectile.velocity.Y / 2f);
				dust.position += Main.rand.NextVector2Square(-10f, 10f);
				dust.velocity = Main.rand.NextVector2Unit() * (10f - (float)dustCount * 2f) / 10f;
				dust.scale = Main.rand.NextFloat(0.5f, 1f);
				dust.noGravity = true;
			}
			base.Time++;
			return false;
		}
		if (!playedSound)
		{
			SoundEngine.PlaySound(in SoundID.Item68, base.Projectile.Center);
			playedSound = true;
		}
		return true;
	}

	public override void PostAI()
	{
		base.Projectile.frameCounter++;
		if ((float)base.Projectile.frameCounter % 5f == 4f)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Projectile.type];
		}
	}

	private void UpdateAim(Vector2 source)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimVector = Vector2.Normalize(Main.MouseWorld - source);
		if (aimVector.HasNaNs())
		{
			aimVector = -Vector2.UnitY;
		}
		aimVector = Vector2.Normalize(Vector2.Lerp(aimVector, Vector2.Normalize(base.Projectile.velocity), 0.8f));
		if (aimVector != base.Projectile.velocity)
		{
			base.Projectile.netUpdate = true;
		}
		base.Projectile.velocity = aimVector;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return base.Time >= 50f;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * base.LaserLength, base.Projectile.width + 16, DelegateMethods.CutTiles);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		if (target.life > 0 || target.lifeMax <= 5 || base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		int shardDamage = base.Projectile.damage / 5;
		int shardAmt = Main.rand.Next(2, 4);
		for (int s = 0; s < shardAmt; s++)
		{
			Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
			int shard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, velocity, ModContent.ProjectileType<AquashardSplit>(), shardDamage, 0f, base.Projectile.owner);
			if (shard.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[shard].DamageType = DamageClass.Magic;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero || base.Time < 50f)
		{
			return false;
		}
		Color beamColor = LaserOverlayColor;
		Rectangle startFrameArea = LaserBeginTexture.Frame(1, Main.projFrames[base.Projectile.type], 0, base.Projectile.frame);
		Rectangle middleFrameArea = LaserMiddleTexture.Frame(1, Main.projFrames[base.Projectile.type], 0, base.Projectile.frame);
		Rectangle endFrameArea = LaserEndTexture.Frame(1, Main.projFrames[base.Projectile.type], 0, base.Projectile.frame);
		Vector2 laserBeginCenter = base.Projectile.Center - Main.screenPosition + base.Projectile.velocity * base.Projectile.scale * 50f;
		Main.EntitySpriteDraw(LaserBeginTexture, laserBeginCenter, startFrameArea, beamColor, base.Projectile.rotation, LaserBeginTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		float laserBodyLength = base.LaserLength;
		laserBodyLength -= (float)(startFrameArea.Height / 2 + endFrameArea.Height) * base.Projectile.scale;
		Vector2 centerOnLaser = base.Projectile.Center - Main.screenPosition;
		centerOnLaser += base.Projectile.velocity * base.Projectile.scale * 10.5f;
		if (laserBodyLength > 0f)
		{
			float laserOffset = (float)middleFrameArea.Height * base.Projectile.scale;
			float incrementalBodyLength = 0f;
			while (incrementalBodyLength + 1f < laserBodyLength)
			{
				centerOnLaser += base.Projectile.velocity * laserOffset;
				incrementalBodyLength += laserOffset;
				Main.EntitySpriteDraw(LaserMiddleTexture, centerOnLaser, middleFrameArea, beamColor, base.Projectile.rotation, (float)LaserMiddleTexture.Width * 0.5f * Vector2.UnitX, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		if (Math.Abs(base.LaserLength - DetermineLaserLength()) < 30f)
		{
			Main.EntitySpriteDraw(LaserEndTexture, centerOnLaser, endFrameArea, beamColor, base.Projectile.rotation, LaserEndTexture.Frame().Top(), base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}
}
