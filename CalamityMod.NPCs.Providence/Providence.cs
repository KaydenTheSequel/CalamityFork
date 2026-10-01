using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Dyes;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Packets;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Systems.Graphic;
using CalamityMod.Tiles.Ores;
using CalamityMod.Utilities;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.NPCs.Providence;

[AutoloadBossHead]
public class Providence : ModNPC
{
	private enum Phase : sbyte
	{
		PhaseChange = -1,
		HolyBlast,
		HolyFire,
		FlameCocoon,
		MoltenBlobs,
		HolyBomb,
		SpearCocoon,
		Crystal,
		Laser
	}

	public enum BossMode
	{
		Enraged = -1,
		Normal,
		Rainbow
	}

	private Color HighFireColor;

	private Color LowFireColor;

	private bool text;

	private bool useDefenseFrames;

	private float bossLife;

	private byte biomeType;

	private int flightPath;

	private sbyte phaseChange;

	private byte frameUsed;

	private int healTimer;

	internal bool challenge;

	public bool hasBeenGivenFullPower;

	public static bool shouldDrawInfernoBorder = true;

	public bool Dying;

	public int DeathAnimationTimer;

	public static float borderRadius = 3000f;

	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceSpawn")
	{
		Volume = 1.2f
	};

	public static readonly SoundStyle HolyRaySound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyRay")
	{
		Volume = 1.25f
	};

	public static readonly SoundStyle HurtSound = new SoundStyle("CalamityMod/Sounds/NPCHit/ProvidenceHurt");

	public static readonly SoundStyle DeathAnimationSound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceDeathAnimation");

	public static readonly SoundStyle NearBurnSound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceSizzle");

	public static readonly SoundStyle BurnStartSound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceBurn");

	public static readonly SoundStyle BurnLoopSound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceBurnLoop")
	{
		IsLooped = true
	};

	public SlotId BurningSoundSlot;

	public float SoundWarningLevel;

	public static float normalDR = 0.3f;

	public static float cocoonDR = 0.9f;

	private const float TimeForStarDespawn = 120f;

	private const float TimeForShieldDespawn = 120f;

	public static Asset<Texture2D> TextureAlt;

	public static Asset<Texture2D> TextureAltNight;

	public static Asset<Texture2D> TextureAttack;

	public static Asset<Texture2D> TextureAttackNight;

	public static Asset<Texture2D> TextureAttackAlt;

	public static Asset<Texture2D> TextureAttackAltNight;

	public static Asset<Texture2D> TextureDefense;

	public static Asset<Texture2D> TextureDefenseNight;

	public static Asset<Texture2D> TextureDefenseAlt;

	public static Asset<Texture2D> TextureDefenseAltNight;

	public static Asset<Texture2D> TextureNight;

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> TextureAlt_Glow;

	public static Asset<Texture2D> TextureAltNight_Glow;

	public static Asset<Texture2D> TextureAttack_Glow;

	public static Asset<Texture2D> TextureAttackNight_Glow;

	public static Asset<Texture2D> TextureAttackAlt_Glow;

	public static Asset<Texture2D> TextureAttackAltNight_Glow;

	public static Asset<Texture2D> TextureDefense_Glow;

	public static Asset<Texture2D> TextureDefenseNight_Glow;

	public static Asset<Texture2D> TextureDefenseAlt_Glow;

	public static Asset<Texture2D> TextureDefenseAltNight_Glow;

	public static Asset<Texture2D> TextureNight_Glow;

	public static Asset<Texture2D> Texture_Glow_2;

	public static Asset<Texture2D> TextureAlt_Glow_2;

	public static Asset<Texture2D> TextureAltNight_Glow_2;

	public static Asset<Texture2D> TextureAttack_Glow_2;

	public static Asset<Texture2D> TextureAttackNight_Glow_2;

	public static Asset<Texture2D> TextureAttackAlt_Glow_2;

	public static Asset<Texture2D> TextureAttackAltNight_Glow_2;

	public static Asset<Texture2D> TextureDefense_Glow_2;

	public static Asset<Texture2D> TextureDefenseNight_Glow_2;

	public static Asset<Texture2D> TextureDefenseAlt_Glow_2;

	public static Asset<Texture2D> TextureDefenseAltNight_Glow_2;

	public static Asset<Texture2D> TextureNight_Glow_2;

	public static int FireDamage = 36;

	public static int BlobDamage = 36;

	public static int FireSentryDamage = 54;

	public static int MoltenBlastDamage = 54;

	public static int StarDamage = 54;

	public static int SpearDamage = 42;

	public static int CrystalDamage = 48;

	public static int HolyBlastDamage = 60;

	public static int RayDamage = 100;

	public static int StarHeal = (Main.expertMode ? 50 : 35);

	[CompilerGenerated]
	private static Asset<Texture2D> _003CDiagonalNoise_003Ek__BackingField;

	[CompilerGenerated]
	private static Asset<Texture2D> _003CUpwardPerlinNoise_003Ek__BackingField;

	[CompilerGenerated]
	private static Asset<Texture2D> _003CUpwardNoise_003Ek__BackingField;

	private float AIState
	{
		get
		{
			return base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = value;
		}
	}

