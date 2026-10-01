using System;
using System.IO;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BladecrestOathswordThrownBlade : ModProjectile, ILocalizedModType, IModType
{
	[Flags]
	public enum State : byte
	{
		None = 0,
		HasSpawned = 1,
		Thrown = 2,
		HitTarget = 4,
		StuckInTarget = 8,
		LeftTarget = 0x10,
		StuckInGround = 0x20
	}

	public static int Lifetime = 100;

	public State CurrentState;

	public static int fadeOutTime = 60;

	public int stuckTimer;

	public Vector2 impalePos;

	public int bounces;

	public CalamityUtils.CurveSegment pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);

	public CalamityUtils.CurveSegment throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/BladecrestOathsword";

	public int ChargeupTime => (int)MathHelper.Clamp((float)Owner.HeldItem.useTime / 2.8f, 1f, 100f);

	public float OverallProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ThrowProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ChargeProgress => 1f - (float)(base.Projectile.timeLeft - Lifetime) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float time => ref base.Projectile.ai[0];

	public ref float stabOrder => ref base.Projectile.ai[1];

	public ref NPC stabbedTarget => ref Main.npc[(int)base.Projectile.ai[2]];

	public bool fadingOut => base.Projectile.timeLeft <= Lifetime - fadeOutTime;

	private Vector2 GetAimDirection()
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 val = (Owner.Calamity().mouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
			float aimAngle = val.ToRotation();
			if (Math.Abs(MathHelper.WrapAngle(aimAngle - base.Projectile.localAI[1])) > 0.02f)
			{
				base.Projectile.localAI[1] = aimAngle;
				if (base.Projectile.timeLeft % 6 == 0)
				{
					base.Projectile.netUpdate = true;
				}
			}
			return val;
		}
		return base.Projectile.localAI[1].ToRotationVector2();
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override bool ShouldUpdatePosition()
	{
		if (ChargeProgress >= 1f)
		{
			return (CurrentState & (State.StuckInTarget | State.StuckInGround)) == 0;
		}
		return false;
	}

	internal float ArmAnticipationMovement()
	{
		return CalamityUtils.PiecewiseAnimation(ChargeProgress, pullback, throwout);
	}

	public override void AI()
	{
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0964: Unknown result type (might be due to invalid IL or missing references)
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0934: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		if ((CurrentState & State.HasSpawned) == 0)
		{
			base.Projectile.timeLeft = Lifetime + ChargeupTime;
			CurrentState |= State.HasSpawned;
		}
		Color newColor;
		if ((CurrentState & State.HitTarget) != State.None)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC target = enumerator.Current;
				if ((CurrentState & State.LeftTarget) != State.None || !CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width / (((CurrentState & State.LeftTarget) != State.None) ? 0.5f : 1f), target.getRect()))
				{
					continue;
				}
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordImpact", 2);
				style.Volume = 0.75f;
				style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
				style.MaxInstances = 3;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.ai[2] = target.whoAmI;
				if (target.Calamity().demonSwordImpales < 0)
				{
					target.Calamity().demonSwordImpales = 0;
				}
				if (target.Calamity().demonSwordImpales >= 3)
				{
					float bladeValue = -1f;
					Projectile ejectedBlade = null;
					for (int x = 0; x < Main.maxProjectiles; x++)
					{
						Projectile projectile = Main.projectile[x];
						if (projectile.owner == base.Projectile.owner && projectile.type == base.Projectile.type && base.Projectile.localAI[0] != 5f && projectile.ai[2] == base.Projectile.ai[2] && projectile.timeLeft > Lifetime - fadeOutTime && (bladeValue == -1f || bladeValue > stabOrder))
						{
							bladeValue = projectile.ai[1];
							ejectedBlade = projectile;
						}
					}
					if (ejectedBlade != null)
					{
						ejectedBlade.localAI[0] = 5f;
						ejectedBlade.ai[1] += 1000f;
						ejectedBlade.velocity = base.Projectile.velocity.RotatedByRandom(0.20000000298023224);
						ejectedBlade.ForceNetUpdate();
					}
					for (int i = 0; i < 8; i++)
					{
						GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, base.Projectile.velocity.RotatedByRandom(0.6) * Main.rand.NextFloat(0.2f, 1f), affectedByGravity: false, 30, Main.rand.NextFloat(0.3f, 0.7f), Main.rand.NextBool(3) ? Color.Red : Color.Crimson));
						Vector2 center = base.Projectile.Center;
						int type = ModContent.DustType<LightDust>();
						Vector2? velocity = base.Projectile.velocity.RotatedByRandom(0.4) * Main.rand.NextFloat(0.3f, 0.8f);
						newColor = default(Color);
						Dust dust = Dust.NewDustPerfect(center, type, velocity, 0, newColor, Main.rand.NextFloat(1.3f, 1.6f));
						dust.noGravity = true;
						dust.noLight = true;
						dust.color = (Main.rand.NextBool(3) ? Color.Red : Color.Crimson);
					}
				}
				target.Calamity().demonSwordImpales++;
				CurrentState |= State.StuckInTarget;
				impalePos = base.Projectile.Center - stabbedTarget.Center;
				stuckTimer = 3600;
				break;
			}
			CurrentState &= ~State.HitTarget;
		}
		if ((CurrentState & State.Thrown) != State.None && (CurrentState & (State.StuckInTarget | State.StuckInGround)) == 0)
		{
			base.Projectile.extraUpdates = 1;
		}
		Vector2.Distance(Owner.Center, base.Projectile.Center);
		base.Projectile.spriteDirection = base.Projectile.direction;
		newColor = Color.Firebrick;
		Vector3 Light = ((Color)(ref newColor)).ToVector3();
		Lighting.AddLight(base.Projectile.Center, Light * 0.5f);
		if (base.Projectile.timeLeft == Lifetime && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 toMouse = GetAimDirection();
			base.Projectile.velocity = toMouse * 14f;
			base.Projectile.Center = Owner.MountedCenter + toMouse * 12f;
			base.Projectile.spriteDirection = base.Projectile.direction;
			CurrentState |= State.Thrown;
			time = 0f;
			base.Projectile.extraUpdates = 1;
			base.Projectile.tileCollide = true;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.direction = 1;
		}
		else
		{
			base.Projectile.direction = -1;
		}
		if ((CurrentState & State.Thrown) != State.None)
		{
			base.Projectile.spriteDirection = base.Projectile.direction;
			if ((CurrentState & State.StuckInGround) == 0 && (CurrentState & State.LeftTarget) != State.None)
			{
				base.Projectile.rotation += 0.35f * (MathF.Abs(base.Projectile.velocity.Y) * 0.03f + 0.85f) * Main.rand.NextFloat(0.7f, 1f) * (float)base.Projectile.direction * base.Projectile.Opacity;
			}
			else
			{
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f * (float)((base.Projectile.direction == 1) ? 1 : 3);
			}
			if (time > (float)fadeOutTime * 0.7f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.93f;
			}
			if ((CurrentState & State.StuckInTarget) != State.None)
			{
				base.Projectile.tileCollide = false;
				time--;
				base.Projectile.timeLeft++;
				if (stuckTimer > 0)
				{
					stuckTimer--;
				}
				else
				{
					Projectile projectile3 = base.Projectile;
					projectile3.velocity *= 0.01f;
					stabbedTarget.Calamity().demonSwordImpales--;
					CurrentState &= ~State.StuckInTarget;
					fadeOutEffect();
				}
				base.Projectile.Center = stabbedTarget.Center + impalePos;
				if (stabbedTarget.life <= 0 || stabbedTarget == null || base.Projectile.localAI[0] == 5f || !stabbedTarget.active)
				{
					base.Projectile.timeLeft = Lifetime;
					stabbedTarget.Calamity().demonSwordImpales--;
					CurrentState &= ~State.StuckInTarget;
					CurrentState |= State.LeftTarget;
					base.Projectile.tileCollide = true;
					base.Projectile.rotation += Main.rand.NextFloat(-1.5f, 1.5f);
					for (int j = 0; j < Main.maxNPCs; j++)
					{
						base.Projectile.localNPCImmunity[j] = 0;
					}
					base.Projectile.numHits = 0;
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfLargeHit", 3);
					style.Volume = 0.85f;
					style.Pitch = Main.rand.NextFloat(0.3f, 0.4f);
					style.MaxInstances = 3;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			if ((CurrentState & State.LeftTarget) != State.None && (CurrentState & State.StuckInGround) == 0 && base.Projectile.velocity.Y < 14f)
			{
				base.Projectile.velocity.Y += 0.1f * (float)((bounces <= 0) ? 1 : 4);
			}
			if (fadingOut)
			{
				fadeOutEffect();
			}
		}
		if (!fadingOut && (CurrentState & State.StuckInTarget) == 0 && (CurrentState & State.StuckInGround) == 0 && ChargeProgress >= 1f)
		{
			for (int k = 0; k < 2; k++)
			{
				Vector2 safeVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
				Vector2 dustVel = (((CurrentState & State.LeftTarget) != State.None) ? Vector2.One.RotatedByRandom(3.1415927410125732) : (safeVel.RotatedBy(MathHelper.ToRadians((float)(105 * ((k == 0) ? 1 : (-1))))).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(3f, 4f)));
				if (Main.rand.NextBool(3))
				{
					Vector2 position = base.Projectile.Center + safeVel.RotatedBy(((CurrentState & State.LeftTarget) != State.None) ? (base.Projectile.rotation - MathHelper.ToRadians((float)(base.Projectile.direction * 45))) : 0f) * (float)(((CurrentState & State.LeftTarget) != State.None) ? 45 : 60);
					int type2 = ModContent.DustType<LightDust>();
					Vector2? velocity2 = dustVel;
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(position, type2, velocity2, 0, newColor, (((CurrentState & State.LeftTarget) != State.None) ? 1.5f : 1f) * Main.rand.NextFloat(0.8f, 0.9f));
					dust2.noGravity = true;
					dust2.color = (Main.rand.NextBool() ? Color.Red : Color.Crimson);
					dust2.noLight = true;
					dust2.noLightEmittence = true;
					dust2.alpha = 100;
				}
			}
			if (Main.rand.NextBool(5))
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.2f, 0.6f), "CalamityMod/Particles/DemonSigilParticle", affectedByGravity: false, 17, Main.rand.NextFloat(0.2f, 0.3f), Color.Lerp(Color.Crimson, Color.Red, Main.rand.NextFloat(0f, 0.7f)) * 0.6f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-1f, 1f)));
			}
		}
		if (ChargeProgress < 1f)
		{
			throwAnimation();
			if (ChargeProgress >= 0.6f && time == 0f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordSwing", 2);
				style.Volume = 0.65f;
				style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				time++;
			}
		}
		else
		{
			time++;
		}
	}

	public void fadeOutEffect()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.tileCollide = false;
		if (base.Projectile.timeLeft > Lifetime - fadeOutTime)
		{
			base.Projectile.timeLeft = Lifetime - fadeOutTime;
		}
		base.Projectile.Opacity = Utils.GetLerpValue(0f, Lifetime - fadeOutTime, base.Projectile.timeLeft, clamped: true);
		Vector2 dustVel = Utils.RotatedByRandom(new Vector2(10f, 10f), Math.PI) * Main.rand.NextFloat(0.2f, 1f) * base.Projectile.Opacity;
		if (Main.rand.NextBool(4))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), dustVel, 0, default(Color), Main.rand.NextFloat(1.1f, 1.4f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.Red : Color.Crimson);
			dust.noLight = true;
			dust.noLightEmittence = true;
			dust.alpha = 100;
			dust.velocity += base.Projectile.velocity;
		}
	}

	public void throwAnimation()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimDirection = GetAimDirection();
		Owner.ChangeDir(MathF.Sign(aimDirection.X));
		float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
		Owner.heldProj = base.Projectile.whoAmI;
		base.Projectile.spriteDirection = Owner.direction;
		base.Projectile.direction = Owner.direction;
		base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -45f * Owner.gravDir;
		base.Projectile.rotation = (-(float)Math.PI / 4f * (float)base.Projectile.direction + armRotation) * Owner.gravDir;
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		CurrentState |= State.HitTarget;
		base.Projectile.netUpdate = true;
		base.Projectile.netSpam = 0;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.4f;
		int hitsToMinMult = 8;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult * (((CurrentState & State.LeftTarget) != State.None) ? 1.15f : 1f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if ((CurrentState & State.LeftTarget) != State.None)
		{
			if (bounces >= 1)
			{
				impaleGround(oldVelocity);
			}
			else
			{
				if (base.Projectile.velocity.X != oldVelocity.X)
				{
					base.Projectile.velocity.X = 0f - oldVelocity.X;
				}
				if (base.Projectile.velocity.Y != oldVelocity.Y)
				{
					base.Projectile.velocity.Y = 0f - oldVelocity.Y;
				}
				if (((Vector2)(ref base.Projectile.velocity)).Length() < 8f)
				{
					base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 8f;
				}
				base.Projectile.velocity.Y *= 0.85f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/CeramicImpact", 2);
				style.Volume = 0.35f;
				style.Pitch = Main.rand.NextFloat(-0.4f, -0.5f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				bounces++;
				base.Projectile.timeLeft = Lifetime;
			}
		}
		else
		{
			impaleGround(oldVelocity);
		}
		return false;
	}

	public void impaleGround(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = oldVelocity;
		CurrentState |= State.StuckInGround;
		base.Projectile.timeLeft = (int)((float)Lifetime - (float)fadeOutTime * 0.3f);
		base.Projectile.tileCollide = false;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordImpact", 2);
		style.Volume = 0.75f;
		style.Pitch = Main.rand.NextFloat(0.2f, 0.3f);
		style.MaxInstances = 3;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		base.Projectile.netUpdate = true;
	}

	public override bool? CanDamage()
	{
		if (ChargeProgress < 1f || fadingOut || (CurrentState & State.StuckInGround) != State.None || (CurrentState & State.StuckInTarget) != State.None || (base.Projectile.numHits > 0 && (CurrentState & State.LeftTarget) == 0))
		{
			return false;
		}
		return base.CanDamage();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		return (base.Projectile.numHits <= 0 || (CurrentState & State.LeftTarget) != State.None) && CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (float)base.Projectile.width / (((CurrentState & State.LeftTarget) != State.None) ? 0.5f : 1f), targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/BladecrestOathsword", (AssetRequestMode)2).Value;
		float fadeScale = 1f - base.Projectile.Opacity;
		Color val;
		for (int i = 0; i < 16; i++)
		{
			val = Color.Crimson;
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.4f * fadeScale * base.Projectile.Opacity;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 16f).ToRotationVector2() * 9f * fadeScale;
			Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + drawOffset, null, auraColor, base.Projectile.rotation, centerTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1));
		}
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		val = Color.Red;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(centerTexture, position, null, Color.Lerp(val, lightColor, base.Projectile.Opacity) * base.Projectile.Opacity, base.Projectile.rotation, centerTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1));
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write((byte)CurrentState);
		writer.Write(base.Projectile.rotation);
		writer.Write(base.Projectile.localAI[1]);
		writer.Write(impalePos.X);
		writer.Write(impalePos.Y);
		writer.Write(stuckTimer);
		writer.Write((short)bounces);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		CurrentState = (State)reader.ReadByte();
		base.Projectile.rotation = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		impalePos = new Vector2(reader.ReadSingle(), reader.ReadSingle());
		stuckTimer = reader.ReadInt32();
		bounces = reader.ReadInt16();
	}
}
