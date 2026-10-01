using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class NeptunesBountyProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public int ChargeupTime;

	public int Lifetime;

	public int startDamage;

	public bool setDamage;

	public int dustType1;

	public int dustType2;

	public bool spinMode;

	public bool spinMode2;

	public Vector2 NPCDestination;

	public SlotId SpinSoundSlot;

	public CalamityUtils.CurveSegment pullback;

	public CalamityUtils.CurveSegment throwout;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/NeptunesBounty";

	public float OverallProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ThrowProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ChargeProgress => 1f - (float)(base.Projectile.timeLeft - Lifetime) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 135;
		base.Projectile.height = 135;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0816: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0888: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_093c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_0974: Unknown result type (might be due to invalid IL or missing references)
		//IL_098d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(SpinSoundSlot, out ActiveSound SpinSound) && SpinSound.IsPlaying)
		{
			SpinSound.Position = base.Projectile.Center;
		}
		float playerDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		Time++;
		base.Projectile.spriteDirection = base.Projectile.direction;
		Vector3 Light = (spinMode2 ? new Vector3(0.2f, 0.2f, 0.255f) : new Vector3(0.07f, 0.07f, 0.25f));
		Lighting.AddLight(base.Projectile.Center, Light * 4f);
		if (ChargeProgress < 1f)
		{
			Owner.ChangeDir(MathF.Sign(Main.MouseWorld.X - Owner.Center.X));
			float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
			Owner.heldProj = base.Projectile.whoAmI;
			base.Projectile.spriteDirection = Owner.direction;
			base.Projectile.direction = Owner.direction;
			base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -90f * Owner.gravDir + new Vector2((float)((Owner.direction == 1) ? 10 : 3), 0f);
			base.Projectile.rotation = (-(float)Math.PI / 4f * (float)base.Projectile.direction + armRotation) * Owner.gravDir;
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
			return;
		}
		if (base.Projectile.timeLeft == Lifetime)
		{
			base.Projectile.netUpdate = true;
			SoundEngine.PlaySound(in SoundID.Item1, base.Projectile.Center);
			base.Projectile.Center = Owner.MountedCenter + (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 12f;
			base.Projectile.velocity = (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 28f;
			startDamage = base.Projectile.damage;
			base.Projectile.spriteDirection = base.Projectile.direction;
			SpinSoundSlot = SoundEngine.PlaySound(in NeptunesBounty.SpinSound, base.Projectile.Center);
			Time = 0;
			spinMode = true;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.direction = 1;
		}
		else
		{
			base.Projectile.direction = -1;
		}
		if (Time >= 75 && !spinMode2 && base.Projectile.velocity.Y >= 22f)
		{
			base.Projectile.extraUpdates = 3;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/VividClarityBeamAppear");
			style.Volume = 0.65f;
			style.PitchVariance = 0.3f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = SoundID.ShimmerWeak1 with
			{
				Pitch = 0.15f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.Aqua, new Vector2(2f, 2f), Main.rand.NextFloat(12f, 25f), 0.01f, 0.9f, 22));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.DodgerBlue, new Vector2(2f, 2f), Main.rand.NextFloat(12f, 25f), 0.01f, 0.83f, 15));
			base.Projectile.damage = startDamage * 2;
			Time = 0;
			bool foundTarget = false;
			NPC target = Owner.ClampedMouseWorld().ClosestNPCAt(1000f);
			if (target != null)
			{
				NPCDestination = target.Center + target.velocity * 5f;
				foundTarget = true;
			}
			else
			{
				foundTarget = false;
			}
			if (!foundTarget)
			{
				base.Projectile.velocity = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX * (float)base.Projectile.direction) * 25f;
			}
			else
			{
				base.Projectile.velocity = (NPCDestination - base.Projectile.Center).SafeNormalize(Vector2.UnitX * (float)base.Projectile.direction) * 25f;
			}
			SpinSound?.Stop();
			spinMode2 = true;
			base.Projectile.numHits = 0;
		}
		if (!spinMode)
		{
			return;
		}
		if (!spinMode2)
		{
			if (base.Projectile.velocity.Y < 22f)
			{
				base.Projectile.velocity.Y += 0.42f;
			}
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.X *= 0.975f;
			}
			if (playerDist < 1400f)
			{
				for (int i = 0; i < 2; i++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + ((float)i * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 75f, ((float)i * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2().RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(5f, 22f), "CalamityMod/Particles/WaterFoam", affectedByGravity: false, Main.rand.Next(7, 16), Main.rand.NextFloat(0.4f, 0.7f), Color.DodgerBlue * 0.45f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-10f, 10f)));
				}
			}
			if (Time % 7 == 0)
			{
				Vector2 velDirection = Utils.RotatedByRandom(new Vector2(80f, 80f), 100.0);
				Vector2 location = base.Projectile.Center + velDirection;
				bool foundTarget2 = false;
				for (int j = 0; j < Main.maxNPCs; j++)
				{
					if (Main.npc[j].CanBeChasedBy(base.Projectile.GetSource_FromThis()))
					{
						NPCDestination = Main.npc[j].Center + Main.npc[j].velocity * 5f;
					}
					foundTarget2 = !(NPCDestination == new Vector2(0f, 0f));
				}
				Vector2 velocity = (foundTarget2 ? ((NPCDestination - location).SafeNormalize(Vector2.UnitX * (float)base.Projectile.direction) * 25f) : (velDirection.SafeNormalize(Vector2.UnitX * (float)base.Projectile.direction) * 25f));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), location, velocity, ModContent.ProjectileType<NeptunesBountySplitProjectile>(), startDamage / 6, base.Projectile.knockBack / 4f, base.Projectile.owner);
				for (int k = 0; k < 6; k++)
				{
					Dust dust = Dust.NewDustPerfect(location, Main.rand.NextBool(3) ? dustType1 : dustType2);
					dust.noGravity = true;
					dust.scale = Main.rand.NextFloat(1.5f, 1.8f);
					dust.velocity = velocity.RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.1f, 0.5f);
				}
			}
		}
		else
		{
			if (Time == 1)
			{
				SoundStyle style = NeptunesBounty.SpinSound with
				{
					Pitch = 0.2f
				};
				SpinSoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			if (playerDist < 1400f)
			{
				for (int l = 0; l < 2; l++)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + ((float)l * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 75f, ((float)l * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2().RotatedByRandom(0.25) * Main.rand.NextFloat(5f, 22f) - base.Projectile.velocity * 2f, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, Main.rand.Next(10, 13), Main.rand.NextFloat(0.2f, 0.55f), Color.DodgerBlue * 0.3f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-10f, 10f)));
				}
				if (Main.rand.NextBool(3))
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Main.rand.NextVector2Circular(60f, 60f), -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 1f), "CalamityMod/Particles/Sparkle", affectedByGravity: false, Main.rand.Next(24, 36), Main.rand.NextFloat(1.7f, 1.85f), Color.DodgerBlue, new Vector2(0.4f, 1f)));
				}
			}
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.rotation += 0.6f * (MathF.Abs(base.Projectile.velocity.Y) * 0.03f + 0.85f) * (float)base.Projectile.direction;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 300);
		if (spinMode2 && base.Projectile.numHits == 0)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 15, 0.13f, Color.DodgerBlue, new Vector2(1.5f, 1f), quickShrink: true, glow: false, 0.8f));
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 15, 0.1f, Color.Aqua, new Vector2(1.5f, 1f), quickShrink: true, glow: false, 0.8f));
			for (int i = 0; i <= 20; i++)
			{
				Dust.NewDustPerfect(target.Center + Main.rand.NextVector2Circular(9f, 9f), Main.rand.NextBool() ? 307 : 180, base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1.2f), 0, default(Color), Main.rand.NextFloat(0.9f, 1.45f)).noGravity = true;
				Dust.NewDustPerfect(target.Center + Main.rand.NextVector2Circular(9f, 9f), Main.rand.NextBool() ? 307 : 180, -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1.2f), 0, default(Color), Main.rand.NextFloat(0.9f, 1.45f)).noGravity = true;
			}
			SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Item/HellkiteSmallHit", 3);
			soundStyle.Volume = 0.45f;
			soundStyle.Pitch = 0.2f;
			SoundStyle HitSound2 = soundStyle;
			SoundEngine.PlaySound(in HitSound2, base.Projectile.Center);
			soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice");
			soundStyle.Volume = 0.65f;
			soundStyle.Pitch = -0.3f;
			SoundEngine.PlaySound(in soundStyle, base.Projectile.Center);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(SpinSoundSlot, out ActiveSound SpinSound))
		{
			SpinSound?.Stop();
		}
		for (int i = 0; i < 40; i++)
		{
			float dustMulti = Main.rand.NextFloat(0.3f, 1.5f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? dustType1 : dustType2);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.6f, 2.5f) - dustMulti;
			dust.velocity = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.3f, 1f) * dustMulti;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		Color val;
		if (spinMode2)
		{
			Projectile projectile = base.Projectile;
			int mode = ProjectileID.Sets.TrailingMode[base.Type];
			val = Color.Aqua;
			((Color)(ref val)).A = 0;
			CalamityUtils.DrawAfterimagesCentered(projectile, mode, val * 0.6f);
		}
		Asset<Texture2D> p = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearSmokey", (AssetRequestMode)2);
		Asset<Texture2D> p2 = ModContent.Request<Texture2D>("CalamityMod/Particles/SemiCircularSmearSwipe", (AssetRequestMode)2);
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		if (spinMode)
		{
			Texture2D value = p2.Value;
			val = (spinMode2 ? Color.Aqua : Color.DeepSkyBlue);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, generalDrawPos, null, val * 0.55f, base.Projectile.rotation * Main.rand.NextFloat(1.6f, 1.7f), p2.Size() * 0.5f, (spinMode2 ? 1.6f : 1.4f) * Main.rand.NextFloat(0.8f, 1.15f), (SpriteEffects)0);
			Texture2D value2 = p.Value;
			val = (spinMode2 ? Color.Aqua : Color.DeepSkyBlue);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, generalDrawPos, null, val * 0.75f, base.Projectile.rotation * Main.rand.NextFloat(1.2f, 1.3f), p.Size() * 0.5f, spinMode2 ? 1.4f : 1.2f, (SpriteEffects)0);
		}
		return true;
	}

	public NeptunesBountyProjectile()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		ChargeupTime = 25;
		Lifetime = 300;
		dustType1 = 103;
		dustType2 = 172;
		NPCDestination = new Vector2(0f, 0f);
		pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);
		throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);
		base._002Ector();
	}
}
