using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Critters;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DraedonLabThings;

public class RepairUnitCritter : ModNPC
{
	public enum BehaviorState
	{
		WalkAround,
		WalkOnWalls,
		SitAndRecharge
	}

	public Vector2 PreviousStuckPosition;

	public const float Gravity = 0.4f;

	public static Asset<Texture2D> GlowTexture;

	public BehaviorState CurrentState
	{
		get
		{
			return (BehaviorState)base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = (float)value;
		}
	}

	public ref float StuckCount => ref base.NPC.ai[1];

	public bool Initialized
	{
		get
		{
			return base.NPC.ai[2] == 1f;
		}
		set
		{
			base.NPC.ai[2] = value.ToInt();
		}
	}

	public bool WantsToClimbOnSomeWall
	{
		get
		{
			return base.NPC.ai[3] == 1f;
		}
		set
		{
			base.NPC.ai[3] = value.ToInt();
		}
	}

	public ref float CurrentFrame => ref base.NPC.localAI[0];

	public ref float Time => ref base.NPC.localAI[1];

	public ref float ClimbTime => ref base.NPC.localAI[2];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 17;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
		NPCID.Sets.NormalGoldCritterBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.Y += 12f;
		value.PortraitPositionYOverride = 32f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glowmask", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 0;
		base.NPC.width = 20;
		base.NPC.height = 22;
		base.NPC.lifeMax = 80;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = false;
		base.NPC.chaseable = false;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath44;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<RepairUnitBanner>();
		base.NPC.catchItem = (short)ModContent.ItemType<RepairUnitItem>();
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<ArsenalLabBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.RepairUnitCritter")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(Time);
		writer.Write(ClimbTime);
		writer.WriteVector2(PreviousStuckPosition);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Time = reader.ReadSingle();
		ClimbTime = reader.ReadSingle();
		PreviousStuckPosition = reader.ReadVector2();
	}

	public override bool? CanBeHitByItem(Player player, Item item)
	{
		return null;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		return null;
	}

	public override void AI()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (!Initialized)
		{
			base.NPC.velocity = Vector2.UnitX * (float)Main.rand.NextBool().ToDirectionInt() * 1.5f;
			Initialized = true;
		}
		switch (CurrentState)
		{
		case BehaviorState.WalkAround:
			WalkAroundOnGround();
			ClimbTime = 0f;
			break;
		case BehaviorState.WalkOnWalls:
			WalkOnWalls();
			ClimbTime++;
			break;
		}
		Time++;
		if (Main.netMode != 1 && Main.BestiaryTracker.Kills.GetKillCount(base.NPC) <= 0)
		{
			Main.BestiaryTracker.Kills.RegisterKill(base.NPC);
		}
	}

	public override bool? CanFallThroughPlatforms()
	{
		return CurrentState == BehaviorState.WalkOnWalls;
	}

	public void WalkAroundOnGround()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		Tile tileBelow = CalamityUtils.ParanoidTileRetrieval((int)(base.NPC.Bottom.X / 16f), (int)(base.NPC.Bottom.Y / 16f));
		bool onSolidGround = WorldGen.SolidTile(tileBelow) || (tileBelow.HasTile && Main.tileSolidTop[tileBelow.TileType]);
		float directionSign = Math.Sign(base.NPC.velocity.X);
		if (directionSign == 0f)
		{
			directionSign = base.NPC.spriteDirection;
		}
		if ((Math.Abs(base.NPC.velocity.X) > 0.5f) & onSolidGround)
		{
			base.NPC.velocity.X = MathHelper.Lerp(base.NPC.velocity.X, directionSign * 5f, 0.05f);
		}
		if (onSolidGround)
		{
			base.NPC.frameCounter += ((Vector2)(ref base.NPC.velocity)).Length() + 0.4f;
		}
		else
		{
			CurrentFrame = 0f;
		}
		base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y + 0.4f, -15f, 15f);
		if (Math.Abs(base.NPC.velocity.X) > 0.4f)
		{
			base.NPC.spriteDirection = (base.NPC.velocity.X > 0f).ToDirectionInt();
		}
		float? distanceToAheadBelowTile = CalamityUtils.DistanceToTileCollisionHit(base.NPC.Bottom + new Vector2((float)Math.Sign(base.NPC.velocity.X) * 24f, 12f), Vector2.UnitX * directionSign);
		if (onSolidGround && distanceToAheadBelowTile.HasValue && distanceToAheadBelowTile.Value >= 2f && distanceToAheadBelowTile.Value < 9f && base.NPC.velocity.Y == 0.4f)
		{
			Vector2 jumpDestination = base.NPC.Center + Vector2.UnitX * (float)base.NPC.spriteDirection * (distanceToAheadBelowTile.Value * 16f + 12f);
			base.NPC.velocity = CalamityUtils.GetProjectilePhysicsFiringVelocity(base.NPC.Center, jumpDestination, 0.4f, 10f);
		}
		bool obstacleInWay = !Collision.CanHit(base.NPC.Center - Vector2.UnitX * directionSign * 8f, 2, 2, base.NPC.Center + Vector2.UnitX * directionSign * 50f, 8, 8);
		if (onSolidGround & obstacleInWay)
		{
			Vector2 jumpDestination2 = base.NPC.Center + Vector2.UnitX * (float)base.NPC.spriteDirection * 132f;
			float? distanceToObstacle = CalamityUtils.DistanceToTileCollisionHit(base.NPC.Center, Vector2.UnitX * directionSign);
			float obstacleHeight = 0f;
			for (int i = 0; i < 10; i++)
			{
				if (WorldGen.SolidTile((int)(base.NPC.Center.X / 16f + distanceToObstacle.GetValueOrDefault() * (float)base.NPC.spriteDirection), (int)(base.NPC.Center.Y / 16f) - i))
				{
					obstacleHeight++;
				}
			}
			if (obstacleHeight >= 10f)
			{
				base.NPC.velocity.X = (float)(-base.NPC.spriteDirection) * 3f;
			}
			else
			{
				base.NPC.velocity = CalamityUtils.GetProjectilePhysicsFiringVelocity(base.NPC.Center, jumpDestination2, 0.4f, 9f + StuckCount * 0.7f + obstacleHeight * 0.4f);
			}
			if (MathHelper.Distance(base.NPC.position.X, base.NPC.oldPosition.X) < 0.2f)
			{
				StuckCount++;
				if (StuckCount > 1f && !WantsToClimbOnSomeWall)
				{
					WantsToClimbOnSomeWall = true;
				}
				if (StuckCount > 2f)
				{
					base.NPC.velocity.X *= -0.56f;
					StuckCount = 0f;
				}
				PreviousStuckPosition = base.NPC.Center;
			}
			base.NPC.netUpdate = true;
		}
		bool closeObstacleInWay = !Collision.CanHit(base.NPC.Center, 2, 2, base.NPC.Center + Vector2.UnitX * directionSign * 34f, 8, 8);
		if (onSolidGround & closeObstacleInWay)
		{
			base.NPC.velocity.X *= -0.7f;
			base.NPC.netUpdate = true;
		}
		if (!base.NPC.WithinRange(PreviousStuckPosition, 180f) && StuckCount > 0f)
		{
			StuckCount = 0f;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.frameCounter >= 11.0 && Math.Abs(base.NPC.velocity.Y) < 1f)
		{
			CurrentFrame = (CurrentFrame + 1f) % 8f;
			base.NPC.frameCounter = 0.0;
		}
		if (Time % 300f == 299f && !WantsToClimbOnSomeWall)
		{
			WantsToClimbOnSomeWall = true;
			base.NPC.netUpdate = true;
		}
		if (WantsToClimbOnSomeWall && CalamityUtils.ParanoidTileRetrieval((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16).WallType > 0)
		{
			CurrentState = BehaviorState.WalkOnWalls;
			StuckCount = 0f;
			WantsToClimbOnSomeWall = false;
			base.NPC.position.Y -= 10f;
			base.NPC.netUpdate = true;
		}
	}

	public void WalkOnWalls()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		StuckCount = 0f;
		WantsToClimbOnSomeWall = false;
		float offsetAngle = CalamityUtils.AperiodicSin(Time / 360f, 0f, 1f) * 0.006f;
		Vector2 currentDirection = base.NPC.velocity.SafeNormalize((base.NPC.rotation - (float)Math.PI / 2f).ToRotationVector2());
		Vector2 aheadCheckPosition = base.NPC.Center + currentDirection * 120f;
		Tile aheadTile = CalamityUtils.ParanoidTileRetrieval((int)(aheadCheckPosition.X / 16f), (int)(aheadCheckPosition.Y / 16f));
		bool aboutToCollideWithSomething = CalamityUtils.DistanceToTileCollisionHit(base.NPC.Center - currentDirection * 5f, currentDirection) < 6f;
		bool almostReadyToWalkAgain = ClimbTime > 445f;
		if (((aheadTile.WallType == 0 || WorldGen.SolidTile(aheadTile)) && !almostReadyToWalkAgain) | aboutToCollideWithSomething)
		{
			float num = CalamityUtils.DistanceToTileCollisionHit(base.NPC.Center - currentDirection.RotatedBy(1.5707963705062866) * 5f, currentDirection.RotatedBy(1.5707963705062866)) ?? 10000f;
			float distanceToCollisionRight = CalamityUtils.DistanceToTileCollisionHit(base.NPC.Center - currentDirection.RotatedBy(-1.5707963705062866) * 5f, currentDirection.RotatedBy(-1.5707963705062866)) ?? 10000f;
			float steerRotation = ((num > distanceToCollisionRight) ? ((float)Math.PI / 2f) : (-(float)Math.PI / 2f));
			Vector2 idealVelocity = base.NPC.velocity.RotatedBy(steerRotation);
			base.NPC.velocity = base.NPC.velocity.MoveTowards(idealVelocity, 0.15f);
		}
		else
		{
			base.NPC.velocity = base.NPC.velocity.RotatedBy(offsetAngle);
		}
		base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.Zero) * 1.56f;
		if (base.NPC.collideX)
		{
			base.NPC.velocity.X *= -0.8f;
			NPC nPC = base.NPC;
			nPC.Center += base.NPC.velocity * 2f;
		}
		if (base.NPC.collideY)
		{
			base.NPC.velocity.Y *= -0.8f;
			NPC nPC2 = base.NPC;
			nPC2.Center += base.NPC.velocity * 2f;
		}
		if (((Vector2)(ref base.NPC.velocity)).Length() > 1.4f)
		{
			base.NPC.rotation = base.NPC.rotation.AngleLerp(base.NPC.velocity.ToRotation() + (float)Math.PI / 2f, 0.2f);
		}
		if (Collision.SolidCollision(base.NPC.position + Vector2.One * 24f, base.NPC.width - 12, base.NPC.height - 12))
		{
			NPC nPC3 = base.NPC;
			nPC3.velocity *= -1f;
			base.NPC.position.Y -= 5f;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 4.0)
		{
			if (CurrentFrame < 9f)
			{
				CurrentFrame = 9f;
			}
			CurrentFrame++;
			if (CurrentFrame >= (float)Main.npcFrameCount[base.Type])
			{
				CurrentFrame = 9f;
			}
			base.NPC.frameCounter = 0.0;
		}
		if (CalamityUtils.ParanoidTileRetrieval((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f)).WallType == 0 || ClimbTime > 620f)
		{
			CurrentState = BehaviorState.WalkAround;
			base.NPC.velocity = new Vector2((float)Main.rand.NextBool().ToDirectionInt() * 1.2f, 3f);
			base.NPC.rotation = 0f;
			base.NPC.netUpdate = true;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 226);
		}
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			for (int j = 1; j <= 3; j++)
			{
				Gore.NewGorePerfect(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.7f, 1f), base.Mod.Find<ModGore>($"RepairUnit{j}").Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<DraedonPowerCell>(), 2, 2, 4);
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter += 1.7999999523162842;
			if (base.NPC.frameCounter >= 11.0)
			{
				CurrentFrame++;
				if (CurrentFrame >= 9f)
				{
					CurrentFrame = 1f;
				}
				base.NPC.frameCounter = 0.0;
			}
		}
		base.NPC.frame.Y = (int)(CurrentFrame * (float)frameHeight);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		Texture2D critterTexture = TextureAssets.Npc[base.Type].Value;
		Texture2D glowmask = GlowTexture.Value;
		Vector2 drawPosition = base.NPC.Center - screenPos + Vector2.UnitY * base.NPC.gfxOffY;
		SpriteEffects direction = (SpriteEffects)(base.NPC.spriteDirection == 1);
		spriteBatch.Draw(critterTexture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(Color.White), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		return false;
	}

	public RepairUnitCritter()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PreviousStuckPosition = Vector2.Zero;
		base._002Ector();
	}
}
