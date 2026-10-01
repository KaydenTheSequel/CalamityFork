using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Cooldowns;
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

public class SuperradiantSlaughtererHoldout : BaseGunHoldoutProjectile
{
	public const float ChargeupTime = 120f;

	public SlotId ChargeIdle;

	public bool NoSawOnHoldout;

	public Particle SmallSlashSmear;

	public Particle LargeSlashSmear;

	public static Asset<Texture2D> Holdout;

	public static Asset<Texture2D> HoldoutGlow;

	public static Asset<Texture2D> MiniSaw;

	public static Asset<Texture2D> SmallSlash;

	public static Asset<Texture2D> LargeSlash;

	public override int AssociatedItemID => ModContent.ItemType<SuperradiantSlaughterer>();

	public override float RecoilResolveSpeed => 0.05f;

	public override float MaxOffsetLengthFromArm => 36f;

	public override float BaseOffsetY => -5f;

	public override float OffsetYUpwards => -5f;

	public override float OffsetYDownwards => 5f;

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
			return base.Projectile.Center + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.25f;
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
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void KillHoldoutLogic()
	{
		if (base.HeldItem.type != base.Owner.HeldItem.type || base.Owner.dead || !base.Owner.active)
		{
			base.Projectile.Kill();
			base.Projectile.netUpdate = true;
		}
	}

