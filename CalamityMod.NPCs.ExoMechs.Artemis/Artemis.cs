using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Potions;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Thanatos;
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
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ExoMechs.Artemis;

public class Artemis : ModNPC
{
	public enum Phase
	{
		Normal,
		Charge,
		LaserShotgun,
		Deathray,
		PhaseTransition
	}

	public enum SecondaryPhase
	{
		Nothing,
		Passive,
		PassiveAndImmune
	}

	public static int phase1IconIndex;

	public static int phase2IconIndex;

	public static readonly SoundStyle AttackSelectionSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ApolloArtemisTargetSelection")
	{
		Volume = 1.3f
	};

	public static readonly SoundStyle ChargeSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ArtemisApolloDash")
	{
		Volume = 1.2f
	};

	public static readonly SoundStyle ChargeTelegraphSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ArtemisApolloDashTelegraph")
	{
		Volume = 1.2f
	};

	public static readonly SoundStyle LensSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ExoTwinsEject")
	{
		Volume = 1.2f
	};

	public static readonly SoundStyle LaserShotgunSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ArtemisShotgunLaser")
	{
		Volume = 1.2f
	};

	public static readonly SoundStyle SpinLaserbeamSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ArtemisSpinLaserbeam")
	{
		Volume = 1.3f
	};

	public static Asset<Texture2D> GlowTexture;

	private float velocityBoostMult;

	private Vector2 chargeVelocityNormalized;

	private const int maxFramesX = 10;

	private const int maxFramesY = 9;

	private int frameX;

	private int frameY;

	private const int normalFrameLimit_Phase1 = 9;

	private const int chargeUpFrameLimit_Phase1 = 19;

	private const int attackFrameLimit_Phase1 = 29;

	private const int phaseTransitionFrameLimit = 59;

	private const int normalFrameLimit_Phase2 = 69;

	private const int chargeUpFrameLimit_Phase2 = 79;

	private const int attackFrameLimit_Phase2 = 89;

	private const float defaultLifeRatio = 5f;

	private const float soundDistance = 2800f;

	private const float defaultAnimationDuration = 60f;

	private const float phaseTransitionDuration = 180f;

	private const float lensPopTime = 48f;

	private const float deathrayTelegraphDuration = 60f;

	private const float PauseDurationBeforeLaserActuallyFires = 20f;

	private Vector2 pointToLookAt;

	private const float deathrayDuration = 180f;

	private bool pickNewLocation;

	public bool exoMechdusa;

	private int rotationDirection;

	private Vector2 spinningPoint;

	private Vector2 spinVelocity;

	public float ChargeFlash;

	private SlotId DeathraySoundSlot;

	public static string NameToDisplay = "XS-01 Artemis";

	public static int LaserDamage = 80;

