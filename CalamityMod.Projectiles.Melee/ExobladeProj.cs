using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ExobladeProj : ModProjectile, ILocalizedModType, IModType
{
	public enum SwingState
	{
		Swinging,
		BonkDash
	}

	private const float BladeLength = 180f;

	public static float MaxSwingAngle = (float)Math.PI * 9f / 10f;

	public CalamityUtils.CurveSegment SlowStart = new CalamityUtils.CurveSegment(CalamityUtils.PolyOutEasing, 0f, -1f, 0.3f, 2);

	public CalamityUtils.CurveSegment SwingFast = new CalamityUtils.CurveSegment(CalamityUtils.PolyInEasing, 0.27f, -0.7f, 1.6f, 4);

	public CalamityUtils.CurveSegment EndSwing = new CalamityUtils.CurveSegment(CalamityUtils.PolyOutEasing, 0.85f, 0.9f, 0.1f, 2);

	public CalamityUtils.CurveSegment GoBack = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0f, -10f, -14f);

	public static Asset<Texture2D> LensFlare;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Exoblade>();

	public Player Owner => Main.player[base.Projectile.owner];

	public int GetSwingTime
	{
		get
		{
			if (State == SwingState.BonkDash)
			{
				return Exoblade.DashTime * base.Projectile.extraUpdates;
			}
			return 78;
		}
	}

	public float Timer => SwingTime - (float)base.Projectile.timeLeft;

	public float Progression => Timer / SwingTime;

	public float LungeProgression
	{
		get
		{
			if (!(Progression < 1f - Exoblade.PercentageOfAnimationSpentLunging))
			{
				return (Progression - (1f - Exoblade.PercentageOfAnimationSpentLunging)) / Exoblade.PercentageOfAnimationSpentLunging;
			}
			return 0f;
		}
	}

	public SwingState State
	{
		get
		{
			if (base.Projectile.ai[0] == 1f)
			{
				return SwingState.BonkDash;
			}
			return SwingState.Swinging;
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public bool PerformingPowerfulSlash => base.Projectile.ai[0] > 1f;

	public bool InPostBonkStasis
	{
		get
		{
			return base.Projectile.ai[1] > 0f;
		}
		set
		{
			base.Projectile.ai[1] = (value ? 1 : 0);
		}
	}

	public ref float SwingTime => ref base.Projectile.localAI[0];

	public ref float SquishFactor => ref base.Projectile.localAI[1];

	public float IdealSize
	{
		get
		{
			if (!PerformingPowerfulSlash)
			{
				return 1f;
			}
			return Exoblade.BigSlashUpscaleFactor;
		}
	}

	public int Direction
	{
		get
		{
			if (Math.Sign(base.Projectile.velocity.X) > 0)
			{
				return 1;
			}
			return -1;
		}
	}

	public float BaseRotation
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.velocity.ToRotation();
		}
	}

	public Vector2 SquishVector
	{
		get
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(1f + (1f - SquishFactor) * 0.6f, SquishFactor);
		}
	}

	public float SwingAngleShift => SwingAngleShiftAtProgress(Progression);

	public float SwordRotation => SwordRotationAtProgress(Progression);

	public float CurrentSquish => SquishAtProgress(Progression);

	public Vector2 SwordDirection
	{
		get
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return DirectionAtProgress(Progression);
		}
	}

	public float TrailEndProgression
	{
		get
		{
			float endProgression = ((!(Progression < 0.75f)) ? (Progression - 0.4f * (1f - (Progression - 0.75f) / 0.75f)) : (Progression - 0.5f + 0.1f * (Progression / 0.75f)));
			return Math.Clamp(endProgression, 0f, 1f);
		}
	}

	public CalamityUtils.CurveSegment AndThrust => new CalamityUtils.CurveSegment(CalamityUtils.PolyOutEasing, 1f - Exoblade.PercentageOfAnimationSpentLunging, -10f, 12f, 5);

	public float DashDisplace => CalamityUtils.PiecewiseAnimation(Progression, GoBack, AndThrust);

	public float RiskOfDust
	{
		get
		{
			if (Progression > 0.85f)
			{
				return 0f;
			}
			if (Progression < 0.4f)
			{
				return (float)Math.Pow(Progression / 0.3f, 2.0) * 0.2f;
			}
			if (Progression < 0.5f)
			{
				return 0.2f + 0.7f * (Progression - 0.4f) / 0.1f;
			}
			return 0.9f;
		}
	}

	public override string Texture => "CalamityMod/Items/Weapons/Melee/ExobladeSquare";

	public float SwingAngleShiftAtProgress(float progress)
	{
		if (State != SwingState.BonkDash)
		{
			return MaxSwingAngle * CalamityUtils.PiecewiseAnimation(progress, SlowStart, SwingFast, EndSwing);
		}
		return 0f;
	}

	public float SwordRotationAtProgress(float progress)
	{
		if (State != SwingState.BonkDash)
		{
			return BaseRotation + SwingAngleShiftAtProgress(progress) * (float)Direction;
		}
		return BaseRotation;
	}

	public float SquishAtProgress(float progress)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (State != SwingState.BonkDash)
		{
			return MathHelper.Lerp(SquishVector.X, SquishVector.Y, (float)Math.Abs(Math.Sin(SwingAngleShiftAtProgress(progress))));
		}
		return 1f;
	}

	public Vector2 DirectionAtProgress(float progress)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (State != SwingState.BonkDash)
		{
			return SwordRotationAtProgress(progress).ToRotationVector2() * SquishAtProgress(progress);
		}
		return base.Projectile.velocity;
	}

	public float RealProgressionAtTrailCompletion(float completion)
	{
		return MathHelper.Lerp(Progression, TrailEndProgression, completion);
	}

	public Vector2 DirectionAtProgressScuffed(float progress)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		float angleShift = SwingAngleShiftAtProgress(progress);
		Vector2 anglePoint = angleShift.ToRotationVector2();
		anglePoint.X *= SquishVector.X;
		anglePoint.Y *= SquishVector.Y;
		angleShift = anglePoint.ToRotation();
		return (BaseRotation + angleShift * (float)Direction).ToRotationVector2() * SquishAtProgress(progress);
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 120;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 98);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 9999;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 8;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(SwingTime);
		writer.Write(SquishFactor);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		SwingTime = reader.ReadSingle();
		SquishFactor = reader.ReadSingle();
	}

	public override bool ShouldUpdatePosition()
	{
		if (State == SwingState.BonkDash)
		{
			return !InPostBonkStasis;
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (State != SwingState.BonkDash)
		{
			return null;
		}
		if (InPostBonkStasis)
		{
			return false;
		}
		if ((float)base.Projectile.timeLeft > SwingTime * Exoblade.PercentageOfAnimationSpentLunging)
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		Vector2 start = base.Projectile.Center;
		Vector2 end = start + SwordDirection * 230f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, (State == SwingState.BonkDash) ? (base.Projectile.scale * 45f) : (base.Projectile.scale * 30f), ref _);
	}

	public void InitializationEffects(bool startInitialization)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Owner.MountedCenter.DirectionTo(Owner.Calamity().mouseWorld);
		SquishFactor = Main.rand.NextFloat(0.67f, 1f);
		if (startInitialization && State != SwingState.BonkDash)
		{
			base.Projectile.scale = 0.02f;
		}
		else
		{
			base.Projectile.scale = 1f;
			if (PerformingPowerfulSlash)
			{
				State = SwingState.Swinging;
			}
		}
		if (PerformingPowerfulSlash)
		{
			SquishFactor = 0.7f;
		}
		SwingTime = GetSwingTime;
		base.Projectile.timeLeft = (int)SwingTime;
		base.Projectile.ForceNetUpdate();
	}

	public override void AI()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (!InPostBonkStasis && base.Projectile.timeLeft != 0)
		{
			if (base.Projectile.timeLeft >= 9999 || (base.Projectile.timeLeft == 1 && Owner.channel && State != SwingState.BonkDash))
			{
				InitializationEffects(base.Projectile.timeLeft >= 9999);
			}
			switch (State)
			{
			case SwingState.Swinging:
				DoBehavior_Swinging();
				break;
			case SwingState.BonkDash:
				DoBehavior_BonkDash();
				break;
			}
			base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
			Owner.heldProj = base.Projectile.whoAmI;
			Owner.SetDummyItemTime(2);
			Owner.ChangeDir(Direction);
			float armRotation = SwordRotation - (float)Math.PI / 2f;
			Owner.SetCompositeArmFront(Math.Abs(armRotation) > 0.01f, Player.CompositeArmStretchAmount.Full, armRotation);
			if (base.Projectile.timeLeft == 1 && State == SwingState.BonkDash && !InPostBonkStasis)
			{
				base.Projectile.timeLeft = Exoblade.LungeCooldown;
				InPostBonkStasis = true;
				Owner.fullRotation = 0f;
				Owner.Calamity().LungingDown = false;
			}
		}
	}

	public void DoBehavior_Swinging()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == (int)(SwingTime / 5f))
		{
			SoundEngine.PlaySound(PerformingPowerfulSlash ? Exoblade.BigSwingSound : Exoblade.SwingSound, base.Projectile.Center);
		}
		Vector2 position = Owner.MountedCenter + SwordDirection * 100f;
		Color newColor = Color.Lerp(Color.GreenYellow, Color.DeepPink, (float)Math.Pow(Progression, 3.0));
		Lighting.AddLight(position, ((Color)(ref newColor)).ToVector3() * 1.6f * (float)Math.Sin(Progression * (float)Math.PI));
		if (base.Projectile.scale < IdealSize)
		{
			base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, IdealSize, 0.08f);
		}
		if (!Owner.channel && Progression > 0.7f)
		{
			base.Projectile.scale = (0.5f + 0.5f * (float)Math.Pow(1f - (Progression - 0.7f) / 0.3f, 0.5)) * IdealSize;
		}
		if (Main.rand.NextFloat() * 3f < RiskOfDust)
		{
			Vector2 position2 = Owner.MountedCenter + SwordDirection * 180f * base.Projectile.scale * (float)Math.Pow(Main.rand.NextFloat(0.5f, 1f), 0.5);
			int type = ModContent.DustType<AuricBarDust>();
			Vector2? velocity = SwordDirection.RotatedBy(-(float)Math.PI / 2f * (float)Direction) * 2f;
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position2, type, velocity, 0, newColor);
			dust.noGravity = true;
			dust.alpha = 10;
			dust.scale = 0.5f;
		}
		if (Main.rand.NextFloat() < RiskOfDust)
		{
			Color dustColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.9f);
			Dust dust2 = Dust.NewDustPerfect(Owner.MountedCenter + SwordDirection * 180f * base.Projectile.scale * (float)Math.Pow(Main.rand.NextFloat(0.2f, 1f), 0.5), 267, SwordDirection.RotatedBy((float)Math.PI / 2f * (float)Direction) * 2.6f, 0, dustColor);
			dust2.scale = 0.3f;
			dust2.fadeIn = Main.rand.NextFloat() * 1.2f;
			dust2.noGravity = true;
		}
		int beamShootStart = (int)(SwingTime * 0.6f);
		int beamShootPeriod = (int)(SwingTime * 0.4f);
		int beamShootEnd = beamShootStart + beamShootPeriod;
		beamShootPeriod /= Exoblade.BeamsPerSwing - 1;
		if (Main.myPlayer == base.Projectile.owner && Timer >= (float)beamShootStart && Timer < (float)beamShootEnd && (Timer - (float)beamShootStart) % (float)beamShootPeriod == 0f)
		{
			_ = (Timer - (float)beamShootStart) / (float)beamShootPeriod;
			int boltDamage = (int)((float)base.Projectile.damage * Exoblade.NotTrueMeleeDamagePenalty);
			Vector2 boltVelocity = base.Projectile.velocity.RotatedByRandom(0.235619455575943);
			boltVelocity *= Owner.HeldItem.shootSpeed;
			Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center + boltVelocity * 5f, boltVelocity, ModContent.ProjectileType<Exobeam>(), boltDamage, base.Projectile.knockBack / 3f, base.Projectile.owner);
		}
	}

	public void DoBehavior_BonkDash()
	{
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		Owner.mount?.Dismount(Owner);
		Owner.RemoveAllGrapplingHooks();
		if (LungeProgression == 0f)
		{
			if (base.Projectile.timeLeft == 1 + (int)(SwingTime * Exoblade.PercentageOfAnimationSpentLunging))
			{
				SoundEngine.PlaySound(in Exoblade.DashSound, base.Projectile.Center);
			}
			base.Projectile.velocity = Owner.MountedCenter.DirectionTo(Owner.Calamity().mouseWorld);
			base.Projectile.oldPos = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				base.Projectile.oldPos[i] = base.Projectile.position;
			}
		}
		else
		{
			float rotationStrength = (float)Math.PI / 80f * (float)Math.Pow(LungeProgression, 3.0);
			float currentRotation = base.Projectile.velocity.ToRotation();
			float idealRotation = Owner.MountedCenter.DirectionTo(Owner.Calamity().mouseWorld).ToRotation();
			base.Projectile.velocity = currentRotation.AngleTowards(idealRotation, rotationStrength).ToRotationVector2();
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			float velocityPower = (float)Math.Sin((float)Math.PI * LungeProgression);
			velocityPower = (float)Math.Pow(Math.Abs(velocityPower), 0.6000000238418579);
			Vector2 newVelocity = base.Projectile.velocity * Exoblade.LungeSpeed * (0.24f + 0.76f * velocityPower);
			Owner.velocity = newVelocity;
			Owner.Calamity().LungingDown = true;
			if (Main.rand.NextBool())
			{
				Color dustColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.9f);
				Dust dust = Dust.NewDustPerfect(Owner.MountedCenter + Main.rand.NextVector2Circular(20f, 20f), 267, SwordDirection * -2.6f, 0, dustColor);
				dust.scale = 0.3f;
				dust.fadeIn = Main.rand.NextFloat() * 1.2f;
				dust.noGravity = true;
			}
			if (Main.rand.NextBool(6) && LungeProgression < 0.8f)
			{
				Vector2 particleSpeed = SwordDirection * -1f * Main.rand.NextFloat(6f, 10f);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(Owner.MountedCenter + Main.rand.NextVector2Circular(20f, 20f) + Owner.velocity * 5f, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.GreenYellow, 30, 3.4f, 4.5f, 3f, 0.02f));
			}
			if (Main.rand.NextBool(5) && LungeProgression >= 0.8f)
			{
				Vector2 particleSpeed2 = SwordDirection * -1f * Main.rand.NextFloat(6f, 10f);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(Owner.MountedCenter + Main.rand.NextVector2Circular(50f, 50f) + Owner.velocity * 4f, particleSpeed2, Main.rand.NextFloat(0.3f, 0.6f), Color.GreenYellow, 30, 3.4f, 4.5f, 3f, 0.02f));
			}
		}
		if (base.Projectile.timeLeft == 1)
		{
			Player owner = Owner;
			owner.velocity *= 0.2f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f * (float)Direction;
	}

	public float SlashWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return SquishAtProgress(RealProgressionAtTrailCompletion(completionRatio)) * base.Projectile.scale * 60.5f;
	}

	public Color SlashColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lime * Utils.GetLerpValue(0.9f, 0.4f, completionRatio, clamped: true) * base.Projectile.Opacity;
	}

	public float PierceWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return Utils.GetLerpValue(0f, 0.2f, completionRatio, clamped: true) * base.Projectile.scale * 50f * (1f - (float)Math.Pow(LungeProgression, 5.0));
	}

	public Color PierceColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lime * base.Projectile.Opacity;
	}

	public List<Vector2> GenerateSlashPoints()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2> result = new List<Vector2>();
		for (int i = 0; i < 40; i++)
		{
			float progress = MathHelper.Lerp(Progression, TrailEndProgression, (float)i / 40f);
			result.Add(DirectionAtProgressScuffed(progress) * 174f * base.Projectile.scale);
		}
		return result;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (base.Projectile.Opacity <= 0f || InPostBonkStasis)
		{
			return false;
		}
		DrawSlash();
		DrawPierceTrail();
		DrawBlade();
		return false;
	}

	public void DrawSlash()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		if (State == SwingState.Swinging && !(Progression < 0.45f))
		{
			Main.spriteBatch.EnterShaderRegion();
			GameShaders.Misc["CalamityMod:ExobladeSlash"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoronoiShapes", (AssetRequestMode)2));
			GameShaders.Misc["CalamityMod:ExobladeSlash"].UseColor(new Color(105, 240, 220));
			GameShaders.Misc["CalamityMod:ExobladeSlash"].UseSecondaryColor(new Color(57, 46, 115));
			EffectParameter obj = GameShaders.Misc["CalamityMod:ExobladeSlash"].Shader.Parameters["fireColor"];
			Color val = new Color(242, 112, 72);
			obj.SetValue(((Color)(ref val)).ToVector3());
			GameShaders.Misc["CalamityMod:ExobladeSlash"].Shader.Parameters["flipped"].SetValue(Direction == 1);
			GameShaders.Misc["CalamityMod:ExobladeSlash"].Apply();
			PrimitiveRenderer.RenderTrail(GenerateSlashPoints(), new PrimitiveSettings(SlashWidthFunction, SlashColorFunction, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Center;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladeSlash"]), 95);
			Main.spriteBatch.ExitShaderRegion();
		}
	}

	public void DrawPierceTrail()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		if (State == SwingState.BonkDash)
		{
			Main.spriteBatch.EnterShaderRegion();
			Color mainColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 2f % 1f, Color.Cyan, Color.Lime, Color.GreenYellow, Color.Goldenrod, Color.Orange);
			Color secondaryColor = CalamityUtils.MulticolorLerp((Main.GlobalTimeWrappedHourly * 2f + 0.2f) % 1f, Color.Cyan, Color.Lime, Color.GreenYellow, Color.Goldenrod, Color.Orange);
			mainColor = Color.Lerp(Color.White, mainColor, 0.4f + 0.6f * (float)Math.Pow(LungeProgression, 0.5));
			secondaryColor = Color.Lerp(Color.White, secondaryColor, 0.4f + 0.6f * (float)Math.Pow(LungeProgression, 0.5));
			Vector2 trailOffset = (base.Projectile.rotation - (float)Direction * ((float)Math.PI / 4f)).ToRotationVector2() * 98f + base.Projectile.Size * 0.5f;
			GameShaders.Misc["CalamityMod:ExobladePierce"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/EternityStreak", (AssetRequestMode)2));
			GameShaders.Misc["CalamityMod:ExobladePierce"].UseImage2("Images/Extra_189");
			GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(mainColor);
			GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(secondaryColor);
			GameShaders.Misc["CalamityMod:ExobladePierce"].Apply();
			int numPointsRendered = 30;
			int numPointsProvided = 60;
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos.Take(numPointsProvided).ToArray(), new PrimitiveSettings(PierceWidthFunction, PierceColorFunction, delegate
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return trailOffset;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), numPointsRendered);
			Main.spriteBatch.ExitShaderRegion();
		}
	}

	public void DrawBlade()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		SpriteEffects direction = (SpriteEffects)(Direction == -1);
		Color val;
		if (State == SwingState.Swinging)
		{
			Effect swingFX = Filters.Scene["CalamityMod:SwingSprite"].GetShader().Shader;
			swingFX.Parameters["rotation"].SetValue(SwingAngleShift + (float)Math.PI / 4f + ((Direction == -1) ? ((float)Math.PI) : 0f));
			swingFX.Parameters["pommelToOriginPercent"].SetValue(0.05f);
			EffectParameter obj = swingFX.Parameters["color"];
			val = Color.White;
			obj.SetValue(((Color)(ref val)).ToVector4());
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, swingFX, Main.GameViewMatrix.TransformationMatrix);
			Main.EntitySpriteDraw(texture, Owner.MountedCenter - Main.screenPosition, null, Color.White, BaseRotation, texture.Size() / 2f, SquishVector * 3f * base.Projectile.scale, direction);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			if (LensFlare == null)
			{
				LensFlare = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2);
			}
			Texture2D shineTex = LensFlare.Value;
			Vector2 shineScale = default(Vector2);
			((Vector2)(ref shineScale))._002Ector(1f, 3f);
			float lensFlareOpacity = ((Progression < 0.3f) ? 0f : (0.2f + 0.8f * (float)Math.Sin((float)Math.PI * (Progression - 0.3f) / 0.7f))) * 0.6f;
			Color lensFlareColor = Color.Lerp(Color.LimeGreen, Color.Plum, (float)Math.Pow(Progression, 3.0));
			((Color)(ref lensFlareColor)).A = 0;
			Main.EntitySpriteDraw(shineTex, Owner.MountedCenter + DirectionAtProgressScuffed(Progression) * base.Projectile.scale * 180f - Main.screenPosition, null, lensFlareColor * lensFlareOpacity, (float)Math.PI / 2f, shineTex.Size() / 2f, shineScale * base.Projectile.scale, (SpriteEffects)0);
		}
		else
		{
			float rotation = BaseRotation + (float)Math.PI / 4f;
			Vector2 origin = default(Vector2);
			((Vector2)(ref origin))._002Ector(0f, (float)texture.Height);
			Vector2 drawPosition = base.Projectile.Center + base.Projectile.velocity * base.Projectile.scale * DashDisplace - Main.screenPosition;
			if (Direction == -1)
			{
				rotation += (float)Math.PI / 2f;
				origin.X = texture.Width;
			}
			base.Projectile.scale = MathHelper.Lerp(1f, 0.22f, MathF.Pow(LungeProgression, 7f));
			Main.EntitySpriteDraw(texture, drawPosition, null, Color.White, rotation, origin, base.Projectile.scale, direction);
			float energyPower = Utils.GetLerpValue(0f, 0.32f, Progression, clamped: true) * Utils.GetLerpValue(1f, 0.85f, Progression, clamped: true);
			for (int i = 0; i < 4; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 4f + BaseRotation).ToRotationVector2() * energyPower * base.Projectile.scale * 7f;
				SpriteBatch spriteBatch = Main.spriteBatch;
				Vector2 val2 = drawPosition + drawOffset;
				val = Color.Lerp(Color.Goldenrod, Color.MediumTurquoise, Progression);
				((Color)(ref val)).A = 0;
				spriteBatch.Draw(texture, val2, (Rectangle?)null, val * 0.16f, rotation, origin, base.Projectile.scale, direction, 0f);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		ItemLoader.OnHitNPC(Owner.HeldItem, Owner, target, in hit, damageDone);
		NPCLoader.OnHitByItem(target, Owner, Owner.HeldItem, in hit, damageDone);
		PlayerLoader.OnHitNPC(Owner, target, in hit, damageDone);
		if (State == SwingState.BonkDash)
		{
			Owner.itemAnimation = 0;
			Owner.velocity = Owner.SafeDirectionTo(target.Center) * (0f - Exoblade.ReboundSpeed);
			base.Projectile.timeLeft = Exoblade.OpportunityForBigSlash + Exoblade.LungeCooldown;
			InPostBonkStasis = true;
			base.Projectile.netUpdate = true;
			SoundEngine.PlaySound(in Exoblade.DashHitSound, target.Center);
			SoundStyle style = Exoblade.BeamHitSound with
			{
				Volume = Exoblade.BeamHitSound.Volume * 1.2f
			};
			SoundEngine.PlaySound(in style, target.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				int lungeHitDamage = (int)((float)base.Projectile.damage * Exoblade.LungeDamageFactor);
				for (int i = 0; i < 5; i++)
				{
					int slash = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), target.Center, base.Projectile.velocity * 0.1f, ModContent.ProjectileType<ExobeamSlashCreator>(), lungeHitDamage, 0f, base.Projectile.owner, target.whoAmI, 100f);
					if (Main.projectile.IndexInRange(slash))
					{
						Main.projectile[slash].timeLeft -= i * 4;
					}
				}
			}
			target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
		}
		if (State == SwingState.Swinging && PerformingPowerfulSlash && Owner.ownedProjectileCounts[ModContent.ProjectileType<Exoboom>()] < 1)
		{
			SoundEngine.PlaySound(in Exoblade.BigHitSound, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				int explosionDamage = (int)((float)base.Projectile.damage * Exoblade.ExplosionDamageFactor);
				Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), target.Center, Vector2.Zero, ModContent.ProjectileType<Exoboom>(), explosionDamage, 0f, base.Projectile.owner);
			}
			Owner.DoLifestealDirect(target, (int)Math.Round((double)hit.Damage * 0.04), 0.4f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		Owner.fullRotation = 0f;
		Owner.Calamity().LungingDown = false;
	}
}
