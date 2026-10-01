using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Potions;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Skies;
using CalamityMod.Sounds;
using CalamityMod.UI.VanillaBossBars;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ExoMechs.Thanatos;

[HasPierceResist(false)]
[LongDistanceNetSync]
public class ThanatosHead : ModNPC
{
	public enum Phase
	{
		Charge,
		UndergroundLaserBarrage,
		Deathray
	}

	public enum SecondaryPhase
	{
		Nothing,
		Passive,
		PassiveAndImmune
	}

	public static int normalIconIndex;

	public static int vulnerableIconIndex;

	public static readonly SoundStyle VentSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ThanatosVent");

	public static readonly SoundStyle LaserSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/THanosLaser");

	public static readonly SoundStyle GFBeam = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/THanosGFBeam");

	public static readonly SoundStyle ThanatosHitSoundOpen = new SoundStyle("CalamityMod/Sounds/NPCHit/ThanatosHitOpen", 2)
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle ThanatosHitSoundClosed = new SoundStyle("CalamityMod/Sounds/NPCHit/ThanatosHitClosed", 3)
	{
		Volume = 0.4f
	};

	public SlotId LaserSoundSlot;

	public static Asset<Texture2D> GlowTexture;

	public static Asset<Texture2D> AuraTexture;

	public static Asset<Texture2D> ReticleLeftTexture;

	public static Asset<Texture2D> ReticleRightTexture;

	public static Asset<Texture2D> ReticleProngLeftTexture;

	public static Asset<Texture2D> ReticleProngRightTexture;

	public static Asset<Texture2D> ReticleTopTexture;

	public static Asset<Texture2D> ReticleBottomTexture;

	public ThanatosSmokeParticleSet SmokeDrawer = new ThanatosSmokeParticleSet(-1, 3, 0f, 16f, 1.5f);

	private int noContactDamageTimer;

	public const float immunityTime = 600f;

	private bool vulnerable;

	public bool exoMechdusa;

	public const float ventDuration = 180f;

	public const int ventCloudSpawnRate = 10;

	private const float defaultLifeRatio = 5f;

	private const float baseDistance = 800f;

	private const float baseTurnDistance = 160f;

	private const float soundDistance = 2800f;

	public const int minLength = 100;

	private const int maxLength = 101;

	private bool tailSpawned;

	public bool berserkEarlyBugFix;

	private float chargeVelocityScalar;

	private const float deathrayTelegraphDuration = 180f;

	private const float deathrayDuration = 180f;

	public static int LaserDamage = 80;

	public static int BeamDamage = 135;

	public float AIState
	{
		get
		{
			return base.NPC.Calamity().newAI[0];
		}
		set
		{
			base.NPC.Calamity().newAI[0] = value;
		}
	}

	public float SecondaryAIState
	{
		get
		{
			return base.NPC.Calamity().newAI[1];
		}
		set
		{
			base.NPC.Calamity().newAI[1] = value;
		}
	}

