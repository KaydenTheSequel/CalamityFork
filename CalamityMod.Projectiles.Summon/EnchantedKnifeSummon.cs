using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EnchantedKnifeSummon : BaseMinionProjectile
{
	private Action _currentState;

	private Vector2 SwingCenter;

	private float RandomRotationOffset;

	private bool HasStartedSwinging;

	private bool HasSpawned;

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<EnchantedKnifeSummon>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<EnchantedKnifeStaffBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.EnchantedKnifeStaffBool;

	public override bool PreHardmodeMinionTileVision => true;

	private Action CurrentState
	{
		get
		{
			return _currentState;
		}
		set
		{
			OnStateChange(value);
			_currentState = value;
		}
	}

	private ref float DashTimer => ref base.Projectile.ai[0];

	private ref float SwingTimer => ref base.Projectile.ai[1];

	private ref float SwingDirection => ref base.Projectile.ai[2];

	private Vector2 IdlePosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			return base.Owner.MountedCenter + new Vector2(50f * MathF.Ceiling((float)base.Projectile.minionPos / 2f) * (float)((base.Projectile.minionPos % 2 != 0) ? 1 : (-1)), -60f + MathF.Ceiling((float)base.Projectile.minionPos / 2f) * 15f);
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.width = (base.Projectile.height = 32);
	}

	public override void MinionAI()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		if (!HasSpawned)
		{
			base.TrailCacheLength = 4;
			DashTimer = Main.rand.Next(30);
			base.Projectile.velocity = base.Projectile.DirectionFrom(base.Owner.Center);
			CurrentState = GoToOwnerState;
			HasSpawned = true;
			NetUpdate();
		}
		Color newColor;
		if (Main.rand.NextBool(6))
		{
			Vector2 vel = Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0);
			Vector2 position = base.Projectile.Center + vel * Main.rand.NextFloat(0.5f, 3f);
			int type = ((!Main.rand.NextBool()) ? 15 : (Main.rand.NextBool() ? 57 : 58));
			Vector2? velocity = vel * Main.rand.NextFloat(0.5f, 1f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.6f, 1.1f);
		}
		CurrentState();
		base.Projectile.MinionAntiClump(0.25f);
		if (!Main.dedServ)
		{
			Vector2 center = base.Projectile.Center;
			newColor = Color.Cyan;
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.4f);
		}
	}

	private void GoToOwnerState()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			CurrentState = AttackState;
			return;
		}
		if (base.Projectile.DistanceSQ(IdlePosition) > 9000000f)
		{
			base.Projectile.Center = base.Owner.Center;
		}
		if (base.Projectile.DistanceSQ(IdlePosition) < 6400f)
		{
			CurrentState = IdleState;
		}
		base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.DirectionTo(IdlePosition).ToRotation(), 0.08f).ToRotationVector2() * 10f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
	}

	private void IdleState()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			CurrentState = AttackState;
			return;
		}
		if (base.Projectile.DistanceSQ(IdlePosition) > 25600f)
		{
			CurrentState = GoToOwnerState;
		}
		float speed = Utils.Remap(base.Projectile.DistanceSQ(IdlePosition), 0f, 25600f, 0.25f, 15f);
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.DirectionTo(IdlePosition).ToRotation(), 0.4f).ToRotationVector2() * speed, 0.2f);
		base.Projectile.rotation = MathHelper.Lerp(base.Projectile.rotation, MathHelper.ToRadians(RandomSineFunction(Main.GlobalTimeWrappedHourly + RandomRotationOffset) * 3f) - (float)Math.PI / 2f, 0.1f);
	}

	private void AttackState()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target == null)
		{
			CurrentState = GoToOwnerState;
			return;
		}
		if (float.IsNaN(SwingCenter.X) || float.IsNaN(SwingCenter.Y) || float.IsNaN(base.Projectile.velocity.X) || float.IsNaN(base.Projectile.velocity.Y))
		{
			SwingCenter = base.Target.Center;
			base.Projectile.velocity = Vector2.Zero;
		}
		SwingCenter += base.Projectile.velocity;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.9f;
		CalamityUtils.CurveSegment easing = new CalamityUtils.CurveSegment(CalamityUtils.PolyInOutEasing, 0f, 0f, 1f, 3);
		float xDifference = base.Target.Center.X - SwingCenter.X;
		int signDirection = (float.IsNaN(xDifference) ? 1 : MathF.Sign(xDifference));
		float swingRotation = MathHelper.Lerp(-(float)Math.PI / 2f, (float)Math.PI / 2f, CalamityUtils.PiecewiseAnimation(Utils.GetLerpValue(EnchantedKnifeStaff.SwingWait, EnchantedKnifeStaff.SwingWait + EnchantedKnifeStaff.SwingTime, SwingTimer, clamped: true), easing)) * (float)signDirection;
		Vector2 predictiveDirection = CalamityUtils.CalculatePredictiveAimToTarget(SwingCenter, base.Target, EnchantedKnifeStaff.ProjectileSpeed);
		if (float.IsNaN(predictiveDirection.X) || float.IsNaN(predictiveDirection.Y))
		{
			predictiveDirection = base.Projectile.DirectionTo(base.Target.Center);
		}
		base.Projectile.Center = SwingCenter + (predictiveDirection.ToRotation() + swingRotation).ToRotationVector2() * 40f;
		base.Projectile.rotation = base.Projectile.DirectionFrom(SwingCenter).ToRotation();
		if (SwingTimer == EnchantedKnifeStaff.SwingWait + EnchantedKnifeStaff.SwingTime * 0.5f && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 spawnPosition = SwingCenter + predictiveDirection.SafeNormalize(-Vector2.UnitY) * 40f;
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), spawnPosition, predictiveDirection, ModContent.ProjectileType<EnchantedKnifeStaffProjectile>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		if (DashTimer >= EnchantedKnifeStaff.DashCooldown)
		{
			Vector2 dashVelocity = CalamityUtils.CalculatePredictiveAimToTarget(SwingCenter, base.Target, EnchantedKnifeStaff.DashSpeed);
			if (!float.IsNaN(dashVelocity.X) && !float.IsNaN(dashVelocity.Y))
			{
				base.Projectile.velocity = dashVelocity;
			}
			DashTimer = 0f;
		}
		SwingTimer += 1f * SwingDirection;
		if (SwingTimer == 0f || SwingTimer == EnchantedKnifeStaff.SwingWait + EnchantedKnifeStaff.SwingTime + EnchantedKnifeStaff.SwingWait)
		{
			if (HasStartedSwinging)
			{
				SwingDirection *= -1f;
			}
			else
			{
				HasStartedSwinging = true;
			}
		}
		if (base.Projectile.DistanceSQ(base.Target.Center) > 102400f)
		{
			DashTimer += ((!Main.rand.NextBool((int)EnchantedKnifeStaff.DashCooldown)) ? 1 : 2);
		}
	}

	private void OnStateChange(Action newState)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		if (newState == new Action(IdleState))
		{
			RandomRotationOffset = Main.rand.NextFloat((float)Math.PI * 2f);
			NetUpdate();
		}
		if (newState != new Action(AttackState) || base.Target == null)
		{
			return;
		}
		Vector2 size = base.Target.Size;
		float targetSize = ((Vector2)(ref size)).Length();
		Vector2 previousCenter = base.Projectile.Center;
		Vector2 newCenter = base.Target.Center + Main.rand.NextVector2CircularEdge(targetSize * 0.5f + 160f, targetSize * 0.5f + 160f);
		int maxAttempts = 10;
		int attempts = 0;
		while (Main.tile[newCenter.ToSafeTileCoordinates()].IsTileSolid() && attempts < maxAttempts)
		{
			newCenter = base.Target.Center + Main.rand.NextVector2CircularEdge(targetSize * 0.5f + 160f, targetSize * 0.5f + 160f);
			attempts++;
		}
		if (float.IsNaN(newCenter.X) || float.IsNaN(newCenter.Y))
		{
			newCenter = base.Target.Center;
		}
		base.Projectile.Center = newCenter;
		SwingCenter = base.Projectile.Center;
		HasStartedSwinging = false;
		SwingDirection = Main.rand.NextBool().ToDirectionInt();
		int totalSwingTime = (int)(2f * EnchantedKnifeStaff.SwingWait + EnchantedKnifeStaff.SwingTime);
		SwingTimer = ((SwingDirection == 1f) ? Main.rand.Next(-30, -2) : Main.rand.Next(totalSwingTime + 1, totalSwingTime + 30 + 1));
		NetUpdate();
		if (!Main.dedServ)
		{
			int dustAmount = Main.rand.Next(30, 40);
			for (int i = 0; i < dustAmount; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)i).ToRotationVector2() * Main.rand.NextFloat(5f);
				Dust tpDustStart = Dust.NewDustPerfect(previousCenter, 309, velocity);
				Dust dust = Dust.NewDustPerfect(SwingCenter, 309, velocity);
				tpDustStart.noGravity = true;
				dust.noGravity = true;
			}
		}
	}

	private float RandomSineFunction(float x)
	{
		return MathF.Sin(3f * x) + MathF.Cos(5f * x) + 2f * MathF.Sin(0.5f * x);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 2f;
		Vector2 anchorPoint = texture.Size() * 0.5f;
		if (CurrentState == new Action(AttackState))
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
				Color afterimageDrawColor = Color.Cyan * ((4f - (float)i) / 4f) * 0.8f;
				float afterimageDrawRotation = base.Projectile.oldRot[i] + (float)Math.PI / 2f;
				Main.EntitySpriteDraw(texture, afterimageDrawPosition, null, afterimageDrawColor, afterimageDrawRotation, anchorPoint, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, anchorPoint, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	private void NetUpdate(int netSpam = 0)
	{
		base.Projectile.netUpdate = true;
		base.Projectile.netSpam = netSpam;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(SwingCenter);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		SwingCenter = reader.ReadVector2();
	}
}
