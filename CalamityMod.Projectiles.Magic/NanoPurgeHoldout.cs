using System;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NanoPurgeHoldout : BaseGunHoldoutProjectile
{
	private const int FramesPerFireRateIncrease = 36;

	private static int[] LaserOffsetByAnimationFrame = new int[4] { 4, 3, 0, 3 };

	public float postShotFade;

	public override int AssociatedItemID => ModContent.ItemType<NanoPurge>();

	public override string Texture => "CalamityMod/Projectiles/Magic/NanoPurgeHoldout";

	public override float MaxOffsetLengthFromArm => 10f;

	public override float OffsetXUpwards => -15f;

	public override float OffsetXDownwards => 5f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYUpwards => 5f;

	public override float OffsetYDownwards => 10f;

	private ref float DeployedFrames => ref base.Projectile.ai[0];

	private ref float ChargeTowardsNextShot => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void KillHoldoutLogic()
	{
		base.KillHoldoutLogic();
		if (DeployedFrames >= (float)(base.HeldItem?.useAnimation ?? NanoPurge.UseTime) && !base.Owner.CheckMana(base.Owner.HeldItem))
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = ((base.HeldItem != null) ? base.Owner.GetWeaponDamage(base.HeldItem) : 0);
		int itemUseTime = base.HeldItem?.useAnimation ?? NanoPurge.UseTime;
		DeployedFrames++;
		int fireRate = (int)MathHelper.Clamp(DeployedFrames / 36f, 1f, 4f);
		ChargeTowardsNextShot += fireRate;
		if (ChargeTowardsNextShot >= (float)itemUseTime)
		{
			ChargeTowardsNextShot -= itemUseTime;
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			if (base.Owner.CheckMana(base.Owner.HeldItem, -1, pay: true))
			{
				postShotFade = 1f;
				SoundEngine.PlaySound(in SoundID.Item91, base.Projectile.Center);
				if (Main.myPlayer == base.Projectile.owner)
				{
					int projID = ModContent.ProjectileType<NanoPurgeLaser>();
					float shootSpeed = base.HeldItem.shootSpeed;
					float inaccuracyRatio = 0.045f;
					Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
					Vector2 perp = shootDirection.RotatedBy(1.5707963705062866);
					for (int i = -1; i <= 1; i += 2)
					{
						Vector2 spread = Main.rand.NextVector2CircularEdge(shootSpeed, shootSpeed);
						Vector2 shootVelocity = shootDirection * shootSpeed + inaccuracyRatio * spread;
						Vector2 splitBarrelPos = GunTipPosition + (float)(i * LaserOffsetByAnimationFrame[base.Projectile.frame]) * perp;
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), splitBarrelPos, shootVelocity, projID, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
						SpawnFiringDust(splitBarrelPos, shootVelocity);
					}
				}
			}
		}
		base.ExtraBackArmRotation = Utils.Remap(Vector2.Dot(-Vector2.UnitY, base.Projectile.velocity.SafeNormalize(-Vector2.UnitY)), 0f, 1f, (float)Math.PI / 4f, 0f);
		postShotFade *= 0.86f;
	}

	private void SpawnFiringDust(Vector2 GunTipPosition, Vector2 laserVelocity)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		int dustID = ModContent.DustType<LightDust>();
		int dustRadius = 5;
		int dustDiameter = 2 * dustRadius;
		Vector2 dustCorner = GunTipPosition - Vector2.One * (float)dustRadius;
		for (int i = 0; i < 2; i++)
		{
			Vector2 dustVel = laserVelocity + Main.rand.NextVector2Circular(7f, 7f);
			Dust dust = Dust.NewDustDirect(dustCorner, dustDiameter, dustDiameter, dustID, dustVel.X, dustVel.Y);
			dust.velocity *= 0.125f;
			dust.noGravity = true;
			dust.scale = 0.9f;
			dust.color = Color.Lime;
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.OnSpawn(source);
		base.FrontArmStretch = Player.CompositeArmStretchAmount.Quarter;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 vel = base.Projectile.velocity * 10f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition - vel * postShotFade;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(rotation: base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) + (float)Math.PI / 2f, origin: frame.Size() * 0.5f, effects: (SpriteEffects)(((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f) ? 2 : 0), texture: value, position: drawPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), scale: base.Projectile.scale * base.Owner.gravDir);
		return false;
	}
}