	public override void Load()
	{
		string normalIconPath = "CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosNormalHead";
		string vulnerableIconPath = "CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosVulnerableHead";
		normalIconIndex = CalamityMod.Instance.AddBossHeadTexture(normalIconPath);
		vulnerableIconIndex = CalamityMod.Instance.AddBossHeadTexture(vulnerableIconPath);
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.MustAlwaysDraw[base.Type] = true;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.65f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.6f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 52f;
		value.Position.Y += 16f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			ReticleLeftTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosReticleLeft", (AssetRequestMode)2);
			ReticleRightTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosReticleRight", (AssetRequestMode)2);
			ReticleTopTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosReticleTop", (AssetRequestMode)2);
			ReticleBottomTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosReticleHead", (AssetRequestMode)2);
			ReticleProngRightTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosReticleProngLeft", (AssetRequestMode)2);
			ReticleProngLeftTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosReticleProngRight", (AssetRequestMode)2);
			AuraTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Thanatos/THanosAura", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 240;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 164;
		base.NPC.height = 164;
		base.NPC.defense = 100;
		base.NPC.DR_NERD(0.9999f);
		base.NPC.Calamity().unbreakableDR = true;
		base.NPC.LifeMaxNERB(800000, 1150000, 600000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.Opacity = 0f;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(1);
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.DeathSound = CommonCalamitySounds.ExoDeathSound;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<ExoMechsBossBar>();
		base.NPC.chaseable = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Thanatos")
		});
	}

	public override void BossHeadSlot(ref int index)
	{
		if (SecondaryAIState == 2f)
		{
			index = -1;
		}
		else if (vulnerable)
		{
			index = vulnerableIconIndex;
		}
		else
		{
			index = normalIconIndex;
		}
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = base.NPC.rotation;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(noContactDamageTimer);
		writer.Write(chargeVelocityScalar);
		writer.Write(vulnerable);
		writer.Write(exoMechdusa);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.chaseable = reader.ReadBoolean();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		noContactDamageTimer = reader.ReadInt32();
		chargeVelocityScalar = reader.ReadSingle();
		vulnerable = reader.ReadBoolean();
		exoMechdusa = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public float GetSlowdownAreaEdgeRadius(bool lastMechAlive)
	{
		return (CalamityWorld.death ? 600f : (CalamityWorld.revenge ? 700f : (Main.expertMode ? 800f : 1000f))) * (lastMechAlive ? 0.75f : 1f) * ((Main.zenithWorld && !exoMechdusa) ? 2f : (Main.getGoodWorld ? 0.75f : 1f));
	}

	public int CheckForOtherMechs(ref int targetIndex, out bool exoPrimeAlive, out bool exoTwinsAlive)
	{
		exoPrimeAlive = false;
		exoTwinsAlive = false;
		int otherExoMechsAlive = 0;
		if (CalamityGlobalNPC.draedonExoMechPrime != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrime].active)
		{
			targetIndex = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].target;
			otherExoMechsAlive++;
			exoPrimeAlive = true;
			if ((Main.npc[CalamityGlobalNPC.draedonExoMechPrime].ModNPC as AresBody).berserkEarlyBugFix)
			{
				berserkEarlyBugFix = true;
			}
		}
		if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
		{
			otherExoMechsAlive++;
			exoTwinsAlive = true;
			if ((Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].ModNPC as global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo).berserkEarlyBugFix)
			{
				berserkEarlyBugFix = true;
			}
		}
		return otherExoMechsAlive;
	}

	public override void AI()
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11db: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_122a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1232: Unknown result type (might be due to invalid IL or missing references)
		//IL_1237: Unknown result type (might be due to invalid IL or missing references)
		//IL_123c: Unknown result type (might be due to invalid IL or missing references)
		//IL_185a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1865: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19db: Unknown result type (might be due to invalid IL or missing references)
		//IL_19bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_193d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1945: Unknown result type (might be due to invalid IL or missing references)
		//IL_194a: Unknown result type (might be due to invalid IL or missing references)
		//IL_194f: Unknown result type (might be due to invalid IL or missing references)
		//IL_190d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1918: Unknown result type (might be due to invalid IL or missing references)
		//IL_191d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1922: Unknown result type (might be due to invalid IL or missing references)
		//IL_1927: Unknown result type (might be due to invalid IL or missing references)
		//IL_192c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1933: Unknown result type (might be due to invalid IL or missing references)
		//IL_1938: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1962: Unknown result type (might be due to invalid IL or missing references)
		//IL_1971: Unknown result type (might be due to invalid IL or missing references)
		//IL_198e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1995: Unknown result type (might be due to invalid IL or missing references)
		//IL_199a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1510: Unknown result type (might be due to invalid IL or missing references)
		//IL_151b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1520: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1559: Unknown result type (might be due to invalid IL or missing references)
		//IL_155e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1631: Unknown result type (might be due to invalid IL or missing references)
		//IL_1636: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa2: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.draedonExoMechWorm = base.NPC.whoAmI;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		int targetIndex = base.NPC.target;
		int otherExoMechsAlive = CheckForOtherMechs(ref targetIndex, out var exoPrimeAlive, out var exoTwinsAlive);
		float exoPrimeLifeRatio = 5f;
		float exoTwinsLifeRatio = 5f;
		if (exoPrimeAlive)
		{
			exoPrimeLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].lifeMax;
		}
		if (exoTwinsAlive)
		{
			exoTwinsLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].lifeMax;
		}
		float totalOtherExoMechLifeRatio = exoPrimeLifeRatio + exoTwinsLifeRatio;
		bool exoPrimePassive = false;
		bool exoTwinsPassive = false;
		if (exoPrimeAlive)
		{
			exoPrimePassive = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].Calamity().newAI[1] == 1f;
		}
		if (exoTwinsAlive)
		{
			exoTwinsPassive = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[1] == 1f;
		}
		bool anyOtherExoMechPassive = exoPrimePassive | exoTwinsPassive;
		bool exoPrimeWasFirst = false;
		bool exoTwinsWereFirst = false;
		if (exoPrimeAlive)
		{
			exoPrimeWasFirst = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].ai[3] == 1f;
		}
		if (exoTwinsAlive)
		{
			exoTwinsWereFirst = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].ai[3] == 1f;
		}
		bool num = exoPrimeWasFirst | exoTwinsWereFirst;
		bool draedonAlive = false;
		if (CalamityGlobalNPC.draedon != -1 && Main.npc[CalamityGlobalNPC.draedon].active)
		{
			draedonAlive = true;
		}
		if (num && base.NPC.ai[3] < 1f)
		{
			base.NPC.ai[3] = 1f;
		}
		bool spawnOtherExoMechs = lifeRatio < 0.7f && base.NPC.ai[3] == 0f;
		bool berserk = lifeRatio < 0.4f || (otherExoMechsAlive == 0 && lifeRatio < 0.7f);
		bool lastMechAlive = berserk && otherExoMechsAlive == 0;
		vulnerable = false;
		bool otherMechIsBerserk = exoPrimeLifeRatio < 0.4f || exoTwinsLifeRatio < 0.4f;
		bool shouldGetBuffedByBerserkPhase = berserk && !otherMechIsBerserk;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (Main.netMode != 1 && !tailSpawned && base.NPC.ai[0] == 0f)
		{
			int Previous = base.NPC.whoAmI;
			for (int i = 0; i < 101; i++)
			{
				int lol = ((i < 0 || i >= 100) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<ThanatosTail>(), base.NPC.whoAmI) : ((i % 2 != 0) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<ThanatosBody2>(), base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<ThanatosBody1>(), base.NPC.whoAmI)));
				Main.npc[lol].realLife = base.NPC.whoAmI;
				Main.npc[lol].ai[2] = base.NPC.whoAmI;
				Main.npc[lol].ai[1] = Previous;
				Main.npc[Previous].ai[0] = lol;
				NetMessage.SendData(23, -1, -1, null, lol);
				Previous = lol;
			}
			tailSpawned = true;
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[0]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[0]].life;
		}
		float velocityAdjustTime = 20f;
		float speedUpTime = (lastMechAlive ? 180f : (shouldGetBuffedByBerserkPhase ? 220f : 300f));
		float slowDownTime = (lastMechAlive ? 30f : (shouldGetBuffedByBerserkPhase ? 40f : 50f));
		float chargePhaseGateValue = speedUpTime + slowDownTime;
		float laserBarrageDuration = (lastMechAlive ? 270f : (shouldGetBuffedByBerserkPhase ? 300f : 360f));
		bool targetDead = false;
		if (Main.player[targetIndex].dead)
		{
			targetDead = true;
			AIState = 0f;
			base.NPC.localAI[0] = 0f;
			base.NPC.localAI[2] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			calamityGlobalNPC.newAI[3] = 0f;
			chargeVelocityScalar = 0f;
			base.NPC.dontTakeDamage = true;
			base.NPC.velocity.Y--;
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				base.NPC.velocity.Y--;
			}
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				for (int a = 0; a < Main.maxNPCs; a++)
				{
					if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>() || Main.npc[a].type == ModContent.NPCType<AresBody>() || Main.npc[a].type == ModContent.NPCType<AresLaserCannon>() || Main.npc[a].type == ModContent.NPCType<AresPlasmaFlamethrower>() || Main.npc[a].type == ModContent.NPCType<AresTeslaCannon>() || Main.npc[a].type == ModContent.NPCType<AresGaussNuke>() || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody1>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody2>() || Main.npc[a].type == ModContent.NPCType<ThanatosTail>())
					{
						Main.npc[a].active = false;
					}
				}
			}
		}
		base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
		int direction = base.NPC.direction;
		base.NPC.direction = (base.NPC.spriteDirection = ((base.NPC.velocity.X > 0f) ? 1 : (-1)));
		if (direction != base.NPC.direction)
		{
			base.NPC.netUpdate = true;
		}
		Vector2 destination = Main.player[targetIndex].Center;
		bool speedUp = false;
		if (base.NPC.localAI[3] < 180f)
		{
			speedUp = true;
			destination += new Vector2(0f, 2400f);
		}
		float distanceFromTarget = Vector2.Distance(base.NPC.Center, destination);
		float increaseSpeedMult = 1f;
		float increaseSpeedGateValue = 600f;
		if (distanceFromTarget > increaseSpeedGateValue)
		{
			float distanceAmount = MathHelper.Clamp((distanceFromTarget - increaseSpeedGateValue) / (5600f - increaseSpeedGateValue), 0f, 1f);
			increaseSpeedMult = MathHelper.Lerp(1f, 3.5f, distanceAmount);
		}
		float turnDistance = 160f;
		float chargeLocationDistance = turnDistance * 0.2f;
		float laserBarrageLocationBaseDistance = ((SecondaryAIState == 2f) ? 1600f : 800f);
		Vector2 laserBarrageLocation = default(Vector2);
		((Vector2)(ref laserBarrageLocation))._002Ector(0f, (base.NPC.ai[1] % 2f == 0f) ? laserBarrageLocationBaseDistance : (0f - laserBarrageLocationBaseDistance));
		float laserBarrageLocationDistance = turnDistance * 3f;
		float baseVelocityMult = (shouldGetBuffedByBerserkPhase ? 0.15f : 0f) + (death ? 1.2f : (revenge ? 1.175f : (expertMode ? 1.15f : 1.1f)));
		float baseVelocity = 10f * baseVelocityMult;
		baseVelocity = ((!(targetDead | speedUp)) ? (baseVelocity * increaseSpeedMult) : (baseVelocity * 4f));
		if (Main.getGoodWorld)
		{
			baseVelocity *= 1.15f;
		}
		float turnSpeed = MathHelper.ToRadians(baseVelocity * 0.1f * (shouldGetBuffedByBerserkPhase ? 1.25f : 1.1f));
		float chargeVelocityMult = MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
		float chargeTurnSpeedMult = MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
		float laserBarragePhaseVelocityMult = MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
		float laserBarragePhaseTurnSpeedMult = MathHelper.Lerp(1f, 3f, chargeVelocityScalar);
		float deathrayVelocityMult = MathHelper.Lerp(0.5f, 3f, chargeVelocityScalar);
		float deathrayTurnSpeedMult = MathHelper.Lerp(0.5f, 3f, chargeVelocityScalar);
		float chargeVelocityScalarIncrement = 1f / speedUpTime;
		float chargeVelocityScalarDecrement = 1f / slowDownTime;
		float deathrayVelocityScalarIncrement = 1f / 180f;
		float laserBarrageVelocityScalarIncrement = (lastMechAlive ? 0.025f : (shouldGetBuffedByBerserkPhase ? 0.0225f : 0.02f));
		float laserBarrageVelocityScalarDecrement = 1f / velocityAdjustTime;
		switch ((int)SecondaryAIState)
		{
		case 0:
			if (otherExoMechsAlive == 0 && !exoMechdusa)
			{
				if (spawnOtherExoMechs)
				{
					if (base.NPC.ai[3] < 1f)
					{
						base.NPC.ai[3] = 1f;
					}
					SecondaryAIState = 2f;
					base.NPC.localAI[0] = 0f;
					base.NPC.localAI[2] = 0f;
					calamityGlobalNPC.newAI[2] = 0f;
					calamityGlobalNPC.newAI[3] = 0f;
					chargeVelocityScalar = 0f;
					base.NPC.TargetClosest();
					if (draedonAlive)
					{
						Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 1f;
						Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
					}
					if (Main.netMode != 1)
					{
						NPC.SpawnOnPlayer(Main.player[targetIndex].whoAmI, ModContent.NPCType<AresBody>());
						NPC.SpawnOnPlayer(Main.player[targetIndex].whoAmI, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>());
						NPC.SpawnOnPlayer(Main.player[targetIndex].whoAmI, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>());
					}
					if (lifeRatio < 0.4f)
					{
						berserkEarlyBugFix = true;
					}
				}
				break;
			}
			if ((anyOtherExoMechPassive || lifeRatio < 0.7f) && !berserk && totalOtherExoMechLifeRatio < 5f)
			{
				SecondaryAIState = 1f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityScalar = 0f;
				base.NPC.TargetClosest();
			}
			if (otherMechIsBerserk && !berserk && !exoMechdusa)
			{
				SecondaryAIState = 2f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityScalar = 0f;
				base.NPC.TargetClosest();
				if (draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 5f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			break;
		case 1:
			AIState = 1f;
			if (otherMechIsBerserk && !exoMechdusa)
			{
				SecondaryAIState = 2f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityScalar = 0f;
				base.NPC.TargetClosest();
			}
			if (berserk)
			{
				AIState = 0f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityScalar = 0f;
				base.NPC.TargetClosest();
				SecondaryAIState = 0f;
				if ((exoTwinsAlive & exoPrimeAlive) && draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 3f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			break;
		case 2:
			AIState = 1f;
			if ((exoPrimeLifeRatio < 0.7f || exoTwinsLifeRatio < 0.7f || berserkEarlyBugFix) && !otherMechIsBerserk)
			{
				SecondaryAIState = ((totalOtherExoMechLifeRatio > 5f) ? 0f : 1f);
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityScalar = 0f;
				base.NPC.TargetClosest();
				if ((exoPrimeAlive & exoTwinsAlive) && draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 2f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			if (berserk)
			{
				AIState = 0f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityScalar = 0f;
				base.NPC.TargetClosest();
				SecondaryAIState = 0f;
			}
			break;
		}
		bool invisiblePhase = SecondaryAIState == 2f;
		base.NPC.dontTakeDamage = invisiblePhase;
		if (!invisiblePhase)
		{
			if (noContactDamageTimer > 0)
			{
				noContactDamageTimer--;
			}
			base.NPC.Opacity += 0.2f;
			if (base.NPC.Opacity > 1f)
			{
				base.NPC.Opacity = 1f;
			}
		}
		else
		{
			noContactDamageTimer = 185;
			base.NPC.Opacity -= 0.05f;
			if (base.NPC.Opacity < 0f)
			{
				base.NPC.Opacity = 0f;
			}
		}
		Vector2 val;
		switch ((int)AIState)
		{
		case 0:
		{
			if (calamityGlobalNPC.newAI[3] == 0f)
			{
				chargeVelocityScalar += chargeVelocityScalarIncrement;
				if (chargeVelocityScalar >= 1f)
				{
					chargeVelocityScalar = 1f;
					calamityGlobalNPC.newAI[3] = 1f;
				}
			}
			else
			{
				chargeVelocityScalar -= chargeVelocityScalarDecrement;
				if (chargeVelocityScalar < 0f)
				{
					chargeVelocityScalar = 0f;
				}
			}
			baseVelocity *= chargeVelocityMult;
			turnSpeed *= chargeTurnSpeedMult;
			turnDistance = chargeLocationDistance;
			float turnSlowerDistanceGateValue = (lastMechAlive ? 160f : 240f);
			if (distanceFromTarget < turnSlowerDistanceGateValue)
			{
				turnSpeed *= distanceFromTarget / turnSlowerDistanceGateValue;
			}
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] >= chargePhaseGateValue)
			{
				AIState = ((base.NPC.localAI[0] == 1f) ? 2f : 1f);
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityScalar = 0f;
				base.NPC.TargetClosest();
			}
			break;
		}
		case 1:
			destination += laserBarrageLocation;
			turnDistance = laserBarrageLocationDistance;
			if (calamityGlobalNPC.newAI[3] == 0f)
			{
				chargeVelocityScalar += laserBarrageVelocityScalarIncrement;
				if (chargeVelocityScalar > 1f)
				{
					chargeVelocityScalar = 1f;
				}
			}
			baseVelocity *= laserBarragePhaseVelocityMult;
			turnSpeed *= laserBarragePhaseTurnSpeedMult;
			val = destination - base.NPC.Center;
			if (!(((Vector2)(ref val)).Length() < laserBarrageLocationDistance) && !(calamityGlobalNPC.newAI[2] > 0f))
			{
				break;
			}
			calamityGlobalNPC.newAI[2]++;
			if (SecondaryAIState != 1f && SecondaryAIState != 2f && calamityGlobalNPC.newAI[2] >= laserBarrageDuration)
			{
				chargeVelocityScalar -= laserBarrageVelocityScalarDecrement;
				if (chargeVelocityScalar < 0f)
				{
					chargeVelocityScalar = 0f;
				}
				calamityGlobalNPC.newAI[3]++;
				if (calamityGlobalNPC.newAI[3] >= velocityAdjustTime)
				{
					base.NPC.ai[1] += ((shouldGetBuffedByBerserkPhase & revenge) ? 1f : 0f);
					base.NPC.localAI[0] = (shouldGetBuffedByBerserkPhase ? 1f : 0f);
					AIState = 0f;
					calamityGlobalNPC.newAI[2] = 0f;
					calamityGlobalNPC.newAI[3] = 0f;
					chargeVelocityScalar = 0f;
					base.NPC.TargetClosest();
				}
			}
			break;
		case 2:
		{
			vulnerable = true;
			float slowDownDistance = GetSlowdownAreaEdgeRadius(lastMechAlive);
			if (distanceFromTarget < slowDownDistance && base.NPC.localAI[2] == 0f)
			{
				base.NPC.localAI[2] = 1f;
			}
			if (calamityGlobalNPC.newAI[3] == 0f)
			{
				chargeVelocityScalar += deathrayVelocityScalarIncrement;
				if (chargeVelocityScalar >= 1f)
				{
					chargeVelocityScalar = 1f;
					if (base.NPC.localAI[2] == 1f)
					{
						calamityGlobalNPC.newAI[3] = 1f;
					}
				}
			}
			else
			{
				chargeVelocityScalar -= deathrayVelocityScalarIncrement * 5f;
				if (chargeVelocityScalar < 0f)
				{
					chargeVelocityScalar = 0f;
				}
			}
			baseVelocity *= deathrayVelocityMult;
			turnSpeed *= deathrayTurnSpeedMult;
			turnDistance = chargeLocationDistance;
			if (!(base.NPC.localAI[2] >= 1f))
			{
				break;
			}
			float velocityScale = distanceFromTarget / slowDownDistance;
			if (velocityScale < 1f)
			{
				velocityScale *= velocityScale;
			}
			baseVelocity *= velocityScale;
			turnSpeed *= velocityScale;
			float pulseIncrement = 1f / 60f * ((distanceFromTarget < slowDownDistance * 0.5f) ? 0.75f : 1.5f);
			base.NPC.localAI[2] += pulseIncrement;
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] < 180f)
			{
				if (calamityGlobalNPC.newAI[2] == 1f)
				{
					LaserSoundSlot = SoundEngine.PlaySound(Main.zenithWorld ? GFBeam : LaserSound, base.NPC.Center);
					ExoMechsSky.CreateLightningBolt(12);
					if (Main.netMode != 1)
					{
						int type = ModContent.ProjectileType<ThanatosBeamTelegraph>();
						for (int b = 0; b < 6; b++)
						{
							int beam = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type, 0, 0f, 255, base.NPC.whoAmI);
							if (Main.projectile.IndexInRange(beam))
							{
								float squishedRatio = (float)Math.Pow((float)Math.Sin((float)Math.PI * (float)b / 6f), 2.0);
								float smoothenedRatio = MathHelper.SmoothStep(0f, 1f, squishedRatio);
								Main.projectile[beam].ai[0] = base.NPC.whoAmI;
								Main.projectile[beam].ai[1] = MathHelper.Lerp(-0.74f, 0.74f, smoothenedRatio);
							}
						}
						int beam2 = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type, 0, 0f, 255, base.NPC.whoAmI);
						if (Main.projectile.IndexInRange(beam2))
						{
							Main.projectile[beam2].ai[0] = base.NPC.whoAmI;
						}
					}
				}
			}
			else if (calamityGlobalNPC.newAI[2] == 180f && Main.netMode != 1)
			{
				int type2 = ModContent.ProjectileType<ThanatosBeamStart>();
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type2, BeamDamage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
			}
			if (calamityGlobalNPC.newAI[2] >= 360f)
			{
				if (Main.zenithWorld && !exoMechdusa)
				{
					AIState = 2f;
				}
				else
				{
					AIState = 0f;
				}
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityScalar = 0f;
				base.NPC.TargetClosest();
			}
			break;
		}
		}
		if (base.NPC.localAI[3] == 0f)
		{
			noContactDamageTimer = 300;
		}
		if (base.NPC.localAI[3] < 600f)
		{
			base.NPC.localAI[3]++;
		}
		base.NPC.chaseable = vulnerable;
		base.NPC.Calamity().DR = (vulnerable ? 0.1f : 0.9999f);
		base.NPC.Calamity().unbreakableDR = !vulnerable;
		SmokeDrawer.ParticleSpawnRate = 9999999;
		if (vulnerable)
		{
			if (base.NPC.localAI[1] == 0f)
			{
				SoundEngine.PlaySound(in VentSound, base.NPC.Center);
			}
			base.NPC.localAI[1]++;
			if (base.NPC.localAI[1] < 180f)
			{
				SmokeDrawer.BaseMoveRotation = base.NPC.rotation - (float)Math.PI / 2f;
				SmokeDrawer.ParticleSpawnRate = 10;
			}
		}
		else
		{
			base.NPC.localAI[1] = 0f;
		}
		SmokeDrawer.Update();
		if (!targetDead)
		{
			if (base.NPC.velocity == Vector2.Zero)
			{
				base.NPC.velocity = Vector2.Normalize(Main.player[targetIndex].Center - base.NPC.Center).SafeNormalize(Vector2.Zero) * baseVelocity;
			}
			val = destination - base.NPC.Center;
			if (!(((Vector2)(ref val)).Length() < turnDistance))
			{
				float targetAngle = base.NPC.AngleTo(destination);
				float f = base.NPC.velocity.ToRotation().AngleTowards(targetAngle, turnSpeed);
				base.NPC.velocity = f.ToRotationVector2() * baseVelocity;
			}
		}
		if (((Vector2)(ref base.NPC.velocity)).Length() > baseVelocity)
		{
			base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.Zero) * baseVelocity;
		}
		if (SoundEngine.TryGetActiveSound(LaserSoundSlot, out ActiveSound laserSound) && laserSound.IsPlaying)
		{
			laserSound.Position = base.NPC.Center;
		}
		if (exoMechdusa && CalamityGlobalNPC.draedonExoMechPrime != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrime].ModNPC<AresBody>().exoMechdusa)
		{
			base.NPC.rotation = 0f;
			NPC aresin = Main.npc[CalamityGlobalNPC.draedonExoMechPrime];
			if (base.NPC.Calamity().newAI[0] != 2f)
			{
				Vector2 pos = default(Vector2);
				((Vector2)(ref pos))._002Ector(aresin.Center.X - 80f, aresin.Center.Y - 89f);
				base.NPC.position = pos;
				base.NPC.Calamity().newAI[2]++;
			}
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		cooldownSlot = 1;
		Rectangle targetHitbox = target.Hitbox;
		float num = Vector2.Distance(base.NPC.Center, targetHitbox.TopLeft());
		float hitboxTopRight = Vector2.Distance(base.NPC.Center, targetHitbox.TopRight());
		float hitboxBotLeft = Vector2.Distance(base.NPC.Center, targetHitbox.BottomLeft());
		float hitboxBotRight = Vector2.Distance(base.NPC.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (hitboxTopRight < minDist)
		{
			minDist = hitboxTopRight;
		}
		if (hitboxBotLeft < minDist)
		{
			minDist = hitboxBotLeft;
		}
		if (hitboxBotRight < minDist)
		{
			minDist = hitboxBotRight;
		}
		if (minDist <= 50f && base.NPC.Opacity == 1f)
		{
			return noContactDamageTimer <= 0;
		}
		return false;
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		if (base.NPC.localAI[3] < 600f)
		{
			modifiers.SourceDamage *= 0.01f;
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 1.5f;
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (AIState == 0f || AIState == 1f)
		{
			if (base.NPC.frameCounter >= 6.0)
			{
				base.NPC.frame.Y -= frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y < 0)
			{
				base.NPC.frame.Y = 0;
			}
			return;
		}
		if (base.NPC.frameCounter >= 6.0)
		{
			base.NPC.frame.Y += frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		int finalFrame = Main.npcFrameCount[base.Type] - 1;
		if (base.NPC.frame.Y >= frameHeight * finalFrame)
		{
			base.NPC.frame.Y = frameHeight * finalFrame;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_0925: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frame = texture.Frame();
			float offset = -0.2f;
			float startX = 60f;
			float startY = 70f;
			int segmentSpacing = 40;
			int animationSpeed = 5;
			float wormTimer = base.NPC.Calamity().bestiaryWormTimer;
			for (int i = 3; i > 0; i--)
			{
				float bodyOffset = ((i == 1) ? ((float)(i * segmentSpacing) * 0.4f) : ((float)(i * segmentSpacing) - (float)segmentSpacing * 0.5f));
				Texture2D toUse = ((i % 2 == 1) ? TextureAssets.Npc[ModContent.NPCType<ThanatosBody1>()].Value : TextureAssets.Npc[ModContent.NPCType<ThanatosBody2>()].Value);
				spriteBatch.Draw(toUse, base.NPC.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * (float)i) * (float)animationSpeed) * 2f + startY), (Rectangle?)toUse.Frame(1, 5), base.NPC.GetAlpha(drawColor), base.NPC.rotation - (float)Math.PI / 2f - MathF.Cos((wormTimer + offset * (float)i) * (float)animationSpeed) * ((float)Math.PI / 4f) * 0.075f, new Vector2((float)(toUse.Width / 2), (float)(toUse.Width / 10)), base.NPC.scale, (SpriteEffects)0, 0f);
			}
			spriteBatch.Draw(texture, base.NPC.position + new Vector2(startX + 24f, MathF.Sin(wormTimer * (float)animationSpeed) * 2f + startY), (Rectangle?)texture.Frame(1, 5), base.NPC.GetAlpha(drawColor), base.NPC.rotation - (float)Math.PI / 2f - MathF.Cos(wormTimer * (float)animationSpeed) * ((float)Math.PI / 4f) * 0.075f, new Vector2((float)texture.Width * 0.5f, (float)(texture.Height / 5)), base.NPC.scale, (SpriteEffects)0, 0f);
			return false;
		}
		Vector2 center = base.NPC.Center - screenPos;
		center -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		center += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, vector, base.NPC.scale, (SpriteEffects)0, 0f);
		texture = GlowTexture.Value;
		Color glowmaskColor = (Color)((!vulnerable) ? Color.White : ((AIState == 2f && SecondaryAIState != 2f) ? Color.Lerp(new Color(255, 64, 64), Color.CornflowerBlue, base.NPC.Calamity().newAI[2] / 180f) : new Color(255, 64, 64)));
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, glowmaskColor * base.NPC.Opacity, base.NPC.rotation, vector, base.NPC.scale, (SpriteEffects)0, 0f);
		SmokeDrawer.DrawSet(base.NPC.Center);
		if (AIState == 2f && SecondaryAIState != 2f)
		{
			spriteBatch.SetBlendState(BlendState.Additive);
			Texture2D auraTexture = AuraTexture.Value;
			int completelyUseless = 0;
			int otherExoMechsAlive = CheckForOtherMechs(ref completelyUseless, out var _, out var _);
			float auraGeneralPower = Utils.GetLerpValue(0f, 59.940002f, base.NPC.Calamity().newAI[2], clamped: true);
			auraGeneralPower *= Utils.GetLerpValue(360f, 360f, base.NPC.Calamity().newAI[2], clamped: true);
			float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
			bool lastMechAlive = (lifeRatio < 0.4f || (otherExoMechsAlive == 0 && lifeRatio < 0.7f)) && otherExoMechsAlive == 0;
			bool num = Vector2.Distance(base.NPC.Center, Main.player[base.NPC.target].Center) < GetSlowdownAreaEdgeRadius(lastMechAlive) * 0.5f;
			float pulse = base.NPC.localAI[2] % 1f;
			float auraRadius = GetSlowdownAreaEdgeRadius(lastMechAlive) * auraGeneralPower * 1.25f;
			Vector2 outerAuraScale = Vector2.One * auraRadius / auraTexture.Size();
			Vector2 innerAuraScale = outerAuraScale * (1f - pulse) * 1.2f;
			Color outerAuraColor = Color.White * auraGeneralPower * 0.65f;
			Color innerAuraColor = (num ? outerAuraColor : (Color.Red * auraGeneralPower * 0.65f)) * (float)Math.Sqrt(pulse);
			spriteBatch.Draw(auraTexture, center, (Rectangle?)null, outerAuraColor, 0f, auraTexture.Size() * 0.5f, outerAuraScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(auraTexture, center, (Rectangle?)null, innerAuraColor, 0f, auraTexture.Size() * 0.5f, innerAuraScale, (SpriteEffects)0, 0f);
			spriteBatch.SetBlendState(BlendState.AlphaBlend);
			if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
			{
				return false;
			}
			Player target = Main.player[base.NPC.target];
			Texture2D leftReticleTexture = ReticleLeftTexture.Value;
			Texture2D rightReticleTexture = ReticleRightTexture.Value;
			Texture2D topReticleTexture = ReticleTopTexture.Value;
			Texture2D bottomReticleTexture = ReticleBottomTexture.Value;
			Texture2D leftReticleProngTexture = ReticleProngLeftTexture.Value;
			Texture2D rightReticleProngTexture = ReticleProngRightTexture.Value;
			float targetHeadDistance = base.NPC.Distance(target.Center);
			float reticleOpacity = Utils.GetLerpValue(auraRadius * 0.5f - 40f, auraRadius * 0.5f + 100f, targetHeadDistance, clamped: true);
			float reticleOffsetDistance = MathHelper.SmoothStep(300f, 0f, reticleOpacity);
			float reticleFadeToWhite = ((float)Math.Cos(Main.GlobalTimeWrappedHourly * 6.8f) * 0.5f + 0.5f) * reticleOpacity * 0.67f;
			Color reticleBaseColor = new Color(255, 0, 0, 127) * reticleOpacity;
			Color reticleFlashBaseColor = Color.Lerp(reticleBaseColor, new Color(255, 255, 255, 0), reticleFadeToWhite) * reticleOpacity;
			Vector2 origin = leftReticleTexture.Size() * 0.5f;
			Vector2 playerDrawPosition = target.Center - screenPos;
			spriteBatch.Draw(leftReticleTexture, playerDrawPosition - Vector2.UnitX * reticleOffsetDistance, (Rectangle?)null, reticleBaseColor, 0f, origin, 1f, (SpriteEffects)0, 0f);
			spriteBatch.Draw(rightReticleTexture, playerDrawPosition + Vector2.UnitX * reticleOffsetDistance, (Rectangle?)null, reticleBaseColor, 0f, origin, 1f, (SpriteEffects)0, 0f);
			for (int j = 0; j < 3; j++)
			{
				float scale = 1f + (float)j * 0.125f;
				spriteBatch.Draw(leftReticleProngTexture, playerDrawPosition - Vector2.UnitX * reticleOffsetDistance, (Rectangle?)null, reticleFlashBaseColor, 0f, origin, scale, (SpriteEffects)0, 0f);
				spriteBatch.Draw(rightReticleProngTexture, playerDrawPosition + Vector2.UnitX * reticleOffsetDistance, (Rectangle?)null, reticleFlashBaseColor, 0f, origin, scale, (SpriteEffects)0, 0f);
				spriteBatch.Draw(bottomReticleTexture, playerDrawPosition + Vector2.UnitY * reticleOffsetDistance, (Rectangle?)null, reticleFlashBaseColor, 0f, origin, scale, (SpriteEffects)0, 0f);
			}
			spriteBatch.Draw(topReticleTexture, playerDrawPosition - Vector2.UnitY * reticleOffsetDistance, (Rectangle?)null, reticleBaseColor, 0f, origin, 1f, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<OmegaHealingPotion>();
	}

	public override bool SpecialOnKill()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<ThanatosHead>(), ModContent.NPCType<ThanatosBody1>(), ModContent.NPCType<ThanatosBody2>(), ModContent.NPCType<ThanatosTail>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public override void OnKill()
	{
		bool exoTwinsAlive = false;
		bool exoPrimeAlive = false;
		if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
		{
			exoTwinsAlive = true;
		}
		if (CalamityGlobalNPC.draedonExoMechPrime != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrime].active)
		{
			exoPrimeAlive = true;
		}
		bool draedonAlive = false;
		if (CalamityGlobalNPC.draedon != -1 && Main.npc[CalamityGlobalNPC.draedon].active)
		{
			draedonAlive = true;
		}
		if (exoTwinsAlive & exoPrimeAlive)
		{
			if (draedonAlive)
			{
				Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 4f;
				Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
			}
		}
		else if (exoTwinsAlive | exoPrimeAlive)
		{
			if (draedonAlive)
			{
				Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 6f;
				Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
			}
		}
		else
		{
			AresBody.DoMiscDeathEffects(base.NPC, AresBody.MechType.Thanatos);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		AresBody.DefineExoMechLoot(base.NPC, npcLoot, 1);
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (exoMechdusa)
		{
			typeName = this.GetLocalizedValue("HekateName");
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			if (vulnerable)
			{
				base.NPC.soundDelay = 8;
				SoundEngine.PlaySound(in ThanatosHitSoundOpen, base.NPC.Center);
			}
			else
			{
				base.NPC.soundDelay = 3;
				SoundEngine.PlaySound(in ThanatosHitSoundClosed, base.NPC.Center);
			}
		}
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255));
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			}
			for (int j = 0; j < 20; j++)
			{
				int plasmaDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
				Main.dust[plasmaDust].noGravity = true;
				Dust obj = Main.dust[plasmaDust];
				obj.velocity *= 3f;
				plasmaDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
				Dust obj2 = Main.dust[plasmaDust];
				obj2.velocity *= 2f;
				Main.dust[plasmaDust].noGravity = true;
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ThanatosHead").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ThanatosHead2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ThanatosHead3").Type);
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
