using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class LaserShot : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public NPC struckNPC;

	public new string LocalizationCategory => "Projectiles.Misc";

	public bool DoneHitting
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = value.ToInt();
		}
	}

	public override string Texture => "CalamityMod/ExtraTextures/Lasers/TurretLaserStart";

	public override float MaxScale => 1f;

	public override float MaxLaserLength => 1030f;

	public override float Lifetime => 20f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Transparent;
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

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/TurretLaserStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/TurretLaserMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/TurretLaserEnd", (AssetRequestMode)1).Value;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 100;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 450;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool PreAI()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.knockBack == 0f)
		{
			base.Projectile.hostile = true;
		}
		else
		{
			base.Projectile.friendly = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 5 == 4)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundStyle style = CommonCalamitySounds.LaserCannonSound with
			{
				Volume = 0.35f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.localAI[0]++;
		}
		return true;
	}

	public override float DetermineLaserLength()
	{
		return DetermineLaserLength_CollideWithTiles();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		Rectangle beginFrame = LaserBeginTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(LaserBeginTexture, base.Projectile.Center - Main.screenPosition, beginFrame, Color.White, base.Projectile.rotation, beginFrame.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		float laserBodyLength = base.LaserLength;
		laserBodyLength -= ((float)LaserBeginTexture.Height * 0.5f + (float)LaserEndTexture.Height) * base.Projectile.scale / (float)Main.projFrames[base.Type];
		Vector2 centerOnLaser = base.Projectile.Center;
		Rectangle middleFrame = LaserMiddleTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		if (laserBodyLength > 30f)
		{
			float laserOffset = ((float)LaserMiddleTexture.Height - 10f) * base.Projectile.scale / (float)Main.projFrames[base.Type];
			float incrementalBodyLength = 0f;
			while (incrementalBodyLength + 1f < laserBodyLength)
			{
				Main.EntitySpriteDraw(LaserMiddleTexture, centerOnLaser - Main.screenPosition, middleFrame, Color.White, base.Projectile.rotation, (float)middleFrame.Width * 0.5f * Vector2.UnitX, base.Projectile.scale, (SpriteEffects)0);
				incrementalBodyLength += laserOffset;
				centerOnLaser += base.Projectile.velocity * laserOffset;
			}
		}
		Rectangle endFrame = LaserEndTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 laserEndCenter = centerOnLaser - Main.screenPosition;
		Main.EntitySpriteDraw(LaserEndTexture, laserEndCenter, endFrame, Color.White, base.Projectile.rotation, endFrame.Size() * new Vector2(0.5f, 0f), base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
