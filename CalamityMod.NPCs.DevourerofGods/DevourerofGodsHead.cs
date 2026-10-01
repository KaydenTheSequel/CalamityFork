using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Placeables.FurnitureCosmilite;
using CalamityMod.Items.Potions;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Melee.Yoyos;
using CalamityMod.UI.DialogueDisplay;
using CalamityMod.UI.DialogueDisplay.DisplayEffects;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DevourerofGods;

[LongDistanceNetSync]
public class DevourerofGodsHead : ModNPC
{
	private enum LaserWallPhase
	{
		SetUp,
		FireLaserWalls,
		End
	}

	public static int phase1IconIndex;

	public static int phase2IconIndex;

	public static Asset<Texture2D> Texture_Glow_Purple;

	public static Asset<Texture2D> Texture_Glow_Cyan;

	public static Asset<Texture2D> TextureP2;

	public static Asset<Texture2D> TextureP2_Glow_Purple;

	public static Asset<Texture2D> TextureP2_Glow_Cyan;

	public static Asset<Texture2D> JawTexture;

	public static Asset<Texture2D> JawTexture_Glow;

	public static Asset<Texture2D> JawTextureP2;

	public static Asset<Texture2D> JawTextureP2_Glow;

	public static Asset<Texture2D> GodSlayerDashJawTexture;

	public static Asset<Texture2D> TextureP2_Full;

	private const float laserVelocity = 14f;

	private const int totalShots = 10;

	public bool AttemptingToEnterPortal;

	public int PortalIndex;

	private bool tail;

	private int minLength;

	private int maxLength;

	private bool spawnedGuardians;

	private bool spawnedGuardians2;

	private int spawnDoGCountdown;

	private bool hasCreatedPhase1Portal;

	public bool Phase2Started;

	public bool AwaitingPhase2Teleport;

	public int laserWallPhase;

	private const int idleCounterMax = 300;

	private int idleCounter;

	public const float LaserWallCooldown = 1800f;

	public int postTeleportTimer;

	public int teleportTimer;

	private const int TimeBeforeTeleport_Death = 150;

	private const int TimeBeforeTeleport_Revengeance = 150;

	private const int TimeBeforeTeleport_Expert = 160;

	private const int TimeBeforeTeleport_Normal = 180;

	private bool spawnedGuardians3;

	private const float AlphaGateValue = 1660f;

	public const float SkyColorTransitionTime = 90f;

	public Vector2 PortalEntryLocation;

	public bool doTpFX;

	public int dashes;

	public bool Dying;

	public int DeathAnimationTimer;

	public int DestroyedSegmentCount;

	public float JawRotation;

	public float JawChompDownProgress;

	public float GodSlayerDashJawFadeProgress;

	public float GodSlayerDashJawTimer;

	public bool ShouldSpawnChompVFX;

	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/DevourerSpawn");

	public static readonly SoundStyle AttackSound = new SoundStyle("CalamityMod/Sounds/Custom/DevourerAttack");

	public static readonly SoundStyle RiftOpenSound = new SoundStyle("CalamityMod/Sounds/Custom/DevourerRiftOpen");

	public static readonly SoundStyle RiftBuildingSound = new SoundStyle("CalamityMod/Sounds/Custom/DevourerRiftBuilding");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/OtherworldlyHit");

	public static readonly SoundStyle DeathAnimationSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerDeath");

	public static readonly SoundStyle DeathExplosionSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerDeathImpact");

	public static readonly SoundStyle DeathSegmentSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerSegmentBreak", 4);

	public float extrapitch;

	public static int LaserWallDamage = 75;

	public static int LaserWallMiddleBeamDamage = 85;

	public static int FireballDamage = 60;

	private Vector2 noiseOffset;

	public static Color SpecialMoveColor
	{
		get
		{
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			if (CalamityClientConfig.Instance.TextEffects)
			{
				return Color.Lerp(Color.Fuchsia, Color.Cyan, MathHelper.SmoothStep(0f, 1f, (MathF.Sin(Main.GlobalTimeWrappedHourly * 2f) + 1f) * 0.5f));
			}
			return Color.Cyan;
		}
	}

	public bool isInPassiveState
	{
		get
		{
			if (Phase2Started && base.NPC.ai[3] == 1f)
			{
				return true;
			}
			if (!Phase2Started && base.NPC.ai[3] == 0f)
			{
				return true;
			}
			return false;
		}
	}

	public bool isInAgressiveState
	{
		get
		{
			if (Phase2Started && base.NPC.ai[3] == 0f)
			{
				return true;
			}
			if (!Phase2Started && base.NPC.ai[3] == 1f)
			{
				return true;
			}
			return false;
		}
	}

	public bool isInLaserWallState
	{
		get
		{
			if (base.NPC.ai[3] == 2f)
			{
				return true;
			}
			return false;
		}
	}

	public bool isInPostWallState
	{
		get
		{
			if (base.NPC.ai[3] > 2f)
			{
				return true;
			}
			return false;
		}
	}

