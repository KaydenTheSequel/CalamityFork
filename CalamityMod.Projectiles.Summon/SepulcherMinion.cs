using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.Summon;
using CalamityMod.NPCs.Other;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SepulcherMinion : ModProjectile, ILocalizedModType, IModType
{
	public struct SepulcherSegment
	{
		public Vector2 CurrentPosition;

		public float Rotation;

		public SepulcherSegment(Vector2 position, float rotation)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			CurrentPosition = position;
			Rotation = rotation;
		}
	}

	public class SepulcherArm
	{
		public class SepulcherArmLimb
		{
			public Vector2 Center;

			public float Rotation;

			public SepulcherArmLimb(Vector2 center, float rotation)
			{
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_0008: Unknown result type (might be due to invalid IL or missing references)
				base._002Ector();
				Center = center;
				Rotation = rotation;
			}

			public void SendData(BinaryWriter writer)
			{
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				writer.WritePackedVector2(Center);
				writer.Write(Rotation);
			}

			public static SepulcherArmLimb ReceiveData(BinaryReader reader)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return new SepulcherArmLimb(reader.ReadPackedVector2(), reader.ReadSingle());
			}
		}

		public SepulcherArmLimb[] Limbs;

		public byte SegmentIndexToAttachTo;

		public Vector2 Center;

		public float Rotation;

		public bool ReelingBack;

		public bool Direction;

		public SepulcherArm(Vector2 center, byte segmentIndexToAttachTo, float rotation, bool reelingBack, bool direction)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			Limbs = new SepulcherArmLimb[2];
			base._002Ector();
			Center = center;
			SegmentIndexToAttachTo = segmentIndexToAttachTo;
			Rotation = rotation;
			ReelingBack = reelingBack;
			Direction = direction;
			Limbs = new SepulcherArmLimb[2]
			{
				new SepulcherArmLimb(center, rotation),
				new SepulcherArmLimb(center, rotation)
			};
		}
	}

	public enum AIState
	{
		HoverNearOwner,
		AttackEnemy_Charge,
		AttackEnemy_ReleaseDartBurst,
		AttackHearts
	}

	public int IdleTimer;

	public int AttackTimer;

	public int HeartAttackCountdown;

	public SepulcherSegment[] Segments = new SepulcherSegment[24];

	public List<SepulcherArm> Arms = new List<SepulcherArm>();

	public const int HeartCreationRate = 420;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public AIState CurrentAIState
	{
		get
		{
			return (AIState)base.Projectile.ai[0];
		}
		set
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				base.Projectile.ai[0] = (float)value;
				base.Projectile.netUpdate = true;
			}
		}
	}

	public ref float JawRotation => ref base.Projectile.localAI[0];

	public ref float JawSnapTimer => ref base.Projectile.localAI[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 12000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 54;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = -1;
		base.Projectile.minionSlots = 4f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.Opacity = 0f;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
		base.Projectile.hide = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (Arms == null || Arms.Count == 0 || Arms[0] == null)
		{
			Initialize();
		}
		writer.Write(IdleTimer);
		writer.Write(HeartAttackCountdown);
		writer.Write(Arms.Count);
		for (int i = 0; i < Arms.Count; i++)
		{
			writer.WritePackedVector2(Arms[i].Center);
			writer.Write(Arms[i].SegmentIndexToAttachTo);
			writer.Write(Arms[i].Rotation);
			writer.Write(Arms[i].ReelingBack);
			writer.Write(Arms[i].Direction);
			Arms[0].Limbs[0].SendData(writer);
			Arms[0].Limbs[1].SendData(writer);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (Arms == null || Arms.Count == 0 || Arms[0] == null)
		{
			Initialize();
		}
		IdleTimer = reader.ReadInt32();
		HeartAttackCountdown = reader.ReadInt32();
		int armCount = reader.ReadInt32();
		Arms.Clear();
		for (int i = 0; i < armCount; i++)
		{
			Arms.Add(new SepulcherArm(reader.ReadPackedVector2(), reader.ReadByte(), reader.ReadSingle(), reader.ReadBoolean(), reader.ReadBoolean()));
			Arms.Last().Limbs[0] = SepulcherArm.SepulcherArmLimb.ReceiveData(reader);
			Arms.Last().Limbs[1] = SepulcherArm.SepulcherArmLimb.ReceiveData(reader);
		}
	}

	public override void AI()
	{
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		Initialize();
		HandleBuffs();
		UpdateSegments();
		base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity + 0.2f, 0f, 1f);
		NPC potentialTarget = AttemptToFindTarget(1450f);
		if (CurrentAIState != AIState.AttackEnemy_ReleaseDartBurst && CurrentAIState != AIState.AttackEnemy_Charge)
		{
			if (potentialTarget != null)
			{
				CurrentAIState = AIState.AttackEnemy_ReleaseDartBurst;
			}
		}
		else if (potentialTarget == null)
		{
			CurrentAIState = AIState.HoverNearOwner;
			AttackTimer = 0;
		}
		if (HeartAttackCountdown > 0)
		{
			if (CurrentAIState != AIState.AttackHearts)
			{
				CurrentAIState = AIState.AttackHearts;
			}
			HeartAttackCountdown--;
			if (HeartAttackCountdown <= 0)
			{
				CurrentAIState = AIState.HoverNearOwner;
			}
		}
		switch (CurrentAIState)
		{
		case AIState.HoverNearOwner:
			HoverNearOwner();
			break;
		case AIState.AttackEnemy_ReleaseDartBurst:
			AttackEnemyWithDarts(potentialTarget);
			AttackTimer++;
			break;
		case AIState.AttackEnemy_Charge:
			AttackEnemyByCharging(potentialTarget);
			AttackTimer++;
			break;
		case AIState.AttackHearts:
			AttackHearts();
			break;
		}
		IdleTimer++;
		if ((float)(IdleTimer % 420) == 419f)
		{
			int heartsAttachedToOwner = 0;
			int heartType = ModContent.NPCType<ExhumedHeart>();
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].type == heartType && Main.npc[i].active && Main.npc[i].target == base.Projectile.owner)
				{
					heartsAttachedToOwner++;
				}
			}
			float chanceToBecomeAngry = 0f;
			if (heartsAttachedToOwner >= 2)
			{
				chanceToBecomeAngry = Utils.GetLerpValue(2f, 6f, heartsAttachedToOwner, clamped: true);
			}
			if (Main.rand.NextFloat() < chanceToBecomeAngry)
			{
				HeartAttackCountdown = 300;
				base.Projectile.netUpdate = true;
			}
			else if (Main.netMode != 1)
			{
				NPC.NewNPC(base.Projectile.GetSource_FromAI(), (int)Owner.Center.X, (int)Owner.Center.Y, heartType);
			}
		}
		if (JawSnapTimer > 0f)
		{
			JawRotation = JawRotation.AngleTowards(-0.44f, 0.064f);
			JawSnapTimer--;
		}
		else
		{
			JawRotation = JawRotation.AngleTowards(0f, 0.03f);
		}
	}

	public void Initialize()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] != 0f)
		{
			return;
		}
		for (int i = 0; i < Segments.Length; i++)
		{
			Segments[i].CurrentPosition = base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * (float)i * 0.2f;
		}
		base.Projectile.ai[1] = 1f;
		if (Main.myPlayer == base.Projectile.owner)
		{
			float rotationalOffset = 0f;
			for (byte i2 = 3; i2 < Segments.Length - 2; i2 += 2)
			{
				Arms.Add(new SepulcherArm(Segments[i2].CurrentPosition, i2, rotationalOffset, reelingBack: false, direction: false));
				rotationalOffset = MathHelper.WrapAngle(rotationalOffset + (float)Math.PI / 6f);
				Arms.Add(new SepulcherArm(Segments[i2].CurrentPosition, i2, rotationalOffset + (float)Math.PI, reelingBack: false, direction: true));
				rotationalOffset = MathHelper.WrapAngle(rotationalOffset + (float)Math.PI / 6f);
			}
			base.Projectile.netUpdate = true;
		}
	}

	public void HandleBuffs()
	{
		Owner.AddBuff(ModContent.BuffType<SepulcherMinionBuff>(), 3600);
		if (Owner.dead)
		{
			Owner.Calamity().sepulcher = false;
		}
		if (Owner.Calamity().sepulcher)
		{
			base.Projectile.timeLeft = 2;
		}
	}

	public void UpdateSegments()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		float aheadRotation = base.Projectile.rotation;
		Vector2 aheadPosition = base.Projectile.Center;
		for (int i = 0; i < Segments.Length; i++)
		{
			Vector2 offsetToDestination = aheadPosition - Segments[i].CurrentPosition;
			if (aheadRotation != Segments[i].Rotation)
			{
				float angleOffset = MathHelper.WrapAngle(aheadRotation - Segments[i].Rotation.AngleLerp(aheadRotation, 0.08f));
				offsetToDestination = offsetToDestination.RotatedBy(angleOffset * 0.08f);
			}
			Segments[i].Rotation = (aheadPosition - Segments[i].CurrentPosition).ToRotation() + (float)Math.PI / 2f;
			Segments[i].CurrentPosition = aheadPosition - offsetToDestination.SafeNormalize(Vector2.Zero) * 48f;
			aheadPosition = Segments[i].CurrentPosition;
			aheadRotation = Segments[i].Rotation;
		}
		for (int j = 0; j < Arms.Count; j++)
		{
			SepulcherSegment segmentToAttachTo = Segments[Arms[j].SegmentIndexToAttachTo];
			Vector2 idealMovePosition = segmentToAttachTo.CurrentPosition;
			float sideFactor = MathHelper.Lerp(200f, 18f, Utils.GetLerpValue(-0.51f, -0.06f, Arms[j].Rotation, clamped: true));
			float aheadFactor = MathHelper.Lerp(284f, 680f, Utils.GetLerpValue(-0.51f, -0.06f, Arms[j].Rotation, clamped: true));
			int direction = Arms[j].Direction.ToDirectionInt();
			idealMovePosition += (segmentToAttachTo.Rotation + Arms[j].Rotation * (float)direction - (float)Math.PI / 2f).ToRotationVector2() * base.Projectile.scale * aheadFactor;
			idealMovePosition += (segmentToAttachTo.Rotation + Arms[j].Rotation * (float)direction - (float)Math.PI / 2f + (float)Math.PI / 2f * (float)direction).ToRotationVector2() * base.Projectile.scale * sideFactor;
			Arms[j].Center = idealMovePosition;
			Vector2 offsetFromSegment = Vector2.Zero;
			offsetFromSegment += Utils.RotatedBy(new Vector2((float)direction * 35f, 28f), (double)(segmentToAttachTo.Rotation - ((float)Math.PI / 2f - Arms[j].Rotation * 1.7f - 0.77f) * (float)direction), default(Vector2)).SafeNormalize(Vector2.UnitY) * 62f;
			Arms[j].Limbs[0].Center = segmentToAttachTo.CurrentPosition + offsetFromSegment;
			Arms[j].Limbs[0].Rotation = offsetFromSegment.ToRotation();
			Arms[j].Limbs[1].Rotation = (Arms[j].Center - Arms[j].Limbs[0].Center).ToRotation();
			Arms[j].Limbs[1].Center = Arms[j].Limbs[0].Center + offsetFromSegment * 0.5f + (Arms[j].Center - Arms[j].Limbs[0].Center).SafeNormalize(Vector2.UnitY) * 64f;
			float rotationalVelocityFactor = Utils.GetLerpValue(0f, 6f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
			if (CurrentAIState == AIState.AttackEnemy_Charge)
			{
				rotationalVelocityFactor *= 0.85f;
			}
			if (CurrentAIState == AIState.AttackHearts)
			{
				rotationalVelocityFactor *= 1.2f;
			}
			if (Arms[j].ReelingBack)
			{
				Arms[j].Rotation -= rotationalVelocityFactor * 0.066f;
				if (Arms[j].Rotation < -0.55f)
				{
					Arms[j].ReelingBack = false;
				}
			}
			else
			{
				Arms[j].Rotation += rotationalVelocityFactor * 0.029f;
				if (Arms[j].Rotation > 0.77f)
				{
					Arms[j].ReelingBack = true;
				}
			}
		}
	}

	public void HoverNearOwner()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.WithinRange(Owner.Center, 3200f))
		{
			base.Projectile.Center = Owner.Center;
			base.Projectile.velocity = Main.rand.NextVector2CircularEdge(4f, 4f);
			base.Projectile.netUpdate = true;
		}
		if (!base.Projectile.WithinRange(Owner.Center, 540f))
		{
			Vector2 destination = Owner.Center + ((float)IdleTimer / 31f).ToRotationVector2() * 240f;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(destination) * 18f, 0.03f);
			float updatedDirectionRotation = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.AngleTo(destination), 0.04f);
			base.Projectile.velocity = updatedDirectionRotation.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		else if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.03f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public void AttackEnemyWithDarts(NPC target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		Vector2 destination = target.Center + ((float)AttackTimer / 20f).ToRotationVector2() * 320f;
		base.Projectile.velocity = base.Projectile.velocity.MoveTowards(base.Projectile.SafeDirectionTo(destination) * 24f, 1f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (Main.myPlayer == base.Projectile.owner && AttackTimer % 60 > 60 - Segments.Length)
		{
			int segmentToFireFrom = 60 - AttackTimer % 60;
			Vector2 spawnPosition = Segments[segmentToFireFrom].CurrentPosition;
			Vector2 shootVelocity = (target.Center - spawnPosition).SafeNormalize(Vector2.UnitY) * 8f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, shootVelocity, ModContent.ProjectileType<BrimstoneDartMinion>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
		}
		if (Main.myPlayer == base.Projectile.owner && AttackTimer > 430)
		{
			CurrentAIState = AIState.AttackEnemy_Charge;
			AttackTimer = 0;
		}
	}

	public void AttackEnemyByCharging(NPC target)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.WithinRange(target.Center, 350f))
		{
			Vector2 destination = target.Center + ((float)AttackTimer / 25f).ToRotationVector2() * MathHelper.Min((float)target.width * 0.35f, 150f);
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(destination) * 34f, 0.08f);
			float updatedDirectionRotation = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.AngleTo(destination), 0.12f);
			base.Projectile.velocity = updatedDirectionRotation.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		else if (((Vector2)(ref base.Projectile.velocity)).Length() < 29f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.035f;
			if (JawSnapTimer <= 0f)
			{
				if (base.Projectile.WithinRange(target.Center, 270f))
				{
					JawRotation = JawRotation.AngleLerp(0.87f, 0.12f);
				}
				if (base.Projectile.WithinRange(target.Center, 165f))
				{
					SoundEngine.PlaySound(in SoundID.DD2_SkeletonHurt, base.Projectile.Center);
					JawSnapTimer = 45f;
				}
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (Main.myPlayer == base.Projectile.owner && AttackTimer > 480)
		{
			CurrentAIState = AIState.AttackEnemy_ReleaseDartBurst;
			AttackTimer = 0;
		}
	}

	public void AttackHearts()
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		float newSpeed = MathHelper.Lerp(((Vector2)(ref base.Projectile.velocity)).Length(), 21f, 0.025f);
		float npcDistCompare = 960f;
		int index = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile) && n.type == ModContent.NPCType<ExhumedHeart>())
			{
				float currentNPCDist = Vector2.Distance(n.Center, base.Projectile.Center);
				if (currentNPCDist < npcDistCompare)
				{
					npcDistCompare = currentNPCDist;
					index = n.whoAmI;
				}
			}
		}
		if (index == -1)
		{
			HeartAttackCountdown = 0;
			CurrentAIState = AIState.HoverNearOwner;
			return;
		}
		NPC targetHeart = Main.npc[index];
		if (!base.Projectile.WithinRange(targetHeart.Center, 185f))
		{
			Vector2 idealVelocity = base.Projectile.SafeDirectionTo(targetHeart.Center) * newSpeed;
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(idealVelocity, 2f);
			float idealAimDirection = idealVelocity.ToRotation();
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleLerp(idealAimDirection, 0.05f).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		else if (((Vector2)(ref base.Projectile.velocity)).Length() < 36f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.066f;
			if (JawSnapTimer <= 0f)
			{
				if (base.Projectile.WithinRange(targetHeart.Center, 150f))
				{
					JawRotation = JawRotation.AngleLerp(0.87f, 0.12f);
				}
				if (base.Projectile.WithinRange(targetHeart.Center, 90f))
				{
					SoundEngine.PlaySound(in SoundID.DD2_SkeletonHurt, base.Projectile.Center);
					JawSnapTimer = 45f;
				}
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public NPC AttemptToFindTarget(float searchDistance)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		NPC closestTarget = null;
		int heartType = ModContent.NPCType<ExhumedHeart>();
		if (Owner.HasMinionAttackTargetNPC)
		{
			NPC targetedNPC = Main.npc[Owner.MinionAttackTargetNPC];
			if (targetedNPC.WithinRange(base.Projectile.Center, searchDistance) && targetedNPC.CanBeChasedBy() && targetedNPC.type != heartType)
			{
				return targetedNPC;
			}
		}
		float distance = searchDistance * searchDistance;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy() && n.type != heartType)
			{
				float extraDistance = n.width / 2 + n.height / 2;
				if (n.WithinRange(base.Projectile.Center, distance + extraDistance))
				{
					distance = n.DistanceSQ(base.Projectile.Center);
					closestTarget = n;
				}
			}
		}
		return closestTarget;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_078e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0819: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0925: Unknown result type (might be due to invalid IL or missing references)
		//IL_092a: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0956: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0960: Unknown result type (might be due to invalid IL or missing references)
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
		float angerFactor = Utils.GetLerpValue(300f, 280f, HeartAttackCountdown, clamped: true) * Utils.GetLerpValue(0f, 30f, HeartAttackCountdown, clamped: true);
		float afterimageOutwardness = MathHelper.Lerp(6f, 8f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 2.3f) * 0.5f + 0.5f) * angerFactor;
		Color backAfterimageColor = Color.Red * angerFactor;
		((Color)(ref backAfterimageColor)).A = 0;
		Texture2D eyesTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherMinionEyes", (AssetRequestMode)2).Value;
		Texture2D jawTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherMinionJaw", (AssetRequestMode)2).Value;
		Texture2D headTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherMinionHead", (AssetRequestMode)2).Value;
		Texture2D bodyTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherMinionBody", (AssetRequestMode)2).Value;
		Texture2D bodyTexture2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherMinionBodyAlt", (AssetRequestMode)2).Value;
		Texture2D tailTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherMinionTail", (AssetRequestMode)2).Value;
		Texture2D armTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherArm", (AssetRequestMode)2).Value;
		Texture2D foreArmTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherForeArm", (AssetRequestMode)2).Value;
		Texture2D handTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/SepulcherHand", (AssetRequestMode)2).Value;
		for (int i = 0; i < Arms.Count; i++)
		{
			Vector2 forearmDrawPosition = Arms[i].Limbs[0].Center - Main.screenPosition;
			Color drawColor = Lighting.GetColor((int)(Arms[i].Limbs[0].Center.X / 16f), (int)(Arms[i].Limbs[0].Center.Y / 16f));
			if (afterimageOutwardness > 0f)
			{
				for (int j = 0; j < 4; j++)
				{
					Vector2 drawOffset = ((float)Math.PI * 2f * (float)j / 4f).ToRotationVector2() * afterimageOutwardness;
					Main.EntitySpriteDraw(foreArmTexture, forearmDrawPosition + drawOffset, null, backAfterimageColor, Arms[i].Limbs[0].Rotation + (float)Math.PI / 2f, foreArmTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
				}
			}
			Main.EntitySpriteDraw(foreArmTexture, forearmDrawPosition, null, drawColor, Arms[i].Limbs[0].Rotation + (float)Math.PI / 2f, foreArmTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			Vector2 armDrawPosition = Arms[i].Limbs[1].Center - Main.screenPosition;
			drawColor = Lighting.GetColor((int)(Arms[i].Limbs[1].Center.X / 16f), (int)(Arms[i].Limbs[1].Center.Y / 16f));
			if (afterimageOutwardness > 0f)
			{
				for (int k = 0; k < 4; k++)
				{
					Vector2 drawOffset2 = ((float)Math.PI * 2f * (float)k / 4f).ToRotationVector2() * afterimageOutwardness;
					Main.EntitySpriteDraw(armTexture, armDrawPosition + drawOffset2, null, backAfterimageColor, Arms[i].Limbs[1].Rotation + (float)Math.PI / 2f, armTexture.Size() * new Vector2(0.5f, 0f), base.Projectile.scale, (SpriteEffects)2);
				}
			}
			Main.EntitySpriteDraw(armTexture, armDrawPosition, null, drawColor, Arms[i].Limbs[1].Rotation + (float)Math.PI / 2f, armTexture.Size() * new Vector2(0.5f, 0f), base.Projectile.scale, (SpriteEffects)2);
			Vector2 handDrawPosition = armDrawPosition;
			SpriteEffects handDirection = (SpriteEffects)(!Arms[i].Direction);
			if (afterimageOutwardness > 0f)
			{
				for (int l = 0; l < 4; l++)
				{
					Vector2 drawOffset3 = ((float)Math.PI * 2f * (float)l / 4f).ToRotationVector2() * afterimageOutwardness;
					Main.EntitySpriteDraw(handTexture, handDrawPosition + drawOffset3, null, backAfterimageColor, Arms[i].Limbs[1].Rotation - (float)Math.PI / 2f, handTexture.Size() * new Vector2(0.5f, 0f), base.Projectile.scale, handDirection);
				}
			}
			Main.EntitySpriteDraw(handTexture, handDrawPosition, null, drawColor, Arms[i].Limbs[1].Rotation - (float)Math.PI / 2f, handTexture.Size() * new Vector2(0.5f, 0f), base.Projectile.scale, handDirection);
		}
		Vector2 drawPosition;
		for (int m = 0; m < Segments.Length; m++)
		{
			Texture2D textureToUse = ((m % 2 == 1) ? bodyTexture2 : bodyTexture);
			if (m == Segments.Length - 1)
			{
				textureToUse = tailTexture;
			}
			drawPosition = Segments[m].CurrentPosition - Main.screenPosition;
			lightColor = Lighting.GetColor((int)(drawPosition.X + Main.screenPosition.X) / 16, (int)(drawPosition.Y + Main.screenPosition.Y) / 16);
			if (afterimageOutwardness > 0f)
			{
				for (int n = 0; n < 4; n++)
				{
					Vector2 drawOffset4 = ((float)Math.PI * 2f * (float)n / 4f).ToRotationVector2() * afterimageOutwardness;
					Main.EntitySpriteDraw(textureToUse, drawPosition + drawOffset4, null, backAfterimageColor, Segments[m].Rotation, textureToUse.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
				}
			}
			Main.EntitySpriteDraw(textureToUse, drawPosition, null, base.Projectile.GetAlpha(lightColor), Segments[m].Rotation, textureToUse.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		drawPosition = base.Projectile.Center - Main.screenPosition;
		lightColor = Lighting.GetColor((int)(drawPosition.X + Main.screenPosition.X) / 16, (int)(drawPosition.Y + Main.screenPosition.Y) / 16);
		for (int num = -1; num <= 1; num += 2)
		{
			float jawBaseOffset = 24f;
			SpriteEffects jawSpriteEffect = (SpriteEffects)(num == 1);
			Vector2 jawPosition = base.Projectile.Center - Main.screenPosition;
			jawPosition += Vector2.UnitX.RotatedBy(base.Projectile.rotation + JawRotation * (float)num) * (float)num * (jawBaseOffset + (float)Math.Sin(JawRotation) * 14f);
			jawPosition -= Vector2.UnitY.RotatedBy(base.Projectile.rotation) * (26f + (float)Math.Sin(JawRotation) * 8f);
			Main.EntitySpriteDraw(jawTexture, jawPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation + JawRotation * (float)num, jawTexture.Size() * 0.5f, base.Projectile.scale * 1.25f, jawSpriteEffect);
		}
		if (afterimageOutwardness > 0f)
		{
			for (int num2 = 0; num2 < 4; num2++)
			{
				Vector2 drawOffset5 = ((float)Math.PI * 2f * (float)num2 / 4f).ToRotationVector2() * afterimageOutwardness;
				Main.EntitySpriteDraw(headTexture, drawPosition + drawOffset5, null, backAfterimageColor, base.Projectile.rotation, headTexture.Size() * 0.5f, base.Projectile.scale * 1.25f, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(headTexture, drawPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, headTexture.Size() * 0.5f, base.Projectile.scale * 1.25f, (SpriteEffects)0);
		if (afterimageOutwardness > 0f)
		{
			for (int num3 = 0; num3 < (int)((float)base.Projectile.oldPos.Length * angerFactor); num3++)
			{
				drawPosition = base.Projectile.Center - Main.screenPosition - base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 3f * (float)num3;
				Color fadeColor = Color.White * (1f - (float)num3 / (float)base.Projectile.oldPos.Length);
				Main.EntitySpriteDraw(eyesTexture, drawPosition, null, base.Projectile.GetAlpha(fadeColor), base.Projectile.oldRot[num3], eyesTexture.Size() * 0.5f, base.Projectile.scale * 1.25f, (SpriteEffects)0);
			}
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		for (int i = 0; i < Segments.Length; i++)
		{
			Rectangle val = Utils.CenteredRectangle(Segments[i].CurrentPosition, Vector2.One * 52f);
			if (((Rectangle)(ref val)).Intersects(targetHitbox))
			{
				return true;
			}
		}
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (target.type == ModContent.NPCType<ExhumedHeart>())
		{
			return HeartAttackCountdown > 0;
		}
		return null;
	}
}