	private static Asset<Texture2D> DiagonalNoise => _003CDiagonalNoise_003Ek__BackingField ?? (_003CDiagonalNoise_003Ek__BackingField = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/HarshNoise", (AssetRequestMode)2));

	private static Asset<Texture2D> UpwardPerlinNoise => _003CUpwardPerlinNoise_003Ek__BackingField ?? (_003CUpwardPerlinNoise_003Ek__BackingField = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Perlin", (AssetRequestMode)2));

	private static Asset<Texture2D> UpwardNoise => _003CUpwardNoise_003Ek__BackingField ?? (_003CUpwardNoise_003Ek__BackingField = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/MeltyNoise", (AssetRequestMode)2));

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<HolyAura>(), 0, 0f);
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 3;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.2f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.32f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 16f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 6f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			string GlowPath = "CalamityMod/NPCs/Providence/Glowmasks/Providence";
			TextureAlt = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "Alt", (AssetRequestMode)2);
			TextureAltNight = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "AltNight", (AssetRequestMode)2);
			TextureAttack = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "Attack", (AssetRequestMode)2);
			TextureAttackNight = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "AttackNight", (AssetRequestMode)2);
			TextureAttackAlt = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "AttackAlt", (AssetRequestMode)2);
			TextureAttackAltNight = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "AttackAltNight", (AssetRequestMode)2);
			TextureDefense = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "Defense", (AssetRequestMode)2);
			TextureDefenseNight = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "DefenseNight", (AssetRequestMode)2);
			TextureDefenseAlt = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "DefenseAlt", (AssetRequestMode)2);
			TextureDefenseAltNight = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "DefenseAltNight", (AssetRequestMode)2);
			TextureNight = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence" + "Night", (AssetRequestMode)2);
			Texture_Glow = ModContent.Request<Texture2D>(GlowPath + "Glow", (AssetRequestMode)2);
			TextureAlt_Glow = ModContent.Request<Texture2D>(GlowPath + "AltGlow", (AssetRequestMode)2);
			TextureAltNight_Glow = ModContent.Request<Texture2D>(GlowPath + "AltGlowNight", (AssetRequestMode)2);
			TextureAttack_Glow = ModContent.Request<Texture2D>(GlowPath + "AttackGlow", (AssetRequestMode)2);
			TextureAttackNight_Glow = ModContent.Request<Texture2D>(GlowPath + "AttackGlowNight", (AssetRequestMode)2);
			TextureAttackAlt_Glow = ModContent.Request<Texture2D>(GlowPath + "AttackAltGlow", (AssetRequestMode)2);
			TextureAttackAltNight_Glow = ModContent.Request<Texture2D>(GlowPath + "AttackAltGlowNight", (AssetRequestMode)2);
			TextureDefense_Glow = ModContent.Request<Texture2D>(GlowPath + "DefenseGlow", (AssetRequestMode)2);
			TextureDefenseNight_Glow = ModContent.Request<Texture2D>(GlowPath + "DefenseGlowNight", (AssetRequestMode)2);
			TextureDefenseAlt_Glow = ModContent.Request<Texture2D>(GlowPath + "DefenseAltGlow", (AssetRequestMode)2);
			TextureDefenseAltNight_Glow = ModContent.Request<Texture2D>(GlowPath + "DefenseAltGlowNight", (AssetRequestMode)2);
			TextureNight_Glow = ModContent.Request<Texture2D>(GlowPath + "GlowNight", (AssetRequestMode)2);
			Texture_Glow_2 = ModContent.Request<Texture2D>(GlowPath + "Glow2", (AssetRequestMode)2);
			TextureAlt_Glow_2 = ModContent.Request<Texture2D>(GlowPath + "AltGlow2", (AssetRequestMode)2);
			TextureAttack_Glow_2 = ModContent.Request<Texture2D>(GlowPath + "AttackGlow2", (AssetRequestMode)2);
			TextureAttackAlt_Glow_2 = ModContent.Request<Texture2D>(GlowPath + "AttackAltGlow2", (AssetRequestMode)2);
			TextureDefense_Glow_2 = ModContent.Request<Texture2D>(GlowPath + "DefenseGlow2", (AssetRequestMode)2);
			TextureDefenseAlt_Glow_2 = ModContent.Request<Texture2D>(GlowPath + "DefenseAltGlow2", (AssetRequestMode)2);
		}
	}

	public override void ModifyHoverBoundingBox(ref Rectangle boundingBox)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		float spawnAnimationTime = 180f;
		if (base.NPC.Calamity().newAI[3] < spawnAnimationTime)
		{
			boundingBox = new Rectangle(0, 0, 0, 0);
		}
		base.ModifyHoverBoundingBox(ref boundingBox);
	}

	public override void Load()
	{
		GeneralDrawLayerSystem.OnBeforeAllTiles += DrawHolyInferno;
	}

	public override void Unload()
	{
		GeneralDrawLayerSystem.OnBeforeAllTiles -= DrawHolyInferno;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 36f;
		base.NPC.damage = 0;
		base.NPC.width = 600;
		base.NPC.height = 450;
		base.NPC.defense = 50;
		base.NPC.DR_NERD(normalDR);
		base.NPC.LifeMaxNERB(250000, 375000, 1250000);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(1, 50);
		base.NPC.boss = true;
		base.NPC.Opacity = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		base.NPC.DeathSound = null;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.25f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Providence")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(new BitsByte
		{
			[0] = text,
			[1] = useDefenseFrames,
			[2] = base.NPC.dontTakeDamage,
			[3] = base.NPC.chaseable,
			[4] = Dying,
			[5] = shouldDrawInfernoBorder,
			[6] = flightPath != 0,
			[7] = flightPath == 1
		});
		writer.Write(biomeType);
		writer.Write(phaseChange);
		writer.Write(frameUsed);
		writer.Write(healTimer);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		writer.Write((Half)SoundWarningLevel);
		writer.Write(DeathAnimationTimer);
		writer.Write(borderRadius);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		bool wasDyingBefore = Dying;
		BitsByte bits = reader.ReadBitsByte();
		text = bits[0];
		useDefenseFrames = bits[1];
		base.NPC.dontTakeDamage = bits[2];
		base.NPC.chaseable = bits[3];
		Dying = bits[4];
		shouldDrawInfernoBorder = bits[5];
		if (bits[6])
		{
			flightPath = (bits[7] ? 1 : (-1));
		}
		else
		{
			flightPath = 0;
		}
		biomeType = reader.ReadByte();
		phaseChange = reader.ReadSByte();
		frameUsed = reader.ReadByte();
		healTimer = reader.ReadInt32();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		SoundWarningLevel = (float)reader.ReadHalf();
		DeathAnimationTimer = reader.ReadInt32();
		borderRadius = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
		if (Main.dedServ && !wasDyingBefore && Dying)
		{
			base.NPC.ForceNetUpdate();
		}
	}

	public override void AI()
	{
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e38: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_173a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1796: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3618: Unknown result type (might be due to invalid IL or missing references)
		//IL_3622: Unknown result type (might be due to invalid IL or missing references)
		//IL_3627: Unknown result type (might be due to invalid IL or missing references)
		//IL_23fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2402: Unknown result type (might be due to invalid IL or missing references)
		//IL_32e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_32ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_3247: Unknown result type (might be due to invalid IL or missing references)
		//IL_324c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3671: Unknown result type (might be due to invalid IL or missing references)
		//IL_367c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3691: Unknown result type (might be due to invalid IL or missing references)
		//IL_3699: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1807: Unknown result type (might be due to invalid IL or missing references)
		//IL_180c: Unknown result type (might be due to invalid IL or missing references)
		//IL_183b: Unknown result type (might be due to invalid IL or missing references)
		//IL_195f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1979: Unknown result type (might be due to invalid IL or missing references)
		//IL_2425: Unknown result type (might be due to invalid IL or missing references)
		//IL_242f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2434: Unknown result type (might be due to invalid IL or missing references)
		//IL_3333: Unknown result type (might be due to invalid IL or missing references)
		//IL_3338: Unknown result type (might be due to invalid IL or missing references)
		//IL_333d: Unknown result type (might be due to invalid IL or missing references)
		//IL_326f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3279: Unknown result type (might be due to invalid IL or missing references)
		//IL_327e: Unknown result type (might be due to invalid IL or missing references)
		//IL_36c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_36d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_36f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_36f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_36f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3708: Unknown result type (might be due to invalid IL or missing references)
		//IL_3716: Unknown result type (might be due to invalid IL or missing references)
		//IL_371b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1072: Unknown result type (might be due to invalid IL or missing references)
		//IL_107d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c98: Unknown result type (might be due to invalid IL or missing references)
		//IL_1107: Unknown result type (might be due to invalid IL or missing references)
		//IL_1112: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_184d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1852: Unknown result type (might be due to invalid IL or missing references)
		//IL_198e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1993: Unknown result type (might be due to invalid IL or missing references)
		//IL_1997: Unknown result type (might be due to invalid IL or missing references)
		//IL_34c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_34c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_34c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_34ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_34d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_34da: Unknown result type (might be due to invalid IL or missing references)
		//IL_3347: Unknown result type (might be due to invalid IL or missing references)
		//IL_3351: Unknown result type (might be due to invalid IL or missing references)
		//IL_3357: Unknown result type (might be due to invalid IL or missing references)
		//IL_3359: Unknown result type (might be due to invalid IL or missing references)
		//IL_3360: Unknown result type (might be due to invalid IL or missing references)
		//IL_3365: Unknown result type (might be due to invalid IL or missing references)
		//IL_1153: Unknown result type (might be due to invalid IL or missing references)
		//IL_115e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2299: Unknown result type (might be due to invalid IL or missing references)
		//IL_3108: Unknown result type (might be due to invalid IL or missing references)
		//IL_1864: Unknown result type (might be due to invalid IL or missing references)
		//IL_1888: Unknown result type (might be due to invalid IL or missing references)
		//IL_1896: Unknown result type (might be due to invalid IL or missing references)
		//IL_189b: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_34e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_34e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_34f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_34fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_3500: Unknown result type (might be due to invalid IL or missing references)
		//IL_336c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3374: Unknown result type (might be due to invalid IL or missing references)
		//IL_337b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3385: Unknown result type (might be due to invalid IL or missing references)
		//IL_338b: Unknown result type (might be due to invalid IL or missing references)
		//IL_394d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3952: Unknown result type (might be due to invalid IL or missing references)
		//IL_3961: Unknown result type (might be due to invalid IL or missing references)
		//IL_3966: Unknown result type (might be due to invalid IL or missing references)
		//IL_3982: Unknown result type (might be due to invalid IL or missing references)
		//IL_3988: Unknown result type (might be due to invalid IL or missing references)
		//IL_398a: Unknown result type (might be due to invalid IL or missing references)
		//IL_398f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3991: Unknown result type (might be due to invalid IL or missing references)
		//IL_3996: Unknown result type (might be due to invalid IL or missing references)
		//IL_3997: Unknown result type (might be due to invalid IL or missing references)
		//IL_3999: Unknown result type (might be due to invalid IL or missing references)
		//IL_399e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bff: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c13: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c15: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c21: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c26: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c60: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c82: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c89: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c98: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_168b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1696: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e19: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_26e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_26ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2504: Unknown result type (might be due to invalid IL or missing references)
		//IL_353f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3541: Unknown result type (might be due to invalid IL or missing references)
		//IL_33ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_33cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_3755: Unknown result type (might be due to invalid IL or missing references)
		//IL_3757: Unknown result type (might be due to invalid IL or missing references)
		//IL_3760: Unknown result type (might be due to invalid IL or missing references)
		//IL_3765: Unknown result type (might be due to invalid IL or missing references)
		//IL_39d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_39c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3da1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d49: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19da: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b16: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b27: Unknown result type (might be due to invalid IL or missing references)
		//IL_275f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2752: Unknown result type (might be due to invalid IL or missing references)
		//IL_2576: Unknown result type (might be due to invalid IL or missing references)
		//IL_2567: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2eb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ed7: Unknown result type (might be due to invalid IL or missing references)
		//IL_3189: Unknown result type (might be due to invalid IL or missing references)
		//IL_3190: Unknown result type (might be due to invalid IL or missing references)
		//IL_3581: Unknown result type (might be due to invalid IL or missing references)
		//IL_3583: Unknown result type (might be due to invalid IL or missing references)
		//IL_3585: Unknown result type (might be due to invalid IL or missing references)
		//IL_340c: Unknown result type (might be due to invalid IL or missing references)
		//IL_340e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3410: Unknown result type (might be due to invalid IL or missing references)
		//IL_379e: Unknown result type (might be due to invalid IL or missing references)
		//IL_378b: Unknown result type (might be due to invalid IL or missing references)
		//IL_39dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_39f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_39fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a25: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a57: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_3dc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_3de2: Unknown result type (might be due to invalid IL or missing references)
		//IL_3de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_3dea: Unknown result type (might be due to invalid IL or missing references)
		//IL_3def: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e03: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e13: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e36: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2037: Unknown result type (might be due to invalid IL or missing references)
		//IL_2047: Unknown result type (might be due to invalid IL or missing references)
		//IL_204c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2051: Unknown result type (might be due to invalid IL or missing references)
		//IL_2068: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dad: Unknown result type (might be due to invalid IL or missing references)
		//IL_2327: Unknown result type (might be due to invalid IL or missing references)
		//IL_232e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2764: Unknown result type (might be due to invalid IL or missing references)
		//IL_257b: Unknown result type (might be due to invalid IL or missing references)
		//IL_37a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_37bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_37c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_37cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_37e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_37f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_3805: Unknown result type (might be due to invalid IL or missing references)
		//IL_3813: Unknown result type (might be due to invalid IL or missing references)
		//IL_381b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3820: Unknown result type (might be due to invalid IL or missing references)
		//IL_3825: Unknown result type (might be due to invalid IL or missing references)
		//IL_3832: Unknown result type (might be due to invalid IL or missing references)
		//IL_3844: Unknown result type (might be due to invalid IL or missing references)
		//IL_384a: Unknown result type (might be due to invalid IL or missing references)
		//IL_384c: Unknown result type (might be due to invalid IL or missing references)
		//IL_3851: Unknown result type (might be due to invalid IL or missing references)
		//IL_3853: Unknown result type (might be due to invalid IL or missing references)
		//IL_386e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3873: Unknown result type (might be due to invalid IL or missing references)
		//IL_38f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_38fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_3aa7: Unknown result type (might be due to invalid IL or missing references)
		//IL_3aa9: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e91: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ebd: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ec5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b80: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ad9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ae0: Unknown result type (might be due to invalid IL or missing references)
		//IL_276e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2778: Unknown result type (might be due to invalid IL or missing references)
		//IL_277e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2780: Unknown result type (might be due to invalid IL or missing references)
		//IL_2785: Unknown result type (might be due to invalid IL or missing references)
		//IL_2585: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f41: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f47: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f49: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f62: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f72: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f95: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f35: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_4000: Unknown result type (might be due to invalid IL or missing references)
		//IL_401c: Unknown result type (might be due to invalid IL or missing references)
		//IL_4024: Unknown result type (might be due to invalid IL or missing references)
		//IL_20bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_28bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_28c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_28c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_28c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_28ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_28d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_28dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_2618: Unknown result type (might be due to invalid IL or missing references)
		//IL_261a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f46: Unknown result type (might be due to invalid IL or missing references)
		//IL_20de: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_28f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_27ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_27af: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_25da: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f54: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f56: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f60: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f67: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2102: Unknown result type (might be due to invalid IL or missing references)
		//IL_2107: Unknown result type (might be due to invalid IL or missing references)
		//IL_293b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2928: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f85: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2116: Unknown result type (might be due to invalid IL or missing references)
		//IL_211b: Unknown result type (might be due to invalid IL or missing references)
		//IL_211d: Unknown result type (might be due to invalid IL or missing references)
		//IL_210e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_2940: Unknown result type (might be due to invalid IL or missing references)
		//IL_2959: Unknown result type (might be due to invalid IL or missing references)
		//IL_295e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_2f9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2122: Unknown result type (might be due to invalid IL or missing references)
		//IL_2130: Unknown result type (might be due to invalid IL or missing references)
		//IL_2132: Unknown result type (might be due to invalid IL or missing references)
		//IL_214d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2154: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b86: Unknown result type (might be due to invalid IL or missing references)
		//IL_2977: Unknown result type (might be due to invalid IL or missing references)
		//IL_298b: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_29a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_29b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_29b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_29be: Unknown result type (might be due to invalid IL or missing references)
		//IL_29c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a44: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2be2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2be8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2bff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2c94: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_2cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d31: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d37: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d58: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d5d: Unknown result type (might be due to invalid IL or missing references)
		shouldDrawInfernoBorder = true;
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.holyBoss = base.NPC.whoAmI;
		base.NPC.rotation = MathHelper.Lerp(base.NPC.rotation, base.NPC.velocity.X * 0.006f, 0.1f);
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		bool getFuckedAI = Main.zenithWorld;
		if (getFuckedAI)
		{
			base.NPC.localAI[1] = 1f;
		}
		else if (hasBeenGivenFullPower)
		{
			base.NPC.localAI[1] = -1f;
		}
		else
		{
			base.NPC.localAI[1] = 0f;
		}
		bool fullPowerAI = base.NPC.localAI[1] != 0f;
		bool death = CalamityWorld.death | fullPowerAI;
		bool revenge = CalamityWorld.revenge | fullPowerAI;
		bool expertMode = Main.expertMode | fullPowerAI;
		bool isHoly = player.ZoneHallow;
		bool isHell = player.ZoneUnderworldHeight;
		bool normalAttackRate = true;
		float spawnAnimationTime = 180f;
		bool spawnAnimation = calamityGlobalNPC.newAI[3] < spawnAnimationTime;
		if (!spawnAnimation)
		{
			base.NPC.Opacity = 1f;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if ((!getFuckedAI & fullPowerAI) && calamityGlobalNPC.newAI[3] == spawnAnimationTime)
		{
			AIState = 0f;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
			calamityGlobalNPC.newAI[1] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			calamityGlobalNPC.newAI[3] = 0f;
			DespawnSpecificProjectiles(everything: true);
			Projectile.NewProjectile(base.NPC.GetSource_FromThis(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<HolyAura>(), 0, 0f);
			if (Main.netMode != 0)
			{
				ProvidenceDyeConditionSyncPacket.Send(this);
			}
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		base.NPC.Calamity().CurrentlyEnraged = !BossRushEvent.BossRushActive & fullPowerAI;
		int dustType = ProvUtils.GetDustID();
		float phaseTime = (fullPowerAI ? (240f - 60f * (1f - lifeRatio)) : 300f);
		float crystalPhaseTime = (fullPowerAI ? ((float)Math.Round(60f * lifeRatio)) : (death ? 60f : 120f));
		int enragedCrystalTime = 210;
		int gfbCrystalTime = 1500 + enragedCrystalTime;
		float attackDelayAfterCocoon = phaseTime * 0.3f;
		bool ignoreGuardianAmt = lifeRatio < (death ? 0.2f : 0.15f);
		bool phase2 = lifeRatio < 0.75f && !fullPowerAI;
		bool delayAttacks = base.NPC.localAI[2] > 0f;
		float spearRateIncrease = 1f - lifeRatio;
		float baseSpearRate = 18f;
		float spearRate = 1f + spearRateIncrease;
		double attackRateMult = 1.0;
		Vector2 fireFrom = default(Vector2);
		((Vector2)(ref fireFrom))._002Ector(base.NPC.Center.X, base.NPC.Center.Y + 20f * base.NPC.scale);
		float cocoonProjVelocity = 3f + (death ? (2f * (1f - lifeRatio)) : 0f);
		float distanceNeededToShoot = (death ? 300f : (revenge ? 360f : 420f)) * base.NPC.scale;
		float distanceX = Math.Abs(base.NPC.Center.X - player.Center.X);
		float burnIntensity = CalculateBurnIntensity(attackDelayAfterCocoon);
		Color hiColor = ProvUtils.GetProjectileColor(0);
		Color medColor = ProvUtils.GetProjectileColor(255);
		Color loColor = ProvUtils.GetProjectileColor(255, Outline: true);
		if (!player.dead && player.active && !player.creativeGodMode && !Dying)
		{
			if (burnIntensity >= 1f)
			{
				if (SoundWarningLevel < 2f)
				{
					SoundEngine.PlaySound(in BurnStartSound, player.Center);
					BurningSoundSlot = SoundEngine.PlaySound(in BurnLoopSound, player.Center);
					SoundWarningLevel = 2f;
				}
				player.AddBuff(ModContent.BuffType<HolyInferno>(), 2);
			}
			else if (SoundWarningLevel > 1f)
			{
				SoundWarningLevel -= 0.01f;
				if (SoundWarningLevel < 1f)
				{
					SoundWarningLevel = 1f;
				}
			}
			else if (burnIntensity > 0.45f)
			{
				if (SoundWarningLevel < 1f)
				{
					SoundEngine.PlaySound(in NearBurnSound, player.Center);
				}
				SoundWarningLevel = 1f;
			}
			else if (burnIntensity <= 0f)
			{
				SoundWarningLevel = 0f;
			}
		}
		else if (SoundWarningLevel > 0f)
		{
			SoundWarningLevel -= 0.02f;
		}
		if (SoundEngine.TryGetActiveSound(BurningSoundSlot, out ActiveSound burningSound) && burningSound.IsPlaying)
		{
			burningSound.Position = player.Center;
		}
		if (burningSound != null)
		{
			if (SoundWarningLevel <= 1f)
			{
				burningSound?.Stop();
			}
			else if (SoundWarningLevel <= 2f)
			{
				burningSound.Volume = SoundWarningLevel - 1f;
			}
		}
		int guardianAmt = 0;
		bool attackerAlive = false;
		bool defenderAlive = false;
		bool healerAlive = false;
		if (CalamityGlobalNPC.holyBossAttacker != -1 && Main.npc[CalamityGlobalNPC.holyBossAttacker].active)
		{
			guardianAmt++;
			attackerAlive = true;
		}
		if (CalamityGlobalNPC.holyBossDefender != -1 && Main.npc[CalamityGlobalNPC.holyBossDefender].active)
		{
			guardianAmt++;
			defenderAlive = true;
		}
		if (CalamityGlobalNPC.holyBossHealer != -1 && Main.npc[CalamityGlobalNPC.holyBossHealer].active)
		{
			guardianAmt++;
			healerAlive = true;
		}
		if (attackerAlive && base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
		}
		if (!attackerAlive && base.NPC.localAI[0] > 0f && base.NPC.localAI[0] < 120f)
		{
			if (base.NPC.localAI[0] == 1f)
			{
				SoundEngine.PlaySound(in SoundID.Item105, base.NPC.Center);
			}
			base.NPC.localAI[0]++;
		}
		if (defenderAlive && base.NPC.localAI[3] == 0f)
		{
			base.NPC.localAI[3] = 1f;
		}
		if (!defenderAlive && base.NPC.localAI[3] > 0f && base.NPC.localAI[3] < 120f)
		{
			if (base.NPC.localAI[3] == 1f)
			{
				SoundEngine.PlaySound(in SoundID.Item105, base.NPC.Center);
			}
			base.NPC.localAI[3]++;
		}
		if (guardianAmt > 0)
		{
			normalAttackRate = ignoreGuardianAmt;
			if (!normalAttackRate)
			{
				switch (guardianAmt)
				{
				case 1:
					attackRateMult = 1.15;
					break;
				case 2:
					attackRateMult = 1.3;
					break;
				case 3:
					attackRateMult = 1.45;
					break;
				}
			}
		}
		base.NPC.chaseable = normalAttackRate;
		if (CalamityServerConfig.Instance.BossesStopWeather)
		{
			CalamityWorld.StopRain();
		}
		if (biomeType == 0)
		{
			if (isHell)
			{
				biomeType = 2;
			}
			else if (isHoly)
			{
				biomeType = 1;
			}
		}
		if (Dying)
		{
			DoDeathAnimation();
			return;
		}
		if (base.NPC.life <= 1)
		{
			base.NPC.life = 1;
			DespawnSpecificProjectiles(everything: true);
			Dying = true;
			base.NPC.dontTakeDamage = true;
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			return;
		}
		if (defenderAlive)
		{
			base.NPC.defense = base.NPC.defDefense * 2;
		}
		else
		{
			base.NPC.defense = base.NPC.defDefense;
		}
		if (healerAlive)
		{
			float distanceFromHealer = Vector2.Distance(Main.npc[CalamityGlobalNPC.holyBossHealer].Center, base.NPC.Center);
			if (Main.npc[CalamityGlobalNPC.holyBossHealer].justHit || base.NPC.life == base.NPC.lifeMax)
			{
				healTimer = 0;
			}
			else
			{
				float healGateValue = (revenge ? 60f : 90f);
				healTimer++;
				if ((float)healTimer >= healGateValue)
				{
					SoundEngine.PlaySound(in SoundID.Item8, base.NPC.Center);
					int maxHealDustIterations = (int)distanceFromHealer;
					int maxDust = 100;
					int dustDivisor = maxHealDustIterations / maxDust;
					if (dustDivisor < 2)
					{
						dustDivisor = 2;
					}
					Vector2 dustLineStart = Main.npc[CalamityGlobalNPC.holyBossHealer].Center;
					Vector2 dustLineEnd = base.NPC.Center;
					Vector2 currentDustPos = default(Vector2);
					Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), 3.1415927410125732);
					Vector2 dustVelocityMult = default(Vector2);
					((Vector2)(ref dustVelocityMult))._002Ector(2.1f, 2f);
					Color dustColor = Main.hslToRgb(Main.rgbToHsl(new Color(255, 200, Main.DiscoB)).X, 1f, 0.5f);
					((Color)(ref dustColor)).A = byte.MaxValue;
					for (int i = 0; i < maxHealDustIterations; i++)
					{
						if (i % dustDivisor == 0)
						{
							currentDustPos = Vector2.Lerp(dustLineStart, dustLineEnd, (float)i / (float)maxHealDustIterations);
							int dust = Dust.NewDust(currentDustPos, 0, 0, 267, 0f, 0f, 0, dustColor);
							Main.dust[dust].position = currentDustPos;
							Main.dust[dust].velocity = spinningpoint.RotatedBy((float)Math.PI * 2f * (float)i / (float)maxHealDustIterations) * dustVelocityMult * (0.8f + Main.rand.NextFloat() * 0.4f) + base.NPC.velocity;
							Main.dust[dust].noGravity = true;
							Main.dust[dust].scale = 1f;
							Main.dust[dust].fadeIn = Main.rand.NextFloat() * 2f;
							Dust dust2 = DustExtensions.BetterCloneDust(dust);
							dust2.scale /= 2f;
							dust2.fadeIn /= 2f;
							dust2.color = new Color(255, 255, 255, 255);
						}
					}
					healTimer = 0;
					if (Main.netMode != 1)
					{
						int healAmt = base.NPC.lifeMax / 200;
						if (healAmt > base.NPC.lifeMax - base.NPC.life)
						{
							healAmt = base.NPC.lifeMax - base.NPC.life;
						}
						if (healAmt > 0)
						{
							base.NPC.life += healAmt;
							base.NPC.HealEffect(healAmt);
							base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
						}
					}
				}
			}
		}
		bool targetDead = false;
		if (!player.active || player.dead)
		{
			if (!player.active || player.dead)
			{
				base.NPC.TargetClosest(faceTarget: false);
				player = Main.player[base.NPC.target];
			}
			if (!player.active || player.dead)
			{
				targetDead = true;
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X += 0.2f;
				}
				else
				{
					base.NPC.velocity.X -= 0.2f;
				}
				base.NPC.velocity.Y -= 0.2f;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (base.NPC.localAI[1] != -1f)
		{
			if (bossLife == 0f && base.NPC.life > 0)
			{
				bossLife = base.NPC.lifeMax;
			}
			if (base.NPC.life > 0)
			{
				int guardianHealthThreshold = (int)((double)base.NPC.lifeMax * 0.66);
				if ((float)(base.NPC.life + guardianHealthThreshold) < bossLife)
				{
					bossLife = base.NPC.life;
					SoundEngine.PlaySound(in SoundID.Item74, base.NPC.Center);
					if (Main.netMode != 1)
					{
						int guardianRingAmt = 3;
						int guardianSpread = 360 / guardianRingAmt;
						int guardianDistance = 400;
						for (int j = 0; j < guardianRingAmt; j++)
						{
							int type = j switch
							{
								1 => ModContent.NPCType<ProvSpawnHealer>(), 
								0 => ModContent.NPCType<ProvSpawnDefense>(), 
								_ => ModContent.NPCType<ProvSpawnOffense>(), 
							};
							int spawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(j * guardianSpread) * (double)guardianDistance), (int)((double)base.NPC.Center.Y + Math.Cos(j * guardianSpread) * (double)guardianDistance), type, base.NPC.whoAmI, 0f, 0f, 0f, -1f);
							Main.npc[spawn].ai[0] = j * guardianSpread;
						}
					}
				}
			}
		}
		base.NPC.Calamity().DR = (((AIState == 2f || AIState == 5f || AIState == 7f) | spawnAnimation) ? cocoonDR : (delayAttacks ? MathHelper.Lerp(normalDR, cocoonDR, base.NPC.localAI[2] / attackDelayAfterCocoon) : normalDR));
		calamityGlobalNPC.CurrentlyIncreasingDefenseOrDR = (AIState == 2f || AIState == 5f || AIState == 7f) | spawnAnimation;
		bool predictiveShots = false;
		if (getFuckedAI || (AIState != 2f && AIState != 5f))
		{
			if (spawnAnimation)
			{
				base.NPC.velocity = Vector2.Zero;
			}
			else
			{
				bool laserPhaseSlow = AIState == 7f;
				if (Main.netMode != 1 && flightPath == 0)
				{
					if (base.NPC.Center.X < player.Center.X)
					{
						flightPath = 1;
						calamityGlobalNPC.newAI[0] = 0f;
						base.NPC.netUpdate = true;
					}
					else
					{
						flightPath = -1;
						calamityGlobalNPC.newAI[0] = 0f;
						base.NPC.netUpdate = true;
					}
				}
				if (revenge)
				{
					calamityGlobalNPC.newAI[0]++;
				}
				float changeDirectionThreshold = 800f;
				if (AIState == 3f || AIState == 4f)
				{
					changeDirectionThreshold += (death ? 240f : (revenge ? 180f : 120f));
				}
				if (Main.netMode != 1)
				{
					if (base.NPC.Center.X < player.Center.X && flightPath < 0 && distanceX > changeDirectionThreshold)
					{
						flightPath = 0;
						base.NPC.netUpdate = true;
					}
					if (base.NPC.Center.X > player.Center.X && flightPath > 0 && distanceX > changeDirectionThreshold)
					{
						flightPath = 0;
						base.NPC.netUpdate = true;
					}
				}
				if ((base.NPC.velocity.X > 0f && base.NPC.Center.X - player.Center.X > 0f && player.velocity.X > 0f) || (base.NPC.velocity.X < 0f && base.NPC.Center.X - player.Center.X < 0f && player.velocity.X < 0f))
				{
					predictiveShots = true;
				}
				float speedIncreaseTimer = (fullPowerAI ? 75f : (death ? 120f : 150f));
				bool increaseSpeed = calamityGlobalNPC.newAI[0] > speedIncreaseTimer;
				float accelerationBoost = (death ? (0.3f * (1f - lifeRatio)) : (0.2f * (1f - lifeRatio)));
				float velocityBoost = (death ? (6f * (1f - lifeRatio)) : (4f * (1f - lifeRatio)));
				float acceleration = (expertMode ? 1.1f : 1.05f) + accelerationBoost;
				float velocity = (expertMode ? 16f : 15f) + velocityBoost;
				if (fullPowerAI)
				{
					acceleration = 1.5f;
					velocity = 25f;
				}
				if (laserPhaseSlow)
				{
					acceleration *= (getFuckedAI ? 0.6f : 0.4f);
					velocity *= (getFuckedAI ? 0.6f : 0.4f);
				}
				else if (increaseSpeed)
				{
					velocity += (calamityGlobalNPC.newAI[0] - speedIncreaseTimer) * 0.04f;
					if (velocity > 30f)
					{
						velocity = 30f;
					}
				}
				if (Main.getGoodWorld)
				{
					velocity *= 1.2f;
					acceleration *= 1.2f;
				}
				if (!targetDead)
				{
					base.NPC.velocity.X += (float)flightPath * acceleration;
					if (base.NPC.velocity.X > velocity)
					{
						base.NPC.velocity.X = velocity;
					}
					if (base.NPC.velocity.X < 0f - velocity)
					{
						base.NPC.velocity.X = 0f - velocity;
					}
					float num = player.position.Y - (base.NPC.position.Y + (float)base.NPC.height);
					if (num < (laserPhaseSlow ? 150f : 200f))
					{
						base.NPC.velocity.Y -= (Main.getGoodWorld ? 0.4f : 0.2f);
					}
					if (num > (laserPhaseSlow ? 200f : 250f))
					{
						base.NPC.velocity.Y += (Main.getGoodWorld ? 0.4f : 0.2f);
					}
					float speedCap = (laserPhaseSlow ? 2f : 6f);
					if (Main.getGoodWorld)
					{
						speedCap *= 1.5f;
					}
					if (base.NPC.velocity.Y > speedCap)
					{
						base.NPC.velocity.Y = speedCap;
					}
					if (base.NPC.velocity.Y < 0f - speedCap)
					{
						base.NPC.velocity.Y = 0f - speedCap;
					}
				}
			}
		}
		switch ((int)AIState)
		{
		case -1:
		{
			if (Main.netMode == 1)
			{
				break;
			}
			phaseChange++;
			if (phaseChange > 14)
			{
				phaseChange = 0;
			}
			int phase3 = 0;
			bool useLaser = (phase2 && biomeType == 1) || BossRushEvent.BossRushActive;
			bool useCrystal = (phase2 && biomeType == 2) || BossRushEvent.BossRushActive;
			if (death)
			{
				switch (phaseChange)
				{
				case 0:
					phase3 = 3;
					break;
				case 1:
					phase3 = 5;
					break;
				case 2:
					phase3 = 0;
					break;
				case 3:
					phase3 = ((useCrystal | fullPowerAI) ? 6 : 3);
					break;
				case 4:
					phase3 = (useCrystal ? 3 : 2);
					break;
				case 5:
					phase3 = ((!useCrystal) ? 1 : 2);
					break;
				case 6:
					phase3 = ((useLaser | fullPowerAI) ? 7 : 4);
					break;
				case 7:
					phase3 = ((useLaser | fullPowerAI) ? 4 : 3);
					break;
				case 8:
					phase3 = ((useLaser | fullPowerAI) ? 3 : 5);
					break;
				case 9:
					phase3 = 0;
					break;
				case 10:
					phase3 = ((useCrystal | fullPowerAI) ? 6 : 2);
					break;
				case 11:
					phase3 = (fullPowerAI ? 2 : 3);
					break;
				case 12:
					phase3 = ((useLaser | fullPowerAI) ? 7 : 4);
					break;
				case 13:
					phase3 = 5;
					break;
				case 14:
					phase3 = ((useLaser | fullPowerAI) ? 4 : 0);
					break;
				}
			}
			else
			{
				switch (phaseChange)
				{
				case 0:
					phase3 = 0;
					break;
				case 1:
					phase3 = ((!useLaser) ? 1 : 7);
					break;
				case 2:
					phase3 = 4;
					break;
				case 3:
					phase3 = 3;
					break;
				case 4:
					phase3 = 5;
					break;
				case 5:
					phase3 = (useCrystal ? 6 : 4);
					break;
				case 6:
					phase3 = 1;
					break;
				case 7:
					phase3 = 0;
					break;
				case 8:
					phase3 = 3;
					break;
				case 9:
					phase3 = 2;
					break;
				case 10:
					phase3 = 4;
					break;
				case 11:
					phase3 = (useLaser ? 7 : 0);
					break;
				case 12:
					phase3 = 1;
					break;
				case 13:
					phase3 = 3;
					break;
				case 14:
					phase3 = 5;
					break;
				}
			}
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 5600f)
			{
				phase3 = 0;
			}
			if (phase3 == 7)
			{
				base.NPC.localAI[2] = 0f;
			}
			AIState = phase3;
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
			base.NPC.ai[3] = 0f;
			calamityGlobalNPC.newAI[1] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			break;
		}
		case 0:
			if (spawnAnimation)
			{
				CalamityUtils.AddScreenshakeAt(base.NPC.Center, MathHelper.Lerp(0f, 0.25f, calamityGlobalNPC.newAI[3] / spawnAnimationTime), 2000f);
				base.NPC.dontTakeDamage = true;
				if (calamityGlobalNPC.newAI[3] == spawnAnimationTime - 1f)
				{
					base.NPC.dontTakeDamage = false;
					CalamityUtils.AddScreenshakeAt(base.NPC.Center, 8f, 2000f);
					SoundEngine.PlaySound(in HolyRaySound, base.NPC.Center);
					bool photos = CalamityClientConfig.Instance.Photosensitivity;
					for (int n = 0; n < 20; n++)
					{
						GeneralParticleHandler.SpawnParticle(new FlameParticle(base.NPC.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(150f), 0f), 6.2831854820251465), 40, Main.rand.NextFloat(1f, 1.6f), Main.rand.NextFloat(2f, 5f), hiColor * (photos ? 0.5f : 1f), loColor * (photos ? 0.5f : 1f))
						{
							Velocity = Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(3f, 19f), 0f), 6.2831854820251465)
						});
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(12f, 40f), 0f), 6.2831854820251465), loColor * (photos ? 0.5f : 1f), 60, Main.rand.NextFloat(2.5f, 5.5f), 2f, Main.rand.NextFloat(-0.05f, 0.05f), glowing: true));
					}
					CalamityUtils.AddScreenshakeAt(base.NPC.Center, 10f, 2000f);
					Color hColor = ProvUtils.GetProjectileColor(255) * (photos ? 0.5f : 1f);
					Color lColor = ProvUtils.GetProjectileColor(0, Outline: true) * (photos ? 0.5f : 1f);
					for (int num2 = 0; num2 < 20; num2++)
					{
						GeneralParticleHandler.SpawnParticle(new FlameParticle(base.NPC.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(150f), 0f), 6.2831854820251465), 40, Main.rand.NextFloat(0.5f, 0.75f), Main.rand.NextFloat(1f, 2.5f), hColor, lColor)
						{
							Velocity = Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(3f, 19f), 0f), 6.2831854820251465)
						});
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(12f, 40f), 0f), 6.2831854820251465), loColor * (photos ? 0.5f : 1f), 60, Main.rand.NextFloat(0.75f, 1.75f), 1f, Main.rand.NextFloat(-0.05f, 0.05f), glowing: true));
					}
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 1f, 0.1f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					for (float i2 = 0f; i2 < 2.8f; i2 += 0.35f)
					{
						GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One * i2, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.35f, 35, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
						GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hColor, "CalamityMod/Particles/ShatteredExplosion", Vector2.One * i2, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.475f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					}
				}
				float sc = calamityGlobalNPC.newAI[3] / spawnAnimationTime;
				if (calamityGlobalNPC.newAI[3] > 10f && calamityGlobalNPC.newAI[3] < spawnAnimationTime)
				{
					Vector2 destination = base.NPC.Center + (Vector2)((base.NPC.localAI[1] != 0f) ? new Vector2(0f, 40f) : Vector2.Zero);
					if (calamityGlobalNPC.newAI[3] % (CalamityClientConfig.Instance.Photosensitivity ? 15f : 10f) == 0f)
					{
						GeneralParticleHandler.SpawnParticle(new CustomPulse(destination, Vector2.Zero, Color.Lerp(new Color(25, 25, 25, 0), medColor, sc), "CalamityMod/Particles/SoftRoundExplosion", new Vector2(1.5f, 1f), Main.rand.NextBool() ? 0f : ((float)Math.PI), sc * 0.5f, sc * 0.1f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
						SoundEngine.PlaySound(SoundID.Item74 with
						{
							MaxInstances = 10
						}.WithVolumeScale(calamityGlobalNPC.newAI[3] / spawnAnimationTime).WithPitchOffset(-1f + calamityGlobalNPC.newAI[3] / spawnAnimationTime), base.NPC.Center);
					}
					Vector2 startPos = destination + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(80f, 300f) * (sc * 1.6f), 0f), Main.rand.NextFloat((float)Math.PI * 2f)) * new Vector2(1.5f, 1f);
					GeneralParticleHandler.SpawnParticle(new SparkParticle(startPos, startPos.DirectionTo(destination) * (startPos.Distance(destination) / 10f), affectedByGravity: false, 10, Main.rand.NextFloat(0.2f, 0.5f) * (sc * 2f), medColor));
				}
				if (fullPowerAI && calamityGlobalNPC.newAI[3] % 9f == 0f && Main.netMode != 1)
				{
					int healAmt2 = base.NPC.lifeMax / 400;
					if (healAmt2 > base.NPC.lifeMax - base.NPC.life)
					{
						healAmt2 = base.NPC.lifeMax - base.NPC.life;
					}
					if (healAmt2 > 0)
					{
						base.NPC.life += healAmt2;
						base.NPC.HealEffect(healAmt2);
						base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
					}
				}
				calamityGlobalNPC.newAI[3]++;
				if (fullPowerAI && calamityGlobalNPC.newAI[3] >= spawnAnimationTime)
				{
					calamityGlobalNPC.newAI[3]++;
				}
				break;
			}
			if (delayAttacks)
			{
				base.NPC.localAI[2]--;
				break;
			}
			if (distanceX > distanceNeededToShoot && base.NPC.position.Y < player.position.Y)
			{
				base.NPC.ai[3]++;
				int shootBoost2 = (death ? ((int)Math.Round(5f * (1f - lifeRatio))) : ((int)Math.Round(4f * (1f - lifeRatio))));
				int projectileShootGateValue2 = (expertMode ? 24 : 26) - shootBoost2;
				projectileShootGateValue2 = (int)((double)projectileShootGateValue2 * attackRateMult);
				if (base.NPC.ai[3] >= (float)projectileShootGateValue2)
				{
					base.NPC.ai[3] = -projectileShootGateValue2;
				}
				if (base.NPC.ai[3] == 0f && Main.netMode != 1)
				{
					Vector2 projectileFirePosition = default(Vector2);
					((Vector2)(ref projectileFirePosition))._002Ector(base.NPC.Center.X + base.NPC.velocity.SafeNormalize(Vector2.UnitX).X * 120f, base.NPC.Center.Y);
					float velocityBoost2 = (death ? (4f * (1f - lifeRatio)) : (2.5f * (1f - lifeRatio)));
					float projSpeed = (revenge ? 12f : (expertMode ? 10.5f : 9f)) + velocityBoost2;
					Vector2 predictionAmount = player.velocity * 100f;
					Vector2 projectileVelocity = (player.Center + (predictiveShots ? predictionAmount : Vector2.Zero) - projectileFirePosition).SafeNormalize(Vector2.UnitY) * projSpeed * 0.1f;
					Vector2 explodePosition = (predictiveShots ? (player.position + predictionAmount) : player.position);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileFirePosition, projectileVelocity, ModContent.ProjectileType<HolyBlast>(), HolyBlastDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, explodePosition.X, explodePosition.Y);
				}
			}
			else if (base.NPC.ai[3] < 0f)
			{
				base.NPC.ai[3]++;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= phaseTime)
			{
				AIState = -1f;
				base.NPC.TargetClosest();
			}
			break;
		case 1:
			if (delayAttacks)
			{
				base.NPC.localAI[2]--;
				break;
			}
			if (Main.netMode != 1)
			{
				base.NPC.ai[3]++;
				int shootBoost4 = (death ? ((int)Math.Round(6f * (1f - lifeRatio))) : ((int)Math.Round(5f * (1f - lifeRatio))));
				int projectileShootGateValue4 = (expertMode ? 36 : 39) - shootBoost4;
				projectileShootGateValue4 = (int)((double)projectileShootGateValue4 * attackRateMult);
				if (base.NPC.ai[3] >= (float)projectileShootGateValue4)
				{
					base.NPC.ai[3] = 0f;
					Vector2 shootFrom2 = default(Vector2);
					((Vector2)(ref shootFrom2))._002Ector(base.NPC.Center.X, base.NPC.position.Y + (float)base.NPC.height - 64f * base.NPC.scale);
					float projectileVelocityY2 = base.NPC.velocity.Y;
					if (projectileVelocityY2 < 0f)
					{
						projectileVelocityY2 = 0f;
					}
					projectileVelocityY2 += (expertMode ? 4f : 3f);
					if (fullPowerAI)
					{
						projectileVelocityY2 *= 2f;
					}
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom2.X, shootFrom2.Y, base.NPC.velocity.X * 0.25f, projectileVelocityY2, ModContent.ProjectileType<HolyFire>(), FireDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer);
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= phaseTime)
			{
				AIState = -1f;
				base.NPC.TargetClosest();
			}
			break;
		case 2:
		{
			Vector2 fireSparklesFrom = fireFrom + new Vector2(0f, -30f);
			if (!targetDead && !getFuckedAI)
			{
				if (((Vector2)(ref base.NPC.velocity)).Length() <= 2f)
				{
					base.NPC.velocity = Vector2.Zero;
				}
				if (((Vector2)(ref base.NPC.velocity)).Length() > 2f)
				{
					NPC nPC2 = base.NPC;
					nPC2.velocity *= 0.9f;
					break;
				}
			}
			float divisor = (expertMode ? 2f : 3f) + (float)Math.Floor(3f * lifeRatio) + ((attackRateMult > 1.0) ? ((float)Math.Ceiling(attackRateMult * 1.6)) : 0f);
			int totalFlameProjectiles = 36;
			int chains = 4;
			float interval = (float)(totalFlameProjectiles / chains) * divisor;
			double num3 = Math.Floor(base.NPC.ai[3] / interval);
			int healingStarChance = (revenge ? 8 : (expertMode ? 6 : 4));
			if (num3 % 2.0 == 0.0)
			{
				if (base.NPC.ai[3] % divisor == 0f)
				{
					SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, base.NPC.Center);
					bool num4 = calamityGlobalNPC.newAI[1] % 2f == 0f;
					double radians2 = (float)Math.PI * 2f / (float)chains;
					double angleA = radians2 * 0.5;
					double angleB = (double)MathHelper.ToRadians(90f) - angleA;
					float velocityX = (float)((double)cocoonProjVelocity * Math.Sin(angleA) / Math.Sin(angleB));
					Vector2 spinningPoint2 = (num4 ? new Vector2(0f, 0f - cocoonProjVelocity) : new Vector2(0f - velocityX, 0f - cocoonProjVelocity));
					for (int num5 = 0; num5 < chains; num5++)
					{
						Vector2 vector3 = spinningPoint2.RotatedBy(radians2 * (double)num5 + (double)MathHelper.ToRadians(base.NPC.ai[2]));
						if (Main.rand.NextBool(healingStarChance) && !death)
						{
							if (Main.netMode != 1)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireSparklesFrom, vector3, ModContent.ProjectileType<HolyLight>(), 0, 0f, Main.myPlayer, 0f, StarHeal);
							}
						}
						else if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireSparklesFrom, vector3, ModContent.ProjectileType<HolyBurnOrb>(), StarDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer);
						}
					}
					base.NPC.ai[2] += 10f;
				}
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			else
			{
				base.NPC.ai[2] = 0f;
				totalFlameProjectiles = 16;
				if (base.NPC.ai[3] % (divisor * (float)totalFlameProjectiles) == 0f)
				{
					calamityGlobalNPC.newAI[1]++;
					double radians3 = (float)Math.PI * 2f / (float)totalFlameProjectiles;
					SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, base.NPC.Center);
					double angleA2 = radians3 * 0.5;
					double angleB2 = (double)MathHelper.ToRadians(90f) - angleA2;
					float velocityX2 = (float)((double)cocoonProjVelocity * Math.Sin(angleA2) / Math.Sin(angleB2));
					Vector2 spinningPoint3 = ((base.NPC.ai[3] % (divisor * (float)totalFlameProjectiles * 2f) == 0f) ? new Vector2(0f - velocityX2, 0f - cocoonProjVelocity) : new Vector2(0f, 0f - cocoonProjVelocity));
					for (int num6 = 0; num6 < totalFlameProjectiles; num6++)
					{
						Vector2 vector4 = spinningPoint3.RotatedBy(radians3 * (double)num6);
						if (Main.rand.NextBool(healingStarChance) && !death)
						{
							if (Main.netMode != 1)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireSparklesFrom, vector4, ModContent.ProjectileType<HolyLight>(), 0, 0f, Main.myPlayer, 0f, StarHeal);
							}
						}
						else if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireSparklesFrom, vector4, ModContent.ProjectileType<HolyBurnOrb>(), StarDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer);
						}
					}
				}
			}
			if ((base.NPC.ai[3] % 60f == 0f) & expertMode)
			{
				List<int> targets = new List<int>();
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player plr = enumerator.Current;
					if (!plr.dead)
					{
						targets.Add(plr.whoAmI);
					}
					if (targets.Count > 4)
					{
						break;
					}
				}
				foreach (int t in targets)
				{
					Vector2 velocity4 = Vector2.Normalize(Main.player[t].Center - fireSparklesFrom) * cocoonProjVelocity * 1.5f;
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireSparklesFrom, velocity4, ModContent.ProjectileType<HolyBurnOrb>(), StarDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer);
					}
					Color dustColor2 = Main.hslToRgb(Main.rgbToHsl((Color)(fullPowerAI ? new Color(100, 200, 250) : Color.Orange)).X, 1f, 0.5f);
					((Color)(ref dustColor2)).A = byte.MaxValue;
					int maxDust2 = 3;
					for (int num7 = 0; num7 < maxDust2; num7++)
					{
						int dust5 = Dust.NewDust(fireFrom, 0, 0, 267, 0f, 0f, 0, dustColor2);
						Main.dust[dust5].position = fireFrom;
						Main.dust[dust5].velocity = velocity4 * cocoonProjVelocity * 2f;
						Main.dust[dust5].noGravity = true;
						Main.dust[dust5].scale = 3f;
						Main.dust[dust5].fadeIn = Main.rand.NextFloat() * 2f;
						Dust dust6 = DustExtensions.BetterCloneDust(dust5);
						dust6.scale /= 2f;
						dust6.fadeIn /= 2f;
						dust6.color = new Color(255, 255, 255, 255);
					}
				}
			}
			if (base.NPC.ai[3] == 0f)
			{
				DespawnSpecificProjectiles();
			}
			base.NPC.ai[3]++;
			if (base.NPC.ai[3] >= phaseTime * 1.5f && !text)
			{
				text = true;
				Color messageColor = Color.Orange;
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.ProfanedBossText", messageColor);
			}
			if (!(base.NPC.ai[3] >= phaseTime * 2f))
			{
				break;
			}
			if (!Main.dedServ)
			{
				Player player2 = Main.LocalPlayer;
				bool inLiquid = player2.Calamity().countsAsAnyWet && !player2.lavaWet;
				if (!player2.dead && player2.active && Vector2.Distance(player2.Center, base.NPC.Center) < 2800f && !inLiquid)
				{
					SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, player2.Center);
					player2.AddBuff(ModContent.BuffType<IcarusFolly>(), 3000);
					for (int num8 = 0; num8 < 40; num8++)
					{
						int icarusFollyDust = Dust.NewDust(new Vector2(player2.position.X, player2.position.Y), player2.width, player2.height, dustType, 0f, 0f, 100, default(Color), 2f);
						Dust obj = Main.dust[icarusFollyDust];
						obj.velocity *= 3f;
						Main.dust[icarusFollyDust].noGravity = true;
						if (Main.rand.NextBool())
						{
							Main.dust[icarusFollyDust].scale = 0.5f;
							Main.dust[icarusFollyDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int num9 = 0; num9 < 60; num9++)
					{
						int icarusFollyDust2 = Dust.NewDust(new Vector2(player2.position.X, player2.position.Y), player2.width, player2.height, dustType, 0f, 0f, 100, default(Color), 3f);
						Main.dust[icarusFollyDust2].noGravity = true;
						Dust obj2 = Main.dust[icarusFollyDust2];
						obj2.velocity *= 5f;
						icarusFollyDust2 = Dust.NewDust(new Vector2(player2.position.X, player2.position.Y), player2.width, player2.height, dustType, 0f, 0f, 100, default(Color), 2f);
						Dust obj3 = Main.dust[icarusFollyDust2];
						obj3.velocity *= 2f;
						Main.dust[icarusFollyDust2].noGravity = true;
					}
				}
			}
			text = false;
			AIState = -1f;
			base.NPC.localAI[2] = attackDelayAfterCocoon;
			base.NPC.TargetClosest();
			break;
		}
		case 3:
			if (delayAttacks)
			{
				base.NPC.localAI[2]--;
				break;
			}
			if (distanceX > distanceNeededToShoot && base.NPC.position.Y < player.position.Y)
			{
				base.NPC.ai[3]++;
				int shootBoost3 = (death ? ((int)Math.Round(5f * (1f - lifeRatio))) : ((int)Math.Round(4f * (1f - lifeRatio))));
				int projectileShootGateValue3 = (expertMode ? 24 : 26) - shootBoost3;
				projectileShootGateValue3 = (int)((double)projectileShootGateValue3 * attackRateMult);
				if (base.NPC.ai[3] >= (float)projectileShootGateValue3)
				{
					base.NPC.ai[3] = -projectileShootGateValue3;
				}
				if (base.NPC.ai[3] == 0f && Main.netMode != 1)
				{
					Vector2 projectileFirePosition2 = default(Vector2);
					((Vector2)(ref projectileFirePosition2))._002Ector(base.NPC.Center.X + base.NPC.velocity.SafeNormalize(Vector2.UnitX).X * 120f, base.NPC.Center.Y);
					float velocityBoost3 = (death ? (4f * (1f - lifeRatio)) : (2.5f * (1f - lifeRatio)));
					float projSpeed2 = (revenge ? 12f : (expertMode ? 10.5f : 9f)) + velocityBoost3;
					Vector2 predictionAmount2 = player.velocity * 100f;
					Vector2 projectileVelocity2 = (player.Center + (predictiveShots ? predictionAmount2 : Vector2.Zero) - projectileFirePosition2).SafeNormalize(Vector2.UnitY) * projSpeed2 * 0.1f;
					Vector2 explodePosition2 = (predictiveShots ? (player.position + predictionAmount2) : player.position);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileFirePosition2, projectileVelocity2, ModContent.ProjectileType<MoltenBlast>(), MoltenBlastDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, explodePosition2.X, explodePosition2.Y);
				}
			}
			else if (base.NPC.ai[3] < 0f)
			{
				base.NPC.ai[3]++;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= phaseTime)
			{
				AIState = -1f;
				base.NPC.TargetClosest();
			}
			break;
		case 4:
			if (delayAttacks)
			{
				base.NPC.localAI[2]--;
				break;
			}
			if (Main.netMode != 1)
			{
				base.NPC.ai[3]++;
				int shootBoost = (death ? ((int)Math.Round(12f * (1f - lifeRatio))) : ((int)Math.Round(10f * (1f - lifeRatio))));
				int projectileShootGateValue = (expertMode ? 73 : 77) - shootBoost;
				projectileShootGateValue = (int)((double)projectileShootGateValue * attackRateMult);
				if (base.NPC.ai[3] >= (float)projectileShootGateValue)
				{
					base.NPC.ai[3] = 0f;
					Vector2 shootFrom = default(Vector2);
					((Vector2)(ref shootFrom))._002Ector(base.NPC.Center.X, base.NPC.position.Y + (float)base.NPC.height - 14f * base.NPC.scale);
					float projectileVelocityY = base.NPC.velocity.Y;
					if (projectileVelocityY < 0f)
					{
						projectileVelocityY = 0f;
					}
					projectileVelocityY += (expertMode ? 4f : 3f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom.X, shootFrom.Y, base.NPC.velocity.X * 0.25f, projectileVelocityY, ModContent.ProjectileType<HolyBomb>(), FireSentryDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer);
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= phaseTime)
			{
				AIState = -1f;
				base.NPC.TargetClosest();
			}
			break;
		case 5:
			if (!targetDead && !getFuckedAI)
			{
				if (((Vector2)(ref base.NPC.velocity)).Length() <= 2f)
				{
					base.NPC.velocity = Vector2.Zero;
				}
				if (((Vector2)(ref base.NPC.velocity)).Length() > 2f)
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.9f;
					break;
				}
			}
			if (base.NPC.ai[1] == 0f)
			{
				DespawnSpecificProjectiles();
			}
			base.NPC.ai[2] += spearRate;
			if (base.NPC.ai[2] >= (float)((double)baseSpearRate * attackRateMult))
			{
				base.NPC.ai[2] = 0f;
				SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballShot, fireFrom);
				int projectileType = ModContent.ProjectileType<HolySpear>();
				int totalDustPerSpear = 15;
				if (calamityGlobalNPC.newAI[2] % 2f == 0f)
				{
					int totalSpearProjectiles = 12;
					double radians = (float)Math.PI * 2f / (float)totalSpearProjectiles;
					Vector2 spinningPoint = Vector2.Normalize(new Vector2(0f - calamityGlobalNPC.newAI[1], 0f - cocoonProjVelocity));
					for (int k = 0; k < totalSpearProjectiles; k++)
					{
						Vector2 vector2 = spinningPoint.RotatedBy(radians * (double)k) * cocoonProjVelocity;
						for (int l = 0; l < totalDustPerSpear; l++)
						{
							int dust3 = Dust.NewDust(fireFrom, 30, 30, dustType, vector2.X, vector2.Y);
							Main.dust[dust3].noGravity = true;
						}
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom, vector2, projectileType, SpearDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer);
							if (Main.getGoodWorld)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom, -vector2, projectileType, SpearDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer);
							}
						}
					}
					if (spearRateIncrease > 1f)
					{
						spearRateIncrease = 1f;
					}
					float radialOffset = MathHelper.Lerp(0.2f, 0.4f, spearRateIncrease);
					calamityGlobalNPC.newAI[1] += radialOffset;
				}
				calamityGlobalNPC.newAI[2]++;
				cocoonProjVelocity = (death ? 14f : (revenge ? 13f : (expertMode ? 12f : 10f)));
				Vector2 velocity3 = Vector2.Normalize(player.Center - fireFrom) * cocoonProjVelocity;
				for (int m = 0; m < totalDustPerSpear; m++)
				{
					int dust4 = Dust.NewDust(fireFrom, 30, 30, dustType, velocity3.X, velocity3.Y);
					Main.dust[dust4].noGravity = true;
				}
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom, velocity3, projectileType, SpearDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, 1f);
					if (Main.getGoodWorld)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom, -velocity3, projectileType, SpearDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, 1f);
					}
				}
			}
			base.NPC.ai[3]++;
			if (base.NPC.ai[3] >= phaseTime)
			{
				AIState = -1f;
				base.NPC.localAI[2] = attackDelayAfterCocoon;
				base.NPC.TargetClosest();
			}
			break;
		case 6:
			if (!targetDead && !getFuckedAI)
			{
				NPC nPC3 = base.NPC;
				nPC3.velocity *= 0.9f;
			}
			base.NPC.ai[1]++;
			if (!(base.NPC.ai[1] >= crystalPhaseTime))
			{
				break;
			}
			if (base.NPC.ai[1] == crystalPhaseTime)
			{
				Vector2 crystalSpawnPos = default(Vector2);
				((Vector2)(ref crystalSpawnPos))._002Ector(player.Center.X, player.Center.Y - 360f);
				int maxHealDustIterations2 = (int)Vector2.Distance(crystalSpawnPos, base.NPC.Center);
				int maxDust3 = 100;
				int dustDivisor2 = maxHealDustIterations2 / maxDust3;
				if (dustDivisor2 < 2)
				{
					dustDivisor2 = 2;
				}
				Vector2 dustLineStart2 = default(Vector2);
				((Vector2)(ref dustLineStart2))._002Ector(base.NPC.Center.X, base.NPC.Center.Y + 64f * base.NPC.scale);
				Vector2 dustLineEnd2 = crystalSpawnPos;
				Vector2 currentDustPos2 = default(Vector2);
				Vector2 spinningpoint2 = Utils.RotatedByRandom(new Vector2(0f, -3f), 3.1415927410125732);
				Vector2 dustVelocityMult2 = default(Vector2);
				((Vector2)(ref dustVelocityMult2))._002Ector(2.1f, 2f);
				int dustSpawned = 0;
				int maxDustLines = 3;
				int blue = Main.DiscoB;
				for (int num10 = 0; num10 < maxDustLines; num10++)
				{
					for (int num11 = 0; num11 < maxHealDustIterations2; num11++)
					{
						if (num11 % dustDivisor2 == 0)
						{
							currentDustPos2 = Vector2.Lerp(dustLineStart2, dustLineEnd2, (float)num11 / (float)maxHealDustIterations2);
							Color dustColor3 = Main.hslToRgb(Main.rgbToHsl(fullPowerAI ? new Color(100, 200, 250) : new Color(255, 200, Math.Abs(Math.Abs(blue) - (int)((float)dustSpawned * 2.55f)))).X, 1f, 0.5f);
							((Color)(ref dustColor3)).A = byte.MaxValue;
							int dust7 = Dust.NewDust(currentDustPos2, 0, 0, 267, 0f, 0f, 0, dustColor3);
							Main.dust[dust7].position = currentDustPos2 + Utils.RotatedByRandom(new Vector2(32f, 32f), 6.2831854820251465) * (float)num10;
							Main.dust[dust7].velocity = spinningpoint2.RotatedBy((float)Math.PI * 2f * (float)num11 / (float)maxHealDustIterations2) * dustVelocityMult2 * (0.8f + Main.rand.NextFloat() * 0.4f);
							Main.dust[dust7].noGravity = true;
							Main.dust[dust7].scale = 1f + (float)num10;
							Main.dust[dust7].fadeIn = Main.rand.NextFloat() * 2f;
							Dust dust8 = DustExtensions.BetterCloneDust(dust7);
							dust8.scale /= 2f;
							dust8.fadeIn /= 2f;
							dust8.color = new Color(255, 255, 255, 255);
							dustSpawned++;
						}
					}
					if (!fullPowerAI)
					{
						blue -= 255 / (maxDustLines - 1);
					}
				}
				int totalDust = 36;
				int circleDustSpawned = 0;
				for (int num12 = 0; num12 < totalDust; num12++)
				{
					Vector2 val = (Vector2.Normalize(base.NPC.velocity) * new Vector2(80f, 160f)).RotatedBy((float)(num12 - (totalDust / 2 - 1)) * ((float)Math.PI * 2f) / (float)totalDust) + dustLineEnd2;
					Vector2 dustVelocity = val - dustLineEnd2;
					Color dustColor4 = Main.hslToRgb(Main.rgbToHsl(fullPowerAI ? new Color(100, 200, 250) : new Color(255, 200, Math.Abs(Math.Abs(blue) - (int)((float)circleDustSpawned * 7.08f)))).X, 1f, 0.5f);
					((Color)(ref dustColor4)).A = byte.MaxValue;
					int dust9 = Dust.NewDust(val + dustVelocity, 0, 0, 267, dustVelocity.X, dustVelocity.Y, 0, dustColor4, 1.4f);
					Main.dust[dust9].noGravity = true;
					Main.dust[dust9].noLight = true;
					Main.dust[dust9].velocity = dustVelocity * 0.33f;
					circleDustSpawned++;
				}
				if (Main.netMode != 1)
				{
					float timeLeft = (fullPowerAI ? ((float)(getFuckedAI ? gfbCrystalTime : enragedCrystalTime)) : 0f);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), crystalSpawnPos, Vector2.Zero, ModContent.ProjectileType<ProvidenceCrystal>(), CrystalDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, lifeRatio, 0f, timeLeft);
				}
			}
			if (base.NPC.ai[1] >= crystalPhaseTime + (float)enragedCrystalTime || !fullPowerAI)
			{
				AIState = -1f;
				base.NPC.TargetClosest();
			}
			break;
		case 7:
		{
			Vector2 dustPosOffset = default(Vector2);
			((Vector2)(ref dustPosOffset))._002Ector(27f, 59f);
			float rotation = (fullPowerAI ? 445f : 460f) + (float)(guardianAmt * 5);
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] < 120f)
			{
				if (base.NPC.ai[2] >= 40f)
				{
					int extraDustAmt = 0;
					if (base.NPC.ai[2] >= 80f)
					{
						extraDustAmt = 1;
					}
					for (int d = 0; d < 1 + extraDustAmt; d++)
					{
						float scalar = 1.2f;
						if (d % 2 == 1)
						{
							scalar = 2.8f;
						}
						Vector2 dustPos = new Vector2(base.NPC.Center.X, base.NPC.Center.Y + 64f * base.NPC.scale) + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * dustPosOffset / 2f;
						int index = Dust.NewDust(dustPos - Vector2.One * 8f, 16, 16, dustType, base.NPC.velocity.X / 2f, base.NPC.velocity.Y / 2f);
						Main.dust[index].velocity = Vector2.Normalize(base.NPC.Center - dustPos) * 3.5f * (10f - (float)extraDustAmt * 2f) / 10f;
						Main.dust[index].noGravity = true;
						Main.dust[index].scale = scalar;
					}
				}
			}
			else if (base.NPC.ai[2] < (revenge ? 220f : 300f) && base.NPC.ai[2] == 120f)
			{
				if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
				{
					SoundEngine.PlaySound(in HolyRaySound, Main.LocalPlayer.Center);
				}
				if (Main.netMode != 1)
				{
					Vector2 velocity2 = player.Center - base.NPC.Center;
					((Vector2)(ref velocity2)).Normalize();
					float beamDirection = -1f;
					if (velocity2.X < 0f)
					{
						beamDirection = 1f;
					}
					velocity2 = velocity2.RotatedBy((0.0 - (double)beamDirection) * 6.2831854820251465 / 6.0);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y + 64f * base.NPC.scale, velocity2.X, velocity2.Y, ModContent.ProjectileType<ProvidenceHolyRay>(), RayDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, beamDirection * ((float)Math.PI * 2f) / rotation, base.NPC.whoAmI, 2f);
					if (revenge)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y + 64f * base.NPC.scale, 0f - velocity2.X, 0f - velocity2.Y, ModContent.ProjectileType<ProvidenceHolyRay>(), RayDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, (0f - beamDirection) * ((float)Math.PI * 2f) / rotation, base.NPC.whoAmI, 2f);
					}
					if (fullPowerAI && lifeRatio < 0.5f)
					{
						rotation *= 0.33f;
						velocity2 = velocity2.RotatedBy((0.0 - (double)beamDirection) * 6.2831854820251465 / 2.0);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y + 64f * base.NPC.scale, velocity2.X, velocity2.Y, ModContent.ProjectileType<ProvidenceHolyRay>(), RayDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, beamDirection * ((float)Math.PI * 2f) / rotation, base.NPC.whoAmI, 2f);
						if (revenge)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y + 64f * base.NPC.scale, 0f - velocity2.X, 0f - velocity2.Y, ModContent.ProjectileType<ProvidenceHolyRay>(), RayDamage.CalculateProvidenceDamage(), 0f, Main.myPlayer, (0f - beamDirection) * ((float)Math.PI * 2f) / rotation, base.NPC.whoAmI, 2f);
						}
					}
					base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (revenge ? 235f : 315f))
			{
				AIState = -1f;
				base.NPC.TargetClosest();
			}
			break;
		}
		}
	}

	public void DoDeathAnimation()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		AIState = 1f;
		useDefenseFrames = false;
		DeathAnimationTimer++;
		NPC nPC = base.NPC;
		nPC.velocity *= 0.9f;
		base.NPC.rotation = base.NPC.velocity.X * 0.004f;
		if ((float)DeathAnimationTimer == 1f)
		{
			if (!Main.dedServ && Main.LocalPlayer.WithinRange(base.NPC.Center, 4800f))
			{
				SoundStyle style = DeathAnimationSound with
				{
					Volume = 1.65f
				};
				SoundEngine.PlaySound(in style);
			}
			DespawnSpecificProjectiles();
			int laserType = ModContent.ProjectileType<ProvidenceHolyRay>();
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == laserType)
				{
					p.Kill();
				}
			}
		}
		if ((float)DeathAnimationTimer >= 370f)
		{
			base.NPC.Opacity *= 0.97f;
		}
		if ((float)DeathAnimationTimer == 92f)
		{
			CalamityUtils.AddScreenshakeAt(base.NPC.Center, 5f, 2000f);
			Color hiColor = ProvUtils.GetProjectileColor(255);
			Color loColor = ProvUtils.GetProjectileColor(0, Outline: true);
			for (int i = 0; i < 30; i++)
			{
				GeneralParticleHandler.SpawnParticle(new FlameParticle(base.NPC.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(150f), 0f), 6.2831854820251465), 40, Main.rand.NextFloat(0.5f, 0.75f), Main.rand.NextFloat(1f, 2.5f), hiColor, loColor)
				{
					Velocity = Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(3f, 19f), 0f), 6.2831854820251465)
				});
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(12f, 40f), 0f), 6.2831854820251465), loColor, 60, Main.rand.NextFloat(0.75f, 1.75f), 1f, Main.rand.NextFloat(-0.05f, 0.05f), glowing: true));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 1f, 0.1f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (float i2 = 0f; i2 < 3f; i2 += 0.25f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One * i2, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.25f, 35, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/ShatteredExplosion", Vector2.One * i2, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.175f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			SoundEngine.PlaySound(in HolyBlast.ImpactSound, base.NPC.Center);
		}
		if (Main.netMode != 1 && (float)DeathAnimationTimer == 310f)
		{
			for (int j = 0; j < 80; j++)
			{
				Vector2 sparkleVelocity = Main.rand.NextVector2Circular(23f, 23f);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, sparkleVelocity, ModContent.ProjectileType<MajesticSparkle>(), 0, 0f);
			}
		}
		if ((float)DeathAnimationTimer >= 92f)
		{
			CalamityUtils.AddScreenshakeAt(base.NPC.Center, MathHelper.Lerp(0f, 0.6f, ((float)DeathAnimationTimer - 92f) / 300f));
			int shootRate = (int)MathHelper.Lerp(12f, 5f, Utils.GetLerpValue(0f, 250f, DeathAnimationTimer, clamped: true));
			if ((float)(DeathAnimationTimer % shootRate) == (float)shootRate - 1f)
			{
				for (int k = 0; k < 3; k++)
				{
					Vector2 shootVelocity = Main.rand.NextVector2CircularEdge(13f, 13f) * Main.rand.NextFloat(1.4f, 2.3f);
					Projectile.NewProjectileDirect(base.NPC.GetSource_FromAI(), base.NPC.Center, shootVelocity, ModContent.ProjectileType<SwirlingFire>(), 0, 0f, 255, 0f, (int)base.NPC.localAI[1]).ai[2] = MathHelper.Lerp(0.5f, 2.5f, (float)DeathAnimationTimer / 300f);
				}
			}
		}
		if (Main.dedServ && (float)DeathAnimationTimer % 45f == 44f)
		{
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
		if ((float)DeathAnimationTimer >= 345f)
		{
			base.NPC.active = false;
			base.NPC.HitEffect();
			base.NPC.NPCLoot();
			base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
	}

	public float CalculateBurnIntensity(float attackDelayAfterCocoon = 1f)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		float distanceToTarget = Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center);
		float aiTimer = base.NPC.ai[3];
		bool fullPower = base.NPC.localAI[1] == -1f;
		float baseDistance = 2800f;
		float shorterFlameCocoonDistance = ((CalamityWorld.death | fullPower) ? 600f : (CalamityWorld.revenge ? 400f : (Main.expertMode ? 200f : 0f)));
		float shorterSpearCocoonDistance = ((CalamityWorld.death | fullPower) ? 1000f : (CalamityWorld.revenge ? 650f : (Main.expertMode ? 300f : 0f)));
		float shorterDistance = ((AIState == 2f) ? shorterFlameCocoonDistance : shorterSpearCocoonDistance);
		bool guardianAlive = false;
		if (CalamityGlobalNPC.holyBossAttacker != -1 && Main.npc[CalamityGlobalNPC.holyBossAttacker].active)
		{
			guardianAlive = true;
		}
		if (CalamityGlobalNPC.holyBossDefender != -1 && Main.npc[CalamityGlobalNPC.holyBossDefender].active)
		{
			guardianAlive = true;
		}
		if (CalamityGlobalNPC.holyBossHealer != -1 && Main.npc[CalamityGlobalNPC.holyBossHealer].active)
		{
			guardianAlive = true;
		}
		float maxDistance = baseDistance;
		float shorterDistanceFade = Utils.GetLerpValue(0f, 120f, aiTimer, clamped: true);
		if (!guardianAlive && base.NPC.localAI[1] < 1f)
		{
			maxDistance = baseDistance;
			if (AIState == 2f || AIState == 5f)
			{
				maxDistance -= shorterDistance * shorterDistanceFade;
			}
			else if (attackDelayAfterCocoon > 1f)
			{
				maxDistance -= shorterDistance * (base.NPC.localAI[2] / attackDelayAfterCocoon);
			}
		}
		float drawFireDistanceStart = maxDistance - 800f;
		float previousBorderEnd = borderRadius;
		return Utils.GetLerpValue(drawFireDistanceStart, borderRadius = MathHelper.Clamp(maxDistance, previousBorderEnd - 10f, previousBorderEnd + 10f), distanceToTarget, clamped: true);
	}

	private void DespawnSpecificProjectiles(bool everything = false)
	{
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile = Main.projectile[x];
			if (!projectile.active)
			{
				continue;
			}
			if (projectile.type == ModContent.ProjectileType<HolyFire2>() || projectile.type == ModContent.ProjectileType<HolyFlare>())
			{
				projectile.Kill();
			}
			else if (projectile.type == ModContent.ProjectileType<HolyBlast>() || projectile.type == ModContent.ProjectileType<HolyFire>())
			{
				projectile.active = false;
			}
			if (everything)
			{
				if (projectile.type == ModContent.ProjectileType<ProvidenceHolyRay>() || projectile.type == ModContent.ProjectileType<ProvidenceCrystal>() || projectile.type == ModContent.ProjectileType<ProvidenceCrystalShard>() || projectile.type == ModContent.ProjectileType<HolySpear>() || projectile.type == ModContent.ProjectileType<HolyBomb>() || projectile.type == ModContent.ProjectileType<MoltenBlob>() || projectile.type == ModContent.ProjectileType<HolyBurnOrb>() || projectile.type == ModContent.ProjectileType<HolyLight>())
				{
					projectile.Kill();
				}
				else if (projectile.type == ModContent.ProjectileType<MoltenBlast>())
				{
					projectile.active = false;
				}
			}
		}
	}

	public override bool CheckDead()
	{
		base.NPC.life = 1;
		DespawnSpecificProjectiles(everything: true);
		Dying = true;
		base.NPC.active = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
		return false;
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.AddScreenshakeAt(base.NPC.Center, 10f, 2000f);
		Color hiColor = ProvUtils.GetProjectileColor(255);
		Color loColor = ProvUtils.GetProjectileColor(0, Outline: true);
		for (int i = 0; i < 30; i++)
		{
			GeneralParticleHandler.SpawnParticle(new FlameParticle(base.NPC.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(150f), 0f), 6.2831854820251465), 40, Main.rand.NextFloat(0.5f, 0.75f), Main.rand.NextFloat(1f, 2.5f), hiColor, loColor)
			{
				Velocity = Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(3f, 19f), 0f), 6.2831854820251465)
			});
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.NPC.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(12f, 40f), 0f), 6.2831854820251465), loColor, 60, Main.rand.NextFloat(0.75f, 1.75f), 1f, Main.rand.NextFloat(-0.05f, 0.05f), glowing: true));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 1f, 0.1f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (float i2 = 0f; i2 < 3f; i2 += 0.25f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One * i2, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.35f, 35, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.NPC.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/ShatteredExplosion", Vector2.One * i2, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.275f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			if (!DownedBossSystem.downedProvidence)
			{
				string key2 = "Mods.CalamityMod.Status.Progression.ProfanedBossText3";
				Color messageColor2 = Color.Orange;
				string key3 = "Mods.CalamityMod.Status.Progression.TreeOreText";
				Color messageColor3 = Color.LightGreen;
				CalamityUtils.SpawnOre(ModContent.TileType<UelibloomOre>(), 0.00017, 0.55f, 0.9f, 8, 14, 59);
				CalamityUtils.BroadcastLocalizedText(key2, messageColor2);
				CalamityUtils.BroadcastLocalizedText(key3, messageColor3);
			}
			if (challenge && Main.netMode == 0)
			{
				Main.NewText(Language.GetTextValue("Mods.CalamityMod.Status.Progression.ProfanedBossText4"), Color.DarkOrange);
			}
			DownedBossSystem.downedProvidence = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ProvidenceBag>()));
		npcLoot.AddIf(() => !DownedBossSystem.downedProvidence, ModContent.ItemType<MarkofProvidence>(), 1, 1, 1, ui: true, DropHelper.FirstKillText);
		npcLoot.Add(DropHelper.PerPlayer(ModContent.ItemType<ElysianWings>()));
		npcLoot.Add(DropHelper.PerPlayer(ModContent.ItemType<ElysianAegis>()));
		npcLoot.DefineConditionalDropSet(DropHelper.If((DropAttemptInfo info) => info.npc.ModNPC<Providence>().challenge, () => Main.expertMode, DropHelper.ProvidenceChallengeText)).Add(ModContent.ItemType<ProfanedSoulCrystal>());
		npcLoot.AddIf((DropAttemptInfo info) => info.npc.ModNPC<Providence>().hasBeenGivenFullPower, ModContent.ItemType<ProfanedMoonlightDye>(), 1, 4, 4, ui: true, DropHelper.ProvidenceEnragedText);
		npcLoot.AddIf((DropAttemptInfo info) => info.npc.ModNPC<Providence>().hasBeenGivenFullPower, ModContent.ItemType<DivineGeode>(), 1, 75, 90);
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[7]
		{
			ModContent.ItemType<HolyCollider>(),
			ModContent.ItemType<BurningRevelation>(),
			ModContent.ItemType<TelluricGlare>(),
			ModContent.ItemType<BlissfulBombardier>(),
			ModContent.ItemType<PurgeGuzzler>(),
			ModContent.ItemType<DazzlingStabberStaff>(),
			ModContent.ItemType<MoltenAmputator>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<PristineFury>(), 10);
		normalOnly.Add(ModContent.ItemType<DivineGeode>(), 1, 50, 60);
		normalOnly.Add(ModContent.ItemType<UnholyEssence>(), 1, 30, 40);
		normalOnly.Add(ModContent.ItemType<ProvidenceMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<ProvidenceTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<ProvidenceRelic>());
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<AscendantSpiritEssence>(), 1, 1, 99), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<BlasphemousDonut>(), 1, 1117, 2201), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedProvidence, ModContent.ItemType<LoreProvidence>(), ui: true, DropHelper.FirstKillText);
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<SupremeHealingPotion>();
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_083d: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		bool offColor = base.NPC.localAI[1] != 0f && (base.NPC.Calamity().newAI[3] >= 180f || Main.zenithWorld);
		Texture2D texture = (offColor ? TextureNight.Value : TextureAssets.Npc[base.Type].Value);
		Texture2D textureGlow = (offColor ? TextureNight_Glow.Value : Texture_Glow.Value);
		Texture2D textureGlow2 = Texture_Glow_2.Value;
		float burnIntensity = Utils.GetLerpValue(0f, 45f, DeathAnimationTimer, clamped: true);
		int totalProvidencesToDraw = (int)MathHelper.Lerp(1f, 30f, burnIntensity);
		for (int i = 0; i < totalProvidencesToDraw; i++)
		{
			float num = (float)Math.PI * 2f * (float)i * 2f / (float)totalProvidencesToDraw;
			float drawOffsetFactor = (float)Math.Sin(num * 6f + Main.GlobalTimeWrappedHourly * (float)Math.PI);
			drawOffsetFactor *= (float)Math.Pow(burnIntensity, 3.0) * 50f;
			Vector2 drawOffset = num.ToRotationVector2() * drawOffsetFactor;
			Color baseColor = Color.White * (MathHelper.Lerp(0.4f, 0.8f, burnIntensity) / (float)totalProvidencesToDraw * 1.5f);
			((Color)(ref baseColor)).A = 0;
			baseColor = Color.Lerp(Color.White, baseColor, burnIntensity);
			drawProvidenceInstance(drawOffset, (totalProvidencesToDraw == 1) ? ((Color?)null) : new Color?(baseColor));
		}
		if (base.NPC.IsABestiaryIconDummy)
		{
			return false;
		}
		if (base.NPC.localAI[0] > 0f && base.NPC.localAI[0] < 120f)
		{
			float lerpMult = MathHelper.Lerp(0.5f, 1.5f, (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f)) / 2f + 1f);
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/StarProj", (AssetRequestMode)2).Value;
			float drawOffsetAmt = ((AIState == 2f || AIState == 5f) ? 20f : 64f);
			Vector2 drawPos = base.NPC.Center + Vector2.UnitY * drawOffsetAmt * base.NPC.scale - Main.screenPosition;
			Color baseColor2 = Color.Lerp(Color.Yellow, Color.OrangeRed, (float)Math.Sin(Main.GlobalTimeWrappedHourly) / 2f + 1f);
			baseColor2 *= 0.5f;
			((Color)(ref baseColor2)).A = 0;
			Color colorA = baseColor2;
			Color colorB = baseColor2 * 0.5f;
			float opacityScaleDuringStarDespawn = (120f - base.NPC.localAI[0]) / 120f;
			float scaleDuringStarDespawnScale = 1.8f;
			float scaleDuringStarDespawn = (1f - opacityScaleDuringStarDespawn) * scaleDuringStarDespawnScale;
			float colorScale = MathHelper.Lerp(0f, lerpMult, opacityScaleDuringStarDespawn);
			colorA *= colorScale;
			colorB *= colorScale;
			Vector2 origin = value.Size() / 2f;
			Vector2 scale = new Vector2(1.5f + scaleDuringStarDespawn, 2.5f + scaleDuringStarDespawn) * lerpMult;
			float upRight = (float)Math.PI / 4f + base.NPC.rotation;
			float up = (float)Math.PI / 2f + base.NPC.rotation;
			float upLeft = (float)Math.PI * 3f / 4f + base.NPC.rotation;
			float left = (float)Math.PI + base.NPC.rotation;
			Main.EntitySpriteDraw(value, drawPos, null, colorA, upLeft, origin, scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, upRight, origin, scale, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, upLeft, origin, scale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, upRight, origin, scale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, up, origin, scale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorA, left, origin, scale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, up, origin, scale * 0.36f, (SpriteEffects)0);
			Main.EntitySpriteDraw(value, drawPos, null, colorB, left, origin, scale * 0.36f, (SpriteEffects)0);
		}
		if (base.NPC.localAI[3] > 0f && base.NPC.localAI[3] < 120f)
		{
			float maxOscillation = 60f;
			float minScale = 0.9f;
			float maxPulseScale = 1f - minScale;
			float minOpacity = 0.5f;
			float maxOpacityScale = 1f - minOpacity;
			float currentOscillation = MathHelper.Lerp(0f, maxOscillation, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * (float)Math.PI) + 1f) * 0.5f);
			float shieldOpacity = minOpacity + maxOpacityScale * Utils.Remap(currentOscillation, 0f, maxOscillation, 1f, 0f);
			float oscillationRatio = currentOscillation / maxOscillation;
			float invertedOscillationRatio = 1f - (1f - oscillationRatio) * (1f - oscillationRatio);
			float oscillationScale = 1f - (1f - invertedOscillationRatio) * (1f - invertedOscillationRatio);
			float num2 = Utils.Remap(currentOscillation, maxOscillation - 15f, maxOscillation, 0f, 1f);
			float twoOscillationsMultipliedTogetherForScaleCalculation = num2 * num2;
			float invertedOscillationUsedForScale = MathHelper.Lerp(minScale, 1f, 1f - twoOscillationsMultipliedTogetherForScaleCalculation);
			float shieldScale = (minScale + maxPulseScale * oscillationScale) * invertedOscillationUsedForScale;
			float smallerRemappedOscillation = Utils.Remap(currentOscillation, 20f, maxOscillation, 0f, 1f);
			float invertedSmallerOscillationRatio = 1f - (1f - smallerRemappedOscillation) * (1f - smallerRemappedOscillation);
			float smallerOscillationScale = 1f - (1f - invertedSmallerOscillationRatio) * (1f - invertedSmallerOscillationRatio);
			float shieldScale2 = (minScale + maxPulseScale * smallerOscillationScale) * invertedOscillationUsedForScale;
			Texture2D shieldTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleOpenCircleButBigger", (AssetRequestMode)2).Value;
			Rectangle shieldFrame = shieldTexture.Frame();
			Vector2 origin2 = shieldFrame.Size() * 0.5f;
			Vector2 shieldDrawPos = base.NPC.Center - screenPos;
			shieldDrawPos -= new Vector2((float)shieldTexture.Width, (float)shieldTexture.Height) * base.NPC.scale / 2f;
			shieldDrawPos += origin2 * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
			float minHue = 0.06f;
			float maxHue = 0.18f;
			float opacityScaleDuringShieldDespawn = (120f - base.NPC.localAI[3]) / 120f;
			float scaleDuringShieldDespawnScale = 1.8f;
			float scaleDuringShieldDespawn = (1f - opacityScaleDuringShieldDespawn) * scaleDuringShieldDespawnScale;
			float colorScale2 = MathHelper.Lerp(0f, shieldOpacity, opacityScaleDuringShieldDespawn);
			Color color = Main.hslToRgb(MathHelper.Lerp(maxHue - minHue, maxHue, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f)) + 1f) * 0.5f), 1f, 0.5f) * colorScale2;
			Color color2 = Main.hslToRgb(MathHelper.Lerp(minHue, maxHue - minHue, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * (float)Math.PI * 3f) + 1f) * 0.5f), 1f, 0.5f) * colorScale2;
			((Color)(ref color2)).A = 0;
			color *= 0.6f;
			color2 *= 0.6f;
			float scaleMult = 2.75f + scaleDuringShieldDespawn;
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color2, base.NPC.rotation, origin2, shieldScale2 * scaleMult * 0.45f, (SpriteEffects)0, 0f);
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color2, base.NPC.rotation, origin2, shieldScale2 * scaleMult * 0.5f, (SpriteEffects)0, 0f);
			float noiseScale = MathHelper.Lerp(0.4f, 0.8f, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.3f) * 0.5f + 0.5f);
			Effect shieldEffect = Terraria.Graphics.Effects.Filters.Scene["CalamityMod:RoverDriveShield"].GetShader().Shader;
			shieldEffect.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 0.058f);
			shieldEffect.Parameters["blowUpPower"].SetValue(2.8f);
			shieldEffect.Parameters["blowUpSize"].SetValue(0.4f);
			shieldEffect.Parameters["noiseScale"].SetValue(noiseScale);
			shieldEffect.Parameters["shieldOpacity"].SetValue(opacityScaleDuringShieldDespawn);
			shieldEffect.Parameters["shieldEdgeBlendStrenght"].SetValue(4f);
			Color edgeColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.2f, color, color2);
			shieldEffect.Parameters["shieldColor"].SetValue(((Color)(ref color)).ToVector3());
			shieldEffect.Parameters["shieldEdgeColor"].SetValue(((Color)(ref edgeColor)).ToVector3());
			Matrix matrix = Main.GameViewMatrix.TransformationMatrix;
			using (Main.spriteBatch.Scope())
			{
				Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, shieldEffect, matrix);
				Texture2D heatTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons2", (AssetRequestMode)2).Value;
				_ = base.NPC.Center + base.NPC.gfxOffY * Vector2.UnitY - Main.screenPosition;
				Main.spriteBatch.Draw(heatTex, shieldDrawPos, (Rectangle?)null, Color.White, 0f, heatTex.Size() / 2f, shieldScale * scaleMult * 0.5f, (SpriteEffects)0, 0f);
				Main.spriteBatch.End();
			}
		}
		return false;
		void drawProvidenceInstance(Vector2 val, Color? colorOverride)
		{
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_0113: Unknown result type (might be due to invalid IL or missing references)
			//IL_0132: Unknown result type (might be due to invalid IL or missing references)
			//IL_0137: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0400: Unknown result type (might be due to invalid IL or missing references)
			//IL_0405: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_060a: Unknown result type (might be due to invalid IL or missing references)
			//IL_061a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0624: Unknown result type (might be due to invalid IL or missing references)
			//IL_0629: Unknown result type (might be due to invalid IL or missing references)
			//IL_062e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0630: Unknown result type (might be due to invalid IL or missing references)
			//IL_0632: Unknown result type (might be due to invalid IL or missing references)
			//IL_063f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0654: Unknown result type (might be due to invalid IL or missing references)
			//IL_0659: Unknown result type (might be due to invalid IL or missing references)
			//IL_065e: Unknown result type (might be due to invalid IL or missing references)
			//IL_065f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0664: Unknown result type (might be due to invalid IL or missing references)
			//IL_0669: Unknown result type (might be due to invalid IL or missing references)
			//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06df: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_070b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0719: Unknown result type (might be due to invalid IL or missing references)
			//IL_071e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0720: Unknown result type (might be due to invalid IL or missing references)
			//IL_0725: Unknown result type (might be due to invalid IL or missing references)
			//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_068a: Unknown result type (might be due to invalid IL or missing references)
			//IL_069e: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_0429: Unknown result type (might be due to invalid IL or missing references)
			//IL_042e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0430: Unknown result type (might be due to invalid IL or missing references)
			//IL_0432: Unknown result type (might be due to invalid IL or missing references)
			//IL_0436: Unknown result type (might be due to invalid IL or missing references)
			//IL_043b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0443: Unknown result type (might be due to invalid IL or missing references)
			//IL_0445: Unknown result type (might be due to invalid IL or missing references)
			//IL_044a: Unknown result type (might be due to invalid IL or missing references)
			//IL_044c: Unknown result type (might be due to invalid IL or missing references)
			//IL_045a: Unknown result type (might be due to invalid IL or missing references)
			//IL_045f: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0480: Unknown result type (might be due to invalid IL or missing references)
			//IL_049d: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_04be: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0504: Unknown result type (might be due to invalid IL or missing references)
			//IL_0509: Unknown result type (might be due to invalid IL or missing references)
			//IL_050b: Unknown result type (might be due to invalid IL or missing references)
			//IL_050d: Unknown result type (might be due to invalid IL or missing references)
			//IL_051a: Unknown result type (might be due to invalid IL or missing references)
			//IL_052f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0534: Unknown result type (might be due to invalid IL or missing references)
			//IL_0539: Unknown result type (might be due to invalid IL or missing references)
			//IL_053a: Unknown result type (might be due to invalid IL or missing references)
			//IL_053f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0544: Unknown result type (might be due to invalid IL or missing references)
			//IL_0552: Unknown result type (might be due to invalid IL or missing references)
			//IL_055a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0564: Unknown result type (might be due to invalid IL or missing references)
			//IL_056c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0580: Unknown result type (might be due to invalid IL or missing references)
			//IL_058e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0593: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_046c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0471: Unknown result type (might be due to invalid IL or missing references)
			//IL_081c: Unknown result type (might be due to invalid IL or missing references)
			//IL_081e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0822: Unknown result type (might be due to invalid IL or missing references)
			//IL_0832: Unknown result type (might be due to invalid IL or missing references)
			//IL_0837: Unknown result type (might be due to invalid IL or missing references)
			//IL_0839: Unknown result type (might be due to invalid IL or missing references)
			//IL_083b: Unknown result type (might be due to invalid IL or missing references)
			//IL_083f: Unknown result type (might be due to invalid IL or missing references)
			//IL_084f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0854: Unknown result type (might be due to invalid IL or missing references)
			//IL_0750: Unknown result type (might be due to invalid IL or missing references)
			//IL_0755: Unknown result type (might be due to invalid IL or missing references)
			//IL_0757: Unknown result type (might be due to invalid IL or missing references)
			//IL_075c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0878: Unknown result type (might be due to invalid IL or missing references)
			//IL_087d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0883: Unknown result type (might be due to invalid IL or missing references)
			//IL_0888: Unknown result type (might be due to invalid IL or missing references)
			//IL_0861: Unknown result type (might be due to invalid IL or missing references)
			//IL_0866: Unknown result type (might be due to invalid IL or missing references)
			//IL_086a: Unknown result type (might be due to invalid IL or missing references)
			//IL_086f: Unknown result type (might be due to invalid IL or missing references)
			//IL_080e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0813: Unknown result type (might be due to invalid IL or missing references)
			//IL_0815: Unknown result type (might be due to invalid IL or missing references)
			//IL_081a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0775: Unknown result type (might be due to invalid IL or missing references)
			//IL_077a: Unknown result type (might be due to invalid IL or missing references)
			//IL_077c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0781: Unknown result type (might be due to invalid IL or missing references)
			//IL_079a: Unknown result type (might be due to invalid IL or missing references)
			//IL_079f: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
			//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aaf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0abd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ae6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0afd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b34: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_08bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_07de: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0917: Unknown result type (might be due to invalid IL or missing references)
			//IL_0921: Unknown result type (might be due to invalid IL or missing references)
			//IL_0926: Unknown result type (might be due to invalid IL or missing references)
			//IL_092c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0931: Unknown result type (might be due to invalid IL or missing references)
			//IL_0936: Unknown result type (might be due to invalid IL or missing references)
			//IL_0938: Unknown result type (might be due to invalid IL or missing references)
			//IL_095f: Unknown result type (might be due to invalid IL or missing references)
			//IL_096f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0979: Unknown result type (might be due to invalid IL or missing references)
			//IL_097e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0983: Unknown result type (might be due to invalid IL or missing references)
			//IL_0985: Unknown result type (might be due to invalid IL or missing references)
			//IL_0987: Unknown result type (might be due to invalid IL or missing references)
			//IL_0994: Unknown result type (might be due to invalid IL or missing references)
			//IL_09a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_09be: Unknown result type (might be due to invalid IL or missing references)
			//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_09de: Unknown result type (might be due to invalid IL or missing references)
			//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_09f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a1b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a32: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a76: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a44: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
			_ = "CalamityMod/NPCs/Providence/" + "Glowmasks/";
			float spawnAnimationTime = 180f;
			bool spawnAnimation = base.NPC.Calamity().newAI[3] < spawnAnimationTime;
			if (spawnAnimation && (base.NPC.localAI[1] == 0f || BossRushEvent.BossRushActive))
			{
				Asset<Texture2D> orbTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
				float sc = CalamityUtils.CircInEasing(base.NPC.Calamity().newAI[3] / spawnAnimationTime, 1);
				for (int j = 0; j < 3; j++)
				{
					_ = (int)(base.NPC.Calamity().newAI[3] / 4f) % 4;
					MathHelper.Lerp(1f, 0f, sc);
					Main.EntitySpriteDraw(orbTex.Value, base.NPC.Center - Main.screenPosition + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(MathHelper.Lerp(0f, 10f, sc)), 0f), 6.2831854820251465), orbTex.Frame(), ProvUtils.GetProjectileColor(0).MultiplyRGBA(new Color(sc * 45f, sc * 11f, sc * 22f, 0f)), 0f, orbTex.Frame().Center(), sc * 4.4f + (float)(Math.Cos(base.NPC.Calamity().newAI[3] / 10f) * (double)sc), (SpriteEffects)0);
				}
			}
			if (AIState == 2f || AIState == 5f)
			{
				if (!useDefenseFrames)
				{
					texture = (offColor ? TextureDefenseNight.Value : TextureDefense.Value);
					textureGlow = (offColor ? TextureDefenseNight_Glow.Value : TextureDefense_Glow.Value);
					textureGlow2 = TextureDefense_Glow_2.Value;
				}
				else
				{
					texture = (offColor ? TextureDefenseAltNight.Value : TextureDefenseAlt.Value);
					textureGlow = (offColor ? TextureDefenseAltNight_Glow.Value : TextureDefenseAlt_Glow.Value);
					textureGlow2 = TextureDefenseAlt_Glow_2.Value;
				}
			}
			else
			{
				switch (frameUsed)
				{
				case 1:
					texture = (offColor ? TextureAltNight.Value : TextureAlt.Value);
					textureGlow = (offColor ? TextureAltNight_Glow.Value : TextureAlt_Glow.Value);
					textureGlow2 = TextureAlt_Glow_2.Value;
					break;
				case 2:
					texture = (offColor ? TextureAttackNight.Value : TextureAttack.Value);
					textureGlow = (offColor ? TextureAttackNight_Glow.Value : TextureAttack_Glow.Value);
					textureGlow2 = TextureAttack_Glow_2.Value;
					break;
				case 3:
					texture = (offColor ? TextureAttackAltNight.Value : TextureAttackAlt.Value);
					textureGlow = (offColor ? TextureAttackAltNight_Glow.Value : TextureAttackAlt_Glow.Value);
					textureGlow2 = TextureAttackAlt_Glow_2.Value;
					break;
				}
			}
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (base.NPC.spriteDirection == 1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Vector2 RotationCenter = default(Vector2);
			((Vector2)(ref RotationCenter))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
			Color BaseColor = Color.White;
			float Brightness = 0.5f;
			int maxAfterimages = 5;
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int k = 1; k < maxAfterimages; k += 2)
				{
					Color AfterimageColor = drawColor;
					AfterimageColor = Color.Lerp(AfterimageColor, BaseColor, Brightness);
					AfterimageColor = base.NPC.GetAlpha(AfterimageColor);
					AfterimageColor *= (float)(maxAfterimages - k) / 15f;
					if (colorOverride.HasValue)
					{
						AfterimageColor = colorOverride.Value;
					}
					Vector2 AfterimageBodyPosition = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					AfterimageBodyPosition -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					AfterimageBodyPosition += RotationCenter * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
					spriteBatch.Draw(texture, AfterimageBodyPosition, (Rectangle?)base.NPC.frame, AfterimageColor.MultiplyRGBA(Lighting.GetColor((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16)), base.NPC.rotation, RotationCenter, base.NPC.scale, spriteEffects, 0f);
				}
			}
			Vector2 BasePosition = base.NPC.Center - screenPos;
			BasePosition -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
			BasePosition += RotationCenter * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
			Color finalDrawColor = (base.NPC.IsABestiaryIconDummy ? Color.White : ((Color)(((_003F?)colorOverride) ?? Lighting.GetColor((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16)) * base.NPC.Opacity));
			spriteBatch.Draw(texture, BasePosition, (Rectangle?)base.NPC.frame, finalDrawColor, base.NPC.rotation, RotationCenter, base.NPC.scale, spriteEffects, 0f);
			Color WingColor = ProvUtils.GetProjectileColor(0);
			Color CrystalColor = Color.Violet;
			if (base.NPC.localAI[1] == 1f)
			{
				if (Main.GlobalTimeWrappedHourly % 6f >= 5f)
				{
					WingColor = Color.Magenta;
					CrystalColor = Color.GreenYellow;
				}
				else if (Main.GlobalTimeWrappedHourly % 6f >= 4f)
				{
					WingColor = Color.Cyan;
					CrystalColor = Color.BlueViolet;
				}
				else if (Main.GlobalTimeWrappedHourly % 6f >= 3f)
				{
					WingColor = Color.Green;
					CrystalColor = Color.Gold;
				}
				else if (Main.GlobalTimeWrappedHourly % 6f >= 2f)
				{
					CrystalColor = Color.Violet;
				}
				else if (Main.GlobalTimeWrappedHourly % 6f >= 1f)
				{
					WingColor = Color.Orange;
					CrystalColor = Color.HotPink;
				}
				else
				{
					WingColor = Color.Red;
					CrystalColor = Color.BlueViolet;
				}
			}
			else if (base.NPC.localAI[1] == -1f && !spawnAnimation)
			{
				WingColor = Color.Cyan;
				CrystalColor = Color.BlueViolet;
			}
			Color BaseWingColor = Color.Lerp(WingColor, BaseColor, Brightness) * base.NPC.Opacity;
			Color BaseCrystalColor = Color.Lerp(CrystalColor, BaseColor, Brightness) * base.NPC.Opacity;
			if (colorOverride.HasValue)
			{
				BaseWingColor = colorOverride.Value;
				BaseCrystalColor = colorOverride.Value;
			}
			Color GlowWingColor = ProvUtils.GetProjectileColor(base.NPC.GetAlpha(drawColor), Outline: true);
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int l = 1; l < maxAfterimages; l++)
				{
					Color AfterimageWingColor = ProvUtils.GetProjectileColor(0, Outline: true);
					AfterimageWingColor = Color.Lerp(AfterimageWingColor, BaseColor, Brightness);
					AfterimageWingColor = base.NPC.GetAlpha(AfterimageWingColor);
					AfterimageWingColor *= (float)(maxAfterimages - l) / 15f;
					if (colorOverride.HasValue)
					{
						AfterimageWingColor = colorOverride.Value;
					}
					Vector2 AfterimageGlowPosition = base.NPC.oldPos[l] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					AfterimageGlowPosition -= new Vector2((float)textureGlow.Width, (float)(textureGlow.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					AfterimageGlowPosition += RotationCenter * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
					spriteBatch.Draw(textureGlow, AfterimageGlowPosition, (Rectangle?)base.NPC.frame, AfterimageWingColor, base.NPC.rotation, RotationCenter, base.NPC.scale, spriteEffects, 0f);
					Color AfterimageCrystalColor = BaseCrystalColor;
					AfterimageCrystalColor = Color.Lerp(AfterimageCrystalColor, BaseColor, Brightness);
					AfterimageCrystalColor = base.NPC.GetAlpha(AfterimageCrystalColor);
					AfterimageCrystalColor *= (float)(maxAfterimages - l) / 15f;
					if (colorOverride.HasValue)
					{
						AfterimageCrystalColor = colorOverride.Value;
					}
					spriteBatch.Draw(textureGlow2, AfterimageGlowPosition, (Rectangle?)base.NPC.frame, AfterimageCrystalColor, base.NPC.rotation, RotationCenter, base.NPC.scale, spriteEffects, 0f);
				}
			}
			if (!Dying)
			{
				base.NPC.DrawBackglow(GlowWingColor, 4f, (SpriteEffects)0, base.NPC.frame, Main.screenPosition, textureGlow);
				spriteBatch.Draw(textureGlow, BasePosition, (Rectangle?)base.NPC.frame, BaseWingColor, base.NPC.rotation, RotationCenter, base.NPC.scale, spriteEffects, 0f);
				spriteBatch.Draw(textureGlow2, BasePosition, (Rectangle?)base.NPC.frame, BaseCrystalColor, base.NPC.rotation, RotationCenter, base.NPC.scale, spriteEffects, 0f);
			}
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetProjectileColor(drawColor) * base.NPC.Opacity;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/NPCs/Providence/Providence_DeathSilhouette", (AssetRequestMode)2).Value;
		if (Dying && (float)DeathAnimationTimer > 91f)
		{
			Main.EntitySpriteDraw(tex, base.NPC.Center + new Vector2(0f, 10f) - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0f, tex.Size() / 2f, 1f, (SpriteEffects)0, 0f);
			Main.EntitySpriteDraw(tex, base.NPC.Center + new Vector2(0f, 15f) - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0f, tex.Size() / 2f, 1f, (SpriteEffects)0, 0f);
			Main.EntitySpriteDraw(tex, base.NPC.Center + new Vector2(0f, 20f) - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0f, tex.Size() / 2f, 1f, (SpriteEffects)0, 0f);
			Main.EntitySpriteDraw(tex, base.NPC.Center + new Vector2(0f, 25f) - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0f, tex.Size() / 2f, 1f, (SpriteEffects)0, 0f);
			Vector2 vec = default(Vector2);
			((Vector2)(ref vec))._002Ector(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
			float progress = MathHelper.Clamp(((float)DeathAnimationTimer - 200f) / 300f, 0f, 1f);
			for (int i = 0; i < base.NPC.frame.Height / 2; i++)
			{
				int outset = (i - base.NPC.frame.Height / 4) * 2;
				int pr = (int)(progress * 30f);
				Color col = ProvUtils.GetProjectileColor(Color.DarkGray, Outline: true);
				((Color)(ref col)).A = byte.MaxValue;
				Main.EntitySpriteDraw(tex, base.NPC.Center + vec + new Vector2(0f, 310f) - Main.screenPosition + new Vector2((float)(Main.rand.Next(-pr, pr) * 2), (float)outset), (Rectangle?)new Rectangle(0, i * 2, tex.Width, 2), Color.Lerp(col, Color.White, progress * 2f).MultiplyRGBA(new Color(255f, 255f, 255f, MathHelper.Lerp(progress, 1f, 0.2f) * 2f)), 0f, tex.Size() / 2f, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		int totalFrames = 3;
		if (AIState == 2f || AIState == 5f)
		{
			if (!useDefenseFrames)
			{
				base.NPC.frameCounter += (Dying ? 0.25 : 1.0);
				if (base.NPC.frameCounter > 10.0)
				{
					base.NPC.frame.Y += frameHeight;
					base.NPC.frameCounter = 0.0;
				}
				if (base.NPC.frame.Y >= frameHeight * totalFrames)
				{
					base.NPC.frame.Y = 0;
					useDefenseFrames = true;
				}
			}
			else
			{
				base.NPC.frameCounter += (Dying ? 0.25 : 1.0);
				if (base.NPC.frameCounter > 10.0)
				{
					base.NPC.frame.Y += frameHeight;
					base.NPC.frameCounter = 0.0;
				}
				if (base.NPC.frame.Y >= frameHeight * 2)
				{
					base.NPC.frame.Y = frameHeight * 2;
				}
			}
		}
		else
		{
			if (useDefenseFrames)
			{
				useDefenseFrames = false;
			}
			base.NPC.frameCounter += (Dying ? 0.25 : ((base.NPC.Calamity().newAI[3] < 180f) ? 0.625 : 1.0));
			if (base.NPC.frameCounter > 5.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.frame.Y >= frameHeight * totalFrames)
			{
				base.NPC.frame.Y = 0;
				frameUsed++;
			}
			int totalSheets = 4;
			if (frameUsed >= totalSheets)
			{
				frameUsed = 0;
			}
		}
	}

	private static void DrawHolyInferno()
	{
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		if (Main.gameMenu || !shouldDrawInfernoBorder || CalamityGlobalNPC.holyBoss == -1)
		{
			return;
		}
		NPC npc = Main.npc[CalamityGlobalNPC.holyBoss];
		float borderDistance = borderRadius;
		if (!npc.active || !npc.HasValidTarget)
		{
			return;
		}
		Player target = Main.LocalPlayer;
		float holyInfernoIntensity = target.Calamity().holyInfernoFadeIntensity;
		Providence prov = npc.ModNPC<Providence>();
		if (prov != null)
		{
			Asset<Texture2D> blackTile = TextureAssets.MagicPixel;
			float maxOpacity = 1f;
			if (prov.Dying)
			{
				maxOpacity = MathHelper.Lerp(1f, 0f, Utils.GetLerpValue(0f, 344f, prov.DeathAnimationTimer));
			}
			Effect shader = GameShaders.Misc["CalamityMod:HolyInfernoShader"].Shader;
			shader.Parameters["colorMult"].SetValue(prov.hasBeenGivenFullPower ? 7.65f : 7.35f);
			shader.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly);
			shader.Parameters["radius"].SetValue(borderDistance);
			shader.Parameters["anchorPoint"].SetValue(npc.Center);
			shader.Parameters["screenPosition"].SetValue(Main.screenPosition);
			shader.Parameters["screenSize"].SetValue(Main.ScreenSize.ToVector2());
			shader.Parameters["burnIntensity"].SetValue(holyInfernoIntensity);
			shader.Parameters["playerPosition"].SetValue(target.Center);
			shader.Parameters["maxOpacity"].SetValue(maxOpacity);
			shader.Parameters["day"].SetValue(!prov.hasBeenGivenFullPower);
			((GraphicsResource)Main.spriteBatch).GraphicsDevice.Textures[1] = (Texture)(object)DiagonalNoise.Value;
			((GraphicsResource)Main.spriteBatch).GraphicsDevice.Textures[2] = (Texture)(object)UpwardNoise.Value;
			((GraphicsResource)Main.spriteBatch).GraphicsDevice.Textures[3] = (Texture)(object)UpwardPerlinNoise.Value;
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, shader, Main.Transform);
			Rectangle rekt = default(Rectangle);
			((Rectangle)(ref rekt))._002Ector(Main.screenWidth / 2, Main.screenHeight / 2, Main.screenWidth, Main.screenHeight);
			Main.spriteBatch.Draw(blackTile.Value, rekt, (Rectangle?)null, default(Color), 0f, blackTile.Value.Size() * 0.5f, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		scale = 2f;
		return null;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
	{
		if (!challenge)
		{
			return;
		}
		List<int> obj = new List<int>
		{
			ModContent.ProjectileType<MiniGuardianDefense>(),
			ModContent.ProjectileType<MiniGuardianAttack>(),
			ModContent.ProjectileType<MiniGuardianRock>(),
			ModContent.ProjectileType<MiniGuardianSpear>(),
			ModContent.ProjectileType<SilvaCrystalExplosion>(),
			ModContent.ProjectileType<GhostlyMine>(),
			ModContent.ProjectileType<EnergyOrb>(),
			ModContent.ProjectileType<IrradiatedAura>(),
			ModContent.ProjectileType<SummonAstralExplosion>(),
			ModContent.ProjectileType<ApparatusExplosion>(),
			ModContent.ProjectileType<TarragonAura>()
		};
		bool allowedDamage = (projectile.CountsAsClass<SummonDamageClass>() || (!projectile.CountsAsClass<MeleeDamageClass>() && !projectile.CountsAsClass<RangedDamageClass>() && !projectile.CountsAsClass<MagicDamageClass>() && !projectile.CountsAsClass<ThrowingDamageClass>() && !projectile.CountsAsClass<SummonMeleeSpeedDamageClass>())) && hit.Damage <= 75;
		bool allowedBabs = Main.player[projectile.owner].Calamity().pSoulArtifact && !Main.player[projectile.owner].Calamity().profanedCrystalBuffs;
		if ((obj.TrueForAll((int x) => projectile.type != x) && !allowedDamage) || !allowedBabs)
		{
			challenge = false;
			if (Main.netMode != 0)
			{
				PSCChallengeSyncPacket.Send(this);
			}
		}
	}

	public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
	{
		if (challenge)
		{
			challenge = false;
			if (Main.netMode != 0)
			{
				PSCChallengeSyncPacket.Send(this);
			}
		}
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		modifiers.SetMaxDamage(base.NPC.life - 1);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0 && !Dying)
		{
			base.NPC.soundDelay = 8;
			SoundEngine.PlaySound(in HurtSound, base.NPC.Center);
		}
		int dustType = ProvUtils.GetDustID();
		for (int k = 0; k < 15; k++)
		{
			int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, hit.HitDirection, -1f);
			Main.dust[dust].noGravity = true;
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread * Main.rand.NextFloat(), base.Mod.Find<ModGore>("Providence").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread * Main.rand.NextFloat(), base.Mod.Find<ModGore>("Providence2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread * Main.rand.NextFloat(), base.Mod.Find<ModGore>("Providence3").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread * Main.rand.NextFloat(), base.Mod.Find<ModGore>("Providence4").Type, base.NPC.scale);
		}
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (int)(400f * base.NPC.scale);
		base.NPC.height = (int)(350f * base.NPC.scale);
		NPC nPC = base.NPC;
		nPC.position -= base.NPC.Size * 0.5f;
		for (int d = 0; d < 60; d++)
		{
			int fire = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[fire];
			obj.velocity *= 3f;
			Main.dust[fire].noGravity = true;
			if (Main.rand.NextBool())
			{
				Main.dust[fire].scale = 0.5f;
				Main.dust[fire].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 90; i++)
		{
			int fire2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, 0f, 0f, 100, default(Color), 3f);
			Main.dust[fire2].noGravity = true;
			Dust obj2 = Main.dust[fire2];
			obj2.velocity *= 5f;
			fire2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fire2];
			obj3.velocity *= 2f;
			Main.dust[fire2].noGravity = true;
		}
	}

	public Providence()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		HighFireColor = new Color(255, 191, 73);
		LowFireColor = new Color(116, 45, 23);
		challenge = Main.expertMode;
		SoundWarningLevel = -1f;
		base._002Ector();
	}
}
