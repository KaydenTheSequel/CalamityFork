using System;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AetherfluxCannonHoldout : BaseGunHoldoutProjectile
{
	public float postShotFade;

	public override int AssociatedItemID => ModContent.ItemType<AetherfluxCannon>();

	public override float MaxOffsetLengthFromArm => 10f;

	public override float OffsetXUpwards => -15f;

	public override float OffsetXDownwards => 10f;

	public override float BaseOffsetY => -15f;

	public override float OffsetYUpwards => 8f;

	public override float OffsetYDownwards => 15f;

	public override string Texture => "CalamityMod/Projectiles/Magic/AetherfluxCannonHoldout";

	private ref float DeployedFrames => ref base.Projectile.ai[0];

	private ref float AnimationRate => ref base.Projectile.ai[1];

	private ref float LastShootAttemptTime => ref base.Projectile.localAI[0];

	private ref float LastAnimationTime => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
	}

	public override void KillHoldoutLogic()
	{
		base.KillHoldoutLogic();
		if (DeployedFrames >= (float)(base.HeldItem?.useAnimation ?? 36) && !base.Owner.CheckMana(base.Owner.HeldItem))
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		if (DeployedFrames <= 0f)
		{
			SoundStyle style = SoundID.DD2_DarkMageCastHeal with
			{
				Volume = SoundID.DD2_DarkMageCastHeal.Volume * 1.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		base.Projectile.damage = ((base.HeldItem != null) ? base.Owner.GetWeaponDamage(base.HeldItem) : 0);
		int itemUseTime = base.HeldItem?.useAnimation ?? 36;
		int framesPerShot = itemUseTime / 7;
		DeployedFrames++;
		AnimationRate = ((DeployedFrames >= (float)itemUseTime) ? 2f : MathHelper.Lerp(7f, 2f, DeployedFrames / (float)itemUseTime));
		if (DeployedFrames - LastAnimationTime >= AnimationRate)
		{
			LastAnimationTime = DeployedFrames;
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
		if (DeployedFrames - LastShootAttemptTime >= (float)framesPerShot)
		{
			LastShootAttemptTime = DeployedFrames;
			bool actuallyShoot = DeployedFrames >= (float)itemUseTime;
			if (!actuallyShoot || base.Owner.CheckMana(base.Owner.HeldItem, -1, pay: true))
			{
				if (actuallyShoot)
				{
					postShotFade = 1f;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonShot");
					style.Volume = 0.4f;
					style.Pitch = Main.rand.NextFloat(0.1f, 0.3f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				int projID = ModContent.ProjectileType<PhasedGodRay>();
				float shootSpeed = base.HeldItem.shootSpeed;
				Vector2 val = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
				Vector2 shootVelocity = val * shootSpeed;
				float waveSideOffset = Main.rand.NextFloat(18f, 28f);
				Vector2 perp = val.RotatedBy(-1.5707963705062866) * waveSideOffset;
				float dustInaccuracy = 0.045f;
				for (int i = -1; i <= 1; i += 2)
				{
					Vector2 laserStartPos = GunTipPosition + (float)i * perp + Main.rand.NextVector2CircularEdge(6f, 6f);
					Vector2 dustOnlySpread = Main.rand.NextVector2Circular(shootSpeed, shootSpeed);
					Vector2 dustVelocity = shootVelocity + dustInaccuracy * dustOnlySpread;
					if (actuallyShoot && Main.myPlayer == base.Projectile.owner)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), laserStartPos, shootVelocity, projID, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, (float)i * 0.5f);
					}
					SpawnFiringDust(GunTipPosition, dustVelocity);
				}
			}
		}
		postShotFade *= 0.86f;
	}

	private void SpawnFiringDust(Vector2 GunTipPosition, Vector2 laserVelocity)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		int dustID = ModContent.DustType<LightDust>();
		int dustRadius = 12;
		float dustRandomness = 11f;
		int dustDiameter = 2 * dustRadius;
		Vector2 dustCorner = GunTipPosition - Vector2.One * (float)dustRadius;
		for (int i = 0; i < 2; i++)
		{
			Vector2 dustVel = laserVelocity + Main.rand.NextVector2Circular(dustRandomness, dustRandomness);
			Dust dust = Dust.NewDustDirect(dustCorner, dustDiameter, dustDiameter, dustID, dustVel.X, dustVel.Y);
			dust.velocity *= 0.18f;
			dust.noGravity = true;
			dust.scale = 0.6f;
			dust.color = Color.Lerp(AetherfluxCannon.accentColor, AetherfluxCannon.mainColor, Main.rand.NextFloat());
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 vel = base.Projectile.velocity * 10f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + vel + vel * (1f - postShotFade);
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f) + (float)Math.PI / 2f;
		Vector2 rotationPoint = frame.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f) ? 2 : 0);
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Color color;
		for (int i = 0; i < 5; i++)
		{
			float glowScale = 0.3f;
			Vector2 position = drawPosition + vel * 1.7f;
			color = Color.Lerp(AetherfluxCannon.accentColor, AetherfluxCannon.mainColor, postShotFade);
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(glowTexture, position, null, color, vel.ToRotation(), glowTexture.Size() * 0.5f, new Vector2(1.4f, 0.75f) * (glowScale - (float)i * 0.05f) * postShotFade * 2.5f, (SpriteEffects)0);
		}
		for (int j = 0; j < 15; j++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)j / 15f).ToRotationVector2() * 6.5f * postShotFade;
			Vector2 position2 = drawPosition + drawOffset;
			Rectangle? sourceRectangle = frame;
			color = Color.Lerp(AetherfluxCannon.accentColor, AetherfluxCannon.mainColor, postShotFade);
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(texture, position2, sourceRectangle, color, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		return false;
	}
}
