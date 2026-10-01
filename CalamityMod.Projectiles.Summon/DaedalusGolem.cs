using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.Projectiles.Summon;

public class DaedalusGolem : ModProjectile, ILocalizedModType, IModType
{
	public int AttackTimer;

	public bool UsingChargedLaserAttack;

	public const int ChargedPelletAttackTime = 30;

	public const int ChargedLaserAttackTime = 120;

	public const float Gravity = 0.35f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public bool Stuck
	{
		get
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			if (!(StuckWalkThroughWallsTimer >= 40f))
			{
				return Collision.SolidCollision(base.Projectile.Center, 2, 2);
			}
			return true;
		}
	}

	public Vector2 ArmPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + new Vector2((base.Projectile.spriteDirection == 1) ? (-4f) : 32f, 0f);
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float StuckWalkThroughWallsTimer => ref base.Projectile.ai[0];

	public ref float StuckJumpSpeed => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 18;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 58;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 90000;
		base.Projectile.penetrate = -1;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(AttackTimer);
		writer.Write(UsingChargedLaserAttack);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		AttackTimer = reader.ReadInt32();
		UsingChargedLaserAttack = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		Main.projFrames[base.Type] = 16;
		bool num = base.Projectile.type == ModContent.ProjectileType<DaedalusGolem>();
		Owner.AddBuff(ModContent.BuffType<DaedalusGolemBuff>(), 3600);
		if (num)
		{
			if (Owner.dead)
			{
				Owner.Calamity().daedalusGolem = false;
			}
			if (Owner.Calamity().daedalusGolem)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (base.Projectile.velocity.Y < 15f)
		{
			base.Projectile.velocity.Y += 0.35f;
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(900f, Owner);
		Vector2 destination;
		if (potentialTarget == null)
		{
			destination = Owner.Center - Vector2.UnitX * (80f + (float)base.Projectile.identity * 28f % 560f) * (float)Owner.direction;
		}
		else
		{
			Vector2 destA = potentialTarget.Center + Vector2.UnitX * (130f + (float)base.Projectile.identity * 28f % 560f);
			Vector2 destB = potentialTarget.Center - Vector2.UnitX * (130f + (float)base.Projectile.identity * 28f % 560f);
			Vector2 val = base.Projectile.Center - destA;
			float num2 = ((Vector2)(ref val)).Length();
			val = base.Projectile.Center - destB;
			destination = ((!(num2 < ((Vector2)(ref val)).Length())) ? destB : destA);
		}
		try
		{
			Vector2 upwardCheck = destination - Vector2.UnitY * 2400f;
			upwardCheck.Y = Utils.Clamp(upwardCheck.Y, 0f, (float)Main.maxTilesY * 16f - 100f);
			WorldUtils.Find(upwardCheck.ToTileCoordinates(), Searches.Chain(new Searches.Down(200), new Conditions.IsSolid()), out var loweredPoint);
			destination = loweredPoint.ToWorldCoordinates();
		}
		catch (NullReferenceException)
		{
		}
		StuckWalkThroughWallsTimer = Utils.Clamp(StuckWalkThroughWallsTimer, 0f, 160f);
		if (base.Projectile.Distance(Owner.Center) > 3500f)
		{
			base.Projectile.Center = Owner.Center;
			StuckWalkThroughWallsTimer = 0f;
			base.Projectile.netImportant = true;
		}
		if ((MoveToDestination(destination) || AttackTimer > 0) && potentialTarget != null)
		{
			AttackTimer++;
			if (AttackTimer == 1)
			{
				UsingChargedLaserAttack = Main.rand.NextBool(7);
				base.Projectile.netUpdate = true;
			}
			if (AttackTimer >= (UsingChargedLaserAttack ? 120 : 30))
			{
				AttackTimer = 0;
				base.Projectile.netUpdate = true;
			}
			if (MathHelper.Distance(potentialTarget.Center.X, base.Projectile.Center.X) > 30f)
			{
				base.Projectile.spriteDirection = (potentialTarget.Center.X - base.Projectile.Center.X < 0f).ToDirectionInt();
			}
			if (UsingChargedLaserAttack)
			{
				if (AttackTimer >= 45 && AttackTimer < 60 && !Main.dedServ)
				{
					Vector2 drawOffset = Main.rand.NextVector2CircularEdge(12f, 12f);
					Dust dust = Dust.NewDustPerfect(ArmPosition + drawOffset, 261);
					dust.velocity = drawOffset.SafeNormalize(Vector2.Zero) * -2.5f;
					dust.color = Color.Lerp(Color.HotPink, Color.LightPink, Main.rand.NextFloat());
					dust.scale = Main.rand.NextFloat(1.2f, 1.45f);
					dust.noGravity = true;
				}
				else
				{
					if (AttackTimer < 60 || AttackTimer > 120 || AttackTimer % 16 != 15)
					{
						return;
					}
					SoundEngine.PlaySound(in SoundID.Item122, ArmPosition);
					if (Main.myPlayer == base.Projectile.owner)
					{
						Vector2 initialVelocity = base.Projectile.SafeDirectionTo(potentialTarget.Center) * 2f;
						if (Main.rand.NextBool())
						{
							initialVelocity = initialVelocity.RotatedByRandom(0.4000000059604645);
						}
						float initialAngle = initialVelocity.ToRotation();
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), ArmPosition, initialVelocity, ModContent.ProjectileType<DaedalusLightning>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, initialAngle, Main.rand.Next(100));
					}
				}
			}
			else if (!UsingChargedLaserAttack && AttackTimer == 15 && Main.myPlayer == base.Projectile.owner)
			{
				Vector2 initialVelocity2 = base.Projectile.SafeDirectionTo(potentialTarget.Center + potentialTarget.velocity * 15f) * 19f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), ArmPosition, initialVelocity2, ModContent.ProjectileType<DaedalusPellet>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		else if (potentialTarget == null && AttackTimer != 0)
		{
			AttackTimer = 0;
			base.Projectile.netUpdate = true;
		}
	}

	public bool MoveToDestination(Vector2 destination)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		Tile tileBelow = CalamityUtils.ParanoidTileRetrieval((int)(base.Projectile.Bottom.X / 16f), (int)(base.Projectile.Bottom.Y / 16f));
		if (Stuck)
		{
			StuckJumpSpeed = 0f;
			base.Projectile.tileCollide = false;
			if (base.Projectile.DistanceSQ(destination - Vector2.UnitY * 16f) > 100f)
			{
				base.Projectile.velocity = base.Projectile.SafeDirectionTo(destination - Vector2.UnitY * 16f) * 6f;
			}
			else
			{
				StuckWalkThroughWallsTimer = 0f;
			}
			StuckWalkThroughWallsTimer -= 4f;
			return false;
		}
		base.Projectile.tileCollide = true;
		if (Math.Abs(base.Projectile.Center.X - destination.X) < 55f + Math.Abs(base.Projectile.velocity.X))
		{
			StuckJumpSpeed = 0f;
			base.Projectile.velocity.X *= 0.8f;
			return true;
		}
		int currentWalkDirection = Math.Sign(base.Projectile.velocity.X);
		int tilesSearchedAhead = 0;
		while (!CalamityUtils.ParanoidTileRetrieval((int)(base.Projectile.Bottom.X / 16f) + currentWalkDirection, (int)(base.Projectile.Bottom.Y / 16f)).IsTileSolidGround())
		{
			tilesSearchedAhead++;
			if (tilesSearchedAhead >= 4)
			{
				break;
			}
		}
		int directionToWalk = Math.Sign(destination.X - base.Projectile.Center.X);
		float idealWalkSpeed = 10f * (float)directionToWalk;
		float walkAcceleration = ((directionToWalk != currentWalkDirection) ? 0.325f : 0.2f);
		base.Projectile.velocity.X = MathHelper.Lerp(base.Projectile.velocity.X, idealWalkSpeed, walkAcceleration);
		if (tileBelow.IsTileSolidGround() || Collision.SolidCollision(base.Projectile.Center, 10, 10))
		{
			if (Math.Abs(base.Projectile.oldPosition.X - base.Projectile.position.X) < 2f || Collision.SolidCollision(base.Projectile.Center, 2, 2) || !Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, Owner.position, Owner.width, Owner.height))
			{
				base.Projectile.velocity.Y = -12f - StuckJumpSpeed;
				StuckJumpSpeed += 3.5f;
				StuckJumpSpeed = Utils.Clamp(StuckJumpSpeed, 0f, 14f);
				StuckWalkThroughWallsTimer += 10f;
				base.Projectile.ForceNetUpdate();
			}
			else if (tilesSearchedAhead > 0)
			{
				base.Projectile.velocity.X = 7f;
				base.Projectile.velocity.Y = 0f - (5f + (float)tilesSearchedAhead * 2f);
				base.Projectile.ForceNetUpdate();
			}
			else
			{
				StuckJumpSpeed = 0f;
				StuckWalkThroughWallsTimer -= 5f;
			}
		}
		base.Projectile.spriteDirection = (Owner.Center.X - base.Projectile.Center.X < 0f).ToDirectionInt();
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		fallThrough = base.Projectile.Bottom.Y < Owner.Top.Y;
		return true;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		int startingWalkFrame = 6;
		int endingWalkFrame = 10;
		int startingPelletFrame = 1;
		int endingPelletFrame = 4;
		int startingBeamFrame = 11;
		int endingBeamFrame = 15;
		base.Projectile.frameCounter++;
		Tile tileBelow = CalamityUtils.ParanoidTileRetrieval((int)(base.Projectile.Bottom.X / 16f), (int)(base.Projectile.Bottom.Y / 16f));
		if (Stuck)
		{
			base.Projectile.frame = 5;
		}
		else if (AttackTimer > 0)
		{
			if (UsingChargedLaserAttack)
			{
				base.Projectile.frame = (int)MathHelper.Lerp((float)startingBeamFrame, (float)(endingBeamFrame + 1), Utils.GetLerpValue(0f, 120f, AttackTimer, clamped: true));
			}
			else
			{
				base.Projectile.frame = (int)MathHelper.Lerp((float)startingPelletFrame, (float)(endingPelletFrame + 1), Utils.GetLerpValue(0f, 30f, AttackTimer, clamped: true));
			}
		}
		else if (Math.Abs(base.Projectile.velocity.X) > 5f && Math.Abs(base.Projectile.velocity.Y) < 2f && tileBelow.IsTileSolidGround())
		{
			if (base.Projectile.frameCounter >= Utils.Clamp(2, 6, (int)Math.Abs((double)base.Projectile.velocity.X * 0.8)))
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame++;
				if (base.Projectile.frame >= endingWalkFrame)
				{
					base.Projectile.frame = startingWalkFrame;
				}
			}
			base.Projectile.frame = Utils.Clamp(base.Projectile.frame, startingWalkFrame, endingWalkFrame);
		}
		else if (Math.Abs(base.Projectile.velocity.X) <= 1f)
		{
			base.Projectile.frame = 0;
		}
	}
}
