using System;
using System.Linq;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class MountedScannerLaser : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public int OwnerIndex
	{
		get
		{
			return Projectile.GetByUUID(base.Projectile.owner, base.Projectile.ai[1]);
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public override float MaxScale => 0.5f + (float)Math.Cos(Main.GlobalTimeWrappedHourly * 10f) * 0.07f;

	public override float MaxLaserLength => 900f;

	public override float Lifetime => 70f;

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return Color.Red * 0.6f;
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

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayMid", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UltimaRayEnd", (AssetRequestMode)1).Value;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 17);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.DamageType = DamageClass.Summon;
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
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (OwnerIndex == -1)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, Main.projectile[OwnerIndex].rotation.ToRotationVector2().RotatedBy(Math.Cos(base.Time / 25f) * 0.05000000074505806), 0.125f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void AttachToSomething()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (OwnerIndex == -1)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = Main.projectile[OwnerIndex].Center + Main.projectile[OwnerIndex].rotation.ToRotationVector2() * 18f;
		if (Main.projectile[OwnerIndex].localAI[0] == 0f)
		{
			base.Projectile.Kill();
		}
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void ExtraBehavior()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			Vector2 laserSpawnPosition = base.Projectile.Center + base.Projectile.velocity * (base.LaserLength - 14f);
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustPerfect(laserSpawnPosition + Main.rand.NextVector2Circular(8f, 8f), 261);
				dust.velocity = Main.rand.NextVector2CircularEdge(9f, 9f) * Main.rand.NextFloat(0.3f, 1f);
				dust.color = Color.Lerp(Color.Crimson, Color.Red, Main.rand.NextFloat());
				dust.noGravity = true;
			}
		}
	}
}
