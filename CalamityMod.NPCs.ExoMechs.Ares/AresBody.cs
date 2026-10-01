using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Mounts;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.Tools.SpawnBlocker;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
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
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ExoMechs.Ares;

[AutoloadBossHead]
[HasPierceResist(false)]
public class AresBody : ModNPC
{
	public enum MechType
	{
		Ares,
		Thanatos,
		ArtemisAndApollo
	}

	public enum Phase
	{
		Normal,
		Deathrays
	}

	public enum SecondaryPhase
	{
		Nothing,
		Passive,
		PassiveAndImmune
	}

	public enum Enraged
	{
		No,
		Yes
	}

	public ThanatosSmokeParticleSet SmokeDrawer = new ThanatosSmokeParticleSet(-1, 3, 0f, 16f, 1.5f);

	public SlotId DeathraySoundSlot;

	public const int ventCloudSpawnRate = 3;

	public const int telegraphParticlesSpawnRate = 5;

	private const int maxFramesX = 6;

	private const int maxFramesY = 8;

	private int frameX;

	private int frameY;

	private const int normalFrameLimit = 11;

	private const int firstStageDeathrayChargeFrameLimit = 23;

	private const int secondStageDeathrayChargeFrameLimit = 35;

	private const int finalStageDeathrayChargeFrameLimit = 47;

	private const float defaultLifeRatio = 5f;

	private bool armsSpawned;

	public bool berserkEarlyBugFix;

	public bool exoMechdusa;

	public int neurontimer;

	public const float deathrayTelegraphDuration_Normal = 150f;

	public const float deathrayTelegraphDuration_Expert = 120f;

	public const float deathrayTelegraphDuration_Rev = 105f;

	public const float deathrayTelegraphDuration_Death = 90f;

	public const float deathrayDuration = 600f;

	private const float soundDistance = 4800f;

	private const float DeathrayEnrageDistance = 2480f;

	public const float plasmaArmStartTimer = 260f;

	public const float teslaArmStartTimer = 80f;

	public static readonly SoundStyle EnragedSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresEnraged");

	public static readonly SoundStyle LaserStartSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresCircleLaserStart");

	public static readonly SoundStyle LaserLoopSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresCircleLaserLoop")
	{
		IsLooped = true
	};

	public static readonly SoundStyle LaserEndSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresCircleLaserEnd");

	public static Asset<Texture2D> GlowTexture;

	public static Asset<Texture2D> NeuronTexture;

	public static Asset<Texture2D> NeuronTexture_Glow;

	public static Asset<Texture2D> ArmTopTexture;

	public static Asset<Texture2D> ArmTopTexture2;

	public static Asset<Texture2D> ArmSegmentTexture;

	public static Asset<Texture2D> ArmTopShoulderTexture;

	public static Asset<Texture2D> ArmBottomConnectorTexture;

	public static Asset<Texture2D> ArmBottomTexture;

	public static Asset<Texture2D> ArmBottomTexture2;

	public static Asset<Texture2D> ArmBottomShoulderTexture;

	public static Asset<Texture2D> ArmTopTexture2_Glow;

	public static Asset<Texture2D> ArmSegmentTexture_Glow;

	public static Asset<Texture2D> ArmTopShoulderTexture_Glow;

	public static Asset<Texture2D> ArmBottomTexture_Glow;

	public static Asset<Texture2D> ArmBottomTexture2_Glow;

	public static Asset<Texture2D> ArmBottomShoulderTexture_Glow;

	public static int BlenderDamage = 105;

	public static int NeuronLaserDamage = 80;

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

	public float EnragedState
	{
		get
		{
			return base.NPC.localAI[1];
		}
		set
		{
			base.NPC.localAI[1] = value;
		}
	}

