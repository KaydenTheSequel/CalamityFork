using System;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FetidEmesisHoldout : BaseGunHoldoutProjectile
{
	public float revSpeed = 1f;

	public int maxFrames = 420;

	public int initialFireTime = 45;

	public float shineScale;

	private bool secondShot = true;

	public override int AssociatedItemID => ModContent.ItemType<FetidEmesis>();

	public override float MaxOffsetLengthFromArm => 24f;

	public override float OffsetXUpwards => -5f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYDownwards => 5f;

	public ref float revFrames => ref base.Projectile.ai[0];

	public ref float cooldownTimer => ref base.Projectile.ai[1];

	public ref float shootingTimer => ref base.Projectile.ai[2];

	public bool isTired => cooldownTimer > 0f;

	public override void KillHoldoutLogic()
	{
		if (!isTired && (base.Owner.CantUseHoldout(needsToHold: false) || base.HeldItem.type != base.Owner.HeldItem.type))
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		if (!isTired && (base.Owner.CantUseHoldout() || base.HeldItem.type != base.Owner.HeldItem.type))
		{
			cooldownTimer = (int)Utils.Remap(revFrames, 0f, 350f, 40f, 180f);
		}
		if (isTired)
		{
			PostFiringCooldown();
			return;
		}
		revSpeed = Utils.Remap(revFrames, 0f, maxFrames - 120, 1f, 20f);
		int usedAmmoItemId;
		SoundStyle style;
		if (shootingTimer >= (float)initialFireTime && revFrames < (float)maxFrames && secondShot && base.Owner.whoAmI == Main.myPlayer)
		{
			base.Owner.PickAmmo(base.Owner.HeldItem, out var bulletAMMO, out var _, out var _, out var _, out usedAmmoItemId);
			style = new SoundStyle("CalamityMod/Sounds/Item/GunShotSmallAlt");
			style.Volume = 0.7f;
			style.Pitch = -0.1f + revSpeed * 0.01f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 19f;
			if (base.Owner.whoAmI == Main.myPlayer)
			{
				float spread = 0.045f * Utils.GetLerpValue(0f, maxFrames, revFrames, clamped: true);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity.RotatedByRandom(spread), bulletAMMO, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
			for (int i = 0; i <= 3; i++)
			{
				Dust.NewDustPerfect(GunTipPosition - base.Projectile.velocity * 5f, Main.rand.NextBool(5) ? 28 : 215, shootVelocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.2f, 1.5f), 0, default(Color), Main.rand.NextFloat(0.6f, 1.4f)).noGravity = true;
			}
			base.OffsetLengthFromArm -= 5f;
			shineScale = 1f;
			secondShot = false;
		}
		if (shootingTimer >= 60f && revFrames < (float)maxFrames)
		{
			base.Owner.PickAmmo(base.Owner.HeldItem, out var bulletAMMO2, out var _, out var _, out var _, out usedAmmoItemId);
			Vector2 shootVelocity2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 19f;
			if (base.Owner.whoAmI == Main.myPlayer)
			{
				float spread2 = 0.045f * Utils.GetLerpValue(0f, maxFrames, revFrames, clamped: true);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity2.RotatedByRandom(spread2).RotatedByRandom(0.03999999910593033), bulletAMMO2, base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
				base.Projectile.netUpdate = true;
			}
			shootingTimer = 0f;
			secondShot = true;
		}
		shootingTimer += revSpeed;
		revFrames++;
		shineScale *= 0.77f;
		if (((!(revFrames >= (float)maxFrames) || isTired) && (!base.Owner.Calamity().mouseRight || !(revFrames > 2f))) || base.Owner.whoAmI != Main.myPlayer)
		{
			return;
		}
		base.Owner.SetScreenshake(6.5f);
		base.OffsetLengthFromArm -= 35f;
		cooldownTimer = (int)MathHelper.Lerp((float)(int)Utils.Remap(revFrames, 0f, 350f, 40f, 180f), 180f, 0.7f);
		SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/Perforator/PerfHiveIchorShoot");
		SoundStyle bigShotGun = new SoundStyle("CalamityMod/Sounds/Item/FlakKrakenShoot");
		style = soundStyle with
		{
			Pitch = -0.2f,
			Volume = 0.6f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = bigShotGun with
		{
			Volume = 0.6f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int chunkDamage = (int)((float)base.Projectile.damage * 2.5f);
		Vector2 shootVelocity3 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 13f;
		GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(GunTipPosition - shootVelocity3, shootVelocity3, affectedByGravity: false, 12, 0.035f, Color.Chartreuse, new Vector2(2.5f, 0.9f), quickShrink: true));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(GunTipPosition, base.Projectile.velocity * 14.5f, Color.Chartreuse * 0.7f, "CalamityMod/Particles/DustyCircleHardEdge", new Vector2(0.4f, 1f), base.Projectile.velocity.ToRotation(), 0.13f, 0f, 34, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(GunTipPosition, base.Projectile.velocity * 9f, Color.Chartreuse * 0.7f, "CalamityMod/Particles/FlameExplosion", new Vector2(0.4f, 1f), base.Projectile.velocity.ToRotation(), 0.25f, 0f, 34, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		if (Main.myPlayer == base.Projectile.owner)
		{
			float velBonus = 1.1f;
			for (int j = 0; j <= 5; j++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity3.RotatedBy(-0.03f * (float)j * velBonus) * (1f - (float)j * 0.08f) * velBonus, ModContent.ProjectileType<EmesisGore>(), chunkDamage, base.Projectile.knockBack * 3f, base.Projectile.owner, -j, 0f, velBonus);
			}
			for (int k = 0; k <= 5; k++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity3.RotatedBy(0.03f * (float)k * velBonus) * (1f - (float)k * 0.08f) * velBonus, ModContent.ProjectileType<EmesisGore>(), chunkDamage, base.Projectile.knockBack * 3f, base.Projectile.owner, k, 0f, velBonus);
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity3 * velBonus, ModContent.ProjectileType<EmesisGore>(), chunkDamage, base.Projectile.knockBack * 3f, base.Projectile.owner, 20f, 0f, velBonus);
		}
		for (int l = 0; l <= 18; l++)
		{
			Dust dust = Dust.NewDustPerfect(GunTipPosition, 66, shootVelocity3.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 1.5f), 0, default(Color), Main.rand.NextFloat(0.6f, 1.4f));
			dust.noGravity = true;
			dust.color = Color.Chartreuse;
		}
	}

	private void PostFiringCooldown()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.channel = true;
		if (revFrames > 3f)
		{
			revFrames *= 0.7f;
		}
		if (cooldownTimer > 1f)
		{
			Vector2 smokeVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 5f;
			if (cooldownTimer % 29f == 0f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeHuff");
				style.Pitch = -0.2f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int i = 0; i <= 12; i++)
				{
					Dust dust = Dust.NewDustPerfect(GunTipPosition, 303, smokeVel.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.2f, 1f), 80, default(Color), Main.rand.NextFloat(0.4f, 1.3f));
					dust.noGravity = false;
					dust.color = Color.White;
				}
				for (int j = 0; j < 5; j++)
				{
					GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, smokeVel.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.2f, 1f), Color.White, Main.rand.Next(40, 61), Main.rand.NextFloat(0.2f, 0.4f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
				}
			}
		}
		else
		{
			base.Projectile.Kill();
		}
		cooldownTimer--;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		if (revFrames < 2f)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Color chartreuse;
		for (int i = 1; i <= 24; i++)
		{
			float attackLerp = (float)Math.Pow(Utils.GetLerpValue(60f, 200f, revFrames, clamped: true), 8.0);
			float mult = MathHelper.Max(Utils.GetLerpValue(7f, 0f, i), Utils.GetLerpValue(17f, 24f, i));
			float outspace = 6f * attackLerp;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 24f).ToRotationVector2().RotatedBy(base.Projectile.rotation) * outspace + Main.rand.NextVector2Circular(2f, 2f);
			chartreuse = Color.Chartreuse;
			((Color)(ref chartreuse)).A = 0;
			Color auraColor = chartreuse * mult * 0.4f * Utils.GetLerpValue(90f, 135f, revFrames, clamped: true);
			float aimAngle = drawRotation;
			Main.EntitySpriteDraw(texture, drawPosition + drawOffset, null, auraColor, aimAngle, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/FullStar", (AssetRequestMode)2).Value;
		float rot = Main.GlobalTimeWrappedHourly * 4f;
		if (!isTired && revFrames < (float)maxFrames && revFrames > 20f)
		{
			for (int j = -2; j <= 2; j++)
			{
				Vector2 position = GunTipPosition - Main.screenPosition;
				chartreuse = Color.Chartreuse;
				((Color)(ref chartreuse)).A = 0;
				Main.EntitySpriteDraw(rechargeTexture, position, null, chartreuse * 0.35f, base.Projectile.rotation + (shineScale + rot), rechargeTexture.Size() * 0.5f, new Vector2(1f - (float)j * 0.2f, 1f + (float)j * 0.2f) * shineScale * 3f, (SpriteEffects)0);
			}
			for (int k = -2; k <= 2; k++)
			{
				Vector2 position2 = GunTipPosition - Main.screenPosition;
				chartreuse = Color.Chartreuse;
				((Color)(ref chartreuse)).A = 0;
				Main.EntitySpriteDraw(rechargeTexture, position2, null, chartreuse * 0.35f, base.Projectile.rotation - (shineScale + rot), rechargeTexture.Size() * 0.5f, new Vector2(1f - (float)k * 0.2f, 1f + (float)k * 0.2f) * shineScale * 3f, (SpriteEffects)0);
			}
		}
		return false;
	}
}
