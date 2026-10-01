using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class ApotheosisWorm : ModProjectile, ILocalizedModType, IModType
{
	internal class Segment
	{
		internal short Alpha;

		internal float Rotation;

		internal Vector2 Center;

		internal void WriteTo(BinaryWriter writer)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			writer.Write(Alpha);
			writer.Write(Rotation);
			writer.WritePackedVector2(Center);
		}

		internal void ReadFrom(BinaryReader reader)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			Alpha = reader.ReadInt16();
			Rotation = reader.ReadSingle();
			Center = reader.ReadPackedVector2();
		}

		internal Segment(byte alpha, float rotation, Vector2 center)
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			base._002Ector();
			Alpha = alpha;
			Rotation = rotation;
			Center = center;
		}
	}

	internal Vector2 PortalPosition;

	internal Segment[] Segments = new Segment[80];

	public new string LocalizationCategory => "Projectiles.Magic";

	internal Player Owner => Main.player[base.Projectile.owner];

	internal ref float Time => ref base.Projectile.ai[0];

	internal ref float FlyAcceleration => ref base.Projectile.ai[1];

	internal ref float JawRotation => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 108;
		base.Projectile.height = 108;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.light = 1.5f;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		if (Segments == null || Segments[0] == null)
		{
			InitializeSegments();
		}
		for (int i = 0; i < Segments.Length; i++)
		{
			Segments[i].WriteTo(writer);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		if (Segments == null || Segments[0] == null)
		{
			InitializeSegments();
		}
		for (int i = 0; i < Segments.Length; i++)
		{
			Segments[i].ReadFrom(reader);
		}
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			PortalPosition = base.Projectile.Center + base.Projectile.velocity * 7f;
			if (Main.myPlayer == base.Projectile.owner)
			{
				InitializeSegments();
			}
			base.Projectile.localAI[0] = 1f;
		}
		if (base.Projectile.timeLeft <= 75)
		{
			base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha + 40, 0, 255);
		}
		else if (Time > 15f)
		{
			base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 40, 0, 255);
		}
		for (int i = 0; i < Segments.Length; i++)
		{
			UpdateSegment(i);
		}
		Time++;
		JawRotation = MathHelper.Lerp(JawRotation, 0f, 0.08f);
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(4200f);
		if (potentialTarget != null)
		{
			AttackTarget(potentialTarget);
		}
	}

	internal void InitializeSegments()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 directionToMouse = (Main.MouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction);
		base.Projectile.rotation = directionToMouse.ToRotation() + (float)Math.PI / 2f;
		for (int i = 0; i < Segments.Length; i++)
		{
			Segments[i] = new Segment(byte.MaxValue, base.Projectile.rotation, base.Projectile.Center + directionToMouse / (float)Segments.Length * (float)i * 900f);
		}
		base.Projectile.netUpdate = true;
	}

	internal void UpdateSegment(int segmentIndex)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		float aheadSegmentRotation = ((segmentIndex > 0) ? Segments[segmentIndex - 1].Rotation : base.Projectile.rotation);
		Vector2 aheadSegmentCenter = ((segmentIndex > 0) ? Segments[segmentIndex - 1].Center : base.Projectile.Center);
		Vector2 offsetToAheadSegment = aheadSegmentCenter - Segments[segmentIndex].Center;
		if (base.Projectile.timeLeft <= 60)
		{
			int invisibleSegmentIndex = (int)MathHelper.Lerp(0f, (float)Segments.Length, 1f - MathHelper.Clamp((float)base.Projectile.timeLeft / 60f * 1.4f, 0f, 1f));
			if (segmentIndex < invisibleSegmentIndex)
			{
				Segments[segmentIndex].Alpha += 31;
				if (Segments[segmentIndex].Alpha > 255)
				{
					Segments[segmentIndex].Alpha = 255;
				}
			}
		}
		if (Time <= 90f)
		{
			int visibleSegmentIndex = (int)MathHelper.Lerp(0f, (float)Segments.Length, Utils.GetLerpValue(15f, 70f, Time, clamped: true));
			if (segmentIndex < visibleSegmentIndex)
			{
				Segments[segmentIndex].Alpha -= 17;
				if (Segments[segmentIndex].Alpha < 0)
				{
					Segments[segmentIndex].Alpha = 0;
				}
			}
		}
		if (aheadSegmentRotation != Segments[segmentIndex].Rotation)
		{
			float offsetAngle = MathHelper.WrapAngle(aheadSegmentRotation - Segments[segmentIndex].Rotation);
			offsetToAheadSegment = offsetToAheadSegment.RotatedBy(offsetAngle * 0.075f);
		}
		Segments[segmentIndex].Rotation = offsetToAheadSegment.ToRotation() + (float)Math.PI / 2f;
		if (offsetToAheadSegment != Vector2.Zero)
		{
			Segments[segmentIndex].Center = aheadSegmentCenter - offsetToAheadSegment.SafeNormalize(Vector2.Zero) * 70f;
		}
	}

	internal void AttackTarget(NPC target)
	{
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		if (Time >= 40f && base.Projectile.alpha == 0 && Main.rand.NextBool(8))
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Segment segmentToShootFrom = Segments[Main.rand.Next(Segments.Length)];
				Vector2 shootVelocity = (target.Center - segmentToShootFrom.Center).SafeNormalize(Vector2.UnitY).RotatedByRandom(0.25) * 18f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), segmentToShootFrom.Center, shootVelocity, ModContent.ProjectileType<ApotheosisEnergy>(), base.Projectile.damage / 2, 0f, base.Projectile.owner);
			}
			if (Main.rand.NextBool(10))
			{
				SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/MechGaussRifle"), base.Projectile.Center);
			}
		}
		float idealFlyAcceleration = 0.18f;
		Vector2 destination = target.Center;
		float distanceFromDestination = base.Projectile.Distance(destination);
		if (base.Projectile.Distance(destination) > 725f)
		{
			destination += (Time % 30f / 30f * ((float)Math.PI * 2f)).ToRotationVector2() * 145f;
			distanceFromDestination = base.Projectile.Distance(destination);
			idealFlyAcceleration *= 2.5f;
		}
		if (distanceFromDestination > 1500f && Time > 45f)
		{
			idealFlyAcceleration = MathHelper.Min(6f, FlyAcceleration + 1f);
		}
		FlyAcceleration = MathHelper.Lerp(FlyAcceleration, idealFlyAcceleration, 0.3f);
		float directionToTargetOrthogonality = Vector2.Dot(base.Projectile.velocity.SafeNormalize(Vector2.Zero), base.Projectile.SafeDirectionTo(destination));
		if (distanceFromDestination > 320f)
		{
			float speed = ((Vector2)(ref base.Projectile.velocity)).Length();
			if (speed < 23f)
			{
				speed += 0.08f;
			}
			if (speed > 32f)
			{
				speed -= 0.08f;
			}
			if (directionToTargetOrthogonality < 0.85f && directionToTargetOrthogonality > 0.5f)
			{
				speed += 6f;
			}
			if (directionToTargetOrthogonality < 0.5f && directionToTargetOrthogonality > -0.7f)
			{
				speed -= 10f;
			}
			speed = MathHelper.Clamp(speed, 16f, 34f);
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.AngleTo(destination), FlyAcceleration).ToRotationVector2() * speed;
		}
		if (distanceFromDestination < 200f)
		{
			JawRotation = MathHelper.Lerp(JawRotation, MathHelper.ToRadians(-34f), 0.4f);
		}
		else if (distanceFromDestination < 480f)
		{
			JawRotation = MathHelper.Lerp(JawRotation, MathHelper.ToRadians(15f), 0.24f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 300, quiet: true);
		target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 300, quiet: true);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D headTexture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 headTextureOrigin = TextureAssets.Projectile[base.Type].Value.Size() * 0.5f;
		drawPosition -= headTexture.Size() * base.Projectile.scale * 0.5f;
		drawPosition += headTextureOrigin * base.Projectile.scale + new Vector2(0f, 4f + base.Projectile.gfxOffY);
		Texture2D jawTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/ApotheosisJaw", (AssetRequestMode)2).Value;
		Vector2 jawOrigin = jawTexture.Size() * 0.5f;
		Texture2D bodyTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/DevourerofGods/DevourerofGodsBody_P2", (AssetRequestMode)2).Value;
		Texture2D tailTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/DevourerofGods/DevourerofGodsTail_P2", (AssetRequestMode)2).Value;
		Color baseColor = Color.Lerp(Color.White, Color.Fuchsia, 0.15f);
		for (int i = 0; i < Segments.Length; i++)
		{
			Texture2D obj = ((i == Segments.Length - 1) ? tailTexture : bodyTexture);
			Main.EntitySpriteDraw(origin: obj.Size() * 0.5f, texture: obj, position: Segments[i].Center - Main.screenPosition, sourceRectangle: null, color: baseColor * ((float)(255 - Segments[i].Alpha) / 255f), rotation: Segments[i].Rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		}
		for (int j = -1; j <= 1; j += 2)
		{
			float jawBaseOffset = 42f;
			SpriteEffects jawSpriteEffect = (SpriteEffects)0;
			if (j == 1)
			{
				jawSpriteEffect = (SpriteEffects)(jawSpriteEffect | 1);
			}
			Vector2 jawPosition = drawPosition;
			jawPosition += Vector2.UnitX.RotatedBy(base.Projectile.rotation + JawRotation * (float)j) * (float)j * (jawBaseOffset + (float)Math.Sin(JawRotation) * 24f);
			jawPosition -= Vector2.UnitY.RotatedBy(base.Projectile.rotation) * (38f + (float)Math.Sin(JawRotation) * 30f);
			Main.EntitySpriteDraw(jawTexture, jawPosition, null, baseColor * base.Projectile.Opacity, base.Projectile.rotation + JawRotation * (float)j, jawOrigin, base.Projectile.scale, jawSpriteEffect);
		}
		if (Time < 60f)
		{
			float currentFade = Utils.GetLerpValue(0f, 8f, Time, clamped: true) * Utils.GetLerpValue(60f, 52f, Time, clamped: true);
			currentFade *= (1f + 0.2f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 30f * (float)Math.PI * 3f)) * 0.8f;
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/StarProj", (AssetRequestMode)2).Value;
			Vector2 drawPos = PortalPosition - Main.screenPosition;
			baseColor = new Color(150, 100, 255, 255) * base.Projectile.Opacity;
			baseColor *= 0.5f;
			((Color)(ref baseColor)).A = 0;
			Color colorA = baseColor;
			Color colorB = baseColor * 0.5f;
			colorA *= currentFade;
			colorB *= currentFade;
			Vector2 origin = value.Size() / 2f;
			Vector2 scale = new Vector2(4f, 10f) * base.Projectile.Opacity * currentFade;
			Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI / 2f, origin, scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, 0f, origin, scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI / 2f, origin, scale * 0.8f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, 0f, origin, scale * 0.8f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI / 2f + Main.GlobalTimeWrappedHourly * 3f * 0.25f, origin, scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, Main.GlobalTimeWrappedHourly * 3f * 0.25f, origin, scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI / 2f + Main.GlobalTimeWrappedHourly * 3f * 0.5f, origin, scale * 0.8f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, Main.GlobalTimeWrappedHourly * 3f * 0.5f, origin, scale * 0.8f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI / 4f, origin, scale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI * 3f / 4f, origin, scale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI / 4f, origin, scale * 0.4f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI * 3f / 4f, origin, scale * 0.4f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI / 4f + Main.GlobalTimeWrappedHourly * 3f * 0.75f, origin, scale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI * 3f / 4f + Main.GlobalTimeWrappedHourly * 3f * 0.75f, origin, scale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI / 4f + Main.GlobalTimeWrappedHourly * 3f, origin, scale * 0.4f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI * 3f / 4f + Main.GlobalTimeWrappedHourly * 3f, origin, scale * 0.4f, (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(headTexture, drawPosition, null, baseColor * base.Projectile.Opacity, base.Projectile.rotation, headTextureOrigin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (Collision.CheckAABBvAABBCollision(base.Projectile.position, projHitbox.Size(), targetHitbox.TopLeft(), targetHitbox.Size()))
		{
			return true;
		}
		for (int i = 0; i < Segments.Length; i++)
		{
			if (Collision.CheckAABBvAABBCollision(Segments[i].Center - projHitbox.Size() * 0.5f, projHitbox.Size(), targetHitbox.TopLeft(), targetHitbox.Size()))
			{
				return true;
			}
		}
		return false;
	}
}
