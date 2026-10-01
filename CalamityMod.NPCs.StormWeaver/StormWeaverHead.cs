using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
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
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.StormWeaver;

[HasPierceResist(false)]
[LongDistanceNetSync]
public class StormWeaverHead : ModNPC
{
	public static int normalIconIndex;

	public static int vulnerableIconIndex;

	private const float BoltAngleSpread = 280f;

	private bool tail;

	public static readonly SoundStyle ArmorShedSound = new SoundStyle("CalamityMod/Sounds/Custom/WeaverArmorShed");

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/WeaverDeath");

	public float lightning;

	private float lightningDecay = 1f;

	private float lightningSpeed;

	public static Asset<Texture2D> Phase2Texture;

	public static int LightningDamage = 64;

	public static int FrostWaveDamage = 64;

	public static int TornadoDamage = 72;

	public override void Load()
	{
		string normalIconPath = "CalamityMod/NPCs/StormWeaver/StormWeaverHead_Head_Boss";
		string vulnerableIconPath = "CalamityMod/NPCs/StormWeaver/StormWeaverHeadNaked_Head_Boss";
		normalIconIndex = CalamityMod.Instance.AddBossHeadTexture(normalIconPath);
		vulnerableIconIndex = CalamityMod.Instance.AddBossHeadTexture(vulnerableIconPath);
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.85f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 40f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 70f;
		value.Position.Y += 55f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			Phase2Texture = ModContent.Request<Texture2D>(Texture + "Naked", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 180;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 74;
		base.NPC.height = 74;
		base.NPC.lifeMax = 825000;
		base.NPC.LifeMaxNERB(base.NPC.lifeMax, base.NPC.lifeMax, 500000);
		base.NPC.value = Item.buyPrice(0, 50);
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		base.NPC.defense = 150;
		calamityGlobalNPC.DR = 0.999999f;
		calamityGlobalNPC.unbreakableDR = true;
		base.NPC.chaseable = false;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = DeathSound;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.boss = true;
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.netAlways = true;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.2f;
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.scale *= 1.15f;
		}
		else if (Main.expertMode)
		{
			base.NPC.scale *= 1.1f;
		}
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.7f;
		}
		base.NPC.Calamity().VulnerableToElectricity = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.StormWeaver")
		});
	}

	public override void BossHeadSlot(ref int index)
	{
		if ((float)base.NPC.life / (float)base.NPC.lifeMax < 0.8f)
		{
			index = vulnerableIconIndex;
		}
		else
		{
			index = normalIconIndex;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
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
		//IL_0dfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_111d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1122: Unknown result type (might be due to invalid IL or missing references)
		//IL_1143: Unknown result type (might be due to invalid IL or missing references)
		//IL_114b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1899: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1530: Unknown result type (might be due to invalid IL or missing references)
		//IL_1537: Unknown result type (might be due to invalid IL or missing references)
		//IL_153c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1560: Unknown result type (might be due to invalid IL or missing references)
		//IL_1567: Unknown result type (might be due to invalid IL or missing references)
		//IL_156c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_120a: Unknown result type (might be due to invalid IL or missing references)
		//IL_123f: Unknown result type (might be due to invalid IL or missing references)
		//IL_124a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1310: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1811: Unknown result type (might be due to invalid IL or missing references)
		//IL_181b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1820: Unknown result type (might be due to invalid IL or missing references)
		//IL_134f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1354: Unknown result type (might be due to invalid IL or missing references)
		//IL_137a: Unknown result type (might be due to invalid IL or missing references)
		//IL_137f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1389: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13af: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1408: Unknown result type (might be due to invalid IL or missing references)
		//IL_140f: Unknown result type (might be due to invalid IL or missing references)
		//IL_141a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1420: Unknown result type (might be due to invalid IL or missing references)
		//IL_1453: Unknown result type (might be due to invalid IL or missing references)
		//IL_145d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1462: Unknown result type (might be due to invalid IL or missing references)
		//IL_1470: Unknown result type (might be due to invalid IL or missing references)
		//IL_147b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1480: Unknown result type (might be due to invalid IL or missing references)
		//IL_1485: Unknown result type (might be due to invalid IL or missing references)
		//IL_108c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1091: Unknown result type (might be due to invalid IL or missing references)
		//IL_1098: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_10af: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_160f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_163f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1661: Unknown result type (might be due to invalid IL or missing references)
		//IL_1684: Unknown result type (might be due to invalid IL or missing references)
		//IL_1689: Unknown result type (might be due to invalid IL or missing references)
		//IL_168b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1690: Unknown result type (might be due to invalid IL or missing references)
		//IL_1699: Unknown result type (might be due to invalid IL or missing references)
		//IL_169e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0704: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0faa: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1700: Unknown result type (might be due to invalid IL or missing references)
		//IL_1734: Unknown result type (might be due to invalid IL or missing references)
		//IL_1739: Unknown result type (might be due to invalid IL or missing references)
		//IL_173b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1740: Unknown result type (might be due to invalid IL or missing references)
		//IL_1764: Unknown result type (might be due to invalid IL or missing references)
		//IL_1766: Unknown result type (might be due to invalid IL or missing references)
		//IL_1779: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_0986: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if (CalamityServerConfig.Instance.BossesStopWeather)
		{
			CalamityWorld.StopRain();
		}
		else if (!Main.raining && !BossRushEvent.BossRushActive)
		{
			CalamityWorld.StartRain();
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = lifeRatio < 0.8f;
		bool phase3 = lifeRatio < 0.55f;
		bool phase4 = lifeRatio < 0.3f;
		if (phase2 && !Main.zenithWorld && !base.NPC.chaseable)
		{
			base.NPC.Calamity().VulnerableToHeat = true;
			base.NPC.Calamity().VulnerableToCold = true;
			base.NPC.Calamity().VulnerableToSickness = true;
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SWArmorHead1").Type, base.NPC.scale);
			}
			SoundEngine.PlaySound(in ArmorShedSound, base.NPC.Center);
			CalamityGlobalNPC calamityGlobalNPC2 = base.NPC.Calamity();
			base.NPC.defense = 20;
			calamityGlobalNPC2.DR = 0.2f;
			calamityGlobalNPC2.unbreakableDR = false;
			base.NPC.chaseable = true;
			base.NPC.HitSound = SoundID.NPCHit13;
			base.NPC.frame = new Rectangle(0, 0, 62, 86);
		}
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		if (base.NPC.alpha != 0)
		{
			for (int i = 0; i < 2; i++)
			{
				int redDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 182, 0f, 0f, 100, default(Color), 2f);
				Main.dust[redDust].noGravity = true;
				Main.dust[redDust].noLight = true;
			}
		}
		base.NPC.alpha -= 12;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		if (Main.netMode != 1)
		{
			if (!tail && base.NPC.ai[0] == 0f)
			{
				int Previous = base.NPC.whoAmI;
				int totalLength = (death ? 60 : (revenge ? 50 : (expertMode ? 40 : 30)));
				int npcCounts = 0;
				if (Main.zenithWorld)
				{
					for (int j = 0; j < Main.maxNPCs; j++)
					{
						if (!Main.npc[j].active)
						{
							npcCounts++;
						}
					}
					totalLength = npcCounts - 20;
				}
				for (int segments = 0; segments < totalLength; segments++)
				{
					int lol = ((segments < 0 || segments >= totalLength - 1) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<StormWeaverTail>(), base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<StormWeaverBody>(), base.NPC.whoAmI));
					Main.npc[lol].realLife = base.NPC.whoAmI;
					Main.npc[lol].ai[2] = base.NPC.whoAmI;
					Main.npc[lol].ai[1] = Previous;
					Main.npc[Previous].ai[0] = lol;
					base.NPC.netUpdate = true;
					Previous = lol;
				}
				tail = true;
			}
			if (!phase2 || Main.zenithWorld)
			{
				base.NPC.localAI[0]++;
			}
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[0]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[0]].life;
		}
		if (Main.player[base.NPC.target].dead && base.NPC.life > 0)
		{
			base.NPC.localAI[1] = 0f;
			calamityGlobalNPC.newAI[0] = 0f;
			calamityGlobalNPC.newAI[2] = 0f;
			base.NPC.TargetClosest(faceTarget: false);
			base.NPC.velocity.Y -= 3f;
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				base.NPC.velocity.Y -= 3f;
			}
			if ((double)base.NPC.position.Y < (double)(Main.topWorld + 16f))
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.type == ModContent.NPCType<StormWeaverBody>() || n.type == ModContent.NPCType<StormWeaverHead>() || n.type == ModContent.NPCType<StormWeaverTail>())
					{
						n.active = false;
					}
				}
			}
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 10000f && base.NPC.life > 0)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				NPC n2 = enumerator2.Current;
				if (n2.type == ModContent.NPCType<StormWeaverBody>() || n2.type == ModContent.NPCType<StormWeaverHead>() || n2.type == ModContent.NPCType<StormWeaverTail>())
				{
					n2.active = false;
				}
			}
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		Vector2 npcCenter = base.NPC.Center;
		float targetCenterX = Main.player[base.NPC.target].Center.X;
		float targetCenterY = Main.player[base.NPC.target].Center.Y;
		float velocity = (phase2 ? 12f : 10f) + (revenge ? 1.5f : (expertMode ? 1f : 0f));
		float acceleration = (phase2 ? 0.24f : 0.2f) + (revenge ? 0.08f : (expertMode ? 0.04f : 0f));
		if (phase2)
		{
			if (!phase4)
			{
				calamityGlobalNPC.newAI[0]++;
			}
			else
			{
				base.NPC.localAI[1] = 0f;
				if (base.NPC.localAI[3] > 0f)
				{
					base.NPC.localAI[3]--;
				}
				calamityGlobalNPC.newAI[0] = 0f;
			}
			calamityGlobalNPC.newAI[2]++;
			bool useTornadoes = calamityGlobalNPC.newAI[3] % 2f != 0f;
			float chargePhaseGateValue = (death ? 320f : (revenge ? 340f : (expertMode ? 360f : 400f)));
			if (!phase3)
			{
				chargePhaseGateValue *= 0.5f;
			}
			if (phase4 & expertMode)
			{
				chargePhaseGateValue *= 0.9f;
			}
			float projectileGateValue = (int)(chargePhaseGateValue * 0.25f);
			if (phase3 && !useTornadoes)
			{
				if (!Main.dedServ)
				{
					Vector2 scaledSize = Main.Camera.ScaledSize;
					Vector2 scaledPosition = Main.Camera.ScaledPosition;
					if (Main.gamePaused || !((double)Main.player[base.NPC.target].position.Y < Main.worldSurface * 16.0))
					{
						return;
					}
					float screenWidth = Main.Camera.ScaledSize.X / (float)Main.maxScreenW;
					int snowDustMax = (int)(500f * screenWidth);
					snowDustMax = (int)((float)snowDustMax * 3f);
					float snowDustAmt = 50f;
					for (int k = 0; (float)k < snowDustAmt; k++)
					{
						try
						{
							if (!((float)Main.snowDust < (float)snowDustMax * (Main.gfxQuality / 2f + 0.5f) + (float)snowDustMax * 0.1f))
							{
								return;
							}
							if (!(Main.rand.NextFloat() < 0.125f))
							{
								continue;
							}
							int snowDustSpawnX = Main.rand.Next((int)scaledSize.X + 1500) - 750;
							int snowDustSpawnY = (int)scaledPosition.Y - Main.rand.Next(50);
							if (Main.player[base.NPC.target].velocity.Y > 0f)
							{
								snowDustSpawnY -= (int)Main.player[base.NPC.target].velocity.Y;
							}
							if (Main.rand.NextBool(5))
							{
								snowDustSpawnX = Main.rand.Next(500) - 500;
							}
							else if (Main.rand.NextBool(5))
							{
								snowDustSpawnX = Main.rand.Next(500) + (int)scaledSize.X;
							}
							if (snowDustSpawnX < 0 || (float)snowDustSpawnX > scaledSize.X)
							{
								snowDustSpawnY += Main.rand.Next((int)((double)scaledSize.Y * 0.8)) + (int)((double)scaledSize.Y * 0.1);
							}
							snowDustSpawnX += (int)scaledPosition.X;
							int snowDustSpawnTileX = snowDustSpawnX / 16;
							int snowDustSpawnTileY = snowDustSpawnY / 16;
							if (WorldGen.InWorld(snowDustSpawnTileX, snowDustSpawnTileY) && !Main.tile[snowDustSpawnTileX, snowDustSpawnTileY].HasUnactuatedTile)
							{
								int dust = Dust.NewDust(new Vector2((float)snowDustSpawnX, (float)snowDustSpawnY), 10, 10, 76);
								Main.dust[dust].scale += 0.2f;
								Main.dust[dust].velocity.Y = 3f + (float)Main.rand.Next(30) * 0.1f;
								Main.dust[dust].velocity.Y *= Main.dust[dust].scale;
								if (!Main.raining)
								{
									Main.dust[dust].velocity.X = Main.windSpeedCurrent + (float)Main.rand.Next(-10, 10) * 0.1f;
									Main.dust[dust].velocity.X += Main.windSpeedCurrent * 15f;
								}
								else
								{
									Main.dust[dust].velocity.X = (float)Math.Sqrt(Math.Abs(Main.windSpeedCurrent)) * (float)Math.Sign(Main.windSpeedCurrent) * 15f + Main.rand.NextFloat() * 0.2f - 0.1f;
									Main.dust[dust].velocity.Y *= 0.5f;
								}
								Main.dust[dust].velocity.Y *= 1.3f;
								Main.dust[dust].scale += 0.2f;
								Dust obj = Main.dust[dust];
								obj.velocity *= 1.5f;
							}
						}
						catch
						{
						}
					}
				}
				if (calamityGlobalNPC.newAI[2] >= projectileGateValue)
				{
					calamityGlobalNPC.newAI[2] = (0f - projectileGateValue) * 4f;
					if (phase4)
					{
						if (!Main.DisableIntenseVisualEffects && !CalamityClientConfig.Instance.Photosensitivity && !Main.dedServ && lightningSpeed == 0f)
						{
							lightningDecay = Main.rand.NextFloat() * 0.05f + 0.008f;
							lightningSpeed = Main.rand.NextFloat() * 0.05f + 0.05f;
						}
						calamityGlobalNPC.newAI[3]++;
					}
					SoundEngine.PlaySound(in SoundID.Item120, Main.player[base.NPC.target].Center);
					if (Main.netMode != 1)
					{
						int type = 348;
						int totalWaves = ((!death) ? (phase4 ? 25 : 23) : (phase4 ? 27 : 25));
						int shotSpacing = ((!death) ? (phase4 ? 200 : 215) : (phase4 ? 185 : 200));
						float projectileSpawnX = Main.player[base.NPC.target].Center.X - (float)(totalWaves * shotSpacing) * 0.5f;
						int centralWave = totalWaves / 2;
						float velocityY = 8f;
						int wavePatternType = (revenge ? Main.rand.Next(3) : (expertMode ? (Main.rand.Next(2) + 1) : 2));
						float delayBeforeFiring = -60f;
						for (int x = 0; x < totalWaves; x++)
						{
							switch (wavePatternType)
							{
							case 0:
								if (x != 0)
								{
									velocityY = ((x > centralWave) ? (velocityY + 1f / 6f) : (velocityY - 1f / 6f));
								}
								break;
							case 1:
								if (x != 0)
								{
									velocityY = ((x % 2 != 0) ? (velocityY - 2f) : (velocityY + 2f));
								}
								break;
							case 2:
								velocityY = 7f;
								break;
							}
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawnX, Main.player[base.NPC.target].Center.Y - 1600f, 0f, velocityY * 0.5f, ModContent.ProjectileType<StormWeaverFrostWaveTelegraph>(), 0, 0f, Main.myPlayer, 0f, velocityY);
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), projectileSpawnX, Main.player[base.NPC.target].Center.Y - 1600f, 0f, velocityY * 0.1f, type, FrostWaveDamage, 0f, Main.myPlayer, delayBeforeFiring, velocityY);
							projectileSpawnX += (float)shotSpacing;
						}
					}
				}
			}
			if (useTornadoes && calamityGlobalNPC.newAI[2] >= projectileGateValue)
			{
				calamityGlobalNPC.newAI[2] = (0f - projectileGateValue) * 4f;
				calamityGlobalNPC.newAI[3]++;
				if (Main.netMode != 1)
				{
					int projectileType = ModContent.ProjectileType<StormMarkHostile>();
					int totalTornadoes = (revenge ? 7 : (expertMode ? 5 : 3));
					float spawnDistance = (revenge ? 750f : (expertMode ? 900f : 1050f));
					for (int l = 0; l < totalTornadoes; l++)
					{
						Vector2 spawnPosition = Main.player[base.NPC.target].Center + Vector2.UnitX * spawnDistance * (float)(l - totalTornadoes / 2);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnPosition, Vector2.Zero, projectileType, 0, 0f, Main.myPlayer, TornadoDamage, 1f);
					}
				}
			}
			if (!phase4)
			{
				if (calamityGlobalNPC.newAI[0] == chargePhaseGateValue - 70f)
				{
					Vector2 soundCenter = Main.player[base.NPC.target].Center;
					SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/LightningTelegraph");
					soundStyle.Volume = 0.7f;
					SoundStyle lightning = soundStyle;
					SoundEngine.PlaySound(in lightning, soundCenter);
				}
				if (calamityGlobalNPC.newAI[0] >= chargePhaseGateValue)
				{
					base.NPC.localAI[3] = 60f;
					if (base.NPC.localAI[1] == 0f)
					{
						base.NPC.localAI[1] = 1f;
					}
					if (calamityGlobalNPC.newAI[0] >= chargePhaseGateValue + 100f)
					{
						base.NPC.TargetClosest();
						base.NPC.localAI[1] = 0f;
						calamityGlobalNPC.newAI[0] = 0f;
					}
					if (base.NPC.localAI[1] == 2f)
					{
						velocity += Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) * 0.01f * (1f - lifeRatio / 0.8f);
						acceleration += Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) * 0.0001f * (1f - lifeRatio / 0.8f);
						velocity *= 2f;
						acceleration *= 0.85f;
						float stopChargeDistance = 800f * base.NPC.localAI[2];
						if (stopChargeDistance < 0f)
						{
							if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X + stopChargeDistance)
							{
								base.NPC.localAI[1] = 0f;
								calamityGlobalNPC.newAI[0] = 0f;
							}
						}
						else if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X + stopChargeDistance)
						{
							base.NPC.localAI[1] = 0f;
							calamityGlobalNPC.newAI[0] = 0f;
						}
					}
					int dustAmt = 5;
					for (int m = 0; m < dustAmt; m++)
					{
						Vector2 val = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)(base.NPC.width + 50) / 2f, (float)base.NPC.height) * 0.75f).RotatedBy((double)(m - (dustAmt / 2 - 1)) * 3.1415927410125732 / (double)(float)dustAmt) + base.NPC.Center;
						Vector2 randDustMovement = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
						int bluishDust = Dust.NewDust(val + randDustMovement, 0, 0, 206, randDustMovement.X, randDustMovement.Y, 100, default(Color), 3f);
						Main.dust[bluishDust].noGravity = true;
						Main.dust[bluishDust].noLight = true;
						Dust obj3 = Main.dust[bluishDust];
						obj3.velocity /= 4f;
						Dust obj4 = Main.dust[bluishDust];
						obj4.velocity -= base.NPC.velocity;
					}
				}
				else if (base.NPC.localAI[3] > 0f)
				{
					base.NPC.localAI[3]--;
				}
			}
		}
		if (Main.getGoodWorld)
		{
			velocity *= 1.4f;
			acceleration *= 1.4f;
		}
		float fasterVelMult = velocity * 1.3f;
		float slowerVelMult = velocity * 0.7f;
		float weaverSpeed = ((Vector2)(ref base.NPC.velocity)).Length();
		if (weaverSpeed > 0f)
		{
			if (weaverSpeed > fasterVelMult)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC = base.NPC;
				nPC.velocity *= fasterVelMult;
			}
			else if (weaverSpeed < slowerVelMult)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC2 = base.NPC;
				nPC2.velocity *= slowerVelMult;
			}
		}
		if (phase2 && !phase4 && base.NPC.localAI[1] == 1f)
		{
			Vector2 soundCenter2 = Main.player[base.NPC.target].Center;
			SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, soundCenter2);
			base.NPC.localAI[1] = 2f;
			if (!phase3 && Main.netMode != 1)
			{
				int speed2 = (revenge ? 8 : 7);
				float spawnX2 = ((base.NPC.Center.X > Main.player[base.NPC.target].Center.X) ? 1000f : (-1000f));
				float spawnY2 = -1000f + Main.player[base.NPC.target].Center.Y;
				Vector2 baseSpawn = default(Vector2);
				((Vector2)(ref baseSpawn))._002Ector(spawnX2 + Main.player[base.NPC.target].Center.X, spawnY2);
				Vector2 baseVelocity = Main.player[base.NPC.target].Center - baseSpawn;
				((Vector2)(ref baseVelocity)).Normalize();
				baseVelocity *= (float)speed2;
				int boltProjectiles = 3;
				for (int num = 0; num < boltProjectiles; num++)
				{
					Vector2 source = baseSpawn;
					source.X += (float)num * 30f - (float)boltProjectiles * 15f;
					Vector2 boltVelocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-140f + 280f * (float)num / (float)boltProjectiles));
					boltVelocity.X = boltVelocity.X + 3f * Main.rand.NextFloat() - 1.5f;
					Vector2 aimDirection = Main.player[base.NPC.target].Center - source;
					float ai = Main.rand.Next(100);
					int type2 = 466;
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), source, boltVelocity, type2, LightningDamage, 0f, Main.myPlayer, aimDirection.ToRotation(), ai);
				}
			}
			if (revenge)
			{
				base.NPC.velocity = Vector2.Normalize(Main.player[base.NPC.target].Center - base.NPC.Center) * (velocity + Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) * 0.01f * (1f - lifeRatio / 0.8f)) * 2f;
			}
			float chargeDirection = 0f;
			if (base.NPC.velocity.X < 0f)
			{
				chargeDirection = -1f;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				chargeDirection = 1f;
			}
			base.NPC.localAI[2] = chargeDirection;
		}
		targetCenterX = (int)(targetCenterX / 16f) * 16;
		targetCenterY = (int)(targetCenterY / 16f) * 16;
		npcCenter.X = (int)(npcCenter.X / 16f) * 16;
		npcCenter.Y = (int)(npcCenter.Y / 16f) * 16;
		targetCenterX -= npcCenter.X;
		targetCenterY -= npcCenter.Y;
		float targetDistance = (float)Math.Sqrt(targetCenterX * targetCenterX + targetCenterY * targetCenterY);
		float absoluteTargetX = Math.Abs(targetCenterX);
		float absoluteTargetY = Math.Abs(targetCenterY);
		float timeToReachTarget = velocity / targetDistance;
		targetCenterX *= timeToReachTarget;
		targetCenterY *= timeToReachTarget;
		if ((base.NPC.velocity.X > 0f && targetCenterX > 0f) || (base.NPC.velocity.X < 0f && targetCenterX < 0f) || (base.NPC.velocity.Y > 0f && targetCenterY > 0f) || (base.NPC.velocity.Y < 0f && targetCenterY < 0f))
		{
			if (base.NPC.velocity.X < targetCenterX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + acceleration;
			}
			else if (base.NPC.velocity.X > targetCenterX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - acceleration;
			}
			if (base.NPC.velocity.Y < targetCenterY)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + acceleration;
			}
			else if (base.NPC.velocity.Y > targetCenterY)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - acceleration;
			}
			if ((double)Math.Abs(targetCenterY) < (double)velocity * 0.2 && ((base.NPC.velocity.X > 0f && targetCenterX < 0f) || (base.NPC.velocity.X < 0f && targetCenterX > 0f)))
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + acceleration * 2f;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - acceleration * 2f;
				}
			}
			if ((double)Math.Abs(targetCenterX) < (double)velocity * 0.2 && ((base.NPC.velocity.Y > 0f && targetCenterY < 0f) || (base.NPC.velocity.Y < 0f && targetCenterY > 0f)))
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + acceleration * 2f;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X - acceleration * 2f;
				}
			}
		}
		else if (absoluteTargetX > absoluteTargetY)
		{
			if (base.NPC.velocity.X < targetCenterX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + acceleration * 1.1f;
			}
			else if (base.NPC.velocity.X > targetCenterX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - acceleration * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)velocity * 0.5)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + acceleration;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - acceleration;
				}
			}
		}
		else
		{
			if (base.NPC.velocity.Y < targetCenterY)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + acceleration * 1.1f;
			}
			else if (base.NPC.velocity.Y > targetCenterY)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - acceleration * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)velocity * 0.5)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + acceleration;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X - acceleration;
				}
			}
		}
		base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
		if (!phase4)
		{
			return;
		}
		if (!Main.dedServ)
		{
			if (lightningSpeed > 0f)
			{
				this.lightning += lightningSpeed;
				if (this.lightning >= 1f)
				{
					this.lightning = 1f;
					lightningSpeed = 0f;
				}
			}
			else if (this.lightning > 0f)
			{
				this.lightning -= lightningDecay;
			}
		}
		if (this.lightning == 1f)
		{
			SoundEngine.PlaySound(in SoundID.Thunder, base.NPC.Center);
		}
		if (Main.netMode != 1 && (Main.netMode != 0 || !Main.gameMenu) && !(calamityGlobalNPC.newAI[1] > 0f) && !BossRushEvent.BossRushActive)
		{
			CalamityWorld.StartRain(adjustSeverity: true, maxSeverity: true);
			calamityGlobalNPC.newAI[1] = 1f;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<StormWeaverBody>()].Value, 6, 24, 0.8f, Vector2.Zero, 3, 26f, 10f, 0.15f);
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		float num = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = num < 0.8f && !Main.zenithWorld;
		bool num2 = num < 0.55f;
		float chargePhaseGateValue = (death ? 320f : (revenge ? 340f : (expertMode ? 360f : 400f)));
		if (!num2)
		{
			chargePhaseGateValue *= 0.5f;
		}
		Texture2D texture = (phase2 ? Phase2Texture.Value : TextureAssets.Npc[base.Type].Value);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(texture.Width / 2), (float)(texture.Height / 2));
		float chargeTelegraphTime = 120f;
		float chargeTelegraphGateValue = chargePhaseGateValue - chargeTelegraphTime;
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture.Width, (float)texture.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Color drawColorAlpha = base.NPC.GetAlpha(drawColor);
		if (calamityGlobalNPC.newAI[0] > chargeTelegraphGateValue)
		{
			drawColorAlpha = Color.Lerp(drawColorAlpha, Color.Cyan, MathHelper.Clamp((calamityGlobalNPC.newAI[0] - chargeTelegraphGateValue) / chargeTelegraphTime, 0f, 1f));
		}
		else if (base.NPC.localAI[3] > 0f)
		{
			drawColorAlpha = Color.Lerp(drawColorAlpha, Color.Cyan, MathHelper.Clamp(base.NPC.localAI[3] / 60f, 0f, 1f));
		}
		spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, drawColorAlpha, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool num = (float)base.NPC.life / (float)base.NPC.lifeMax < 0.55f;
		float chargePhaseGateValue = (death ? 320f : (revenge ? 340f : (expertMode ? 360f : 400f)));
		if (!num)
		{
			chargePhaseGateValue *= 0.5f;
		}
		int buffDuration = ((base.NPC.Calamity().newAI[0] >= chargePhaseGateValue) ? 360 : 240);
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(144, buffDuration);
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SWNudeHead1").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SWNudeHead2").Type, base.NPC.scale);
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = (int)(50f * base.NPC.scale);
		base.NPC.height = (int)(50f * base.NPC.scale);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 20; i++)
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
		for (int j = 0; j < 40; j++)
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

	public override bool CheckDead()
	{
		for (int k = 0; k < Main.maxNPCs; k++)
		{
			if (Main.npc[k].active && (Main.npc[k].type == ModContent.NPCType<StormWeaverBody>() || Main.npc[k].type == ModContent.NPCType<StormWeaverTail>()))
			{
				Main.npc[k].life = 0;
			}
		}
		return true;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<SupremeHealingPotion>();
	}

	public override bool SpecialOnKill()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<StormWeaverHead>(), ModContent.NPCType<StormWeaverBody>(), ModContent.NPCType<StormWeaverTail>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public static bool LastSentinelKilled()
	{
		if (!DownedBossSystem.downedSignus && DownedBossSystem.downedStormWeaver)
		{
			return DownedBossSystem.downedCeaselessVoid;
		}
		return false;
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			DownedBossSystem.downedStormWeaver = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<StormWeaverBag>()));
		LeadingConditionRule normalOnly = new LeadingConditionRule(new Conditions.NotExpert());
		npcLoot.Add(normalOnly);
		int[] weapons = new int[3]
		{
			ModContent.ItemType<SkytideDragoon>(),
			ModContent.ItemType<TheStorm>(),
			ModContent.ItemType<Volterion>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<ArmoredShell>(), 1, 10, 12));
		normalOnly.Add(ModContent.ItemType<StormWeaverMask>(), 7);
		normalOnly.Add(ModContent.ItemType<LittleLight>(), 10);
		IItemDropRule godSlayerVanity = ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerHelm>(), 20);
		godSlayerVanity.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerChestplate>()));
		godSlayerVanity.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerLeggings>()));
		normalOnly.Add(godSlayerVanity);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<WeaverTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<StormWeaverRelic>());
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<ElementalGauntlet>()), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<PlanebreakersPouch>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedStormWeaver, ModContent.ItemType<LoreStormWeaver>(), ui: true, DropHelper.FirstKillText);
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
