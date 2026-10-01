using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Events;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Fishing.FishingRods;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Systems.Collections;
using CalamityMod.Systems.Mechanic;
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
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SupremeCalamitas;

public class SupremeCalamitas : ModNPC
{
	public enum FrameAnimationType
	{
		UpwardDraft,
		FasterUpwardDraft,
		Casting,
		BlastCast,
		BlastPunchCast,
		OutwardHandCast,
		PunchHandCast,
		Count
	}

	public const int BulletHellDuration = 900;

	public const int SecondBulletHellEndValue = 1800;

	public const int ThirdBulletHellEndValue = 2700;

	public const int FourthBulletHellEndValue = 3600;

	public const int FifthBulletHellEndValue = 4500;

	public const int PermafrostAbsoluteZeroDamage = 3725;

	private const float PermafrostPhotonRipperDashVelocity = 6f;

	private const float PermafrostPhotonRipperMinDistanceFromTarget = 64f;

	private const float PermafrostPhotonRipperDashAcceleration = 0.3f;

	public float bossLife;

	public float uDieLul = 1f;

	public float passedVar;

	public bool protectionBoost;

	public bool canDespawn;

	public bool despawnProj;

	public bool startText;

	public bool startBattle;

	public bool hasSummonedSepulcher1;

	public bool startSecondAttack;

	public bool startThirdAttack;

	public bool halfLife;

	public bool startFourthAttack;

	public bool secondStage;

	public bool startFifthAttack;

	public bool gettingTired;

	public bool hasSummonedSepulcher2;

	public bool gettingTired2;

	public bool gettingTired3;

	public bool gettingTired4;

	public bool gettingTired5;

	public bool willCharge;

	public bool canFireSplitingFireball = true;

	public bool fireFireblastFirst = true;

	public bool spawnArena;

	public bool enteredBrothersPhase;

	public bool hasSummonedBrothers;

	public bool permafrost;

	public bool hasDoneDeathAnim;

	public bool postMusicHit;

	private const int GiveUpCounterMax = 1200;

	private const int musicSyncCounterMax = 2082;

	public int giveUpCounter = 1200;

	public int musicSyncCounter = 2082;

	public int phaseChange;

	public int spawnX;

	public int spawnX2;

	public int spawnXReset;

	public int spawnXReset2;

	public int spawnXAdd = 200;

	public int spawnY;

	public int spawnYReset;

	public int spawnYAdd;

	public int bulletHellCounter;

	public int bulletHellCounter2;

	public int attackCastDelay;

	public int hitTimer;

	public int dashVisualCounter;

	public int preventionPause = 15;

	public int attackPause;

	public bool respawnBro = true;

	public float shieldOpacity = 1f;

	public float shieldRotation;

	public float forcefieldOpacity = 1f;

	public float forcefieldScale = 1f;

	public float forcefieldPureVisualScale = 1f;

	public float rotateToPlayer;

	public float rotateAwayPlayer;

	public float colorCompletion = 1f;

	public ArenaWallSystem.Box ArenaBox;

	public Vector2 cataclysmSpawnPosition;

	public Vector2 catastropheSpawnPosition;

	public Vector2 initialRitualPosition;

	public Rectangle safeBox;

	public static int hoodedHeadIconIndex;

	public static int hoodedHeadIconP2Index;

	public static int hoodlessHeadIconIndex;

	public static int hoodlessHeadIconP2Index;

	public static int permafrostHeadIconIndex;

	public static float normalDR;

	public static float enragedDR;

	public static readonly Color textColor;

	public static readonly Color permafrostTextColor;

	public const int sepulcherSpawnCastTime = 75;

	public const int brothersSpawnCastTime = 150;

	public static readonly SoundStyle SpawnSound;

	public static readonly SoundStyle SepulcherSummonSound;

	public static readonly SoundStyle BrimstoneShotSound;

	public static readonly SoundStyle BrotherHit;

	public static readonly SoundStyle BrotherDeath;

	public static readonly SoundStyle CatastropheSwing;

	public static readonly SoundStyle BrimstoneBigShotSound;

	public static readonly SoundStyle DashSound;

	public static readonly SoundStyle HellblastSound;

	public static readonly SoundStyle HurtSound;

	public static readonly SoundStyle BulletHellSound;

	public static readonly SoundStyle BulletHellEndSound;

	public static readonly SoundStyle GiveUpSound;

	public SlotId BulletHellRumbleSlot;

	public static Asset<Texture2D> HoodedTexture;

	public static Asset<Texture2D> PermafrostTexture;

	public static Asset<Texture2D> ShieldTopTexture;

	public static Asset<Texture2D> ShieldBottomTexture;

	public static Asset<Texture2D> ForcefieldTexture;

	public static int DartDamage;

	public static int SkullDamage;

	public static int HellblastDamage;

	public static int FireblastDamage;

	public static int GigablastDamage;

