using System;
using CalamityMod.Buffs.DamageOverTime;
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
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BuzzkillHoldout : BaseGunHoldoutProjectile
{
	public const float ChargeupTime = 120f;

	public SlotId ChargeIdle;

	public bool NoSawOnHoldout;

	public static Asset<Texture2D> Holdout;

	public static Asset<Texture2D> SmallSlash;

	public static Asset<Texture2D> LargeSlash;

	public override int AssociatedItemID => ModContent.ItemType<Buzzkill>();

	public override float RecoilResolveSpeed => 0.05f;

	public override float MaxOffsetLengthFromArm => 30f;

	public override float OffsetXUpwards => -10f;

	public override float OffsetXDownwards => 5f;

	public override float BaseOffsetY => -10f;

	public override float OffsetYDownwards => 10f;

	public override Vector2 GunTipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.28f;
		}
	}

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void KillHoldoutLogic()
	{
		if (base.HeldItem.type != base.Owner.HeldItem.type)
		{
			base.Projectile.Kill();
			base.Projectile.netUpdate = true;
		}
	}

	public override void HoldoutAI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		float SawPower = MathHelper.Clamp(Time / 120f, 0f, 1f);
		if (SoundEngine.TryGetActiveSound(ChargeIdle, out ActiveSound Idle) && Idle.IsPlaying)
		{
			Idle.Position = GunTipPosition;
		}
		if (base.Owner.CantUseHoldout() && base.Projectile.ai[1] < 1f)
		{
			base.KeepRefreshingLifetime = false;
			Idle?.Stop();
			base.Projectile.ai[1] = 1f;
			base.Projectile.timeLeft = base.Owner.HeldItem.useAnimation;
			SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Item/SawShot", 2);
			soundStyle.PitchVariance = 0.1f;
			soundStyle.Volume = 0.4f + SawPower * 0.5f;
			SoundStyle ShootSound = soundStyle;
			SoundEngine.PlaySound(in ShootSound, GunTipPosition);
			int sawLevel = (SawPower >= 1f).ToInt() + (SawPower >= 0.25f).ToInt();
			if (Main.myPlayer == base.Projectile.owner)
			{
				float sawDamageMult = MathHelper.Lerp(1f, 5f, SawPower) / 1.5f;
				int sawPierce = (int)MathHelper.Lerp(2f, 6f, SawPower);
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * base.Owner.HeldItem.shootSpeed, ModContent.ProjectileType<BuzzkillSaw>(), (int)((float)base.Projectile.damage * sawDamageMult), (int)(base.Projectile.knockBack * (sawDamageMult / 2f)), Main.myPlayer, sawLevel);
				projectile.penetrate = sawPierce;
				projectile.rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			}
			NoSawOnHoldout = true;
			base.OffsetLengthFromArm -= 4f + 12f * SawPower;
			int sparkPairCount = 3 + 2 * sawLevel;
			for (int s = 0; s < sparkPairCount; s++)
			{
				float velocityMult = Main.rand.NextFloat(5f, 8f) + Main.rand.NextFloat(4f, 7f) * (float)sawLevel;
				float scale = Main.rand.NextFloat(0.6f, 0.8f) + Main.rand.NextFloat(0.3f, 0.5f) * (float)sawLevel;
				Vector2 sparkVelocity = base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * velocityMult;
				GeneralParticleHandler.SpawnParticle(new AltLineParticle(GunTipPosition, sparkVelocity, affectedByGravity: false, 40, scale, new Color(250, 250, 107)));
				sparkVelocity = base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * velocityMult;
				GeneralParticleHandler.SpawnParticle(new AltSparkParticle(GunTipPosition, sparkVelocity, affectedByGravity: false, 40, scale, new Color(250, 250, 107)));
			}
		}
		if (NoSawOnHoldout)
		{
			base.Projectile.frame = 4;
			return;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 3)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 3)
			{
				base.Projectile.frame = 1;
			}
		}
		if (Time > 30f && Time % 3f == 0f)
		{
			Vector2 sparkVel = Main.rand.NextVector2CircularEdge(1f, 1f);
			sparkVel.SafeNormalize(Vector2.Zero);
			sparkVel *= Main.rand.NextFloat(3f, 4.5f) + SawPower * 4f;
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(GunTipPosition, sparkVel, affectedByGravity: false, 10, Utils.GetLerpValue(0.05f, 0.65f, SawPower, clamped: true), new Color(250, 250, 107)));
		}
		if (Time < 120f)
		{
			if (Time == 30f)
			{
				SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/BuzzsawCharge")
				{
					Volume = 0.3f
				};
				ChargeIdle = SoundEngine.PlaySound(in soundStyle, GunTipPosition);
			}
			if (Time > 30f && base.Projectile.frame == 0)
			{
				base.Projectile.frame = 1;
			}
		}
		else
		{
			if ((Time + 240f) % 360f == 0f)
			{
				ChargeIdle = SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/BuzzsawIdle"), GunTipPosition);
			}
			if (Time % 3f == 0f)
			{
				Vector2 smokeVelocity = Vector2.UnitY * Main.rand.NextFloat(-7f, -12f);
				smokeVelocity = smokeVelocity.RotatedByRandom(0.39269909262657166);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition + Main.rand.NextVector2CircularEdge(3f, 3f), smokeVelocity, Color.Gray, 30, 0.65f, 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), glowing: true));
			}
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.OnSpawn(source);
		base.ExtraBackArmRotation = MathHelper.ToRadians(15f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(ChargeIdle, out ActiveSound Idle))
		{
			Idle?.Stop();
		}
	}

	public override bool? CanDamage()
	{
		return !NoSawOnHoldout;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 240);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice");
		style.Volume = 0.7f;
		SoundEngine.PlaySound(in style, GunTipPosition);
		int SawLevel = (Time / 120f >= 1f).ToInt() + (Time / 120f >= 0.25f).ToInt();
		int bloodCount = 4 + 3 * SawLevel;
		for (int p = 0; p < bloodCount; p++)
		{
			float radius = Main.rand.NextFloat(6f, 10f) + Main.rand.NextFloat(4f, 10f) * (float)SawLevel;
			Vector2 velocity = Main.rand.NextVector2CircularEdge(radius, radius);
			float scale = Main.rand.NextFloat(0.3f, 0.5f) + Main.rand.NextFloat(0.1f, 0.4f) * (float)SawLevel;
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(target.Center, velocity, affectedByGravity: false, 20, scale, new Color(112, 16, 16)));
		}
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		hitbox = new Rectangle((int)GunTipPosition.X - 19, (int)GunTipPosition.Y - 20, 38, 40);
		if (Time / 120f >= 1f)
		{
			((Rectangle)(ref hitbox)).Inflate(65, 65);
		}
		else if (Time / 120f >= 0.25f)
		{
			((Rectangle)(ref hitbox)).Inflate(28, 28);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		if (Holdout == null)
		{
			Holdout = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/BuzzkillHoldout", (AssetRequestMode)2);
		}
		Texture2D value = Holdout.Value;
		if (LargeSlash == null)
		{
			LargeSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/BuzzkillSawLargeSlash", (AssetRequestMode)2);
		}
		Texture2D largeSlashTexture = LargeSlash.Value;
		if (SmallSlash == null)
		{
			SmallSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/BuzzkillSawSmallSlash", (AssetRequestMode)2);
		}
		Texture2D smallSlashTexture = SmallSlash.Value;
		Color slashColor = default(Color);
		((Color)(ref slashColor))._002Ector(200, 200, 200, 100);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = frame.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		if (!NoSawOnHoldout)
		{
			float shake = Utils.Remap(Time, 0f, 120f, 0f, 3f);
			drawPosition += Main.rand.NextVector2Circular(shake, shake);
		}
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		if (Time > 30f && !NoSawOnHoldout)
		{
			if (Time / 120f >= 1f)
			{
				Main.EntitySpriteDraw(largeSlashTexture, GunTipPosition - Main.screenPosition, null, slashColor, Time * (0f - MathHelper.ToRadians(42f)), largeSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			}
			if (Time / 120f >= 0.25f)
			{
				Main.EntitySpriteDraw(smallSlashTexture, GunTipPosition - Main.screenPosition, null, slashColor, Time * MathHelper.ToRadians(42f), smallSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			}
			if (!CalamityClientConfig.Instance.Afterimages)
			{
				return false;
			}
			for (int i = 1; i < 3; i++)
			{
				float intensity = MathHelper.Lerp(0.05f, 0.25f, 1f - (float)i / 3f);
				if (Time / 120f >= 1f)
				{
					Main.EntitySpriteDraw(largeSlashTexture, GunTipPosition - Main.screenPosition, null, slashColor * intensity, (Time - (float)i) * (0f - MathHelper.ToRadians(42f)), largeSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
				}
				if (Time / 120f >= 0.25f)
				{
					Main.EntitySpriteDraw(smallSlashTexture, GunTipPosition - Main.screenPosition, null, slashColor * intensity, (Time - (float)i) * MathHelper.ToRadians(42f), smallSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
				}
			}
		}
		return false;
	}
}
