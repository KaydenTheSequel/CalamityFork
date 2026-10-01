using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace CalamityMod.NPCs.AcidRain;

[AutoloadBossHead]
public class Mauler : ModNPC
{
	public enum MaulerAttackState
	{
		BubbleBursts,
		HorizontalAcidCharge,
		BigChargeupDash
	}

	public static Asset<Texture2D> GlowTexture;

	public static readonly SoundStyle RoarSound = new SoundStyle("CalamityMod/Sounds/Custom/MaulerRoar");

	public static int DropletDamage = 55;

	public static int BubbleDamage = 55;

	public static int GFBDeathBombDamage = 100;

	public bool InWater
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return Collision.DrownCollision(base.NPC.Center, 1, 1);
		}
	}

	public Player Target => Main.player[base.NPC.target];

	public MaulerAttackState CurrentAttack
	{
		get
		{
			return (MaulerAttackState)base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = (float)value;
		}
	}

	public ref float AttackTimer => ref base.NPC.ai[1];

	public int CurrentFrame
	{
		get
		{
			return (int)base.NPC.localAI[0];
		}
		set
		{
			base.NPC.localAI[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.425f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.9f;
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 10f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glowmask", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 135;
		base.NPC.width = 180;
		base.NPC.height = 90;
		base.NPC.defense = 50;
		base.NPC.lifeMax = 90000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 20);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath60;
		base.NPC.knockBackResist = 0f;
		base.NPC.waterMovementSpeed = 1f;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Mauler")
		});
	}

	public override void AI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		base.NPC.noTileCollide = false;
		base.NPC.noGravity = InWater;
		switch (CurrentAttack)
		{
		case MaulerAttackState.BubbleBursts:
			DoBehavior_BubbleBursts();
			break;
		case MaulerAttackState.HorizontalAcidCharge:
			DoBehavior_HorizontalAcidCharge();
			break;
		case MaulerAttackState.BigChargeupDash:
			DoBehavior_BigChargeupDash();
			break;
		}
		Vector2 start = base.NPC.Center - Vector2.UnitX * 15f;
		Vector2 right = base.NPC.Center + Vector2.UnitX * 15f;
		Color lime = Color.Lime;
		DelegateMethods.v3_1 = ((Color)(ref lime)).ToVector3();
		Utils.PlotTileLine(start, right, 10f, DelegateMethods.CastLightOpen);
		if (base.NPC.Center.X < ((float)Main.offLimitBorderTiles + 6f) * 16f || base.NPC.Center.X >= ((float)(Main.maxTilesX - Main.offLimitBorderTiles) - 6f) * 16f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.9f;
			base.NPC.Center = base.NPC.Center.MoveTowards(Target.Center, 12f);
		}
		AttackTimer++;
	}

	public void DoBehavior_BubbleBursts()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		int roarDelay = 32;
		int shootDelay = 24;
		int bubblesPerBurst = 7;
		int burstShootRate = 45;
		int burstCount = 3;
		float wrappedAttackTimer = (AttackTimer - (float)roarDelay - (float)shootDelay) % (float)burstShootRate;
		NPC nPC = base.NPC;
		nPC.velocity *= 0.94f;
		if (!InWater)
		{
			base.NPC.noTileCollide = true;
			base.NPC.position.Y += 6f;
		}
		if (AttackTimer <= (float)roarDelay)
		{
			CurrentFrame = (int)Math.Round(Utils.GetLerpValue(0f, roarDelay, AttackTimer, clamped: true) * 5f);
		}
		else if (AttackTimer >= (float)(roarDelay + shootDelay))
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter >= 5.0)
			{
				CurrentFrame = (CurrentFrame + 1) % Main.npcFrameCount[base.Type];
				base.NPC.frameCounter = 0.0;
			}
			if (wrappedAttackTimer >= (float)burstShootRate * 0.7f)
			{
				CurrentFrame = 4;
			}
		}
		if (AttackTimer == (float)roarDelay)
		{
			SoundEngine.PlaySound(in SoundID.Zombie97, Target.Center);
		}
		int spriteDirection = base.NPC.spriteDirection;
		float idealRotation = base.NPC.AngleTo(Target.Center);
		if (spriteDirection == 1)
		{
			idealRotation += (float)Math.PI;
		}
		base.NPC.spriteDirection = (Target.Center.X < base.NPC.Center.X).ToDirectionInt();
		if (spriteDirection != base.NPC.spriteDirection)
		{
			base.NPC.rotation += (float)Math.PI;
		}
		base.NPC.rotation = base.NPC.rotation.AngleTowards(idealRotation, 0.125f).AngleLerp(idealRotation, 0.075f);
		if (AttackTimer >= (float)(roarDelay + shootDelay) && wrappedAttackTimer == (float)burstShootRate - 1f)
		{
			Vector2 mouthPosition = base.NPC.Center + Utils.RotatedBy(new Vector2((float)base.NPC.spriteDirection * -56f, 22f), (double)base.NPC.rotation, default(Vector2));
			SoundEngine.PlaySound(in SoundID.Item96, mouthPosition);
			if (Main.netMode != 1)
			{
				int bubbleShootType = ModContent.ProjectileType<MaulerAcidBubble>();
				Vector2 baseBubbleShootVelocity = base.NPC.SafeDirectionTo(mouthPosition) * 13.5f;
				for (int i = 0; i < bubblesPerBurst; i++)
				{
					Vector2 bubbleShootVelocity = baseBubbleShootVelocity + Main.rand.NextVector2Circular(4f, 4f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouthPosition, bubbleShootVelocity, bubbleShootType, BubbleDamage, 0f);
				}
				if (InWater && Collision.CanHit(base.NPC.Center, 1, 1, base.NPC.Center - baseBubbleShootVelocity.SafeNormalize(Vector2.Zero) * 100f, 1, 1))
				{
					NPC nPC2 = base.NPC;
					nPC2.velocity -= baseBubbleShootVelocity * 0.62f;
				}
				base.NPC.netUpdate = true;
			}
		}
		if (AttackTimer >= (float)(roarDelay + shootDelay + burstShootRate * burstCount))
		{
			SelectNextAttack();
		}
	}

	public void DoBehavior_HorizontalAcidCharge()
	{
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		int stuckGiveupTime = 360;
		int chargeDelay = 45;
		int chargePreparationTime = 15;
		int chargeTime = 72;
		int acidShootRate = 4;
		float chargeSpeed = 24.5f;
		ref float stuckTimer = ref base.NPC.ai[2];
		ref float chargeDirection = ref base.NPC.ai[3];
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 5.0)
		{
			CurrentFrame = (CurrentFrame + 1) % Main.npcFrameCount[base.Type];
			base.NPC.frameCounter = 0.0;
		}
		base.NPC.rotation = base.NPC.velocity.X * 0.0125f;
		if (Math.Abs(base.NPC.velocity.X) > 0.1f)
		{
			base.NPC.spriteDirection = (base.NPC.velocity.X < 0f).ToDirectionInt();
		}
		if (AttackTimer < (float)(chargeDelay + chargePreparationTime))
		{
			base.NPC.spriteDirection = (Target.Center.X < base.NPC.Center.X).ToDirectionInt();
		}
		Vector2 idealPosition = Target.Center + Vector2.UnitX * (float)(Target.Center.X < base.NPC.Center.X).ToDirectionInt() * 480f;
		if (!InWater && AttackTimer < (float)chargeDelay)
		{
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
			AttackTimer = 0f;
			if (WorldUtils.Find(idealPosition.ToTileCoordinates(), Searches.Chain(new Searches.Down(1500), new CustomConditions.IsWater()), out var hitPoint))
			{
				idealPosition = hitPoint.ToWorldCoordinates();
			}
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(idealPosition) * 20f;
			if (!base.NPC.WithinRange(idealPosition, 120f))
			{
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, idealVelocity, 0.05f);
			}
			stuckTimer++;
			if (stuckTimer >= (float)stuckGiveupTime)
			{
				base.NPC.Opacity = 1f;
				SelectNextAttack();
			}
		}
		else
		{
			stuckTimer = 0f;
		}
		base.NPC.Opacity = MathHelper.Clamp(base.NPC.Opacity + (float)InWater.ToDirectionInt() * 0.04f, 0.33f, 1f);
		if (AttackTimer >= (float)chargeDelay && AttackTimer < (float)(chargeDelay + chargePreparationTime))
		{
			if (chargeDirection == 0f)
			{
				chargeDirection = (Target.Center.X > base.NPC.Center.X).ToDirectionInt();
				base.NPC.netUpdate = true;
			}
			base.NPC.position.Y += base.NPC.SafeDirectionTo(Target.Center).Y * 8f;
			base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, Vector2.UnitX * chargeDirection * chargeSpeed, 0.075f);
		}
		bool isCharging = AttackTimer >= (float)(chargeDelay + chargePreparationTime) && AttackTimer < (float)(chargeDelay + chargePreparationTime + chargeTime);
		if (isCharging && AttackTimer % (float)acidShootRate == (float)acidShootRate - 1f)
		{
			SoundEngine.PlaySound(in SoundID.Item95, base.NPC.Center);
			if (Main.netMode != 1)
			{
				int acidShootType = ModContent.ProjectileType<MaulerAcidDrop>();
				Vector2 acidSpawnPosition = base.NPC.Center + Main.rand.NextVector2Circular(30f, 10f).RotatedBy(base.NPC.rotation);
				Vector2 acidShootVelocity = -Vector2.UnitY.RotatedByRandom(0.33000001311302185) * Main.rand.NextFloat(8f, 10.5f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), acidSpawnPosition, acidShootVelocity, acidShootType, DropletDamage, 0f);
				base.NPC.netUpdate = true;
			}
		}
		if (((Vector2)(ref base.NPC.velocity)).Length() < 0.4f)
		{
			base.NPC.velocity = Vector2.Zero;
		}
		Vector2 aheadPosition = base.NPC.position + base.NPC.velocity.SafeNormalize(Vector2.UnitX * (float)(-base.NPC.spriteDirection)) * 150f;
		bool stuckWhileCharging = !Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, aheadPosition, base.NPC.width, base.NPC.height) || Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height);
		stuckWhileCharging &= isCharging;
		if ((AttackTimer >= (float)(chargeDelay + chargePreparationTime + chargeTime)) | stuckWhileCharging)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.5f;
			SelectNextAttack();
		}
	}

	public void DoBehavior_BigChargeupDash()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		int chargeDelay = 105;
		int telegraphTime = 60;
		int chargeTime = 50;
		float chargeSpeed = MathHelper.Clamp(base.NPC.Distance(Target.Center) / (float)chargeTime * 1.35f, 29f, 45f);
		bool hasCharged = AttackTimer >= (float)chargeDelay;
		if (!hasCharged)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.94f;
			base.NPC.noGravity = true;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= (double)(hasCharged ? 4 : 7))
		{
			CurrentFrame = (CurrentFrame + 1) % Main.npcFrameCount[base.Type];
			base.NPC.frameCounter = 0.0;
		}
		if (CurrentFrame < 4 && base.NPC.WithinRange(Target.Center, 200f) && AttackTimer > (float)chargeDelay)
		{
			CurrentFrame = 4;
		}
		int previousSpriteDirection = base.NPC.spriteDirection;
		float idealRotation = base.NPC.AngleTo(Target.Center);
		if (previousSpriteDirection == 1)
		{
			idealRotation += (float)Math.PI;
		}
		if (AttackTimer == (float)(chargeDelay - telegraphTime))
		{
			SoundEngine.PlaySound(in RoarSound, Target.Center + Target.SafeDirectionTo(base.NPC.Center) * 300f);
		}
		if (AttackTimer == (float)chargeDelay)
		{
			SoundEngine.PlaySound(in SoundID.DD2_FlameburstTowerShot, Target.Center);
			base.NPC.velocity = base.NPC.SafeDirectionTo(Target.Center) * chargeSpeed;
			base.NPC.netUpdate = true;
		}
		if (!hasCharged)
		{
			base.NPC.spriteDirection = (Target.Center.X < base.NPC.Center.X).ToDirectionInt();
			if (previousSpriteDirection != base.NPC.spriteDirection)
			{
				base.NPC.rotation += (float)Math.PI;
			}
			base.NPC.rotation = base.NPC.rotation.AngleTowards(idealRotation, 0.18f).AngleLerp(idealRotation, 0.08f);
		}
		else
		{
			base.NPC.noGravity = (base.NPC.noTileCollide = true);
			base.NPC.rotation = base.NPC.velocity.ToRotation();
			if (previousSpriteDirection == 1)
			{
				base.NPC.rotation += (float)Math.PI;
			}
		}
		if (AttackTimer >= (float)(chargeDelay + chargeTime))
		{
			SelectNextAttack();
		}
	}

	public void SelectNextAttack()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		AttackTimer = 0f;
		base.NPC.ai[2] = 0f;
		base.NPC.ai[3] = 0f;
		MaulerAttackState oldAttack = CurrentAttack;
		bool targetDefinitelyNotNearWater = false;
		bool isInCrampedPosition = Collision.SolidCollision(base.NPC.Center - Vector2.One * 150f, 300, 300) && InWater;
		if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
		{
			isInCrampedPosition = true;
		}
		if (Target.position.X > 8000f && Target.position.X < (float)Main.maxTilesX * 16f - 8000f)
		{
			targetDefinitelyNotNearWater = true;
		}
		if (WorldUtils.Find(Target.Center.ToTileCoordinates(), Searches.Chain(new Searches.Down(750), new CustomConditions.IsWater()), out var hitPoint))
		{
			if (Target.Distance(hitPoint.ToWorldCoordinates()) > 6400f)
			{
				targetDefinitelyNotNearWater = true;
			}
		}
		else
		{
			targetDefinitelyNotNearWater = true;
		}
		WeightedRandom<MaulerAttackState> attackSelector = new WeightedRandom<MaulerAttackState>(Main.rand);
		attackSelector.Add(MaulerAttackState.BubbleBursts);
		attackSelector.Add(MaulerAttackState.HorizontalAcidCharge);
		attackSelector.Add(MaulerAttackState.BigChargeupDash);
		do
		{
			CurrentAttack = attackSelector.Get();
		}
		while (oldAttack == CurrentAttack);
		if (targetDefinitelyNotNearWater | isInCrampedPosition)
		{
			CurrentAttack = MaulerAttackState.BigChargeupDash;
		}
		base.NPC.netUpdate = true;
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
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Texture2D glowmask = GlowTexture.Value;
		Vector2 drawPosition = base.NPC.Center - screenPos + Vector2.UnitY * base.NPC.gfxOffY;
		Vector2 origin = base.NPC.frame.Size() * 0.5f;
		SpriteEffects direction = (SpriteEffects)(base.NPC.spriteDirection == 1);
		spriteBatch.Draw(texture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, direction, 0f);
		spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(Color.White), base.NPC.rotation, origin, base.NPC.scale, direction, 0f);
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance);
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frame.Y = CurrentFrame * frameHeight;
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter >= 5.0)
			{
				CurrentFrame = (CurrentFrame + 1) % Main.npcFrameCount[base.Type];
				base.NPC.frameCounter = 0.0;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 420);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(319, 1, 2, 4);
		npcLoot.Add(ModContent.ItemType<SulphuricAcidCannon>(), 3);
		npcLoot.Add(ModContent.ItemType<MaulerTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<MaulerRelic>());
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 30; i++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Mauler").Type, base.NPC.scale);
			for (int j = 2; j <= 5; j++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>($"Mauler{j}").Type, base.NPC.scale);
			}
		}
	}

	public override void OnKill()
	{
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			Vector2 valueBoom = default(Vector2);
			((Vector2)(ref valueBoom))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
			float spreadBoom = 0.261f;
			double startAngleBoom = Math.Atan2(base.NPC.velocity.X, base.NPC.velocity.Y) - (double)(spreadBoom / 2f);
			double deltaAngleBoom = spreadBoom / 8f;
			for (int iBoom = 0; iBoom < 25; iBoom++)
			{
				int projectileType = (Main.rand.NextBool() ? ModContent.ProjectileType<SulphuricAcidMist>() : ModContent.ProjectileType<SulphuricAcidBubble>());
				double offsetAngleBoom = startAngleBoom + deltaAngleBoom * (double)(iBoom + iBoom * iBoom) / 2.0 + (double)(32f * (float)iBoom);
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_Death(), valueBoom.X, valueBoom.Y, (float)(Math.Sin(offsetAngleBoom) * 6.0), (float)(Math.Cos(offsetAngleBoom) * 6.0), projectileType, GFBDeathBombDamage, 0f, Main.myPlayer);
					Projectile.NewProjectile(base.NPC.GetSource_Death(), valueBoom.X, valueBoom.Y, (float)((0.0 - Math.Sin(offsetAngleBoom)) * 6.0), (float)((0.0 - Math.Cos(offsetAngleBoom)) * 6.0), projectileType, GFBDeathBombDamage, 0f, Main.myPlayer);
				}
			}
			for (int i = 0; i < 25; i++)
			{
				int deathDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 31, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[deathDust];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[deathDust].scale = 0.5f;
					Main.dust[deathDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
				Main.dust[deathDust].noGravity = true;
			}
			for (int j = 0; j < 50; j++)
			{
				int deathDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 3f);
				Main.dust[deathDust2].noGravity = true;
				Dust obj2 = Main.dust[deathDust2];
				obj2.velocity *= 5f;
				deathDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
				Dust obj3 = Main.dust[deathDust2];
				obj3.velocity *= 2f;
				Main.dust[deathDust2].noGravity = true;
			}
			base.NPC.netUpdate = true;
		}
		DownedBossSystem.downedMauler = true;
		CalamityNetcode.SyncWorld();
	}
}