	public override void HoldoutAI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_092d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0932: Unknown result type (might be due to invalid IL or missing references)
		//IL_0934: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		float SawPower = MathHelper.Clamp(Time / 120f, 0f, 1f);
		if (SoundEngine.TryGetActiveSound(ChargeIdle, out ActiveSound Idle) && Idle.IsPlaying)
		{
			Idle.Position = GunTipPosition;
		}
		if (base.Owner.Calamity().mouseRight && !base.Owner.HasCooldown(SuperradiantSawBoost.ID))
		{
			if (base.Projectile.ai[1] >= 2f)
			{
				base.Owner.AddCooldown(SuperradiantSawBoost.ID, 360);
				SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/MeatySlash"), GunTipPosition);
				if (Main.myPlayer == base.Projectile.owner)
				{
					float adjustedMouseDist = MathHelper.Clamp(Vector2.Distance(GunTipPosition, base.Owner.Calamity().mouseWorld), 0f, 960f) / 21f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * adjustedMouseDist, ModContent.ProjectileType<SuperradiantSawLingering>(), (int)((float)base.Projectile.damage * 1.5f), base.Projectile.knockBack, base.Projectile.owner);
				}
				NoSawOnHoldout = true;
				base.OffsetLengthFromArm -= 16f;
				base.Projectile.timeLeft = base.Owner.HeldItem.useAnimation;
				base.KeepRefreshingLifetime = false;
				Idle?.Stop();
				if (base.Owner.velocity != Vector2.Zero)
				{
					int particleAmt = 7;
					for (int c = 0; c < particleAmt; c++)
					{
						Color sparkColor = Color.Lerp(new Color(122, 240, 58), new Color(32, 186, 171), (float)(c / (particleAmt - 1)));
						GeneralParticleHandler.SpawnParticle(new CritSpark(base.Owner.Center, base.Owner.velocity.RotatedByRandom(MathHelper.ToRadians(13f)) * Main.rand.NextFloat(-2.1f, -4.5f), Color.White, sparkColor, 2f, 45, 2.25f, 2f));
					}
					for (int e = 0; e < particleAmt * 2; e++)
					{
						Color sparkColor2 = Color.Lerp(new Color(122, 240, 58), new Color(32, 186, 171), (float)(e / (particleAmt - 1)));
						GeneralParticleHandler.SpawnParticle(new NanoParticle(base.Owner.Center, base.Owner.velocity.RotatedByRandom(MathHelper.ToRadians(-(float)Math.PI / 4f)) * Main.rand.NextFloat(2.5f, 4.5f), sparkColor2, 1f, 45, Main.rand.NextBool(3)));
					}
				}
			}
		}
		else if (base.Owner.CantUseHoldout() && base.Projectile.ai[1] < 1f)
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
			float sawDamageMult = MathHelper.Lerp(1f, 5f, SawPower) / 2f;
			int sawPierce = (int)MathHelper.Lerp(2f, 7f, SawPower);
			int sawLevel = (SawPower >= 1f).ToInt() + (SawPower >= 0.25f).ToInt();
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), GunTipPosition, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 24f, ModContent.ProjectileType<SuperradiantSaw>(), (int)((float)base.Projectile.damage * sawDamageMult), base.Projectile.knockBack, base.Projectile.owner, sawLevel, 0f, sawPierce);
			}
			NoSawOnHoldout = true;
			base.OffsetLengthFromArm -= 4f + 12f * SawPower;
			int sparkPairCount = 3 + 2 * sawLevel;
			for (int s = 0; s < sparkPairCount; s++)
			{
				float velocityMult = Main.rand.NextFloat(5f, 8f) + Main.rand.NextFloat(4f, 7f) * (float)sawLevel;
				float scale = Main.rand.NextFloat(0.6f, 0.8f) + Main.rand.NextFloat(0.3f, 0.5f) * (float)sawLevel;
				Color color = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.8f);
				Vector2 sparkVelocity = base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * velocityMult;
				GeneralParticleHandler.SpawnParticle(new AltLineParticle(GunTipPosition, sparkVelocity, affectedByGravity: false, 40, scale, color));
				sparkVelocity = base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * velocityMult;
				GeneralParticleHandler.SpawnParticle(new AltSparkParticle(GunTipPosition, sparkVelocity, affectedByGravity: false, 40, scale, color));
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
				base.Projectile.frame = 0;
			}
		}
		if (SawPower >= 1f)
		{
			if (LargeSlashSmear == null)
			{
				LargeSlashSmear = new CircularSmearVFX(GunTipPosition, Color.Black, Time * (0f - MathHelper.ToRadians(42f)), 1.35f);
				GeneralParticleHandler.SpawnParticle(LargeSlashSmear);
			}
			else
			{
				LargeSlashSmear.Rotation = Time * (0f - MathHelper.ToRadians(42f));
				LargeSlashSmear.Time = 0;
				LargeSlashSmear.Position = GunTipPosition;
				LargeSlashSmear.Scale = 1.35f;
				LargeSlashSmear.Color = Main.hslToRgb(0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.6f) * 0.8f;
			}
		}
		if (SawPower >= 0.25f)
		{
			if (SmallSlashSmear == null)
			{
				SmallSlashSmear = new CircularSmearVFX(GunTipPosition, Color.Black, Time * MathHelper.ToRadians(42f), 0.8f);
				GeneralParticleHandler.SpawnParticle(SmallSlashSmear);
			}
			else
			{
				SmallSlashSmear.Rotation = Time * MathHelper.ToRadians(42f);
				SmallSlashSmear.Time = 0;
				SmallSlashSmear.Position = GunTipPosition;
				SmallSlashSmear.Scale = 0.8f;
				SmallSlashSmear.Color = Main.hslToRgb(0.5f + 0.5f * MathF.Cos(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.6f) * 0.6f;
			}
		}
		if (Time < 120f)
		{
			if (Time == 30f && !NoSawOnHoldout)
			{
				SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/BuzzsawCharge")
				{
					Volume = 0.3f
				};
				ChargeIdle = SoundEngine.PlaySound(in soundStyle, GunTipPosition);
			}
			return;
		}
		if ((Time + 240f) % 360f == 0f)
		{
			ChargeIdle = SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/BuzzsawIdle"), GunTipPosition);
		}
		if (Time % 3f == 0f)
		{
			Vector2 smokeVelocity = Vector2.UnitY * Main.rand.NextFloat(-7f, -12f);
			smokeVelocity = smokeVelocity.RotatedByRandom(0.39269909262657166);
			Color smokeColor = (Main.rand.NextBool() ? Main.DiscoColor : Color.Gray);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(GunTipPosition + Main.rand.NextVector2CircularEdge(3f, 3f), smokeVelocity, smokeColor, 30, 0.65f, 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), glowing: true));
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

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		hitbox = new Rectangle((int)GunTipPosition.X - 23, (int)GunTipPosition.Y - 23, 46, 46);
		if (Time / 120f >= 1f)
		{
			((Rectangle)(ref hitbox)).Inflate(72, 72);
		}
		else if (Time / 120f >= 0.25f)
		{
			((Rectangle)(ref hitbox)).Inflate(32, 32);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Laceration>(), 300);
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 150);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice");
		style.Volume = 0.7f;
		SoundEngine.PlaySound(in style, GunTipPosition);
		int SawLevel = (Time / 120f >= 1f).ToInt() + (Time / 120f >= 0.25f).ToInt();
		int onHitSparkAmount = 4 + 4 * SawLevel;
		for (int s = 0; s < onHitSparkAmount; s++)
		{
			Vector2 sparkVel = Main.rand.NextVector2CircularEdge(1f, 1f) * (Main.rand.NextFloat(6f, 10f) + 5f * (float)SawLevel);
			float sparkSize = 0.4f + Main.rand.NextFloat(0.3f, 0.6f) * (float)SawLevel;
			Color sparkColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(target.Center, sparkVel, affectedByGravity: false, 20, sparkSize, sparkColor));
		}
		for (int sq = 0; sq < 5; sq++)
		{
			Vector2 squareVel = Main.rand.NextVector2CircularEdge(1f, 1f) * (Main.rand.NextFloat(6f, 10f) + 5f * (float)SawLevel);
			float squareSize = 1.6f + Main.rand.NextFloat(1f, 1.6f) * (float)SawLevel;
			Color squareColor = Main.hslToRgb(Main.rand.NextFloat(), 0.6f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new SquareParticle(target.Center, squareVel, affectedByGravity: true, 20, squareSize, squareColor));
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
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		if (Holdout == null)
		{
			Holdout = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSlaughtererHoldout", (AssetRequestMode)2);
		}
		Texture2D value = Holdout.Value;
		if (LargeSlash == null)
		{
			LargeSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSawLargeSlash", (AssetRequestMode)2);
		}
		Texture2D largeSlashTexture = LargeSlash.Value;
		if (SmallSlash == null)
		{
			SmallSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSawSmallSlash", (AssetRequestMode)2);
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
		if (MiniSaw == null)
		{
			MiniSaw = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSlaughtererHoldoutMiniSaw", (AssetRequestMode)2);
		}
		Texture2D mini = MiniSaw.Value;
		Vector2 verticalOffset = Vector2.UnitY.RotatedBy(base.Projectile.rotation);
		if (Math.Cos(base.Projectile.rotation) < 0.0)
		{
			verticalOffset *= -1f;
		}
		Vector2 miniSawPosition = drawPosition - Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.125f + verticalOffset * 6f;
		Main.EntitySpriteDraw(mini, miniSawPosition, null, Color.White, Time * MathHelper.ToRadians(24f), mini.Size() * 0.5f, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		if (HoldoutGlow == null)
		{
			HoldoutGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SuperradiantSlaughtererHoldoutGlow", (AssetRequestMode)2);
		}
		Main.EntitySpriteDraw(HoldoutGlow.Value, drawPosition, frame, Color.White, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		if (NoSawOnHoldout)
		{
			return false;
		}
		if (Time > 30f)
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