	public override void Load()
	{
		string phase1IconPath = "CalamityMod/NPCs/DevourerofGods/DevourerofGodsHead_Head_Boss";
		string phase2IconPath = "CalamityMod/NPCs/DevourerofGods/DevourerofGodsHead_P2_Head_Boss";
		phase1IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase1IconPath);
		phase2IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase2IconPath);
	}

	private Vector2 AdjustedPlayerCenter(float predictiveness = 0f)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.NPC.target];
		if (!Main.getGoodWorld)
		{
			return player.Center;
		}
		return player.Center + player.velocity * predictiveness;
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.6f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.6f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 60f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 40f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 82f;
		value.Position.Y += 38f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			Texture_Glow_Purple = ModContent.Request<Texture2D>(Texture + "_Glow_Purple", (AssetRequestMode)2);
			Texture_Glow_Cyan = ModContent.Request<Texture2D>(Texture + "_Glow_Cyan", (AssetRequestMode)2);
			TextureP2 = ModContent.Request<Texture2D>(Texture + "_Jawless_P2", (AssetRequestMode)2);
			TextureP2_Glow_Purple = ModContent.Request<Texture2D>(Texture + "_Jawless_P2_Glow_Purple", (AssetRequestMode)2);
			TextureP2_Glow_Cyan = ModContent.Request<Texture2D>(Texture + "_Jawless_P2_Glow_Cyan", (AssetRequestMode)2);
			JawTexture = ModContent.Request<Texture2D>(Texture + "_Jaw", (AssetRequestMode)2);
			JawTexture_Glow = ModContent.Request<Texture2D>(Texture + "_Jaw_Glow", (AssetRequestMode)2);
			JawTextureP2 = ModContent.Request<Texture2D>(Texture + "_Jaw_P2", (AssetRequestMode)2);
			JawTextureP2_Glow = ModContent.Request<Texture2D>(Texture + "_Jaw_P2_Glow", (AssetRequestMode)2);
			GodSlayerDashJawTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/DevourerofGods/GodSlayerDashJaw", (AssetRequestMode)2);
			TextureP2_Full = ModContent.Request<Texture2D>(Texture + "_P2", (AssetRequestMode)2);
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.DevourerofGods")
		});
	}

	public override void SetDefaults()
	{
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 250;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 104;
		base.NPC.height = 104;
		base.NPC.defense = 50;
		base.NPC.LifeMaxNERB(750000, 900000, 1500000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.boss = true;
		base.NPC.value = Item.buyPrice(1, 50);
		base.NPC.Opacity = 0f;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.ai[3] = 1f;
		base.NPC.netAlways = true;
		if (Main.rand.NextBool())
		{
			base.NPC.velocity = new Vector2(-60f, 0f);
		}
		else
		{
			base.NPC.velocity = new Vector2(60f, 0f);
		}
		if (Main.zenithWorld)
		{
			base.NPC.scale *= 1.5f;
		}
		if (Main.zenithWorld)
		{
			base.NPC.takenDamageMultiplier = 2f;
		}
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.life = base.NPC.lifeMax;
	}

	public override void BossHeadSlot(ref int index)
	{
		if ((Phase2Started && (base.NPC.localAI[2] > 60f || AwaitingPhase2Teleport)) || base.NPC.Opacity < 0.1f)
		{
			index = -1;
		}
		else if (Phase2Started && !AwaitingPhase2Teleport)
		{
			index = phase2IconIndex;
		}
		else
		{
			index = phase1IconIndex;
		}
	}

	public override void BossHeadRotation(ref float rotation)
	{
		if (Phase2Started && base.NPC.localAI[2] <= 60f)
		{
			rotation = base.NPC.rotation;
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		Color messageColor = Color.Cyan;
		CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DoGSpawn", messageColor);
		DialogueDisplaySystem.StartDialogue("Mods.CalamityMod.DevourerOfGods.Phases", (Entity)base.NPC, 0, 120, false, (DisplayEffect)new BossText(), -1f, -1, -1);
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		bool wasDyingBefore = Dying;
		writer.Write(base.NPC.Calamity().velocityPriorToPhaseSwap);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(spawnedGuardians);
		writer.Write(spawnedGuardians2);
		writer.Write(spawnedGuardians3);
		writer.Write(Phase2Started);
		writer.Write(hasCreatedPhase1Portal);
		writer.Write(AwaitingPhase2Teleport);
		writer.Write(spawnDoGCountdown);
		writer.Write(PortalIndex);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		writer.Write(idleCounter);
		writer.Write(laserWallPhase);
		writer.Write(postTeleportTimer);
		writer.Write(teleportTimer);
		writer.Write(base.NPC.Opacity);
		writer.Write(Dying);
		writer.Write(DeathAnimationTimer);
		writer.Write(DestroyedSegmentCount);
		writer.Write(JawRotation);
		writer.Write(JawChompDownProgress);
		writer.Write(GodSlayerDashJawFadeProgress);
		writer.Write(GodSlayerDashJawTimer);
		writer.Write(ShouldSpawnChompVFX);
		writer.Write(base.NPC.frame.X);
		writer.Write(base.NPC.frame.Y);
		writer.Write(base.NPC.frame.Width);
		writer.Write(base.NPC.frame.Height);
		writer.Write(extrapitch);
		if (Main.dedServ && !wasDyingBefore && Dying)
		{
			base.NPC.ForceNetUpdate();
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.Calamity().velocityPriorToPhaseSwap = reader.ReadSingle();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		spawnedGuardians = reader.ReadBoolean();
		spawnedGuardians2 = reader.ReadBoolean();
		spawnedGuardians3 = reader.ReadBoolean();
		Phase2Started = reader.ReadBoolean();
		hasCreatedPhase1Portal = reader.ReadBoolean();
		AwaitingPhase2Teleport = reader.ReadBoolean();
		spawnDoGCountdown = reader.ReadInt32();
		PortalIndex = reader.ReadInt32();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		idleCounter = reader.ReadInt32();
		laserWallPhase = reader.ReadInt32();
		postTeleportTimer = reader.ReadInt32();
		teleportTimer = reader.ReadInt32();
		base.NPC.Opacity = reader.ReadSingle();
		Dying = reader.ReadBoolean();
		DeathAnimationTimer = reader.ReadInt32();
		DestroyedSegmentCount = reader.ReadInt32();
		JawRotation = reader.ReadSingle();
		JawChompDownProgress = reader.ReadSingle();
		GodSlayerDashJawFadeProgress = reader.ReadSingle();
		GodSlayerDashJawTimer = reader.ReadSingle();
		ShouldSpawnChompVFX = reader.ReadBoolean();
		extrapitch = reader.ReadSingle();
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
		if (frame.Width > 0 && frame.Height > 0)
		{
			base.NPC.frame = frame;
		}
	}

	public override void AI()
	{
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_0927: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b65: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b74: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b80: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d25: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3080: Unknown result type (might be due to invalid IL or missing references)
		//IL_308c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3091: Unknown result type (might be due to invalid IL or missing references)
		//IL_309b: Unknown result type (might be due to invalid IL or missing references)
		//IL_30a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_30a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_3015: Unknown result type (might be due to invalid IL or missing references)
		//IL_3020: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bed: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_3307: Unknown result type (might be due to invalid IL or missing references)
		//IL_310a: Unknown result type (might be due to invalid IL or missing references)
		//IL_311e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3123: Unknown result type (might be due to invalid IL or missing references)
		//IL_3128: Unknown result type (might be due to invalid IL or missing references)
		//IL_33b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_33b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_3168: Unknown result type (might be due to invalid IL or missing references)
		//IL_317c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3181: Unknown result type (might be due to invalid IL or missing references)
		//IL_3186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_4c99: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cce: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_49c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_49c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_49d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_49e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_49f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_49ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_34ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_34e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_3500: Unknown result type (might be due to invalid IL or missing references)
		//IL_350c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3559: Unknown result type (might be due to invalid IL or missing references)
		//IL_3351: Unknown result type (might be due to invalid IL or missing references)
		//IL_335c: Unknown result type (might be due to invalid IL or missing references)
		//IL_124f: Unknown result type (might be due to invalid IL or missing references)
		//IL_125a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_346d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3474: Unknown result type (might be due to invalid IL or missing references)
		//IL_3479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0deb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ded: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e07: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_349d: Unknown result type (might be due to invalid IL or missing references)
		//IL_34a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_34a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe7: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d12: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d22: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d40: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d68: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d83: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d88: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1381: Unknown result type (might be due to invalid IL or missing references)
		//IL_1026: Unknown result type (might be due to invalid IL or missing references)
		//IL_103a: Unknown result type (might be due to invalid IL or missing references)
		//IL_103f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1044: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e76: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e85: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ead: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ec6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3edf: Unknown result type (might be due to invalid IL or missing references)
		//IL_3eeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_4dd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_4df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_4dfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1121: Unknown result type (might be due to invalid IL or missing references)
		//IL_1135: Unknown result type (might be due to invalid IL or missing references)
		//IL_113a: Unknown result type (might be due to invalid IL or missing references)
		//IL_113f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b40: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b63: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b69: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b70: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14be: Unknown result type (might be due to invalid IL or missing references)
		//IL_14cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1505: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_117c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1181: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c11: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15de: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1603: Unknown result type (might be due to invalid IL or missing references)
		//IL_1650: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11da: Unknown result type (might be due to invalid IL or missing references)
		//IL_11df: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1564: Unknown result type (might be due to invalid IL or missing references)
		//IL_156b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1570: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1594: Unknown result type (might be due to invalid IL or missing references)
		//IL_159b: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_4783: Unknown result type (might be due to invalid IL or missing references)
		//IL_478e: Unknown result type (might be due to invalid IL or missing references)
		//IL_4793: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e35: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ce1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a29: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff4: Unknown result type (might be due to invalid IL or missing references)
		//IL_200d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2026: Unknown result type (might be due to invalid IL or missing references)
		//IL_2032: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_28da: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e28: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b24: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.DoGHead = base.NPC.whoAmI;
		CalamityGlobalNPC.DoGP2 = -1;
		if (CalamityServerConfig.Instance.BossesStopWeather)
		{
			CalamityWorld.StopRain();
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		else if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f && Main.time % 60.0 == 0.0)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (DeathAnimationTimer < 435 && !CalamityClientConfig.Instance.FancyBackgroundVisuals)
		{
			player.shimmerMonolithShader = true;
			if (Main.shimmerDarken > 0.75f)
			{
				Main.shimmerDarken = 0.75f;
			}
		}
		bool flies = base.NPC.ai[3] == 0f;
		if (Main.getGoodWorld)
		{
			flies = true;
		}
		Vector2 destination = AdjustedPlayerCenter(2f);
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = lifeRatio < 1f;
		bool phase3 = lifeRatio < 0.75f;
		bool bigDaddyPhase2 = lifeRatio < 0.65f;
		bool phase4 = lifeRatio < 0.65f;
		bool phase5 = lifeRatio < 0.5f;
		bool phase6 = lifeRatio < 0.25f;
		extrapitch = (Main.zenithWorld ? 0.3f : 0f);
		float segmentVelocity = (death ? 17.5f : 16f);
		if (expertMode)
		{
			segmentVelocity += 4f * (1f - (lifeRatio * 0.75f + 0.25f));
		}
		float speed = (death ? 16.5f : 15f);
		float turnSpeed = (death ? 0.33f : 0.3f);
		float homingSpeed = (death ? 30f : 24f);
		float homingTurnSpeed = 0.405f;
		if (expertMode)
		{
			speed += 3f * (1f - (lifeRatio * 0.75f + 0.25f));
			turnSpeed += 0.06f * (1f - (lifeRatio * 0.75f + 0.25f));
			homingSpeed += 12f * (1f - (lifeRatio * 0.75f + 0.25f));
			homingTurnSpeed += 0.15f * (1f - (lifeRatio * 0.75f + 0.25f));
		}
		float groundPhaseTurnSpeed = (death ? 0.24f : 0.21f);
		if (expertMode)
		{
			groundPhaseTurnSpeed += 0.1f * (1f - (lifeRatio * 0.75f + 0.25f));
		}
		groundPhaseTurnSpeed += Vector2.Distance(destination, base.NPC.Center) * 0.0002f;
		if (Vector2.Distance(destination, base.NPC.Center) < 320f)
		{
			groundPhaseTurnSpeed *= 0.75f;
		}
		int phaseLimit = 900;
		if (spawnedGuardians3)
		{
			phaseLimit -= 180;
		}
		if (Main.getGoodWorld)
		{
			homingSpeed *= 3f;
			homingTurnSpeed *= base.NPC.Distance(destination) / 500f;
			if (base.NPC.Distance(destination) > 1400f)
			{
				base.NPC.ai[1] = 1f;
			}
			if (base.NPC.ai[1] == -1f)
			{
				base.NPC.ai[1] = 0f;
			}
			else if (base.NPC.ai[1] == 1f)
			{
				homingTurnSpeed *= 5f;
				if (Vector2.Dot(base.NPC.DirectionTo(destination), base.NPC.velocity.SafeNormalize(Vector2.Zero)) > 0.95f)
				{
					base.NPC.ai[1] = -1f;
				}
			}
			else
			{
				speed *= 5f;
			}
			phaseLimit = ((base.NPC.ai[3] != 0f) ? (phaseLimit - 600) : (phaseLimit + 600));
			if (lifeRatio < 0.1f && base.NPC.ai[3] <= 1f)
			{
				SpawnTeleportLocation(player);
			}
		}
		AttemptingToEnterPortal = false;
		if (player.dead)
		{
			base.NPC.ai[3] = 3f;
			calamityGlobalNPC.newAI[2] = -1f;
			base.NPC.velocity.Y -= 4f;
			int bodyType = ModContent.NPCType<DevourerofGodsBody>();
			int tailType = ModContent.NPCType<DevourerofGodsTail>();
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				for (int a = 0; a < Main.maxNPCs; a++)
				{
					if (Main.npc[a].type == base.NPC.type || Main.npc[a].type == bodyType || Main.npc[a].type == tailType)
					{
						Main.npc[a].active = false;
						Main.npc[a].ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
				}
			}
		}
		float distanceFromTarget = Vector2.Distance(destination, base.NPC.Center);
		bool increaseSpeed = distanceFromTarget > 3200f;
		bool increaseSpeedMore = distanceFromTarget > 11200f;
		if (base.NPC.localAI[2] > 0f)
		{
			base.NPC.localAI[2]--;
			base.NPC.Calamity().ShouldCloseHPBar = true;
		}
		float timeWhenDoGShouldTeleportDuringPhase2Countdown = 61f;
		if (base.NPC.localAI[2] == timeWhenDoGShouldTeleportDuringPhase2Countdown + (float)(death ? 150 : (CalamityWorld.revenge ? 150 : (Main.expertMode ? 160 : 180))))
		{
			SpawnTeleportLocation(player, phase2Transition: true);
		}
		if (base.NPC.localAI[2] == timeWhenDoGShouldTeleportDuringPhase2Countdown)
		{
			Teleport(player, death, revenge, expertMode, phase5);
		}
		if (AwaitingPhase2Teleport && base.NPC.localAI[2] == 0f)
		{
			AwaitingPhase2Teleport = false;
		}
		if (Phase2Started && AwaitingPhase2Teleport && base.NPC.localAI[2] < 60f)
		{
			base.NPC.Opacity = 0f;
			base.NPC.dontTakeDamage = true;
		}
		if (bigDaddyPhase2)
		{
			if (!Phase2Started)
			{
				Phase2Started = true;
				if (Main.netMode != 1)
				{
					base.NPC.ai[3] = 0f;
					calamityGlobalNPC.newAI[1] = 0f;
					calamityGlobalNPC.newAI[2] = 0f;
					base.NPC.ForceNetUpdate();
				}
				base.NPC.localAI[2] = 705f;
			}
			if (base.NPC.localAI[2] <= 635f)
			{
				CalamityGlobalNPC.DoGP2 = base.NPC.whoAmI;
			}
			if (base.NPC.localAI[2] == 60f)
			{
				base.NPC.position = base.NPC.Center;
				base.NPC.width = (int)(186f * base.NPC.scale);
				base.NPC.height = (int)(186f * base.NPC.scale);
				NPC nPC = base.NPC;
				nPC.position -= base.NPC.Size * 0.5f;
				base.NPC.frame = new Rectangle(0, 0, 134, 196);
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			if (base.NPC.localAI[2] == 60f && !BossRushEvent.BossRushActive)
			{
				Color messageColor = Color.Cyan;
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DoGPhase2", messageColor);
				DialogueDisplaySystem.StartDialogue("Mods.CalamityMod.DevourerOfGods.Phases", (Entity)base.NPC, 2, 120, false, (DisplayEffect)new BossText(), -1f, -1, -1);
			}
		}
		float VelocityRotation;
		float VelocityRotation2;
		if (Phase2Started)
		{
			if (base.NPC.localAI[2] > 5f)
			{
				base.NPC.dontTakeDamage = true;
				float idealFlySpeed = 28f;
				float oldVelocity = ((Vector2)(ref base.NPC.velocity)).Length();
				float horizontalInterpolant = Utils.GetLerpValue(1200f, 600f, base.NPC.Center.Y, clamped: true);
				Vector2 idealDirection = base.NPC.velocity.SafeNormalize(-Vector2.UnitY);
				idealDirection = Vector2.Lerp(idealDirection, Vector2.UnitX * (float)Math.Sign(idealDirection.X), horizontalInterpolant);
				base.NPC.velocity = idealDirection * MathHelper.Lerp(oldVelocity, idealFlySpeed, 0.1f);
				base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)Math.PI / 2f;
				if (PortalIndex != -1)
				{
					Projectile portal = Main.projectile[PortalIndex];
					float newOpacity = 1f - Utils.GetLerpValue(200f, 130f, base.NPC.Distance(portal.Center), clamped: true);
					if (Main.netMode != 1 && newOpacity > 0f && base.NPC.Opacity > newOpacity)
					{
						base.NPC.Opacity = newOpacity;
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
					if (base.NPC.Opacity < 0.2f)
					{
						base.NPC.Opacity = 0f;
					}
					if (base.NPC.localAI[2] > 360f)
					{
						Main.projectile[PortalIndex].Center = base.NPC.Center + base.NPC.SafeDirectionTo(Main.projectile[PortalIndex].Center) * base.NPC.Distance(Main.projectile[PortalIndex].Center);
					}
				}
				if (Main.netMode != 1 && !hasCreatedPhase1Portal)
				{
					Vector2 portalSpawnPosition = base.NPC.Center + base.NPC.velocity.SafeNormalize(-Vector2.UnitY) * 1000f;
					PortalIndex = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), portalSpawnPosition, Vector2.Zero, ModContent.ProjectileType<DoGP1EndPortal>(), 0, 0f);
					hasCreatedPhase1Portal = true;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
				AttemptingToEnterPortal = true;
			}
			else
			{
				base.NPC.dontTakeDamage = postTeleportTimer > 0 || Dying;
				if (teleportTimer > 0)
				{
					teleportTimer--;
					if (teleportTimer == 0)
					{
						Teleport(player, death, revenge, expertMode, phase5);
					}
				}
				if (Dying)
				{
					teleportTimer = 0;
					DoDeathAnimation();
					return;
				}
				if (base.NPC.life == 1)
				{
					Dying = true;
					base.NPC.dontTakeDamage = true;
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					return;
				}
				float adjustedAlphaGateValue = 1660f;
				if (phase4 && postTeleportTimer <= 0)
				{
					if (laserWallPhase == 0)
					{
						if (phase6 && !spawnedGuardians3 && calamityGlobalNPC.newAI[3] < adjustedAlphaGateValue && base.NPC.ai[3] < 2f)
						{
							base.NPC.ai[3] = 2f;
							calamityGlobalNPC.newAI[3] = adjustedAlphaGateValue;
						}
						if (base.NPC.ai[3] == 2f || ((!spawnedGuardians3 & phase6) && base.NPC.ai[3] < 2f))
						{
							calamityGlobalNPC.newAI[3]++;
						}
						if (calamityGlobalNPC.newAI[3] > adjustedAlphaGateValue && teleportTimer > 0)
						{
							GetRiftLocation(activateRift: true);
							teleportTimer = 0;
						}
						float laserWallGateValue = 1800f;
						if (calamityGlobalNPC.newAI[3] >= laserWallGateValue)
						{
							calamityGlobalNPC.newAI[1] = 0f;
							calamityGlobalNPC.newAI[3] = 0f;
							laserWallPhase = 1;
						}
					}
					else if (laserWallPhase == 1)
					{
						idleCounter--;
						if (idleCounter <= 0)
						{
							laserWallPhase = 2;
							idleCounter = 300;
							base.NPC.ai[3] = 3f;
							calamityGlobalNPC.newAI[2] = 0f;
						}
					}
					else if (laserWallPhase == 2)
					{
						float totalTimeBeforeFullOpacity = 250f;
						float timeBeforeTeleportHappens = (death ? 150 : (CalamityWorld.revenge ? 150 : (Main.expertMode ? 160 : 180)));
						float opacityIncrement = 1f / (totalTimeBeforeFullOpacity - timeBeforeTeleportHappens);
						if (teleportTimer == 0)
						{
							base.NPC.Opacity += opacityIncrement;
						}
						if (base.NPC.Opacity >= 1f)
						{
							base.NPC.damage = 0;
							base.NPC.Opacity = 1f;
							laserWallPhase = 0;
							if (!spawnedGuardians3 & phase6)
							{
								calamityGlobalNPC.newAI[1] = 0f;
								calamityGlobalNPC.newAI[3] = 0f;
								if (!BossRushEvent.BossRushActive)
								{
									Color messageColor2 = Color.Cyan;
									CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DoGPhase3", messageColor2);
									Vector2 start = GetRiftLocation();
									DialogueDisplaySystem.StartDialogue("Mods.CalamityMod.DevourerOfGods.Phases", start, 3, 120, progressDialogue: false, new BossText());
								}
								spawnedGuardians3 = true;
							}
						}
					}
				}
				else if (postTeleportTimer > 0)
				{
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
					postTeleportTimer--;
					base.NPC.Opacity = 1f - (float)postTeleportTimer / 255f;
					if (base.NPC.ai[3] < 2f)
					{
						calamityGlobalNPC.newAI[2] = 0f;
					}
				}
				else
				{
					base.NPC.Opacity += 0.024f;
					if (base.NPC.Opacity > 1f)
					{
						base.NPC.Opacity = 1f;
					}
				}
				if (isInPassiveState)
				{
					ShootFireballs(player, distanceFromTarget, revenge);
				}
				else
				{
					calamityGlobalNPC.newAI[0] = 0f;
				}
				if (laserWallPhase == 1 && Main.netMode != 1)
				{
					if (death & phase6)
					{
						float spacing = 320f;
						float miniInterval = 12f;
						float megaInterval = 120f;
						float time = 0.35f;
						for (int i = 0; i < 3; i++)
						{
							if ((float)(int)(calamityGlobalNPC.newAI[1] - miniInterval * (float)i) % megaInterval == 0f)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center + Main.rand.NextVector2CircularEdge(600f, 600f), Vector2.Zero, ModContent.ProjectileType<DoGLaserWalls>(), LaserWallDamage, 0f, Main.myPlayer, time, spacing, 2 - i);
								if (i == 2)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center, Vector2.Zero, ModContent.ProjectileType<DoGLaserWallsBigBeam>(), LaserWallMiddleBeamDamage, 0f, Main.myPlayer, time, 0f, i);
								}
								else if (Main.zenithWorld)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center + Main.rand.NextVector2CircularEdge(600f, 600f), Vector2.Zero, ModContent.ProjectileType<DoGLaserWalls>(), LaserWallDamage, 0f, Main.myPlayer, time, 240f, 6f);
								}
							}
						}
						calamityGlobalNPC.newAI[1]++;
					}
					else
					{
						float divisor = (death ? 100f : 120f);
						if (phase6)
						{
							divisor -= 15f;
						}
						if (calamityGlobalNPC.newAI[1] % divisor == 0f)
						{
							float spacing2 = (death ? 144 : 160);
							if (divisor == 0f)
							{
								spacing2 += 64f;
							}
							if (phase6)
							{
								spacing2 += (float)(death ? 64 : 32);
							}
							int bType = Main.rand.Next(0, 6);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center + Main.rand.NextVector2CircularEdge(600f, 600f), Vector2.Zero, ModContent.ProjectileType<DoGLaserWalls>(), LaserWallDamage, 0f, Main.myPlayer, 0.5f, spacing2, bType);
							if (phase6 | death)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center, Vector2.Zero, ModContent.ProjectileType<DoGLaserWallsBigBeam>(), LaserWallMiddleBeamDamage, 0f, Main.myPlayer, 0.5f, 0f, bType);
							}
							if (Main.zenithWorld)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center + Main.rand.NextVector2CircularEdge(600f, 600f), Vector2.Zero, ModContent.ProjectileType<DoGLaserWalls>(), LaserWallDamage, 0f, Main.myPlayer, 0.5f, 120f, 6f);
							}
						}
						calamityGlobalNPC.newAI[1]++;
					}
				}
				if (laserWallPhase == 1 && !Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
				{
					Main.LocalPlayer.Calamity().infiniteFlight = true;
				}
				int tilePositionX = (int)(base.NPC.position.X / 16f) - 1;
				int tileWidthPosX = (int)((base.NPC.position.X + (float)base.NPC.width) / 16f) + 2;
				int tilePositionY = (int)(base.NPC.position.Y / 16f) - 1;
				int tileWidthPosY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f) + 2;
				if (tilePositionX < 0)
				{
					tilePositionX = 0;
				}
				if (tileWidthPosX > Main.maxTilesX)
				{
					tileWidthPosX = Main.maxTilesX;
				}
				if (tilePositionY < 0)
				{
					tilePositionY = 0;
				}
				if (tileWidthPosY > Main.maxTilesY)
				{
					tileWidthPosY = Main.maxTilesY;
				}
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.spriteDirection = -1;
				}
				else if (base.NPC.velocity.X > 0f)
				{
					base.NPC.spriteDirection = 1;
				}
				VelocityRotation = base.NPC.velocity.ToRotation();
				if (base.NPC.ai[3] == 0f)
				{
					if (!Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
					{
						Main.LocalPlayer.AddBuff(ModContent.BuffType<DoGExtremeGravity>(), 2);
					}
					if (postTeleportTimer > 0)
					{
						base.NPC.damage = base.NPC.defDamage;
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
						return;
					}
					calamityGlobalNPC.newAI[2]++;
					base.NPC.localAI[1] = 0f;
					if (increaseSpeedMore && laserWallPhase == 0 && calamityGlobalNPC.newAI[3] <= adjustedAlphaGateValue)
					{
						SpawnTeleportLocation(player);
					}
					float speedCopy = speed;
					float turnSpeedCopy = turnSpeed;
					Vector2 npcCenter = base.NPC.Center;
					float targetX = destination.X;
					float targetY = destination.Y;
					_ = destination.X / 16f;
					_ = destination.Y / 16f;
					speedCopy = homingSpeed;
					turnSpeedCopy = homingTurnSpeed;
					speedCopy += Vector2.Distance(destination, base.NPC.Center) * 0.005f;
					turnSpeedCopy += Vector2.Distance(destination, base.NPC.Center) * 0.00025f;
					float fasterSpeedMult = speedCopy * 1.3f;
					float slowerSpeedMult = speedCopy * 0.7f;
					float npcSpeed = ((Vector2)(ref base.NPC.velocity)).Length();
					if (npcSpeed > 0f)
					{
						if (npcSpeed > fasterSpeedMult)
						{
							((Vector2)(ref base.NPC.velocity)).Normalize();
							NPC nPC2 = base.NPC;
							nPC2.velocity *= fasterSpeedMult;
						}
						else if (npcSpeed < slowerSpeedMult)
						{
							((Vector2)(ref base.NPC.velocity)).Normalize();
							NPC nPC3 = base.NPC;
							nPC3.velocity *= slowerSpeedMult;
						}
					}
					targetX = (int)(targetX / 16f) * 16;
					targetY = (int)(targetY / 16f) * 16;
					npcCenter.X = (int)(npcCenter.X / 16f) * 16;
					npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
					targetX -= npcCenter.X;
					targetY -= npcCenter.Y;
					float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
					float absoluteTargetX = Math.Abs(targetX);
					float absoluteTargetY = Math.Abs(targetY);
					float timeToReachTarget = speedCopy / targetDistance;
					targetX *= timeToReachTarget;
					targetY *= timeToReachTarget;
					turnSpeedCopy *= base.NPC.Distance(destination) / (death ? 800f : 1000f);
					if ((base.NPC.velocity.X > 0f && targetX > 0f) || (base.NPC.velocity.X < 0f && targetX < 0f) || (base.NPC.velocity.Y > 0f && targetY > 0f) || (base.NPC.velocity.Y < 0f && targetY < 0f))
					{
						if (base.NPC.velocity.X < targetX)
						{
							base.NPC.velocity.X += turnSpeedCopy;
						}
						else if (base.NPC.velocity.X > targetX)
						{
							base.NPC.velocity.X -= turnSpeedCopy;
						}
						if (base.NPC.velocity.Y < targetY)
						{
							base.NPC.velocity.Y += turnSpeedCopy;
						}
						else if (base.NPC.velocity.Y > targetY)
						{
							base.NPC.velocity.Y -= turnSpeedCopy;
						}
						if ((double)Math.Abs(targetY) < (double)speedCopy * 0.2 && ((base.NPC.velocity.X > 0f && targetX < 0f) || (base.NPC.velocity.X < 0f && targetX > 0f)))
						{
							if (base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y += turnSpeedCopy * 2f;
							}
							else
							{
								base.NPC.velocity.Y -= turnSpeedCopy * 2f;
							}
						}
						if ((double)Math.Abs(targetX) < (double)speedCopy * 0.2 && ((base.NPC.velocity.Y > 0f && targetY < 0f) || (base.NPC.velocity.Y < 0f && targetY > 0f)))
						{
							if (base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X += turnSpeedCopy * 2f;
							}
							else
							{
								base.NPC.velocity.X -= turnSpeedCopy * 2f;
							}
						}
					}
					else if (absoluteTargetX > absoluteTargetY)
					{
						if (base.NPC.velocity.X < targetX)
						{
							base.NPC.velocity.X += turnSpeedCopy * 1.1f;
						}
						else if (base.NPC.velocity.X > targetX)
						{
							base.NPC.velocity.X -= turnSpeedCopy * 1.1f;
						}
						if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)speedCopy * 0.5)
						{
							if (base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y += turnSpeedCopy;
							}
							else
							{
								base.NPC.velocity.Y -= turnSpeedCopy;
							}
						}
					}
					else
					{
						if (base.NPC.velocity.Y < targetY)
						{
							base.NPC.velocity.Y += turnSpeedCopy * 1.1f;
						}
						else if (base.NPC.velocity.Y > targetY)
						{
							base.NPC.velocity.Y -= turnSpeedCopy * 1.1f;
						}
						if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)speedCopy * 0.5)
						{
							if (base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X += turnSpeedCopy;
							}
							else
							{
								base.NPC.velocity.X -= turnSpeedCopy;
							}
						}
					}
					if (calamityGlobalNPC.velocityPriorToPhaseSwap > 0f && ((Vector2)(ref base.NPC.velocity)).Length() > calamityGlobalNPC.velocityPriorToPhaseSwap)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC4 = base.NPC;
						nPC4.velocity *= calamityGlobalNPC.velocityPriorToPhaseSwap;
						calamityGlobalNPC.velocityPriorToPhaseSwap += 0.1f;
					}
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
					if (calamityGlobalNPC.newAI[2] > (float)phaseLimit)
					{
						calamityGlobalNPC.velocityPriorToPhaseSwap = ((Vector2)(ref base.NPC.velocity)).Length();
						base.NPC.ai[3] = 1f;
						calamityGlobalNPC.newAI[2] = 0f;
						base.NPC.TargetClosest();
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
				}
				else if (base.NPC.ai[3] == 1f)
				{
					if (!Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
					{
						Main.LocalPlayer.AddBuff(ModContent.BuffType<Warped>(), 2);
					}
					if (postTeleportTimer > 0)
					{
						base.NPC.damage = base.NPC.defDamage;
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
						return;
					}
					calamityGlobalNPC.newAI[2]++;
					if (increaseSpeedMore)
					{
						if (laserWallPhase == 0 && calamityGlobalNPC.newAI[3] <= adjustedAlphaGateValue)
						{
							SpawnTeleportLocation(player);
						}
						else
						{
							groundPhaseTurnSpeed *= 4f;
						}
					}
					else if (increaseSpeed)
					{
						groundPhaseTurnSpeed *= 2f;
					}
					if (!flies)
					{
						Vector2 positionCheck = default(Vector2);
						for (int r = tilePositionX; r < tileWidthPosX; r++)
						{
							for (int s = tilePositionY; s < tileWidthPosY; s++)
							{
								if (Main.tile[r, s] != null && ((Main.tile[r, s].HasUnactuatedTile && (Main.tileSolid[Main.tile[r, s].TileType] || (Main.tileSolidTop[Main.tile[r, s].TileType] && Main.tile[r, s].TileFrameY == 0))) || Main.tile[r, s].LiquidAmount > 64))
								{
									positionCheck.X = r * 16;
									positionCheck.Y = s * 16;
									if (base.NPC.position.X + (float)base.NPC.width > positionCheck.X && base.NPC.position.X < positionCheck.X + 16f && base.NPC.position.Y + (float)base.NPC.height > positionCheck.Y && base.NPC.position.Y < positionCheck.Y + 16f)
									{
										flies = true;
										break;
									}
								}
							}
						}
					}
					if (!flies)
					{
						base.NPC.localAI[1] = 1f;
						Rectangle rectangle12 = default(Rectangle);
						((Rectangle)(ref rectangle12))._002Ector((int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height);
						int directChargeRange = (death ? 1125 : 1200);
						if (expertMode)
						{
							directChargeRange -= (int)(150f * (1f - lifeRatio));
						}
						if (directChargeRange < 1050)
						{
							directChargeRange = 1050;
						}
						bool canDirectlyCharge = true;
						if (base.NPC.position.Y > player.position.Y)
						{
							Rectangle rectangle13 = default(Rectangle);
							for (int k = 0; k < 255; k++)
							{
								if (Main.player[k].active)
								{
									((Rectangle)(ref rectangle13))._002Ector((int)Main.player[k].position.X - 1000, (int)Main.player[k].position.Y - 1000, 2000, directChargeRange);
									if (((Rectangle)(ref rectangle12)).Intersects(rectangle13))
									{
										canDirectlyCharge = false;
										break;
									}
								}
							}
							if (canDirectlyCharge)
							{
								flies = true;
							}
						}
					}
					else
					{
						base.NPC.localAI[1] = 0f;
					}
					float turnSpeedCopy2 = groundPhaseTurnSpeed;
					Vector2 npcCenter2 = base.NPC.Center;
					float targetX2 = destination.X;
					float targetY2 = destination.Y;
					targetX2 = (int)(targetX2 / 16f) * 16;
					targetY2 = (int)(targetY2 / 16f) * 16;
					npcCenter2.X = (int)(npcCenter2.X / 16f) * 16;
					npcCenter2.Y = (int)(npcCenter2.Y / 16f) * 16;
					targetX2 -= npcCenter2.X;
					targetY2 -= npcCenter2.Y;
					if (!flies)
					{
						base.NPC.velocity.Y += groundPhaseTurnSpeed;
						if (base.NPC.velocity.Y > segmentVelocity)
						{
							base.NPC.velocity.Y = segmentVelocity;
						}
						bool slowXVelocity = Math.Abs(base.NPC.velocity.X) > turnSpeedCopy2;
						if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 2.2)
						{
							if (base.NPC.velocity.X < 0f)
							{
								base.NPC.velocity.X -= turnSpeedCopy2 * 1.1f;
							}
							else
							{
								base.NPC.velocity.X += turnSpeedCopy2 * 1.1f;
							}
						}
						else if (base.NPC.velocity.Y == segmentVelocity)
						{
							if (slowXVelocity)
							{
								if (base.NPC.velocity.X < targetX2)
								{
									base.NPC.velocity.X += turnSpeedCopy2;
								}
								else if (base.NPC.velocity.X > targetX2)
								{
									base.NPC.velocity.X -= turnSpeedCopy2;
								}
							}
							else
							{
								base.NPC.velocity.X = 0f;
							}
						}
						else if (base.NPC.velocity.Y > 4f)
						{
							if (slowXVelocity)
							{
								if (base.NPC.velocity.X < 0f)
								{
									base.NPC.velocity.X += turnSpeedCopy2 * 0.9f;
								}
								else
								{
									base.NPC.velocity.X -= turnSpeedCopy2 * 0.9f;
								}
							}
							else
							{
								base.NPC.velocity.X = 0f;
							}
						}
					}
					else
					{
						double maximumSpeed1 = (death ? 0.46 : 0.4);
						double maximumSpeed2 = (death ? 1.125 : 1.0);
						if (expertMode)
						{
							maximumSpeed1 += (double)(0.1f * (1f - lifeRatio));
							maximumSpeed2 += (double)(0.2f * (1f - lifeRatio));
						}
						float targetDistance2 = (float)Math.Sqrt(targetX2 * targetX2 + targetY2 * targetY2);
						float absoluteTargetX2 = Math.Abs(targetX2);
						float absoluteTargetY2 = Math.Abs(targetY2);
						float timeToReachTarget2 = segmentVelocity / targetDistance2;
						targetX2 *= timeToReachTarget2;
						targetY2 *= timeToReachTarget2;
						if (((base.NPC.velocity.X > 0f && targetX2 > 0f) || (base.NPC.velocity.X < 0f && targetX2 < 0f)) && ((base.NPC.velocity.Y > 0f && targetY2 > 0f) || (base.NPC.velocity.Y < 0f && targetY2 < 0f)))
						{
							if (base.NPC.velocity.X < targetX2)
							{
								base.NPC.velocity.X += groundPhaseTurnSpeed * 1.5f;
							}
							else if (base.NPC.velocity.X > targetX2)
							{
								base.NPC.velocity.X -= groundPhaseTurnSpeed * 1.5f;
							}
							if (base.NPC.velocity.Y < targetY2)
							{
								base.NPC.velocity.Y += groundPhaseTurnSpeed * 1.5f;
							}
							else if (base.NPC.velocity.Y > targetY2)
							{
								base.NPC.velocity.Y -= groundPhaseTurnSpeed * 1.5f;
							}
						}
						if ((base.NPC.velocity.X > 0f && targetX2 > 0f) || (base.NPC.velocity.X < 0f && targetX2 < 0f) || (base.NPC.velocity.Y > 0f && targetY2 > 0f) || (base.NPC.velocity.Y < 0f && targetY2 < 0f))
						{
							if (base.NPC.velocity.X < targetX2)
							{
								base.NPC.velocity.X += groundPhaseTurnSpeed;
							}
							else if (base.NPC.velocity.X > targetX2)
							{
								base.NPC.velocity.X -= groundPhaseTurnSpeed;
							}
							if (base.NPC.velocity.Y < targetY2)
							{
								base.NPC.velocity.Y += groundPhaseTurnSpeed;
							}
							else if (base.NPC.velocity.Y > targetY2)
							{
								base.NPC.velocity.Y -= groundPhaseTurnSpeed;
							}
							if ((double)Math.Abs(targetY2) < (double)segmentVelocity * maximumSpeed1 && ((base.NPC.velocity.X > 0f && targetX2 < 0f) || (base.NPC.velocity.X < 0f && targetX2 > 0f)))
							{
								if (base.NPC.velocity.Y > 0f)
								{
									base.NPC.velocity.Y += groundPhaseTurnSpeed * 2f;
								}
								else
								{
									base.NPC.velocity.Y -= groundPhaseTurnSpeed * 2f;
								}
							}
							if ((double)Math.Abs(targetX2) < (double)segmentVelocity * maximumSpeed1 && ((base.NPC.velocity.Y > 0f && targetY2 < 0f) || (base.NPC.velocity.Y < 0f && targetY2 > 0f)))
							{
								if (base.NPC.velocity.X > 0f)
								{
									base.NPC.velocity.X += groundPhaseTurnSpeed * 2f;
								}
								else
								{
									base.NPC.velocity.X -= groundPhaseTurnSpeed * 2f;
								}
							}
						}
						else if (absoluteTargetX2 > absoluteTargetY2)
						{
							if (base.NPC.velocity.X < targetX2)
							{
								base.NPC.velocity.X += groundPhaseTurnSpeed * 1.1f;
							}
							else if (base.NPC.velocity.X > targetX2)
							{
								base.NPC.velocity.X -= groundPhaseTurnSpeed * 1.1f;
							}
							if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * maximumSpeed2)
							{
								if (base.NPC.velocity.Y > 0f)
								{
									base.NPC.velocity.Y += groundPhaseTurnSpeed;
								}
								else
								{
									base.NPC.velocity.Y -= groundPhaseTurnSpeed;
								}
							}
						}
						else
						{
							if (base.NPC.velocity.Y < targetY2)
							{
								base.NPC.velocity.Y += groundPhaseTurnSpeed * 1.1f;
							}
							else if (base.NPC.velocity.Y > targetY2)
							{
								base.NPC.velocity.Y -= groundPhaseTurnSpeed * 1.1f;
							}
							if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * maximumSpeed2)
							{
								if (base.NPC.velocity.X > 0f)
								{
									base.NPC.velocity.X += groundPhaseTurnSpeed;
								}
								else
								{
									base.NPC.velocity.X -= groundPhaseTurnSpeed;
								}
							}
						}
					}
					if (calamityGlobalNPC.velocityPriorToPhaseSwap > 0f && ((Vector2)(ref base.NPC.velocity)).Length() > calamityGlobalNPC.velocityPriorToPhaseSwap)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC5 = base.NPC;
						nPC5.velocity *= calamityGlobalNPC.velocityPriorToPhaseSwap;
						calamityGlobalNPC.velocityPriorToPhaseSwap += 0.1f;
					}
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
					if (flies)
					{
						if (base.NPC.localAI[0] != 1f)
						{
							base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
						}
						base.NPC.localAI[0] = 1f;
					}
					else
					{
						if (base.NPC.localAI[0] != 0f)
						{
							base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
						}
						base.NPC.localAI[0] = 0f;
					}
					if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
					{
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
					if (calamityGlobalNPC.newAI[2] > (float)phaseLimit)
					{
						calamityGlobalNPC.velocityPriorToPhaseSwap = ((Vector2)(ref base.NPC.velocity)).Length();
						base.NPC.ai[3] = 0f;
						calamityGlobalNPC.newAI[2] = 0f;
						if (phase4)
						{
							calamityGlobalNPC.newAI[3] = 1540f;
							base.NPC.ai[3] = 2f;
						}
						base.NPC.TargetClosest();
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
				}
				else if (base.NPC.ai[3] == 2f)
				{
					calamityGlobalNPC.newAI[2]++;
					if (laserWallPhase != 2)
					{
						float dogRotation = player.DirectionTo(base.NPC.Center).ToRotation();
						int DOGDIR = 1;
						Vector2 goalpos = player.Center + Utils.RotatedBy(new Vector2(1000f, 0f), (double)(dogRotation + 0.05f * (float)DOGDIR), default(Vector2));
						float currentVelLength = ((Vector2)(ref base.NPC.velocity)).Length();
						_ = base.NPC.DirectionTo(goalpos) * currentVelLength;
						TurnTowards(goalpos, 0f, 6f * calamityGlobalNPC.newAI[2] / 120f);
						base.NPC.velocity = VelocityRotation.ToRotationVector2() * ((currentVelLength < 40f) ? (currentVelLength + 0.2f) : ((currentVelLength > 42f) ? (currentVelLength - 0.2f) : currentVelLength));
						base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
					}
					else
					{
						base.NPC.dontTakeDamage = true;
					}
				}
				else
				{
					if (calamityGlobalNPC.newAI[2] == 0f)
					{
						SpawnTeleportLocation(player);
					}
					NPC nPC6 = base.NPC;
					nPC6.velocity *= 1.02f;
					calamityGlobalNPC.newAI[2]++;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
				}
			}
		}
		else
		{
			if (phase3)
			{
				if (isInPassiveState)
				{
					ShootFireballs(player, distanceFromTarget, revenge);
				}
				else
				{
					calamityGlobalNPC.newAI[0] = 0f;
				}
				if (!spawnedGuardians)
				{
					if (revenge)
					{
						spawnDoGCountdown = 10;
					}
					if (!BossRushEvent.BossRushActive)
					{
						Color messageColor3 = Color.Cyan;
						CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DoGSubphase1", messageColor3);
						DialogueDisplaySystem.StartDialogue("Mods.CalamityMod.DevourerOfGods.Phases", (Entity)base.NPC, 1, 120, false, (DisplayEffect)new BossText(), -1f, -1, -1);
					}
					base.NPC.TargetClosest();
					spawnedGuardians = true;
				}
			}
			else if (phase2 && !spawnedGuardians2)
			{
				if (revenge)
				{
					spawnDoGCountdown = 10;
				}
				spawnedGuardians2 = true;
			}
			float laserBarrageGateValue = 1440f;
			float laserBarrageShootGateValue = 240f;
			float laserBarragePhaseGateValue = laserBarrageGateValue - laserBarrageShootGateValue;
			if (Main.netMode != 1 && !tail && base.NPC.ai[0] == 0f)
			{
				int Previous = base.NPC.whoAmI;
				if (Main.zenithWorld)
				{
					maxLength = 2;
					minLength = 1;
				}
				for (int segmentSpawn = 0; segmentSpawn < maxLength; segmentSpawn++)
				{
					int segment;
					if (segmentSpawn >= 0 && segmentSpawn < minLength)
					{
						segment = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<DevourerofGodsBody>(), base.NPC.whoAmI);
						Main.npc[segment].ModNPC<DevourerofGodsBody>().SegmentIndex = maxLength - segmentSpawn;
					}
					else
					{
						segment = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<DevourerofGodsTail>(), base.NPC.whoAmI);
					}
					Main.npc[segment].realLife = base.NPC.whoAmI;
					Main.npc[segment].ai[2] = base.NPC.whoAmI;
					Main.npc[segment].ai[1] = Previous;
					Main.npc[Previous].ai[0] = segment;
					NetMessage.SendData(23, -1, -1, null, segment);
					Previous = segment;
				}
				tail = true;
			}
			if (phase2)
			{
				if (base.NPC.ai[3] == 2f)
				{
					calamityGlobalNPC.newAI[1]++;
				}
				if (calamityGlobalNPC.newAI[1] >= laserBarragePhaseGateValue)
				{
					if (!Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
					{
						Main.LocalPlayer.Calamity().infiniteFlight = true;
					}
					if (calamityGlobalNPC.newAI[1] >= laserBarrageGateValue)
					{
						calamityGlobalNPC.newAI[1] = 0f;
						base.NPC.ai[3] = 3f;
						calamityGlobalNPC.newAI[2] = 0f;
						NPC nPC7 = base.NPC;
						nPC7.velocity += player.DirectionTo(base.NPC.Center) * 15f;
					}
					if (calamityGlobalNPC.newAI[1] % (float)(int)(laserBarrageShootGateValue * (death ? 0.33f : 0.5f)) == 0f && calamityGlobalNPC.newAI[1] > 0f && Main.netMode != 1)
					{
						int bType2 = Main.rand.Next(0, 6);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center + Main.rand.NextVector2CircularEdge(600f, 600f), Vector2.Zero, ModContent.ProjectileType<DoGLaserWalls>(), LaserWallDamage, 0f, Main.myPlayer, 0.45f, 170f, bType2);
						if (Main.zenithWorld)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center + Main.rand.NextVector2CircularEdge(600f, 600f), Vector2.Zero, ModContent.ProjectileType<DoGLaserWalls>(), LaserWallDamage, 0f, Main.myPlayer, 0.45f, 120f, 6f);
						}
					}
				}
			}
			if ((!phase2 || !(calamityGlobalNPC.newAI[1] >= laserBarragePhaseGateValue)) && !bigDaddyPhase2)
			{
				base.NPC.Opacity += 0.0083f;
				if (base.NPC.Opacity > 1f)
				{
					base.NPC.Opacity = 1f;
				}
			}
			int tilePositionX2 = (int)(base.NPC.position.X / 16f) - 1;
			int tileWidthPosX2 = (int)((base.NPC.position.X + (float)base.NPC.width) / 16f) + 2;
			int tilePositionY2 = (int)(base.NPC.position.Y / 16f) - 1;
			int tileWidthPosY2 = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f) + 2;
			if (tilePositionX2 < 0)
			{
				tilePositionX2 = 0;
			}
			if (tileWidthPosX2 > Main.maxTilesX)
			{
				tileWidthPosX2 = Main.maxTilesX;
			}
			if (tilePositionY2 < 0)
			{
				tilePositionY2 = 0;
			}
			if (tileWidthPosY2 > Main.maxTilesY)
			{
				tileWidthPosY2 = Main.maxTilesY;
			}
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.spriteDirection = -1;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				base.NPC.spriteDirection = 1;
			}
			VelocityRotation2 = base.NPC.velocity.ToRotation();
			if (base.NPC.ai[3] == 1f)
			{
				if (!Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
				{
					Main.LocalPlayer.AddBuff(ModContent.BuffType<DoGExtremeGravity>(), 2);
				}
				base.NPC.localAI[1] = 0f;
				calamityGlobalNPC.newAI[2]++;
				float speedCopy2 = speed;
				float turnSpeedCopy3 = turnSpeed;
				Vector2 npcCenter3 = base.NPC.Center;
				float targetX3 = player.position.X + (float)(player.width / 2);
				float targetY3 = player.position.Y + (float)(player.height / 2);
				speedCopy2 = homingSpeed;
				turnSpeedCopy3 = homingTurnSpeed;
				if (expertMode)
				{
					speedCopy2 += distanceFromTarget * 0.005f * (1f - lifeRatio);
					turnSpeedCopy3 += distanceFromTarget * 0.0001f * (1f - lifeRatio);
				}
				float fasterSpeedMult2 = speedCopy2 * 1.3f;
				float slowerSpeedMult2 = speedCopy2 * 0.7f;
				float npcSpeed2 = ((Vector2)(ref base.NPC.velocity)).Length();
				if (npcSpeed2 > 0f)
				{
					if (npcSpeed2 > fasterSpeedMult2)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC8 = base.NPC;
						nPC8.velocity *= fasterSpeedMult2;
					}
					else if (npcSpeed2 < slowerSpeedMult2)
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC9 = base.NPC;
						nPC9.velocity *= slowerSpeedMult2;
					}
				}
				targetX3 = (int)(targetX3 / 16f) * 16;
				targetY3 = (int)(targetY3 / 16f) * 16;
				npcCenter3.X = (int)(npcCenter3.X / 16f) * 16;
				npcCenter3.Y = (int)(npcCenter3.Y / 16f) * 16;
				targetX3 -= npcCenter3.X;
				targetY3 -= npcCenter3.Y;
				float targetDistance3 = (float)Math.Sqrt(targetX3 * targetX3 + targetY3 * targetY3);
				float absoluteTargetX3 = Math.Abs(targetX3);
				float absoluteTargetY3 = Math.Abs(targetY3);
				float timeToReachTarget3 = speedCopy2 / targetDistance3;
				targetX3 *= timeToReachTarget3;
				targetY3 *= timeToReachTarget3;
				turnSpeedCopy3 *= base.NPC.Distance(destination) / (float)(death ? 800 : 1000);
				if ((base.NPC.velocity.X > 0f && targetX3 > 0f) || (base.NPC.velocity.X < 0f && targetX3 < 0f) || (base.NPC.velocity.Y > 0f && targetY3 > 0f) || (base.NPC.velocity.Y < 0f && targetY3 < 0f))
				{
					if (base.NPC.velocity.X < targetX3)
					{
						base.NPC.velocity.X += turnSpeedCopy3;
					}
					else if (base.NPC.velocity.X > targetX3)
					{
						base.NPC.velocity.X -= turnSpeedCopy3;
					}
					if (base.NPC.velocity.Y < targetY3)
					{
						base.NPC.velocity.Y += turnSpeedCopy3;
					}
					else if (base.NPC.velocity.Y > targetY3)
					{
						base.NPC.velocity.Y -= turnSpeedCopy3;
					}
					if ((double)Math.Abs(targetY3) < (double)speedCopy2 * 0.2 && ((base.NPC.velocity.X > 0f && targetX3 < 0f) || (base.NPC.velocity.X < 0f && targetX3 > 0f)))
					{
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y += turnSpeedCopy3 * 2f;
						}
						else
						{
							base.NPC.velocity.Y -= turnSpeedCopy3 * 2f;
						}
					}
					if ((double)Math.Abs(targetX3) < (double)speedCopy2 * 0.2 && ((base.NPC.velocity.Y > 0f && targetY3 < 0f) || (base.NPC.velocity.Y < 0f && targetY3 > 0f)))
					{
						if (base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X += turnSpeedCopy3 * 2f;
						}
						else
						{
							base.NPC.velocity.X -= turnSpeedCopy3 * 2f;
						}
					}
				}
				else if (absoluteTargetX3 > absoluteTargetY3)
				{
					if (base.NPC.velocity.X < targetX3)
					{
						base.NPC.velocity.X += turnSpeedCopy3 * 1.1f;
					}
					else if (base.NPC.velocity.X > targetX3)
					{
						base.NPC.velocity.X -= turnSpeedCopy3 * 1.1f;
					}
					if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)speedCopy2 * 0.5)
					{
						if (base.NPC.velocity.Y > 0f)
						{
							base.NPC.velocity.Y += turnSpeedCopy3;
						}
						else
						{
							base.NPC.velocity.Y -= turnSpeedCopy3;
						}
					}
				}
				else
				{
					if (base.NPC.velocity.Y < targetY3)
					{
						base.NPC.velocity.Y += turnSpeedCopy3 * 1.1f;
					}
					else if (base.NPC.velocity.Y > targetY3)
					{
						base.NPC.velocity.Y -= turnSpeedCopy3 * 1.1f;
					}
					if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)speedCopy2 * 0.5)
					{
						if (base.NPC.velocity.X > 0f)
						{
							base.NPC.velocity.X += turnSpeedCopy3;
						}
						else
						{
							base.NPC.velocity.X -= turnSpeedCopy3;
						}
					}
				}
				if (calamityGlobalNPC.velocityPriorToPhaseSwap > 0f && ((Vector2)(ref base.NPC.velocity)).Length() > calamityGlobalNPC.velocityPriorToPhaseSwap)
				{
					((Vector2)(ref base.NPC.velocity)).Normalize();
					NPC nPC10 = base.NPC;
					nPC10.velocity *= calamityGlobalNPC.velocityPriorToPhaseSwap;
					calamityGlobalNPC.velocityPriorToPhaseSwap += 0.1f;
				}
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
				if (calamityGlobalNPC.newAI[2] > (float)phaseLimit)
				{
					calamityGlobalNPC.velocityPriorToPhaseSwap = ((Vector2)(ref base.NPC.velocity)).Length();
					base.NPC.ai[3] = 0f;
					calamityGlobalNPC.newAI[2] = 0f;
					if (phase2)
					{
						base.NPC.ai[3] = 2f;
						calamityGlobalNPC.newAI[1] = laserBarragePhaseGateValue - 120f;
					}
					base.NPC.TargetClosest();
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
			}
			else if (base.NPC.ai[3] == 0f)
			{
				if (!Main.dedServ && !Main.LocalPlayer.dead && Main.LocalPlayer.active && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 5600f)
				{
					Main.LocalPlayer.AddBuff(ModContent.BuffType<Warped>(), 2);
				}
				calamityGlobalNPC.newAI[2]++;
				if (increaseSpeedMore)
				{
					groundPhaseTurnSpeed *= 4f;
				}
				else if (increaseSpeed)
				{
					groundPhaseTurnSpeed *= 2f;
				}
				if (!flies)
				{
					Vector2 positionCheck2 = default(Vector2);
					for (int j = tilePositionX2; j < tileWidthPosX2; j++)
					{
						for (int l = tilePositionY2; l < tileWidthPosY2; l++)
						{
							if (Main.tile[j, l] != null && ((Main.tile[j, l].HasUnactuatedTile && (Main.tileSolid[Main.tile[j, l].TileType] || (Main.tileSolidTop[Main.tile[j, l].TileType] && Main.tile[j, l].TileFrameY == 0))) || Main.tile[j, l].LiquidAmount > 64))
							{
								positionCheck2.X = j * 16;
								positionCheck2.Y = l * 16;
								if (base.NPC.position.X + (float)base.NPC.width > positionCheck2.X && base.NPC.position.X < positionCheck2.X + 16f && base.NPC.position.Y + (float)base.NPC.height > positionCheck2.Y && base.NPC.position.Y < positionCheck2.Y + 16f)
								{
									flies = true;
									break;
								}
							}
						}
					}
				}
				if (!flies)
				{
					base.NPC.localAI[1] = 1f;
					Rectangle rectangle14 = default(Rectangle);
					((Rectangle)(ref rectangle14))._002Ector((int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height);
					int directChargeRange2 = (death ? 1125 : 1200);
					if (expertMode)
					{
						directChargeRange2 -= (int)(150f * (1f - lifeRatio));
					}
					if (directChargeRange2 < 1050)
					{
						directChargeRange2 = 1050;
					}
					bool canDirectlyCharge2 = true;
					if (base.NPC.position.Y > player.position.Y)
					{
						Rectangle rectangle15 = default(Rectangle);
						for (int m = 0; m < 255; m++)
						{
							if (Main.player[m].active)
							{
								((Rectangle)(ref rectangle15))._002Ector((int)Main.player[m].position.X - 1000, (int)Main.player[m].position.Y - 1000, 2000, directChargeRange2);
								if (((Rectangle)(ref rectangle14)).Intersects(rectangle15))
								{
									canDirectlyCharge2 = false;
									break;
								}
							}
						}
						if (canDirectlyCharge2)
						{
							flies = true;
						}
					}
				}
				else
				{
					base.NPC.localAI[1] = 0f;
				}
				float turnSpeedCopy4 = groundPhaseTurnSpeed;
				Vector2 npcCenter4 = base.NPC.Center;
				float targetX4 = destination.X;
				float targetY4 = destination.Y;
				targetX4 = (int)(targetX4 / 16f) * 16;
				targetY4 = (int)(targetY4 / 16f) * 16;
				npcCenter4.X = (int)(npcCenter4.X / 16f) * 16;
				npcCenter4.Y = (int)(npcCenter4.Y / 16f) * 16;
				targetX4 -= npcCenter4.X;
				targetY4 -= npcCenter4.Y;
				if (!flies)
				{
					base.NPC.velocity.Y += groundPhaseTurnSpeed;
					if (base.NPC.velocity.Y > segmentVelocity)
					{
						base.NPC.velocity.Y = segmentVelocity;
					}
					bool slowXVelocity2 = Math.Abs(base.NPC.velocity.X) > turnSpeedCopy4;
					if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * 2.2)
					{
						if (base.NPC.velocity.X < 0f)
						{
							base.NPC.velocity.X -= turnSpeedCopy4 * 1.1f;
						}
						else
						{
							base.NPC.velocity.X += turnSpeedCopy4 * 1.1f;
						}
					}
					else if (base.NPC.velocity.Y == segmentVelocity)
					{
						if (slowXVelocity2)
						{
							if (base.NPC.velocity.X < targetX4)
							{
								base.NPC.velocity.X += turnSpeedCopy4;
							}
							else if (base.NPC.velocity.X > targetX4)
							{
								base.NPC.velocity.X -= turnSpeedCopy4;
							}
						}
						else
						{
							base.NPC.velocity.X = 0f;
						}
					}
					else if (base.NPC.velocity.Y > 4f)
					{
						if (slowXVelocity2)
						{
							if (base.NPC.velocity.X < 0f)
							{
								base.NPC.velocity.X += turnSpeedCopy4 * 0.9f;
							}
							else
							{
								base.NPC.velocity.X -= turnSpeedCopy4 * 0.9f;
							}
						}
						else
						{
							base.NPC.velocity.X = 0f;
						}
					}
				}
				else
				{
					double maximumSpeed3 = (death ? 0.46 : 0.4);
					double maximumSpeed4 = (death ? 1.125 : 1.0);
					if (expertMode)
					{
						maximumSpeed3 += (double)(0.1f * (1f - lifeRatio));
						maximumSpeed4 += (double)(0.2f * (1f - lifeRatio));
					}
					float targetDistance4 = (float)Math.Sqrt(targetX4 * targetX4 + targetY4 * targetY4);
					float absoluteTargetX4 = Math.Abs(targetX4);
					float absoluteTargetY4 = Math.Abs(targetY4);
					float timeToReachTarget4 = segmentVelocity / targetDistance4;
					targetX4 *= timeToReachTarget4;
					targetY4 *= timeToReachTarget4;
					if (((base.NPC.velocity.X > 0f && targetX4 > 0f) || (base.NPC.velocity.X < 0f && targetX4 < 0f)) && ((base.NPC.velocity.Y > 0f && targetY4 > 0f) || (base.NPC.velocity.Y < 0f && targetY4 < 0f)))
					{
						if (base.NPC.velocity.X < targetX4)
						{
							base.NPC.velocity.X += groundPhaseTurnSpeed * 1.5f;
						}
						else if (base.NPC.velocity.X > targetX4)
						{
							base.NPC.velocity.X -= groundPhaseTurnSpeed * 1.5f;
						}
						if (base.NPC.velocity.Y < targetY4)
						{
							base.NPC.velocity.Y += groundPhaseTurnSpeed * 1.5f;
						}
						else if (base.NPC.velocity.Y > targetY4)
						{
							base.NPC.velocity.Y -= groundPhaseTurnSpeed * 1.5f;
						}
					}
					if ((base.NPC.velocity.X > 0f && targetX4 > 0f) || (base.NPC.velocity.X < 0f && targetX4 < 0f) || (base.NPC.velocity.Y > 0f && targetY4 > 0f) || (base.NPC.velocity.Y < 0f && targetY4 < 0f))
					{
						if (base.NPC.velocity.X < targetX4)
						{
							base.NPC.velocity.X += groundPhaseTurnSpeed;
						}
						else if (base.NPC.velocity.X > targetX4)
						{
							base.NPC.velocity.X -= groundPhaseTurnSpeed;
						}
						if (base.NPC.velocity.Y < targetY4)
						{
							base.NPC.velocity.Y += groundPhaseTurnSpeed;
						}
						else if (base.NPC.velocity.Y > targetY4)
						{
							base.NPC.velocity.Y -= groundPhaseTurnSpeed;
						}
						if ((double)Math.Abs(targetY4) < (double)segmentVelocity * maximumSpeed3 && ((base.NPC.velocity.X > 0f && targetX4 < 0f) || (base.NPC.velocity.X < 0f && targetX4 > 0f)))
						{
							if (base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y += groundPhaseTurnSpeed * 2f;
							}
							else
							{
								base.NPC.velocity.Y -= groundPhaseTurnSpeed * 2f;
							}
						}
						if ((double)Math.Abs(targetX4) < (double)segmentVelocity * maximumSpeed3 && ((base.NPC.velocity.Y > 0f && targetY4 < 0f) || (base.NPC.velocity.Y < 0f && targetY4 > 0f)))
						{
							if (base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X += groundPhaseTurnSpeed * 2f;
							}
							else
							{
								base.NPC.velocity.X -= groundPhaseTurnSpeed * 2f;
							}
						}
					}
					else if (absoluteTargetX4 > absoluteTargetY4)
					{
						if (base.NPC.velocity.X < targetX4)
						{
							base.NPC.velocity.X += groundPhaseTurnSpeed * 1.1f;
						}
						else if (base.NPC.velocity.X > targetX4)
						{
							base.NPC.velocity.X -= groundPhaseTurnSpeed * 1.1f;
						}
						if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * maximumSpeed4)
						{
							if (base.NPC.velocity.Y > 0f)
							{
								base.NPC.velocity.Y += groundPhaseTurnSpeed;
							}
							else
							{
								base.NPC.velocity.Y -= groundPhaseTurnSpeed;
							}
						}
					}
					else
					{
						if (base.NPC.velocity.Y < targetY4)
						{
							base.NPC.velocity.Y += groundPhaseTurnSpeed * 1.1f;
						}
						else if (base.NPC.velocity.Y > targetY4)
						{
							base.NPC.velocity.Y -= groundPhaseTurnSpeed * 1.1f;
						}
						if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)segmentVelocity * maximumSpeed4)
						{
							if (base.NPC.velocity.X > 0f)
							{
								base.NPC.velocity.X += groundPhaseTurnSpeed;
							}
							else
							{
								base.NPC.velocity.X -= groundPhaseTurnSpeed;
							}
						}
					}
				}
				if (calamityGlobalNPC.velocityPriorToPhaseSwap > 0f && ((Vector2)(ref base.NPC.velocity)).Length() > calamityGlobalNPC.velocityPriorToPhaseSwap)
				{
					((Vector2)(ref base.NPC.velocity)).Normalize();
					NPC nPC11 = base.NPC;
					nPC11.velocity *= calamityGlobalNPC.velocityPriorToPhaseSwap;
					calamityGlobalNPC.velocityPriorToPhaseSwap += 0.1f;
				}
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
				if (flies)
				{
					if (base.NPC.localAI[0] != 1f)
					{
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
					base.NPC.localAI[0] = 1f;
				}
				else
				{
					if (base.NPC.localAI[0] != 0f)
					{
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
					base.NPC.localAI[0] = 0f;
				}
				if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
				{
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
				if (calamityGlobalNPC.newAI[2] > (float)phaseLimit)
				{
					calamityGlobalNPC.velocityPriorToPhaseSwap = ((Vector2)(ref base.NPC.velocity)).Length();
					base.NPC.ai[3] = 1f;
					calamityGlobalNPC.newAI[2] = 0f;
					base.NPC.TargetClosest();
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[3] == 2f)
			{
				calamityGlobalNPC.newAI[2]++;
				if (laserWallPhase != 2)
				{
					float dogRotation2 = player.DirectionTo(base.NPC.Center).ToRotation();
					int DOGDIR2 = 1;
					Vector2 goalpos2 = player.Center + Utils.RotatedBy(new Vector2(1000f, 0f), (double)(dogRotation2 + 0.05f * (float)DOGDIR2), default(Vector2));
					float currentVelLength2 = ((Vector2)(ref base.NPC.velocity)).Length();
					_ = base.NPC.DirectionTo(goalpos2) * currentVelLength2;
					TurnTowards2(goalpos2, 0f, 6f * calamityGlobalNPC.newAI[2] / 120f);
					base.NPC.velocity = VelocityRotation2.ToRotationVector2() * ((currentVelLength2 < 40f) ? (currentVelLength2 + 0.2f) : currentVelLength2);
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
				}
			}
			else
			{
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
				calamityGlobalNPC.newAI[2]++;
				if (calamityGlobalNPC.newAI[2] >= 45f)
				{
					base.NPC.ai[3] = 0f;
					calamityGlobalNPC.newAI[2] = 0f;
				}
			}
		}
		if (base.NPC.Distance(destination) > 2400f)
		{
			NPC nPC12 = base.NPC;
			nPC12.velocity += (destination - base.NPC.Center).SafeNormalize(Vector2.UnitY) * turnSpeed;
		}
		float targetRotationReset = (((Phase2Started | phase3) && isInPassiveState) ? MathHelper.ToRadians(10f) : 0f);
		float targetRotationOpenJaw = MathHelper.ToRadians(Phase2Started ? 32f : 22f);
		float targetRotationChompDown = MathHelper.ToRadians(Phase2Started ? (-25f) : (-15f));
		bool rotatedTowardsPlayer = Vector2.Dot(base.NPC.DirectionTo(destination), base.NPC.velocity.SafeNormalize(Vector2.Zero)) > 0.8f;
		JawRotation = MathHelper.Lerp(JawRotation, targetRotationReset, 0.08f);
		if (distanceFromTarget < 110f)
		{
			JawRotation = MathHelper.Lerp(JawRotation, targetRotationChompDown, JawChompDownProgress);
			JawChompDownProgress = MathHelper.Clamp(JawChompDownProgress + 0.28f, 0f, 1f);
			if (JawChompDownProgress >= 0.94f && ShouldSpawnChompVFX)
			{
				Vector2 jawChompPosition = base.NPC.Center - Vector2.UnitY.RotatedBy(base.NPC.rotation) * 36f + base.NPC.velocity * 1.45f;
				Color jawParticleColor = (isInPassiveState ? Color.Cyan : Color.Purple);
				for (int n = 0; n < 20; n++)
				{
					Vector2 sparkVelocity = base.NPC.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(20f)) * Main.rand.NextFloat(26f, 32f);
					int sparkLifetime = Main.rand.Next(20, 30);
					float sparkScale = Main.rand.NextFloat(1.4f, 1.8f);
					Color sparkColor = Color.Lerp(jawParticleColor, Color.White, Main.rand.NextFloat(0f, 0.3f));
					GeneralParticleHandler.SpawnParticle(new SparkParticle(jawChompPosition, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
				}
				float shakeStrength = (Phase2Started ? 14f : 8f);
				CalamityUtils.AddScreenshakeAt(jawChompPosition, shakeStrength, 100f);
				SoundEngine.PlaySound(HitSound with
				{
					Pitch = -0.45f
				}, jawChompPosition);
				ShouldSpawnChompVFX = false;
				base.NPC.netUpdate = true;
			}
		}
		else if ((distanceFromTarget < 480f) & rotatedTowardsPlayer)
		{
			JawRotation = MathHelper.Lerp(JawRotation, targetRotationOpenJaw, 0.28f);
			if (!ShouldSpawnChompVFX)
			{
				ShouldSpawnChompVFX = true;
			}
		}
		GodSlayerDashJawTimer--;
		if (GodSlayerDashJawTimer <= 0f)
		{
			GodSlayerDashJawFadeProgress = MathHelper.Lerp(GodSlayerDashJawFadeProgress, 0f, 0.15f);
			if (GodSlayerDashJawTimer < 0f)
			{
				GodSlayerDashJawTimer = 0f;
			}
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[0]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[0]].life;
		}
		void TurnTowards(Vector2 goal, float offset = 0f, float maxSpeed = 1f)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			float goal2 = (goal - base.NPC.Center).ToRotation() + offset;
			maxSpeed *= (float)Math.PI / 180f;
			float dif = MathF.Atan2(MathF.Sin(goal2 - VelocityRotation), MathF.Cos(goal2 - VelocityRotation));
			if (dif < 0f)
			{
				if (0f - dif > maxSpeed)
				{
					VelocityRotation -= maxSpeed;
				}
				else
				{
					VelocityRotation += dif;
				}
			}
			else if (dif > maxSpeed)
			{
				VelocityRotation += maxSpeed;
			}
			else
			{
				VelocityRotation += dif;
			}
		}
		void TurnTowards2(Vector2 goal, float offset = 0f, float maxSpeed = 1f)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			float goal2 = (goal - base.NPC.Center).ToRotation() + offset;
			maxSpeed *= (float)Math.PI / 180f;
			float dif = MathF.Atan2(MathF.Sin(goal2 - VelocityRotation2), MathF.Cos(goal2 - VelocityRotation2));
			if (dif < 0f)
			{
				if (0f - dif > maxSpeed)
				{
					VelocityRotation2 -= maxSpeed;
				}
				else
				{
					VelocityRotation2 += dif;
				}
			}
			else if (dif > maxSpeed)
			{
				VelocityRotation2 += maxSpeed;
			}
			else
			{
				VelocityRotation2 += dif;
			}
		}
	}

	private void ShootFireballs(Player player, float distanceFromTarget, bool revenge)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		Vector2 mouthPosition = base.NPC.Center - Vector2.UnitY.RotatedBy(base.NPC.rotation) * (Phase2Started ? 28f : (-16f));
		calamityGlobalNPC.newAI[0]++;
		if (base.NPC.Opacity >= 1f && distanceFromTarget > (revenge ? 320f : 480f) && Vector2.Dot(base.NPC.DirectionTo(AdjustedPlayerCenter()), base.NPC.velocity.SafeNormalize(Vector2.Zero)) > 0.8f && calamityGlobalNPC.newAI[0] > 75f)
		{
			for (int i = 0; i < 18; i++)
			{
				Vector2 flameVelocity = base.NPC.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(32f, 44f);
				int flameLifetime = Main.rand.Next(45, 60);
				float flameScale = Main.rand.NextFloat(0.6f, 0.9f);
				float flameOpacity = Main.rand.NextFloat(0.7f, 0.9f);
				Color flameColor = Color.Lerp(Color.Cyan, Color.White, Main.rand.NextFloat(0f, 0.4f));
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(mouthPosition, flameVelocity, flameColor, flameLifetime, flameScale, flameOpacity, 0.01f, glowing: true));
			}
			for (int j = 0; j < 10; j++)
			{
				Vector2 cinderVelocity = base.NPC.velocity.SafeNormalize(Vector2.Zero).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(36f, 48f);
				float cinderScale = Main.rand.NextFloat(0.3f, 0.5f);
				Color cinderColor = Color.Lerp(Color.Cyan, Color.White, Main.rand.NextFloat(0.2f, 0.6f));
				int cinderLifetime = Main.rand.Next(45, 60);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(mouthPosition, cinderVelocity, cinderScale, cinderColor, cinderLifetime));
			}
			JawRotation = MathHelper.ToRadians(22f);
			float fireballSpeed = 8f;
			Vector2 fireballVelocity = Vector2.Normalize(AdjustedPlayerCenter() - base.NPC.Center) * (fireballSpeed + ((Vector2)(ref base.NPC.velocity)).Length() * 0.5f);
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), mouthPosition, fireballVelocity, ModContent.ProjectileType<DoGFire>(), FireballDamage, 0f, Main.myPlayer, 2f);
			}
			calamityGlobalNPC.newAI[0] = 0f;
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
	}

	private void SpawnTeleportLocation(Player player, bool phase2Transition = false)
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		if (teleportTimer > 0 || player.dead || !player.active)
		{
			return;
		}
		if (base.NPC.ai[3] < 2f)
		{
			base.NPC.ai[3] = 3f;
		}
		int baseTeleportTime = ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 150 : (CalamityWorld.revenge ? 150 : (Main.expertMode ? 160 : 180)));
		if ((CalamityWorld.death || BossRushEvent.BossRushActive) && (float)base.NPC.life / (float)base.NPC.lifeMax < 0.25f && base.NPC.ai[3] < 8f && base.NPC.ai[3] > 3f)
		{
			baseTeleportTime -= 45;
		}
		if (!phase2Transition)
		{
			teleportTimer = baseTeleportTime;
		}
		SoundEngine.PlaySound(RiftOpenSound with
		{
			Volume = 1.5f
		}, player.Center);
		if (Main.netMode == 1)
		{
			return;
		}
		int randomRange = (Main.zenithWorld ? 960 : 48);
		float distance = 500f;
		if ((CalamityWorld.death || BossRushEvent.BossRushActive) && (float)base.NPC.life / (float)base.NPC.lifeMax < 0.25f && base.NPC.ai[3] < 8f)
		{
			distance += 450f;
		}
		Vector2 targetVector = player.Center + player.velocity.SafeNormalize(Vector2.UnitX) * distance + new Vector2((float)Main.rand.Next(-randomRange, randomRange + 1), (float)Main.rand.Next(-randomRange, randomRange + 1));
		int rift = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), targetVector, Vector2.Zero, ModContent.ProjectileType<DoGTeleportRift>(), 0, 0f, Main.myPlayer, base.NPC.whoAmI);
		if (Main.projectile.IndexInRange(rift))
		{
			Main.projectile[rift].ModProjectile<DoGTeleportRift>().RiftLifetime = baseTeleportTime;
		}
		if (!Main.zenithWorld)
		{
			return;
		}
		randomRange = 2000;
		for (int k = 0; k < 35; k++)
		{
			targetVector = player.Center + player.velocity.SafeNormalize(Vector2.UnitX) * distance + new Vector2((float)Main.rand.Next(-randomRange, randomRange + 1), (float)Main.rand.Next(-randomRange, randomRange + 1));
			int faker = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), targetVector, Vector2.Zero, ModContent.ProjectileType<DoGTeleportRift>(), 0, 0f, Main.myPlayer, base.NPC.whoAmI);
			if (Main.projectile.IndexInRange(faker))
			{
				Main.projectile[faker].ModProjectile<DoGTeleportRift>().FakeRift = true;
				Main.projectile[faker].ModProjectile<DoGTeleportRift>().RiftLifetime = baseTeleportTime;
			}
		}
	}

	private void Teleport(Player player, bool death, bool revenge, bool expertMode, bool phase5)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newPosition = GetRiftLocation(activateRift: true);
		if ((!AwaitingPhase2Teleport && (player.dead || !player.active)) || newPosition == default(Vector2))
		{
			return;
		}
		bool phase6 = (float)base.NPC.life / (float)base.NPC.lifeMax < 0.25f;
		if (Main.netMode != 1 && (!(death & phase6) || !(base.NPC.ai[3] < 7f)))
		{
			float finalVelocity = (death ? 12f : 10f);
			int totalSpreads = (revenge ? 6 : 3);
			float mult = (revenge ? 1.5f : 3f);
			for (int i = 0; i < totalSpreads; i++)
			{
				if (death || i % 3 != 2)
				{
					int totalProjectiles = 12;
					float radians = (float)Math.PI * 2f / (float)totalProjectiles;
					float newVelocity = finalVelocity - (float)i * mult;
					float velocityMult = 1f + (finalVelocity - newVelocity) / (newVelocity * 2f) / 100f;
					double angleA = (double)radians * 0.5;
					double angleB = (double)MathHelper.ToRadians(90f) - angleA;
					float velocityX = (float)((double)newVelocity * Math.Sin(angleA) / Math.Sin(angleB));
					Vector2 spinningPoint = ((i < 3) ? new Vector2(0f, 0f - newVelocity) : new Vector2(0f - velocityX, 0f - newVelocity));
					float finalVelocityReduction = (float)Math.Pow(1.25, i) - 1f;
					for (int k = 0; k < totalProjectiles; k++)
					{
						Vector2 vector255 = spinningPoint.RotatedBy(radians * (float)k);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), newPosition, vector255, ModContent.ProjectileType<DoGFire>(), FireballDamage, 0f, Main.myPlayer, velocityMult, finalVelocity - finalVelocityReduction);
					}
				}
			}
		}
		base.NPC.TargetClosest();
		base.NPC.position = newPosition;
		float chargeVelocity = (death ? 26f : (revenge ? 24f : (expertMode ? 22f : 20f)));
		chargeVelocity *= ((!death) ? 2.25f : ((!phase6 || !(base.NPC.ai[3] < 7f)) ? 2.5f : 3f));
		float maxChargeDistance = 1800f;
		postTeleportTimer = (int)Math.Round(maxChargeDistance / chargeVelocity);
		int phase6dashcount = (death ? 5 : 3);
		if (phase6 && base.NPC.ai[3] < (float)(2 + phase6dashcount))
		{
			if (base.NPC.ai[3] < 3f)
			{
				base.NPC.ai[3] = 3f;
			}
			base.NPC.ai[3]++;
			if (Main.getGoodWorld && (float)base.NPC.life / (float)base.NPC.lifeMax < 0.1f)
			{
				base.NPC.ai[3] = 4f;
				base.NPC.SimpleStrikeNPC(10000, 1);
				if (Main.netMode != 1)
				{
					int bType = Main.rand.Next(0, 2);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center + Main.rand.NextVector2CircularEdge(600f, 600f), Vector2.Zero, ModContent.ProjectileType<DoGLaserWalls>(), LaserWallDamage, 0f, Main.myPlayer, 0.45f, 300f, bType);
				}
			}
		}
		else
		{
			base.NPC.ai[3] = 0f;
		}
		base.NPC.Calamity().newAI[2] = 0f;
		AwaitingPhase2Teleport = false;
		base.NPC.Opacity = 1f - (float)postTeleportTimer / 255f;
		base.NPC.velocity = Vector2.Normalize(AdjustedPlayerCenter(10f) - base.NPC.Center) * chargeVelocity;
		base.NPC.damage = base.NPC.defDamage;
		base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.type == ModContent.NPCType<DevourerofGodsBody>() || n.type == ModContent.NPCType<DevourerofGodsTail>())
			{
				n.position = newPosition;
				if (n.type == ModContent.NPCType<DevourerofGodsTail>())
				{
					((DevourerofGodsTail)n.ModNPC).setInvulTime(720);
				}
				n.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
		}
		GodSlayerDashJawFadeProgress = 1f;
		GodSlayerDashJawTimer = 60f;
		SoundEngine.PlaySound(AttackSound with
		{
			Pitch = AttackSound.Pitch + extrapitch
		}, player.Center);
		if (Main.getGoodWorld)
		{
			base.NPC.Calamity().velocityPriorToPhaseSwap = 20f;
		}
	}

	public void DoDeathAnimation()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		if ((float)DeathAnimationTimer == 1f)
		{
			SoundEngine.PlaySound(in DeathExplosionSound, base.NPC.Center);
		}
		base.NPC.Calamity().CanHaveBossHealthBar = false;
		base.NPC.Opacity = MathHelper.Clamp(base.NPC.Opacity + 0.1f, 0f, 1f);
		base.NPC.dontTakeDamage = true;
		base.NPC.damage = 0;
		float idealSpeed = MathHelper.Lerp(8.4f, 4f, Utils.GetLerpValue(15f, 210f, DeathAnimationTimer, clamped: true));
		if (((Vector2)(ref base.NPC.velocity)).Length() != idealSpeed)
		{
			base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.UnitY) * MathHelper.Lerp(((Vector2)(ref base.NPC.velocity)).Length(), idealSpeed, 0.08f);
		}
		if (base.NPC.Center.X < 300f || base.NPC.Center.X > (float)Main.maxTilesX * 16f - 300f)
		{
			base.NPC.velocity.X *= -1f;
		}
		if (base.NPC.Center.Y < 300f || base.NPC.Center.Y > (float)Main.maxTilesY * 16f - 300f)
		{
			base.NPC.velocity.Y *= -1f;
		}
		if ((float)DeathAnimationTimer >= 120f && (float)DeathAnimationTimer < 370f && (float)DeathAnimationTimer % 3f == 0f)
		{
			int segmentToDestroy = (int)(Utils.GetLerpValue(120f, 370f, DeathAnimationTimer, clamped: true) * 60f);
			destroySegment(segmentToDestroy, ref DestroyedSegmentCount);
		}
		if ((float)DeathAnimationTimer == 452f)
		{
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<DoGDeathBoom>(), 0, 0f);
			}
			if (!Main.dedServ)
			{
				SoundEngine.PlaySound(in DeathAnimationSound, base.NPC.Center);
				for (int i = 0; i < 3; i++)
				{
					SoundEngine.PlaySound(in DeathExplosionSound, base.NPC.Center);
				}
			}
		}
		if ((float)DeathAnimationTimer >= 410f && (float)DeathAnimationTimer < 470f && (float)DeathAnimationTimer % 2f == 0f)
		{
			int segmentToDestroy2 = (int)(Utils.GetLerpValue(410f, 470f, DeathAnimationTimer, clamped: true) * 10f) + 60;
			destroySegment(segmentToDestroy2, ref DeathAnimationTimer);
		}
		MoonlordDeathDrama.RequestLight(Utils.GetLerpValue(430f, 465f, DeathAnimationTimer, clamped: true), Main.LocalPlayer.Center);
		if ((float)DeathAnimationTimer >= 485f)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.NPCLoot();
			base.NPC.active = false;
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		DeathAnimationTimer++;
		void destroySegment(int index, ref int destroyedSegments)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0104: Unknown result type (might be due to invalid IL or missing references)
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in DeathSegmentSound, base.NPC.Center);
			}
			List<int> segments = new List<int>
			{
				ModContent.NPCType<DevourerofGodsBody>(),
				ModContent.NPCType<DevourerofGodsTail>()
			};
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (segments.Contains(n.type) && n.active && (n.type == segments[1] || n.ModNPC<DevourerofGodsBody>().SegmentIndex == index))
				{
					for (int j = 0; j < 20; j++)
					{
						Dust dust = Dust.NewDustPerfect(n.Center + Main.rand.NextVector2Circular(25f, 25f), 234);
						dust.scale = 1.7f;
						dust.velocity = Main.rand.NextVector2Circular(9f, 9f);
						dust.noGravity = true;
					}
					n.life = 0;
					n.HitEffect();
					n.active = false;
					n.ForceNetUpdate(ignoreCurrentNetSpam: false);
					destroyedSegments++;
					break;
				}
			}
		}
	}

	public Vector2 GetRiftLocation(bool activateRift = false)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 realSpot = Vector2.Zero;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.type == ModContent.ProjectileType<DoGTeleportRift>() && proj.ModProjectile is DoGTeleportRift { FakeRift: false })
			{
				realSpot = proj.Center;
				if (activateRift)
				{
					proj.ModProjectile<DoGTeleportRift>().SwitchAIStates();
				}
			}
		}
		return realSpot;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		bool actuallyInPhaseTwo = Phase2Started && base.NPC.localAI[2] <= 60f;
		Texture2D mainTexture = (actuallyInPhaseTwo ? TextureP2.Value : TextureAssets.Npc[base.Type].Value);
		Texture2D mainGlowTexture = (actuallyInPhaseTwo ? TextureP2_Glow_Purple.Value : Texture_Glow_Purple.Value);
		Texture2D mainJawTexture = (actuallyInPhaseTwo ? JawTextureP2.Value : JawTexture.Value);
		Texture2D mainJawGlowTexture = (actuallyInPhaseTwo ? JawTextureP2_Glow.Value : JawTexture_Glow.Value);
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureP2_Full.Value, DevourerofGodsBody.TextureP2.Value, 4, 26, 0.5f, new Vector2(30f, 10f), 2, 20f);
		}
		bool shouldUseShader = CalamityDrawParameterNPC.DoGDeathAnimationTimer != 0;
		SpriteBatchSnapshot snap = new SpriteBatchSnapshot(spriteBatch);
		if (shouldUseShader)
		{
			if (noiseOffset == Vector2.zeroVector)
			{
				noiseOffset = base.NPC.Center;
			}
			Main.spriteBatch.End(out snap);
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.NonPremultiplied, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
			MiscShaderData miscShaderData = GameShaders.Misc["CalamityMod:Dissolve"];
			Texture2D dissolveTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/HarshNoise", (AssetRequestMode)2).Value;
			miscShaderData.Shader.Parameters["noiseScale"].SetValue(0.5f);
			miscShaderData.Shader.Parameters["dissolveIntensity"].SetValue((float)CalamityDrawParameterNPC.DoGDeathAnimationTimer / 600f);
			miscShaderData.Shader.Parameters["sampleOffset"].SetValue(noiseOffset * 0.5f);
			EffectParameter obj = miscShaderData.Shader.Parameters["transitionColor"];
			Color specialMoveColor = SpecialMoveColor;
			obj.SetValue(((Color)(ref specialMoveColor)).ToVector4());
			miscShaderData.Shader.Parameters["transitionOffset"].SetValue(0.05f);
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)dissolveTexture;
			((Game)Main.instance).GraphicsDevice.SamplerStates[1] = SamplerState.LinearWrap;
			miscShaderData.Apply();
		}
		SpriteEffects spriteEffects = (SpriteEffects)(base.NPC.spriteDirection == 1);
		Vector2 halfSizeTexture = mainTexture.Size() * 0.5f;
		Vector2 drawPosition = base.NPC.Center - screenPos;
		drawPosition -= new Vector2((float)mainTexture.Width, (float)mainTexture.Height) * base.NPC.scale / 2f;
		drawPosition += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Vector2 jawOrigin = mainJawTexture.Size() * 0.5f;
		for (int i = -1; i <= 1; i += 2)
		{
			float jawBaseOffset = (actuallyInPhaseTwo ? 42f : 28f);
			SpriteEffects jawSpriteEffect = (SpriteEffects)(i == 1);
			Vector2 jawDrawPosition = drawPosition;
			jawDrawPosition += Vector2.UnitX.RotatedBy(base.NPC.rotation + JawRotation * (float)i) * (float)i * (jawBaseOffset + MathF.Sin(JawRotation) * 24f);
			jawDrawPosition -= Vector2.UnitY.RotatedBy(base.NPC.rotation) * ((actuallyInPhaseTwo ? 44f : 8f) + MathF.Sin(JawRotation) * (actuallyInPhaseTwo ? 30f : (-6f)));
			spriteBatch.Draw(mainJawTexture, jawDrawPosition, (Rectangle?)null, base.NPC.GetAlpha(drawColor), base.NPC.rotation + JawRotation * (float)i, jawOrigin, base.NPC.scale, jawSpriteEffect, 0f);
			spriteBatch.Draw(mainJawGlowTexture, jawDrawPosition, (Rectangle?)null, base.NPC.GetAlpha(Color.White), base.NPC.rotation + JawRotation * (float)i, jawOrigin, base.NPC.scale, jawSpriteEffect, 0f);
			if (GodSlayerDashJawFadeProgress > 0.02f && CalamityDrawParameterNPC.DoGDeathAnimationTimer <= 0)
			{
				using (spriteBatch.Scope())
				{
					spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
					Vector2 godSlayerJawOrigin = GodSlayerDashJawTexture.Size() * 0.5f;
					float godSlayerJawOpacity = GodSlayerDashJawFadeProgress;
					spriteBatch.Draw(GodSlayerDashJawTexture.Value, jawDrawPosition, (Rectangle?)null, base.NPC.GetAlpha(Color.Fuchsia) * godSlayerJawOpacity, base.NPC.rotation + JawRotation * (float)i, godSlayerJawOrigin, base.NPC.scale * 1.6f, jawSpriteEffect, 0f);
					spriteBatch.Draw(GodSlayerDashJawTexture.Value, jawDrawPosition, (Rectangle?)null, base.NPC.GetAlpha(Color.Cyan) * godSlayerJawOpacity, base.NPC.rotation + JawRotation * (float)i, godSlayerJawOrigin, base.NPC.scale * 1.3f, jawSpriteEffect, 0f);
					spriteBatch.End();
				}
			}
		}
		spriteBatch.Draw(mainTexture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		Color glowmaskColor = Color.Lerp(Color.White, Color.Fuchsia, 0.5f);
		spriteBatch.Draw(mainGlowTexture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(glowmaskColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		mainGlowTexture = (actuallyInPhaseTwo ? TextureP2_Glow_Cyan.Value : Texture_Glow_Cyan.Value);
		glowmaskColor = Color.Lerp(Color.White, Color.Cyan, 0.5f);
		spriteBatch.Draw(mainGlowTexture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(glowmaskColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		if (shouldUseShader)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(in snap);
		}
		return false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<CosmiliteBrick>();
	}

	public override void OnKill()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			CalamityGlobalTownNPC.SetNewShopVariable(new int[1] { ModContent.NPCType<Bandit>() }, DownedBossSystem.downedDoG);
			if (!DownedBossSystem.downedDoG)
			{
				string key = "Mods.CalamityMod.Status.Progression.DoGBossText";
				Color messageColor = Color.Cyan;
				string key2 = "Mods.CalamityMod.Status.Progression.DoGBossText2";
				Color messageColor2 = Color.Orange;
				Color messageColor3 = Color.Yellow;
				CalamityUtils.BroadcastLocalizedText(key, messageColor);
				CalamityUtils.BroadcastLocalizedText(key2, messageColor2);
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.DargonBossText", messageColor3);
			}
			DownedBossSystem.downedDoG = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override bool SpecialOnKill()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<DevourerofGodsHead>(), ModContent.NPCType<DevourerofGodsBody>(), ModContent.NPCType<DevourerofGodsTail>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<DevourerofGodsBag>()));
		npcLoot.DefineConditionalDropSet(() => true).Add(DropHelper.PerPlayer(ModContent.ItemType<OmegaHealingPotion>(), 1, 5, 15), hideLootReport: true);
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[6]
		{
			ModContent.ItemType<MawOfInfinity>(),
			ModContent.ItemType<TheObliterator>(),
			ModContent.ItemType<ThreadOfEradication>(),
			ModContent.ItemType<HyperdeathRiftScepter>(),
			ModContent.ItemType<VoidEaterMarionette>(),
			ModContent.ItemType<DimensionTearingDisk>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<CosmicDischarge>(), 10);
		normalOnly.Add(ModContent.ItemType<DevourerofGodsMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<CosmiliteBar>(), 1, 65, 80));
		normalOnly.Add(ModContent.ItemType<CosmiliteBrick>(), 1, 150, 250);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<DevourerOfGodsRelic>());
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<TheWand>()), hideLootReport: true);
		int dropRate = 10;
		int dropMin = 1;
		int dropMax = 9999;
		mainRule.Add(DropHelper.PerPlayer(8, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(430, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(432, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(429, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(428, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(431, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(1245, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(3114, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(3045, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(974, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(3004, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(2274, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(433, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(523, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(1333, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4383, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4384, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4385, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4386, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4387, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(4388, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(5293, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(5353, dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<CausticTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<KelpTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<ThermalTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<VoidTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<AlgalPrismTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<AstralTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<GloomTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<NavyPrismTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<RefractivePrismTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<SulphurousTorch>(), dropRate, dropMin, dropMax), hideLootReport: true);
		npcLoot.Add(ModContent.ItemType<DevourerofGodsTrophy>(), 10);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedDoG, ModContent.ItemType<LoreDevourerofGods>(), ui: true, DropHelper.FirstKillText);
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		if (Phase2Started && base.NPC.localAI[2] > 60f)
		{
			return false;
		}
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
		if (minDist <= (Phase2Started ? 80f : 55f) * base.NPC.scale)
		{
			if (!(base.NPC.Opacity >= 1f))
			{
				return postTeleportTimer > 0;
			}
			return true;
		}
		return false;
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		modifiers.SetMaxDamage(base.NPC.life - 1);
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (Main.zenithWorld && projectile.type == ModContent.ProjectileType<LaceratorYoyo>())
		{
			modifiers.SourceDamage *= 10f;
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 2f;
		return null;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool CheckDead()
	{
		base.NPC.life = 1;
		Dying = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.active = true;
		base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 8;
			SoundStyle style = HitSound with
			{
				Pitch = HitSound.Pitch + extrapitch
			};
			SoundEngine.PlaySound(in style, base.NPC.Center);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DoGS").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DoGS2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("DoGS5").Type, base.NPC.scale);
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = (int)(100f * base.NPC.scale);
		base.NPC.height = (int)(100f * base.NPC.scale);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 15; i++)
		{
			int cosmiliteDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[cosmiliteDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[cosmiliteDust].scale = 0.5f;
				Main.dust[cosmiliteDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 30; j++)
		{
			int cosmiliteDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
			Main.dust[cosmiliteDust2].noGravity = true;
			Dust obj2 = Main.dust[cosmiliteDust2];
			obj2.velocity *= 5f;
			cosmiliteDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[cosmiliteDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage <= 0)
		{
			return;
		}
		if (!DialogueDisplaySystem.ContainsDialogueKey("Mods.CalamityMod.DevourerOfGods.Phases") && target.Calamity().dogTextCooldown <= 0 && !BossRushEvent.BossRushActive)
		{
			int counter = target.Calamity().DoGHeadHitCounter;
			string DialogueGroup;
			int DialogueIndex;
			if (target.statLife - hurtInfo.Damage <= 0)
			{
				DialogueGroup = "Mods.CalamityMod.DevourerOfGods.Death";
				DialogueIndex = ((counter == 0) ? ((base.NPC.GetLifePercent() <= 0.25f) ? 4 : 0) : ((!(base.NPC.GetLifePercent() <= 0.25f) && counter < 10) ? (Phase2Started ? 2 : (spawnedGuardians ? 1 : 0)) : 3));
			}
			else if (counter == 0 && Phase2Started)
			{
				DialogueGroup = "Mods.CalamityMod.DevourerOfGods.Running";
				DialogueIndex = ((base.NPC.GetLifePercent() <= 0.25f) ? 1 : 0);
			}
			else
			{
				DialogueGroup = "Mods.CalamityMod.DevourerOfGods.Head";
				DialogueIndex = ((counter > 9) ? Main.rand.Next(10, 13) : ((counter == 9) ? 10 : ((counter <= 4) ? Main.rand.Next(0, 4) : Main.rand.Next(4, 9))));
			}
			DialogueDisplaySystem.StartDialogueOnClient(DialogueGroup, (Entity)base.NPC, DialogueIndex, 60, false, (DisplayEffect)new BossText(), -1f);
			target.Calamity().dogTextCooldown = 90;
		}
		target.Calamity().DoGHeadHitCounter++;
	}

	public DevourerofGodsHead()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		PortalIndex = -1;
		minLength = 100;
		maxLength = 101;
		AwaitingPhase2Teleport = true;
		idleCounter = 300;
		teleportTimer = -1;
		PortalEntryLocation = Vector2.Zero;
		doTpFX = true;
		noiseOffset = Vector2.Zero;
		base._002Ector();
	}
}
