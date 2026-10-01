using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class ReaperProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int ChargeupTime;

	public int Lifetime;

	public bool spinning;

	public SlotId SpinSoundSlot;

	private Vector2 squash;

	public CalamityUtils.CurveSegment pullback;

	public CalamityUtils.CurveSegment throwout;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/TheOldReaper";

	public float OverallProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ThrowProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ChargeProgress => 1f - (float)(base.Projectile.timeLeft - Lifetime) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 100;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15 * base.Projectile.MaxUpdates;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool ShouldUpdatePosition()
	{
		return ChargeProgress >= 1f;
	}

	public override bool? CanDamage()
	{
		if (ChargeProgress < 1f)
		{
			return false;
		}
		return base.CanDamage();
	}

	internal float ArmAnticipationMovement()
	{
		return CalamityUtils.PiecewiseAnimation(ChargeProgress, pullback, throwout);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_094b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_089a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		base.Projectile.spriteDirection = base.Projectile.direction;
		if (SoundEngine.TryGetActiveSound(SpinSoundSlot, out ActiveSound SpinSound) && SpinSound.IsPlaying)
		{
			SpinSound.Position = base.Projectile.Center;
		}
		if (ChargeProgress < 1f)
		{
			Owner.ChangeDir(MathF.Sign(Main.MouseWorld.X - Owner.Center.X));
			float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
			Owner.heldProj = base.Projectile.whoAmI;
			base.Projectile.spriteDirection = Owner.direction;
			base.Projectile.direction = Owner.direction;
			base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -70f * Owner.gravDir + new Vector2((float)(14 * Owner.direction), 0f);
			base.Projectile.rotation = (-(float)Math.PI / 4f * (float)base.Projectile.direction + armRotation) * Owner.gravDir;
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
			time++;
			return;
		}
		if (base.Projectile.timeLeft == Lifetime)
		{
			base.Projectile.netUpdate = true;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SwingMid");
			style.Volume = 0.8f;
			style.Pitch = Main.rand.NextFloat(0.2f, 0.3f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.Center = Owner.MountedCenter + base.Projectile.velocity * 4f;
			if (base.Projectile.Calamity().stealthStrike)
			{
				base.Projectile.velocity = new Vector2(0.5f * (float)Owner.direction, -1f) * 18f;
			}
			else
			{
				base.Projectile.velocity = (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 15f;
			}
			base.Projectile.spriteDirection = base.Projectile.direction;
			SpinSoundSlot = SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/SpinningWoosh")
			{
				Pitch = -0.3f,
				Volume = 0.75f
			}, base.Projectile.Center);
			time = 0;
			spinning = true;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.direction = 1;
		}
		else
		{
			base.Projectile.direction = -1;
		}
		if (spinning)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 15f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.01f;
			}
			if (time == 0)
			{
				base.Projectile.rotation += Main.rand.NextFloat(0f, 10f) * (float)base.Projectile.direction;
			}
			squash = new Vector2(1.3f, 0.8f);
			base.Projectile.rotation += 0.2f * (float)base.Projectile.direction;
			if (targetDist < 1400f)
			{
				for (int i = 0; i < 2; i++)
				{
					float rot = Main.rand.NextFloat(-5.5f, 5.5f);
					float scale = Main.rand.NextFloat(1f, 1.15f);
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 18f, base.Projectile.velocity, Color.Chartreuse * Main.rand.NextFloat(0.68f, 0.75f), "CalamityMod/Particles/CircularSmearSmokey", squash.RotatedBy(rot), base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(150f) - rot, scale, scale, 3, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				if (time % 7 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(80f, 80f), -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f), affectedByGravity: false, 60, Main.rand.NextFloat(0.9f, 1.5f), Color.Chartreuse));
				}
				for (int j = 0; j < 2; j++)
				{
					_ = base.Projectile.rotation;
					_ = base.Projectile.direction;
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center + ((float)j * (float)Math.PI + base.Projectile.rotation * 0.3f + (float)Math.PI / 2f).ToRotationVector2() * 70f, 267);
					dust.noGravity = true;
					dust.scale = 0.8f;
					dust.color = Color.Chartreuse;
					dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.45f, 0.6f);
				}
				if (Main.rand.NextBool())
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(23f, 23f), Main.rand.NextBool(7) ? 28 : 215);
					dust2.noGravity = true;
					dust2.scale = Main.rand.NextFloat(0.9f, 1.3f);
					dust2.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.7f);
				}
			}
			if (base.Projectile.Calamity().stealthStrike)
			{
				if (time == 30)
				{
					SpinSound?.Stop();
					SoundStyle fire = new SoundStyle("CalamityMod/Sounds/Item/RadiationBurst");
					SpinSoundSlot = SoundEngine.PlaySound(fire with
					{
						Volume = 1f,
						Pitch = 0.3f
					}, base.Projectile.Center);
				}
				if (time >= 150)
				{
					NPC target = Owner.ClampedMouseWorld().ClosestNPCAt(2000f);
					if (time == 150)
					{
						base.Projectile.extraUpdates = 25;
					}
					if (target != null)
					{
						if (base.Projectile.numHits <= 0)
						{
							CalamityUtils.HomeInOnSelectedNPC(base.Projectile, target, ignoreTiles: true, 0.85f, 15f, 0.97f);
						}
					}
					else if (time == 150)
					{
						base.Projectile.velocity = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 15f;
					}
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-2f, -1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 7, 0.13f, Color.Lerp(Color.Green, Color.Chartreuse, 0.8f) * 0.65f, new Vector2(1f, 0.3f), quickShrink: true, glow: false, 1.3f));
				}
				else if (time > 30)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.955f;
					float fade = Utils.GetLerpValue(150f, 0f, time);
					float numberOfDusts = 2f;
					float rotFactor = 360f / numberOfDusts;
					for (int k = 0; (float)k < numberOfDusts; k++)
					{
						MathHelper.ToRadians((float)k * rotFactor);
						Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 250f, 0.04f);
						velOffset *= Main.rand.NextFloat(25f, 45f) * fade;
						GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + velOffset * 2.5f, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, affectedByGravity: false, (int)(14f - 5f * fade), Main.rand.NextFloat(1.1f, 1.25f) - 0.5f * fade, Color.Chartreuse));
						Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + velOffset * 2.5f, 278, -velOffset * Main.rand.NextFloat(0.08f, 0.12f) * 1.5f, 0, default(Color), Main.rand.NextFloat(0.4f, 0.6f));
						dust3.noGravity = true;
						dust3.color = Color.Chartreuse;
					}
				}
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike && time <= 151)
		{
			base.Projectile.numHits--;
		}
		if (base.Projectile.numHits == 0)
		{
			if (base.Projectile.Calamity().stealthStrike)
			{
				Vector2 rainSpot = base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 490f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/RadiationRain");
				style.Volume = 1f;
				style.Pitch = 0f;
				SoundEngine.PlaySound(in style, rainSpot);
				style = new SoundStyle("CalamityMod/Sounds/Item/ViperSpit");
				style.Volume = 1f;
				style.Pitch = -0.3f;
				SoundEngine.PlaySound(in style, rainSpot);
				for (int i = 0; i < 37; i++)
				{
					Dust dust = Dust.NewDustPerfect(rainSpot, 278);
					dust.velocity = base.Projectile.velocity.RotatedByRandom(100.0) * Main.rand.NextFloat(1f, 8f);
					dust.scale = Main.rand.NextFloat(0.45f, 0.75f);
					dust.noGravity = true;
					dust.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Green : Color.Chartreuse, 0.7f);
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(rainSpot, Vector2.Zero, Color.Chartreuse * 0.7f, "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.35f, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), rainSpot, Vector2.Zero, ModContent.ProjectileType<RadiationRain>(), (int)((double)base.Projectile.damage * 0.13), 0f, base.Projectile.owner, 0f, 0f, 100f);
			}
			else
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/RadiationBurst");
				style.Volume = 1f;
				style.Pitch = 0f;
				style.MaxInstances = -1;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + target.velocity * 32f, Vector2.Zero, ModContent.ProjectileType<RadiationBurst>(), base.Projectile.damage, base.Projectile.knockBack * 3f, base.Projectile.owner);
			}
		}
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 90);
		float minMult = 0.1f;
		int hitsToMinMult = 5;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(SpinSoundSlot, out ActiveSound SpinSound))
		{
			SpinSound?.Stop();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Main.EntitySpriteDraw(tex.Value, drawPos, null, spinning ? Color.White : Color.Lerp(lightColor, Color.White, Utils.GetLerpValue(0f, ChargeupTime, time, clamped: true)), base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection <= 0));
		Asset<Texture2D> p = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmear", (AssetRequestMode)2);
		if (spinning)
		{
			Texture2D value = p.Value;
			Vector2 position = drawPos + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f;
			Color chartreuse = Color.Chartreuse;
			((Color)(ref chartreuse)).A = 0;
			Main.EntitySpriteDraw(value, position, null, chartreuse * 0.45f, base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f, p.Size() * 0.5f, new Vector2(0.9f - 0.3f * Utils.GetLerpValue(25f, 0f, time, clamped: true), 1f + 0.6f * Utils.GetLerpValue(25f, 0f, time, clamped: true)) * Main.rand.NextFloat(1.25f, 1.4f), (SpriteEffects)0);
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.Chartreuse * 0.5f);
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 70f, targetHitbox);
	}

	public ReaperProjectile()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		ChargeupTime = 50;
		Lifetime = 500;
		squash = Vector2.One;
		pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);
		throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);
		base._002Ector();
	}
}
