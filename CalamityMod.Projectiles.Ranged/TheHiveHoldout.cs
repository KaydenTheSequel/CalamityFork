using System;
using System.Runtime.CompilerServices;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TheHiveHoldout : BaseGunHoldoutProjectile
{
	[CompilerGenerated]
	private static Color _003CEffectsColor_003Ek__BackingField;

	[CompilerGenerated]
	private static Color _003CStaticEffectsColor_003Ek__BackingField;

	[CompilerGenerated]
	private SlotId _003CHiveHum_003Ek__BackingField;

	public override int AssociatedItemID => ModContent.ItemType<TheHive>();

	public override float RecoilResolveSpeed => 0.1f;

	public override float MaxOffsetLengthFromArm => 15f;

	public override float OffsetXUpwards => -12f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYUpwards => 15f;

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

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public ref float PostFireCooldown => ref base.Projectile.ai[1];

	public bool HasLetGo
	{
		get
		{
			return base.Projectile.ai[2] == 1f;
		}
		set
		{
			base.Projectile.ai[2] = (value ? 1f : 0f);
		}
	}

	public bool FireNuke => ShootingTimer > TheHive.MaxCharge;

	public SlotId HiveHum
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CHiveHum_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CHiveHum_003Ek__BackingField = value;
		}
	}

	public override void KillHoldoutLogic()
	{
		if (HasLetGo)
		{
			PostFiringCooldown();
		}
	}

	public override void HoldoutAI()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.CantUseHoldout() && !HasLetGo)
		{
			if (SoundEngine.TryGetActiveSound(HiveHum, out ActiveSound hum) && hum.IsPlaying)
			{
				hum?.Stop();
			}
			if (base.HeldItem.type == ModContent.ItemType<TheHive>())
			{
				ShootRocket();
			}
			HasLetGo = true;
		}
		if (!HasLetGo)
		{
			ShootingTimer++;
			if (SoundEngine.TryGetActiveSound(HiveHum, out ActiveSound hum2) && hum2.IsPlaying)
			{
				hum2.Position = base.Projectile.Center;
				hum2.Pitch = MathHelper.Lerp(0f, 0.8f, Utils.GetLerpValue(0f, TheHive.MaxCharge, ShootingTimer, clamped: true));
			}
		}
		if (Main.dedServ)
		{
			return;
		}
		Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 20f;
		if (ShootingTimer >= TheHive.MaxCharge && !HasLetGo)
		{
			for (int k = 0; k < 2; k++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(GunTipPosition + base.Projectile.velocity * 1.5f, Vector2.Zero, affectedByGravity: false, 2, Main.rand.NextFloat(0.5f, 1.1f), Color.Red));
			}
			for (int e = 0; e < 2; e++)
			{
				Dust dust = Dust.NewDustPerfect(GunTipPosition + base.Projectile.velocity * 1.5f, 90, shootDirection * Main.rand.NextFloat(0.01f, 0.8f));
				dust.scale = Main.rand.NextFloat(0.45f, 0.75f);
				dust.noGravity = true;
			}
		}
		if (ShootingTimer == TheHive.MaxCharge && !HasLetGo)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGAttackSwitchShort");
			style.Volume = 0.9f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i < 15; i++)
			{
				Dust dust2 = Dust.NewDustPerfect(GunTipPosition, 90, Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
				dust2.scale = Main.rand.NextFloat(0.85f, 1.15f);
				dust2.noGravity = true;
			}
		}
	}

	public void ShootRocket()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		float VelocityMultiplier = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0f, TheHive.MaxCharge, ShootingTimer, clamped: true));
		base.Owner.PickAmmo(base.HeldItem, out var _, out var projSpeed, out var damage, out var knockback, out var rocketType);
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
			DustEffectsID = 131;
			EffectsColor = Color.LawnGreen;
			break;
		}
		if (FireNuke)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGBarrageLaunch");
			style.Volume = 0.5f;
			style.Pitch = 0.1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, shootDirection * projSpeed * 0.3f, ModContent.ProjectileType<HiveNuke>(), damage * 10, knockback, base.Projectile.owner, rocketType);
			}
			PostFireCooldown = 75f;
		}
		else
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGBarrageLaunch");
			style.Volume = 0.4f;
			style.Pitch = 0.7f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				int numProj = 4;
				float rotation = MathHelper.ToRadians(MathHelper.Clamp(35f - VelocityMultiplier * 26f, 2f, 25f));
				for (int i = 0; i < numProj; i++)
				{
					Vector2 perturbedSpeed = shootDirection.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, perturbedSpeed * projSpeed * VelocityMultiplier, ModContent.ProjectileType<HiveMissile>(), damage, knockback, base.Projectile.owner, rocketType);
				}
			}
			PostFireCooldown = 30f;
		}
		if (Main.dedServ)
		{
			return;
		}
		if (FireNuke)
		{
			for (int k = 0; k < 10; k++)
			{
				float pulseScale = Main.rand.NextFloat(0.35f, 0.55f);
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, (shootDirection * 25f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.5f, 1.2f), (Main.rand.NextBool(3) ? EffectsColor : StaticEffectsColor) * 0.8f, new Vector2(1f, 1f), pulseScale - 0.25f, pulseScale, 0f, 20));
			}
			for (int j = 0; j <= 15; j++)
			{
				Dust dust = Dust.NewDustPerfect(GunTipPosition, Main.rand.NextBool(3) ? DustEffectsID : 303, (shootDirection * 20f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.5f, 1.2f), 0, default(Color), Main.rand.NextFloat(0.5f, 0.9f));
				dust.noGravity = false;
				if (dust.type != DustEffectsID)
				{
					dust.color = (Main.rand.NextBool(3) ? EffectsColor : StaticEffectsColor);
				}
			}
		}
		else
		{
			for (int l = 0; l < 6; l++)
			{
				float pulseScale2 = Main.rand.NextFloat(0.2f, 0.4f);
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(GunTipPosition, (shootDirection * 20f).RotatedByRandom(0.25) * Main.rand.NextFloat(0.5f, 1.2f), (Main.rand.NextBool(3) ? EffectsColor : StaticEffectsColor) * 0.8f, new Vector2(1f, 1f), pulseScale2 - 0.25f, pulseScale2, 0f, 20));
			}
		}
		base.OffsetLengthFromArm -= (FireNuke ? 16f : 6f);
	}

	public void PostFiringCooldown()
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		base.Owner.channel = true;
		if (PostFireCooldown > 0f)
		{
			PostFireCooldown--;
			Vector2 smokeVel = new Vector2(0f, -8f) * Main.rand.NextFloat(0.1f, 1.1f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition, smokeVel, StaticEffectsColor, Main.rand.Next(40, 61), Main.rand.NextFloat(0.3f, 0.6f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
			Dust dust = Dust.NewDustPerfect(GunTipPosition, 303, smokeVel.RotatedByRandom(0.10000000149011612), 80, default(Color), Main.rand.NextFloat(0.4f, 1.3f));
			dust.noGravity = false;
			dust.color = StaticEffectsColor;
		}
		else
		{
			if (SoundEngine.TryGetActiveSound(HiveHum, out ActiveSound hum) && hum.IsPlaying)
			{
				hum?.Stop();
			}
			base.Projectile.Kill();
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		base.OnSpawn(source);
		SoundStyle charge = new SoundStyle("CalamityMod/Sounds/Item/LowHum");
		base.FrontArmStretch = Player.CompositeArmStretchAmount.Quarter;
		base.ExtraBackArmRotation = MathHelper.ToRadians(15f);
		SoundStyle style = charge with
		{
			Volume = 1.6f,
			IsLooped = true
		};
		HiveHum = SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		if (ShootingTimer <= 0f)
		{
			return false;
		}
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>(GlowTexture, (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = value.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * base.Owner.gravDir == -1f);
		if (!base.Owner.CantUseHoldout() && PostFireCooldown <= 0f)
		{
			float rumble = ((Utils.GetLerpValue(0f, TheHive.MaxCharge, ShootingTimer, clamped: true) * ShootingTimer >= TheHive.MaxCharge) ? 2f : 0.8f);
			drawPosition += Main.rand.NextVector2Circular(rumble, rumble);
		}
		Main.EntitySpriteDraw(value, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		Main.EntitySpriteDraw(glowTexture, drawPosition, null, Color.White, drawRotation, rotationPoint, base.Projectile.scale * base.Owner.gravDir, flipSprite);
		return false;
	}

	static TheHiveHoldout()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		StaticEffectsColor = Color.Lime;
	}
}