	public static int BeamDamage = 105;

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
		string phase1IconPath = "CalamityMod/NPCs/ExoMechs/Artemis/ArtemisHead";
		string phase2IconPath = "CalamityMod/NPCs/ExoMechs/Artemis/ArtemisPhase2Head";
		phase1IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase1IconPath);
		phase2IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase2IconPath);
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.TrailingMode[base.Type] = 3;
		NPCID.Sets.TrailCacheLength[base.Type] = 15;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = -50f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -40f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		nPCBestiaryDrawModifiers.Scale = 0.45f;
		nPCBestiaryDrawModifiers.Rotation = (float)Math.PI * 3f / 4f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 190;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 204;
		base.NPC.height = 226;
		base.NPC.defense = 100;
		base.NPC.DR_NERD(0.25f);
		base.NPC.LifeMaxNERB(1000000, 1495000, 650000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.Opacity = 0f;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.DeathSound = CommonCalamitySounds.ExoDeathSound;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<ExoMechsBossBar>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Artemis")
		});
	}

	public override void BossHeadSlot(ref int index)
	{
		if (SecondaryAIState == 2f)
		{
			index = -1;
		}
		else if ((float)base.NPC.life / (float)base.NPC.lifeMax < 0.6f)
		{
			index = phase2IconIndex;
		}
		else
		{
			index = phase1IconIndex;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(velocityBoostMult);
		writer.WriteVector2(pointToLookAt);
		writer.WriteVector2(spinVelocity);
		writer.WriteVector2(chargeVelocityNormalized);
		writer.Write(frameX);
		writer.Write(frameY);
		writer.Write(pickNewLocation);
		writer.Write(exoMechdusa);
		writer.Write(rotationDirection);
		writer.WriteVector2(spinningPoint);
		writer.Write(base.NPC.dontTakeDamage);
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		velocityBoostMult = reader.ReadSingle();
		pointToLookAt = reader.ReadVector2();
		spinVelocity = reader.ReadVector2();
		chargeVelocityNormalized = reader.ReadVector2();
		frameX = reader.ReadInt32();
		frameY = reader.ReadInt32();
		pickNewLocation = reader.ReadBoolean();
		exoMechdusa = reader.ReadBoolean();
		rotationDirection = reader.ReadInt32();
		spinningPoint = reader.ReadVector2();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ced: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1746: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19db: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2155: Unknown result type (might be due to invalid IL or missing references)
		//IL_1179: Unknown result type (might be due to invalid IL or missing references)
		//IL_1180: Unknown result type (might be due to invalid IL or missing references)
		//IL_1186: Unknown result type (might be due to invalid IL or missing references)
		//IL_1159: Unknown result type (might be due to invalid IL or missing references)
		//IL_1167: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2181: Unknown result type (might be due to invalid IL or missing references)
		//IL_218c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2192: Unknown result type (might be due to invalid IL or missing references)
		//IL_2194: Unknown result type (might be due to invalid IL or missing references)
		//IL_2199: Unknown result type (might be due to invalid IL or missing references)
		//IL_219b: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_11af: Unknown result type (might be due to invalid IL or missing references)
		//IL_1190: Unknown result type (might be due to invalid IL or missing references)
		//IL_1192: Unknown result type (might be due to invalid IL or missing references)
		//IL_1197: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f35: Unknown result type (might be due to invalid IL or missing references)
		//IL_2276: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_22c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a62: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a86: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a92: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1abb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ace: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ada: Unknown result type (might be due to invalid IL or missing references)
		//IL_1adf: Unknown result type (might be due to invalid IL or missing references)
		//IL_179b: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_17bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b68: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1980: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d71: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d83: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db8: Unknown result type (might be due to invalid IL or missing references)
		//IL_134d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2357: Unknown result type (might be due to invalid IL or missing references)
		//IL_236b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2384: Unknown result type (might be due to invalid IL or missing references)
		//IL_2386: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1edd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1382: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1827: Unknown result type (might be due to invalid IL or missing references)
		//IL_1834: Unknown result type (might be due to invalid IL or missing references)
		//IL_1839: Unknown result type (might be due to invalid IL or missing references)
		//IL_183b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1291: Unknown result type (might be due to invalid IL or missing references)
		//IL_1293: Unknown result type (might be due to invalid IL or missing references)
		//IL_1298: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_127f: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_130d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_1314: Unknown result type (might be due to invalid IL or missing references)
		//IL_1319: Unknown result type (might be due to invalid IL or missing references)
		//IL_1875: Unknown result type (might be due to invalid IL or missing references)
		//IL_187a: Unknown result type (might be due to invalid IL or missing references)
		//IL_187c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1881: Unknown result type (might be due to invalid IL or missing references)
		//IL_1411: Unknown result type (might be due to invalid IL or missing references)
		//IL_1413: Unknown result type (might be due to invalid IL or missing references)
		//IL_1418: Unknown result type (might be due to invalid IL or missing references)
		//IL_141a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1421: Unknown result type (might be due to invalid IL or missing references)
		//IL_1426: Unknown result type (might be due to invalid IL or missing references)
		//IL_143a: Unknown result type (might be due to invalid IL or missing references)
		//IL_143f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1441: Unknown result type (might be due to invalid IL or missing references)
		//IL_1446: Unknown result type (might be due to invalid IL or missing references)
		//IL_1653: Unknown result type (might be due to invalid IL or missing references)
		//IL_165e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1676: Unknown result type (might be due to invalid IL or missing references)
		//IL_167d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1682: Unknown result type (might be due to invalid IL or missing references)
		//IL_168d: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_188e: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18da: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1504: Unknown result type (might be due to invalid IL or missing references)
		//IL_153d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1555: Unknown result type (might be due to invalid IL or missing references)
		//IL_155b: Unknown result type (might be due to invalid IL or missing references)
		//IL_155d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1562: Unknown result type (might be due to invalid IL or missing references)
		//IL_1567: Unknown result type (might be due to invalid IL or missing references)
		//IL_1569: Unknown result type (might be due to invalid IL or missing references)
		//IL_1570: Unknown result type (might be due to invalid IL or missing references)
		//IL_1575: Unknown result type (might be due to invalid IL or missing references)
		//IL_157d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1582: Unknown result type (might be due to invalid IL or missing references)
		//IL_1584: Unknown result type (might be due to invalid IL or missing references)
		//IL_1589: Unknown result type (might be due to invalid IL or missing references)
		//IL_1597: Unknown result type (might be due to invalid IL or missing references)
		//IL_1599: Unknown result type (might be due to invalid IL or missing references)
		//IL_159b: Unknown result type (might be due to invalid IL or missing references)
		//IL_159f: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a4: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.draedonExoMechTwinRed = base.NPC.whoAmI;
		base.NPC.frame = new Rectangle(base.NPC.width * frameX, base.NPC.height * frameY, base.NPC.width, base.NPC.height);
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		int targetIndex = base.NPC.target;
		int otherExoMechsAlive = 0;
		bool exoTwinGreenAlive = false;
		bool exoWormAlive = false;
		bool exoPrimeAlive = false;
		bool apolloUsingChargeCombo = false;
		if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
		{
			targetIndex = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].target;
			if (base.NPC.life > Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].life)
			{
				base.NPC.life = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].life;
			}
			exoTwinGreenAlive = true;
			apolloUsingChargeCombo = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[0] == 2f || Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[0] == 3f;
		}
		if (CalamityGlobalNPC.draedonExoMechWorm != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechWorm].active)
		{
			targetIndex = Main.npc[CalamityGlobalNPC.draedonExoMechWorm].target;
			otherExoMechsAlive++;
			exoWormAlive = true;
		}
		if (CalamityGlobalNPC.draedonExoMechPrime != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrime].active)
		{
			targetIndex = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].target;
			otherExoMechsAlive++;
			exoPrimeAlive = true;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float exoWormLifeRatio = 5f;
		float exoPrimeLifeRatio = 5f;
		if (exoWormAlive)
		{
			exoWormLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechWorm].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechWorm].lifeMax;
		}
		if (exoPrimeAlive)
		{
			exoPrimeLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].lifeMax;
		}
		bool exoWormPassive = false;
		bool exoPrimePassive = false;
		if (exoWormAlive)
		{
			exoWormPassive = Main.npc[CalamityGlobalNPC.draedonExoMechWorm].Calamity().newAI[1] == 1f;
		}
		if (exoPrimeAlive)
		{
			exoPrimePassive = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].Calamity().newAI[1] == 1f;
		}
		bool nerfedAttacks = false;
		if (exoWormAlive && !nerfedAttacks)
		{
			nerfedAttacks = Main.npc[CalamityGlobalNPC.draedonExoMechWorm].Calamity().newAI[1] != 2f;
		}
		if (exoPrimeAlive && !nerfedAttacks)
		{
			nerfedAttacks = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].Calamity().newAI[1] != 2f;
		}
		bool nerfedLaserShotgun = false;
		bool canFireLasers = true;
		if (exoTwinGreenAlive)
		{
			nerfedLaserShotgun = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[0] == 2f || Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[0] == 3f;
			canFireLasers = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].ai[3] <= 1f;
			if (base.NPC.ai[0] >= 10f)
			{
				base.NPC.ai[0] = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].ai[0];
			}
		}
		bool exoWormWasFirst = false;
		bool exoPrimeWasFirst = false;
		if (exoWormAlive)
		{
			exoWormWasFirst = Main.npc[CalamityGlobalNPC.draedonExoMechWorm].ai[3] == 1f;
		}
		if (exoPrimeAlive)
		{
			exoPrimeWasFirst = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].ai[3] == 1f;
		}
		if ((exoWormWasFirst | exoPrimeWasFirst) && base.NPC.ai[3] < 1f)
		{
			base.NPC.ai[3] = 1f;
		}
		bool phase2 = lifeRatio < 0.6f;
		if (lifeRatio < 0.7f)
		{
			_ = base.NPC.ai[3] == 0f;
		}
		else
			_ = 0;
		int num;
		int num2;
		if (!(lifeRatio < 0.4f))
		{
			if (otherExoMechsAlive == 0)
			{
				num = ((lifeRatio < 0.7f) ? 1 : 0);
				if (num != 0)
				{
					goto IL_04dc;
				}
			}
			else
			{
				num = 0;
			}
			num2 = 0;
			goto IL_04e4;
		}
		num = 1;
		goto IL_04dc;
		IL_04e4:
		bool lastMechAlive = (byte)num2 != 0;
		bool otherMechIsBerserk = exoWormLifeRatio < 0.4f || exoPrimeLifeRatio < 0.4f;
		bool shouldGetBuffedByBerserkPhase = num != 0 && !otherMechIsBerserk;
		if (base.NPC.ai[0] < 10f)
		{
			base.NPC.ai[0]++;
			if (base.NPC.ai[0] == 10f && !NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>()))
			{
				NPC.SpawnOnPlayer(Main.player[targetIndex].whoAmI, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>());
			}
		}
		else if (!NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>()))
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
		}
		float reducedTimeForGateValue = (death ? 32f : (revenge ? 24f : (expertMode ? 16f : 0f)));
		float reducedTimeForGateValue_Berserk = reducedTimeForGateValue * 0.5f;
		float normalAttackTime = 360f - reducedTimeForGateValue;
		float berserkAttackTime = (lastMechAlive ? (225f - reducedTimeForGateValue_Berserk) : (270f - reducedTimeForGateValue_Berserk));
		float attackPhaseGateValue = (shouldGetBuffedByBerserkPhase ? berserkAttackTime : normalAttackTime);
		float timeToLineUpAttack = (phase2 ? 30f : 45f);
		if (Main.getGoodWorld)
		{
			timeToLineUpAttack *= 0.5f;
		}
		float spinRadius = 500f;
		float spinLocationDistance = 50f;
		Vector2 spinLocation = Main.player[targetIndex].Center;
		switch ((int)base.NPC.ai[3])
		{
		case 0:
		case 1:
			spinLocation.Y -= spinRadius;
			break;
		case 2:
			spinLocation.Y += spinRadius;
			break;
		case 3:
			spinRadius *= 1.7f;
			spinLocation.X -= spinRadius;
			break;
		case 4:
			spinRadius *= 1.7f;
			spinLocation.X += spinRadius;
			break;
		}
		float movementDistanceGateValue = 100f;
		float chargeVelocity = (nerfedAttacks ? 60f : (death ? 74f : (revenge ? 70.5f : (expertMode ? 67f : 60f))));
		if (Main.getGoodWorld)
		{
			chargeVelocity *= 1.15f;
		}
		float chargeDistance = 2000f;
		float chargeDuration = chargeDistance / chargeVelocity;
		bool num3 = calamityGlobalNPC.newAI[3] >= attackPhaseGateValue - 30f + 2f && !phase2 && AIState == 0f;
		bool lineUpAttack = calamityGlobalNPC.newAI[3] >= attackPhaseGateValue + 2f;
		bool doBigAttack = calamityGlobalNPC.newAI[3] >= attackPhaseGateValue + 2f + timeToLineUpAttack;
		float predictionAmt = 20f;
		if (num3)
		{
			predictionAmt *= 2f;
		}
		if (AIState == 2f)
		{
			predictionAmt *= 1.5f;
		}
		if (nerfedAttacks)
		{
			predictionAmt *= 0.5f;
		}
		if (SecondaryAIState == 1f)
		{
			predictionAmt *= 0.5f;
		}
		float baseVelocityMult = (shouldGetBuffedByBerserkPhase ? 0.25f : 0f) + (death ? 1.1f : (revenge ? 1.075f : (expertMode ? 1.05f : 1f)));
		float baseVelocity = ((((AIState == 3f) | lineUpAttack) || AIState == 2f) ? 40f : 20f) * baseVelocityMult;
		float decelerationVelocityMult = 0.85f;
		if (Main.getGoodWorld)
		{
			baseVelocity *= 1.5f;
		}
		float laserShotgunDuration = (lastMechAlive ? 120f : 90f);
		if (pickNewLocation)
		{
			pickNewLocation = false;
			int randomLocationVarianceX = (shouldGetBuffedByBerserkPhase ? 50 : 20);
			int randomLocationVarianceY = (shouldGetBuffedByBerserkPhase ? 250 : 100);
			if (Main.getGoodWorld)
			{
				randomLocationVarianceX *= 2;
				randomLocationVarianceY *= 2;
			}
			base.NPC.localAI[0] = Main.rand.Next(-randomLocationVarianceX, randomLocationVarianceX + 1);
			base.NPC.localAI[1] = Main.rand.Next(-randomLocationVarianceY, randomLocationVarianceY + 1);
			if (SecondaryAIState == 1f)
			{
				base.NPC.localAI[0] *= 0.5f;
				base.NPC.localAI[1] *= 0.5f;
			}
			base.NPC.netUpdate = true;
		}
		float destinationX = ((base.NPC.ai[0] % 2f == 0f || base.NPC.ai[0] < 10f || !revenge) ? (-750f) : 750f);
		float destinationY = Main.player[targetIndex].Center.Y;
		Vector2 destination = (Vector2)((SecondaryAIState == 2f) ? new Vector2(Main.player[targetIndex].Center.X + destinationX * 1.6f, destinationY) : ((SecondaryAIState == 1f) ? new Vector2(Main.player[targetIndex].Center.X + destinationX, destinationY + 360f) : ((AIState == 3f) ? spinLocation : new Vector2(Main.player[targetIndex].Center.X + destinationX, destinationY))));
		if (AIState == 0f || AIState == 2f || AIState == 4f)
		{
			destination.X += base.NPC.localAI[0];
			destination.Y += base.NPC.localAI[1];
		}
		Vector2 distanceFromDestination = destination - base.NPC.Center;
		if (((Vector2)(ref distanceFromDestination)).Length() > movementDistanceGateValue && AIState != 1f)
		{
			if (velocityBoostMult < 1f)
			{
				velocityBoostMult += 0.004f;
			}
		}
		else if (velocityBoostMult > 0f)
		{
			velocityBoostMult -= 0.004f;
		}
		baseVelocity *= 1f + velocityBoostMult;
		bool canFire = (((Vector2)(ref distanceFromDestination)).Length() <= 320f) & canFireLasers;
		Vector2 predictionVector = ((AIState == 3f) ? Vector2.Zero : (Main.player[targetIndex].velocity * predictionAmt));
		Vector2 aimedVector = Main.player[targetIndex].Center + predictionVector - base.NPC.Center;
		float rateOfRotation = 0.1f;
		Vector2 rotateTowards = Main.player[targetIndex].Center - base.NPC.Center;
		bool stopRotatingAndSlowDown = (!phase2 && AIState == 0f) & lineUpAttack;
		Vector2 val2;
		if (!stopRotatingAndSlowDown)
		{
			if (AIState == 1f)
			{
				rateOfRotation = 0f;
				base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
			}
			else
			{
				Vector2 val = spinningPoint;
				val2 = default(Vector2);
				if (val != val2)
				{
					rateOfRotation = 0f;
					float x = spinningPoint.X - base.NPC.Center.X;
					float y = spinningPoint.Y - base.NPC.Center.Y;
					base.NPC.rotation = (float)Math.Atan2(y, x) + (float)Math.PI / 2f;
				}
				else
				{
					Vector2 val3 = pointToLookAt;
					val2 = default(Vector2);
					if (val3 != val2)
					{
						rateOfRotation = 0f;
						float x2 = pointToLookAt.X - base.NPC.Center.X;
						float y2 = pointToLookAt.Y - base.NPC.Center.Y;
						base.NPC.rotation = (float)Math.Atan2(y2, x2) + (float)Math.PI / 2f;
					}
					else
					{
						float num4 = Main.player[targetIndex].Center.X + predictionVector.X - base.NPC.Center.X;
						float y3 = Main.player[targetIndex].Center.Y + predictionVector.Y - base.NPC.Center.Y;
						rotateTowards = Vector2.Normalize(new Vector2(num4, y3)) * baseVelocity;
					}
				}
			}
			if (rateOfRotation != 0f)
			{
				base.NPC.rotation = base.NPC.rotation.AngleTowards((float)Math.Atan2(rotateTowards.Y, rotateTowards.X) + (float)Math.PI / 2f, rateOfRotation);
			}
		}
		if (Main.player[targetIndex].dead)
		{
			AIState = 0f;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.localAI[0] = 0f;
			base.NPC.localAI[1] = 0f;
			base.NPC.localAI[2] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			calamityGlobalNPC.newAI[3] = 0f;
			rotationDirection = 0;
			chargeVelocityNormalized = default(Vector2);
			spinningPoint = default(Vector2);
			spinVelocity = default(Vector2);
			base.NPC.dontTakeDamage = true;
			base.NPC.velocity.Y--;
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				base.NPC.velocity.Y--;
			}
			if (!((double)base.NPC.position.Y < (double)(Main.topWorld + 16f)))
			{
				return;
			}
			for (int a = 0; a < Main.maxNPCs; a++)
			{
				if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>() || Main.npc[a].type == ModContent.NPCType<AresBody>() || Main.npc[a].type == ModContent.NPCType<AresLaserCannon>() || Main.npc[a].type == ModContent.NPCType<AresPlasmaFlamethrower>() || Main.npc[a].type == ModContent.NPCType<AresTeslaCannon>() || Main.npc[a].type == ModContent.NPCType<AresGaussNuke>() || Main.npc[a].type == ModContent.NPCType<ThanatosHead>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody1>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody2>() || Main.npc[a].type == ModContent.NPCType<ThanatosTail>())
				{
					Main.npc[a].active = false;
				}
			}
			return;
		}
		float spinTime = 120f - 320f * (baseVelocityMult - 1.25f);
		if (phase2 && base.NPC.localAI[3] == 0f)
		{
			AIState = 4f;
			base.NPC.localAI[3] = 1f;
			calamityGlobalNPC.newAI[2] = 0f;
			calamityGlobalNPC.newAI[3] = 0f;
			base.NPC.frameCounter = 0.0;
			frameX = 3;
			frameY = 3;
		}
		bool shouldDoChargeFlash = false;
		switch ((int)SecondaryAIState)
		{
		}
		bool invisiblePhase = SecondaryAIState == 2f;
		base.NPC.dontTakeDamage = invisiblePhase || AIState == 4f;
		if (!invisiblePhase)
		{
			base.NPC.Opacity += 0.2f;
			if (base.NPC.Opacity > 1f)
			{
				base.NPC.Opacity = 1f;
			}
		}
		else
		{
			base.NPC.Opacity -= 0.05f;
			if (base.NPC.Opacity < 0f)
			{
				base.NPC.Opacity = 0f;
			}
		}
		switch ((int)AIState)
		{
		case 0:
		{
			base.NPC.damage = 0;
			if (!stopRotatingAndSlowDown)
			{
				chargeVelocityNormalized = default(Vector2);
				CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
			}
			else
			{
				Vector2 val4 = chargeVelocityNormalized;
				val2 = default(Vector2);
				if (val4 == val2)
				{
					chargeVelocityNormalized = Vector2.Normalize(aimedVector);
				}
				NPC nPC2 = base.NPC;
				nPC2.velocity *= decelerationVelocityMult;
			}
			bool attacking = calamityGlobalNPC.newAI[3] >= 2f;
			bool firingLasers = attacking && calamityGlobalNPC.newAI[3] + 2f < attackPhaseGateValue;
			if (SecondaryAIState != 2f)
			{
				calamityGlobalNPC.newAI[2]++;
			}
			if (!((calamityGlobalNPC.newAI[2] >= 60f) | attacking))
			{
				break;
			}
			if (firingLasers)
			{
				float divisor = (nerfedAttacks ? 45f : (lastMechAlive ? 25f : 30f));
				float laserTimer = calamityGlobalNPC.newAI[3] - 2f;
				if ((laserTimer % divisor == 0f) & canFire)
				{
					if (laserTimer % (divisor * 2f) == 0f)
					{
						pointToLookAt = default(Vector2);
						pickNewLocation = true;
					}
					else
					{
						Vector2 laserVelocity = Vector2.Normalize(aimedVector);
						Vector2 projectileDestination = (pointToLookAt = Main.player[targetIndex].Center + predictionVector);
						SoundEngine.PlaySound(in CommonCalamitySounds.ExoLaserShootSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int type = ModContent.ProjectileType<ArtemisLaser>();
							Vector2 offset2 = laserVelocity * 70f;
							float setVelocityInAI = 7.5f;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset2, projectileDestination, type, LaserDamage, 0f, Main.myPlayer, setVelocityInAI, base.NPC.whoAmI);
						}
					}
				}
			}
			else
			{
				pointToLookAt = default(Vector2);
				calamityGlobalNPC.newAI[2] = 0f;
			}
			calamityGlobalNPC.newAI[3]++;
			if (!lineUpAttack)
			{
				break;
			}
			pointToLookAt = default(Vector2);
			if (SecondaryAIState == 1f)
			{
				pickNewLocation = true;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityNormalized = default(Vector2);
				break;
			}
			if (!phase2 && calamityGlobalNPC.newAI[3] == attackPhaseGateValue + 5f)
			{
				shouldDoChargeFlash = true;
				SoundEngine.PlaySound(in ChargeTelegraphSound, base.NPC.Center);
				if (Main.netMode != 1)
				{
					int type2 = ModContent.ProjectileType<ArtemisChargeTelegraph>();
					Vector2 laserVelocity2 = Vector2.Normalize(aimedVector);
					Vector2 offset3 = laserVelocity2 * 50f;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset3, laserVelocity2, type2, 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
				}
			}
			if (!phase2 && calamityGlobalNPC.newAI[3] == attackPhaseGateValue + 2f + (timeToLineUpAttack - 30f) && Main.netMode != 1)
			{
				int type3 = ModContent.ProjectileType<ArtemisLaser>();
				Vector2 laserVelocity3 = chargeVelocityNormalized * 10f;
				int numLasersPerSpread = (death ? 8 : (expertMode ? 6 : 4));
				float rotation = MathHelper.ToRadians((float)(death ? 26 : (expertMode ? 21 : 15)));
				float distanceFromTarget = Vector2.Distance(base.NPC.Center, base.NPC.Center + chargeVelocityNormalized * chargeDistance);
				float setVelocityInAI2 = (death ? 7f : (revenge ? 6.75f : (expertMode ? 6.5f : 6f)));
				for (int i = 0; i < numLasersPerSpread + 1; i++)
				{
					double radians = MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numLasersPerSpread - 1));
					val2 = default(Vector2);
					Vector2 normalizedPerturbedSpeed = Vector2.Normalize(laserVelocity3.RotatedBy(radians, val2));
					Vector2 offset4 = normalizedPerturbedSpeed * 70f;
					Vector2 newCenter = base.NPC.Center + offset4;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), newCenter, newCenter + normalizedPerturbedSpeed * distanceFromTarget, type3, LaserDamage, 0f, Main.myPlayer, setVelocityInAI2, base.NPC.whoAmI);
				}
			}
			if (doBigAttack)
			{
				calamityGlobalNPC.newAI[3] = 0f;
				if (phase2)
				{
					AIState = ((base.NPC.localAI[2] == 1f && (!apolloUsingChargeCombo || Main.zenithWorld)) ? 3f : 2f);
					break;
				}
				base.NPC.damage = base.NPC.defDamage;
				SoundEngine.PlaySound(in ChargeSound, base.NPC.Center);
				AIState = 1f;
				base.NPC.velocity = chargeVelocityNormalized * chargeVelocity;
				chargeVelocityNormalized = default(Vector2);
			}
			break;
		}
		case 1:
			base.NPC.damage = base.NPC.defDamage;
			shouldDoChargeFlash = true;
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] >= chargeDuration)
			{
				base.NPC.damage = 0;
				NPC nPC = base.NPC;
				nPC.velocity *= decelerationVelocityMult;
				if (calamityGlobalNPC.newAI[2] >= chargeDuration + 10f)
				{
					pickNewLocation = true;
					AIState = 0f;
					calamityGlobalNPC.newAI[2] = 0f;
				}
			}
			break;
		case 2:
		{
			base.NPC.damage = 0;
			CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
			int numSpreads = (lastMechAlive ? 3 : 2);
			float divisor2 = laserShotgunDuration / (float)numSpreads;
			if (((calamityGlobalNPC.newAI[2] % divisor2 == 0f) & canFire) && calamityGlobalNPC.newAI[2] < laserShotgunDuration)
			{
				SoundEngine.PlaySound(in LaserShotgunSound, base.NPC.Center);
				Vector2 laserVelocity5 = Vector2.Normalize(aimedVector) * 10f;
				int type6 = ModContent.ProjectileType<ArtemisLaser>();
				int numLasersAddedByDifficulty = (death ? 2 : (expertMode ? 1 : 0));
				int numLasersPerSpread2 = ((nerfedAttacks | nerfedLaserShotgun) ? 3 : (lastMechAlive ? 7 : 5)) + numLasersAddedByDifficulty;
				int baseSpread = ((nerfedAttacks | nerfedLaserShotgun) ? 9 : (lastMechAlive ? 20 : 15)) + numLasersAddedByDifficulty * 2;
				float rotation2 = MathHelper.ToRadians((float)(baseSpread + (int)(calamityGlobalNPC.newAI[2] / divisor2) * (baseSpread / 4)));
				float distanceFromTarget2 = Vector2.Distance(base.NPC.Center, Main.player[targetIndex].Center + predictionVector);
				float setVelocityInAI3 = (death ? 6.5f : (revenge ? 6.25f : (expertMode ? 6f : 5.5f)));
				pointToLookAt = Main.player[targetIndex].Center + predictionVector;
				for (int j = 0; j < numLasersPerSpread2 + 1; j++)
				{
					double radians2 = MathHelper.Lerp(0f - rotation2, rotation2, (float)j / (float)(numLasersPerSpread2 - 1));
					val2 = default(Vector2);
					Vector2 normalizedPerturbedSpeed2 = Vector2.Normalize(laserVelocity5.RotatedBy(radians2, val2));
					Vector2 offset6 = normalizedPerturbedSpeed2 * 70f;
					Vector2 newCenter2 = base.NPC.Center + offset6;
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), newCenter2, newCenter2 + normalizedPerturbedSpeed2 * distanceFromTarget2, type6, LaserDamage, 0f, Main.myPlayer, setVelocityInAI3, base.NPC.whoAmI);
					}
				}
			}
			if (canFire || calamityGlobalNPC.newAI[2] > 0f)
			{
				calamityGlobalNPC.newAI[2]++;
			}
			if (calamityGlobalNPC.newAI[2] >= laserShotgunDuration + 20f)
			{
				pointToLookAt = default(Vector2);
				pickNewLocation = true;
				AIState = 0f;
				base.NPC.localAI[2] = (shouldGetBuffedByBerserkPhase ? 1f : 0f);
				calamityGlobalNPC.newAI[2] = 0f;
			}
			break;
		}
		case 3:
			base.NPC.damage = 0;
			val2 = destination - base.NPC.Center;
			if (((Vector2)(ref val2)).Length() < spinLocationDistance || calamityGlobalNPC.newAI[2] > 0f)
			{
				if (calamityGlobalNPC.newAI[2] == 0f)
				{
					base.NPC.velocity = Vector2.Zero;
					switch ((int)base.NPC.ai[3])
					{
					case 0:
					case 1:
						spinningPoint = base.NPC.Center + Vector2.UnitY * spinRadius;
						break;
					case 2:
						spinningPoint = base.NPC.Center - Vector2.UnitY * spinRadius;
						break;
					case 3:
						spinningPoint = base.NPC.Center + Vector2.UnitX * spinRadius;
						break;
					case 4:
						spinningPoint = base.NPC.Center - Vector2.UnitX * spinRadius;
						break;
					}
					base.NPC.ai[1] = spinningPoint.X;
					base.NPC.ai[2] = spinningPoint.Y;
					SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int type4 = ModContent.ProjectileType<ArtemisDeathrayTelegraph>();
						Vector2 laserVelocity4 = Vector2.Normalize(spinningPoint - base.NPC.Center);
						Vector2 offset5 = laserVelocity4 * 70f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset5, laserVelocity4, type4, 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					}
					base.NPC.netUpdate = true;
				}
				calamityGlobalNPC.newAI[2]++;
				if (calamityGlobalNPC.newAI[2] >= 60f)
				{
					if (rotationDirection == 0)
					{
						spinVelocity.X = (float)Math.PI * spinRadius / spinTime;
						switch ((int)base.NPC.ai[3])
						{
						case 0:
						case 1:
							if (Main.player[targetIndex].Center.X >= base.NPC.Center.X)
							{
								rotationDirection = 1;
							}
							else
							{
								rotationDirection = -1;
							}
							break;
						case 2:
							if (Main.player[targetIndex].Center.X >= base.NPC.Center.X)
							{
								rotationDirection = -1;
							}
							else
							{
								rotationDirection = 1;
							}
							spinVelocity = -spinVelocity;
							break;
						case 3:
						{
							if (Main.player[targetIndex].Center.Y >= base.NPC.Center.Y)
							{
								rotationDirection = -1;
							}
							else
							{
								rotationDirection = 1;
							}
							Vector2 spinningpoint2 = spinVelocity;
							val2 = default(Vector2);
							spinVelocity = spinningpoint2.RotatedBy(-1.5707963705062866, val2);
							break;
						}
						case 4:
						{
							if (Main.player[targetIndex].Center.Y >= base.NPC.Center.Y)
							{
								rotationDirection = 1;
							}
							else
							{
								rotationDirection = -1;
							}
							Vector2 spinningpoint = spinVelocity;
							val2 = default(Vector2);
							spinVelocity = spinningpoint.RotatedBy(1.5707963705062866, val2);
							break;
						}
						}
						spinVelocity *= (float)(-rotationDirection);
						base.NPC.netUpdate = true;
						ExoMechsSky.CreateLightningBolt(12);
						DeathraySoundSlot = SoundEngine.PlaySound(in SpinLaserbeamSound, base.NPC.Center);
						if (Main.netMode != 1)
						{
							int type5 = ModContent.ProjectileType<ArtemisSpinLaserbeam>();
							int laser = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, type5, BeamDamage, 0f, Main.myPlayer, base.NPC.whoAmI);
							if (Main.projectile.IndexInRange(laser))
							{
								Main.projectile[laser].ai[0] = base.NPC.whoAmI;
								Main.projectile[laser].ai[1] = rotationDirection;
							}
						}
					}
					else
					{
						float rotationSpeedMult = (death ? 4f : (revenge ? 3f : (expertMode ? 2.5f : 2f)));
						float rotationMult = (calamityGlobalNPC.newAI[2] - 60f) / 180f * rotationSpeedMult;
						if (rotationMult > 1f)
						{
							double radiansOfRotation = (float)Math.PI / spinTime * (float)(-rotationDirection);
							NPC nPC3 = base.NPC;
							Vector2 velocity = base.NPC.velocity;
							val2 = default(Vector2);
							nPC3.velocity = velocity.RotatedBy(radiansOfRotation, val2);
						}
						else
						{
							float decelerationMult = rotationMult * rotationMult;
							double radiansOfRotation2 = (float)Math.PI / spinTime * (float)(-rotationDirection) * decelerationMult;
							NPC nPC4 = base.NPC;
							Vector2 spinningpoint3 = spinVelocity * decelerationMult;
							val2 = default(Vector2);
							nPC4.velocity = spinningpoint3.RotatedBy(radiansOfRotation2, val2);
						}
					}
				}
			}
			else
			{
				CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
			}
			if (!(calamityGlobalNPC.newAI[2] >= 240f))
			{
				break;
			}
			if (Main.zenithWorld && !exoMechdusa)
			{
				calamityGlobalNPC.newAI[3] = 0f;
				AIState = 3f;
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile p = enumerator.Current;
					if (p.type == ModContent.ProjectileType<ArtemisSpinLaserbeam>())
					{
						p.active = false;
					}
				}
			}
			else
			{
				AIState = 0f;
			}
			spinVelocity = default(Vector2);
			rotationDirection = 0;
			spinningPoint = default(Vector2);
			pickNewLocation = true;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			if (revenge)
			{
				switch ((int)base.NPC.ai[3])
				{
				case 0:
				case 1:
					base.NPC.ai[3] = 3f + (float)Main.rand.Next(2);
					break;
				case 2:
					base.NPC.ai[3] = 3f + (float)Main.rand.Next(2);
					break;
				case 3:
					base.NPC.ai[3] = 1f + (float)Main.rand.Next(2);
					break;
				case 4:
					base.NPC.ai[3] = 1f + (float)Main.rand.Next(2);
					break;
				}
			}
			else if (expertMode)
			{
				base.NPC.ai[3]++;
				if (base.NPC.ai[3] > 2f)
				{
					base.NPC.ai[3] = 1f;
				}
			}
			base.NPC.localAI[2] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			base.NPC.netUpdate = true;
			break;
		case 4:
			base.NPC.damage = 0;
			CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
			if (calamityGlobalNPC.newAI[2] == 48f)
			{
				SoundEngine.PlaySound(in LensSound, base.NPC.Center);
				Vector2 lensDirection = Vector2.Normalize(aimedVector);
				Vector2 offset = lensDirection * 70f;
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset, lensDirection * 24f, ModContent.ProjectileType<BrokenArtemisLens>(), 0, 0f);
				}
			}
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] >= 180f)
			{
				pickNewLocation = true;
				AIState = 0f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				chargeVelocityNormalized = default(Vector2);
			}
			break;
		}
		ChargeFlash = MathHelper.Clamp(ChargeFlash + (float)shouldDoChargeFlash.ToDirectionInt() * 0.08f, 0f, 1f);
		if (SoundEngine.TryGetActiveSound(DeathraySoundSlot, out ActiveSound deathraySound) && deathraySound.IsPlaying)
		{
			deathraySound.Position = base.NPC.Center;
		}
		if (!exoMechdusa)
		{
			return;
		}
		int twinoffset = 300;
		int extratwinoffset = 100;
		int twinheight = 300;
		if (CalamityGlobalNPC.draedonExoMechPrime != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrime].ModNPC<AresBody>().exoMechdusa)
		{
			NPC aresin = Main.npc[CalamityGlobalNPC.draedonExoMechPrime];
			if (base.NPC.Calamity().newAI[0] != 1f && base.NPC.Calamity().newAI[0] != 3f)
			{
				Vector2 pos = default(Vector2);
				((Vector2)(ref pos))._002Ector(aresin.Center.X + (float)twinoffset - (float)extratwinoffset, aresin.Center.Y - (float)twinheight);
				base.NPC.position = pos;
			}
		}
		return;
		IL_04dc:
		num2 = ((otherExoMechsAlive == 0) ? 1 : 0);
		goto IL_04e4;
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
		if (minDist <= 100f)
		{
			return base.NPC.Opacity == 1f;
		}
		return false;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 2f;
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		bool phase2 = (float)base.NPC.life / (float)base.NPC.lifeMax < 0.6f;
		base.NPC.frameCounter++;
		if (AIState == 4f)
		{
			if (base.NPC.frameCounter >= 6.0)
			{
				base.NPC.frameCounter = 0.0;
				frameY++;
				if (frameY == 9)
				{
					frameX++;
					frameY = 0;
				}
			}
		}
		else if (AIState == 0f)
		{
			int frameLimit = ((!phase2) ? ((base.NPC.Calamity().newAI[3] == 0f) ? 9 : ((base.NPC.Calamity().newAI[3] == 1f) ? 19 : 29)) : ((base.NPC.Calamity().newAI[3] == 0f) ? 69 : ((base.NPC.Calamity().newAI[3] == 1f) ? 79 : 89)));
			if (base.NPC.frameCounter >= 6.0)
			{
				base.NPC.frameCounter = 0.0;
				frameY++;
				if (frameY == 9)
				{
					frameX++;
					frameY = 0;
				}
				if (frameX * 9 + frameY > frameLimit)
				{
					frameX = (frameY = ((!phase2) ? ((base.NPC.Calamity().newAI[3] != 0f) ? ((base.NPC.Calamity().newAI[3] == 1f) ? 1 : 2) : 0) : ((base.NPC.Calamity().newAI[3] == 0f) ? 6 : ((base.NPC.Calamity().newAI[3] == 1f) ? 7 : 8))));
				}
			}
		}
		else if (AIState == 1f || AIState == 2f || AIState == 3f)
		{
			int frameLimit2 = (phase2 ? 89 : 29);
			if (base.NPC.frameCounter >= 6.0)
			{
				base.NPC.frameCounter = 0.0;
				frameY++;
				if (frameY == 9)
				{
					frameX++;
					frameY = 0;
				}
				if (frameX * 9 + frameY > frameLimit2)
				{
					frameX = (frameY = (phase2 ? 8 : 2));
				}
			}
		}
		base.NPC.frame = new Rectangle(base.NPC.width * frameX, base.NPC.height * frameY, base.NPC.width, base.NPC.height);
	}

	public float FlameTrailWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.SmoothStep(21f, 8f, completionRatio) * ChargeFlash;
	}

	public float FlameTrailWidthFunctionBig(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.SmoothStep(34f, 12f, completionRatio) * ChargeFlash;
	}

	public float RibbonTrailWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float num = Utils.GetLerpValue(1f, 0.54f, completionRatio, clamped: true) * 5f;
		float endTipWidth = CalamityUtils.Convert01To010(Utils.GetLerpValue(0.96f, 0.89f, completionRatio, clamped: true)) * 2.4f;
		return num + endTipWidth;
	}

	public Color FlameTrailColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		float trailOpacity = Utils.GetLerpValue(0.8f, 0.27f, completionRatio, clamped: true) * Utils.GetLerpValue(0f, 0.067f, completionRatio, clamped: true);
		Color startingColor = Color.Lerp(Color.White, Color.Cyan, 0.27f);
		Color middleColor = Color.Lerp(Color.Orange, Color.Yellow, 0.31f);
		Color endColor = Color.OrangeRed;
		return CalamityUtils.MulticolorLerp(completionRatio, startingColor, middleColor, endColor) * ChargeFlash * trailOpacity;
	}

	public Color FlameTrailColorFunctionBig(float completionRatio, Vector2 vertexPos)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		float trailOpacity = Utils.GetLerpValue(0.8f, 0.27f, completionRatio, clamped: true) * Utils.GetLerpValue(0f, 0.067f, completionRatio, clamped: true) * 0.56f;
		Color startingColor = Color.Lerp(Color.White, Color.Cyan, 0.25f);
		Color middleColor = Color.Lerp(Color.Blue, Color.White, 0.35f);
		Color endColor = Color.Lerp(Color.DarkBlue, Color.White, 0.47f);
		Color color = CalamityUtils.MulticolorLerp(completionRatio, startingColor, middleColor, endColor) * ChargeFlash * trailOpacity;
		((Color)(ref color)).A = 0;
		return color;
	}

	public Color RibbonTrailColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Color val = new Color(34, 40, 48);
		Color endColor = default(Color);
		((Color)(ref endColor))._002Ector(219, 82, 28);
		return Color.Lerp(val, endColor, (float)Math.Pow(completionRatio, 1.5)) * base.NPC.Opacity;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		int numAfterimages = ((!(ChargeFlash > 0f)) ? 5 : 0);
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Rectangle frame = new Rectangle(base.NPC.width * frameX, base.NPC.height * frameY, base.NPC.width, base.NPC.height);
		Vector2 origin = base.NPC.Size * 0.5f;
		Vector2 center = base.NPC.Center - screenPos;
		Color afterimageBaseColor = Color.White;
		for (int direction = -1; direction <= 1; direction += 2)
		{
			if (base.NPC.IsABestiaryIconDummy)
			{
				break;
			}
			Vector2 ribbonOffset = -Vector2.UnitY.RotatedBy(base.NPC.rotation) * 14f;
			ribbonOffset += Vector2.UnitX.RotatedBy(base.NPC.rotation) * (float)direction * 26f;
			float currentSegmentRotation = base.NPC.rotation;
			List<Vector2> ribbonDrawPositions = new List<Vector2>();
			for (int i = 0; i < 12; i++)
			{
				float ribbonCompletionRatio = (float)i / 12f;
				float segmentRotationOffset = MathHelper.Clamp(MathHelper.WrapAngle(base.NPC.oldRot[i + 1] - currentSegmentRotation) * 0.3f, -0.12f, 0.12f);
				float sinusoidalRotationOffset = (float)Math.Sin(ribbonCompletionRatio * 2.22f + Main.GlobalTimeWrappedHourly * 3.4f) * 1.36f;
				float sinusoidalRotationOffsetFactor = Utils.GetLerpValue(0f, 0.37f, ribbonCompletionRatio, clamped: true) * (float)direction * 24f;
				sinusoidalRotationOffsetFactor *= Utils.GetLerpValue(24f, 16f, ((Vector2)(ref base.NPC.velocity)).Length(), clamped: true);
				Vector2 sinusoidalOffset = Vector2.UnitY.RotatedBy(base.NPC.rotation + sinusoidalRotationOffset) * sinusoidalRotationOffsetFactor;
				Vector2 ribbonSegmentOffset = Vector2.UnitY.RotatedBy(currentSegmentRotation) * ribbonCompletionRatio * 540f + sinusoidalOffset;
				ribbonDrawPositions.Add(base.NPC.Center + ribbonSegmentOffset + ribbonOffset);
				currentSegmentRotation += segmentRotationOffset;
			}
			PrimitiveRenderer.RenderTrail(ribbonDrawPositions, new PrimitiveSettings(RibbonTrailWidthFunction, RibbonTrailColorFunction), 66);
		}
		int instanceCount = (int)MathHelper.Lerp(1f, 15f, ChargeFlash);
		Color baseInstanceColor = Color.Lerp(drawColor, Color.White, ChargeFlash);
		((Color)(ref baseInstanceColor)).A = (byte)(int)(255f - ChargeFlash * 255f);
		if (!base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.EnterShaderRegion();
		}
		drawInstance(Vector2.Zero, baseInstanceColor);
		if (instanceCount > 1)
		{
			baseInstanceColor *= 0.04f;
			float backAfterimageOffset = MathHelper.SmoothStep(0f, 2f, ChargeFlash);
			for (int j = 0; j < instanceCount; j++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)j / (float)instanceCount + Main.GlobalTimeWrappedHourly * 0.8f).ToRotationVector2() * backAfterimageOffset;
				drawInstance(drawOffset, baseInstanceColor);
			}
		}
		texture = GlowTexture.Value;
		if (CalamityClientConfig.Instance.Afterimages && !base.NPC.IsABestiaryIconDummy)
		{
			for (int k = 1; k < numAfterimages; k += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, afterimageBaseColor, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(numAfterimages - k) / 15f;
				Vector2 afterimageCenter = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageCenter -= new Vector2((float)texture.Width, (float)texture.Height) / new Vector2(10f, 9f) * base.NPC.scale / 2f;
				afterimageCenter += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimageCenter, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.oldRot[k], origin, base.NPC.scale, (SpriteEffects)0, 0f);
			}
		}
		spriteBatch.Draw(texture, center, (Rectangle?)frame, Color.White * base.NPC.Opacity, base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
		if (!base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.ExitShaderRegion();
		}
		if (ChargeFlash > 0f)
		{
			for (int l = -1; l <= 1; l++)
			{
				Vector2 baseDrawOffset = Utils.RotatedBy(new Vector2(0f, ((float)l == 0f) ? 18f : 60f), (double)base.NPC.rotation, default(Vector2));
				baseDrawOffset += Utils.RotatedBy(new Vector2((float)l * 64f, 0f), (double)base.NPC.rotation, default(Vector2));
				float backFlameLength = (((float)l == 0f) ? 700f : 190f);
				Vector2 drawStart = base.NPC.Center + baseDrawOffset;
				Vector2 drawEnd = drawStart - (base.NPC.rotation - (float)Math.PI / 2f).ToRotationVector2() * ChargeFlash * backFlameLength;
				Vector2[] drawPositions = (Vector2[])(object)new Vector2[2] { drawStart, drawEnd };
				if (l == 0)
				{
					for (int m = 0; m < 4; m++)
					{
						Vector2 drawOffset2 = ((float)Math.PI * 2f * (float)m / 4f).ToRotationVector2() * 8f;
						PrimitiveRenderer.RenderTrail(drawPositions, new PrimitiveSettings(FlameTrailWidthFunctionBig, FlameTrailColorFunctionBig, delegate
						{
							//IL_0001: Unknown result type (might be due to invalid IL or missing references)
							return drawOffset2;
						}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), 70);
					}
				}
				else
				{
					PrimitiveRenderer.RenderTrail(drawPositions, new PrimitiveSettings(FlameTrailWidthFunction, FlameTrailColorFunction, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), 70);
				}
			}
		}
		return false;
		void drawInstance(Vector2 val, Color baseColor)
		{
			//IL_019b: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_012f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_0131: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_014a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_0163: Unknown result type (might be due to invalid IL or missing references)
			if (CalamityClientConfig.Instance.Afterimages && !base.NPC.IsABestiaryIconDummy)
			{
				for (int n = 1; n < numAfterimages; n += 2)
				{
					Color afterimageColor2 = baseColor;
					afterimageColor2 = Color.Lerp(afterimageColor2, afterimageBaseColor, 0.5f);
					afterimageColor2 = base.NPC.GetAlpha(afterimageColor2);
					afterimageColor2 *= (float)(numAfterimages - n) / 15f;
					Vector2 afterimageCenter2 = base.NPC.oldPos[n] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					afterimageCenter2 -= new Vector2((float)texture.Width, (float)texture.Height) / new Vector2(10f, 9f) * base.NPC.scale / 2f;
					afterimageCenter2 += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
					afterimageCenter2 += val;
					spriteBatch.Draw(texture, afterimageCenter2, (Rectangle?)base.NPC.frame, afterimageColor2, base.NPC.oldRot[n], origin, base.NPC.scale, (SpriteEffects)0, 0f);
				}
			}
			spriteBatch.Draw(texture, center + val, (Rectangle?)frame, base.NPC.GetAlpha(baseColor), base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
		}
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (exoMechdusa)
		{
			typeName = (NameToDisplay = this.GetLocalizedValue("HekateName"));
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255));
		}
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 3;
			SoundEngine.PlaySound(in CommonCalamitySounds.ExoHitSound, base.NPC.Center);
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Artemis1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Artemis2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Artemis3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Artemis4").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Artemis5").Type);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<OmegaHealingPotion>();
	}

	public override bool CheckDead()
	{
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC nPC = enumerator.Current;
			if (nPC.type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>() && nPC.life > 0)
			{
				nPC.life = 0;
				nPC.HitEffect();
				nPC.checkDead();
				nPC.active = false;
				nPC.netUpdate = true;
			}
		}
		return true;
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
