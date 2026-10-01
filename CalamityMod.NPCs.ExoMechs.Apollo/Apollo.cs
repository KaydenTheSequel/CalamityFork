using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Potions;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Skies;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ExoMechs.Apollo;

public class Apollo : ModNPC
{
	public enum Phase
	{
		Normal,
		RocketBarrage,
		LineUpChargeCombo,
		ChargeCombo,
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

	public static Asset<Texture2D> GlowTexture;

	private float velocityBoostMult;

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

	private bool pickNewLocation;

	public bool berserkEarlyBugFix;

	public bool exoMechdusa;

	private const int maxCharges = 4;

	public Vector2[] chargeLocations = (Vector2[])(object)new Vector2[4];

	public float ChargeComboFlash;

	public static string NameToDisplay = "XS-03 Apollo";

	public static readonly SoundStyle MissileLaunchSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/ApolloMissileLaunch")
	{
		Volume = 1.3f
	};

	public static int FireballDamage = 80;

	public static int BoltDamage = 70;

	public static int RocketDamage = 95;

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
		string phase1IconPath = "CalamityMod/NPCs/ExoMechs/Apollo/ApolloHead";
		string phase2IconPath = "CalamityMod/NPCs/ExoMechs/Apollo/ApolloPhase2Head";
		phase1IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase1IconPath);
		phase2IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase2IconPath);
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.TrailingMode[base.Type] = 3;
		NPCID.Sets.TrailCacheLength[base.Type] = 15;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 50f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 40f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		nPCBestiaryDrawModifiers.Scale = 0.45f;
		nPCBestiaryDrawModifiers.Rotation = -(float)Math.PI / 4f;
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
		base.NPC.damage = 240;
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
		base.NPC.value = Item.buyPrice(1);
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.DeathSound = CommonCalamitySounds.ExoDeathSound;
		base.NPC.netAlways = true;
		base.NPC.boss = true;
		base.NPC.BossBar = Main.BigBossProgressBar.NeverValid;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Apollo")
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
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(velocityBoostMult);
		writer.Write(frameX);
		writer.Write(frameY);
		writer.Write(exoMechdusa);
		writer.Write(pickNewLocation);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
			writer.WriteVector2(chargeLocations[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		velocityBoostMult = reader.ReadSingle();
		frameX = reader.ReadInt32();
		frameY = reader.ReadInt32();
		pickNewLocation = reader.ReadBoolean();
		exoMechdusa = reader.ReadBoolean();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
			chargeLocations[i] = reader.ReadVector2();
		}
	}

	public override void AI()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0862: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1870: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1350: Unknown result type (might be due to invalid IL or missing references)
		//IL_171c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c13: Unknown result type (might be due to invalid IL or missing references)
		//IL_2155: Unknown result type (might be due to invalid IL or missing references)
		//IL_240a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2415: Unknown result type (might be due to invalid IL or missing references)
		//IL_2432: Unknown result type (might be due to invalid IL or missing references)
		//IL_2446: Unknown result type (might be due to invalid IL or missing references)
		//IL_244b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2450: Unknown result type (might be due to invalid IL or missing references)
		//IL_2457: Unknown result type (might be due to invalid IL or missing references)
		//IL_245c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2918: Unknown result type (might be due to invalid IL or missing references)
		//IL_2923: Unknown result type (might be due to invalid IL or missing references)
		//IL_2929: Unknown result type (might be due to invalid IL or missing references)
		//IL_292b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2930: Unknown result type (might be due to invalid IL or missing references)
		//IL_2932: Unknown result type (might be due to invalid IL or missing references)
		//IL_2939: Unknown result type (might be due to invalid IL or missing references)
		//IL_293e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1155: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a86: Unknown result type (might be due to invalid IL or missing references)
		//IL_274f: Unknown result type (might be due to invalid IL or missing references)
		//IL_275f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2764: Unknown result type (might be due to invalid IL or missing references)
		//IL_295a: Unknown result type (might be due to invalid IL or missing references)
		//IL_295f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2961: Unknown result type (might be due to invalid IL or missing references)
		//IL_2966: Unknown result type (might be due to invalid IL or missing references)
		//IL_296d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2039: Unknown result type (might be due to invalid IL or missing references)
		//IL_2044: Unknown result type (might be due to invalid IL or missing references)
		//IL_2180: Unknown result type (might be due to invalid IL or missing references)
		//IL_2187: Unknown result type (might be due to invalid IL or missing references)
		//IL_218d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2319: Unknown result type (might be due to invalid IL or missing references)
		//IL_231e: Unknown result type (might be due to invalid IL or missing references)
		//IL_233e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2349: Unknown result type (might be due to invalid IL or missing references)
		//IL_1507: Unknown result type (might be due to invalid IL or missing references)
		//IL_2063: Unknown result type (might be due to invalid IL or missing references)
		//IL_2065: Unknown result type (might be due to invalid IL or missing references)
		//IL_206c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2076: Unknown result type (might be due to invalid IL or missing references)
		//IL_207b: Unknown result type (might be due to invalid IL or missing references)
		//IL_207d: Unknown result type (might be due to invalid IL or missing references)
		//IL_207f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2089: Unknown result type (might be due to invalid IL or missing references)
		//IL_208e: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_21fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2211: Unknown result type (might be due to invalid IL or missing references)
		//IL_2220: Unknown result type (might be due to invalid IL or missing references)
		//IL_2225: Unknown result type (might be due to invalid IL or missing references)
		//IL_222a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2240: Unknown result type (might be due to invalid IL or missing references)
		//IL_2250: Unknown result type (might be due to invalid IL or missing references)
		//IL_2255: Unknown result type (might be due to invalid IL or missing references)
		//IL_225a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2515: Unknown result type (might be due to invalid IL or missing references)
		//IL_2506: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ac7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_251a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e13: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2281: Unknown result type (might be due to invalid IL or missing references)
		//IL_2286: Unknown result type (might be due to invalid IL or missing references)
		//IL_2874: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e39: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e60: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e65: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e67: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e87: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2521: Unknown result type (might be due to invalid IL or missing references)
		//IL_252c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2532: Unknown result type (might be due to invalid IL or missing references)
		//IL_2534: Unknown result type (might be due to invalid IL or missing references)
		//IL_2539: Unknown result type (might be due to invalid IL or missing references)
		//IL_254d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2552: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_26c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_26cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_26d0: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.draedonExoMechTwinGreen = base.NPC.whoAmI;
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
		bool exoWormAlive = false;
		bool exoPrimeAlive = false;
		bool exoMechTwinRedAlive = false;
		bool artemisUsingDeathray = false;
		if (CalamityGlobalNPC.draedonExoMechTwinRed != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].active)
		{
			exoMechTwinRedAlive = true;
			artemisUsingDeathray = Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[0] == 3f;
			if (base.NPC.life > Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].life)
			{
				base.NPC.life = Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].life;
			}
		}
		if (CalamityGlobalNPC.draedonExoMechWorm != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechWorm].active)
		{
			targetIndex = Main.npc[CalamityGlobalNPC.draedonExoMechWorm].target;
			otherExoMechsAlive++;
			exoWormAlive = true;
			if ((Main.npc[CalamityGlobalNPC.draedonExoMechWorm].ModNPC as ThanatosHead).berserkEarlyBugFix)
			{
				berserkEarlyBugFix = true;
			}
		}
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
		float totalOtherExoMechLifeRatio = exoWormLifeRatio + exoPrimeLifeRatio;
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
		bool anyOtherExoMechPassive = exoWormPassive | exoPrimePassive;
		bool nerfedAttacks = false;
		if (exoWormAlive && !nerfedAttacks)
		{
			nerfedAttacks = Main.npc[CalamityGlobalNPC.draedonExoMechWorm].Calamity().newAI[1] != 2f;
		}
		if (exoPrimeAlive && !nerfedAttacks)
		{
			nerfedAttacks = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].Calamity().newAI[1] != 2f;
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
		bool num = exoWormWasFirst | exoPrimeWasFirst;
		bool draedonAlive = false;
		if (CalamityGlobalNPC.draedon != -1 && Main.npc[CalamityGlobalNPC.draedon].active)
		{
			draedonAlive = true;
		}
		if (num && base.NPC.ai[3] < 1f)
		{
			base.NPC.ai[3] = 1f;
		}
		if (base.NPC.ai[3] > 1f)
		{
			base.NPC.ai[3]--;
		}
		bool phase2 = lifeRatio < 0.6f;
		bool spawnOtherExoMechs = lifeRatio < 0.7f && base.NPC.ai[3] == 0f;
		bool berserk = lifeRatio < 0.4f || (otherExoMechsAlive == 0 && lifeRatio < 0.7f);
		bool lastMechAlive = berserk && otherExoMechsAlive == 0;
		bool otherMechIsBerserk = exoWormLifeRatio < 0.4f || exoPrimeLifeRatio < 0.4f;
		bool shouldGetBuffedByBerserkPhase = berserk && !otherMechIsBerserk;
		if (base.NPC.ai[0] < 10f)
		{
			base.NPC.ai[0]++;
			if (base.NPC.ai[0] == 10f && !NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>()))
			{
				NPC.SpawnOnPlayer(Main.player[targetIndex].whoAmI, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>());
			}
		}
		else if (!NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>()))
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
		float timeToLineUpAttack = 30f;
		float timeToLineUpCharge = (death ? 60f : (revenge ? 68f : (expertMode ? 75f : 90f)));
		if (Main.getGoodWorld)
		{
			timeToLineUpAttack *= 0.5f;
			timeToLineUpCharge *= 0.5f;
		}
		float movementDistanceGateValue = 100f;
		float chargeLocationDistanceGateValue = 40f;
		float baseVelocityMult = (shouldGetBuffedByBerserkPhase ? 0.25f : 0f) + (death ? 1.1f : (revenge ? 1.075f : (expertMode ? 1.05f : 1f)));
		float baseVelocity = ((AIState == 2f) ? 40f : 20f) * baseVelocityMult;
		bool lineUpAttack = calamityGlobalNPC.newAI[3] >= attackPhaseGateValue + 2f;
		bool doBigAttack = calamityGlobalNPC.newAI[3] >= attackPhaseGateValue + 2f + timeToLineUpAttack;
		float chargeVelocity = (death ? 105f : (revenge ? 101.25f : (expertMode ? 97.5f : 90f)));
		if (Main.getGoodWorld)
		{
			baseVelocity *= 1.5f;
			chargeVelocity *= 1.15f;
		}
		float chargeTime = (float)Math.Sqrt(890000.0) / chargeVelocity;
		float projectileVelocity = 12f;
		if (lastMechAlive)
		{
			projectileVelocity *= 1.2f;
		}
		else if (shouldGetBuffedByBerserkPhase)
		{
			projectileVelocity *= 1.1f;
		}
		float rocketPhaseDuration = (lastMechAlive ? 60f : 90f);
		int numRockets = (nerfedAttacks ? 2 : 3);
		if (Main.getGoodWorld)
		{
			numRockets += 3;
		}
		int num2;
		float num3;
		if (base.NPC.ai[0] % 2f != 0f && !(base.NPC.ai[0] < 10f))
		{
			num2 = ((!revenge) ? 1 : 0);
			if (num2 == 0)
			{
				num3 = -750f;
				goto IL_07ac;
			}
		}
		else
		{
			num2 = 1;
		}
		num3 = 750f;
		goto IL_07ac;
		IL_07ac:
		float destinationX = num3;
		float destinationY = Main.player[targetIndex].Center.Y;
		float chargeComboXOffset = ((num2 != 0) ? (-600f) : 600f);
		float chargeComboYOffset = ((base.NPC.ai[2] % 2f == 0f) ? 480f : (-480f));
		Vector2 destination = ((SecondaryAIState == 2f) ? new Vector2(Main.player[targetIndex].Center.X + destinationX * 1.6f, destinationY) : ((SecondaryAIState == 1f) ? new Vector2(Main.player[targetIndex].Center.X + destinationX, destinationY + 360f) : ((AIState == 2f) ? new Vector2(Main.player[targetIndex].Center.X + destinationX * 1.2f, destinationY + chargeComboYOffset) : new Vector2(Main.player[targetIndex].Center.X + destinationX, destinationY))));
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
			if (AIState == 1f || SecondaryAIState == 1f)
			{
				base.NPC.localAI[0] *= 0.5f;
				base.NPC.localAI[1] *= 0.5f;
			}
			base.NPC.netUpdate = true;
		}
		if (AIState == 0f || AIState == 1f || AIState == 4f)
		{
			destination.X += base.NPC.localAI[0];
			destination.Y += base.NPC.localAI[1];
		}
		Vector2 distanceFromDestination = destination - base.NPC.Center;
		if (((Vector2)(ref distanceFromDestination)).Length() > movementDistanceGateValue && AIState != 3f)
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
		bool canFire = ((Vector2)(ref distanceFromDestination)).Length() <= 320f;
		Vector2 aimedVector = Main.player[targetIndex].Center - base.NPC.Center;
		float rateOfRotation = 0.1f;
		Vector2 rotateTowards = Main.player[targetIndex].Center - base.NPC.Center;
		bool readyToCharge = AIState == 2f && (Vector2.Distance(base.NPC.Center, destination) <= chargeLocationDistanceGateValue || calamityGlobalNPC.newAI[2] > 0f);
		if (AIState == 3f)
		{
			rateOfRotation = 0f;
			base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		else if (AIState == 2f && chargeLocations[1] != default(Vector2))
		{
			float num4 = chargeLocations[1].X - base.NPC.Center.X;
			float y = chargeLocations[1].Y - base.NPC.Center.Y;
			rotateTowards = Vector2.Normalize(new Vector2(num4, y)) * baseVelocity;
		}
		else
		{
			float num5 = Main.player[targetIndex].Center.X - base.NPC.Center.X;
			float y2 = Main.player[targetIndex].Center.Y - base.NPC.Center.Y;
			rotateTowards = Vector2.Normalize(new Vector2(num5, y2)) * baseVelocity;
		}
		if (rateOfRotation != 0f)
		{
			base.NPC.rotation = base.NPC.rotation.AngleTowards((float)Math.Atan2(rotateTowards.Y, rotateTowards.X) + (float)Math.PI / 2f, rateOfRotation);
		}
		if (Main.player[targetIndex].dead)
		{
			AIState = 0f;
			base.NPC.localAI[0] = 0f;
			base.NPC.localAI[1] = 0f;
			base.NPC.localAI[2] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			calamityGlobalNPC.newAI[3] = 0f;
			for (int i = 0; i < 4; i++)
			{
				chargeLocations[i] = default(Vector2);
			}
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
				if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>() || Main.npc[a].type == ModContent.NPCType<AresBody>() || Main.npc[a].type == ModContent.NPCType<AresLaserCannon>() || Main.npc[a].type == ModContent.NPCType<AresPlasmaFlamethrower>() || Main.npc[a].type == ModContent.NPCType<AresTeslaCannon>() || Main.npc[a].type == ModContent.NPCType<AresGaussNuke>() || Main.npc[a].type == ModContent.NPCType<ThanatosHead>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody1>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody2>() || Main.npc[a].type == ModContent.NPCType<ThanatosTail>())
				{
					Main.npc[a].active = false;
				}
			}
			return;
		}
		if (AIState == 2f || AIState == 3f)
		{
			ChargeComboFlash = MathHelper.Clamp(ChargeComboFlash + 0.08f, 0f, 1f);
		}
		else
		{
			ChargeComboFlash = MathHelper.Clamp(ChargeComboFlash - 0.1f, 0f, 1f);
		}
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
		switch ((int)SecondaryAIState)
		{
		case 0:
			if (otherExoMechsAlive == 0 && !exoMechdusa)
			{
				if (!spawnOtherExoMechs)
				{
					break;
				}
				KillProjectiles();
				if (exoMechTwinRedAlive)
				{
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[2] = 0f;
					if (Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[3] < 1f)
					{
						Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[3] = 1f;
					}
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[1] = 2f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[0] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[3] = 0f;
				}
				if (base.NPC.ai[0] < 10f)
				{
					base.NPC.ai[0] = 10f;
				}
				base.NPC.ai[0]++;
				if (base.NPC.ai[3] < 1f)
				{
					base.NPC.ai[3] = 1f;
				}
				SecondaryAIState = 2f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int n = 0; n < 4; n++)
				{
					chargeLocations[n] = default(Vector2);
				}
				base.NPC.TargetClosest();
				if (draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 1f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
				if (Main.netMode != 1)
				{
					NPC.SpawnOnPlayer(Main.player[targetIndex].whoAmI, ModContent.NPCType<ThanatosHead>());
					NPC.SpawnOnPlayer(Main.player[targetIndex].whoAmI, ModContent.NPCType<AresBody>());
				}
				if (lifeRatio < 0.4f)
				{
					berserkEarlyBugFix = true;
				}
				break;
			}
			if ((anyOtherExoMechPassive || lifeRatio < 0.7f) && !berserk && totalOtherExoMechLifeRatio < 5f)
			{
				if (exoMechTwinRedAlive)
				{
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[1] = 1f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[0] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[3] = 0f;
				}
				SecondaryAIState = 1f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int num6 = 0; num6 < 4; num6++)
				{
					chargeLocations[num6] = default(Vector2);
				}
				base.NPC.TargetClosest();
			}
			if (otherMechIsBerserk && !berserk && !exoMechdusa)
			{
				KillProjectiles();
				if (exoMechTwinRedAlive)
				{
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[1] = 2f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[0] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[3] = 0f;
				}
				if (base.NPC.ai[0] < 10f)
				{
					base.NPC.ai[0] = 10f;
				}
				base.NPC.ai[0]++;
				SecondaryAIState = 2f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int num7 = 0; num7 < 4; num7++)
				{
					chargeLocations[num7] = default(Vector2);
				}
				base.NPC.TargetClosest();
				if (draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 5f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			break;
		case 1:
			if (exoMechTwinRedAlive)
			{
				Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[0] = 0f;
			}
			AIState = 0f;
			if (otherMechIsBerserk && !exoMechdusa)
			{
				KillProjectiles();
				if (exoMechTwinRedAlive)
				{
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[1] = 2f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[0] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[3] = 0f;
				}
				if (base.NPC.ai[0] < 10f)
				{
					base.NPC.ai[0] = 10f;
				}
				base.NPC.ai[0]++;
				SecondaryAIState = 2f;
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int l = 0; l < 4; l++)
				{
					chargeLocations[l] = default(Vector2);
				}
				base.NPC.TargetClosest();
			}
			if (berserk)
			{
				if (exoMechTwinRedAlive)
				{
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[0] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[3] = 0f;
				}
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int m = 0; m < 4; m++)
				{
					chargeLocations[m] = default(Vector2);
				}
				base.NPC.TargetClosest();
				SecondaryAIState = 0f;
				if ((exoWormAlive & exoPrimeAlive) && draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 3f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			break;
		case 2:
			if (exoMechTwinRedAlive)
			{
				Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[0] = 0f;
			}
			AIState = 0f;
			if ((exoWormLifeRatio < 0.7f || exoPrimeLifeRatio < 0.7f || berserkEarlyBugFix) && !otherMechIsBerserk)
			{
				if (exoMechTwinRedAlive)
				{
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[1] = ((totalOtherExoMechLifeRatio > 5f) ? 0f : 1f);
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[0] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[3] = 0f;
				}
				SecondaryAIState = ((totalOtherExoMechLifeRatio > 5f) ? 0f : 1f);
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int j = 0; j < 4; j++)
				{
					chargeLocations[j] = default(Vector2);
				}
				base.NPC.TargetClosest();
				if ((exoWormAlive & exoPrimeAlive) && draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 2f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			if (berserk)
			{
				if (exoMechTwinRedAlive)
				{
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].ai[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[0] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[1] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].localAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[2] = 0f;
					Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[3] = 0f;
				}
				base.NPC.localAI[0] = 0f;
				base.NPC.localAI[1] = 0f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int k = 0; k < 4; k++)
				{
					chargeLocations[k] = default(Vector2);
				}
				base.NPC.TargetClosest();
				SecondaryAIState = 0f;
			}
			break;
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
			CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
			bool attacking = calamityGlobalNPC.newAI[3] >= 2f;
			bool firingPlasma = attacking && calamityGlobalNPC.newAI[3] + 2f < attackPhaseGateValue;
			if (SecondaryAIState != 2f)
			{
				calamityGlobalNPC.newAI[2]++;
			}
			if (!((calamityGlobalNPC.newAI[2] >= 60f) | attacking))
			{
				break;
			}
			if (firingPlasma)
			{
				float divisor = (nerfedAttacks ? 60f : (lastMechAlive ? 40f : 45f));
				if (((calamityGlobalNPC.newAI[3] - 2f) % divisor == 0f) & canFire)
				{
					pickNewLocation = true;
					base.NPC.ai[2]++;
					SoundEngine.PlaySound(in CommonCalamitySounds.ExoPlasmaShootSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int type4 = ModContent.ProjectileType<ApolloFireball>();
						Vector2 plasmaVelocity = Vector2.Normalize(aimedVector) * projectileVelocity;
						Vector2 offset3 = Vector2.Normalize(plasmaVelocity) * 70f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset3, plasmaVelocity, type4, FireballDamage, 0f, Main.myPlayer, Main.player[targetIndex].Center.X, Main.player[targetIndex].Center.Y);
					}
				}
			}
			else
			{
				calamityGlobalNPC.newAI[2] = 0f;
			}
			calamityGlobalNPC.newAI[3]++;
			if (!lineUpAttack)
			{
				break;
			}
			if (SecondaryAIState == 1f)
			{
				pickNewLocation = true;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				for (int num12 = 0; num12 < 4; num12++)
				{
					chargeLocations[num12] = default(Vector2);
				}
				base.NPC.TargetClosest();
				PlayTargetingSound();
			}
			else if (doBigAttack)
			{
				pickNewLocation = base.NPC.localAI[2] == 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				if (phase2)
				{
					AIState = ((base.NPC.localAI[2] == 1f && (!artemisUsingDeathray || Main.zenithWorld)) ? 2f : 1f);
				}
				else
				{
					AIState = 1f;
				}
			}
			break;
		}
		case 1:
			base.NPC.damage = 0;
			CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
			if (canFire || calamityGlobalNPC.newAI[2] > 0f)
			{
				calamityGlobalNPC.newAI[2]++;
			}
			if ((calamityGlobalNPC.newAI[2] % (rocketPhaseDuration / (float)numRockets) == 0f) & canFire)
			{
				SoundEngine.PlaySound(in MissileLaunchSound, base.NPC.Center);
				pickNewLocation = true;
				if (Main.netMode != 1)
				{
					int type2 = ModContent.ProjectileType<ApolloRocket>();
					Vector2 rocketVelocity = Vector2.Normalize(aimedVector) * projectileVelocity * 1.2f;
					Vector2 offset2 = Vector2.Normalize(rocketVelocity) * 70f;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset2, rocketVelocity, type2, RocketDamage, 0f, Main.myPlayer, 0f, Main.player[targetIndex].Center.Y);
				}
			}
			if (calamityGlobalNPC.newAI[2] >= rocketPhaseDuration)
			{
				AIState = 0f;
				base.NPC.localAI[2] = (shouldGetBuffedByBerserkPhase ? 1f : 0f);
				calamityGlobalNPC.newAI[2] = 0f;
				PlayTargetingSound();
				base.NPC.TargetClosest();
			}
			break;
		case 2:
		{
			base.NPC.damage = 0;
			if (!readyToCharge)
			{
				CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
				break;
			}
			int type = ModContent.ProjectileType<ApolloChargeTelegraph>();
			for (int num8 = 0; num8 < 4; num8++)
			{
				if (!(chargeLocations[num8] == default(Vector2)))
				{
					continue;
				}
				switch (num8)
				{
				case 0:
					chargeLocations[num8] = base.NPC.Center;
					break;
				case 1:
					chargeLocations[num8] = chargeLocations[0] + new Vector2(chargeComboXOffset, (0f - chargeComboYOffset) * 2f);
					break;
				case 2:
					chargeLocations[num8] = chargeLocations[1] + new Vector2(chargeComboXOffset, chargeComboYOffset * 2f);
					break;
				case 3:
					chargeLocations[num8] = chargeLocations[2] + new Vector2(chargeComboXOffset, (0f - chargeComboYOffset) * 2f);
					break;
				}
				if (num8 != 0)
				{
					continue;
				}
				if (Main.netMode != 1)
				{
					int telegraph = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), chargeLocations[0], Vector2.Zero, type, 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					if (Main.projectile.IndexInRange(telegraph))
					{
						Main.projectile[telegraph].ModProjectile<ApolloChargeTelegraph>().ChargePositions = chargeLocations;
						Main.projectile[telegraph].netUpdate = true;
					}
				}
				SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, base.NPC.Center);
			}
			base.NPC.velocity = Vector2.Zero;
			SoundStyle style = global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis.ChargeTelegraphSound with
			{
				Volume = 1.6f
			};
			SoundEngine.PlaySound(in style, base.NPC.Center);
			calamityGlobalNPC.newAI[2]++;
			if (calamityGlobalNPC.newAI[2] >= timeToLineUpCharge)
			{
				base.NPC.damage = base.NPC.defDamage;
				ExoMechsSky.CreateLightningBolt(10);
				AIState = 3f;
				base.NPC.localAI[2] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
			}
			break;
		}
		case 3:
		{
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.ai[3] = 61f;
			if (base.NPC.localAI[2] == 0f)
			{
				SoundEngine.PlaySound(in global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis.ChargeSound, base.NPC.Center);
				base.NPC.velocity = Vector2.Normalize(chargeLocations[(int)calamityGlobalNPC.newAI[2] + 1] - chargeLocations[(int)calamityGlobalNPC.newAI[2]]) * chargeVelocity;
				base.NPC.localAI[2] = 1f;
				base.NPC.ForceNetUpdate();
				if (Main.netMode != 1 && (!Main.zenithWorld || exoMechdusa))
				{
					int totalProjectiles = (death ? 12 : 8);
					float radians = (float)Math.PI * 2f / (float)totalProjectiles;
					int type3 = ModContent.ProjectileType<AresPlasmaBolt>();
					float velocity = 0.5f;
					double angleA = (double)radians * 0.5;
					double angleB = (double)MathHelper.ToRadians(90f) - angleA;
					float velocityX2 = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
					Vector2 spinningPoint = (Main.rand.NextBool() ? new Vector2(0f, 0f - velocity) : new Vector2(0f - velocityX2, 0f - velocity));
					for (int num9 = 0; num9 < totalProjectiles; num9++)
					{
						Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)num9);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity2, type3, BoltDamage, 0f, Main.myPlayer);
					}
				}
				for (int num10 = 0; num10 < 200; num10++)
				{
					float dustVelocity = 16f;
					if (num10 < 150)
					{
						dustVelocity = 12f;
					}
					if (num10 < 100)
					{
						dustVelocity = 8f;
					}
					if (num10 < 50)
					{
						dustVelocity = 4f;
					}
					int dust1 = Dust.NewDust(base.NPC.Center, 6, 6, Main.rand.NextBool() ? 107 : 110, 0f, 0f, 100);
					float dustVelX = Main.dust[dust1].velocity.X;
					float dustVelY = Main.dust[dust1].velocity.Y;
					if (dustVelX == 0f && dustVelY == 0f)
					{
						dustVelX = 1f;
					}
					float dustVelocity2 = (float)Math.Sqrt(dustVelX * dustVelX + dustVelY * dustVelY);
					dustVelocity2 = dustVelocity / dustVelocity2;
					dustVelX *= dustVelocity2;
					dustVelY *= dustVelocity2;
					float scale = 1f;
					switch ((int)dustVelocity)
					{
					case 4:
						scale = 1.2f;
						break;
					case 8:
						scale = 1.1f;
						break;
					case 12:
						scale = 1f;
						break;
					case 16:
						scale = 0.9f;
						break;
					}
					Dust obj = Main.dust[dust1];
					obj.velocity *= 0.5f;
					obj.velocity.X += dustVelX;
					obj.velocity.Y += dustVelY;
					obj.scale = scale;
					obj.noGravity = true;
				}
			}
			calamityGlobalNPC.newAI[3]++;
			if (calamityGlobalNPC.newAI[3] >= chargeTime)
			{
				base.NPC.Center = chargeLocations[(int)calamityGlobalNPC.newAI[2] + 1];
				base.NPC.velocity = Vector2.Zero;
				calamityGlobalNPC.newAI[2]++;
				calamityGlobalNPC.newAI[3] = 0f;
				base.NPC.localAI[2] = 0f;
			}
			if (!(calamityGlobalNPC.newAI[2] >= 3f))
			{
				break;
			}
			base.NPC.damage = 0;
			if (Main.zenithWorld && !exoMechdusa)
			{
				pickNewLocation = base.NPC.localAI[2] == 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				AIState = 2f;
			}
			else
			{
				pickNewLocation = true;
				AIState = 0f;
				if (base.NPC.ai[0] < 10f)
				{
					base.NPC.ai[0] = 10f;
				}
				base.NPC.ai[0]++;
			}
			base.NPC.localAI[2] = 0f;
			for (int num11 = 0; num11 < 4; num11++)
			{
				chargeLocations[num11] = default(Vector2);
			}
			ChargeComboFlash = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			base.NPC.ai[2] = Main.rand.Next(2);
			base.NPC.TargetClosest();
			PlayTargetingSound();
			base.NPC.netUpdate = true;
			break;
		}
		case 4:
			base.NPC.damage = 0;
			CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
			if (calamityGlobalNPC.newAI[2] == 48f)
			{
				SoundEngine.PlaySound(in global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis.LensSound, base.NPC.Center);
				Vector2 lensDirection = Vector2.Normalize(aimedVector);
				Vector2 offset = lensDirection * 70f;
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + offset, lensDirection * 24f, ModContent.ProjectileType<BrokenApolloLens>(), 0, 0f);
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
				base.NPC.TargetClosest();
				PlayTargetingSound();
			}
			break;
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
			if (base.NPC.Calamity().newAI[0] != 3f && base.NPC.Calamity().newAI[0] != 2f)
			{
				Vector2 pos = default(Vector2);
				((Vector2)(ref pos))._002Ector(aresin.Center.X - (float)twinoffset - (float)extratwinoffset, aresin.Center.Y - (float)twinheight);
				base.NPC.position = pos;
			}
		}
	}

	public static void PlayTargetingSound()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.draedonExoMechTwinRed >= 0 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].active)
		{
			SoundEngine.PlaySound(in global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis.AttackSelectionSound, Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Center);
		}
		if (CalamityGlobalNPC.draedonExoMechTwinGreen >= 0 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
		{
			SoundEngine.PlaySound(in global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis.AttackSelectionSound, Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Center);
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
		return MathHelper.SmoothStep(21f, 8f, completionRatio) * ChargeComboFlash;
	}

	public float FlameTrailWidthFunctionBig(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.SmoothStep(34f, 12f, completionRatio) * ChargeComboFlash;
	}

	public static float RibbonTrailWidthFunction(float completionRatio, Vector2 vertexPos)
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
		Color middleColor = Color.Lerp(Color.Orange, Color.ForestGreen, 0.74f);
		Color endColor = Color.Lime;
		return CalamityUtils.MulticolorLerp(completionRatio, startingColor, middleColor, endColor) * ChargeComboFlash * trailOpacity;
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
		Color color = CalamityUtils.MulticolorLerp(completionRatio, startingColor, middleColor, endColor) * ChargeComboFlash * trailOpacity;
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
		((Color)(ref endColor))._002Ector(40, 160, 32);
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
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		int numAfterimages = ((!(ChargeComboFlash > 0f)) ? 5 : 0);
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
		int instanceCount = (int)MathHelper.Lerp(1f, 15f, ChargeComboFlash);
		Color baseInstanceColor = Color.Lerp(drawColor, Color.White, ChargeComboFlash);
		((Color)(ref baseInstanceColor)).A = (byte)(int)(255f - ChargeComboFlash * 255f);
		if (!base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.EnterShaderRegion();
		}
		drawInstance(Vector2.Zero, baseInstanceColor);
		if (instanceCount > 1)
		{
			baseInstanceColor *= 0.04f;
			float backAfterimageOffset = MathHelper.SmoothStep(0f, 2f, ChargeComboFlash);
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
				spriteBatch.Draw(texture, afterimageCenter, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
			}
		}
		spriteBatch.Draw(texture, center, (Rectangle?)frame, Color.White * base.NPC.Opacity, base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
		if (!base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.ExitShaderRegion();
		}
		if (ChargeComboFlash > 0f && !base.NPC.IsABestiaryIconDummy)
		{
			for (int l = -1; l <= 1; l++)
			{
				Vector2 baseDrawOffset = Utils.RotatedBy(new Vector2(0f, ((float)l == 0f) ? 18f : 60f), (double)base.NPC.rotation, default(Vector2));
				baseDrawOffset += Utils.RotatedBy(new Vector2((float)l * 64f, 0f), (double)base.NPC.rotation, default(Vector2));
				float backFlameLength = (((float)l == 0f) ? 700f : 190f);
				Vector2 drawStart = base.NPC.Center + baseDrawOffset;
				Vector2 drawEnd = drawStart - (base.NPC.rotation - (float)Math.PI / 2f).ToRotationVector2() * ChargeComboFlash * backFlameLength;
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
			//IL_0199: Unknown result type (might be due to invalid IL or missing references)
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_019f: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
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
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
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
					spriteBatch.Draw(texture, afterimageCenter2, (Rectangle?)base.NPC.frame, afterimageColor2, base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
				}
			}
			spriteBatch.Draw(texture, center + val, (Rectangle?)frame, base.NPC.GetAlpha(baseColor), base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
		}
	}

	public override bool SpecialOnKill()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>(), ModContent.NPCType<Apollo>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<OmegaHealingPotion>();
	}

	public override void OnKill()
	{
		bool exoWormAlive = false;
		bool exoPrimeAlive = false;
		if (CalamityGlobalNPC.draedonExoMechWorm != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechWorm].active)
		{
			exoWormAlive = true;
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
		if (exoWormAlive & exoPrimeAlive)
		{
			if (draedonAlive)
			{
				Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 4f;
				Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
			}
		}
		else if (exoWormAlive | exoPrimeAlive)
		{
			if (draedonAlive)
			{
				Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 6f;
				Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
			}
		}
		else
		{
			AresBody.DoMiscDeathEffects(base.NPC, AresBody.MechType.ArtemisAndApollo);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		AresBody.DefineExoMechLoot(base.NPC, npcLoot, 2);
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
			for (int num193 = 0; num193 < 2; num193++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 107, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
			}
			for (int i = 0; i < 20; i++)
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Apollo1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Apollo2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Apollo3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Apollo4").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Apollo5").Type);
			}
		}
	}

	public override bool CheckDead()
	{
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC nPC = enumerator.Current;
			if (nPC.type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>() && nPC.life > 0)
			{
				nPC.life = 0;
				nPC.HitEffect();
				nPC.checkDead();
				nPC.active = false;
				nPC.netUpdate = true;
			}
		}
		KillProjectiles();
		return true;
	}

	private void KillProjectiles()
	{
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile = Main.projectile[x];
			if (projectile.active && (projectile.type == ModContent.ProjectileType<ArtemisLaser>() || projectile.type == ModContent.ProjectileType<ArtemisChargeTelegraph>() || projectile.type == ModContent.ProjectileType<ApolloFireball>() || projectile.type == ModContent.ProjectileType<ApolloRocket>()))
			{
				projectile.ai[2] = -1f;
				projectile.Kill();
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
