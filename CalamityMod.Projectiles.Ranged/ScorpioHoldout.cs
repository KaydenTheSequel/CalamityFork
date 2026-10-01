using System;
using System.Runtime.CompilerServices;
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

public class ScorpioHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private static Color _003CEffectsColor_003Ek__BackingField;

	[CompilerGenerated]
	private static Color _003CStaticEffectsColor_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<Scorpio>();

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
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			return base.GunTipPosition - Vector2.UnitY.RotatedBy(base.Projectile.rotation) * 10f * (float)base.Projectile.spriteDirection * base.Owner.gravDir;
		}
	}

	public override float RecoilResolveSpeed => 0.1f;

	public override float MaxOffsetLengthFromArm => 15f;

	public override float OffsetXUpwards => -12f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYDownwards => 10f;

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public ref float TimerBetweenBursts => ref base.Projectile.ai[1];

	public ref float ChargeLV => ref base.Projectile.ai[2];

	public static int DustEffectsID { get; set; }

	public static Color EffectsColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _003CEffectsColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			_003CEffectsColor_003Ek__BackingField = value;
		}
	}

	public static Color StaticEffectsColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return _003CStaticEffectsColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			_003CStaticEffectsColor_003Ek__BackingField = value;
		}
	}

	public override void HoldoutAI()
	{
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.Calamity().mouseRight && ChargeLV >= 3f)
		{
			ShootRocket(base.HeldItem, isRMB: true);
			ShootingTimer = base.HeldItem.useAnimation;
			ChargeLV = -1f;
		}
		if (ShootingTimer >= (float)base.HeldItem.useAnimation)
		{
			if (ShootingTimer == (float)base.HeldItem.useAnimation && ChargeLV < 3f)
			{
				ChargeLV++;
			}
			int adaptedTimeBetweenBursts = Scorpio.TimeBetweenBursts * base.HeldItem.useAnimation / Scorpio.OriginalUseTime;
			if (ShootingTimer % (float)adaptedTimeBetweenBursts == 0f)
			{
				ShootRocket(base.HeldItem, isRMB: false);
			}
			if (ShootingTimer >= (float)(base.HeldItem.useAnimation + adaptedTimeBetweenBursts * (Scorpio.ProjectilesPerBurst - 1)))
			{
				ShootingTimer = 0f;
				TimerBetweenBursts = 0f;
			}
		}
		ShootingTimer++;
		if (Main.dedServ)
		{
			return;
		}
		if (ChargeLV == 1f && ShootingTimer % 3f == 0f)
		{
			Vector2 position = GunTipPosition - base.Projectile.velocity * 95f;
			Vector2 velocity = (-base.Projectile.velocity * 5f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.6f, 1.4f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(position, velocity, Main.rand.NextFloat(0.12f, 0.14f), StaticEffectsColor, Main.rand.Next(3, 6), 1f, 1.5f));
			Dust.NewDustPerfect(position, DustEffectsID, velocity, 0, default(Color), Main.rand.NextFloat(1.2f, 1.7f)).noGravity = true;
		}
		if ((ChargeLV == 2f) & (ShootingTimer % 2f == 0f))
		{
			Vector2 position2 = GunTipPosition - base.Projectile.velocity * 95f;
			Vector2 velocity2 = (-base.Projectile.velocity * 5f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.9f, 1.9f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(position2, velocity2, Main.rand.NextFloat(0.18f, 0.22f), StaticEffectsColor, Main.rand.Next(3, 6), 1f, 1.5f));
			Dust.NewDustPerfect(position2, DustEffectsID, velocity2, 0, default(Color), Main.rand.NextFloat(1.2f, 1.7f)).noGravity = true;
		}
		if (ChargeLV >= 3f)
		{
			Vector2 position3 = GunTipPosition - base.Projectile.velocity * 95f;
			Vector2 velocity3 = (-base.Projectile.velocity * 5f).RotatedByRandom(0.75) * Main.rand.NextFloat(1.2f, 2.3f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(position3, velocity3, Main.rand.NextFloat(0.24f, 0.34f), StaticEffectsColor, Main.rand.Next(3, 6), 1f, 1.5f));
			for (int i = 0; i < 2; i++)
			{
				Dust.NewDustPerfect(position3, DustEffectsID, velocity3, 0, default(Color), Main.rand.NextFloat(1.6f, 2.1f)).noGravity = true;
			}
		}
		if (ChargeLV == 0f && ShootingTimer % 3f == 0f)
		{
			Vector2 position4 = GunTipPosition - base.Projectile.velocity * 95f;
			Vector2 velocity4 = (-base.Projectile.velocity * 4f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.9f, 2f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(position4, velocity4, Color.SlateGray, Main.rand.Next(40, 61), Main.rand.NextFloat(0.3f, 0.6f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), glowing: true, 0f, required: true));
			for (int j = 0; j < 2; j++)
			{
				Dust.NewDustPerfect(position4, 303, velocity4 * 0.5f, 200, default(Color), Main.rand.NextFloat(0.9f, 1.3f)).noGravity = false;
			}
		}
	}

	public void ShootRocket(Item item, bool isRMB)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		Vector2 projectileVelocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		base.Owner.PickAmmo(item, out var _, out var projSpeed, out var damage, out var knockback, out var rocketType, Main.rand.Next(100) > 70);
		switch (rocketType)
		{
		case 4447:
			DustEffectsID = 45;
			EffectsColor = Color.RoyalBlue;
			break;
		case 4448:
			DustEffectsID = 6;
			EffectsColor = Color.Red;
			break;
		case 4449:
			DustEffectsID = 152;
			EffectsColor = Color.Yellow;
			break;
		default:
			DustEffectsID = 302;
			EffectsColor = Color.Aquamarine;
			break;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, projectileVelocity.RotatedByRandom(isRMB ? 0f : ((float)Math.PI / 4f)) * projSpeed * (isRMB ? 1f : Main.rand.NextFloat(0.8f, 1f)), isRMB ? ModContent.ProjectileType<ScorpioLargeRocket>() : ModContent.ProjectileType<ScorpioRocket>(), damage, knockback, base.Projectile.owner, rocketType, projSpeed);
		}
		if (!Main.dedServ)
		{
			SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Item/RealityRuptureStealth");
			soundStyle.Volume = 0.45f;
			SoundStyle RightClickSound = soundStyle;
			if (isRMB)
			{
				soundStyle = RightClickSound with
				{
					Pitch = -0.1f
				};
				SoundEngine.PlaySound(in soundStyle, base.Projectile.Center);
			}
			else
			{
				soundStyle = Scorpio.RocketShoot with
				{
					Pitch = ChargeLV * 0.055f
				};
				SoundEngine.PlaySound(in soundStyle, base.Projectile.Center);
			}
			base.OffsetLengthFromArm -= (isRMB ? 30f : 5f);
			int dustAmount = Main.rand.Next(10, 16);
			for (int i = 0; i < dustAmount; i++)
			{
				Dust dust = Dust.NewDustPerfect(GunTipPosition, DustEffectsID, projectileVelocity.RotatedByRandom((float)Math.PI / 2f - MathHelper.ToRadians(15f)) * Main.rand.NextFloat(6f, 10f));
				dust.noGravity = true;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, Vector2.Zero, Color.Gray * 0.7f, new Vector2(0.5f, 1f), base.Projectile.rotation, 0.1f, 0.4f, 20));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>(GlowTexture, (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = value.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		Main.EntitySpriteDraw(value, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		Main.EntitySpriteDraw(glowTexture, drawPosition, null, Color.White, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		return false;
	}

	static ScorpioHoldout()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		StaticEffectsColor = Color.Turquoise;
	}
}
