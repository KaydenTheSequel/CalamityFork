using System;
using System.IO;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class ExoskeletonLaserCannon : ExoskeletonCannon
{
	public const int NormalLasersBeforeBeam = 6;

	public ref float ShootCounter => ref base.Projectile.localAI[0];

	public override int ShootRate
	{
		get
		{
			if (ShootCounter % 6f != 0f)
			{
				return 15;
			}
			return 150;
		}
	}

	public override float ShootSpeed => 19f;

	public override Vector2 OwnerRestingOffset
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return ExoskeletonCannon.HoverOffsetTable[base.HoverOffsetIndex];
		}
	}

	public override void ClampFirstLimbRotation(ref double limbRotation)
	{
		limbRotation = ExoskeletonCannon.RotationalClampTable[base.HoverOffsetIndex];
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(ShootCounter);
		base.SendExtraAI(writer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		ShootCounter = reader.ReadSingle();
		base.ReceiveExtraAI(reader);
	}

	public override void ShootAtTarget(NPC target, Vector2 shootDirection)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = CommonCalamitySounds.LaserCannonSound with
		{
			Volume = 0.2f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 24; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + shootDirection * (float)base.Projectile.width * base.Projectile.scale * 0.45f, 182);
			dust.velocity = ((float)Math.PI * 2f * (float)i / 24f).ToRotationVector2() * 4f;
			dust.scale = 1.1f;
			dust.fadeIn = 0.4f;
			dust.noGravity = true;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			int laserID = ModContent.ProjectileType<MinionLaserBurst>();
			bool fireLaser = ShootCounter % 6f == 5f;
			if (fireLaser)
			{
				laserID = ModContent.ProjectileType<CannonLaserbeam>();
			}
			Vector2 laserVelocity = shootDirection * ShootSpeed;
			int laser = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, laserVelocity, laserID, (int)((float)base.Projectile.damage * 1.1f), 0f, base.Projectile.owner);
			if (Main.projectile.IndexInRange(laser) && fireLaser)
			{
				Main.projectile[laser].ai[1] = base.Projectile.identity;
			}
			ShootCounter++;
			base.Projectile.netUpdate = true;
		}
	}

	public override void PostAI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		if (ShootCounter % 6f == 5f && base.TargetingSomething)
		{
			Vector2 aimDirection = base.Projectile.rotation.ToRotationVector2();
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + aimDirection * (float)base.Projectile.width * base.Projectile.scale * 0.51f, 182);
			dust.velocity = aimDirection.RotatedByRandom(0.47999998927116394) * Main.rand.NextFloat(3f);
			dust.scale = 1.1f;
			dust.fadeIn = 0.4f;
			dust.noGravity = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		DefaultDrawCannon(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SmallAresArms/ExoskeletonLaserCannonGlowmask", (AssetRequestMode)2).Value);
		return false;
	}
}
