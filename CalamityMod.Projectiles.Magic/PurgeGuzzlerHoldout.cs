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
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

internal class PurgeGuzzlerHoldout : BaseGunHoldoutProjectile
{
	public float revSpeed;

	public int shotsFired;

	public Color color1;

	public Color color2;

	public override int AssociatedItemID => ModContent.ItemType<PurgeGuzzler>();

	public override float MaxOffsetLengthFromArm => 35f;

	public override float OffsetYUpwards => base.OffsetYUpwards;

	public override float OffsetXUpwards => base.OffsetXUpwards;

	public override float OffsetXDownwards => base.OffsetXDownwards;

	public override float OffsetYDownwards => base.OffsetYDownwards;

	public override float BaseOffsetY => -5f;

	public override float RecoilResolveSpeed => 0.4f;

	public override float WeaponTurnSpeed
	{
		get
		{
			if (!(cooldownTimer > 0f))
			{
				return 0.06f;
			}
			return 0.02f;
		}
	}

	public override string Texture => "CalamityMod/Items/Weapons/Magic/PurgeGuzzler";

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition - Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 10f + Vector2.UnitY * 3f;
		}
	}

	public ref float revFrames => ref base.Projectile.ai[0];

	public ref float cooldownTimer => ref base.Projectile.ai[1];

	public ref float shootingTimer => ref base.Projectile.ai[2];

	public bool isOnCooldown => cooldownTimer > 0f;

	public override void KillHoldoutLogic()
	{
		if (!isOnCooldown && (base.Owner.CantUseHoldout(needsToHold: false) || base.HeldItem.type != base.Owner.HeldItem.type))
		{
			base.Projectile.Kill();
		}
	}

	public override void HoldoutAI()
	{
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		if (!isOnCooldown && (base.Owner.CantUseHoldout() || base.HeldItem.type != base.Owner.HeldItem.type))
		{
			cooldownTimer = (int)Utils.Remap(revFrames, 0f, 350f, 40f, 120f);
		}
		if (isOnCooldown)
		{
			PostFiringCooldown();
			return;
		}
		revSpeed = Utils.Remap(revFrames, 0f, 150f, 1f, 3f);
		if (shootingTimer >= 10f && revFrames < 150f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianShieldDeactivate");
			style.Pitch = Utils.Remap(revFrames, 0f, 150f, 0.2f, 0.8f);
			style.Volume = 0.2f;
			style.MaxInstances = -1;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
				float spread = 0.045f * Utils.GetLerpValue(0f, 300f, revFrames, clamped: true);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity.RotatedByRandom(spread), ModContent.ProjectileType<HolyLaser>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, revSpeed * ((shotsFired % 2 == 0) ? (-1f) : 1f) * Utils.Remap(revFrames, 120f, 150f, 0.1f, 0.45f), base.Projectile.whoAmI);
			}
			for (int i = 0; i <= 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(GunTipPosition, ModContent.DustType<LightDust>(), Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(2f, 5f) * revSpeed, 0, default(Color), Main.rand.NextFloat(0.3f, 0.7f) * revSpeed);
				dust.noGravity = true;
				dust.color = Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f));
			}
			base.OffsetLengthFromArm -= 7f;
			shootingTimer = 0f;
			shotsFired++;
		}
		shootingTimer += revSpeed;
		revFrames++;
		if (revFrames >= 150f && !isOnCooldown)
		{
			revSpeed = 4f;
			base.Owner.SetScreenshake(3.5f);
			base.OffsetLengthFromArm -= 35f;
			cooldownTimer = 60f;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianRay");
			style.Pitch = 0.2f;
			style.Volume = 0.7f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyRay");
			style.Pitch = 0.4f;
			style.Volume = 0.8f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Vector2 shootVelocity2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY);
			if (Main.myPlayer == base.Projectile.owner)
			{
				int bigBeamDamage = (int)((float)base.Projectile.damage * 6.5f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity2, ModContent.ProjectileType<HolyLaser>(), bigBeamDamage, base.Projectile.knockBack * 3f, base.Projectile.owner, 1f, base.Projectile.whoAmI, 1f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootVelocity2, ModContent.ProjectileType<HolyLaser>(), bigBeamDamage, base.Projectile.knockBack * 3f, base.Projectile.owner, -1f, base.Projectile.whoAmI, 1f);
			}
			for (int j = 0; j <= 25; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(GunTipPosition, 278, shootVelocity2.RotatedByRandom(0.800000011920929) * Main.rand.NextFloat(5f, 30f), 0, default(Color), Main.rand.NextFloat(0.6f, 1.4f));
				dust2.noGravity = true;
				dust2.color = Color.Lerp(Color.Orchid, Color.White, Main.rand.NextFloat(0f, 0.7f));
			}
		}
	}

	private void PostFiringCooldown()
	{
		base.Owner.channel = true;
		if (cooldownTimer <= 1f)
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
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		if (revFrames < 2f)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Color val2;
		if (base.Owner.ownedProjectileCounts[ModContent.ProjectileType<HolyLaser>()] > 0 && shotsFired > 0)
		{
			float fade = Utils.GetLerpValue(1f, 4f, revSpeed);
			for (int i = 0; i < 10; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * 5f * fade;
				SpriteBatch spriteBatch = Main.spriteBatch;
				Vector2 val = drawPosition + drawOffset;
				val2 = Color.Orchid;
				((Color)(ref val2)).A = 0;
				spriteBatch.Draw(texture, val, (Rectangle?)null, val2 * fade, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite, 0f);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		if (base.Owner.ownedProjectileCounts[ModContent.ProjectileType<HolyLaser>()] > 0 && shotsFired > 0)
		{
			float fade2 = Utils.GetLerpValue(1f, 4f, revSpeed);
			Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/LargeBloom", (AssetRequestMode)2).Value;
			float rot = Main.GlobalTimeWrappedHourly * 25f;
			float sine = MathHelper.Clamp(Math.Abs((float)Math.Sin(rot * 2.275f / (float)Math.PI)), 0.9f, 1f);
			for (int j = 0; j < 6; j++)
			{
				Vector2 position = GunTipPosition - Main.screenPosition;
				val2 = Color.Lerp(Color.Orange, Color.Lerp(Color.Orchid, Color.Khaki, (float)j * 0.1f), fade2 + 0.3f);
				((Color)(ref val2)).A = 0;
				Main.EntitySpriteDraw(rechargeTexture, position, null, val2 * 0.35f, rot * ((float)j * 0.3f), rechargeTexture.Size() * 0.5f, (0.03f + (float)j * 0.007f) * MathHelper.Clamp(revSpeed, 1f, 3.3f) * sine, (SpriteEffects)0);
			}
		}
		return false;
	}

	public PurgeGuzzlerHoldout()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		revSpeed = 1f;
		color1 = Color.Goldenrod;
		color2 = Color.Orange;
		base._002Ector();
	}
}