	public static Color CurrentColor
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			if (CalamityGlobalNPC.SCal < 0)
			{
				return AcceptanceColor;
			}
			NPC npc = Main.npc[CalamityGlobalNPC.SCal];
			if (npc == null || !npc.active)
			{
				return AcceptanceColor;
			}
			if (!(npc.ModNPC is SupremeCalamitas { ArenaBox: not null } calamitas))
			{
				return AcceptanceColor;
			}
			return calamitas.ArenaBox.borderColor;
		}
	}

	public static Color GriefColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Crimson;
		}
	}

	public static Color LamentColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.RoyalBlue;
		}
	}

	public static Color EpiphanyColor
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			return new Color(219, 75, 2);
		}
	}

	public static Color AcceptanceColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Gray;
		}
	}

	public FrameAnimationType FrameType
	{
		get
		{
			return (FrameAnimationType)base.NPC.localAI[2];
		}
		set
		{
			base.NPC.localAI[2] = (float)value;
		}
	}

	public bool AttackCloseToBeingOver
	{
		get
		{
			int attackLength = 0;
			if (base.NPC.ai[0] == 0f)
			{
				if (base.NPC.ai[1] == 0f)
				{
					attackLength = 300;
				}
				if (base.NPC.ai[1] == 2f)
				{
					attackLength = 70;
				}
				if (base.NPC.ai[1] == 3f)
				{
					attackLength = 480;
				}
				if (base.NPC.ai[1] == 4f)
				{
					attackLength = 300;
				}
			}
			else
			{
				if (base.NPC.ai[1] == 0f)
				{
					attackLength = 240;
				}
				if (base.NPC.ai[1] == 2f)
				{
					attackLength = 70;
				}
				if (base.NPC.ai[1] == 3f)
				{
					attackLength = 300;
				}
				if (base.NPC.ai[1] == 4f)
				{
					attackLength = 240;
				}
			}
			return base.NPC.ai[2] >= (float)attackLength - 30f;
		}
	}

	public ref float FrameChangeSpeed => ref base.NPC.localAI[3];

	public override void Load()
	{
		string hoodedIconPath = "CalamityMod/NPCs/SupremeCalamitas/HoodedHeadIcon";
		string hoodlessIconPath = "CalamityMod/NPCs/SupremeCalamitas/HoodlessHeadIcon";
		string permafrostIconPath = "CalamityMod/NPCs/TownNPCs/Archmage_Head";
		hoodedHeadIconIndex = CalamityMod.Instance.AddBossHeadTexture(hoodedIconPath);
		hoodlessHeadIconIndex = CalamityMod.Instance.AddBossHeadTexture(hoodlessIconPath);
		permafrostHeadIconIndex = CalamityMod.Instance.AddBossHeadTexture(permafrostIconPath);
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 21;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 14f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			HoodedTexture = ModContent.Request<Texture2D>(Texture + "Hooded", (AssetRequestMode)2);
			PermafrostTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/SupremePermafrost", (AssetRequestMode)2);
			ShieldTopTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/SupremeShieldTop", (AssetRequestMode)2);
			ShieldBottomTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/SupremeShieldBottom", (AssetRequestMode)2);
			ForcefieldTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/ForcefieldTexture", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 210;
		base.NPC.npcSlots = 50f;
		base.NPC.width = (base.NPC.height = 44);
		base.NPC.defense = 100;
		base.NPC.DR_NERD(normalDR);
		base.NPC.value = Item.buyPrice(3);
		base.NPC.LifeMaxNERB(750000, 1150000, 900000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.dontTakeDamage = false;
		base.NPC.chaseable = true;
		base.NPC.boss = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SupremeCalamitas")
		});
	}

	public override void BossHeadSlot(ref int index)
	{
		_ = base.NPC.ai[0];
		if (permafrost)
		{
			index = permafrostHeadIconIndex;
		}
		else if (!DownedBossSystem.downedCalamitas || BossRushEvent.BossRushActive)
		{
			index = hoodedHeadIconIndex;
		}
		else
		{
			index = hoodlessHeadIconIndex;
		}
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (permafrost)
		{
			typeName = CalamityUtils.GetTextValue("NPCs.SupremePermafrost");
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(protectionBoost);
		writer.Write(canDespawn);
		writer.Write(despawnProj);
		writer.Write(startText);
		writer.Write(startBattle);
		writer.Write(hasSummonedSepulcher1);
		writer.Write(startSecondAttack);
		writer.Write(startThirdAttack);
		writer.Write(startFourthAttack);
		writer.Write(startFifthAttack);
		writer.Write(halfLife);
		writer.Write(secondStage);
		writer.Write(hasSummonedSepulcher2);
		writer.Write(gettingTired);
		writer.Write(gettingTired2);
		writer.Write(gettingTired3);
		writer.Write(gettingTired4);
		writer.Write(gettingTired5);
		writer.Write(willCharge);
		writer.Write(canFireSplitingFireball);
		writer.Write(spawnArena);
		writer.Write(hasSummonedBrothers);
		writer.Write(enteredBrothersPhase);
		writer.Write(permafrost);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.chaseable);
		writer.Write(giveUpCounter);
		writer.Write(phaseChange);
		writer.Write(spawnX);
		writer.Write(spawnX2);
		writer.Write(spawnXReset);
		writer.Write(spawnXReset2);
		writer.Write(spawnXAdd);
		writer.Write(spawnY);
		writer.Write(spawnYReset);
		writer.Write(spawnYAdd);
		writer.Write(bulletHellCounter);
		writer.Write(bulletHellCounter2);
		writer.Write(hitTimer);
		writer.Write(attackCastDelay);
		writer.Write(shieldRotation);
		writer.Write(safeBox.X);
		writer.Write(safeBox.Y);
		writer.Write(safeBox.Width);
		writer.Write(safeBox.Height);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		protectionBoost = reader.ReadBoolean();
		canDespawn = reader.ReadBoolean();
		despawnProj = reader.ReadBoolean();
		startText = reader.ReadBoolean();
		startBattle = reader.ReadBoolean();
		hasSummonedSepulcher1 = reader.ReadBoolean();
		startSecondAttack = reader.ReadBoolean();
		startThirdAttack = reader.ReadBoolean();
		startFourthAttack = reader.ReadBoolean();
		startFifthAttack = reader.ReadBoolean();
		halfLife = reader.ReadBoolean();
		secondStage = reader.ReadBoolean();
		hasSummonedSepulcher2 = reader.ReadBoolean();
		gettingTired = reader.ReadBoolean();
		gettingTired2 = reader.ReadBoolean();
		gettingTired3 = reader.ReadBoolean();
		gettingTired4 = reader.ReadBoolean();
		gettingTired5 = reader.ReadBoolean();
		willCharge = reader.ReadBoolean();
		canFireSplitingFireball = reader.ReadBoolean();
		spawnArena = reader.ReadBoolean();
		hasSummonedBrothers = reader.ReadBoolean();
		enteredBrothersPhase = reader.ReadBoolean();
		permafrost = reader.ReadBoolean();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
		giveUpCounter = reader.ReadInt32();
		phaseChange = reader.ReadInt32();
		spawnX = reader.ReadInt32();
		spawnX2 = reader.ReadInt32();
		spawnXReset = reader.ReadInt32();
		spawnXReset2 = reader.ReadInt32();
		spawnXAdd = reader.ReadInt32();
		spawnY = reader.ReadInt32();
		spawnYReset = reader.ReadInt32();
		spawnYAdd = reader.ReadInt32();
		bulletHellCounter = reader.ReadInt32();
		bulletHellCounter2 = reader.ReadInt32();
		hitTimer = reader.ReadInt32();
		attackCastDelay = reader.ReadInt32();
		shieldRotation = reader.ReadSingle();
		safeBox = new Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
	}

	public override void AI()
	{
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_093c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0faa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1739: Unknown result type (might be due to invalid IL or missing references)
		//IL_173e: Unknown result type (might be due to invalid IL or missing references)
		//IL_12dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1752: Unknown result type (might be due to invalid IL or missing references)
		//IL_174b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1300: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1102: Unknown result type (might be due to invalid IL or missing references)
		//IL_1107: Unknown result type (might be due to invalid IL or missing references)
		//IL_111a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1120: Unknown result type (might be due to invalid IL or missing references)
		//IL_1021: Unknown result type (might be due to invalid IL or missing references)
		//IL_1027: Unknown result type (might be due to invalid IL or missing references)
		//IL_1761: Unknown result type (might be due to invalid IL or missing references)
		//IL_1792: Unknown result type (might be due to invalid IL or missing references)
		//IL_1797: Unknown result type (might be due to invalid IL or missing references)
		//IL_1339: Unknown result type (might be due to invalid IL or missing references)
		//IL_1343: Unknown result type (might be due to invalid IL or missing references)
		//IL_1348: Unknown result type (might be due to invalid IL or missing references)
		//IL_113c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1135: Unknown result type (might be due to invalid IL or missing references)
		//IL_1040: Unknown result type (might be due to invalid IL or missing references)
		//IL_1039: Unknown result type (might be due to invalid IL or missing references)
		//IL_195d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1968: Unknown result type (might be due to invalid IL or missing references)
		//IL_196d: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_131b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1141: Unknown result type (might be due to invalid IL or missing references)
		//IL_1147: Unknown result type (might be due to invalid IL or missing references)
		//IL_1161: Unknown result type (might be due to invalid IL or missing references)
		//IL_1166: Unknown result type (might be due to invalid IL or missing references)
		//IL_1051: Unknown result type (might be due to invalid IL or missing references)
		//IL_1980: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1075: Unknown result type (might be due to invalid IL or missing references)
		//IL_1086: Unknown result type (might be due to invalid IL or missing references)
		//IL_22b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_22bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_22c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_22d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_199b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1806: Unknown result type (might be due to invalid IL or missing references)
		//IL_1814: Unknown result type (might be due to invalid IL or missing references)
		//IL_1819: Unknown result type (might be due to invalid IL or missing references)
		//IL_1821: Unknown result type (might be due to invalid IL or missing references)
		//IL_1826: Unknown result type (might be due to invalid IL or missing references)
		//IL_182d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1832: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_18dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2129: Unknown result type (might be due to invalid IL or missing references)
		//IL_2140: Unknown result type (might be due to invalid IL or missing references)
		//IL_19cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_19da: Unknown result type (might be due to invalid IL or missing references)
		//IL_19df: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_24be: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_24d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_24dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_24fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2503: Unknown result type (might be due to invalid IL or missing references)
		//IL_2508: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2232: Unknown result type (might be due to invalid IL or missing references)
		//IL_222b: Unknown result type (might be due to invalid IL or missing references)
		//IL_216e: Unknown result type (might be due to invalid IL or missing references)
		//IL_217e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1857: Unknown result type (might be due to invalid IL or missing references)
		//IL_185d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1871: Unknown result type (might be due to invalid IL or missing references)
		//IL_1887: Unknown result type (might be due to invalid IL or missing references)
		//IL_188c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a05: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2568: Unknown result type (might be due to invalid IL or missing references)
		//IL_2572: Unknown result type (might be due to invalid IL or missing references)
		//IL_2577: Unknown result type (might be due to invalid IL or missing references)
		//IL_2543: Unknown result type (might be due to invalid IL or missing references)
		//IL_254d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2552: Unknown result type (might be due to invalid IL or missing references)
		//IL_251c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2524: Unknown result type (might be due to invalid IL or missing references)
		//IL_2321: Unknown result type (might be due to invalid IL or missing references)
		//IL_232f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2334: Unknown result type (might be due to invalid IL or missing references)
		//IL_233c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2341: Unknown result type (might be due to invalid IL or missing references)
		//IL_2348: Unknown result type (might be due to invalid IL or missing references)
		//IL_234d: Unknown result type (might be due to invalid IL or missing references)
		//IL_23da: Unknown result type (might be due to invalid IL or missing references)
		//IL_23df: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c04: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b08: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c13: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c49: Unknown result type (might be due to invalid IL or missing references)
		//IL_2372: Unknown result type (might be due to invalid IL or missing references)
		//IL_2378: Unknown result type (might be due to invalid IL or missing references)
		//IL_238c: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2402: Unknown result type (might be due to invalid IL or missing references)
		//IL_2433: Unknown result type (might be due to invalid IL or missing references)
		//IL_2438: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f92: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c56: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c65: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_245b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2445: Unknown result type (might be due to invalid IL or missing references)
		//IL_244a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2454: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_246a: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_3aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2df8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dff: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ac4: Unknown result type (might be due to invalid IL or missing references)
		//IL_31a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_31aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_31af: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d20: Unknown result type (might be due to invalid IL or missing references)
		//IL_3917: Unknown result type (might be due to invalid IL or missing references)
		//IL_392e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3001: Unknown result type (might be due to invalid IL or missing references)
		//IL_300f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3014: Unknown result type (might be due to invalid IL or missing references)
		//IL_301c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3021: Unknown result type (might be due to invalid IL or missing references)
		//IL_3028: Unknown result type (might be due to invalid IL or missing references)
		//IL_302d: Unknown result type (might be due to invalid IL or missing references)
		//IL_30ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_30bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_45e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_45e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_45ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_4605: Unknown result type (might be due to invalid IL or missing references)
		//IL_460b: Unknown result type (might be due to invalid IL or missing references)
		//IL_460d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4612: Unknown result type (might be due to invalid IL or missing references)
		//IL_4617: Unknown result type (might be due to invalid IL or missing references)
		//IL_461f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cad: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ccc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cd6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ce5: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_3adf: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_395c: Unknown result type (might be due to invalid IL or missing references)
		//IL_396c: Unknown result type (might be due to invalid IL or missing references)
		//IL_30d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_30cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e91: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e93: Unknown result type (might be due to invalid IL or missing references)
		//IL_2e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ec6: Unknown result type (might be due to invalid IL or missing references)
		//IL_47cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_47d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_47da: Unknown result type (might be due to invalid IL or missing references)
		//IL_47ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_47f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_47ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_4810: Unknown result type (might be due to invalid IL or missing references)
		//IL_447c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4493: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d61: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d32: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d41: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d13: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b10: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b37: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_31e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_31e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_31f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_31f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_3202: Unknown result type (might be due to invalid IL or missing references)
		//IL_3207: Unknown result type (might be due to invalid IL or missing references)
		//IL_320c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3211: Unknown result type (might be due to invalid IL or missing references)
		//IL_3218: Unknown result type (might be due to invalid IL or missing references)
		//IL_321d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3231: Unknown result type (might be due to invalid IL or missing references)
		//IL_3236: Unknown result type (might be due to invalid IL or missing references)
		//IL_323e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3052: Unknown result type (might be due to invalid IL or missing references)
		//IL_3058: Unknown result type (might be due to invalid IL or missing references)
		//IL_306c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3082: Unknown result type (might be due to invalid IL or missing references)
		//IL_3087: Unknown result type (might be due to invalid IL or missing references)
		//IL_30e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3113: Unknown result type (might be due to invalid IL or missing references)
		//IL_3118: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4644: Unknown result type (might be due to invalid IL or missing references)
		//IL_4648: Unknown result type (might be due to invalid IL or missing references)
		//IL_458c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4585: Unknown result type (might be due to invalid IL or missing references)
		//IL_44c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_44d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_313b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3125: Unknown result type (might be due to invalid IL or missing references)
		//IL_312a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3134: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f16: Unknown result type (might be due to invalid IL or missing references)
		//IL_5559: Unknown result type (might be due to invalid IL or missing references)
		//IL_5564: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a82: Unknown result type (might be due to invalid IL or missing references)
		//IL_49a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_49a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_49ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_49ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_49bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_4837: Unknown result type (might be due to invalid IL or missing references)
		//IL_484b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4850: Unknown result type (might be due to invalid IL or missing references)
		//IL_4863: Unknown result type (might be due to invalid IL or missing references)
		//IL_4869: Unknown result type (might be due to invalid IL or missing references)
		//IL_4876: Unknown result type (might be due to invalid IL or missing references)
		//IL_487b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4881: Unknown result type (might be due to invalid IL or missing references)
		//IL_489b: Unknown result type (might be due to invalid IL or missing references)
		//IL_48a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b96: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_3379: Unknown result type (might be due to invalid IL or missing references)
		//IL_337e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3383: Unknown result type (might be due to invalid IL or missing references)
		//IL_314a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3180: Unknown result type (might be due to invalid IL or missing references)
		//IL_318b: Unknown result type (might be due to invalid IL or missing references)
		//IL_546b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5476: Unknown result type (might be due to invalid IL or missing references)
		//IL_53d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_53de: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_49d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_49d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3da0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c39: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c43: Unknown result type (might be due to invalid IL or missing references)
		//IL_33c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_33cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_53a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_539e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f73: Unknown result type (might be due to invalid IL or missing references)
		//IL_49e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_49ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_49fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a02: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_46c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_46cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_46d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_46d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_46da: Unknown result type (might be due to invalid IL or missing references)
		//IL_46ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_46f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_46f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_46fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_4704: Unknown result type (might be due to invalid IL or missing references)
		//IL_4709: Unknown result type (might be due to invalid IL or missing references)
		//IL_470e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3df1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3df8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e17: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e41: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e46: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5854: Unknown result type (might be due to invalid IL or missing references)
		//IL_585e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5863: Unknown result type (might be due to invalid IL or missing references)
		//IL_57bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_57c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_57cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_502c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5040: Unknown result type (might be due to invalid IL or missing references)
		//IL_5045: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_4eba: Unknown result type (might be due to invalid IL or missing references)
		//IL_33da: Unknown result type (might be due to invalid IL or missing references)
		//IL_33dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_33e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_33e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_33f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3425: Unknown result type (might be due to invalid IL or missing references)
		//IL_3427: Unknown result type (might be due to invalid IL or missing references)
		//IL_342c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fe4: Unknown result type (might be due to invalid IL or missing references)
		//IL_4fdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_346f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3474: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a72: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a99: Unknown result type (might be due to invalid IL or missing references)
		//IL_5a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5aa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_532f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5328: Unknown result type (might be due to invalid IL or missing references)
		//IL_506b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5071: Unknown result type (might be due to invalid IL or missing references)
		//IL_507e: Unknown result type (might be due to invalid IL or missing references)
		//IL_508c: Unknown result type (might be due to invalid IL or missing references)
		//IL_50a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_50ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_50f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_510c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5111: Unknown result type (might be due to invalid IL or missing references)
		//IL_51bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_51c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_7093: Unknown result type (might be due to invalid IL or missing references)
		//IL_70a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_70ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ab8: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c21: Unknown result type (might be due to invalid IL or missing references)
		//IL_3497: Unknown result type (might be due to invalid IL or missing references)
		//IL_34a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_34be: Unknown result type (might be due to invalid IL or missing references)
		//IL_34cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_34d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_351e: Unknown result type (might be due to invalid IL or missing references)
		//IL_352c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3545: Unknown result type (might be due to invalid IL or missing references)
		//IL_354a: Unknown result type (might be due to invalid IL or missing references)
		//IL_354d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3552: Unknown result type (might be due to invalid IL or missing references)
		//IL_3557: Unknown result type (might be due to invalid IL or missing references)
		//IL_355e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3563: Unknown result type (might be due to invalid IL or missing references)
		//IL_3568: Unknown result type (might be due to invalid IL or missing references)
		//IL_35d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_35d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_712f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7139: Unknown result type (might be due to invalid IL or missing references)
		//IL_713e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_744d: Unknown result type (might be due to invalid IL or missing references)
		//IL_7459: Unknown result type (might be due to invalid IL or missing references)
		//IL_7469: Unknown result type (might be due to invalid IL or missing references)
		//IL_7474: Unknown result type (might be due to invalid IL or missing references)
		//IL_7479: Unknown result type (might be due to invalid IL or missing references)
		//IL_747e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b25: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b35: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b62: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b64: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_5b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_54be: Unknown result type (might be due to invalid IL or missing references)
		//IL_54d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_5137: Unknown result type (might be due to invalid IL or missing references)
		//IL_513d: Unknown result type (might be due to invalid IL or missing references)
		//IL_514a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5158: Unknown result type (might be due to invalid IL or missing references)
		//IL_5172: Unknown result type (might be due to invalid IL or missing references)
		//IL_5177: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b40: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b45: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b58: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_359f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3589: Unknown result type (might be due to invalid IL or missing references)
		//IL_358e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3598: Unknown result type (might be due to invalid IL or missing references)
		//IL_70d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_70d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_70e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_70ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_7104: Unknown result type (might be due to invalid IL or missing references)
		//IL_7493: Unknown result type (might be due to invalid IL or missing references)
		//IL_6591: Unknown result type (might be due to invalid IL or missing references)
		//IL_565c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5661: Unknown result type (might be due to invalid IL or missing references)
		//IL_566d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5672: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ce5: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b73: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_6eeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ef9: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f12: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f72: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f80: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f99: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_6fab: Unknown result type (might be due to invalid IL or missing references)
		//IL_6fb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_6fb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_6fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_7016: Unknown result type (might be due to invalid IL or missing references)
		//IL_701b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7500: Unknown result type (might be due to invalid IL or missing references)
		//IL_750b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7510: Unknown result type (might be due to invalid IL or missing references)
		//IL_7515: Unknown result type (might be due to invalid IL or missing references)
		//IL_751a: Unknown result type (might be due to invalid IL or missing references)
		//IL_751f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7527: Unknown result type (might be due to invalid IL or missing references)
		//IL_752c: Unknown result type (might be due to invalid IL or missing references)
		//IL_7533: Unknown result type (might be due to invalid IL or missing references)
		//IL_7538: Unknown result type (might be due to invalid IL or missing references)
		//IL_753d: Unknown result type (might be due to invalid IL or missing references)
		//IL_753f: Unknown result type (might be due to invalid IL or missing references)
		//IL_754d: Unknown result type (might be due to invalid IL or missing references)
		//IL_7552: Unknown result type (might be due to invalid IL or missing references)
		//IL_6994: Unknown result type (might be due to invalid IL or missing references)
		//IL_65b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_65cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_65d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_65e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_65e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_65ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_6388: Unknown result type (might be due to invalid IL or missing references)
		//IL_6392: Unknown result type (might be due to invalid IL or missing references)
		//IL_6397: Unknown result type (might be due to invalid IL or missing references)
		//IL_60db: Unknown result type (might be due to invalid IL or missing references)
		//IL_60e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_60eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_60f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_60f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_60fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_6102: Unknown result type (might be due to invalid IL or missing references)
		//IL_6106: Unknown result type (might be due to invalid IL or missing references)
		//IL_610b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6117: Unknown result type (might be due to invalid IL or missing references)
		//IL_613d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6148: Unknown result type (might be due to invalid IL or missing references)
		//IL_568b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5684: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d16: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_6fe4: Unknown result type (might be due to invalid IL or missing references)
		//IL_6fdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_702f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7028: Unknown result type (might be due to invalid IL or missing references)
		//IL_7d8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_7b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_7b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_69ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_69d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_69da: Unknown result type (might be due to invalid IL or missing references)
		//IL_69e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_69ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_69ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_6613: Unknown result type (might be due to invalid IL or missing references)
		//IL_662f: Unknown result type (might be due to invalid IL or missing references)
		//IL_6634: Unknown result type (might be due to invalid IL or missing references)
		//IL_6639: Unknown result type (might be due to invalid IL or missing references)
		//IL_6601: Unknown result type (might be due to invalid IL or missing references)
		//IL_569b: Unknown result type (might be due to invalid IL or missing references)
		//IL_56a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_56ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_56b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_56bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_56c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_56d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_85ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_85f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_85f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_8606: Unknown result type (might be due to invalid IL or missing references)
		//IL_860c: Unknown result type (might be due to invalid IL or missing references)
		//IL_860e: Unknown result type (might be due to invalid IL or missing references)
		//IL_8615: Unknown result type (might be due to invalid IL or missing references)
		//IL_861f: Unknown result type (might be due to invalid IL or missing references)
		//IL_8624: Unknown result type (might be due to invalid IL or missing references)
		//IL_862c: Unknown result type (might be due to invalid IL or missing references)
		//IL_8631: Unknown result type (might be due to invalid IL or missing references)
		//IL_8633: Unknown result type (might be due to invalid IL or missing references)
		//IL_863e: Unknown result type (might be due to invalid IL or missing references)
		//IL_8643: Unknown result type (might be due to invalid IL or missing references)
		//IL_8648: Unknown result type (might be due to invalid IL or missing references)
		//IL_8652: Unknown result type (might be due to invalid IL or missing references)
		//IL_8657: Unknown result type (might be due to invalid IL or missing references)
		//IL_8662: Unknown result type (might be due to invalid IL or missing references)
		//IL_8667: Unknown result type (might be due to invalid IL or missing references)
		//IL_8671: Unknown result type (might be due to invalid IL or missing references)
		//IL_703e: Unknown result type (might be due to invalid IL or missing references)
		//IL_7074: Unknown result type (might be due to invalid IL or missing references)
		//IL_707f: Unknown result type (might be due to invalid IL or missing references)
		//IL_822b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_7dc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_7dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_7ddc: Unknown result type (might be due to invalid IL or missing references)
		//IL_7de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_7de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_7a7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_7a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_7a8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_7a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_7a96: Unknown result type (might be due to invalid IL or missing references)
		//IL_7a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7aa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_7aa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_7aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_7ab8: Unknown result type (might be due to invalid IL or missing references)
		//IL_7ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_7ae9: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_64f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_616f: Unknown result type (might be due to invalid IL or missing references)
		//IL_617a: Unknown result type (might be due to invalid IL or missing references)
		//IL_619f: Unknown result type (might be due to invalid IL or missing references)
		//IL_61aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_61af: Unknown result type (might be due to invalid IL or missing references)
		//IL_61b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_61b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_61c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_61c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_86c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_86b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_86b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_86c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_8251: Unknown result type (might be due to invalid IL or missing references)
		//IL_8267: Unknown result type (might be due to invalid IL or missing references)
		//IL_8271: Unknown result type (might be due to invalid IL or missing references)
		//IL_827c: Unknown result type (might be due to invalid IL or missing references)
		//IL_8281: Unknown result type (might be due to invalid IL or missing references)
		//IL_8286: Unknown result type (might be due to invalid IL or missing references)
		//IL_7e0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_7e29: Unknown result type (might be due to invalid IL or missing references)
		//IL_7e2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_7e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_7dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_7cee: Unknown result type (might be due to invalid IL or missing references)
		//IL_86e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_86ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_86f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_86fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_8700: Unknown result type (might be due to invalid IL or missing references)
		//IL_8705: Unknown result type (might be due to invalid IL or missing references)
		//IL_870f: Unknown result type (might be due to invalid IL or missing references)
		//IL_8714: Unknown result type (might be due to invalid IL or missing references)
		//IL_871f: Unknown result type (might be due to invalid IL or missing references)
		//IL_8724: Unknown result type (might be due to invalid IL or missing references)
		//IL_872e: Unknown result type (might be due to invalid IL or missing references)
		//IL_829b: Unknown result type (might be due to invalid IL or missing references)
		//IL_66fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_6709: Unknown result type (might be due to invalid IL or missing references)
		//IL_6896: Unknown result type (might be due to invalid IL or missing references)
		//IL_68a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_68b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_68c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_61db: Unknown result type (might be due to invalid IL or missing references)
		//IL_61f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_61f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_61fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_6200: Unknown result type (might be due to invalid IL or missing references)
		//IL_6214: Unknown result type (might be due to invalid IL or missing references)
		//IL_6219: Unknown result type (might be due to invalid IL or missing references)
		//IL_621b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6220: Unknown result type (might be due to invalid IL or missing references)
		//IL_622a: Unknown result type (might be due to invalid IL or missing references)
		//IL_622f: Unknown result type (might be due to invalid IL or missing references)
		//IL_6234: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d89: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_8784: Unknown result type (might be due to invalid IL or missing references)
		//IL_876e: Unknown result type (might be due to invalid IL or missing references)
		//IL_8773: Unknown result type (might be due to invalid IL or missing references)
		//IL_877d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_6a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_6916: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ee4: Unknown result type (might be due to invalid IL or missing references)
		//IL_5eef: Unknown result type (might be due to invalid IL or missing references)
		//IL_87a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_87c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_87cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_87e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_87ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_8804: Unknown result type (might be due to invalid IL or missing references)
		//IL_8812: Unknown result type (might be due to invalid IL or missing references)
		//IL_8817: Unknown result type (might be due to invalid IL or missing references)
		//IL_8830: Unknown result type (might be due to invalid IL or missing references)
		//IL_8835: Unknown result type (might be due to invalid IL or missing references)
		//IL_7ef8: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f03: Unknown result type (might be due to invalid IL or missing references)
		//IL_812e: Unknown result type (might be due to invalid IL or missing references)
		//IL_8091: Unknown result type (might be due to invalid IL or missing references)
		//IL_809d: Unknown result type (might be due to invalid IL or missing references)
		//IL_80ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_80c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_7730: Unknown result type (might be due to invalid IL or missing references)
		//IL_773b: Unknown result type (might be due to invalid IL or missing references)
		//IL_75db: Unknown result type (might be due to invalid IL or missing references)
		//IL_75e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_75fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_7602: Unknown result type (might be due to invalid IL or missing references)
		//IL_7609: Unknown result type (might be due to invalid IL or missing references)
		//IL_671f: Unknown result type (might be due to invalid IL or missing references)
		//IL_6724: Unknown result type (might be due to invalid IL or missing references)
		//IL_6732: Unknown result type (might be due to invalid IL or missing references)
		//IL_674b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6750: Unknown result type (might be due to invalid IL or missing references)
		//IL_6752: Unknown result type (might be due to invalid IL or missing references)
		//IL_6754: Unknown result type (might be due to invalid IL or missing references)
		//IL_675b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6760: Unknown result type (might be due to invalid IL or missing references)
		//IL_6765: Unknown result type (might be due to invalid IL or missing references)
		//IL_676c: Unknown result type (might be due to invalid IL or missing references)
		//IL_690f: Unknown result type (might be due to invalid IL or missing references)
		//IL_68f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_68fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_6908: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_5db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_5dcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_5dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_5dd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_5dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ddb: Unknown result type (might be due to invalid IL or missing references)
		//IL_5de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_5de5: Unknown result type (might be due to invalid IL or missing references)
		//IL_5dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c31: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c68: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_8111: Unknown result type (might be due to invalid IL or missing references)
		//IL_788c: Unknown result type (might be due to invalid IL or missing references)
		//IL_7897: Unknown result type (might be due to invalid IL or missing references)
		//IL_7766: Unknown result type (might be due to invalid IL or missing references)
		//IL_776b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7772: Unknown result type (might be due to invalid IL or missing references)
		//IL_761d: Unknown result type (might be due to invalid IL or missing references)
		//IL_7616: Unknown result type (might be due to invalid IL or missing references)
		//IL_6aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_6aab: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_6adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_6aec: Unknown result type (might be due to invalid IL or missing references)
		//IL_6af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_67ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_67f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_67fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_6801: Unknown result type (might be due to invalid IL or missing references)
		//IL_6806: Unknown result type (might be due to invalid IL or missing references)
		//IL_680b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6810: Unknown result type (might be due to invalid IL or missing references)
		//IL_6818: Unknown result type (might be due to invalid IL or missing references)
		//IL_681d: Unknown result type (might be due to invalid IL or missing references)
		//IL_6824: Unknown result type (might be due to invalid IL or missing references)
		//IL_6829: Unknown result type (might be due to invalid IL or missing references)
		//IL_682e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6830: Unknown result type (might be due to invalid IL or missing references)
		//IL_683e: Unknown result type (might be due to invalid IL or missing references)
		//IL_6843: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5cc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_833f: Unknown result type (might be due to invalid IL or missing references)
		//IL_835b: Unknown result type (might be due to invalid IL or missing references)
		//IL_8360: Unknown result type (might be due to invalid IL or missing references)
		//IL_8368: Unknown result type (might be due to invalid IL or missing references)
		//IL_8373: Unknown result type (might be due to invalid IL or missing references)
		//IL_8378: Unknown result type (might be due to invalid IL or missing references)
		//IL_837d: Unknown result type (might be due to invalid IL or missing references)
		//IL_8382: Unknown result type (might be due to invalid IL or missing references)
		//IL_8387: Unknown result type (might be due to invalid IL or missing references)
		//IL_838f: Unknown result type (might be due to invalid IL or missing references)
		//IL_8394: Unknown result type (might be due to invalid IL or missing references)
		//IL_839b: Unknown result type (might be due to invalid IL or missing references)
		//IL_83a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_83a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_83ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_83b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_83b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f45: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f55: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f66: Unknown result type (might be due to invalid IL or missing references)
		//IL_8150: Unknown result type (might be due to invalid IL or missing references)
		//IL_8156: Unknown result type (might be due to invalid IL or missing references)
		//IL_8174: Unknown result type (might be due to invalid IL or missing references)
		//IL_8182: Unknown result type (might be due to invalid IL or missing references)
		//IL_819b: Unknown result type (might be due to invalid IL or missing references)
		//IL_81a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_810a: Unknown result type (might be due to invalid IL or missing references)
		//IL_80f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_80f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_8103: Unknown result type (might be due to invalid IL or missing references)
		//IL_7786: Unknown result type (might be due to invalid IL or missing references)
		//IL_777f: Unknown result type (might be due to invalid IL or missing references)
		//IL_762c: Unknown result type (might be due to invalid IL or missing references)
		//IL_7631: Unknown result type (might be due to invalid IL or missing references)
		//IL_765f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7664: Unknown result type (might be due to invalid IL or missing references)
		//IL_766b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_6b95: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ba0: Unknown result type (might be due to invalid IL or missing references)
		//IL_6ba8: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_6be0: Unknown result type (might be due to invalid IL or missing references)
		//IL_6be5: Unknown result type (might be due to invalid IL or missing references)
		//IL_6be7: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_6bfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_67a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_67a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_67b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_679d: Unknown result type (might be due to invalid IL or missing references)
		//IL_685d: Unknown result type (might be due to invalid IL or missing references)
		//IL_685f: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e24: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e29: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e88: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_5cb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_5cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_5cc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_5caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d15: Unknown result type (might be due to invalid IL or missing references)
		//IL_5d17: Unknown result type (might be due to invalid IL or missing references)
		//IL_83cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_83c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_7fb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_7fec: Unknown result type (might be due to invalid IL or missing references)
		//IL_7ff7: Unknown result type (might be due to invalid IL or missing references)
		//IL_7ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_8001: Unknown result type (might be due to invalid IL or missing references)
		//IL_8006: Unknown result type (might be due to invalid IL or missing references)
		//IL_800b: Unknown result type (might be due to invalid IL or missing references)
		//IL_8013: Unknown result type (might be due to invalid IL or missing references)
		//IL_8018: Unknown result type (might be due to invalid IL or missing references)
		//IL_801f: Unknown result type (might be due to invalid IL or missing references)
		//IL_8024: Unknown result type (might be due to invalid IL or missing references)
		//IL_8029: Unknown result type (might be due to invalid IL or missing references)
		//IL_802b: Unknown result type (might be due to invalid IL or missing references)
		//IL_8039: Unknown result type (might be due to invalid IL or missing references)
		//IL_803e: Unknown result type (might be due to invalid IL or missing references)
		//IL_7795: Unknown result type (might be due to invalid IL or missing references)
		//IL_779a: Unknown result type (might be due to invalid IL or missing references)
		//IL_77c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_77cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_77d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_767f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7678: Unknown result type (might be due to invalid IL or missing references)
		//IL_6b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_6b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_6b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_6b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f14: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f42: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f47: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_83dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_83e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_840f: Unknown result type (might be due to invalid IL or missing references)
		//IL_8414: Unknown result type (might be due to invalid IL or missing references)
		//IL_841b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_7fa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_7fad: Unknown result type (might be due to invalid IL or missing references)
		//IL_7f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_8058: Unknown result type (might be due to invalid IL or missing references)
		//IL_805a: Unknown result type (might be due to invalid IL or missing references)
		//IL_77e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_77e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_768e: Unknown result type (might be due to invalid IL or missing references)
		//IL_7693: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f95: Unknown result type (might be due to invalid IL or missing references)
		//IL_842f: Unknown result type (might be due to invalid IL or missing references)
		//IL_8428: Unknown result type (might be due to invalid IL or missing references)
		//IL_78c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_78ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_78dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_78f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_78fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_78fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_78fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_77f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_77fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_76d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_76d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_5fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ff7: Unknown result type (might be due to invalid IL or missing references)
		//IL_5ffd: Unknown result type (might be due to invalid IL or missing references)
		//IL_5fff: Unknown result type (might be due to invalid IL or missing references)
		//IL_6004: Unknown result type (might be due to invalid IL or missing references)
		//IL_6012: Unknown result type (might be due to invalid IL or missing references)
		//IL_6014: Unknown result type (might be due to invalid IL or missing references)
		//IL_843e: Unknown result type (might be due to invalid IL or missing references)
		//IL_8443: Unknown result type (might be due to invalid IL or missing references)
		//IL_7948: Unknown result type (might be due to invalid IL or missing references)
		//IL_798a: Unknown result type (might be due to invalid IL or missing references)
		//IL_79a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_79a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_79aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_79af: Unknown result type (might be due to invalid IL or missing references)
		//IL_79bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_79bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_783b: Unknown result type (might be due to invalid IL or missing references)
		//IL_783d: Unknown result type (might be due to invalid IL or missing references)
		//IL_8490: Unknown result type (might be due to invalid IL or missing references)
		//IL_849b: Unknown result type (might be due to invalid IL or missing references)
		//IL_84a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_84af: Unknown result type (might be due to invalid IL or missing references)
		//IL_84b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_84c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_84c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_7941: Unknown result type (might be due to invalid IL or missing references)
		//IL_792b: Unknown result type (might be due to invalid IL or missing references)
		//IL_7930: Unknown result type (might be due to invalid IL or missing references)
		//IL_793a: Unknown result type (might be due to invalid IL or missing references)
		FrameType = FrameAnimationType.UpwardDraft;
		FrameChangeSpeed = 0.15f;
		CalamityGlobalNPC.SCal = base.NPC.whoAmI;
		HandleMusicVariables();
		bool wormAlive = false;
		if (CalamityGlobalNPC.SCalWorm != -1)
		{
			wormAlive = Main.npc[CalamityGlobalNPC.SCalWorm].active;
		}
		bool cataclysmAlive = false;
		if (CalamityGlobalNPC.SCalCataclysm != -1)
		{
			cataclysmAlive = Main.npc[CalamityGlobalNPC.SCalCataclysm].active;
		}
		bool catastropheAlive = false;
		if (CalamityGlobalNPC.SCalCatastrophe != -1)
		{
			catastropheAlive = Main.npc[CalamityGlobalNPC.SCalCatastrophe].active;
		}
		if (Main.slimeRain)
		{
			Main.StopSlimeRain();
			CalamityNetcode.SyncWorld();
		}
		if (CalamityServerConfig.Instance.BossesStopWeather)
		{
			CalamityWorld.StopRain();
		}
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool teleport = false;
		bool num = Main.zenithWorld && !permafrost;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		int bulletHellblast = (num ? ModContent.ProjectileType<BrimstoneWave>() : ModContent.ProjectileType<BrimstoneHellblast2>());
		int barrage = ModContent.ProjectileType<BrimstoneBarrage>();
		int gigablast = (num ? ModContent.ProjectileType<SCalBrimstoneFireblast>() : ModContent.ProjectileType<SCalBrimstoneGigablast>());
		int fireblast = (num ? ModContent.ProjectileType<SCalBrimstoneGigablast>() : ModContent.ProjectileType<SCalBrimstoneFireblast>());
		int wave = (num ? ModContent.ProjectileType<BrimstoneHellblast2>() : ModContent.ProjectileType<BrimstoneWave>());
		int hellblast = (num ? ModContent.ProjectileType<BrimstoneWave>() : ModContent.ProjectileType<BrimstoneHellblast>());
		int bodyWidth = 44;
		int bodyHeight = 42;
		int baseBulletHellProjectileGateValue = (revenge ? 8 : (expertMode ? 9 : 10));
		if (Main.getGoodWorld)
		{
			baseBulletHellProjectileGateValue -= 2;
		}
		Vector2 vectorCenter = base.NPC.Center;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (!startText)
		{
			if (!BossRushEvent.BossRushActive)
			{
				string key = "Mods.CalamityMod.Status.Boss.SCalSummonText";
				if (permafrost)
				{
					key = "Mods.CalamityMod.Status.Boss.PermafrostSummonText";
				}
				else if (DownedBossSystem.downedCalamitas)
				{
					key += "Rematch";
				}
				CalamityUtils.BroadcastLocalizedText(key, permafrost ? permafrostTextColor : textColor);
			}
			startText = true;
		}
		if (base.NPC.ai[1] != 2f && Math.Abs(player.Center.X - base.NPC.Center.X) > 16f)
		{
			base.NPC.spriteDirection = (player.Center.X < base.NPC.Center.X).ToDirectionInt();
		}
		rotateToPlayer = rotateToPlayer.AngleLerp((player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY).ToRotation() + (float)Math.PI / 2f, 0.04f);
		rotateAwayPlayer = rotateAwayPlayer.AngleLerp((player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY).ToRotation() - (float)Math.PI / 2f, 0.04f);
		if (hitTimer > 0)
		{
			hitTimer--;
		}
		if (base.NPC.dontTakeDamage && !hasDoneDeathAnim)
		{
			Vector2 sustVel = Utils.RotatedBy(new Vector2(-78f * Main.rand.NextFloat(0.95f, 1.05f), 0f), (double)(rotateToPlayer + (float)Math.PI / 2f), default(Vector2)).RotatedByRandom(1.4);
			Dust dust = Dust.NewDustPerfect(base.NPC.Center + sustVel, 269, sustVel * Main.rand.NextFloat(0.001f, 0.03f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 0.9f);
			dust.alpha = 200;
			dust.color = (Main.rand.NextBool() ? Color.Goldenrod : Color.Red);
		}
		Vector2 hitboxSize = default(Vector2);
		((Vector2)(ref hitboxSize))._002Ector(forcefieldScale * 216f / 1.4142f);
		hitboxSize = Vector2.Max(hitboxSize, new Vector2(42f, 44f));
		if (base.NPC.Size != hitboxSize)
		{
			base.NPC.Size = hitboxSize;
		}
		int num3;
		if (bulletHellCounter2 % 900 == 0 && attackCastDelay <= 0)
		{
			bool num2;
			if (!permafrost)
			{
				if (NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>()))
				{
					goto IL_0596;
				}
				num2 = NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>());
			}
			else
			{
				num2 = NPC.AnyNPCs(ModContent.NPCType<DevourerofGodsHead>());
			}
			if (!num2 && base.NPC.ai[0] != 1f)
			{
				num3 = ((base.NPC.ai[0] == 2f) ? 1 : 0);
				goto IL_0597;
			}
		}
		goto IL_0596;
		IL_8548:
		if (!canDespawn && !hasDoneDeathAnim && shieldOpacity >= 0.9f)
		{
			base.NPC.damage = base.NPC.defDamage;
			if (dashVisualCounter < 9)
			{
				dashVisualCounter++;
				return;
			}
			float sine = (float)Math.Sin(base.NPC.ai[2] * (0.975f * MathHelper.Clamp(Utils.GetLerpValue(120f, 0f, dashVisualCounter), 0.5f, 1.1f)) / (float)Math.PI);
			Vector2 offset = base.NPC.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 33f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.NPC.Center + offset - base.NPC.velocity.SafeNormalize(Vector2.UnitY) * 15f, -base.NPC.velocity * 0.85f, affectedByGravity: false, 10, 1.9f * MathHelper.Clamp(Utils.GetLerpValue(120f, 0f, dashVisualCounter), 0.5f, 1.1f), Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.5f)));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.NPC.Center - offset - base.NPC.velocity.SafeNormalize(Vector2.UnitY) * 15f, -base.NPC.velocity * 0.85f, affectedByGravity: false, 10, 1.9f * MathHelper.Clamp(Utils.GetLerpValue(120f, 0f, dashVisualCounter), 0.5f, 1.1f), Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.5f)));
			Dust dust2 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Circular(base.NPC.width, base.NPC.height), 182);
			dust2.noGravity = true;
			dust2.velocity = -base.NPC.velocity.RotatedByRandom(0.15000000596046448) * Main.rand.NextFloat(0.9f, 1.2f);
			dust2.scale = Main.rand.NextFloat(0.6f, 1.4f);
			dashVisualCounter++;
		}
		else
		{
			base.NPC.damage = 0;
			dashVisualCounter = 0;
		}
		return;
		IL_5345:
		if (lifeRatio <= 0.2f && !secondStage)
		{
			if (!BossRushEvent.BossRushActive)
			{
				string key2 = "Mods.CalamityMod.Status.Boss.SCalSeekerRingText";
				if (permafrost)
				{
					key2 = "Mods.CalamityMod.Status.Boss.PermafrostHallowBossSpamText";
				}
				else if (DownedBossSystem.downedCalamitas)
				{
					key2 += "Rematch";
				}
				CalamityUtils.BroadcastLocalizedText(key2, permafrost ? permafrostTextColor : textColor);
			}
			if (Main.netMode != 1)
			{
				if (permafrost)
				{
					int npc = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)vectorCenter.X, (int)vectorCenter.Y, ModContent.NPCType<global::CalamityMod.NPCs.Cryogen.Cryogen>(), base.NPC.whoAmI);
					Main.npc[npc].timeLeft *= 20;
					Main.npc[npc].lifeMax = (Main.npc[npc].life *= 22);
					CalamityUtils.BossAwakenMessage(npc);
				}
				else
				{
					SoundEngine.PlaySound(in SoundID.Item74, base.NPC.Center);
					int totalSeekers = (Main.getGoodWorld ? 20 : 10);
					int degreesBetweenEachSeeker = 360 / totalSeekers;
					int distanceFromSCal = (Main.getGoodWorld ? 300 : 225);
					for (int i = 0; i < totalSeekers; i++)
					{
						int FireEye = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)vectorCenter.X + Math.Sin(i * degreesBetweenEachSeeker) * (double)distanceFromSCal), (int)((double)vectorCenter.Y + Math.Cos(i * degreesBetweenEachSeeker) * (double)distanceFromSCal), ModContent.NPCType<SoulSeekerSupreme>(), base.NPC.whoAmI, 0f, 0f, 0f, -1f);
						NPC obj = Main.npc[FireEye];
						obj.ai[0] = i * degreesBetweenEachSeeker;
						obj.ai[3] = i * degreesBetweenEachSeeker;
					}
				}
			}
			SoundEngine.PlaySound(in SoundID.DD2_DarkMageHealImpact, player.Center);
			secondStage = true;
		}
		if (bossLife == 0f && base.NPC.life > 0)
		{
			bossLife = base.NPC.lifeMax;
		}
		if (base.NPC.life > 0)
		{
			if (attackPause > 0)
			{
				attackPause--;
			}
			if (lifeRatio < 0.45f && !enteredBrothersPhase)
			{
				attackPause = 5;
				if (preventionPause != 0)
				{
					preventionPause--;
					base.NPC.dontTakeDamage = true;
					NPC nPC = base.NPC;
					nPC.velocity *= 0.85f;
					return;
				}
				enteredBrothersPhase = true;
				attackCastDelay = 150;
				base.NPC.netUpdate = true;
				if (!teleport)
				{
					Vector2 goalPos = default(Vector2);
					((Vector2)(ref goalPos))._002Ector((float)(spawnX + (death ? 1000 : 1250)), (float)(spawnY + (death ? 1000 : 1250)));
					Dust.QuickDustLine(base.NPC.Center, goalPos + new Vector2(0f, -20f), 500f, permafrost ? Color.Cyan : Color.Red);
					base.NPC.velocity = Vector2.Zero;
					base.NPC.Center = goalPos;
					GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, Color.Red, new Vector2(1f, 1f), 0f, 0.1f, 5f, 15));
					for (int x = 0; x < Main.maxProjectiles; x++)
					{
						Projectile projectile = Main.projectile[x];
						if (!projectile.active)
						{
							continue;
						}
						if (projectile.type == bulletHellblast || projectile.type == barrage || projectile.type == wave)
						{
							if (projectile.timeLeft > 60)
							{
								projectile.timeLeft = 60;
							}
						}
						else if (projectile.type == fireblast || projectile.type == gigablast)
						{
							projectile.ai[2] = 1f;
							if (projectile.timeLeft > 15)
							{
								projectile.timeLeft = 15;
							}
						}
					}
					teleport = true;
				}
			}
		}
		if (base.NPC.ai[0] == 0f)
		{
			if (wormAlive)
			{
				base.NPC.dontTakeDamage = true;
				base.NPC.chaseable = false;
			}
			else
			{
				if (cataclysmAlive | catastropheAlive)
				{
					base.NPC.dontTakeDamage = true;
					base.NPC.chaseable = false;
					base.NPC.damage = 0;
					attackPause = 5;
					if (!canDespawn)
					{
						NPC nPC2 = base.NPC;
						nPC2.velocity *= 0.95f;
					}
					return;
				}
				base.NPC.dontTakeDamage = false;
				base.NPC.chaseable = true;
			}
			if (base.NPC.ai[1] == -1f)
			{
				phaseChange++;
				if (phaseChange > 23)
				{
					phaseChange = 0;
				}
				int phase = 0;
				switch (phaseChange)
				{
				case 0:
					phase = 0;
					willCharge = false;
					break;
				case 1:
					phase = 3;
					break;
				case 2:
					phase = 4;
					willCharge = true;
					break;
				case 3:
					phase = 1;
					break;
				case 4:
					phase = 1;
					break;
				case 5:
					phase = 4;
					willCharge = false;
					break;
				case 6:
					phase = 3;
					break;
				case 7:
					phase = 0;
					willCharge = true;
					break;
				case 8:
					phase = 1;
					break;
				case 9:
					phase = 0;
					willCharge = false;
					break;
				case 10:
					phase = 3;
					break;
				case 11:
					phase = 4;
					break;
				case 12:
					phase = 4;
					break;
				case 13:
					phase = 3;
					willCharge = true;
					break;
				case 14:
					phase = 1;
					break;
				case 15:
					phase = 0;
					willCharge = false;
					break;
				case 16:
					phase = 4;
					break;
				case 17:
					phase = 4;
					willCharge = true;
					break;
				case 18:
					phase = 1;
					break;
				case 19:
					phase = 1;
					break;
				case 20:
					phase = 0;
					break;
				case 21:
					phase = 1;
					break;
				case 22:
					phase = 0;
					break;
				case 23:
					phase = 1;
					break;
				}
				base.NPC.ai[1] = phase;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
			}
			else if (base.NPC.ai[1] == 0f)
			{
				base.NPC.damage = 0;
				float velocity = 12f;
				float acceleration = 0.12f;
				if (Main.getGoodWorld)
				{
					velocity *= 1.15f;
					acceleration *= 1.15f;
				}
				Vector2 distanceFromDestination = new Vector2(player.Center.X, player.Center.Y - 550f) - base.NPC.Center;
				if (!canDespawn)
				{
					CalamityUtils.SmoothMovement(base.NPC, 0f, distanceFromDestination, velocity, acceleration, useSimpleFlyMovement: true);
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 300f)
				{
					base.NPC.ai[1] = -1f;
					fireFireblastFirst = true;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
				Vector2 projectileVelocity = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
				Vector2 projectileSpawn = base.NPC.Center + projectileVelocity * 8f;
				projectileVelocity *= 10f * uDieLul;
				base.NPC.localAI[1] += (wormAlive ? 0.5f : 1f);
				if (base.NPC.localAI[1] > 90f)
				{
					base.NPC.localAI[1] = 0f;
					int randomShot = Main.rand.Next(6);
					if (randomShot == 0 && canFireSplitingFireball && !fireFireblastFirst)
					{
						canFireSplitingFireball = false;
						randomShot = gigablast;
						SoundEngine.PlaySound(in BrimstoneBigShotSound, base.NPC.Center);
						for (int j = 0; j < 9; j++)
						{
							Vector2 velOffset = base.NPC.DirectionTo(player.Center).RotatedByRandom(0.6) * Main.rand.NextFloat(5f, 13f);
							GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(projectileSpawn + velOffset * 2f, velOffset * 0.7f, affectedByGravity: false, 30, Main.rand.NextFloat(0.4f, 0.65f), permafrost ? Color.LightBlue : (Main.rand.NextBool(3) ? Color.Lerp(Color.Red, Color.Magenta, 0.3f) : Color.Red)));
						}
						if (Main.netMode != 1 && attackPause == 0)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn, projectileVelocity, randomShot, GigablastDamage, 0f, Main.myPlayer, 0f, 2f);
							base.NPC.netUpdate = true;
						}
					}
					else if ((randomShot == 1 && canFireSplitingFireball) || fireFireblastFirst)
					{
						canFireSplitingFireball = false;
						randomShot = fireblast;
						SoundEngine.PlaySound(in BrimstoneShotSound, base.NPC.Center);
						for (int k = 0; k < 9; k++)
						{
							Vector2 velOffset2 = base.NPC.DirectionTo(player.Center).RotatedByRandom(0.6) * Main.rand.NextFloat(5f, 13f);
							GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(projectileSpawn + velOffset2 * 2f, velOffset2 * 0.8f, affectedByGravity: false, 30, Main.rand.NextFloat(0.4f, 0.65f), permafrost ? Color.LightBlue : (Main.rand.NextBool(3) ? Color.Lerp(Color.Red, Color.Magenta, 0.3f) : Color.Red)));
						}
						if (Main.netMode != 1 && attackPause == 0)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn, projectileVelocity, randomShot, FireblastDamage, 0f, Main.myPlayer, 0f, 2f);
							base.NPC.netUpdate = true;
						}
					}
					else if (!fireFireblastFirst)
					{
						canFireSplitingFireball = true;
						randomShot = barrage;
						SoundEngine.PlaySound(in BrimstoneBigShotSound, base.NPC.Center);
						float rotation = MathHelper.ToRadians(20f);
						int numProj = 8;
						for (int l = 0; l < numProj; l++)
						{
							for (int m = 0; m < 6; m++)
							{
								Vector2 dustVel = (projectileVelocity * 2f).RotatedByRandom(0.9) * Main.rand.NextFloat(0.5f, 1.9f);
								GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(projectileSpawn, dustVel, affectedByGravity: false, 15, Main.rand.NextFloat(0.65f, 0.9f), permafrost ? Color.Cyan : (Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.3f))));
							}
							if (Main.netMode != 1 && attackPause == 0)
							{
								float projectileVelocityToPass = ((Vector2)(ref projectileVelocity)).Length() * 1.3f;
								Vector2 perturbedSpeed = projectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)l / (float)(numProj - 1)));
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn, perturbedSpeed, randomShot, DartDamage, 0f, Main.myPlayer, 0f, 4f, projectileVelocityToPass);
								base.NPC.netUpdate = true;
							}
						}
					}
					fireFireblastFirst = false;
				}
				FrameType = FrameAnimationType.FasterUpwardDraft;
			}
			else if (base.NPC.ai[1] == 1f)
			{
				base.NPC.damage = base.NPC.defDamage;
				float chargeVelocity = (wormAlive ? 26f : 30f) + (1f - lifeRatio) * 8f;
				if (Main.getGoodWorld)
				{
					chargeVelocity *= 1.15f;
				}
				if (!canDespawn)
				{
					Vector2 vector = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
					base.NPC.velocity = vector * chargeVelocity;
					shieldRotation = base.NPC.velocity.ToRotation();
					base.NPC.netUpdate = true;
					SoundEngine.PlaySound(in DashSound, base.NPC.Center);
					if (permafrost && Main.netMode != 1)
					{
						SoundEngine.PlaySound(in SoundID.Item60, base.NPC.Center);
						float velocity2 = 8f;
						int type = ModContent.ProjectileType<DarkIceZero>();
						int damage = base.NPC.damage / 3;
						Vector2 projectileVelocity2 = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY) * velocity2;
						float rotation2 = MathHelper.ToRadians(22f);
						for (int n = 0; n < 3; n++)
						{
							Vector2 perturbedSpeed2 = projectileVelocity2.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)n / 2f));
							int p = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + perturbedSpeed2.SafeNormalize(Vector2.UnitY) * 3f, perturbedSpeed2, type, damage, 0f, Main.myPlayer, 0f, 2f);
							if (p.WithinBounds(Main.maxProjectiles))
							{
								Main.projectile[p].DamageType = DamageClass.Default;
								Main.projectile[p].friendly = false;
								Main.projectile[p].hostile = true;
								Main.projectile[p].tileCollide = true;
							}
						}
					}
				}
				base.NPC.ai[1] = 2f;
			}
			else if (base.NPC.ai[1] == 2f)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.ai[2]++;
				if (Math.Abs(base.NPC.velocity.X) > 0.15f)
				{
					base.NPC.spriteDirection = (base.NPC.velocity.X < 0f).ToDirectionInt();
				}
				if (base.NPC.ai[2] >= 25f)
				{
					base.NPC.damage = 0;
					if (!canDespawn)
					{
						NPC nPC3 = base.NPC;
						nPC3.velocity *= 0.96f;
						if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
						{
							base.NPC.velocity.X = 0f;
						}
						if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
						{
							base.NPC.velocity.Y = 0f;
						}
					}
				}
				bool willChargeAgain = base.NPC.ai[3] + 1f < 2f;
				if (base.NPC.ai[2] >= 70f)
				{
					base.NPC.damage = 0;
					base.NPC.ai[3]++;
					base.NPC.ai[2] = 0f;
					base.NPC.TargetClosest();
					if (!willChargeAgain)
					{
						base.NPC.ai[1] = -1f;
					}
					else
					{
						base.NPC.ai[1] = 1f;
					}
				}
				if (willChargeAgain && base.NPC.ai[2] > 50f)
				{
					float idealRotation = base.NPC.AngleTo(player.Center);
					shieldRotation = shieldRotation.AngleLerp(idealRotation, 0.125f);
					shieldRotation = shieldRotation.AngleTowards(idealRotation, 0.18f);
				}
				FrameType = FrameAnimationType.FasterUpwardDraft;
			}
			else if (base.NPC.ai[1] == 3f)
			{
				base.NPC.damage = 0;
				float velocity3 = 32f;
				float acceleration2 = 1.2f;
				if (Main.getGoodWorld)
				{
					velocity3 *= 1.15f;
					acceleration2 *= 1.15f;
				}
				int posX = 1;
				if (base.NPC.Center.X < player.position.X + (float)player.width)
				{
					posX = -1;
				}
				Vector2 distanceFromDestination2 = new Vector2(player.Center.X + (float)posX * 600f, player.Center.Y) - base.NPC.Center;
				if (!canDespawn)
				{
					CalamityUtils.SmoothMovement(base.NPC, 0f, distanceFromDestination2, velocity3, acceleration2, useSimpleFlyMovement: true);
				}
				Vector2 handPosition = base.NPC.Center + new Vector2((float)base.NPC.spriteDirection * -18f, 2f);
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 480f)
				{
					base.NPC.ai[1] = -1f;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
				else
				{
					if (!player.dead)
					{
						base.NPC.ai[3] += (wormAlive ? 0.5f : 1f);
					}
					if (base.NPC.ai[3] >= 20f)
					{
						base.NPC.ai[3] = 0f;
						SoundEngine.PlaySound(in HellblastSound, base.NPC.Center);
						for (int num4 = 0; num4 < 6; num4++)
						{
							Vector2 velOffset3 = base.NPC.DirectionTo(player.Center).RotatedByRandom(0.6) * Main.rand.NextFloat(5f, 13f);
							GeneralParticleHandler.SpawnParticle(new PointParticle(handPosition + velOffset3 * 2f, velOffset3 * 1.5f, affectedByGravity: false, 18, Main.rand.NextFloat(0.4f, 0.65f), permafrost ? Color.Cyan : (Main.rand.NextBool(3) ? Color.Lerp(Color.Red, Color.Magenta, 0.3f) : Color.Red)));
						}
						if (Main.netMode != 1)
						{
							Vector2 projectileVelocity3 = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
							Vector2 projectileSpawn2 = base.NPC.Center + projectileVelocity3 * 4f;
							projectileVelocity3 *= 10f * uDieLul;
							int projectileType = hellblast;
							if (attackPause == 0)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn2, projectileVelocity3, projectileType, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
							}
						}
					}
				}
				if (Main.rand.NextBool())
				{
					GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(handPosition, Utils.RotatedByRandom(new Vector2(0f, -6f), 0.4) * Main.rand.NextFloat(0.8f, 1.4f), affectedByGravity: false, 15, Main.rand.NextFloat(0.85f, 1.2f), permafrost ? Color.LightBlue : (Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.3f)), AddativeBlend: true, needed: true));
				}
				FrameType = FrameAnimationType.OutwardHandCast;
			}
			else if (base.NPC.ai[1] == 4f)
			{
				base.NPC.damage = 0;
				float velocity4 = 32f;
				float acceleration3 = 1.2f;
				if (Main.getGoodWorld)
				{
					velocity4 *= 1.15f;
					acceleration3 *= 1.15f;
				}
				int posX2 = 1;
				if (base.NPC.Center.X < player.position.X + (float)player.width)
				{
					posX2 = -1;
				}
				Vector2 distanceFromDestination3 = new Vector2(player.Center.X + (float)posX2 * 750f, player.Center.Y) - base.NPC.Center;
				if (!canDespawn)
				{
					CalamityUtils.SmoothMovement(base.NPC, 0f, distanceFromDestination3, velocity4, acceleration3, useSimpleFlyMovement: true);
				}
				int shootRate = (wormAlive ? 280 : 140);
				base.NPC.localAI[1]++;
				FrameChangeSpeed = 0.175f;
				FrameType = FrameAnimationType.BlastCast;
				if (base.NPC.localAI[1] > (float)shootRate)
				{
					Vector2 handPosition2 = base.NPC.Center + new Vector2((float)base.NPC.spriteDirection * -22f, 2f);
					for (int num5 = 0; num5 < 25; num5++)
					{
						Vector2 velOffset4 = base.NPC.DirectionTo(player.Center).RotatedByRandom(0.6) * Main.rand.NextFloat(5f, 13f);
						GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(handPosition2 + velOffset4 * 2f, velOffset4 * 1.5f, affectedByGravity: false, 9, Main.rand.NextFloat(0.4f, 0.65f), permafrost ? Color.Cyan : (Main.rand.NextBool(3) ? Color.Lerp(Color.Red, Color.Magenta, 0.3f) : Color.Red)));
					}
					base.NPC.localAI[1] = 0f;
					if (Main.netMode != 1)
					{
						SoundEngine.PlaySound(in BrimstoneBigShotSound, base.NPC.Center);
						Vector2 projectileVelocity4 = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
						Vector2 projectileSpawn3 = base.NPC.Center + projectileVelocity4 * 8f;
						projectileVelocity4 *= 5f * uDieLul;
						int projectileType2 = gigablast;
						if (attackPause == 0)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn3, projectileVelocity4, projectileType2, GigablastDamage, 0f, Main.myPlayer, 0f, 2f);
						}
					}
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 300f)
				{
					base.NPC.ai[1] = -1f;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
			}
			if (lifeRatio <= 0.45f && hasSummonedBrothers)
			{
				bool num6;
				if (!permafrost)
				{
					if (NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>()))
					{
						goto IL_8548;
					}
					num6 = NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>());
				}
				else
				{
					num6 = NPC.AnyNPCs(ModContent.NPCType<DevourerofGodsHead>());
				}
				if (!num6)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
			}
		}
		else if (base.NPC.ai[0] == 1f || base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			base.NPC.dontTakeDamage = true;
			base.NPC.chaseable = false;
			if (base.NPC.ai[0] == 1f)
			{
				base.NPC.ai[2] += 0.005f;
				if ((double)base.NPC.ai[2] > 0.5)
				{
					base.NPC.ai[2] = 0.5f;
				}
			}
			else
			{
				base.NPC.ai[2] -= 0.005f;
				if (base.NPC.ai[2] < 0f)
				{
					base.NPC.ai[2] = 0f;
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] == 100f)
			{
				base.NPC.ai[0]++;
				base.NPC.ai[1] = 0f;
				if (base.NPC.ai[0] == 3f)
				{
					base.NPC.ai[2] = 0f;
				}
				else
				{
					for (int num7 = 0; num7 < 90; num7++)
					{
						Dust dust3 = Dust.NewDustPerfect(base.NPC.Center, permafrost ? 161 : 235, Utils.RotatedByRandom(new Vector2(30f, 30f), 100.0) * Main.rand.NextFloat(0.05f, 1.2f));
						dust3.noGravity = true;
						dust3.scale = Main.rand.NextFloat(1.2f, 2.3f);
					}
					for (int num8 = 0; num8 < 40; num8++)
					{
						Vector2 sparkVel = Utils.RotatedByRandom(new Vector2(20f, 20f), 100.0) * Main.rand.NextFloat(0.1f, 1.1f);
						GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.NPC.Center + sparkVel * 2f, sparkVel, affectedByGravity: false, 120, Main.rand.NextFloat(1.55f, 2.75f), permafrost ? Color.LightBlue : Color.Red, AddativeBlend: true, needed: true));
					}
					GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.LightBlue : Color.Red, new Vector2(2f, 2f), 0f, 0f, 1.1f, 25));
					SoundEngine.PlaySound(in SpawnSound, base.NPC.Center);
				}
			}
			for (int num9 = 0; num9 < 4; num9++)
			{
				Dust dust4 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Square(-24f, 24f), permafrost ? 161 : 235);
				dust4.velocity = Vector2.UnitY * (0f - Main.rand.NextFloat(2.75f, 4.25f));
				dust4.noGravity = true;
			}
			if (!canDespawn)
			{
				NPC nPC4 = base.NPC;
				nPC4.velocity *= 0.98f;
				if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
				{
					base.NPC.velocity.X = 0f;
				}
				if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
				{
					base.NPC.velocity.Y = 0f;
				}
			}
		}
		else
		{
			if (wormAlive)
			{
				base.NPC.dontTakeDamage = true;
				base.NPC.chaseable = false;
			}
			else if (permafrost ? NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.Providence.Providence>()) : NPC.AnyNPCs(ModContent.NPCType<SoulSeekerSupreme>()))
			{
				base.NPC.dontTakeDamage = true;
				base.NPC.chaseable = false;
			}
			else
			{
				base.NPC.dontTakeDamage = false;
				base.NPC.chaseable = true;
			}
			if (base.NPC.ai[1] == -1f)
			{
				phaseChange++;
				if (phaseChange > 23)
				{
					phaseChange = 0;
				}
				int phase2 = 0;
				switch (phaseChange)
				{
				case 0:
					phase2 = 0;
					willCharge = false;
					break;
				case 1:
					phase2 = 3;
					break;
				case 2:
					phase2 = 4;
					willCharge = true;
					break;
				case 3:
					phase2 = 1;
					break;
				case 4:
					phase2 = 1;
					break;
				case 5:
					phase2 = 4;
					willCharge = false;
					break;
				case 6:
					phase2 = 3;
					break;
				case 7:
					phase2 = 0;
					willCharge = true;
					break;
				case 8:
					phase2 = 1;
					break;
				case 9:
					phase2 = 0;
					willCharge = false;
					break;
				case 10:
					phase2 = 3;
					break;
				case 11:
					phase2 = 4;
					break;
				case 12:
					phase2 = 4;
					break;
				case 13:
					phase2 = 3;
					willCharge = true;
					break;
				case 14:
					phase2 = 1;
					break;
				case 15:
					phase2 = 0;
					willCharge = false;
					break;
				case 16:
					phase2 = 4;
					break;
				case 17:
					phase2 = 4;
					willCharge = true;
					break;
				case 18:
					phase2 = 1;
					break;
				case 19:
					phase2 = 1;
					break;
				case 20:
					phase2 = 0;
					break;
				case 21:
					phase2 = 1;
					break;
				case 22:
					phase2 = 0;
					break;
				case 23:
					phase2 = 1;
					break;
				}
				base.NPC.ai[1] = phase2;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
			}
			else if (base.NPC.ai[1] == 0f)
			{
				base.NPC.damage = 0;
				float velocity5 = 12f;
				float acceleration4 = 0.12f;
				if (Main.getGoodWorld)
				{
					velocity5 *= 1.15f;
					acceleration4 *= 1.15f;
				}
				Vector2 distanceFromDestination4 = new Vector2(player.Center.X, player.Center.Y - 550f) - base.NPC.Center;
				if (!canDespawn)
				{
					CalamityUtils.SmoothMovement(base.NPC, 0f, distanceFromDestination4, velocity5, acceleration4, useSimpleFlyMovement: true);
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 240f)
				{
					base.NPC.ai[1] = -1f;
					fireFireblastFirst = true;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
				Vector2 projectileVelocity5 = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
				Vector2 projectileSpawn4 = base.NPC.Center + projectileVelocity5 * 8f;
				projectileVelocity5 *= 10f * uDieLul;
				base.NPC.localAI[1] += (wormAlive ? 0.5f : 1f);
				if (base.NPC.localAI[1] > 60f)
				{
					base.NPC.localAI[1] = 0f;
					int randomShot2 = Main.rand.Next(6);
					if (randomShot2 == 0 && canFireSplitingFireball && !fireFireblastFirst)
					{
						SoundEngine.PlaySound(in BrimstoneBigShotSound, base.NPC.Center);
						canFireSplitingFireball = false;
						randomShot2 = gigablast;
						GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, projectileVelocity5 * 1.2f, permafrost ? Color.Cyan : Color.Red, new Vector2(0.5f, 1f), projectileVelocity5.ToRotation(), 0.92f, 0f, 55));
						GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, projectileVelocity5 * 1f, permafrost ? Color.Cyan : Color.Magenta, new Vector2(0.5f, 1f), projectileVelocity5.ToRotation(), 0.95f, 0.4f, 55));
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn4, projectileVelocity5, randomShot2, GigablastDamage, 0f, Main.myPlayer, 0f, 2f);
							base.NPC.netUpdate = true;
						}
					}
					else if ((randomShot2 == 1 && canFireSplitingFireball) || fireFireblastFirst)
					{
						SoundEngine.PlaySound(in BrimstoneShotSound, base.NPC.Center);
						if (base.NPC.ai[2] > 1f)
						{
							canFireSplitingFireball = false;
						}
						randomShot2 = fireblast;
						GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, projectileVelocity5 * 1.2f, permafrost ? Color.Cyan : Color.Red, new Vector2(0.5f, 1f), projectileVelocity5.ToRotation(), 0.95f, 0f, 55));
						GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, projectileVelocity5 * 1f, permafrost ? Color.Cyan : Color.Magenta, new Vector2(0.5f, 1f), projectileVelocity5.ToRotation(), 0.98f, 0.4f, 55));
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn4, projectileVelocity5, randomShot2, FireblastDamage, 0f, Main.myPlayer, 0f, 2f);
							base.NPC.netUpdate = true;
						}
					}
					else if (!fireFireblastFirst)
					{
						SoundEngine.PlaySound(in BrimstoneBigShotSound, base.NPC.Center);
						canFireSplitingFireball = true;
						randomShot2 = barrage;
						float rotation3 = MathHelper.ToRadians(20f);
						int numProj2 = 8;
						for (int num10 = 0; num10 < numProj2; num10++)
						{
							for (int num11 = 0; num11 < 7; num11++)
							{
								Vector2 dustVel2 = (projectileVelocity5 * 2f).RotatedByRandom(0.9) * Main.rand.NextFloat(0.5f, 1.9f);
								GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(projectileSpawn4, dustVel2, affectedByGravity: false, 15, Main.rand.NextFloat(0.75f, 1f), permafrost ? Color.LightBlue : (Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.3f)), AddativeBlend: true, needed: true));
							}
							if (Main.netMode != 1)
							{
								float projectileVelocityToPass2 = ((Vector2)(ref projectileVelocity5)).Length() * 1.3f;
								Vector2 perturbedSpeed3 = projectileVelocity5.RotatedBy(MathHelper.Lerp(0f - rotation3, rotation3, (float)num10 / (float)(numProj2 - 1)));
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn4, perturbedSpeed3, randomShot2, DartDamage, 0f, Main.myPlayer, 0f, 4f, projectileVelocityToPass2);
								base.NPC.netUpdate = true;
							}
						}
					}
					fireFireblastFirst = false;
				}
			}
			else if (base.NPC.ai[1] == 1f)
			{
				base.NPC.damage = base.NPC.defDamage;
				float chargeVelocity2 = (wormAlive ? 26f : 30f) + (1f - lifeRatio) * 8f;
				if (Main.getGoodWorld)
				{
					chargeVelocity2 *= 1.15f;
				}
				if (!canDespawn)
				{
					Vector2 vector2 = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
					base.NPC.velocity = vector2 * chargeVelocity2;
					shieldRotation = base.NPC.velocity.ToRotation();
					base.NPC.netUpdate = true;
					SoundEngine.PlaySound(in DashSound, base.NPC.Center);
				}
				base.NPC.ai[1] = 2f;
			}
			else if (base.NPC.ai[1] == 2f)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 25f)
				{
					base.NPC.damage = 0;
					if (!canDespawn)
					{
						NPC nPC5 = base.NPC;
						nPC5.velocity *= 0.96f;
						if ((double)base.NPC.velocity.X > -0.1 && (double)base.NPC.velocity.X < 0.1)
						{
							base.NPC.velocity.X = 0f;
						}
						if ((double)base.NPC.velocity.Y > -0.1 && (double)base.NPC.velocity.Y < 0.1)
						{
							base.NPC.velocity.Y = 0f;
						}
					}
				}
				bool willChargeAgain2 = base.NPC.ai[3] + 1f < 1f;
				if (base.NPC.ai[2] >= 70f)
				{
					base.NPC.damage = 0;
					base.NPC.ai[3]++;
					base.NPC.ai[2] = 0f;
					base.NPC.TargetClosest();
					if (!willChargeAgain2)
					{
						base.NPC.ai[1] = -1f;
					}
					else
					{
						base.NPC.ai[1] = 1f;
					}
				}
				if (willChargeAgain2 && base.NPC.ai[2] > 50f)
				{
					float idealRotation2 = base.NPC.AngleTo(player.Center);
					shieldRotation = shieldRotation.AngleLerp(idealRotation2, 0.125f);
					shieldRotation = shieldRotation.AngleTowards(idealRotation2, 0.18f);
				}
				FrameType = FrameAnimationType.FasterUpwardDraft;
			}
			else if (base.NPC.ai[1] == 3f)
			{
				base.NPC.damage = 0;
				float velocity6 = 32f;
				float acceleration5 = 1.2f;
				if (Main.getGoodWorld)
				{
					velocity6 *= 1.15f;
					acceleration5 *= 1.15f;
				}
				int posX3 = 1;
				if (base.NPC.Center.X < player.position.X + (float)player.width)
				{
					posX3 = -1;
				}
				Vector2 distanceFromDestination5 = new Vector2(player.Center.X + (float)posX3 * 600f, player.Center.Y) - base.NPC.Center;
				if (!canDespawn)
				{
					CalamityUtils.SmoothMovement(base.NPC, 0f, distanceFromDestination5, velocity6, acceleration5, useSimpleFlyMovement: true);
				}
				Vector2 handPosition3 = base.NPC.Center + new Vector2((float)base.NPC.spriteDirection * -18f, 2f);
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 300f)
				{
					base.NPC.ai[1] = -1f;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
				else
				{
					if (!player.dead)
					{
						base.NPC.ai[3] += (wormAlive ? 0.5f : 1f);
					}
					if (base.NPC.ai[3] >= 24f)
					{
						base.NPC.ai[3] = 0f;
						SoundEngine.PlaySound(in HellblastSound, base.NPC.Center);
						for (int num12 = 0; num12 < 9; num12++)
						{
							Vector2 velOffset5 = base.NPC.DirectionTo(player.Center).RotatedByRandom(0.6) * Main.rand.NextFloat(5f, 13f);
							GeneralParticleHandler.SpawnParticle(new PointParticle(handPosition3 + velOffset5 * 2f, velOffset5 * 1.5f, affectedByGravity: false, 9, Main.rand.NextFloat(0.5f, 0.75f), permafrost ? Color.Cyan : (Main.rand.NextBool(3) ? Color.Lerp(Color.Red, Color.Magenta, 0.3f) : Color.Red)));
						}
						if (Main.netMode != 1)
						{
							Vector2 projectileVelocity6 = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
							Vector2 projectileSpawn5 = base.NPC.Center + projectileVelocity6 * 4f;
							projectileVelocity6 *= 10f * uDieLul;
							int projectileType3 = hellblast;
							if (attackPause == 0)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn5, projectileVelocity6, projectileType3, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
							}
						}
					}
				}
				if (Main.rand.NextBool())
				{
					GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(handPosition3, Utils.RotatedByRandom(new Vector2(0f, -6f), 0.4) * Main.rand.NextFloat(0.8f, 1.4f), affectedByGravity: false, 15, Main.rand.NextFloat(0.95f, 1.45f), permafrost ? Color.LightBlue : (Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.5f)), AddativeBlend: true, needed: true));
				}
				Dust dust5 = Dust.NewDustPerfect(handPosition3, Main.rand.NextBool(3) ? 60 : 114);
				dust5.noGravity = true;
				dust5.velocity = Utils.RotatedByRandom(new Vector2(0f, -6f), 0.4) * Main.rand.NextFloat(0.8f, 1.4f);
				dust5.scale = Main.rand.NextFloat(0.3f, 0.65f);
				FrameChangeSpeed = 0.245f;
				FrameType = FrameAnimationType.PunchHandCast;
			}
			else if (base.NPC.ai[1] == 4f)
			{
				base.NPC.damage = 0;
				float velocity7 = 32f;
				float acceleration6 = 1.2f;
				if (Main.getGoodWorld)
				{
					velocity7 *= 1.15f;
					acceleration6 *= 1.15f;
				}
				int posX4 = 1;
				if (base.NPC.Center.X < player.position.X + (float)player.width)
				{
					posX4 = -1;
				}
				Vector2 distanceFromDestination6 = new Vector2(player.Center.X + (float)posX4 * 750f, player.Center.Y) - base.NPC.Center;
				if (!canDespawn)
				{
					CalamityUtils.SmoothMovement(base.NPC, 0f, distanceFromDestination6, velocity7, acceleration6, useSimpleFlyMovement: true);
				}
				int shootRate2 = (wormAlive ? 200 : 100);
				base.NPC.localAI[1]++;
				if (base.NPC.ai[2] > 40f && (base.NPC.localAI[1] > (float)(shootRate2 - 18) || base.NPC.localAI[1] <= 15f))
				{
					FrameChangeSpeed = 0f;
					FrameType = FrameAnimationType.BlastPunchCast;
				}
				if (base.NPC.localAI[1] > (float)shootRate2)
				{
					_ = base.NPC.Center + new Vector2((float)base.NPC.spriteDirection * -22f, 2f);
					Vector2 projectileVelocity7 = (player.Center - base.NPC.Center).SafeNormalize(Vector2.UnitY);
					Vector2 projectileSpawn6 = base.NPC.Center + projectileVelocity7 * 8f;
					GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, projectileVelocity7 * 9f, permafrost ? Color.Cyan : Color.Red, new Vector2(0.5f, 1f), projectileVelocity7.ToRotation(), 0.9f, 0f, 60));
					GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, projectileVelocity7 * 8f, permafrost ? Color.Cyan : Color.Magenta, new Vector2(0.5f, 1f), projectileVelocity7.ToRotation(), 0.93f, 0.4f, 60));
					base.NPC.localAI[1] = 0f;
					if (Main.netMode != 1)
					{
						SoundEngine.PlaySound(in BrimstoneBigShotSound, base.NPC.Center);
						projectileVelocity7 *= 5f * uDieLul;
						int projectileType4 = gigablast;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawn6, projectileVelocity7, projectileType4, GigablastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 240f)
				{
					base.NPC.ai[1] = -1f;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
			}
		}
		goto IL_8548;
		IL_0597:
		bool shouldNotUseShield = (byte)num3 != 0;
		if (lifeRatio <= 0.01f && hasDoneDeathAnim)
		{
			shieldOpacity = MathHelper.Lerp(shieldOpacity, 0f, 0.08f);
			forcefieldScale = MathHelper.Lerp(forcefieldScale, 0f, 0.08f);
		}
		else if (((willCharge && AttackCloseToBeingOver) || base.NPC.ai[1] == 2f) && !shouldNotUseShield)
		{
			if (base.NPC.ai[1] != 2f)
			{
				float idealRotation3 = base.NPC.AngleTo(player.Center);
				if (Math.Abs(MathHelper.WrapAngle(shieldRotation - idealRotation3)) > 0.04f)
				{
					shieldRotation = shieldRotation.AngleLerp(idealRotation3, 0.125f);
					shieldRotation = shieldRotation.AngleTowards(idealRotation3, 0.18f);
				}
			}
			else if (!permafrost)
			{
				for (float num13 = 1f; num13 < 16f; num13++)
				{
					Dust dust6 = Dust.NewDustPerfect(base.NPC.Center, 182);
					dust6.position = Vector2.Lerp(base.NPC.position, base.NPC.oldPosition, num13 / 16f) + base.NPC.Size * 0.5f;
					dust6.position += shieldRotation.ToRotationVector2() * 42f;
					dust6.position += (shieldRotation - (float)Math.PI / 2f).ToRotationVector2() * (float)Math.Cos(base.NPC.velocity.ToRotation()) * -4f;
					dust6.noGravity = true;
					dust6.velocity = base.NPC.velocity;
					dust6.color = Color.Red;
					dust6.scale = MathHelper.Lerp(0.6f, 0.85f, 1f - num13 / 16f);
				}
			}
			forcefieldScale = MathHelper.Lerp(forcefieldScale, 0.45f, 0.08f);
			shieldOpacity = MathHelper.Lerp(shieldOpacity, 1f, 0.08f);
		}
		else
		{
			shieldOpacity = MathHelper.Lerp(shieldOpacity, 0f, 0.08f);
			forcefieldScale = MathHelper.Lerp(forcefieldScale, 1f, 0.08f);
		}
		if (!base.NPC.dontTakeDamage && willCharge && base.NPC.ai[1] != 2f)
		{
			_ = CalamityUtils.RandomVelocity(100f, 70f, 150f, 0.04f) * Main.rand.NextFloat(2f, 13f);
		}
		if (!spawnArena && Main.netMode != 1)
		{
			if (death)
			{
				safeBox.X = (spawnX = (spawnXReset = (int)(base.NPC.Center.X - 1000f)));
				spawnX2 = (spawnXReset2 = (int)(base.NPC.Center.X + 1000f));
				safeBox.Y = (spawnY = (spawnYReset = (int)(base.NPC.Center.Y - 1000f)));
				safeBox.Width = 2000;
				safeBox.Height = 2000;
				spawnYAdd = 100;
			}
			else
			{
				safeBox.X = (spawnX = (spawnXReset = (int)(base.NPC.Center.X - 1250f)));
				spawnX2 = (spawnXReset2 = (int)(base.NPC.Center.X + 1250f));
				safeBox.Y = (spawnY = (spawnYReset = (int)(base.NPC.Center.Y - 1250f)));
				safeBox.Width = 2500;
				safeBox.Height = 2500;
				spawnYAdd = 125;
			}
			_ = (int)((float)safeBox.X + (float)(safeBox.Width / 2)) / 16;
			_ = (int)((float)safeBox.Y + (float)(safeBox.Height / 2)) / 16;
			_ = safeBox.Width / 2 / 16;
			if (initialRitualPosition == Vector2.Zero)
			{
				initialRitualPosition = base.NPC.Center + Vector2.UnitY * 24f;
				base.NPC.netUpdate = true;
			}
			spawnArena = true;
			base.NPC.netUpdate = true;
		}
		if (ArenaBox == null)
		{
			ArenaBox = new ArenaWallSystem.Box
			{
				position = new Vector2((float)(spawnX + (death ? 1000 : 1250)), (float)(spawnY + (death ? 1000 : 1250))),
				boxDimensions = GetArenaSize() * 2f,
				borderThickness = 2000f,
				RemovalCondition = () => !Main.npc[base.NPC.whoAmI].active || Main.npc[base.NPC.whoAmI].type != base.Type,
				UpdateBox = UpdateArena,
				DrawBox = DrawArena,
				DespawnAction = delegate(ArenaWallSystem.Box box)
				{
					box.borderThickness *= 0.9f;
					if (box.borderThickness < 4f)
					{
						return true;
					}
					UpdateArena(box);
					return false;
				}
			};
			ArenaWallSystem.ActiveBoxes.Add(ArenaBox);
		}
		ArenaBox.NewDimensions = Vector4.Lerp(ArenaBox.boxDimensions, GetArenaSize(), (lifeRatio <= 0.01f) ? 0.02f : (startFourthAttack ? 0.05f : 0.1f));
		Color color = GetArenaColor(out var oldColor);
		if (colorCompletion > 1.1f && color != ArenaBox.borderColor)
		{
			colorCompletion = 0f;
		}
		if (colorCompletion < 1f)
		{
			ArenaBox.borderColor = Color.Lerp(oldColor, color, colorCompletion);
		}
		else
		{
			ArenaBox.borderColor = color;
		}
		colorCompletion += 0.003f;
		if (ArenaWallSystem.ActiveBoxes.Count > 0 && !Collision.CheckAABBvAABBCollision(ArenaBox.TopLeft, ArenaBox.Size, player.position, player.Size))
		{
			float projectileVelocityMultCap = 2f;
			uDieLul = MathHelper.Clamp(uDieLul * 1.01f, 1f, projectileVelocityMultCap);
			protectionBoost = true;
			base.NPC.Calamity().CurrentlyEnraged = true;
		}
		else
		{
			uDieLul = MathHelper.Clamp(uDieLul * 0.99f, 1f, 2f);
			protectionBoost = false;
			base.NPC.Calamity().CurrentlyEnraged = false;
		}
		if (permafrost && base.NPC.Calamity().CurrentlyEnraged && player.mount.Active)
		{
			player.ResetEffects();
			player.head = -1;
			player.body = -1;
			player.legs = -1;
			player.handon = -1;
			player.handoff = -1;
			player.back = -1;
			player.front = -1;
			player.shoe = -1;
			player.waist = -1;
			player.shield = -1;
			player.neck = -1;
			player.face = -1;
			player.balloon = -1;
			player.mount.Dismount(player);
		}
		CalamityGlobalNPC global = base.NPC.Calamity();
		if (protectionBoost && !gettingTired5)
		{
			global.DR = enragedDR;
			global.unbreakableDR = true;
		}
		else
		{
			global.DR = normalDR;
			global.unbreakableDR = false;
			if (startFifthAttack)
			{
				global.DR *= 1.2f;
			}
		}
		if (!player.active || player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead)
			{
				if (SoundEngine.TryGetActiveSound(BulletHellRumbleSlot, out ActiveSound rumbleSound) && rumbleSound.IsPlaying)
				{
					rumbleSound.Stop();
				}
				canDespawn = true;
				for (int x2 = 0; x2 < Main.maxProjectiles; x2++)
				{
					Projectile projectile2 = Main.projectile[x2];
					if (!projectile2.active)
					{
						continue;
					}
					if (projectile2.type == bulletHellblast || projectile2.type == barrage || projectile2.type == wave)
					{
						if (projectile2.timeLeft > 60)
						{
							projectile2.timeLeft = 60;
						}
					}
					else if (projectile2.type == fireblast || projectile2.type == gigablast)
					{
						projectile2.ai[2] = 1f;
						if (projectile2.timeLeft > 15)
						{
							projectile2.timeLeft = 15;
						}
					}
				}
				base.NPC.Opacity = MathHelper.Lerp(base.NPC.Opacity, 0f, 0.065f);
				base.NPC.velocity = Vector2.Lerp(Vector2.UnitY * -4f, Vector2.Zero, (float)Math.Sin((float)Math.PI * base.NPC.Opacity));
				forcefieldOpacity = Utils.GetLerpValue(0.1f, 0.6f, base.NPC.Opacity, clamped: true);
				if (base.NPC.alpha >= 230)
				{
					if (DownedBossSystem.downedCalamitas && !BossRushEvent.BossRushActive)
					{
						Dust.QuickDustLine(base.NPC.Center, initialRitualPosition, 500f, permafrost ? Color.Cyan : Color.Red);
						base.NPC.Center = initialRitualPosition;
						if (Main.netMode != 1)
						{
							NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y + 12, permafrost ? ModContent.NPCType<Archmage>() : ModContent.NPCType<BrimstoneWitch>());
						}
					}
					base.NPC.active = false;
					base.NPC.netUpdate = true;
				}
				for (int i2 = 0; (float)i2 < MathHelper.Lerp(2f, 6f, 1f - base.NPC.Opacity); i2++)
				{
					Dust dust7 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Square(-24f, 24f), 6);
					dust7.color = (permafrost ? Color.Cyan : Color.Red);
					dust7.velocity = Vector2.UnitY * (0f - Main.rand.NextFloat(2f, 3.25f));
					dust7.scale = Main.rand.NextFloat(0.95f, 1.15f);
					dust7.noGravity = true;
				}
			}
		}
		else
		{
			canDespawn = false;
		}
		if (attackCastDelay > 0)
		{
			attackCastDelay--;
			NPC nPC6 = base.NPC;
			nPC6.velocity *= 0.94f;
			base.NPC.damage = 0;
			base.NPC.dontTakeDamage = true;
			if ((startBattle && !hasSummonedSepulcher1) || (gettingTired && !hasSummonedSepulcher2))
			{
				DoHeartsSpawningCastAnimation(player, death);
			}
			if (enteredBrothersPhase && !hasSummonedBrothers)
			{
				DoBrothersSpawningCastAnimation(bodyWidth, bodyHeight);
			}
			if (attackCastDelay == 0)
			{
				base.NPC.dontTakeDamage = false;
				base.NPC.netUpdate = true;
			}
			FrameType = FrameAnimationType.Casting;
			return;
		}
		if (bulletHellCounter2 < 900)
		{
			despawnProj = true;
			bulletHellCounter2++;
			base.NPC.damage = 0;
			base.NPC.chaseable = false;
			base.NPC.dontTakeDamage = true;
			if (bulletHellCounter2 == 540)
			{
				BulletHellRumbleSlot = SoundEngine.PlaySound(in BulletHellSound, player.Center);
			}
			if (bulletHellCounter2 > 540 && SoundEngine.TryGetActiveSound(BulletHellRumbleSlot, out ActiveSound BHSound) && BHSound.IsPlaying)
			{
				BHSound.Position = player.MountedCenter;
			}
			if (!canDespawn)
			{
				NPC nPC7 = base.NPC;
				nPC7.velocity *= 0.95f;
			}
			if (Main.netMode != 1)
			{
				bulletHellCounter++;
				if (bulletHellCounter >= baseBulletHellProjectileGateValue)
				{
					bulletHellCounter = 0;
					if (bulletHellCounter2 % (baseBulletHellProjectileGateValue * 6) == 0)
					{
						float distance = (Main.rand.NextBool() ? (-1000f) : 1000f);
						float velocity8 = ((distance == -1000f) ? 4f : (-4f)) * uDieLul;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity8, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					if (bulletHellCounter2 < 300 && !Main.zenithWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * uDieLul, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else if (bulletHellCounter2 < 600)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else
					{
						if (!Main.zenithWorld)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 3f * uDieLul, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						}
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -3f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
				}
			}
			FrameType = FrameAnimationType.Casting;
			return;
		}
		if (!startBattle)
		{
			attackCastDelay = 75;
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Red, new Vector2(1f, 1f), 0f, 0.1f, 5f, 15));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Lerp(Color.Red, Color.Magenta, 0.3f), new Vector2(1f, 1f), 0f, 0.05f, 4f, 18));
			for (int i3 = 0; i3 < 100; i3++)
			{
				Vector2 dustVel3 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0);
				Dust dust8 = Dust.NewDustPerfect(base.NPC.Center + dustVel3 * 3f, Main.rand.NextBool(3) ? 60 : 114);
				dust8.noGravity = true;
				dust8.velocity = dustVel3 * Main.rand.NextFloat(0.3f, 1.3f);
				dust8.scale = Main.rand.NextFloat(2f, 3.2f);
			}
			SoundEngine.PlaySound(in BulletHellEndSound, base.NPC.Center);
			SoundEngine.PlaySound(in SoundID.DD2_DarkMageCastHeal, player.Center);
			startBattle = true;
		}
		if (bulletHellCounter2 < 1800 && startSecondAttack)
		{
			despawnProj = true;
			bulletHellCounter2++;
			base.NPC.damage = 0;
			base.NPC.chaseable = false;
			base.NPC.dontTakeDamage = true;
			if (bulletHellCounter2 == 1440)
			{
				BulletHellRumbleSlot = SoundEngine.PlaySound(in BulletHellSound, player.Center);
			}
			if (bulletHellCounter2 > 1440 && SoundEngine.TryGetActiveSound(BulletHellRumbleSlot, out ActiveSound BHSound2) && BHSound2.IsPlaying)
			{
				BHSound2.Position = player.MountedCenter;
			}
			if (bulletHellCounter2 == 1800)
			{
				for (int i4 = 0; i4 < 100; i4++)
				{
					Vector2 dustVel4 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0);
					Dust dust9 = Dust.NewDustPerfect(base.NPC.Center + dustVel4 * 3f, Main.rand.NextBool(3) ? 60 : 114);
					dust9.noGravity = true;
					dust9.velocity = dustVel4 * Main.rand.NextFloat(0.3f, 1.3f);
					dust9.scale = Main.rand.NextFloat(2f, 3.2f);
				}
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Red, new Vector2(1f, 1f), 0f, 0.1f, 5f, 15));
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Lerp(Color.Red, Color.Magenta, 0.3f), new Vector2(1f, 1f), 0f, 0.05f, 4f, 18));
				SoundEngine.PlaySound(in BulletHellEndSound, base.NPC.Center);
			}
			if (!canDespawn)
			{
				NPC nPC8 = base.NPC;
				nPC8.velocity *= 0.95f;
			}
			if (Main.netMode != 1)
			{
				if (permafrost && bulletHellCounter2 % 90 == 0)
				{
					float bottleSpeed = 12f;
					Vector2 bottleVelocity = (player.Center + player.velocity * 20f - base.NPC.Center).SafeNormalize(Vector2.UnitY) * bottleSpeed;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, bottleVelocity * uDieLul, ModContent.ProjectileType<PermafrostMeat>(), 350, 0f, Main.myPlayer, 0f, 2f);
				}
				if (bulletHellCounter2 < 1200)
				{
					if (bulletHellCounter2 % 180 == 0)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * uDieLul, fireblast, FireblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
				}
				else if (bulletHellCounter2 < 1500 && bulletHellCounter2 > 1200)
				{
					if (bulletHellCounter2 % 180 == 0)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -5f * uDieLul, 0f, fireblast, FireblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
				}
				else if (bulletHellCounter2 > 1500 && bulletHellCounter2 % 180 == 0)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * uDieLul, fireblast, FireblastDamage, 0f, Main.myPlayer, 0f, 2f);
				}
				bulletHellCounter++;
				if (bulletHellCounter >= baseBulletHellProjectileGateValue + 1)
				{
					bulletHellCounter = 0;
					if (bulletHellCounter2 % ((baseBulletHellProjectileGateValue + 1) * 6) == 0)
					{
						float distance2 = (Main.rand.NextBool() ? (-1000f) : 1000f);
						float velocity9 = ((distance2 == -1000f) ? 4f : (-4f)) * uDieLul;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + distance2, player.position.Y, velocity9, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					if (bulletHellCounter2 < 1200 && !Main.zenithWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y + 1000f, 0f, -4f * uDieLul, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else if (bulletHellCounter2 < 1500)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -3f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
				}
			}
			FrameType = FrameAnimationType.Casting;
			return;
		}
		if (!startSecondAttack && lifeRatio <= 0.75f)
		{
			if (permafrost && Main.netMode != 1)
			{
				for (int i5 = 0; i5 < 5; i5++)
				{
					if (!WorldGen.SolidTile((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f)))
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (float)Main.rand.Next(-1599, 1600) * 0.01f, (float)Main.rand.Next(-1599, 1) * 0.01f, 1013, 300, 10f);
					}
				}
			}
			if (!BossRushEvent.BossRushActive)
			{
				string key3 = "Mods.CalamityMod.Status.Boss.SCalBH2Text";
				if (permafrost)
				{
					key3 = "Mods.CalamityMod.Status.Boss.PermafrostBH2Text";
				}
				else if (DownedBossSystem.downedCalamitas)
				{
					key3 += "Rematch";
				}
				CalamityUtils.BroadcastLocalizedText(key3, permafrost ? permafrostTextColor : textColor);
			}
			startSecondAttack = true;
			return;
		}
		if (bulletHellCounter2 < 2700 && startThirdAttack)
		{
			despawnProj = true;
			bulletHellCounter2++;
			base.NPC.damage = 0;
			base.NPC.chaseable = false;
			base.NPC.dontTakeDamage = true;
			if (bulletHellCounter2 == 2340)
			{
				BulletHellRumbleSlot = SoundEngine.PlaySound(in BulletHellSound, player.Center);
			}
			if (bulletHellCounter2 > 2340 && SoundEngine.TryGetActiveSound(BulletHellRumbleSlot, out ActiveSound BHSound3) && BHSound3.IsPlaying)
			{
				BHSound3.Position = player.MountedCenter;
			}
			if (bulletHellCounter2 == 2700)
			{
				for (int i6 = 0; i6 < 100; i6++)
				{
					Vector2 dustVel5 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0);
					Dust dust10 = Dust.NewDustPerfect(base.NPC.Center + dustVel5 * 3f, Main.rand.NextBool(3) ? 60 : 114);
					dust10.noGravity = true;
					dust10.velocity = dustVel5 * Main.rand.NextFloat(0.3f, 1.3f);
					dust10.scale = Main.rand.NextFloat(2f, 3.2f);
				}
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Red, new Vector2(1f, 1f), 0f, 0.1f, 5f, 15));
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Lerp(Color.Red, Color.Magenta, 0.3f), new Vector2(1f, 1f), 0f, 0.05f, 4f, 18));
				SoundEngine.PlaySound(in BulletHellEndSound, base.NPC.Center);
			}
			if (permafrost)
			{
				Vector2 destination = player.Center;
				Vector2 desiredVelocity = (destination - base.NPC.Center - base.NPC.velocity).SafeNormalize(Vector2.UnitY) * 6f;
				if (Vector2.Distance(base.NPC.Center, destination) > 64f)
				{
					base.NPC.SimpleFlyMovement(desiredVelocity * uDieLul, 0.3f * uDieLul);
				}
				else
				{
					NPC nPC9 = base.NPC;
					nPC9.velocity *= 0.9f;
				}
			}
			else if (!canDespawn)
			{
				NPC nPC10 = base.NPC;
				nPC10.velocity *= 0.95f;
			}
			if (Main.netMode != 1)
			{
				if (bulletHellCounter2 == 1801 && permafrost)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.One, ModContent.ProjectileType<PermafrostAbsoluteZeroProjectile>(), 3725, 0f, Main.myPlayer, 0f, 0f, base.NPC.whoAmI);
				}
				if (bulletHellCounter2 % 180 == 0)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * uDieLul, fireblast, FireblastDamage, 0f, Main.myPlayer, 0f, 2f);
				}
				if (bulletHellCounter2 % 240 == 0)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * uDieLul, gigablast, GigablastDamage, 0f, Main.myPlayer, 0f, 2f);
				}
				bulletHellCounter++;
				if (bulletHellCounter >= baseBulletHellProjectileGateValue + 4)
				{
					bulletHellCounter = 0;
					if (bulletHellCounter2 % ((baseBulletHellProjectileGateValue + 4) * 6) == 0)
					{
						float distance3 = (Main.rand.NextBool() ? (-1000f) : 1000f);
						float velocity10 = ((distance3 == -1000f) ? 4f : (-4f)) * uDieLul;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + distance3, player.position.Y, velocity10, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					if (bulletHellCounter2 < 2100 && !Main.zenithWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * uDieLul, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else if (bulletHellCounter2 < 2400)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
				}
			}
			FrameType = FrameAnimationType.Casting;
			return;
		}
		if (!startThirdAttack && lifeRatio <= 0.5f)
		{
			if (permafrost && Main.netMode != 1)
			{
				for (int i7 = 0; i7 < 10; i7++)
				{
					if (!WorldGen.SolidTile((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f)))
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (float)Main.rand.Next(-1599, 1600) * 0.01f, (float)Main.rand.Next(-1599, 1) * 0.01f, 1013, 300, 10f);
					}
				}
			}
			if (!BossRushEvent.BossRushActive)
			{
				string key4 = "Mods.CalamityMod.Status.Boss.SCalBH3Text";
				if (permafrost)
				{
					key4 = "Mods.CalamityMod.Status.Boss.PermafrostBH3Text";
				}
				else if (DownedBossSystem.downedCalamitas)
				{
					key4 += "Rematch";
				}
				CalamityUtils.BroadcastLocalizedText(key4, permafrost ? permafrostTextColor : textColor);
			}
			startThirdAttack = true;
			return;
		}
		if (lifeRatio <= 0.3f && musicSyncCounter > -120)
		{
			musicSyncCounter--;
		}
		if (musicSyncCounter <= 126 && musicSyncCounter > 0)
		{
			forcefieldOpacity = MathHelper.Lerp(forcefieldOpacity, 0.4f, 0.06f);
			if (shieldOpacity > 0f)
			{
				shieldOpacity = MathHelper.Lerp(shieldOpacity, 0f, 0.065f);
			}
		}
		if (musicSyncCounter <= 42 && musicSyncCounter > 0)
		{
			forcefieldPureVisualScale = MathHelper.Lerp(forcefieldPureVisualScale, 0.45f, 0.095f);
		}
		if (musicSyncCounter == 0 && !postMusicHit)
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Red, new Vector2(1f, 1f), 0f, 0.1f, 4f, 17));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Lerp(Color.Red, Color.Magenta, 0.3f), new Vector2(1f, 1f), 0f, 0.05f, 3f, 19));
			for (int i8 = 0; i8 < 30; i8++)
			{
				Vector2 orbvel = Utils.RotatedByRandom(new Vector2(25f, 25f), 100.0) * Main.rand.NextFloat(0.1f, 1.2f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.NPC.Center + orbvel, orbvel, affectedByGravity: false, 90, Main.rand.NextFloat(0.95f, 1.85f), Color.Lerp(Color.Red, Color.Magenta, 0.3f)));
			}
			forcefieldOpacity = 0.7f;
			forcefieldPureVisualScale = 1.5f;
			postMusicHit = true;
		}
		if (postMusicHit && lifeRatio > 0.01f && !canDespawn && base.NPC.ai[1] != 2f)
		{
			forcefieldPureVisualScale = MathHelper.Lerp(forcefieldPureVisualScale, 1f, 0.095f);
			Vector2 velOffset6 = Utils.RotatedByRandom(new Vector2(56f, 56f), 100.0) * forcefieldPureVisualScale;
			for (int i9 = 0; i9 < 2; i9++)
			{
				Dust dust11 = Dust.NewDustPerfect(base.NPC.Center + velOffset6, Main.rand.NextBool(3) ? 60 : 114);
				dust11.noGravity = true;
				dust11.velocity = velOffset6 * Main.rand.NextFloat(0.05f, 0.2f);
				dust11.scale = Main.rand.NextFloat(0.3f, 0.8f);
				if (Main.rand.NextBool())
				{
					Dust dust12 = Dust.NewDustPerfect(base.NPC.Center + velOffset6, 269, velOffset6 * Main.rand.NextFloat(0.01f, 0.1f));
					dust12.noGravity = true;
					dust12.scale = Main.rand.NextFloat(0.2f, 0.6f);
					dust12.alpha = 200;
					dust12.color = (Main.rand.NextBool() ? Color.Goldenrod : Color.Red);
				}
			}
		}
		if (bulletHellCounter2 < 3600 && startFourthAttack)
		{
			despawnProj = true;
			bulletHellCounter2++;
			base.NPC.damage = 0;
			base.NPC.chaseable = false;
			base.NPC.dontTakeDamage = true;
			if (bulletHellCounter2 == 3240)
			{
				BulletHellRumbleSlot = SoundEngine.PlaySound(in BulletHellSound, player.Center);
			}
			if (bulletHellCounter2 > 3240 && SoundEngine.TryGetActiveSound(BulletHellRumbleSlot, out ActiveSound BHSound4) && BHSound4.IsPlaying)
			{
				BHSound4.Position = player.MountedCenter;
			}
			if (bulletHellCounter2 == 3600)
			{
				for (int i10 = 0; i10 < 100; i10++)
				{
					Vector2 dustVel6 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0);
					Dust dust13 = Dust.NewDustPerfect(base.NPC.Center + dustVel6 * 3f, Main.rand.NextBool(3) ? 60 : 114);
					dust13.noGravity = true;
					dust13.velocity = dustVel6 * Main.rand.NextFloat(0.3f, 1.3f);
					dust13.scale = Main.rand.NextFloat(2f, 3.2f);
				}
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Red, new Vector2(1f, 1f), 0f, 0.1f, 5f, 15));
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Lerp(Color.Red, Color.Magenta, 0.3f), new Vector2(1f, 1f), 0f, 0.05f, 4f, 18));
				SoundEngine.PlaySound(in BulletHellEndSound, base.NPC.Center);
			}
			if (!canDespawn)
			{
				NPC nPC11 = base.NPC;
				nPC11.velocity *= 0.95f;
			}
			if (Main.netMode != 1)
			{
				if (permafrost && bulletHellCounter2 % 90 == 0)
				{
					float bottleSpeed2 = 12f;
					Vector2 bottleVelocity2 = (player.Center + player.velocity * 20f - base.NPC.Center).SafeNormalize(Vector2.UnitY) * bottleSpeed2;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, bottleVelocity2 * uDieLul, ModContent.ProjectileType<PermafrostMeat>(), 125, 0f, Main.myPlayer, 0f, 2f);
				}
				if (bulletHellCounter2 % 180 == 0)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * uDieLul, fireblast, FireblastDamage, 0f, Main.myPlayer, 0f, 2f);
				}
				if (bulletHellCounter2 % 240 == 0)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * uDieLul, gigablast, GigablastDamage, 0f, Main.myPlayer, 0f, 2f);
				}
				if (!revenge)
				{
				}
				Vector2 spawnSpot = safeBox.Center();
				passedVar++;
				if (passedVar == 180f)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/SCalAltarSummon");
					style.Pitch = 0.3f;
					SoundEngine.PlaySound(in style, player.Center);
					for (int i11 = 0; i11 < 2; i11++)
					{
						GeneralParticleHandler.SpawnParticle(new BloomParticle(spawnSpot, Vector2.Zero, Color.Lerp(Color.Red, Color.Magenta, 0.3f), 0f, 1.45f, 240, fade: false));
					}
					GeneralParticleHandler.SpawnParticle(new BloomParticle(spawnSpot, Vector2.Zero, Color.White, 0f, 1.35f, 240, fade: false));
				}
				if (passedVar == 420f)
				{
					for (int i12 = 0; i12 < 90; i12++)
					{
						Dust dust14 = Dust.NewDustPerfect(safeBox.Center(), permafrost ? 161 : 235, Utils.RotatedByRandom(new Vector2(30f, 30f), 100.0) * Main.rand.NextFloat(0.05f, 1.2f));
						dust14.noGravity = true;
						dust14.scale = Main.rand.NextFloat(1.2f, 2.3f);
					}
					for (int i13 = 0; i13 < 40; i13++)
					{
						Vector2 sparkVel2 = Utils.RotatedByRandom(new Vector2(20f, 20f), 100.0) * Main.rand.NextFloat(0.1f, 1.1f);
						GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(safeBox.Center() + sparkVel2 * 2f, sparkVel2, affectedByGravity: false, 120, Main.rand.NextFloat(1.55f, 2.75f), permafrost ? Color.DarkCyan : Color.Lerp(Color.Red, Color.Magenta, 0.3f), AddativeBlend: true, needed: true));
					}
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnSpot, Vector2.Zero, ModContent.ProjectileType<BrimstoneMonster>(), 100, 0f, Main.myPlayer);
				}
				bulletHellCounter++;
				if (bulletHellCounter >= baseBulletHellProjectileGateValue + 6)
				{
					bulletHellCounter = 0;
					if (bulletHellCounter2 % ((baseBulletHellProjectileGateValue + 6) * 6) == 0)
					{
						float distance4 = (Main.rand.NextBool() ? (-1000f) : 1000f);
						float velocity11 = ((distance4 == -1000f) ? 4f : (-4f)) * uDieLul;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + distance4, player.position.Y, velocity11, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					if (bulletHellCounter2 < 3000 && !Main.zenithWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y + 1000f, 0f, -4f * uDieLul, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else if (bulletHellCounter2 < 3300)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
				}
			}
			FrameType = FrameAnimationType.Casting;
			return;
		}
		if (!startFourthAttack && lifeRatio <= 0.3f)
		{
			if (permafrost && Main.netMode != 1)
			{
				for (int i14 = 0; i14 < 15; i14++)
				{
					if (!WorldGen.SolidTile((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f)))
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (float)Main.rand.Next(-1599, 1600) * 0.01f, (float)Main.rand.Next(-1599, 1) * 0.01f, 1013, 300, 10f);
					}
				}
			}
			if (!BossRushEvent.BossRushActive)
			{
				string key5 = "Mods.CalamityMod.Status.Boss.SCalBH4Text";
				if (permafrost)
				{
					key5 = "Mods.CalamityMod.Status.Boss.PermafrostBH4Text";
				}
				else if (DownedBossSystem.downedCalamitas)
				{
					key5 += "Rematch";
				}
				CalamityUtils.BroadcastLocalizedText(key5, permafrost ? permafrostTextColor : textColor);
			}
			startFourthAttack = true;
			return;
		}
		if (bulletHellCounter2 < 4500 && startFifthAttack)
		{
			despawnProj = true;
			bulletHellCounter2++;
			base.NPC.damage = 0;
			base.NPC.chaseable = false;
			base.NPC.dontTakeDamage = true;
			if (bulletHellCounter2 == 4140)
			{
				BulletHellRumbleSlot = SoundEngine.PlaySound(in BulletHellSound, player.Center);
			}
			if (bulletHellCounter2 > 4140 && SoundEngine.TryGetActiveSound(BulletHellRumbleSlot, out ActiveSound BHSound5) && BHSound5.IsPlaying)
			{
				BHSound5.Position = player.MountedCenter;
			}
			if (bulletHellCounter2 == 4500)
			{
				for (int i15 = 0; i15 < 100; i15++)
				{
					Vector2 dustVel7 = Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0);
					Dust dust15 = Dust.NewDustPerfect(base.NPC.Center + dustVel7 * 3f, Main.rand.NextBool(3) ? 60 : 114);
					dust15.noGravity = true;
					dust15.velocity = dustVel7 * Main.rand.NextFloat(0.3f, 1.3f);
					dust15.scale = Main.rand.NextFloat(2f, 3.2f);
				}
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Red, new Vector2(1f, 1f), 0f, 0.1f, 5f, 15));
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.NPC.Center, Vector2.Zero, permafrost ? Color.Cyan : Color.Lerp(Color.Red, Color.Magenta, 0.3f), new Vector2(1f, 1f), 0f, 0.05f, 4f, 18));
				SoundEngine.PlaySound(in BulletHellEndSound, base.NPC.Center);
			}
			if (permafrost)
			{
				Vector2 destination2 = player.Center;
				Vector2 desiredVelocity2 = (destination2 - base.NPC.Center - base.NPC.velocity).SafeNormalize(Vector2.UnitY) * 6f;
				if (Vector2.Distance(base.NPC.Center, destination2) > 64f)
				{
					base.NPC.SimpleFlyMovement(desiredVelocity2 * uDieLul, 0.3f * uDieLul);
				}
				else
				{
					NPC nPC12 = base.NPC;
					nPC12.velocity *= 0.9f;
				}
			}
			else if (!canDespawn)
			{
				NPC nPC13 = base.NPC;
				nPC13.velocity *= 0.95f;
			}
			if (Main.netMode != 1)
			{
				if (permafrost)
				{
					if (bulletHellCounter2 == 3601)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.One, ModContent.ProjectileType<PermafrostAbsoluteZeroProjectile>(), 3725, 0f, Main.myPlayer, 0f, 0f, base.NPC.whoAmI);
					}
					if (bulletHellCounter2 % 90 == 0)
					{
						float bottleSpeed3 = 12f;
						Vector2 bottleVelocity3 = (player.Center + player.velocity * 20f - base.NPC.Center).SafeNormalize(Vector2.UnitY) * bottleSpeed3;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, bottleVelocity3 * uDieLul, ModContent.ProjectileType<PermafrostMeat>(), 125, 0f, Main.myPlayer);
					}
				}
				if (bulletHellCounter2 % 240 == 0)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 5f * uDieLul, fireblast, FireblastDamage, 0f, Main.myPlayer, 0f, 2f);
				}
				if (bulletHellCounter2 % 360 == 0)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 10f * uDieLul, gigablast, GigablastDamage, 0f, Main.myPlayer, 0f, 2f);
				}
				if (bulletHellCounter2 % 30 == 0)
				{
					int random = Main.rand.Next(-500, 501);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)random, -5f * uDieLul, 0f, wave, SkullDamage, 0f, Main.myPlayer, 0f, 2f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y - (float)random, 5f * uDieLul, 0f, wave, SkullDamage, 0f, Main.myPlayer, 0f, 2f);
				}
				bulletHellCounter++;
				if (bulletHellCounter >= baseBulletHellProjectileGateValue + 8)
				{
					bulletHellCounter = 0;
					if (bulletHellCounter2 % ((baseBulletHellProjectileGateValue + 8) * 6) == 0)
					{
						float distance5 = (Main.rand.NextBool() ? (-1000f) : 1000f);
						float velocity12 = ((distance5 == -1000f) ? 4f : (-4f)) * uDieLul;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + distance5, player.position.Y, velocity12, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					if (bulletHellCounter2 < 3900 && !Main.zenithWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 4f * uDieLul, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else if (bulletHellCounter2 < 4200)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3.5f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
					else
					{
						if (!Main.zenithWorld)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, 3f * uDieLul, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						}
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), -3f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 3f * uDieLul, 0f, bulletHellblast, HellblastDamage, 0f, Main.myPlayer, 0f, 2f);
					}
				}
			}
			FrameType = FrameAnimationType.Casting;
			return;
		}
		if (!startFifthAttack && lifeRatio <= 0.1f)
		{
			if (permafrost && Main.netMode != 1)
			{
				for (int i16 = 0; i16 < 20; i16++)
				{
					if (!WorldGen.SolidTile((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f)))
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, (float)Main.rand.Next(-1599, 1600) * 0.01f, (float)Main.rand.Next(-1599, 1) * 0.01f, 1013, 300, 10f);
					}
				}
			}
			string key6 = "Mods.CalamityMod.Status.Boss.SCalBH5Text";
			if (permafrost)
			{
				key6 = "Mods.CalamityMod.Status.Boss.PermafrostBH5Text";
			}
			if (!BossRushEvent.BossRushActive)
			{
				if (DownedBossSystem.downedCalamitas && !permafrost)
				{
					key6 += "Rematch";
				}
				CalamityUtils.BroadcastLocalizedText(key6, permafrost ? permafrostTextColor : textColor);
			}
			startFifthAttack = true;
			return;
		}
		if (startFifthAttack)
		{
			if (gettingTired5)
			{
				if (permafrost)
				{
					if (giveUpCounter <= 1)
					{
						base.NPC.noTileCollide = false;
						base.NPC.noGravity = false;
						base.NPC.damage = 0;
						if (giveUpCounter == 1)
						{
							base.NPC.velocity = Vector2.Zero;
							CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.PermafrostGiveUpText", permafrostTextColor);
							Dust.QuickDustLine(base.NPC.Center, initialRitualPosition, 500f, Color.Cyan);
							base.NPC.Center = initialRitualPosition;
							giveUpCounter--;
							return;
						}
						for (int i17 = 0; i17 < 24; i17++)
						{
							Dust dust16 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Square(-24f, 24f), 6);
							dust16.color = Color.Pink;
							dust16.velocity = Vector2.UnitY * (0f - Main.rand.NextFloat(2f, 3.25f));
							dust16.scale = Main.rand.NextFloat(0.95f, 1.15f);
							dust16.fadeIn = 1.25f;
							dust16.noGravity = true;
						}
						base.NPC.active = false;
						base.NPC.netUpdate = true;
						base.NPC.NPCLoot();
						int cryo = NPC.FindFirstNPC(ModContent.NPCType<global::CalamityMod.NPCs.Cryogen.Cryogen>());
						if (cryo > -1)
						{
							Main.npc[cryo].active = false;
							Main.npc[cryo].netUpdate = true;
						}
						return;
					}
					int blasterTimer = 1200 - giveUpCounter;
					Vector2 circleOffset = player.Center + (Vector2.UnitY * 640f).RotatedBy(MathHelper.ToRadians((float)blasterTimer * 3f));
					base.NPC.Center = circleOffset;
					int blasterDivisor = 5;
					if (blasterTimer % blasterDivisor == 0 && Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), circleOffset, player.Center, ModContent.ProjectileType<PermafrostBlaster>(), 350, 0f, Main.myPlayer, 0f, 1f);
					}
					int beamDivisor = 60;
					if (blasterTimer % beamDivisor == 0)
					{
						int totalProjectiles = 5;
						float radians = (float)Math.PI * 2f / (float)totalProjectiles;
						float velocity13 = 12f * uDieLul;
						Vector2 spinningPoint = default(Vector2);
						((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity13);
						if (Main.netMode != 1)
						{
							for (int k2 = 0; k2 < totalProjectiles; k2++)
							{
								Vector2 rayVelocity = spinningPoint.RotatedBy(radians * (float)k2);
								int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + rayVelocity.SafeNormalize(Vector2.UnitY) * 16f, rayVelocity, ModContent.ProjectileType<DarkIceZero>(), 250, 0f, Main.myPlayer);
								if (proj.WithinBounds(Main.maxProjectiles))
								{
									Main.projectile[proj].DamageType = DamageClass.Default;
									Main.projectile[proj].friendly = false;
									Main.projectile[proj].hostile = true;
									Main.projectile[proj].tileCollide = false;
								}
							}
						}
					}
				}
				else
				{
					for (int l2 = 0; l2 < NPC.maxBuffs; l2++)
					{
						int buffID = base.NPC.buffType[l2];
						if (CalamityBuffSets.IsDebuff[buffID] && base.NPC.buffTime[l2] > 4)
						{
							base.NPC.buffTime[l2] = 4;
						}
					}
					if (!hasDoneDeathAnim && !BossRushEvent.BossRushActive)
					{
						attackPause = 5;
						Dust.QuickDustLine(base.NPC.Center, safeBox.Center() + new Vector2(0f, -30f), 500f, permafrost ? Color.Cyan : Color.Red);
						base.NPC.Center = safeBox.Center() + new Vector2(0f, -30f);
						base.NPC.velocity = new Vector2((float)(10 * base.NPC.spriteDirection), -7f);
						hasDoneDeathAnim = true;
						base.NPC.noTileCollide = false;
						base.NPC.noGravity = false;
						base.NPC.damage = 0;
					}
					else
					{
						if (BossRushEvent.BossRushActive)
						{
							base.NPC.Center = initialRitualPosition + new Vector2(0f, -30f);
						}
						if (base.NPC.velocity.Y < 8f)
						{
							base.NPC.velocity.Y += 0.165f;
						}
						if (!canDespawn)
						{
							base.NPC.velocity.X *= 0.965f;
						}
						if (DownedBossSystem.downedCalamitas || BossRushEvent.BossRushActive)
						{
							if (giveUpCounter == 720)
							{
								if (BossRushEvent.BossRushActive)
								{
									base.NPC.chaseable = true;
									base.NPC.dontTakeDamage = false;
									return;
								}
								for (int i18 = 0; i18 < 24; i18++)
								{
									Dust dust17 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Square(-24f, 24f), 6);
									dust17.color = (permafrost ? Color.Cyan : Color.Red);
									dust17.velocity = Vector2.UnitY * (0f - Main.rand.NextFloat(2f, 3.25f));
									dust17.scale = Main.rand.NextFloat(0.95f, 1.15f);
									dust17.fadeIn = 1.25f;
									dust17.noGravity = true;
								}
								base.NPC.active = false;
								base.NPC.netUpdate = true;
								base.NPC.NPCLoot();
							}
						}
						else if (giveUpCounter == 900 && !BossRushEvent.BossRushActive)
						{
							CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.SCalAcceptanceText1", textColor);
						}
						else if (giveUpCounter == 600 && !BossRushEvent.BossRushActive)
						{
							CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.SCalAcceptanceText2", textColor);
						}
						else if (giveUpCounter == 300 && !BossRushEvent.BossRushActive)
						{
							CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.SCalAcceptanceText3", textColor);
						}
						if (giveUpCounter <= 0)
						{
							for (int i19 = 0; i19 < 24; i19++)
							{
								Dust dust18 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Square(-24f, 24f), 6);
								dust18.color = (permafrost ? Color.Cyan : Color.Red);
								dust18.velocity = Vector2.UnitY * (0f - Main.rand.NextFloat(2f, 3.25f));
								dust18.scale = Main.rand.NextFloat(0.95f, 1.15f);
								dust18.fadeIn = 1.25f;
								dust18.noGravity = true;
							}
							base.NPC.active = false;
							base.NPC.netUpdate = true;
							base.NPC.NPCLoot();
							return;
						}
					}
				}
				giveUpCounter--;
				base.NPC.chaseable = false;
				base.NPC.dontTakeDamage = true;
				return;
			}
			if (!gettingTired5 && lifeRatio <= 0.01f)
			{
				for (int x3 = 0; x3 < Main.maxProjectiles; x3++)
				{
					Projectile projectile3 = Main.projectile[x3];
					if (!projectile3.active)
					{
						continue;
					}
					if (projectile3.type == ModContent.ProjectileType<BrimstoneMonster>() && projectile3.timeLeft > 90)
					{
						projectile3.timeLeft = 90;
					}
					if (projectile3.type == bulletHellblast || projectile3.type == barrage || projectile3.type == wave)
					{
						if (projectile3.timeLeft > 60)
						{
							projectile3.timeLeft = 60;
						}
					}
					else if (projectile3.type == fireblast || projectile3.type == gigablast)
					{
						projectile3.ai[2] = 1f;
						if (projectile3.timeLeft > 15)
						{
							projectile3.timeLeft = 15;
						}
					}
				}
				if (!BossRushEvent.BossRushActive)
				{
					string key7 = "Mods.CalamityMod.Status.Boss.SCalDesparationText4";
					if (permafrost)
					{
						key7 = "Mods.CalamityMod.Status.Boss.PermafrostBruhText";
					}
					else if (DownedBossSystem.downedCalamitas)
					{
						key7 += "Rematch";
					}
					CalamityUtils.BroadcastLocalizedText(key7, permafrost ? permafrostTextColor : textColor);
				}
				gettingTired5 = true;
				return;
			}
			if (!gettingTired4 && lifeRatio <= 0.02f)
			{
				if (!BossRushEvent.BossRushActive && !permafrost)
				{
					string key8 = "Mods.CalamityMod.Status.Boss.SCalDesparationText3";
					if (DownedBossSystem.downedCalamitas)
					{
						key8 += "Rematch";
					}
					CalamityUtils.BroadcastLocalizedText(key8, textColor);
				}
				gettingTired4 = true;
				return;
			}
			if (!gettingTired3 && lifeRatio <= 0.04f)
			{
				if (!BossRushEvent.BossRushActive && !permafrost)
				{
					string key9 = "Mods.CalamityMod.Status.Boss.SCalDesparationText2";
					if (DownedBossSystem.downedCalamitas)
					{
						key9 += "Rematch";
					}
					CalamityUtils.BroadcastLocalizedText(key9, textColor);
				}
				gettingTired3 = true;
				return;
			}
			if (!gettingTired2 && lifeRatio <= 0.06f)
			{
				if (!BossRushEvent.BossRushActive)
				{
					string key10 = "Mods.CalamityMod.Status.Boss.SCalDesparationText1";
					if (permafrost)
					{
						key10 = "Mods.CalamityMod.Status.Boss.PermafrostNonchalantText";
					}
					else if (DownedBossSystem.downedCalamitas)
					{
						key10 += "Rematch";
					}
					CalamityUtils.BroadcastLocalizedText(key10, permafrost ? permafrostTextColor : textColor);
				}
				gettingTired2 = true;
				return;
			}
			if (!gettingTired && lifeRatio <= 0.08f)
			{
				attackCastDelay = 75;
				for (int i20 = 0; i20 < 40; i20++)
				{
					Dust dust19 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Square(-70f, 70f), permafrost ? 173 : 235);
					dust19.velocity = Vector2.UnitY.RotatedByRandom(0.07999999821186066) * (0f - Main.rand.NextFloat(3f, 4.45f));
					dust19.scale = Main.rand.NextFloat(1.35f, 1.6f);
					dust19.fadeIn = 1.25f;
					dust19.noGravity = true;
				}
				for (int i21 = 0; i21 < 40; i21++)
				{
					Dust dust20 = Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2Square(-70f, 70f), permafrost ? 173 : 235);
					dust20.velocity = Vector2.UnitY.RotatedByRandom(0.07999999821186066) * (0f - Main.rand.NextFloat(3f, 4.45f));
					dust20.scale = Main.rand.NextFloat(1.35f, 1.6f);
					dust20.fadeIn = 1.25f;
					dust20.noGravity = true;
				}
				SoundEngine.PlaySound(in SoundID.DD2_DarkMageCastHeal, player.Center);
				gettingTired = true;
				return;
			}
		}
		if (bulletHellCounter2 % 900 == 0 && despawnProj)
		{
			for (int x4 = 0; x4 < Main.maxProjectiles; x4++)
			{
				Projectile projectile4 = Main.projectile[x4];
				if (!projectile4.active)
				{
					continue;
				}
				if (projectile4.type == bulletHellblast || projectile4.type == barrage || projectile4.type == wave)
				{
					if (projectile4.timeLeft > 60)
					{
						projectile4.timeLeft = 60;
					}
				}
				else if (projectile4.type == fireblast || projectile4.type == gigablast)
				{
					projectile4.ai[2] = 1f;
					if (projectile4.timeLeft > 15)
					{
						projectile4.timeLeft = 15;
					}
				}
			}
			despawnProj = false;
		}
		if (!halfLife && lifeRatio <= 0.45f && hasSummonedBrothers)
		{
			bool num14;
			if (!permafrost)
			{
				if (NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>()))
				{
					goto IL_5345;
				}
				num14 = NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>());
			}
			else
			{
				num14 = NPC.AnyNPCs(ModContent.NPCType<DevourerofGodsHead>());
			}
			if (!num14)
			{
				if (!BossRushEvent.BossRushActive)
				{
					string key11 = "Mods.CalamityMod.Status.Boss.SCalPhase2Text";
					if (permafrost)
					{
						key11 = "Mods.CalamityMod.Status.Boss.PermafrostPhase2Text";
					}
					else if (DownedBossSystem.downedCalamitas)
					{
						key11 += "Rematch";
					}
					CalamityUtils.BroadcastLocalizedText(key11, permafrost ? permafrostTextColor : textColor);
				}
				halfLife = true;
			}
		}
		goto IL_5345;
		IL_0596:
		num3 = 1;
		goto IL_0597;
		static void DrawArena(ArenaWallSystem.Box box)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			_ = Color.Black * 0.15f;
			box.DrawBoxWithOffset(box.borderThickness * 0.5f, box.borderThickness, Color.Black * 0.15f);
			box.DrawBoxWithOffset(4f, 2f, box.borderColor * 0.15f);
			box.DrawBoxWithOffset(box.borderThickness - 2f, 2f, box.borderColor);
		}
		Color GetArenaColor(out Color reference)
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			if (permafrost)
			{
				reference = Color.LightBlue;
				return Color.LightBlue;
			}
			Color c = GriefColor;
			reference = GriefColor;
			if (startFifthAttack && gettingTired5 && (giveUpCounter < 1160 || hasDoneDeathAnim))
			{
				c = AcceptanceColor;
				reference = EpiphanyColor;
			}
			else if (lifeRatio <= 0.3f)
			{
				c = EpiphanyColor;
				reference = LamentColor;
			}
			else if (lifeRatio <= 0.5f)
			{
				c = LamentColor;
			}
			return c;
		}
		Vector4 GetArenaSize(bool brothersActive = false)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0159: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_0153: Unknown result type (might be due to invalid IL or missing references)
			//IL_0158: Unknown result type (might be due to invalid IL or missing references)
			Vector4 baseSize = (death ? new Vector4(1000f) : new Vector4(1250f));
			if (wormAlive)
			{
				baseSize *= new Vector4(1f, 1.15f, startFourthAttack ? 0.75f : 0.25f, 1.15f);
			}
			if (NPC.AnyNPCs(ModContent.NPCType<SoulSeekerSupreme>()))
			{
				baseSize *= new Vector4(1.5f, 0.85f, 1.5f, 0.85f);
			}
			if (cataclysmAlive | catastropheAlive)
			{
				baseSize *= new Vector4(0.85f, 1.33f, 0.85f, 1.33f);
			}
			if (lifeRatio <= 0.01f)
			{
				baseSize = ((Main.zenithWorld && !permafrost) ? new Vector4(22f, 22f, 22f, 22f) : new Vector4(400f, 500f, 73f, 500f));
			}
			else if (Main.zenithWorld && lifeRatio <= 0.08f && !wormAlive && !permafrost)
			{
				baseSize *= MathHelper.Lerp(0.22f, 1f, lifeRatio * 12.5f);
			}
			return baseSize;
		}
		static void UpdateArena(ArenaWallSystem.Box box)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_004c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0144: Unknown result type (might be due to invalid IL or missing references)
			//IL_0155: Unknown result type (might be due to invalid IL or missing references)
			//IL_015a: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0175: Unknown result type (might be due to invalid IL or missing references)
			//IL_017a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0184: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0204: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_021f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0224: Unknown result type (might be due to invalid IL or missing references)
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			//IL_024a: Unknown result type (might be due to invalid IL or missing references)
			//IL_024f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0254: Unknown result type (might be due to invalid IL or missing references)
			//IL_028c: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_0360: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_02df: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0312: Unknown result type (might be due to invalid IL or missing references)
			//IL_0318: Unknown result type (might be due to invalid IL or missing references)
			//IL_0327: Unknown result type (might be due to invalid IL or missing references)
			//IL_032c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0336: Unknown result type (might be due to invalid IL or missing references)
			//IL_033b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0340: Unknown result type (might be due to invalid IL or missing references)
			//IL_0413: Unknown result type (might be due to invalid IL or missing references)
			//IL_037d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0383: Unknown result type (might be due to invalid IL or missing references)
			//IL_0392: Unknown result type (might be due to invalid IL or missing references)
			//IL_0397: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
			//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_03da: Unknown result type (might be due to invalid IL or missing references)
			//IL_03df: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
			if (!Main.dedServ)
			{
				float x5 = 1f / (float)TextureAssets.MagicPixel.Height();
				ScalArenaMetaball.Particle particle = ScalArenaMetaball.SpawnParticle((box.TopLeft + box.BottomLeft) * 0.5f - new Vector2(box.borderThickness * 0.5f + 2f, 0f), Vector2.Zero, 1f);
				particle.SizeScaling = 0f;
				particle.TextureToUse = TextureAssets.MagicPixel.Value;
				particle.Scale = new Vector2(box.borderThickness - 6f, box.borderThickness * 2f + box.Size.Y - 4f);
				particle.Scale.Y *= x5;
				ScalArenaMetaball.Particle particle2 = ScalArenaMetaball.SpawnParticle((box.TopRight + box.BottomRight) * 0.5f + new Vector2(box.borderThickness * 0.5f + 2f, 0f), Vector2.Zero, 1f);
				particle2.SizeScaling = 0f;
				particle2.TextureToUse = TextureAssets.MagicPixel.Value;
				particle2.Scale = new Vector2(box.borderThickness - 6f, box.borderThickness * 2f + box.Size.Y - 4f);
				particle2.Scale.Y *= x5;
				ScalArenaMetaball.Particle particle3 = ScalArenaMetaball.SpawnParticle((box.TopRight + box.TopLeft) * 0.5f - new Vector2(0f, box.borderThickness * 0.5f + 2f), Vector2.Zero, 1f);
				particle3.SizeScaling = 0f;
				particle3.TextureToUse = TextureAssets.MagicPixel.Value;
				particle3.Scale = new Vector2(box.borderThickness * 2f + box.Size.X - 4f, box.borderThickness - 6f);
				particle3.Scale.Y *= x5;
				ScalArenaMetaball.Particle particle4 = ScalArenaMetaball.SpawnParticle((box.BottomLeft + box.BottomRight) * 0.5f + new Vector2(0f, box.borderThickness * 0.5f + 2f), Vector2.Zero, 1f);
				particle4.SizeScaling = 0f;
				particle4.TextureToUse = TextureAssets.MagicPixel.Value;
				particle4.Scale = new Vector2(box.borderThickness * 2f + box.Size.X - 4f, box.borderThickness - 6f);
				particle4.Scale.Y *= x5;
				for (int num15 = 0; (float)num15 < box.Size.Y / 100f; num15++)
				{
					ScalArenaMetaball.SpawnParticle(Vector2.Lerp(box.BottomRight, box.TopRight, Main.rand.NextFloat()) + Vector2.UnitX * 8f, Vector2.Zero, 16f).SizeScaling = 0.95f;
					ScalArenaMetaball.SpawnParticle(Vector2.Lerp(box.TopLeft, box.BottomLeft, Main.rand.NextFloat()) + Vector2.UnitX * -8f, Vector2.Zero, 16f).SizeScaling = 0.95f;
				}
				for (int num16 = 0; (float)num16 < box.Size.X / 100f; num16++)
				{
					ScalArenaMetaball.SpawnParticle(Vector2.Lerp(box.TopLeft, box.TopRight, Main.rand.NextFloat()) + Vector2.UnitY * -8f, Vector2.Zero, 16f).SizeScaling = 0.95f;
					ScalArenaMetaball.SpawnParticle(Vector2.Lerp(box.BottomRight, box.BottomLeft, Main.rand.NextFloat()) + Vector2.UnitY * 8f, Vector2.Zero, 16f).SizeScaling = 0.95f;
				}
			}
		}
	}

	public void DoHeartsSpawningCastAnimation(Player target, bool death)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		int tempSpawnY = spawnY;
		tempSpawnY += 250;
		if (death)
		{
			tempSpawnY -= 50;
		}
		List<Vector2> heartSpawnPositions = new List<Vector2>();
		for (int i = 0; i < 5; i++)
		{
			heartSpawnPositions.Add(new Vector2((float)(spawnX + spawnXAdd * i + 50), (float)(tempSpawnY + spawnYAdd * i)));
			heartSpawnPositions.Add(new Vector2((float)(spawnX2 - spawnXAdd * i - 50), (float)(tempSpawnY + spawnYAdd * i)));
		}
		float castCompletion = Utils.GetLerpValue(50f, 0f, attackCastDelay, clamped: true);
		Vector2 armPosition = base.NPC.Center + Vector2.UnitX * (float)base.NPC.spriteDirection * -8f;
		foreach (Vector2 heartSpawnPosition in heartSpawnPositions)
		{
			Dust dust = Dust.NewDustPerfect(Vector2.CatmullRom(armPosition + Vector2.UnitY * 1000f, armPosition, heartSpawnPosition, heartSpawnPosition + Vector2.UnitY * 1000f, castCompletion), 267);
			dust.scale = 1.67f;
			dust.velocity = Main.rand.NextVector2CircularEdge(0.2f, 0.2f);
			dust.fadeIn = 0.67f;
			dust.color = (permafrost ? Color.Cyan : Color.Red);
			dust.noGravity = true;
		}
		if (attackCastDelay != 0)
		{
			return;
		}
		string key = (permafrost ? "Mods.CalamityMod.Status.Boss.PermafrostBirbSwarmText" : "Mods.CalamityMod.Status.Boss.SCalStartText");
		if ((double)base.NPC.life <= (double)base.NPC.lifeMax * 0.08)
		{
			key = (permafrost ? "Mods.CalamityMod.Status.Boss.PermafrostSecondBirbSwarmText" : "Mods.CalamityMod.Status.Boss.SCalSepulcher2Text");
		}
		if (!BossRushEvent.BossRushActive)
		{
			if (DownedBossSystem.downedCalamitas && !permafrost)
			{
				key += "Rematch";
			}
			CalamityUtils.BroadcastLocalizedText(key, permafrost ? permafrostTextColor : textColor);
		}
		foreach (Vector2 heartSpawnPosition2 in heartSpawnPositions)
		{
			for (int j = 0; j < 20; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(heartSpawnPosition2 + Main.rand.NextVector2Square(-30f, 30f), permafrost ? 60 : 235);
				dust2.velocity = Vector2.UnitY.RotatedByRandom(0.07999999821186066) * (0f - Main.rand.NextFloat(3f, 4.45f));
				dust2.scale = Main.rand.NextFloat(1.35f, 1.6f);
				dust2.fadeIn = 1.25f;
				dust2.noGravity = true;
			}
		}
		hasSummonedSepulcher1 = true;
		hasSummonedSepulcher2 = (double)base.NPC.life <= (double)base.NPC.lifeMax * 0.08;
		if (Main.netMode != 1)
		{
			if (permafrost)
			{
				for (int x = 0; x < 5; x++)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), spawnX + 50, tempSpawnY, ModContent.NPCType<Dragonfolly>());
					spawnX += spawnXAdd;
					NPC.NewNPC(base.NPC.GetSource_FromAI(), spawnX2 - 50, tempSpawnY, ModContent.NPCType<Dragonfolly>());
					spawnX2 -= spawnXAdd;
					tempSpawnY += spawnYAdd;
				}
				spawnX = spawnXReset;
				spawnX2 = spawnXReset2;
				spawnY = spawnYReset;
			}
			else
			{
				List<int> hearts = new List<int>();
				for (int k = 0; k < 5; k++)
				{
					hearts.Add(NPC.NewNPC(base.NPC.GetSource_FromAI(), spawnX + 50, tempSpawnY, ModContent.NPCType<BrimstoneHeart>()));
					spawnX += spawnXAdd;
					hearts.Add(NPC.NewNPC(base.NPC.GetSource_FromAI(), spawnX2 - 50, tempSpawnY, ModContent.NPCType<BrimstoneHeart>()));
					spawnX2 -= spawnXAdd;
					tempSpawnY += spawnYAdd;
				}
				ConnectAllBrimstoneHearts(hearts);
				spawnX = spawnXReset;
				spawnX2 = spawnXReset2;
				spawnY = spawnYReset;
				if (NPC.CountNPCS(ModContent.NPCType<SepulcherHead>()) <= 0)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)safeBox.Center().X, (int)safeBox.Bottom().Y, ModContent.NPCType<SepulcherHead>());
				}
			}
			base.NPC.netUpdate = true;
		}
		SoundEngine.PlaySound(in SepulcherSummonSound, target.Center);
	}

	public void DoBrothersSpawningCastAnimation(int bodyWidth, int bodyHeight)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.NPC.target];
		Vector2 leftOfCircle = base.NPC.Center + Vector2.UnitY * (float)bodyHeight * 0.5f - Vector2.UnitX * (float)bodyWidth * 0.45f;
		Vector2 rightOfCircle = base.NPC.Center + Vector2.UnitY * (float)bodyHeight * 0.5f + Vector2.UnitX * (float)bodyWidth * 0.45f;
		if (Main.netMode != 1 && catastropheSpawnPosition == Vector2.Zero)
		{
			catastropheSpawnPosition = base.NPC.Center - Vector2.UnitX * 500f;
			cataclysmSpawnPosition = base.NPC.Center + Vector2.UnitX * 500f;
			base.NPC.netUpdate = true;
		}
		if ((float)attackCastDelay < 105f && (float)attackCastDelay >= 60f)
		{
			float castCompletion = Utils.GetLerpValue(105f, 60f, attackCastDelay);
			Vector2 relativePosition = Vector2.CatmullRom(leftOfCircle + Vector2.UnitY * 1000f, leftOfCircle, catastropheSpawnPosition, catastropheSpawnPosition + Vector2.UnitY * 1000f, castCompletion);
			Vector2 rightDustPosition = Vector2.CatmullRom(rightOfCircle + Vector2.UnitY * 1000f, rightOfCircle, cataclysmSpawnPosition, cataclysmSpawnPosition + Vector2.UnitY * 1000f, castCompletion);
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(relativePosition, Vector2.Zero, affectedByGravity: false, 20, 2.8f - (float)attackCastDelay * 0.01f, permafrost ? Color.Cyan : Color.Red, AddativeBlend: true, needed: true));
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(rightDustPosition, Vector2.Zero, affectedByGravity: false, 20, 2.8f - (float)attackCastDelay * 0.01f, permafrost ? Color.Cyan : Color.Red, AddativeBlend: true, needed: true));
		}
		if ((float)attackCastDelay < 60f)
		{
			if (attackCastDelay == 59)
			{
				GeneralParticleHandler.SpawnParticle(new BloomParticle(catastropheSpawnPosition, Vector2.Zero, Color.DeepSkyBlue, 0f, 1.4f, 59, fade: false));
				GeneralParticleHandler.SpawnParticle(new BloomParticle(catastropheSpawnPosition, Vector2.Zero, Color.DeepSkyBlue, 0f, 1f, 59, fade: false));
				GeneralParticleHandler.SpawnParticle(new BloomParticle(cataclysmSpawnPosition, Vector2.Zero, new Color(121, 21, 77), 0f, 1.4f, 59, fade: false));
				GeneralParticleHandler.SpawnParticle(new BloomParticle(cataclysmSpawnPosition, Vector2.Zero, new Color(121, 21, 77), 0f, 1f, 59, fade: false));
			}
			float burnPower = Utils.GetLerpValue(60f, 20f, attackCastDelay);
			if ((float)attackCastDelay == 0f)
			{
				burnPower = 4f;
			}
			for (int i = 0; (float)i < MathHelper.Lerp(1f, 6f, burnPower); i++)
			{
				Vector2 velOffset = CalamityUtils.RandomVelocity(100f, 70f, 150f, 0.04f);
				velOffset *= Main.rand.NextFloat(2f, 13f);
				GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(catastropheSpawnPosition + velOffset * 4.5f, -velOffset * 0.25f, affectedByGravity: false, 9, Main.rand.NextFloat(0.08f, 0.16f) - (float)attackCastDelay * 0.002f, Main.rand.NextBool(5) ? Color.DeepSkyBlue : Color.Red));
				GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(cataclysmSpawnPosition + velOffset * 4.5f, -velOffset * 0.25f, affectedByGravity: false, 9, Main.rand.NextFloat(0.08f, 0.16f) - (float)attackCastDelay * 0.002f, (Color)(Main.rand.NextBool(5) ? new Color(121, 21, 77) : Color.Red)));
			}
		}
		if (attackCastDelay != 0)
		{
			return;
		}
		for (int j = 0; j < 30; j++)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(catastropheSpawnPosition, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f), affectedByGravity: false, 35, Main.rand.NextFloat(1.2f, 1.45f), Main.rand.NextBool() ? Color.DeepSkyBlue : Color.Red));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(cataclysmSpawnPosition, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f), affectedByGravity: false, 35, Main.rand.NextFloat(1.2f, 1.45f), (Color)(Main.rand.NextBool() ? new Color(80, 21, 77) : Color.Red)));
			Dust dust = Dust.NewDustPerfect(catastropheSpawnPosition, 279, Utils.RotatedByRandom(new Vector2(9f, 9f), 100.0) * Main.rand.NextFloat(0.1f, 1.5f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.5f, 1.85f);
			dust.color = Color.DeepSkyBlue;
			Dust dust2 = Dust.NewDustPerfect(cataclysmSpawnPosition, 279, Utils.RotatedByRandom(new Vector2(9f, 9f), 100.0) * Main.rand.NextFloat(0.1f, 1.5f));
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(1.5f, 1.85f);
			dust2.color = new Color(80, 21, 77);
		}
		if (!BossRushEvent.BossRushActive)
		{
			string key = "Mods.CalamityMod.Status.Boss.SCalBrothersText";
			if (permafrost)
			{
				key = "Mods.CalamityMod.Status.Boss.PermafrostDoGText";
			}
			else if (DownedBossSystem.downedCalamitas)
			{
				key += "Rematch";
			}
			CalamityUtils.BroadcastLocalizedText(key, permafrost ? permafrostTextColor : textColor);
		}
		if (Main.netMode != 1)
		{
			bool broDirection = Main.rand.NextBool();
			CalamityUtils.SpawnBossBetter(catastropheSpawnPosition, permafrost ? ModContent.NPCType<DevourerofGodsHead>() : ModContent.NPCType<SupremeCatastrophe>(), null, (!broDirection) ? 1 : (-1));
			if (!permafrost)
			{
				CalamityUtils.SpawnBossBetter(cataclysmSpawnPosition, ModContent.NPCType<SupremeCataclysm>(), null, broDirection ? 1 : (-1));
			}
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCKilled/RavagerDeath1");
		style.Pitch = -0.2f;
		SoundEngine.PlaySound(in style, cataclysmSpawnPosition);
		style = new SoundStyle("CalamityMod/Sounds/NPCKilled/RavagerDeath2");
		style.Pitch = -0.2f;
		SoundEngine.PlaySound(in style, catastropheSpawnPosition);
		hasSummonedBrothers = true;
	}

	public void ConnectAllBrimstoneHearts(List<int> heartIndices)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		ModContent.NPCType<BrimstoneHeart>();
		IEnumerable<NPC> hearts = heartIndices.Select((int num) => Main.npc[num]);
		hearts = hearts.OrderByDescending(delegate(NPC nPC)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return Math.Abs(nPC.Center.X - (float)((Rectangle)(ref safeBox)).Left);
		}).ToList();
		heartIndices.First();
		heartIndices.Last();
		heartIndices = heartIndices.OrderByDescending(delegate(int num)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return Math.Abs(Main.npc[num].Center.X - (float)((Rectangle)(ref safeBox)).Left);
		}).ToList();
		for (int i = 0; i < hearts.Count(); i++)
		{
			NPC heart = hearts.ElementAt(i);
			Vector2 endpoint = safeBox.TopLeft();
			_ = Vector2.Zero;
			for (int j = 0; j < 2; j++)
			{
				int tries = 0;
				do
				{
					endpoint.X = heart.Center.X + (float)(j == 0).ToDirectionInt() * Main.rand.NextFloat(75f, 250f);
					tries++;
				}
				while (tries < 100 && Math.Abs(endpoint.X - (float)((Rectangle)(ref safeBox)).Center.X) > (float)safeBox.Width * 0.48f);
				if (tries >= 100)
				{
					endpoint.X = MathHelper.Clamp(endpoint.X, (float)((Rectangle)(ref safeBox)).Left, (float)((Rectangle)(ref safeBox)).Right);
				}
				heart.ModNPC<BrimstoneHeart>().ChainEndpoints.Add(endpoint);
			}
			if (Main.rand.NextBool())
			{
				endpoint.X = heart.Center.X + (float)Main.rand.NextBool().ToDirectionInt() * Main.rand.NextFloat(45f, 360f);
				endpoint.X = MathHelper.Clamp(endpoint.X, (float)((Rectangle)(ref safeBox)).Left, (float)((Rectangle)(ref safeBox)).Right);
				heart.ModNPC<BrimstoneHeart>().ChainEndpoints.Add(endpoint);
			}
			heart.netUpdate = true;
		}
	}

	public void HandleMusicVariables()
	{
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		CalamityGlobalNPC.SCalGrief = -1;
		CalamityGlobalNPC.SCalLament = -1;
		CalamityGlobalNPC.SCalEpiphany = -1;
		CalamityGlobalNPC.SCalAcceptance = -1;
		if (startFifthAttack && gettingTired5 && (giveUpCounter < 1160 || hasDoneDeathAnim))
		{
			CalamityGlobalNPC.SCalAcceptance = base.NPC.whoAmI;
		}
		else if (lifeRatio <= 0.3f)
		{
			CalamityGlobalNPC.SCalEpiphany = base.NPC.whoAmI;
		}
		else if (lifeRatio <= 0.5f)
		{
			CalamityGlobalNPC.SCalLament = base.NPC.whoAmI;
		}
		else
		{
			CalamityGlobalNPC.SCalGrief = base.NPC.whoAmI;
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<OmegaHealingPotion>();
	}

	public override void OnKill()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			if (Main.player[base.NPC.target].Calamity().sCalKillCount < 5)
			{
				Main.player[base.NPC.target].Calamity().sCalKillCount++;
			}
			if (!BossRushEvent.BossRushActive)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y + 12, permafrost ? ModContent.NPCType<Archmage>() : ModContent.NPCType<BrimstoneWitch>());
			}
			DownedBossSystem.downedCalamitas = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CalamitasCoffer>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[7]
		{
			ModContent.ItemType<Violence>(),
			ModContent.ItemType<Condemnation>(),
			ModContent.ItemType<Heresy>(),
			ModContent.ItemType<Vehemence>(),
			ModContent.ItemType<Perdition>(),
			ModContent.ItemType<Vigilance>(),
			ModContent.ItemType<Sacrifice>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<AshesofAnnihilation>(), 1, 25, 30));
		IItemDropRule scalVanitySet = ItemDropRule.Common(ModContent.ItemType<AshenHorns>(), 7);
		scalVanitySet.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SCalMask>()));
		scalVanitySet.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SCalRobes>()));
		scalVanitySet.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SCalBoots>()));
		normalOnly.Add(scalVanitySet);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.AddIf(() => CalamityWorld.death, ModContent.ItemType<Levi>());
		npcLoot.AddIf(() => CalamityWorld.death, ModContent.ItemType<GaelsGreatsword>());
		npcLoot.Add(ModContent.ItemType<SupremeCalamitasTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<CalamitasRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<SlurperPole>()), hideLootReport: true);
		npcLoot.Add(ItemDropRule.ByCondition(DropHelper.If((DropAttemptInfo info) => info.npc.type == ModContent.NPCType<SupremeCalamitas>() && info.npc.ModNPC<SupremeCalamitas>().permafrost, ui: false), ModContent.ItemType<ColdheartIcicle>()));
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedCalamitas, ModContent.ItemType<LoreCalamitas>(), ui: true, DropHelper.FirstKillText);
		npcLoot.Add(ItemDropRule.ByCondition(DropHelper.If(() => DownedBossSystem.downedExoMechs && !DownedBossSystem.downedCalamitas, ui: true, DropHelper.CynosureText), ModContent.ItemType<LoreCynosure>()));
	}

	public override bool CheckDead()
	{
		if (BossRushEvent.BossRushActive)
		{
			return true;
		}
		base.NPC.life = 1;
		base.NPC.active = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.netUpdate = true;
		return false;
	}

	public override bool CheckActive()
	{
		return canDespawn;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 1.5f;
		return null;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		cooldownSlot = 1;
		Vector2 val = base.NPC.Center + shieldRotation.ToRotationVector2() * 24f;
		Vector2 shieldTop = val - (shieldRotation + (float)Math.PI / 2f).ToRotationVector2() * 61f;
		Vector2 shieldBottom = val - (shieldRotation + (float)Math.PI / 2f).ToRotationVector2() * 61f;
		float _ = 0f;
		if (!Collision.CheckAABBvLineCollision(target.TopLeft, target.Size, shieldTop, shieldBottom, 64f, ref _) || !(shieldOpacity > 0.55f))
		{
			Rectangle hitbox = base.NPC.Hitbox;
			if (!((Rectangle)(ref hitbox)).Intersects(target.Hitbox))
			{
				return false;
			}
		}
		return spawnArena;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			FrameType = FrameAnimationType.UpwardDraft;
			FrameChangeSpeed = 0.2f;
		}
		bool wormAlive = false;
		if (CalamityGlobalNPC.SCalWorm != -1)
		{
			wormAlive = Main.npc[CalamityGlobalNPC.SCalWorm].active;
		}
		int shootRate = (wormAlive ? 200 : 100);
		if (FrameType == FrameAnimationType.BlastPunchCast && (base.NPC.localAI[1] > (float)(shootRate - 18) || base.NPC.localAI[1] <= 15f))
		{
			if (base.NPC.localAI[1] > (float)(shootRate - 18))
			{
				base.NPC.frame.Y = (int)MathHelper.Lerp(0f, 3f, Utils.GetLerpValue(shootRate - 18, shootRate, base.NPC.localAI[1], clamped: true));
			}
			else
			{
				base.NPC.frame.Y = (int)MathHelper.Lerp(3f, 5f, Utils.GetLerpValue(0f, 15f, base.NPC.localAI[1], clamped: true));
			}
			base.NPC.frame.Y += (int)FrameType * 6;
		}
		else
		{
			base.NPC.frameCounter += FrameChangeSpeed;
			base.NPC.frameCounter %= 6.0;
			base.NPC.frame.Y = (int)base.NPC.frameCounter + (int)FrameType * 6;
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (!willCharge)
		{
			return null;
		}
		return Color.Lerp(Color.Red, drawColor, 0.7f) * base.NPC.Opacity * 0.45f;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = ((DownedBossSystem.downedCalamitas && !BossRushEvent.BossRushActive) ? TextureAssets.Npc[base.Type].Value : HoodedTexture.Value);
		bool inPhase2 = base.NPC.ai[0] >= 3f && ((double)base.NPC.life > (double)base.NPC.lifeMax * 0.01 || permafrost);
		if (permafrost)
		{
			texture2D15 = PermafrostTexture.Value;
		}
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)texture2D15.Width / 2f, (float)(texture2D15.Height / Main.npcFrameCount[base.Type]) / 2f);
		int afterimageAmt = 7;
		Rectangle frame = texture2D15.Frame(2, Main.npcFrameCount[base.Type], base.NPC.frame.Y / Main.npcFrameCount[base.Type], base.NPC.frame.Y % Main.npcFrameCount[base.Type]);
		if (CalamityClientConfig.Instance.Afterimages && (!permafrost || base.NPC.ai[1] != 2f))
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture2D15.Width / 2f, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width / 2f, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		if (!permafrost || base.NPC.ai[1] != 2f)
		{
			if (inPhase2)
			{
				if (!DownedBossSystem.downedCalamitas)
				{
					drawLocation += Main.rand.NextVector2Circular(0.25f, 0.7f);
				}
				Color auraColor = base.NPC.GetAlpha(permafrost ? Color.Cyan : Color.Red) * 0.4f;
				for (int j = 0; j < 7; j++)
				{
					Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)j / 7f + Main.GlobalTimeWrappedHourly * 4f).ToRotationVector2();
					rotationalDrawOffset *= MathHelper.Lerp(3f, 4.25f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f);
					spriteBatch.Draw(texture2D15, drawLocation + rotationalDrawOffset, (Rectangle?)frame, auraColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale * 1.1f, spriteEffects, 0f);
				}
			}
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		if (!base.NPC.IsABestiaryIconDummy)
		{
			DrawForcefield(spriteBatch);
			DrawShield(spriteBatch);
		}
		return false;
	}

	public void DrawForcefield(SpriteBatch spriteBatch)
	{
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.NPC.target];
		float opacity = 1f;
		spriteBatch.EnterShaderRegion();
		float intensity = (float)hitTimer / 35f;
		if (base.NPC.dontTakeDamage && attackCastDelay <= 0)
		{
			intensity = 0.75f + Math.Abs((float)Math.Cos(Main.GlobalTimeWrappedHourly * 1.7f)) * 0.1f;
		}
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (num < 0.05f)
		{
			forcefieldOpacity = 0.75f;
		}
		if (num <= 0.01f)
		{
			forcefieldOpacity = 0.6f;
		}
		float flickerPower = 0f;
		if (num < 0.6f)
		{
			flickerPower += 0.1f;
		}
		if (num < 0.3f)
		{
			flickerPower += 0.25f;
		}
		if (postMusicHit)
		{
			flickerPower += 0.61f;
		}
		if (num < 0.05f)
		{
			flickerPower += Main.rand.NextFloat(0.7f, 1f);
		}
		if (num <= 0.01f)
		{
			flickerPower += 0.08f;
		}
		opacity = forcefieldOpacity;
		opacity *= MathHelper.Lerp(1f, MathHelper.Max(1f - flickerPower, 0.56f), (float)Math.Pow(Math.Cos(Main.GlobalTimeWrappedHourly * MathHelper.Lerp(3f, 5f, flickerPower)), 24.0));
		if (!base.NPC.dontTakeDamage && (willCharge || base.NPC.ai[1] == 2f))
		{
			intensity = 1.1f;
		}
		intensity *= ((musicSyncCounter <= 0 && musicSyncCounter > -30) ? Utils.GetLerpValue(120f, 0f, musicSyncCounter, clamped: true) : 0.75f);
		opacity *= ((musicSyncCounter <= 0 && musicSyncCounter > -30) ? Utils.GetLerpValue(120f, 0f, musicSyncCounter, clamped: true) : 0.75f);
		Texture2D forcefieldTexture = ForcefieldTexture.Value;
		GameShaders.Misc["CalamityMod:SupremeShield"].UseImage1("Images/Misc/Perlin");
		Color forcefieldColor = Color.DarkViolet;
		Color secondaryForcefieldColor = (permafrost ? Color.Cyan : Color.Red) * 1.4f;
		if (!base.NPC.dontTakeDamage && willCharge && base.NPC.ai[1] != 2f)
		{
			forcefieldColor = Color.Magenta;
			secondaryForcefieldColor = Color.Lerp(secondaryForcefieldColor, Color.Lerp(Color.Red, Color.Magenta, 0.3f), 0.7f);
		}
		else
		{
			forcefieldColor = Color.DarkViolet;
		}
		forcefieldColor *= opacity;
		secondaryForcefieldColor *= opacity;
		GameShaders.Misc["CalamityMod:SupremeShield"].UseSecondaryColor(secondaryForcefieldColor);
		GameShaders.Misc["CalamityMod:SupremeShield"].UseColor(forcefieldColor);
		GameShaders.Misc["CalamityMod:SupremeShield"].UseSaturation(1f);
		GameShaders.Misc["CalamityMod:SupremeShield"].UseOpacity(0.65f);
		GameShaders.Misc["CalamityMod:SupremeShield"].Apply();
		Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/CentralGold", (AssetRequestMode)2).Value;
		Texture2D immuneTex = ModContent.Request<Texture2D>("CalamityMod/Particles/SemiCircularSmearVertical", (AssetRequestMode)2).Value;
		if (postMusicHit)
		{
			Vector2 val = base.NPC.Center - Main.screenPosition;
			Color white = Color.White;
			((Color)(ref white)).A = 0;
			spriteBatch.Draw(centerTexture, val, (Rectangle?)null, white * opacity * 2f, rotateAwayPlayer, centerTexture.Size() * 0.5f, forcefieldScale * 0.088f * forcefieldPureVisualScale, (SpriteEffects)0, 0f);
		}
		if (!base.NPC.dontTakeDamage)
		{
			spriteBatch.Draw(forcefieldTexture, base.NPC.Center - Main.screenPosition, (Rectangle?)null, Color.White * opacity, postMusicHit ? rotateToPlayer : 0f, forcefieldTexture.Size() * 0.5f, forcefieldScale * 3f * forcefieldPureVisualScale, (SpriteEffects)0, 0f);
		}
		else
		{
			spriteBatch.Draw(immuneTex, base.NPC.Center - Main.screenPosition, (Rectangle?)null, Color.White * opacity * 0.3f, rotateToPlayer, immuneTex.Size() * 0.5f, forcefieldScale * 1.35f * forcefieldPureVisualScale, (SpriteEffects)0, 0f);
		}
		spriteBatch.ExitShaderRegion();
	}

	public void DrawShield(SpriteBatch spriteBatch)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Invalid comparison between Unknown and I4
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		float jawRotation = shieldRotation;
		float jawRotationOffset = 0f;
		if (base.NPC.ai[1] == 2f)
		{
			jawRotationOffset -= 0.71f;
		}
		else if (willCharge && base.NPC.ai[1] != 2f && AttackCloseToBeingOver)
		{
			jawRotationOffset += MathHelper.Lerp(0.04f, -0.82f, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 17.2f) * 0.5f + 0.5f);
		}
		Color shieldColor = Color.White * shieldOpacity;
		Texture2D shieldSkullTexture = ShieldTopTexture.Value;
		Texture2D shieldJawTexture = ShieldBottomTexture.Value;
		Vector2 drawPosition = base.NPC.Center + shieldRotation.ToRotationVector2() * 24f - Main.screenPosition;
		Vector2 jawDrawPosition = drawPosition;
		SpriteEffects direction = (SpriteEffects)((!(Math.Cos(shieldRotation) > 0.0)) ? 2 : 0);
		if ((int)direction == 2)
		{
			jawDrawPosition += (shieldRotation - (float)Math.PI / 2f).ToRotationVector2() * 42f;
		}
		else
		{
			jawDrawPosition += (shieldRotation + (float)Math.PI / 2f).ToRotationVector2() * 42f;
			jawRotationOffset *= -1f;
		}
		spriteBatch.Draw(shieldJawTexture, jawDrawPosition, (Rectangle?)null, shieldColor, jawRotation + jawRotationOffset, shieldJawTexture.Size() * 0.5f, 1f, direction, 0f);
		spriteBatch.Draw(shieldSkullTexture, drawPosition, (Rectangle?)null, shieldColor, shieldRotation, shieldSkullTexture.Size() * 0.5f, 1f, direction, 0f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1)
		{
			hitTimer = 35;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = Main.rand.Next(5, 8);
			SoundEngine.PlaySound(in HurtSound, base.NPC.Center);
		}
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, permafrost ? 161 : 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (base.NPC.height = 100);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int onHitDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, permafrost ? 161 : 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[onHitDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[onHitDust].scale = 0.5f;
				Main.dust[onHitDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int onHitDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, permafrost ? 161 : 235, 0f, 0f, 100, default(Color), 3f);
			Main.dust[onHitDust2].noGravity = true;
			Dust obj2 = Main.dust[onHitDust2];
			obj2.velocity *= 5f;
			onHitDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, permafrost ? 161 : 235, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[onHitDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	static SupremeCalamitas()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		normalDR = 0.25f;
		enragedDR = 0.9999f;
		textColor = Color.Orange;
		permafrostTextColor = Color.LightCyan;
		SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/SupremeCalamitasSpawn")
		{
			Volume = 1.2f
		};
		SepulcherSummonSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/SepulcherSpawn");
		BrimstoneShotSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneShoot");
		BrotherHit = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrothersHurt", 2);
		BrotherDeath = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrothersDeath", 2);
		CatastropheSwing = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/CatastropheResonanceSlash");
		BrimstoneBigShotSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneBigShoot");
		DashSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/SCalDash");
		HellblastSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneHellblastSound");
		HurtSound = new SoundStyle("CalamityMod/Sounds/NPCHit/ShieldHit", 3);
		BulletHellSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/SCalRumble");
		BulletHellEndSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/SCalEndBH");
		GiveUpSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/SupremeCalamitasGiveUp");
		DartDamage = 80;
		SkullDamage = 85;
		HellblastDamage = 95;
		FireblastDamage = 95;
		GigablastDamage = 105;
	}
}
