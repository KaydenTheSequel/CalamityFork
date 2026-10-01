using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class ViridVanguardBlade : ModProjectile, ILocalizedModType, IModType
{
	public enum ViridVanguardAIState
	{
		CircleOwner,
		HorizontalSlashes,
		VerticalPierceTeleport,
		RegularPierceSlashes,
		ChargingCircle,
		PhotonRipperZenithSlashes,
		PhotonRipperZenithEndlag,
		AxeCircle
	}

	public int BladeIndex;

	public VertexStrip TrailDrawer;

	public Vector2 ChargeStartingPosition;

	public float BladeAngleForPhripperZenith;

	public static List<ViridVanguardAIState> ActiveAttackStateList = new List<ViridVanguardAIState>
	{
		ViridVanguardAIState.ChargingCircle,
		ViridVanguardAIState.PhotonRipperZenithSlashes,
		ViridVanguardAIState.PhotonRipperZenithEndlag
	};

	private bool IsInDotRange;

	private bool ActiveAttacking;

	private Vector2 StoredAttackMousePos;

	public const int HorizontalSlashChargeTime = 14;

	public const float HorizontalSlashSpeed = 44f;

	public const int VerticalSlashChargeTime = 32;

	public const float VerticalSlashSpeed = 45f;

	public const float VerticalTeleportOffset = 850f;

	public const int PierceChargeAttackCycleTime = 44;

	public const float MaxTargetingDistance = 1550f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public int CurrentViridCount => (int)MathHelper.Max(1f, (float)Owner.ownedProjectileCounts[base.Type]);

	public float BladeHoverOffsetAngle => (float)Math.PI * 2f * (float)BladeIndex / (float)CurrentViridCount + Owner.Calamity().ViridVanguardRotation * (float)((!Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : (-1));

	public ViridVanguardAIState CurrentState
	{
		get
		{
			return (ViridVanguardAIState)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = (float)value;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float AITimer => ref base.Projectile.ai[1];

	public float ActiveTimer
	{
		get
		{
			return Owner.Calamity().ViridVanguardActiveCooldown;
		}
		set
		{
			Owner.Calamity().ViridVanguardActiveCooldown = value;
		}
	}

	public bool IsAxe => BladeIndex % 2 == 0;

	public bool DrawAsAxe
	{
		get
		{
			if (!IsInDotRange || CurrentState != ViridVanguardAIState.PhotonRipperZenithSlashes)
			{
				return IsAxe;
			}
			return !ActiveAttacking;
		}
	}

	public ref float BladeGleamInterpolant => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 45;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 120;
		base.Projectile.height = 120;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 14;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(BladeIndex);
		writer.WriteVector2(ChargeStartingPosition);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		BladeIndex = reader.ReadInt32();
		ChargeStartingPosition = reader.ReadVector2();
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void AI()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		HandleMinionBools();
		base.Projectile.MaxUpdates = 1;
		BladeGleamInterpolant = MathHelper.Lerp(BladeGleamInterpolant, 0f, 0.1f);
		if (BladeGleamInterpolant <= 0.02f)
		{
			BladeGleamInterpolant = 0f;
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1550f, Owner);
		switch (CurrentState)
		{
		case ViridVanguardAIState.CircleOwner:
			DoBehavior_CircleOwner(potentialTarget);
			break;
		case ViridVanguardAIState.HorizontalSlashes:
			DoBehavior_HorizontalSlashes(potentialTarget);
			break;
		case ViridVanguardAIState.VerticalPierceTeleport:
			DoBehavior_VerticalPierceTeleport(potentialTarget);
			break;
		case ViridVanguardAIState.RegularPierceSlashes:
			DoBehavior_RegularPierceSlashes(potentialTarget);
			break;
		case ViridVanguardAIState.ChargingCircle:
			DoBehavior_PhotonRipperZenithStartup(potentialTarget);
			break;
		case ViridVanguardAIState.PhotonRipperZenithSlashes:
			DoBehavior_PhotonRipperZenithActive(potentialTarget);
			break;
		case ViridVanguardAIState.PhotonRipperZenithEndlag:
			DoBehavior_PhotonRipperZenithEndlag(potentialTarget);
			break;
		case ViridVanguardAIState.AxeCircle:
			DoBehavior_AxeCircle(potentialTarget);
			break;
		}
		AITimer++;
		if (CurrentState == ViridVanguardAIState.PhotonRipperZenithEndlag)
		{
			base.Projectile.MaxUpdates = 1;
		}
	}

	public void DoBehavior_CircleOwner(NPC potentialTarget)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		if (ActiveTimer < 0f)
		{
			ActiveTimer = ViridVanguard.ActiveAttackCooldown;
		}
		_ = 300f / (float)CurrentViridCount;
		if (potentialTarget != null)
		{
			CurrentState = ((!IsAxe) ? ViridVanguardAIState.HorizontalSlashes : ViridVanguardAIState.AxeCircle);
			AITimer = 0f;
			base.Projectile.netUpdate = true;
			return;
		}
		float hoverDistMult = MathHelper.Lerp((float)(IsAxe ? 170 : 200), (float)(IsAxe ? 200 : 170), (MathF.Sin(AITimer * 0.06f) + 1f) * 0.5f);
		Vector2 hoverDestination = Owner.Center + BladeHoverOffsetAngle.ToRotationVector2() * hoverDistMult;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.04f).MoveTowards(hoverDestination, 20f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		if (!base.Projectile.WithinRange(Owner.Center, 2500f))
		{
			base.Projectile.Center = hoverDestination;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.rotation = base.Projectile.AngleFrom(Owner.Center) + (float)Math.PI / 2f;
	}

	public void DoBehavior_AxeCircle(NPC potentialTarget)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		_ = 300f / (float)CurrentViridCount;
		if (potentialTarget == null)
		{
			ReturnToIdleState();
			return;
		}
		float offset = (float)Math.PI * 2f * MathF.Floor((float)BladeIndex * 0.5f) / MathF.Ceiling((float)CurrentViridCount * 0.5f) + Owner.Calamity().ViridVanguardRotation * (float)((!Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : (-1));
		Vector2 hoverDestination = Owner.Center + offset.ToRotationVector2() * 200f;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.04f).MoveTowards(hoverDestination, 32f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		if (!base.Projectile.WithinRange(Owner.Center, 2500f))
		{
			base.Projectile.Center = hoverDestination;
			base.Projectile.netUpdate = true;
		}
		Owner.Calamity().ViridVanguardRotationToAdd = ViridVanguard.IdleCirclingSpeed * MathHelper.SmoothStep(1f, ViridVanguard.AxeCirclingSpeedMultiplier, AITimer / 150f);
		base.Projectile.rotation = base.Projectile.AngleFrom(Owner.Center) + (float)Math.PI / 2f;
	}

	public void DoBehavior_HorizontalSlashes(NPC target)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		int hoverTime = 22;
		int chargeTime = 14;
		if (target == null)
		{
			ReturnToIdleState();
			return;
		}
		base.Projectile.MaxUpdates = 2;
		float wrappedAttackTimer = AITimer % (float)(hoverTime + chargeTime);
		float fadeIn = Utils.GetLerpValue((float)hoverTime - 6f, hoverTime, wrappedAttackTimer, clamped: true);
		float fadeOut = Utils.GetLerpValue(chargeTime, (float)chargeTime - 6f, wrappedAttackTimer - (float)hoverTime, clamped: true);
		BladeGleamInterpolant = fadeIn * fadeOut;
		if (wrappedAttackTimer < (float)hoverTime)
		{
			Vector2 hoverDestination = target.Center + Vector2.UnitX * (float)(target.Center.X < base.Projectile.Center.X).ToDirectionInt() * 250f;
			hoverDestination.Y += (float)Math.Cos((float)base.Projectile.identity * 1.7f) * 67f;
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.08f).MoveTowards(hoverDestination, 16f);
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.5f;
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(0f, 0.08f);
		}
		else
		{
			base.Projectile.rotation += (float)Math.Sign(base.Projectile.velocity.X) * (float)Math.PI / (float)chargeTime * 0.36f;
		}
		if (wrappedAttackTimer == (float)hoverTime)
		{
			SoundStyle style = CommonCalamitySounds.MeatySlashSound with
			{
				Pitch = 1.6f,
				Volume = 0.27f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.oldPos = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
			base.Projectile.velocity = Vector2.UnitX * (float)(target.Center.X > base.Projectile.Center.X).ToDirectionInt() * 44f;
			base.Projectile.netUpdate = true;
		}
		if (AITimer >= (float)((hoverTime + chargeTime) * ViridVanguard.HorizontalSlashAmount))
		{
			AITimer = 0f;
			CurrentState = ViridVanguardAIState.RegularPierceSlashes;
			base.Projectile.netUpdate = true;
		}
	}

	public void DoBehavior_VerticalPierceTeleport(NPC target)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		int hoverTime = 26;
		int chargeTime = 32;
		if (target == null)
		{
			ReturnToIdleState();
			return;
		}
		base.Projectile.MaxUpdates = 2;
		float wrappedAttackTimer = ((int)AITimer + base.Projectile.identity / 2) % (hoverTime + chargeTime);
		float fadeIn = Utils.GetLerpValue((float)hoverTime - 6f, hoverTime, wrappedAttackTimer, clamped: true);
		float fadeOut = Utils.GetLerpValue(chargeTime, (float)chargeTime - 6f, wrappedAttackTimer - (float)hoverTime, clamped: true);
		BladeGleamInterpolant = fadeIn * fadeOut;
		if (wrappedAttackTimer < (float)hoverTime)
		{
			Vector2 hoverDestination = target.Center - Vector2.UnitY * 500f;
			hoverDestination.X += (float)Math.Cos((float)base.Projectile.identity * 1.7f) * 50f;
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.08f).MoveTowards(hoverDestination, 16f);
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.5f;
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(base.Projectile.AngleTo(target.Center) + (float)Math.PI / 2f, 0.24f);
		}
		if (wrappedAttackTimer == (float)hoverTime)
		{
			SoundStyle style = CommonCalamitySounds.MeatySlashSound with
			{
				Pitch = 1.6f,
				Volume = 0.27f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.oldPos = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
			base.Projectile.velocity = Vector2.UnitY * 45f;
			base.Projectile.netUpdate = true;
		}
		if (wrappedAttackTimer >= (float)hoverTime + 5f && base.Projectile.Center.Y > target.Center.Y + 850f)
		{
			base.Projectile.oldPos = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
			base.Projectile.Center = target.Center - Vector2.UnitY * 850f * 0.7f;
			base.Projectile.netUpdate = true;
		}
		if (AITimer >= (float)((hoverTime + chargeTime) * ViridVanguard.VerticalPierceAmount))
		{
			AITimer = 0f;
			CurrentState = ViridVanguardAIState.HorizontalSlashes;
			base.Projectile.netUpdate = true;
		}
	}

	public void DoBehavior_RegularPierceSlashes(NPC target)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		int attackCycleTime = 44;
		float upwardRiseTimeRatio = 0.4f;
		float pierceTimeRatio = 0.14f;
		if (target == null)
		{
			ReturnToIdleState();
			ChargeStartingPosition = Vector2.Zero;
			return;
		}
		if (AITimer % (float)attackCycleTime == 1f)
		{
			ChargeStartingPosition = base.Projectile.Center + Main.rand.NextVector2Circular(80f, 80f);
			base.Projectile.netUpdate = true;
		}
		float attackCompletion = AITimer / (float)attackCycleTime % 1f;
		if (attackCompletion < upwardRiseTimeRatio)
		{
			base.Projectile.oldPos = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
		}
		base.Projectile.MaxUpdates = 2;
		float offsetDistanceFactor = MathHelper.Lerp(1.61f, 3f, (float)base.Projectile.identity / 7f % 1f);
		Vector2 startingPosition = ChargeStartingPosition + Vector2.UnitY * Utils.GetLerpValue(0f, upwardRiseTimeRatio, attackCompletion, clamped: true) * -200f;
		Vector2 targetOffset = target.Center - startingPosition;
		Vector2 endingPosition = target.Center + targetOffset.SafeNormalize(Vector2.Zero) * MathHelper.Clamp(((Vector2)(ref targetOffset)).Length(), 60f, 240f) * offsetDistanceFactor;
		float pierceCompletion = Utils.GetLerpValue(upwardRiseTimeRatio, upwardRiseTimeRatio + pierceTimeRatio, attackCompletion, clamped: true);
		float throughTargetCompletion = Utils.GetLerpValue(upwardRiseTimeRatio + pierceTimeRatio, 1f, attackCompletion, clamped: true);
		base.Projectile.rotation = base.Projectile.rotation.AngleTowards(targetOffset.ToRotation() + (float)Math.PI / 2f, (float)Math.PI / 5f);
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Vector2.Lerp(startingPosition, target.Center, pierceCompletion), pierceCompletion * 0.5f);
		if (throughTargetCompletion > 0f)
		{
			base.Projectile.Center = Vector2.Lerp(target.Center, endingPosition, throughTargetCompletion);
		}
		base.Projectile.velocity = Vector2.Zero;
		if (AITimer % (float)attackCycleTime == (float)(int)((float)attackCycleTime * upwardRiseTimeRatio))
		{
			SoundStyle style = CommonCalamitySounds.MeatySlashSound with
			{
				Pitch = 1.6f,
				Volume = 0.27f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (AITimer >= (float)(attackCycleTime * ViridVanguard.StabAmount))
		{
			AITimer = 0f;
			CurrentState = ViridVanguardAIState.VerticalPierceTeleport;
			base.Projectile.netUpdate = true;
		}
	}

	public void DoBehavior_PhotonRipperZenithStartup(NPC target)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.MaxUpdates = 2;
		float completion = AITimer / ((float)ViridVanguard.ActiveAttackStartup * (float)base.Projectile.MaxUpdates);
		Vector2 hoverDestination = Owner.Center + (BladeAngleForPhripperZenith * (float)((!Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : (-1))).ToRotationVector2() * MathHelper.Lerp(200f, 116f, completion);
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.04f).MoveTowards(hoverDestination, MathHelper.Lerp(16f, 64f, completion));
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		Owner.Calamity().ViridVanguardRotationToAdd = MathHelper.Lerp(1f, ViridVanguard.ActiveAttackCirclingSpeedMultiplier, completion) * ViridVanguard.IdleCirclingSpeed;
		BladeAngleForPhripperZenith += Owner.Calamity().ViridVanguardRotationToAdd / (float)base.Projectile.MaxUpdates;
		ActiveTimer = -1f;
		IsInDotRange = false;
		if (completion >= 1f)
		{
			CurrentState = ViridVanguardAIState.PhotonRipperZenithSlashes;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.rotation = base.Projectile.AngleFrom(Owner.Center) + (float)Math.PI / 2f;
	}

	public void DoBehavior_PhotonRipperZenithActive(NPC target)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.MaxUpdates = 2;
		float dotResult = Vector2.Dot(Owner.DirectionTo(IsInDotRange ? StoredAttackMousePos : Owner.Calamity().mouseWorld), BladeAngleForPhripperZenith.ToRotationVector2());
		if (dotResult < -0.5f)
		{
			IsInDotRange = false;
			ActiveAttacking = false;
		}
		else if (!IsInDotRange)
		{
			IsInDotRange = true;
			StoredAttackMousePos = Owner.Calamity().mouseWorld;
			ActiveTimer--;
			if (ActiveTimer % 2f == 0f)
			{
				ActiveAttacking = true;
				base.Projectile.ResetLocalNPCHitImmunity();
			}
		}
		dotResult++;
		dotResult /= 2f;
		if (!ActiveAttacking)
		{
			Vector2 hoverDestination = Owner.Center + (BladeAngleForPhripperZenith * (float)((!Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : (-1))).ToRotationVector2() * 116f;
			base.Projectile.Center = hoverDestination;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.8f;
			base.Projectile.rotation = base.Projectile.AngleFrom(Owner.Center) + (float)Math.PI / 2f;
			if ((ActiveTimer < (float)(-2 * ViridVanguard.ActiveAttackSlashCount * CurrentViridCount) || ActiveTimer > 0f) && base.Projectile.FinalExtraUpdate())
			{
				CurrentState = ViridVanguardAIState.PhotonRipperZenithEndlag;
				ActiveTimer = ViridVanguard.ActiveAttackCooldown;
				AITimer = 0f;
			}
		}
		else
		{
			float distance = Owner.Distance(StoredAttackMousePos);
			float endSize = MathHelper.Clamp(distance * 0.25f, 164f, 300f);
			Vector2 hoverCenter = Vector2.Lerp(Owner.Center, Owner.Center + Owner.Center.DirectionTo(StoredAttackMousePos) * ((distance > 164f) ? (distance - endSize) : 0f), dotResult);
			Vector2 finalHoverDestination = hoverCenter + (BladeAngleForPhripperZenith * (float)((!Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : (-1))).ToRotationVector2() * MathHelper.Lerp(116f, endSize, dotResult);
			base.Projectile.Center = finalHoverDestination;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.8f;
			base.Projectile.rotation = base.Projectile.AngleFrom(hoverCenter) + (float)Math.PI / 2f;
		}
		Owner.Calamity().ViridVanguardRotationToAdd = ViridVanguard.ActiveAttackCirclingSpeedMultiplier * ViridVanguard.IdleCirclingSpeed;
		BladeAngleForPhripperZenith += Owner.Calamity().ViridVanguardRotationToAdd / (float)base.Projectile.MaxUpdates;
	}

	public void DoBehavior_PhotonRipperZenithEndlag(NPC target)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.MaxUpdates = 1;
		float completion = 1f - AITimer / ((float)ViridVanguard.ActiveAttackEndlag * (float)base.Projectile.MaxUpdates);
		if (Owner.Calamity().ViridVanguardActiveAttackerThisFrame)
		{
			AITimer = 0f;
			ActiveTimer = ViridVanguard.ActiveAttackCooldown;
			completion = 1f;
		}
		Vector2 hoverDestination = Owner.Center + (BladeAngleForPhripperZenith * (float)((!Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : (-1))).ToRotationVector2() * MathHelper.Lerp(200f, 116f, completion);
		base.Projectile.Center = hoverDestination;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		Owner.Calamity().ViridVanguardRotationToAdd = MathHelper.Lerp(1f, ViridVanguard.ActiveAttackCirclingSpeedMultiplier, completion) * ViridVanguard.IdleCirclingSpeed;
		BladeAngleForPhripperZenith += Owner.Calamity().ViridVanguardRotationToAdd / (float)base.Projectile.MaxUpdates;
		if (completion <= 0f)
		{
			ReturnToIdleState();
			Owner.Calamity().ViridVanguardRotation += MathHelper.WrapAngle(BladeAngleForPhripperZenith - BladeHoverOffsetAngle);
			base.Projectile.netUpdate = true;
		}
		base.Projectile.rotation = base.Projectile.AngleFrom(Owner.Center) + (float)Math.PI / 2f;
	}

	public void BeginSuperEpicPhotonRipperZenithKnockoffAttack()
	{
		if (CurrentState != ViridVanguardAIState.CircleOwner)
		{
			AITimer = 0f;
		}
		BladeAngleForPhripperZenith = BladeHoverOffsetAngle;
		CurrentState = ViridVanguardAIState.ChargingCircle;
		AITimer = 0f;
		base.Projectile.netUpdate = true;
	}

	public void ReturnToIdleState()
	{
		AITimer = 0f;
		base.Projectile.extraUpdates = 0;
		CurrentState = ViridVanguardAIState.CircleOwner;
		base.Projectile.netUpdate = true;
	}

	public void HandleMinionBools()
	{
		Owner.AddBuff(ModContent.BuffType<ViridVanguardBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<ViridVanguardBlade>())
		{
			if (Owner.dead)
			{
				Owner.Calamity().viridVanguard = false;
			}
			if (Owner.Calamity().viridVanguard)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		target.Calamity().sagePoisonDamage = (int)(SagePoison.debuffData.EnemyLostRegen * SagePoison.ViridVanguardPoisonMultiplier * (float)CurrentViridCount);
		target.AddBuff(ModContent.BuffType<SagePoison>(), 60);
		if (ActiveAttacking)
		{
			Vector2 vel = Utils.RotatedByRandom(new Vector2(0.1f, 0.1f), 100.0);
			GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(target.Center, vel, affectedByGravity: false, 9, Main.rand.NextFloat(0.25f, 0.35f), Main.rand.NextBool() ? Color.LimeGreen : Color.Goldenrod));
			SoundEngine.PlaySound(CommonCalamitySounds.SwiftSliceSound with
			{
				Volume = CommonCalamitySounds.SwiftSliceSound.Volume * 0.3f,
				MaxInstances = 0
			}, base.Projectile.Center);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (ActiveAttacking)
		{
			modifiers.SourceDamage *= ViridVanguard.ActiveAttackSlashDmgMult;
		}
		else if (!ActiveAttackStateList.Contains(CurrentState))
		{
			if (IsAxe)
			{
				modifiers.SourceDamage *= ViridVanguard.AxeDmgMult;
			}
			else
			{
				modifiers.SourceDamage *= ViridVanguard.SwordDmgMult;
			}
		}
	}

	public Color TrailColorFunction(float completionRatio)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		float opacity = (float)Math.Pow(Utils.GetLerpValue(1f, 0.45f, completionRatio, clamped: true), 4.0) * base.Projectile.Opacity * ((CurrentState == ViridVanguardAIState.CircleOwner) ? 0.3f : 0.48f);
		return Color.Lerp(new Color(115, 196, 127), Color.Yellow, MathHelper.Clamp(completionRatio * 1.4f, 0f, 1f)) * opacity;
	}

	public float TrailWidthFunction(float completionRatio)
	{
		return (float)base.Projectile.height * (1f - completionRatio) * (DrawAsAxe ? 0.6f : 0.8f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(2, 1, (!DrawAsAxe) ? 1 : 0);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		SpriteEffects direction = (SpriteEffects)(((base.Projectile.spriteDirection == 1) ^ Owner.Calamity().InvertExaltationLineRotationDirections) ? 1 : 0);
		if (TrailDrawer == null)
		{
			TrailDrawer = new VertexStrip();
		}
		GameShaders.Misc["EmpressBlade"].UseImage0("Images/Extra_201");
		GameShaders.Misc["EmpressBlade"].UseImage1("Images/Extra_193");
		GameShaders.Misc["EmpressBlade"].UseShaderSpecificData(new Vector4(1f, 0f, 0f, 0.6f));
		GameShaders.Misc["EmpressBlade"].Apply();
		TrailDrawer.PrepareStrip(base.Projectile.oldPos, base.Projectile.oldRot, TrailColorFunction, TrailWidthFunction, base.Projectile.Size * 0.5f - Main.screenPosition, base.Projectile.oldPos.Length, includeBacksides: true);
		TrailDrawer.DrawTrail();
		Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		float outlineOpacity = 1f;
		float outlineWidth = 1f;
		Color outlineColor = Color.LimeGreen;
		if (!ActiveAttackStateList.Contains(CurrentState) || !(ActiveTimer > 0f))
		{
			outlineWidth = Math.Clamp(MathF.Pow(1f - ActiveTimer / (float)ViridVanguard.ActiveAttackCooldown, 3f), 0f, 1f);
			outlineOpacity = 0.75f;
		}
		Texture2D bladeOutlineTex = ViridVanguard.GetBladeOutlineTex();
		float rotation = base.Projectile.rotation + ((DrawAsAxe ^ Owner.Calamity().InvertExaltationLineRotationDirections) ? (-0.2f) : 0.2f);
		Main.EntitySpriteDraw(bladeOutlineTex, drawPosition + new Vector2(2f, 0f) * outlineWidth, frame, outlineColor * outlineOpacity, rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(bladeOutlineTex, drawPosition + new Vector2(0f, 2f) * outlineWidth, frame, outlineColor * outlineOpacity, rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(bladeOutlineTex, drawPosition + new Vector2(-2f, 0f) * outlineWidth, frame, outlineColor * outlineOpacity, rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(bladeOutlineTex, drawPosition + new Vector2(0f, -2f) * outlineWidth, frame, outlineColor * outlineOpacity, rotation, origin, base.Projectile.scale, direction);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale, direction);
		if (ActiveTimer <= 0f)
		{
			Texture2D shineTex = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2).Value;
			Vector2 shineScale = new Vector2(1.67f, 3f) * base.Projectile.scale;
			shineScale *= MathHelper.Lerp(0.9f, 1.1f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 7.4f + (float)base.Projectile.identity) * 0.5f + 0.5f);
			Vector2 lensFlareWorldPosition = base.Projectile.Center + (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * (float)base.Projectile.width * base.Projectile.scale * 0.88f;
			Color val = Color.Lerp(Color.LimeGreen, Color.Yellow, 0.23f);
			((Color)(ref val)).A = 0;
			Color lensFlareColor = val * BladeGleamInterpolant;
			Main.EntitySpriteDraw(shineTex, lensFlareWorldPosition - Main.screenPosition, null, lensFlareColor, 0f, shineTex.Size() * 0.5f, shineScale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(shineTex, lensFlareWorldPosition - Main.screenPosition, null, lensFlareColor, (float)Math.PI / 2f, shineTex.Size() * 0.5f, shineScale, (SpriteEffects)0);
		}
		GameShaders.Misc["EmpressBlade"].UseImage0("Images/Extra_209");
		GameShaders.Misc["EmpressBlade"].UseImage1("Images/Extra_210");
		return false;
	}

	public ViridVanguardBlade()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		StoredAttackMousePos = Vector2.Zero;
		base._002Ector();
	}
}