	public float VelocityBoostMult
	{
		get
		{
			return base.NPC.localAI[2];
		}
		set
		{
			base.NPC.localAI[2] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.TrailingMode[base.Type] = 3;
		NPCID.Sets.TrailCacheLength[base.Type] = base.NPC.oldPos.Length;
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitScale = 0.54f;
		nPCBestiaryDrawModifiers.Scale = 0.4f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			NeuronTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/AergiaNeuron", (AssetRequestMode)2);
			NeuronTexture_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/AergiaNeuron_Glow", (AssetRequestMode)2);
			ArmTopTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "ArmTopPart1", (AssetRequestMode)2);
			ArmTopTexture2 = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "ArmTopPart2", (AssetRequestMode)2);
			ArmSegmentTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "ArmTopSegment", (AssetRequestMode)2);
			ArmTopShoulderTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "ArmTopShoulder", (AssetRequestMode)2);
			ArmBottomConnectorTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "BottomArmConnector", (AssetRequestMode)2);
			ArmBottomTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "BottomArmPart1", (AssetRequestMode)2);
			ArmBottomTexture2 = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "BottomArmPart2", (AssetRequestMode)2);
			ArmBottomShoulderTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "BottomArmShoulder", (AssetRequestMode)2);
			ArmTopTexture2_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "ArmTopPart2Glow", (AssetRequestMode)2);
			ArmSegmentTexture_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "ArmTopSegmentGlow", (AssetRequestMode)2);
			ArmTopShoulderTexture_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "ArmTopShoulderGlow", (AssetRequestMode)2);
			ArmBottomTexture_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "BottomArmPart1Glow", (AssetRequestMode)2);
			ArmBottomTexture2_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "BottomArmPart2Glow", (AssetRequestMode)2);
			ArmBottomShoulderTexture_Glow = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/Ares/Ares" + "BottomArmShoulderGlow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 220;
		base.NPC.height = 252;
		base.NPC.defense = 100;
		base.NPC.DR_NERD(0.35f);
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
		base.NPC.BossBar = ModContent.GetInstance<ExoMechsBossBar>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Ares")
		});
	}

	public override void BossHeadSlot(ref int index)
	{
		if (SecondaryAIState == 2f)
		{
			index = -1;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(frameX);
		writer.Write(frameY);
		writer.Write(armsSpawned);
		writer.Write(exoMechdusa);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
		writer.Write(neurontimer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		frameX = reader.ReadInt32();
		frameY = reader.ReadInt32();
		armsSpawned = reader.ReadBoolean();
		exoMechdusa = reader.ReadBoolean();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
		neurontimer = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_090b: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_092b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_103c: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1673: Unknown result type (might be due to invalid IL or missing references)
		//IL_1675: Unknown result type (might be due to invalid IL or missing references)
		//IL_167c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1681: Unknown result type (might be due to invalid IL or missing references)
		//IL_1689: Unknown result type (might be due to invalid IL or missing references)
		//IL_1632: Unknown result type (might be due to invalid IL or missing references)
		//IL_163d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1742: Unknown result type (might be due to invalid IL or missing references)
		//IL_1749: Unknown result type (might be due to invalid IL or missing references)
		//IL_174e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1714: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16db: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1701: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b54: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1abe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac3: Unknown result type (might be due to invalid IL or missing references)
		//IL_199f: Unknown result type (might be due to invalid IL or missing references)
		//IL_19aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1add: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_19dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c03: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.draedonExoMechPrime = base.NPC.whoAmI;
		base.NPC.frame = new Rectangle(base.NPC.width * frameX, base.NPC.height * frameY, base.NPC.width, base.NPC.height);
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (Main.netMode != 1 && !armsSpawned && base.NPC.ai[0] == 0f)
		{
			int totalArms = 4;
			int Previous = base.NPC.whoAmI;
			for (int i = 0; i < totalArms; i++)
			{
				int lol = 0;
				switch (i)
				{
				case 0:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<AresLaserCannon>(), base.NPC.whoAmI);
					Main.npc[lol].localAI[0] = Main.rand.Next(240);
					break;
				case 1:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<AresPlasmaFlamethrower>(), base.NPC.whoAmI);
					Main.npc[lol].Calamity().newAI[3] = Main.rand.Next(240);
					Main.npc[lol].Calamity().newAI[1] = 260f;
					break;
				case 2:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<AresTeslaCannon>(), base.NPC.whoAmI);
					Main.npc[lol].localAI[0] = Main.rand.Next(240);
					Main.npc[lol].Calamity().newAI[1] = 80f;
					break;
				case 3:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<AresGaussNuke>(), base.NPC.whoAmI);
					Main.npc[lol].Calamity().newAI[3] = Main.rand.Next(240);
					break;
				}
				Main.npc[lol].realLife = base.NPC.whoAmI;
				Main.npc[lol].ai[2] = base.NPC.whoAmI;
				Main.npc[lol].ai[1] = Previous;
				Main.npc[Previous].ai[0] = lol;
				NetMessage.SendData(23, -1, -1, null, lol);
				Previous = lol;
			}
			if (exoMechdusa)
			{
				CalamityUtils.SpawnBossBetter(base.NPC.Center, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>()).ModNPC<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>().exoMechdusa = true;
				CalamityUtils.SpawnBossBetter(base.NPC.Center, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>()).ModNPC<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>().exoMechdusa = true;
				CalamityUtils.SpawnBossBetter(base.NPC.Center, ModContent.NPCType<ThanatosHead>()).ModNPC<ThanatosHead>().exoMechdusa = true;
			}
			armsSpawned = true;
		}
		if (exoMechdusa)
		{
			int yoffset = 180;
			int xoffset = 180;
			Vector2 NeuronRight = default(Vector2);
			((Vector2)(ref NeuronRight))._002Ector(base.NPC.Center.X + (float)xoffset, base.NPC.Center.Y + (float)yoffset);
			Vector2 NeuronLeft = default(Vector2);
			((Vector2)(ref NeuronLeft))._002Ector(base.NPC.Center.X - (float)xoffset, base.NPC.Center.Y + (float)yoffset);
			base.NPC.alpha = 0;
			base.NPC.dontTakeDamage = true;
			neurontimer++;
			if (Main.netMode != 1 && neurontimer >= 180)
			{
				float variance = (float)Math.PI / 3f;
				Vector2 velocity = default(Vector2);
				for (int j = 0; j < 6; j++)
				{
					((Vector2)(ref velocity))._002Ector(0f, 6f);
					velocity = velocity.RotatedBy(variance * (float)j);
					((Vector2)(ref velocity)).Normalize();
					Vector2 betweenR = NeuronRight + velocity * 650f;
					Vector2 betweenL = NeuronLeft + velocity * 650f;
					SoundEngine.PlaySound(CommonCalamitySounds.LaserCannonSound with
					{
						Volume = CommonCalamitySounds.LaserCannonSound.Volume - 0.2f,
						Pitch = CommonCalamitySounds.LaserCannonSound.Pitch + 0.2f
					}, NeuronRight);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), betweenL, betweenL + velocity, ModContent.ProjectileType<ArtemisLaser>(), NeuronLaserDamage, 0f, Main.myPlayer, 7f, base.NPC.whoAmI);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), betweenR, betweenR + velocity, ModContent.ProjectileType<ArtemisLaser>(), NeuronLaserDamage, 0f, Main.myPlayer, 7f, base.NPC.whoAmI);
				}
				neurontimer = 0;
			}
			if (NPC.CountNPCS(ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>()) > 0 || NPC.CountNPCS(ModContent.NPCType<ThanatosHead>()) > 0)
			{
				base.NPC.TargetClosest();
				Vector2 where2 = default(Vector2);
				((Vector2)(ref where2))._002Ector(Main.player[base.NPC.target].Center.X + 600f, Main.player[base.NPC.target].Center.Y - 200f);
				if (CalamityGlobalNPC.draedonExoMechWorm != -1)
				{
					if (Main.npc[CalamityGlobalNPC.draedonExoMechWorm].Calamity().newAI[0] == 2f)
					{
						((Vector2)(ref where2))._002Ector(Main.npc[CalamityGlobalNPC.draedonExoMechWorm].position.X, Main.npc[CalamityGlobalNPC.draedonExoMechWorm].position.Y - 40f);
						base.NPC.position = where2;
					}
					else
					{
						CalamityUtils.SmoothMovement(base.NPC, 100f, where2 - base.NPC.Center, 8f, 1.4f, useSimpleFlyMovement: true);
					}
				}
				else if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1)
				{
					if (Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].Calamity().newAI[0] == 3f)
					{
						((Vector2)(ref where2))._002Ector(Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].position.X, Main.npc[CalamityGlobalNPC.draedonExoMechTwinRed].position.Y);
						base.NPC.position = where2;
					}
					else if (Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[0] == 3f || Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[0] == 2f)
					{
						((Vector2)(ref where2))._002Ector(Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].position.X, Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].position.Y);
						base.NPC.position = where2;
					}
					else
					{
						CalamityUtils.SmoothMovement(base.NPC, 100f, where2 - base.NPC.Center, 8f, 1.4f, useSimpleFlyMovement: true);
					}
				}
				else
				{
					CalamityUtils.SmoothMovement(base.NPC, 100f, where2 - base.NPC.Center, 8f, 1.4f, useSimpleFlyMovement: true);
				}
				return;
			}
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[0]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[0]].life;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		int otherExoMechsAlive = 0;
		bool exoWormAlive = false;
		bool exoTwinsAlive = false;
		if (CalamityGlobalNPC.draedonExoMechWorm != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechWorm].active)
		{
			otherExoMechsAlive++;
			exoWormAlive = true;
			if ((Main.npc[CalamityGlobalNPC.draedonExoMechWorm].ModNPC as ThanatosHead).berserkEarlyBugFix)
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
		float exoWormLifeRatio = 5f;
		float exoTwinsLifeRatio = 5f;
		if (exoWormAlive)
		{
			exoWormLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechWorm].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechWorm].lifeMax;
		}
		if (exoTwinsAlive)
		{
			exoTwinsLifeRatio = (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].lifeMax;
		}
		float totalOtherExoMechLifeRatio = exoWormLifeRatio + exoTwinsLifeRatio;
		bool exoWormPassive = false;
		bool exoTwinsPassive = false;
		if (exoWormAlive)
		{
			exoWormPassive = Main.npc[CalamityGlobalNPC.draedonExoMechWorm].Calamity().newAI[1] == 1f;
		}
		if (exoTwinsAlive)
		{
			exoTwinsPassive = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].Calamity().newAI[1] == 1f;
		}
		bool anyOtherExoMechPassive = exoWormPassive | exoTwinsPassive;
		bool exoWormWasFirst = false;
		bool exoTwinsWereFirst = false;
		if (exoWormAlive)
		{
			exoWormWasFirst = Main.npc[CalamityGlobalNPC.draedonExoMechWorm].ai[3] == 1f;
		}
		if (exoTwinsAlive)
		{
			exoTwinsWereFirst = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].ai[3] == 1f;
		}
		bool draedonAlive = false;
		if (CalamityGlobalNPC.draedon != -1 && Main.npc[CalamityGlobalNPC.draedon].active)
		{
			draedonAlive = true;
		}
		bool spawnOtherExoMechs = lifeRatio < 0.7f && base.NPC.ai[3] == 0f;
		bool berserk = lifeRatio < 0.4f || (otherExoMechsAlive == 0 && lifeRatio < 0.7f);
		bool lastMechAlive = berserk && otherExoMechsAlive == 0;
		bool otherMechIsBerserk = exoWormLifeRatio < 0.4f || exoTwinsLifeRatio < 0.4f;
		bool shouldGetBuffedByBerserkPhase = berserk && !otherMechIsBerserk;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		base.NPC.rotation = base.NPC.velocity.X * 0.003f;
		if (EnragedState == 1f)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
		}
		if (player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (player.dead)
			{
				AIState = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
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
					if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>() || Main.npc[a].type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>() || Main.npc[a].type == ModContent.NPCType<AresLaserCannon>() || Main.npc[a].type == ModContent.NPCType<AresPlasmaFlamethrower>() || Main.npc[a].type == ModContent.NPCType<AresTeslaCannon>() || Main.npc[a].type == ModContent.NPCType<AresGaussNuke>() || Main.npc[a].type == ModContent.NPCType<ThanatosHead>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody1>() || Main.npc[a].type == ModContent.NPCType<ThanatosBody2>() || Main.npc[a].type == ModContent.NPCType<ThanatosTail>())
					{
						Main.npc[a].active = false;
					}
				}
				return;
			}
		}
		_003F val = ((SecondaryAIState == 2f) ? new Vector2(player.Center.X, player.Center.Y - 800f) : ((AIState != 1f) ? new Vector2(player.Center.X, player.Center.Y - 425f) : player.Center));
		float baseVelocityMult = (shouldGetBuffedByBerserkPhase ? 0.25f : 0f) + (death ? 1.1f : (revenge ? 1.075f : (expertMode ? 1.05f : 1f)));
		float baseVelocity = ((EnragedState == 1f) ? 22f : 14f) * baseVelocityMult;
		float baseAcceleration = (shouldGetBuffedByBerserkPhase ? 1.25f : 1f);
		float decelerationVelocityMult = 0.85f;
		float movementDistanceGateValue = 50f;
		Vector2 distanceFromDestination = (Vector2)val - base.NPC.Center;
		if (((Vector2)(ref distanceFromDestination)).Length() > movementDistanceGateValue && AIState != 1f)
		{
			if (VelocityBoostMult < 1f)
			{
				VelocityBoostMult += 0.004f;
			}
		}
		else if (VelocityBoostMult > 0f)
		{
			VelocityBoostMult -= 0.004f;
		}
		baseVelocity *= 1f + VelocityBoostMult;
		float distanceFromTarget = Vector2.Distance(base.NPC.Center, player.Center);
		float deathrayPhaseGateValue = (lastMechAlive ? 420f : 600f);
		float deathrayDistanceGateValue = 480f;
		if (EnragedState == 1f)
		{
			deathrayPhaseGateValue *= 0.75f;
		}
		SmokeDrawer.ParticleSpawnRate = 9999999;
		if (EnragedState == 1f)
		{
			SmokeDrawer.ParticleSpawnRate = 3;
			SmokeDrawer.BaseMoveRotation = base.NPC.rotation + (float)Math.PI / 2f;
			SmokeDrawer.SpawnAreaCompactness = 80f;
			base.NPC.Calamity().DR = 0.85f;
		}
		else
		{
			base.NPC.Calamity().DR = 0.35f;
		}
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = EnragedState == 1f;
		SmokeDrawer.Update();
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
					base.NPC.TargetClosest();
					if (draedonAlive)
					{
						Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 1f;
						Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
					}
					if (Main.netMode != 1)
					{
						NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<ThanatosHead>());
						NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>());
						NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>());
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
				base.NPC.TargetClosest();
			}
			if (otherMechIsBerserk && !berserk)
			{
				if (base.NPC.ai[3] < 2f)
				{
					base.NPC.ai[3] = 2f;
				}
				SecondaryAIState = 2f;
				base.NPC.TargetClosest();
				if (draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 5f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			break;
		case 1:
			if (otherMechIsBerserk)
			{
				if (base.NPC.ai[3] < 2f)
				{
					base.NPC.ai[3] = 2f;
				}
				SecondaryAIState = 2f;
				base.NPC.TargetClosest();
			}
			if (berserk)
			{
				base.NPC.TargetClosest();
				SecondaryAIState = 0f;
				if ((exoWormAlive & exoTwinsAlive) && draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 3f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			break;
		case 2:
			if ((exoWormLifeRatio < 0.7f || exoTwinsLifeRatio < 0.7f || berserkEarlyBugFix) && !otherMechIsBerserk)
			{
				SecondaryAIState = ((totalOtherExoMechLifeRatio > 5f) ? 0f : 1f);
				base.NPC.TargetClosest();
				if ((exoWormAlive & exoTwinsAlive) && draedonAlive)
				{
					Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 2f;
					Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
				}
			}
			if (berserk)
			{
				base.NPC.TargetClosest();
				SecondaryAIState = 0f;
			}
			break;
		}
		bool invisiblePhase = SecondaryAIState == 2f;
		base.NPC.dontTakeDamage = invisiblePhase;
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
			CalamityUtils.SmoothMovement(base.NPC, movementDistanceGateValue, distanceFromDestination, baseVelocity, 0f, useSimpleFlyMovement: false);
			if (!shouldGetBuffedByBerserkPhase)
			{
				break;
			}
			calamityGlobalNPC.newAI[2]++;
			if (!(calamityGlobalNPC.newAI[2] > deathrayPhaseGateValue))
			{
				break;
			}
			for (int n = 0; n < Main.maxProjectiles; n++)
			{
				Projectile projectile3 = Main.projectile[n];
				if (!projectile3.active)
				{
					continue;
				}
				if (projectile3.type == ModContent.ProjectileType<AresTeslaOrb>() || projectile3.type == ModContent.ProjectileType<AresPlasmaFireball>() || projectile3.type == ModContent.ProjectileType<AresPlasmaBolt>() || projectile3.type == ModContent.ProjectileType<AresGaussNukeProjectile>() || projectile3.type == ModContent.ProjectileType<AresGaussNukeProjectileSpark>())
				{
					if (projectile3.timeLeft > 15)
					{
						projectile3.timeLeft = 15;
					}
					if (projectile3.type == ModContent.ProjectileType<AresPlasmaFireball>())
					{
						projectile3.ai[0] = -1f;
						projectile3.ai[1] = -1f;
					}
					else if (projectile3.type == ModContent.ProjectileType<AresGaussNukeProjectile>())
					{
						projectile3.ai[0] = -1f;
					}
				}
				else if (projectile3.type == ModContent.ProjectileType<AresGaussNukeProjectileBoom>())
				{
					projectile3.Kill();
				}
			}
			calamityGlobalNPC.newAI[2] = 0f;
			AIState = 1f;
			if (EnragedState == 1f)
			{
				EnragedState = 0f;
			}
			break;
		}
		case 1:
		{
			if (!Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2480f)
			{
				Main.LocalPlayer.Calamity().infiniteFlight = true;
			}
			if (distanceFromTarget > deathrayDistanceGateValue && calamityGlobalNPC.newAI[3] == 0f)
			{
				Vector2 desiredVelocity2 = Vector2.Normalize(distanceFromDestination) * baseVelocity;
				base.NPC.SimpleFlyMovement(desiredVelocity2, baseAcceleration);
				break;
			}
			if ((distanceFromTarget > 2480f || Main.zenithWorld) && EnragedState == 0f)
			{
				if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 4800f)
				{
					SoundEngine.PlaySound(in EnragedSound, Main.LocalPlayer.Center);
				}
				if (Main.netMode != 1)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonAresEnrageText", Draedon.TextColor);
				}
				EnragedState = 1f;
			}
			calamityGlobalNPC.newAI[3] = 1f;
			NPC nPC = base.NPC;
			nPC.velocity *= decelerationVelocityMult;
			int totalProjectiles = (death ? 10 : (revenge ? 9 : (expertMode ? 8 : 6)));
			if (Main.getGoodWorld)
			{
				totalProjectiles += 4;
			}
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			bool num = base.NPC.localAI[0] % 2f == 0f;
			float velocity2 = 6f;
			double angleA = (double)radians * 0.5;
			double angleB = (double)MathHelper.ToRadians(90f) - angleA;
			float velocityX2 = (float)((double)velocity2 * Math.Sin(angleA) / Math.Sin(angleB));
			Vector2 spinningPoint = (num ? new Vector2(0f, 0f - velocity2) : new Vector2(0f - velocityX2, 0f - velocity2));
			((Vector2)(ref spinningPoint)).Normalize();
			float deathrayTelegraphDuration = (death ? 90f : (revenge ? 105f : (expertMode ? 120f : 150f)));
			calamityGlobalNPC.newAI[2] += ((EnragedState == 1f && calamityGlobalNPC.newAI[2] % 2f == 0f) ? 2f : 1f);
			if (calamityGlobalNPC.newAI[2] < deathrayTelegraphDuration)
			{
				if (calamityGlobalNPC.newAI[2] == 1f)
				{
					for (int x = 0; x < Main.maxProjectiles; x++)
					{
						Projectile projectile = Main.projectile[x];
						if (!projectile.active)
						{
							continue;
						}
						if (projectile.type == ModContent.ProjectileType<AresTeslaOrb>() || projectile.type == ModContent.ProjectileType<AresPlasmaFireball>() || projectile.type == ModContent.ProjectileType<AresPlasmaBolt>() || projectile.type == ModContent.ProjectileType<AresGaussNukeProjectile>() || projectile.type == ModContent.ProjectileType<AresGaussNukeProjectileSpark>())
						{
							if (projectile.timeLeft > 15)
							{
								projectile.timeLeft = 15;
							}
							if (projectile.type == ModContent.ProjectileType<AresPlasmaFireball>())
							{
								projectile.ai[0] = -1f;
								projectile.ai[1] = -1f;
							}
							else if (projectile.type == ModContent.ProjectileType<AresGaussNukeProjectile>())
							{
								projectile.ai[0] = -1f;
							}
						}
						else if (projectile.type == ModContent.ProjectileType<AresGaussNukeProjectileBoom>())
						{
							projectile.Kill();
						}
					}
					base.NPC.frameCounter = 0.0;
					frameX = 1;
					frameY = 4;
					ExoMechsSky.CreateLightningBolt(12);
					SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int type = ModContent.ProjectileType<AresDeathBeamTelegraph>();
						Vector2 spawnPoint = base.NPC.Center + new Vector2(-1f, 23f);
						for (int k = 0; k < totalProjectiles; k++)
						{
							Vector2 laserVelocity = spinningPoint.RotatedBy(radians * (float)k);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPoint + Vector2.Normalize(laserVelocity) * 17f, laserVelocity, type, 0, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
						}
					}
				}
			}
			else if (calamityGlobalNPC.newAI[2] == deathrayTelegraphDuration)
			{
				DeathraySoundSlot = SoundEngine.PlaySound(in LaserStartSound, base.NPC.Center);
				if (Main.netMode != 1)
				{
					int type2 = ModContent.ProjectileType<AresDeathBeamStart>();
					Vector2 spawnPoint2 = base.NPC.Center + new Vector2(-1f, 23f);
					for (int l = 0; l < totalProjectiles; l++)
					{
						Vector2 laserVelocity2 = spinningPoint.RotatedBy(radians * (float)l);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPoint2 + Vector2.Normalize(laserVelocity2) * 35f, laserVelocity2, type2, BlenderDamage, 0f, Main.myPlayer, 0f, base.NPC.whoAmI);
					}
				}
			}
			float stopSound = deathrayTelegraphDuration + 600f - 160f;
			if (SoundEngine.TryGetActiveSound(DeathraySoundSlot, out ActiveSound deathraySound) && deathraySound.IsPlaying && calamityGlobalNPC.newAI[2] < stopSound)
			{
				deathraySound.Position = base.NPC.Center;
			}
			if (calamityGlobalNPC.newAI[2] >= deathrayTelegraphDuration && calamityGlobalNPC.newAI[2] < stopSound && (deathraySound == null || !deathraySound.IsPlaying || calamityGlobalNPC.newAI[2] == deathrayTelegraphDuration + 180f))
			{
				if (deathraySound == null || deathraySound.Style == LaserStartSound)
				{
					deathraySound?.Stop();
					DeathraySoundSlot = SoundEngine.PlaySound(in LaserLoopSound, base.NPC.Center);
				}
				else
				{
					deathraySound?.Resume();
				}
			}
			if (calamityGlobalNPC.newAI[2] == stopSound)
			{
				deathraySound?.Stop();
				SoundEngine.PlaySound(in LaserEndSound, base.NPC.Center);
			}
			if (!(calamityGlobalNPC.newAI[2] >= deathrayTelegraphDuration + 600f))
			{
				break;
			}
			if (!Main.zenithWorld || exoMechdusa)
			{
				AIState = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				if (revenge)
				{
					base.NPC.ai[3] += 1f + (float)Main.rand.Next(2);
					if (base.NPC.ai[3] > 5f)
					{
						base.NPC.ai[3] -= 4f;
					}
				}
				else if (expertMode)
				{
					base.NPC.ai[3] += Main.rand.Next(2);
					if (base.NPC.ai[3] > 3f)
					{
						base.NPC.ai[3] -= 2f;
					}
				}
			}
			else
			{
				for (int m = 0; m < Main.maxProjectiles; m++)
				{
					Projectile projectile2 = Main.projectile[m];
					if (!projectile2.active)
					{
						continue;
					}
					if (projectile2.type == ModContent.ProjectileType<AresTeslaOrb>() || projectile2.type == ModContent.ProjectileType<AresPlasmaFireball>() || projectile2.type == ModContent.ProjectileType<AresPlasmaBolt>() || projectile2.type == ModContent.ProjectileType<AresGaussNukeProjectile>() || projectile2.type == ModContent.ProjectileType<AresGaussNukeProjectileSpark>())
					{
						if (projectile2.timeLeft > 15)
						{
							projectile2.timeLeft = 15;
						}
						if (projectile2.type == ModContent.ProjectileType<AresPlasmaFireball>())
						{
							projectile2.ai[0] = -1f;
							projectile2.ai[1] = -1f;
						}
						else if (projectile2.type == ModContent.ProjectileType<AresGaussNukeProjectile>())
						{
							projectile2.ai[0] = -1f;
						}
					}
					else if (projectile2.type == ModContent.ProjectileType<AresGaussNukeProjectileBoom>())
					{
						projectile2.Kill();
					}
				}
				calamityGlobalNPC.newAI[2] = 0f;
				calamityGlobalNPC.newAI[3] = 0f;
				if (EnragedState == 1f)
				{
					EnragedState = 0f;
				}
			}
			base.NPC.localAI[0]++;
			base.NPC.TargetClosest();
			base.NPC.netUpdate = true;
			break;
		}
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 2f;
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		base.NPC.frameCounter++;
		if ((AIState == 0f || base.NPC.Calamity().newAI[3] == 0f) && !base.NPC.IsABestiaryIconDummy)
		{
			if (base.NPC.frameCounter >= 6.0)
			{
				base.NPC.frameCounter = 0.0;
				frameY++;
				if (frameY == 8)
				{
					frameX++;
					frameY = 0;
				}
				if (frameX * 8 + frameY > 11)
				{
					frameX = (frameY = 0);
				}
			}
		}
		else if (base.NPC.frameCounter >= 6.0)
		{
			base.NPC.frameCounter = 0.0;
			frameY++;
			if (frameY == 8)
			{
				frameX++;
				frameY = 0;
			}
			if (frameX * 8 + frameY > 47)
			{
				frameX = (frameY = 4);
			}
		}
		base.NPC.frame = new Rectangle(base.NPC.width * frameX, base.NPC.height * frameY, base.NPC.width, base.NPC.height);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_082d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		SmokeDrawer.DrawSet(base.NPC.Center);
		int laserArm = NPC.FindFirstNPC(ModContent.NPCType<AresLaserCannon>());
		int gaussArm = NPC.FindFirstNPC(ModContent.NPCType<AresGaussNuke>());
		int teslaArm = NPC.FindFirstNPC(ModContent.NPCType<AresTeslaCannon>());
		int plasmaArm = NPC.FindFirstNPC(ModContent.NPCType<AresPlasmaFlamethrower>());
		Color afterimageBaseColor = ((EnragedState == 1f) ? Color.Red : Color.White);
		Color armGlowmaskColor = afterimageBaseColor;
		((Color)(ref armGlowmaskColor)).A = 184;
		(int, bool)[] armProperties = new(int, bool)[4]
		{
			(-1, true),
			(1, true),
			(-1, false),
			(1, false)
		};
		switch ((int)base.NPC.ai[3])
		{
		case 0:
			if (AIState == 1f)
			{
				CalamityUtils.SwapArrayIndices(ref armProperties, 1, 3);
				CalamityUtils.SwapArrayIndices(ref armProperties, 0, 1);
			}
			break;
		case 1:
			CalamityUtils.SwapArrayIndices(ref armProperties, 0, 1);
			if (AIState == 1f)
			{
				CalamityUtils.SwapArrayIndices(ref armProperties, 0, 3);
			}
			break;
		case 2:
			if (AIState != 1f)
			{
				CalamityUtils.SwapArrayIndices(ref armProperties, 0, 1);
				CalamityUtils.SwapArrayIndices(ref armProperties, 2, 3);
			}
			else
			{
				CalamityUtils.SwapArrayIndices(ref armProperties, 0, 1);
				CalamityUtils.SwapArrayIndices(ref armProperties, 2, 3);
				CalamityUtils.SwapArrayIndices(ref armProperties, 0, 2);
			}
			break;
		case 3:
			CalamityUtils.SwapArrayIndices(ref armProperties, 2, 3);
			break;
		case 4:
			CalamityUtils.SwapArrayIndices(ref armProperties, 1, 3);
			break;
		case 5:
			if (AIState != 1f)
			{
				CalamityUtils.SwapArrayIndices(ref armProperties, 0, 1);
				break;
			}
			CalamityUtils.SwapArrayIndices(ref armProperties, 0, 3);
			CalamityUtils.SwapArrayIndices(ref armProperties, 1, 3);
			break;
		}
		if (laserArm != -1)
		{
			DrawArm(spriteBatch, Main.npc[laserArm].Center, screenPos, armGlowmaskColor, armProperties[0].Item1, armProperties[0].Item2);
		}
		if (gaussArm != -1)
		{
			DrawArm(spriteBatch, Main.npc[gaussArm].Center, screenPos, armGlowmaskColor, armProperties[1].Item1, armProperties[1].Item2);
		}
		if (teslaArm != -1)
		{
			DrawArm(spriteBatch, Main.npc[teslaArm].Center, screenPos, armGlowmaskColor, armProperties[2].Item1, armProperties[2].Item2);
		}
		if (plasmaArm != -1)
		{
			DrawArm(spriteBatch, Main.npc[plasmaArm].Center, screenPos, armGlowmaskColor, armProperties[3].Item1, armProperties[3].Item2);
		}
		if (base.NPC.IsABestiaryIconDummy)
		{
			DrawArm(spriteBatch, base.NPC.Center + base.NPC.scale * new Vector2(-300f, 200f), screenPos, armGlowmaskColor, -1, backArm: true);
			DrawArm(spriteBatch, base.NPC.Center + base.NPC.scale * new Vector2(-400f, 300f), screenPos, armGlowmaskColor, -1, backArm: false);
			DrawArm(spriteBatch, base.NPC.Center + base.NPC.scale * new Vector2(300f, 200f), screenPos, armGlowmaskColor, 1, backArm: true);
			DrawArm(spriteBatch, base.NPC.Center + base.NPC.scale * new Vector2(400f, 300f), screenPos, armGlowmaskColor, 1, backArm: false);
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(base.NPC.width * frameX, base.NPC.height * frameY, base.NPC.width, base.NPC.height);
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector((float)(base.NPC.width / 2), (float)(base.NPC.height / 2));
		int numAfterimages = 5;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < numAfterimages; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, afterimageBaseColor, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(numAfterimages - i) / 15f;
				Vector2 afterimageCenter = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageCenter -= new Vector2((float)texture.Width, (float)texture.Height) / new Vector2(6f, 8f) * base.NPC.scale / 2f;
				afterimageCenter += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimageCenter, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.oldRot[i], vector, base.NPC.scale, (SpriteEffects)0, 0f);
			}
		}
		Vector2 center = base.NPC.Center - screenPos;
		spriteBatch.Draw(texture, center, (Rectangle?)frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, vector, base.NPC.scale, (SpriteEffects)0, 0f);
		texture = GlowTexture.Value;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < numAfterimages; j += 2)
			{
				Color afterimageColor2 = drawColor;
				afterimageColor2 = Color.Lerp(afterimageColor2, afterimageBaseColor, 0.5f);
				afterimageColor2 = base.NPC.GetAlpha(afterimageColor2);
				afterimageColor2 *= (float)(numAfterimages - j) / 15f;
				Vector2 afterimageCenter2 = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageCenter2 -= new Vector2((float)texture.Width, (float)texture.Height) / new Vector2(6f, 8f) * base.NPC.scale / 2f;
				afterimageCenter2 += vector * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimageCenter2, (Rectangle?)base.NPC.frame, afterimageColor2, base.NPC.oldRot[j], vector, base.NPC.scale, (SpriteEffects)0, 0f);
			}
		}
		spriteBatch.Draw(texture, center, (Rectangle?)frame, afterimageBaseColor * base.NPC.Opacity, base.NPC.rotation, vector, base.NPC.scale, (SpriteEffects)0, 0f);
		if (exoMechdusa)
		{
			Texture2D neurontexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/AergiaNeuron", (AssetRequestMode)2).Value;
			Texture2D glowtexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/AergiaNeuron_Glow", (AssetRequestMode)2).Value;
			Vector2 NeuronRight = default(Vector2);
			((Vector2)(ref NeuronRight))._002Ector(base.NPC.Center.X + 40f, base.NPC.Center.Y + 50f);
			Vector2 NeuronLeft = default(Vector2);
			((Vector2)(ref NeuronLeft))._002Ector(base.NPC.Center.X - 40f, base.NPC.Center.Y + 50f);
			Vector2 origin = default(Vector2);
			((Vector2)(ref origin))._002Ector((float)(neurontexture.Width / 2), (float)(neurontexture.Height / 2));
			spriteBatch.Draw(neurontexture, NeuronRight - Main.screenPosition, (Rectangle?)null, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(neurontexture, NeuronLeft - Main.screenPosition, (Rectangle?)null, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(glowtexture, NeuronRight - Main.screenPosition, (Rectangle?)null, Color.White, base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(glowtexture, NeuronLeft - Main.screenPosition, (Rectangle?)null, Color.White, base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
		}
		return false;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(0.5f, 1.3f, (float)Math.Sin((float)Math.PI * completionRatio)) * base.NPC.scale;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		Color val = ((EnragedState == 1f) ? Color.Red : Color.Cyan);
		Color baseColor2 = ((EnragedState == 1f) ? Color.IndianRed : Color.Cyan);
		float fadeToWhite = MathHelper.Lerp(0f, 0.65f, (float)Math.Sin((float)Math.PI * 2f * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f);
		Color color = Color.Lerp(Color.Lerp(val, Color.White, fadeToWhite), baseColor2, ((float)Math.Sin((float)Math.PI * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f) * 0.8f) * 0.65f;
		((Color)(ref color)).A = 84;
		if (base.NPC.Opacity <= 0f)
		{
			return Color.Transparent;
		}
		return color;
	}

	internal float BackgroundWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return WidthFunction(completionRatio, vertexPos) * 4f;
	}

	public Color BackgroundColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		return ((EnragedState == 1f) ? Color.Crimson : Color.CornflowerBlue) * base.NPC.Opacity * 0.4f;
	}

	public void DrawArm(SpriteBatch spriteBatch, Vector2 handPosition, Vector2 screenOffset, Color glowmaskColor, int direction, bool backArm)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		//IL_090f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_0927: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0942: Unknown result type (might be due to invalid IL or missing references)
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_0956: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_0976: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_098f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteDirection = (SpriteEffects)(direction != 1);
		float distanceFromHand = base.NPC.Distance(handPosition);
		float frameTime = Main.GlobalTimeWrappedHourly * 0.9f % 1f;
		if (backArm)
		{
			Texture2D shoulderTexture = ArmTopShoulderTexture.Value;
			Texture2D armTexture1 = ArmTopTexture.Value;
			Texture2D armSegmentTexture = ArmSegmentTexture.Value;
			Texture2D armTexture2 = ArmTopTexture2.Value;
			Texture2D shoulderGlowmask = ArmTopShoulderTexture_Glow.Value;
			Texture2D armSegmentGlowmask = ArmSegmentTexture_Glow.Value;
			Texture2D armGlowmask2 = ArmTopTexture2_Glow.Value;
			Vector2 shoulderDrawPosition = base.NPC.Center + base.NPC.scale * new Vector2((float)direction * 176f, -100f);
			Vector2 arm1DrawPosition = shoulderDrawPosition + base.NPC.scale * new Vector2((float)direction * ((float)shoulderTexture.Width + 16f), 10f);
			Vector2 armSegmentDrawPosition = arm1DrawPosition;
			Rectangle shoulderFrame = shoulderTexture.Frame(1, 9, 0, (int)(frameTime * 9f));
			Rectangle armSegmentFrame = armSegmentTexture.Frame(1, 9, 0, (int)(frameTime * 9f));
			Rectangle arm2Frame = armTexture2.Frame(1, 9, 0, (int)(frameTime * 9f));
			Vector2 arm1Origin = armTexture1.Size() * new Vector2((float)(direction == 1).ToInt(), 0.5f);
			Vector2 arm2Origin = arm2Frame.Size() * new Vector2((float)(direction == 1).ToInt(), 0.5f);
			float arm1Rotation = MathHelper.Clamp(distanceFromHand * (float)direction / 1200f, -0.12f, 0.12f);
			float arm2Rotation = (handPosition - armSegmentDrawPosition - Vector2.UnitY * 12f).ToRotation();
			if (direction == 1)
			{
				arm2Rotation += (float)Math.PI;
			}
			float armSegmentRotation = arm2Rotation;
			armSegmentDrawPosition += arm1Rotation.ToRotationVector2() * base.NPC.scale * (float)direction * -14f;
			armSegmentDrawPosition -= arm2Rotation.ToRotationVector2() * base.NPC.scale * (float)direction * 20f;
			Vector2 arm2DrawPosition = armSegmentDrawPosition;
			arm2DrawPosition -= arm2Rotation.ToRotationVector2() * (float)direction * base.NPC.scale * 40f;
			arm2DrawPosition += (arm2Rotation - (float)Math.PI / 2f).ToRotationVector2() * base.NPC.scale * 14f;
			Color shoulderLightColor = base.NPC.GetAlpha(Lighting.GetColor((int)shoulderDrawPosition.X / 16, (int)shoulderDrawPosition.Y / 16));
			Color arm1LightColor = base.NPC.GetAlpha(Lighting.GetColor((int)arm1DrawPosition.X / 16, (int)arm1DrawPosition.Y / 16));
			Color armSegmentLightColor = base.NPC.GetAlpha(Lighting.GetColor((int)armSegmentDrawPosition.X / 16, (int)armSegmentDrawPosition.Y / 16));
			Color arm2LightColor = base.NPC.GetAlpha(Lighting.GetColor((int)arm2DrawPosition.X / 16, (int)arm2DrawPosition.Y / 16));
			Color glowmaskAlphaColor = base.NPC.GetAlpha(glowmaskColor);
			if (base.NPC.Opacity > 0f && !base.NPC.IsABestiaryIconDummy)
			{
				List<Vector2> positions = AresTeslaOrb.DetermineElectricArcPoints(armSegmentDrawPosition, arm2DrawPosition + arm2Rotation.ToRotationVector2() * (float)(-direction) * 20f, 250290787);
				PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(BackgroundWidthFunction, BackgroundColorFunction, null, smoothen: false), 90);
				PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false), 90);
				List<Vector2> positions2 = AresTeslaOrb.DetermineElectricArcPoints(arm2DrawPosition - arm2Rotation.ToRotationVector2() * (float)direction * 100f, handPosition, 27182);
				PrimitiveRenderer.RenderTrail(positions2, new PrimitiveSettings(BackgroundWidthFunction, BackgroundColorFunction, null, smoothen: false), 90);
				PrimitiveRenderer.RenderTrail(positions2, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false), 90);
			}
			shoulderDrawPosition += Vector2.UnitY * base.NPC.gfxOffY - screenOffset;
			arm1DrawPosition += Vector2.UnitY * base.NPC.gfxOffY - screenOffset;
			armSegmentDrawPosition += Vector2.UnitY * base.NPC.gfxOffY - screenOffset;
			arm2DrawPosition += Vector2.UnitY * base.NPC.gfxOffY - screenOffset;
			spriteBatch.Draw(armTexture1, arm1DrawPosition, (Rectangle?)null, arm1LightColor, arm1Rotation, arm1Origin, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(shoulderTexture, shoulderDrawPosition, (Rectangle?)shoulderFrame, shoulderLightColor, 0f, shoulderFrame.Size() * 0.5f, base.NPC.scale, spriteDirection, 0f);
			spriteBatch.Draw(shoulderGlowmask, shoulderDrawPosition, (Rectangle?)shoulderFrame, glowmaskAlphaColor, 0f, shoulderFrame.Size() * 0.5f, base.NPC.scale, spriteDirection, 0f);
			spriteBatch.Draw(armSegmentTexture, armSegmentDrawPosition, (Rectangle?)armSegmentFrame, armSegmentLightColor, armSegmentRotation, armSegmentFrame.Size() * 0.5f, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(armSegmentGlowmask, armSegmentDrawPosition, (Rectangle?)armSegmentFrame, glowmaskAlphaColor, armSegmentRotation, armSegmentFrame.Size() * 0.5f, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(armTexture2, arm2DrawPosition, (Rectangle?)arm2Frame, arm2LightColor, arm2Rotation, arm2Origin, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 2), 0f);
			spriteBatch.Draw(armGlowmask2, arm2DrawPosition, (Rectangle?)arm2Frame, glowmaskAlphaColor, arm2Rotation, arm2Origin, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 2), 0f);
		}
		else
		{
			Texture2D shoulderTexture2 = ArmBottomShoulderTexture.Value;
			Texture2D connectorTexture = ArmBottomConnectorTexture.Value;
			Texture2D armTexture3 = ArmBottomTexture.Value;
			Texture2D armTexture4 = ArmBottomTexture2.Value;
			Texture2D shoulderGlowmask2 = ArmBottomShoulderTexture_Glow.Value;
			Texture2D armTexture1Glowmask = ArmBottomTexture_Glow.Value;
			Texture2D armTexture2Glowmask = ArmBottomTexture2_Glow.Value;
			Vector2 shoulderDrawPosition2 = base.NPC.Center + base.NPC.scale * new Vector2((float)direction * 110f, -54f);
			Vector2 connectorDrawPosition = shoulderDrawPosition2 + base.NPC.scale * new Vector2((float)direction * 20f, 32f);
			Vector2 arm1DrawPosition2 = shoulderDrawPosition2 + base.NPC.scale * Vector2.UnitX * (float)direction * 20f;
			Rectangle arm1Frame = armTexture3.Frame(1, 9, 0, (int)(frameTime * 9f));
			Rectangle shoulderFrame2 = shoulderTexture2.Frame(1, 9, 0, (int)(frameTime * 9f));
			Rectangle arm2Frame2 = armTexture4.Frame(1, 9, 0, (int)(frameTime * 9f));
			Vector2 arm1Origin2 = arm1Frame.Size() * new Vector2((float)(direction == 1).ToInt(), 0.5f);
			Vector2 arm2Origin2 = arm2Frame2.Size() * new Vector2((float)(direction == 1).ToInt(), 0.5f);
			float arm1Rotation2 = CalamityUtils.WrapAngle90Degrees((handPosition - shoulderDrawPosition2).ToRotation()) * 0.5f;
			connectorDrawPosition += arm1Rotation2.ToRotationVector2() * base.NPC.scale * (float)direction * -26f;
			arm1DrawPosition2 += arm1Rotation2.ToRotationVector2() * base.NPC.scale * (float)direction * ((float)armTexture3.Width - 14f);
			float arm2Rotation2 = CalamityUtils.WrapAngle90Degrees((handPosition - arm1DrawPosition2).ToRotation());
			Vector2 arm2DrawPosition2 = arm1DrawPosition2 + arm2Rotation2.ToRotationVector2() * base.NPC.scale * (float)direction * ((float)armTexture4.Width + 16f) - Vector2.UnitY * 16f;
			Color shoulderLightColor2 = base.NPC.GetAlpha(Lighting.GetColor((int)shoulderDrawPosition2.X / 16, (int)shoulderDrawPosition2.Y / 16));
			Color arm1LightColor2 = base.NPC.GetAlpha(Lighting.GetColor((int)arm1DrawPosition2.X / 16, (int)arm1DrawPosition2.Y / 16));
			Color arm2LightColor2 = base.NPC.GetAlpha(Lighting.GetColor((int)arm2DrawPosition2.X / 16, (int)arm2DrawPosition2.Y / 16));
			Color glowmaskAlphaColor2 = base.NPC.GetAlpha(glowmaskColor);
			if (base.NPC.Opacity > 0f && !base.NPC.IsABestiaryIconDummy)
			{
				List<Vector2> positions3 = AresTeslaOrb.DetermineElectricArcPoints(arm1DrawPosition2 - arm2Rotation2.ToRotationVector2() * (float)direction * 10f, arm1DrawPosition2 + arm2Rotation2.ToRotationVector2() * (float)direction * 20f, 31416);
				PrimitiveRenderer.RenderTrail(positions3, new PrimitiveSettings(BackgroundWidthFunction, BackgroundColorFunction, null, smoothen: false), 90);
				PrimitiveRenderer.RenderTrail(positions3, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false), 90);
				List<Vector2> positions4 = AresTeslaOrb.DetermineElectricArcPoints(arm2DrawPosition2 - arm2Rotation2.ToRotationVector2() * (float)direction * 20f, handPosition, 27182);
				PrimitiveRenderer.RenderTrail(positions4, new PrimitiveSettings(BackgroundWidthFunction, BackgroundColorFunction, null, smoothen: false), 90);
				PrimitiveRenderer.RenderTrail(positions4, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false), 90);
			}
			shoulderDrawPosition2 += Vector2.UnitY * base.NPC.gfxOffY - screenOffset;
			connectorDrawPosition += Vector2.UnitY * base.NPC.gfxOffY - screenOffset;
			arm1DrawPosition2 += Vector2.UnitY * base.NPC.gfxOffY - screenOffset;
			arm2DrawPosition2 += Vector2.UnitY * base.NPC.gfxOffY - screenOffset;
			spriteBatch.Draw(shoulderTexture2, shoulderDrawPosition2, (Rectangle?)shoulderFrame2, shoulderLightColor2, arm1Rotation2, shoulderFrame2.Size() * 0.5f, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(shoulderGlowmask2, shoulderDrawPosition2, (Rectangle?)shoulderFrame2, glowmaskAlphaColor2, arm1Rotation2, shoulderFrame2.Size() * 0.5f, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(connectorTexture, connectorDrawPosition, (Rectangle?)null, shoulderLightColor2, 0f, connectorTexture.Size() * 0.5f, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(armTexture3, arm1DrawPosition2, (Rectangle?)arm1Frame, arm1LightColor2, arm1Rotation2, arm1Origin2, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(armTexture1Glowmask, arm1DrawPosition2, (Rectangle?)arm1Frame, glowmaskAlphaColor2, arm1Rotation2, arm1Origin2, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(armTexture4, arm2DrawPosition2, (Rectangle?)arm2Frame2, arm2LightColor2, arm2Rotation2, arm2Origin2, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
			spriteBatch.Draw(armTexture2Glowmask, arm2DrawPosition2, (Rectangle?)arm2Frame2, glowmaskAlphaColor2, arm2Rotation2, arm2Origin2, base.NPC.scale, (SpriteEffects)(spriteDirection ^ 1), 0f);
		}
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (exoMechdusa)
		{
			typeName = this.GetLocalizedValue("HekateName");
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<OmegaHealingPotion>();
	}

	public override void OnKill()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		bool exoWormAlive = false;
		bool exoTwinsAlive = false;
		if (SoundEngine.TryGetActiveSound(DeathraySoundSlot, out ActiveSound deathraySound) && deathraySound.IsPlaying)
		{
			deathraySound?.Stop();
		}
		if (CalamityGlobalNPC.draedonExoMechWorm != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechWorm].active)
		{
			exoWormAlive = true;
		}
		if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
		{
			exoTwinsAlive = true;
		}
		bool draedonAlive = false;
		if (CalamityGlobalNPC.draedon != -1 && Main.npc[CalamityGlobalNPC.draedon].active)
		{
			draedonAlive = true;
		}
		if (exoWormAlive & exoTwinsAlive)
		{
			if (draedonAlive)
			{
				Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 4f;
				Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
			}
		}
		else if (exoWormAlive | exoTwinsAlive)
		{
			if (draedonAlive)
			{
				Main.npc[CalamityGlobalNPC.draedon].localAI[0] = 6f;
				Main.npc[CalamityGlobalNPC.draedon].ai[0] = 780f;
			}
		}
		else
		{
			DoMiscDeathEffects(base.NPC, MechType.Ares);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		DefineExoMechLoot(base.NPC, npcLoot, 0);
	}

	public static bool CanDropLoot()
	{
		return NPC.CountNPCS(ModContent.NPCType<ThanatosHead>()) + NPC.CountNPCS(ModContent.NPCType<AresBody>()) + NPC.CountNPCS(ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>()) <= 1;
	}

	public static void DoMiscDeathEffects(NPC npc, MechType mechType)
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(npc);
			switch (mechType)
			{
			case MechType.Thanatos:
				DownedBossSystem.downedThanatos = true;
				DownedBossSystem.downedExoMechs = true;
				break;
			case MechType.Ares:
				DownedBossSystem.downedAres = true;
				DownedBossSystem.downedExoMechs = true;
				break;
			case MechType.ArtemisAndApollo:
				DownedBossSystem.downedArtemisAndApollo = true;
				DownedBossSystem.downedExoMechs = true;
				break;
			}
			CalamityNetcode.SyncWorld();
		}
	}

	public static void DefineExoMechLoot(NPC npc, NPCLoot npcLoot, int mechType)
	{
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(CanDropLoot);
		LeadingConditionRule normalOnly = new LeadingConditionRule(new Conditions.NotExpert());
		mainRule.Add(normalOnly);
		mainRule.Add(ItemDropRule.ByCondition(DropHelper.If((DropAttemptInfo info) => info.npc.type == ModContent.NPCType<ThanatosHead>()), ModContent.ItemType<ThanatosTrophy>()));
		mainRule.Add(ItemDropRule.ByCondition(DropHelper.If((DropAttemptInfo info) => info.npc.type == ModContent.NPCType<AresBody>()), ModContent.ItemType<AresTrophy>()));
		mainRule.Add(ItemDropRule.ByCondition(DropHelper.If((DropAttemptInfo info) => info.npc.type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>()), ModContent.ItemType<ArtemisTrophy>()));
		mainRule.Add(ItemDropRule.ByCondition(DropHelper.If((DropAttemptInfo info) => info.npc.type == ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>()), ModContent.ItemType<ApolloTrophy>()));
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).AddIf(CanDropLoot, ModContent.ItemType<DraedonRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).AddIf(CanDropLoot, ModContent.ItemType<ExoArmamentsKit>(), 4);
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<BrokenWaterFilter>()), hideLootReport: true);
		mainRule.Add(ItemDropRule.ByCondition(DropHelper.If(() => !DownedBossSystem.downedExoMechs, ui: true, DropHelper.FirstKillText), ModContent.ItemType<LoreExoMechs>()));
		mainRule.Add(ItemDropRule.ByCondition(DropHelper.If(() => !DownedBossSystem.downedExoMechs && DownedBossSystem.downedCalamitas, ui: true, DropHelper.CynosureText), ModContent.ItemType<LoreCynosure>()));
		npcLoot.Add(ItemDropRule.BossBagByCondition(DropHelper.If(CanDropLoot), ModContent.ItemType<DraedonBag>()));
		mainRule.Add(ItemDropRule.ByCondition(DropHelper.If((DropAttemptInfo info) => info.npc.type == ModContent.NPCType<AresBody>() && info.npc.ModNPC<AresBody>().exoMechdusa), ModContent.ItemType<LavaChickenBroth>()), hideLootReport: true);
		if (!Main.expertMode)
		{
			normalOnly.Add(ModContent.ItemType<ExoPrism>(), 1, 25, 30);
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(ThanatosLoot), ModContent.ItemType<SpineOfThanatos>()));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(ThanatosLoot), ModContent.ItemType<RefractionRotor>()));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(ThanatosLoot), ModContent.ItemType<AtlasMunitionsBeacon>()));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(AresLoot), ModContent.ItemType<PhotonRipper>()));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(AresLoot), ModContent.ItemType<TheJailor>()));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(AresLoot), ModContent.ItemType<AresExoskeleton>()));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(ApolloLoot), ModContent.ItemType<TheAtomSplitter>()));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(ApolloLoot), ModContent.ItemType<SurgeDriver>()));
			normalOnly.Add(ModContent.ItemType<ExoThrone>());
			normalOnly.Add(ModContent.ItemType<DraedonMask>(), 3);
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(ThanatosLoot), ModContent.ItemType<ThanatosMask>(), 7, 1, 1, 2));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(AresLoot), ModContent.ItemType<AresMask>(), 7, 1, 1, 2));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(ApolloLoot), ModContent.ItemType<ArtemisMask>(), 7, 1, 1, 2));
			normalOnly.Add(ItemDropRule.ByCondition(DropHelper.If(ApolloLoot), ModContent.ItemType<ApolloMask>(), 7, 1, 1, 2));
			normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		}
		static bool ApolloLoot(DropAttemptInfo info)
		{
			if (info.npc.type != ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>())
			{
				return DownedBossSystem.downedArtemisAndApollo;
			}
			return true;
		}
		static bool AresLoot(DropAttemptInfo info)
		{
			if (info.npc.type != ModContent.NPCType<AresBody>())
			{
				return DownedBossSystem.downedAres;
			}
			return true;
		}
		static bool ThanatosLoot(DropAttemptInfo info)
		{
			if (info.npc.type != ModContent.NPCType<ThanatosHead>())
			{
				return DownedBossSystem.downedThanatos;
			}
			return true;
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
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresBody1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresBody2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresBody3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresBody4").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresBody5").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresBody6").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AresBody7").Type);
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
