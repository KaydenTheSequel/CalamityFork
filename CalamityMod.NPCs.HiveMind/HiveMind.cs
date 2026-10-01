using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Boss;
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

namespace CalamityMod.NPCs.HiveMind;

public class HiveMind : ModNPC
{
	public static int normalIconIndex;

	public static int phase2IconIndex;

	private int burrowTimer = 120;

	private int minimumDriftTime = 300;

	private int teleportRadius = 300;

	private int decelerationTime = 30;

	private int reelbackFade = 2;

	private float arcTime = 45f;

	private float driftSpeed = 1f;

	private float driftBoost = 1f;

	private int lungeDelay = 90;

	private int lungeTime = 33;

	private int lungeFade = 15;

	private int lungeAmount = 1;

	private int lungesPerformed;

	private bool performingLungeCombo;

	private double lungeRots = 0.2;

	private bool dashStarted;

	private int rainDashAmount = 1;

	private int rainDashesPerformed;

	private bool performingRainDashCombo;

	private int phase2timer = 360;

	private int rotationDirection;

	private double rotation;

	private double rotationIncrement;

	private int state;

	private int previousState;

	private int nextState;

	private int reelCount;

	private Vector2 deceleration;

	private int frameX;

	private int frameY;

	private int frameWidth;

	private int frameHeight;

	private int maxFrameX;

	private int maxFrameY;

	public static readonly SoundStyle RoarSound = new SoundStyle("CalamityMod/Sounds/Custom/HiveMindRoar");

	public static readonly SoundStyle FastRoarSound = new SoundStyle("CalamityMod/Sounds/Custom/HiveMindRoarFast");

	public static Asset<Texture2D> Phase2Texture;

	private const int framesX_P1 = 1;

	private const int framesY_P1 = 16;

	private const int framesX_P2 = 2;

	private const int framesY_P2 = 8;

	private const int frameWidth_P1 = 178;

	private const int frameHeight_P1 = 122;

	private const int frameWidth_P2 = 178;

	private const int frameHeight_P2 = 142;

	public static int ShaderainDamage = 12;

	private bool IsPhaseTwo => (float)base.NPC.life / (float)base.NPC.lifeMax < 0.8f;

	public override void Load()
	{
		string normalIconPath = "CalamityMod/NPCs/HiveMind/HiveMind_Head_Boss";
		string phase2IconPath = "CalamityMod/NPCs/HiveMind/HiveMindP2_Head_Boss";
		normalIconIndex = CalamityMod.Instance.AddBossHeadTexture(normalIconPath);
		phase2IconIndex = CalamityMod.Instance.AddBossHeadTexture(phase2IconPath);
	}

	public override void SetStaticDefaults()
	{
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.TrailCacheLength[base.Type] = base.NPC.oldPos.Length;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.4f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 3f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 3f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			Phase2Texture = ModContent.Request<Texture2D>(Texture + "P2", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 40;
		base.NPC.npcSlots = 5f;
		base.NPC.width = 178;
		base.NPC.height = 122;
		base.NPC.defense = 8;
		base.NPC.LifeMaxNERB(5000, 7000, 350000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 5);
		base.NPC.boss = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		bool num = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (num)
		{
			minimumDriftTime = 120;
			reelbackFade = 4;
		}
		if (revenge)
		{
			lungeRots = 0.3;
			minimumDriftTime = 90;
			reelbackFade = 5;
			lungeTime = 28;
			driftSpeed = 2f;
			driftBoost = 2f;
		}
		if (death)
		{
			lungeRots = 0.4;
			lungeAmount = 3;
			rainDashAmount = 2;
			minimumDriftTime = 60;
			reelbackFade = 6;
			lungeTime = 23;
			driftSpeed = 3.5f;
			driftBoost = 1.5f;
		}
		if (Main.getGoodWorld)
		{
			reelbackFade *= 10;
			arcTime *= 0.5f;
		}
		phase2timer = minimumDriftTime;
		rotationIncrement = 0.0246399424 * lungeRots * (double)lungeFade;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.HiveMind")
		});
	}

	public override void BossHeadSlot(ref int index)
	{
		index = (IsPhaseTwo ? phase2IconIndex : normalIconIndex);
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.noTileCollide);
		writer.Write(base.NPC.noGravity);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[3]);
		writer.Write(burrowTimer);
		writer.Write(state);
		writer.Write(nextState);
		writer.Write(phase2timer);
		writer.Write(dashStarted);
		writer.Write(rotationDirection);
		writer.Write(rotation);
		writer.Write(previousState);
		writer.Write(reelCount);
		writer.Write(lungesPerformed);
		writer.Write(performingLungeCombo);
		writer.Write(rainDashesPerformed);
		writer.Write(performingRainDashCombo);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.noTileCollide = reader.ReadBoolean();
		base.NPC.noGravity = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		burrowTimer = reader.ReadInt32();
		state = reader.ReadInt32();
		nextState = reader.ReadInt32();
		phase2timer = reader.ReadInt32();
		dashStarted = reader.ReadBoolean();
		rotationDirection = reader.ReadInt32();
		rotation = reader.ReadDouble();
		previousState = reader.ReadInt32();
		reelCount = reader.ReadInt32();
		lungesPerformed = reader.ReadInt32();
		performingLungeCombo = reader.ReadBoolean();
		rainDashesPerformed = reader.ReadInt32();
		performingRainDashCombo = reader.ReadBoolean();
	}

	public override void FindFrame(int _)
	{
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		maxFrameX = 1;
		maxFrameY = 16;
		frameWidth = 178;
		frameHeight = 122;
		if (IsPhaseTwo)
		{
			maxFrameX = 2;
			maxFrameY = 8;
			frameWidth = 178;
			frameHeight = 142;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 6.0)
		{
			base.NPC.frameCounter = 0.0;
			frameY++;
			frameY %= maxFrameY;
			if (frameY == 0)
			{
				frameX++;
				frameX %= maxFrameX;
			}
		}
		base.NPC.frame = new Rectangle(frameWidth * frameX, frameHeight * frameY, base.NPC.width, base.NPC.height);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return true;
		}
		Texture2D texture = (IsPhaseTwo ? Phase2Texture.Value : TextureAssets.Npc[base.Type].Value);
		SpriteEffects spriteEffects = (SpriteEffects)(base.NPC.direction == 1);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(base.NPC.width / 2), (float)base.NPC.height);
		Vector2 center = base.NPC.position - screenPos + origin;
		if (CalamityClientConfig.Instance.Afterimages && state != 0 && IsPhaseTwo)
		{
			Color afterimageBaseColor = Color.White;
			int numAfterimages = 5;
			for (int i = 1; i < numAfterimages; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, afterimageBaseColor, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(numAfterimages - i) / 15f;
				Vector2 afterimageCenter = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageCenter -= new Vector2((float)texture.Width, (float)texture.Height) / new Vector2((float)maxFrameX, (float)maxFrameY) * base.NPC.scale / 2f;
				afterimageCenter += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture, afterimageCenter, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.oldRot[i], origin, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture, center, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	private void SpawnStuff()
	{
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		int maxSpawns = (death ? Main.rand.Next(3, 5) : (revenge ? 3 : (expertMode ? Main.rand.Next(2, 4) : 2)));
		for (int i = 0; i < maxSpawns; i++)
		{
			int choice = -1;
			int type;
			do
			{
				choice++;
				type = choice switch
				{
					0 => (!NPC.AnyNPCs(6)) ? 6 : 0, 
					1 => (!NPC.AnyNPCs(7)) ? 7 : 0, 
					2 => (!NPC.AnyNPCs(ModContent.NPCType<DankCreeper>())) ? ModContent.NPCType<DankCreeper>() : 0, 
					3 => (!NPC.AnyNPCs(94) & death) ? 94 : 0, 
					_ => -1, 
				};
			}
			while (type == 0);
			if (type > 0)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + Main.rand.Next(base.NPC.width), (int)base.NPC.position.Y + Main.rand.Next(base.NPC.height), type);
			}
		}
		if (Main.zenithWorld && NPC.CountNPCS(ModContent.NPCType<HiveTumor>()) < 3)
		{
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + Main.rand.Next(base.NPC.width), (int)base.NPC.position.Y + Main.rand.Next(base.NPC.height), ModContent.NPCType<HiveTumor>());
		}
	}

	private void ReelBack()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		bool num = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		base.NPC.alpha = 0;
		phase2timer = 0;
		deceleration = base.NPC.velocity / 255f * (float)reelbackFade;
		if (num)
		{
			state = 2;
			SoundEngine.PlaySound(in FastRoarSound, base.NPC.Center);
		}
		else
		{
			if (Main.netMode != 1 && base.NPC.Distance(Main.player[base.NPC.target].Center) > 80f)
			{
				SpawnStuff();
			}
			state = nextState;
			nextState = 0;
			if (state == 2)
			{
				SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
			}
			else
			{
				SoundEngine.PlaySound(in FastRoarSound, base.NPC.Center);
			}
		}
		base.NPC.netUpdate = true;
	}

	public override void AI()
	{
		//IL_158d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1593: Unknown result type (might be due to invalid IL or missing references)
		//IL_1598: Unknown result type (might be due to invalid IL or missing references)
		//IL_159d: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1826: Unknown result type (might be due to invalid IL or missing references)
		//IL_182b: Unknown result type (might be due to invalid IL or missing references)
		//IL_190e: Unknown result type (might be due to invalid IL or missing references)
		//IL_191f: Unknown result type (might be due to invalid IL or missing references)
		//IL_192c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1932: Unknown result type (might be due to invalid IL or missing references)
		//IL_1934: Unknown result type (might be due to invalid IL or missing references)
		//IL_1939: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ccd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_1edc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1abb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a77: Unknown result type (might be due to invalid IL or missing references)
		//IL_1306: Unknown result type (might be due to invalid IL or missing references)
		//IL_130c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d59: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f35: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_1366: Unknown result type (might be due to invalid IL or missing references)
		//IL_136c: Unknown result type (might be due to invalid IL or missing references)
		//IL_16dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_170e: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_20c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_203e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2028: Unknown result type (might be due to invalid IL or missing references)
		//IL_1476: Unknown result type (might be due to invalid IL or missing references)
		//IL_1481: Unknown result type (might be due to invalid IL or missing references)
		//IL_1486: Unknown result type (might be due to invalid IL or missing references)
		//IL_148b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2309: Unknown result type (might be due to invalid IL or missing references)
		//IL_2334: Unknown result type (might be due to invalid IL or missing references)
		//IL_233a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2351: Unknown result type (might be due to invalid IL or missing references)
		//IL_235b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2360: Unknown result type (might be due to invalid IL or missing references)
		//IL_2043: Unknown result type (might be due to invalid IL or missing references)
		//IL_2051: Unknown result type (might be due to invalid IL or missing references)
		//IL_2053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b70: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0774: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d59: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1756: Unknown result type (might be due to invalid IL or missing references)
		//IL_175e: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_241c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2426: Unknown result type (might be due to invalid IL or missing references)
		//IL_242b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2436: Unknown result type (might be due to invalid IL or missing references)
		//IL_2461: Unknown result type (might be due to invalid IL or missing references)
		//IL_2467: Unknown result type (might be due to invalid IL or missing references)
		//IL_247e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2488: Unknown result type (might be due to invalid IL or missing references)
		//IL_248d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0830: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_1548: Unknown result type (might be due to invalid IL or missing references)
		//IL_1553: Unknown result type (might be due to invalid IL or missing references)
		//IL_1558: Unknown result type (might be due to invalid IL or missing references)
		//IL_151d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1532: Unknown result type (might be due to invalid IL or missing references)
		//IL_1537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1014: Unknown result type (might be due to invalid IL or missing references)
		//IL_101e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1023: Unknown result type (might be due to invalid IL or missing references)
		//IL_103e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1048: Unknown result type (might be due to invalid IL or missing references)
		//IL_1075: Unknown result type (might be due to invalid IL or missing references)
		//IL_107b: Unknown result type (might be due to invalid IL or missing references)
		//IL_109e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ad: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.width = frameWidth;
		base.NPC.height = frameHeight - 2;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (IsPhaseTwo)
		{
			if (base.NPC.localAI[1] == 0f)
			{
				base.NPC.localAI[1] = 1f;
				if (!Main.dedServ)
				{
					int goreAmount = 7;
					for (int i = 1; i <= goreAmount; i++)
					{
						Gore.NewGore(base.NPC.GetSource_FromAI(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("HiveMindGore" + i).Type);
					}
				}
				for (int j = 0; j < 20; j++)
				{
					int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
					Dust obj = Main.dust[dust];
					obj.velocity *= 3f;
					if (Main.rand.NextBool())
					{
						Main.dust[dust].scale = 0.5f;
						Main.dust[dust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int k = 0; k < 35; k++)
				{
					int dust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dust2].noGravity = true;
					Dust obj2 = Main.dust[dust2];
					obj2.velocity *= 5f;
					dust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
					Dust obj3 = Main.dust[dust2];
					obj3.velocity *= 2f;
				}
				SoundEngine.PlaySound(in SoundID.NPCDeath1, base.NPC.Center);
				base.NPC.position = base.NPC.Center;
				NPC nPC = base.NPC;
				nPC.position -= base.NPC.Size * 0.5f;
				base.NPC.noGravity = true;
				base.NPC.noTileCollide = true;
				base.NPC.scale = 1f;
				base.NPC.alpha = 0;
				base.NPC.dontTakeDamage = false;
				base.NPC.damage = 0;
				base.NPC.ForceNetUpdate();
			}
			switch (state)
			{
			case 0:
				base.NPC.damage = base.NPC.defDamage;
				if (base.NPC.alpha > 0)
				{
					base.NPC.alpha -= 3;
					if (base.NPC.alpha < 0)
					{
						base.NPC.alpha = 0;
					}
				}
				if (nextState == 0)
				{
					base.NPC.TargetClosest();
					if (revenge && lifeRatio < 0.6f)
					{
						if (death)
						{
							do
							{
								nextState = Main.rand.Next(3, 6);
							}
							while (nextState == previousState);
							previousState = nextState;
						}
						else if (lifeRatio < 0.3f)
						{
							do
							{
								nextState = Main.rand.Next(3, 6);
							}
							while (nextState == previousState);
							previousState = nextState;
						}
						else
						{
							do
							{
								nextState = Main.rand.Next(3, 5);
							}
							while (nextState == previousState);
							previousState = nextState;
						}
					}
					else if (revenge && (Main.rand.NextBool(3) || reelCount == 2))
					{
						reelCount = 0;
						nextState = 2;
					}
					else
					{
						reelCount++;
						if (expertMode && reelCount == 2)
						{
							reelCount = 0;
							nextState = 2;
						}
						else
						{
							nextState = 1;
						}
						base.NPC.ai[1] = 0f;
						base.NPC.ai[2] = 0f;
					}
					if (nextState == 3)
					{
						rotation = MathHelper.ToRadians((float)Main.rand.Next(360));
					}
					base.NPC.ForceNetUpdate();
				}
				if (!player.active || player.dead || Vector2.Distance(base.NPC.Center, player.Center) > 8000f || (!player.ZoneCorrupt && !BossRushEvent.BossRushActive))
				{
					base.NPC.TargetClosest(faceTarget: false);
					player = Main.player[base.NPC.target];
					if (!player.active || player.dead || Vector2.Distance(base.NPC.Center, player.Center) > 8000f || (!player.ZoneCorrupt && !BossRushEvent.BossRushActive))
					{
						if (base.NPC.timeLeft > 60)
						{
							base.NPC.timeLeft = 60;
						}
						if (base.NPC.localAI[3] < 120f)
						{
							base.NPC.localAI[3]++;
						}
						if (base.NPC.localAI[3] > 60f)
						{
							base.NPC.velocity.Y += (base.NPC.localAI[3] - 60f) * 0.5f;
						}
						return;
					}
				}
				else if (base.NPC.timeLeft < 1800)
				{
					base.NPC.timeLeft = 1800;
				}
				if (base.NPC.localAI[3] > 0f)
				{
					base.NPC.localAI[3]--;
					return;
				}
				base.NPC.velocity = player.Center - base.NPC.Center;
				phase2timer--;
				if (death && base.NPC.justHit)
				{
					phase2timer -= 4;
				}
				if (phase2timer <= -180)
				{
					NPC nPC6 = base.NPC;
					nPC6.velocity *= 0.007843138f * (float)reelbackFade;
					ReelBack();
					base.NPC.ForceNetUpdate();
					break;
				}
				((Vector2)(ref base.NPC.velocity)).Normalize();
				if (expertMode)
				{
					NPC nPC7 = base.NPC;
					nPC7.velocity *= driftSpeed + driftBoost * lifeRatio;
				}
				else
				{
					NPC nPC8 = base.NPC;
					nPC8.velocity *= driftSpeed;
				}
				break;
			case 1:
			{
				base.NPC.damage = 0;
				base.NPC.alpha += reelbackFade;
				NPC nPC5 = base.NPC;
				nPC5.velocity -= deceleration;
				if (base.NPC.alpha >= 255)
				{
					base.NPC.alpha = 255;
					base.NPC.velocity = Vector2.Zero;
					state = 0;
					if (Main.netMode != 1 && base.NPC.ai[1] != 0f && base.NPC.ai[2] != 0f)
					{
						base.NPC.position.X = base.NPC.ai[1] * 16f - (float)(base.NPC.width / 2);
						base.NPC.position.Y = base.NPC.ai[2] * 16f - (float)(base.NPC.height / 2);
					}
					phase2timer = minimumDriftTime + Main.rand.Next(death ? 61 : 121);
					base.NPC.ForceNetUpdate();
				}
				else
				{
					if (base.NPC.ai[1] != 0f || base.NPC.ai[2] != 0f)
					{
						break;
					}
					for (int l = 0; l < 10; l++)
					{
						int posX = (int)player.Center.X / 16 + Main.rand.Next(15, 46) * ((!Main.rand.NextBool()) ? 1 : (-1));
						int posY = (int)player.Center.Y / 16 + Main.rand.Next(15, 46) * ((!Main.rand.NextBool()) ? 1 : (-1));
						if (!WorldGen.SolidTile(posX, posY) && Collision.CanHit(new Vector2((float)(posX * 16), (float)(posY * 16)), 1, 1, player.position, player.width, player.height))
						{
							base.NPC.ai[1] = posX;
							base.NPC.ai[2] = posY;
							base.NPC.ForceNetUpdate();
							break;
						}
					}
				}
				break;
			}
			case 2:
			{
				base.NPC.damage = 0;
				base.NPC.alpha += reelbackFade;
				NPC nPC3 = base.NPC;
				nPC3.velocity -= deceleration;
				if (base.NPC.alpha < 255)
				{
					break;
				}
				base.NPC.alpha = 255;
				base.NPC.velocity = Vector2.Zero;
				dashStarted = false;
				if (revenge && lifeRatio < 0.6f && lungesPerformed == 0 && rainDashesPerformed == 0)
				{
					state = nextState;
					nextState = 0;
					previousState = state;
				}
				else
				{
					state = (performingRainDashCombo ? 5 : 3);
				}
				if (!performingRainDashCombo)
				{
					if (player.velocity.X > 0f)
					{
						rotationDirection = 1;
					}
					else if (player.velocity.X < 0f)
					{
						rotationDirection = -1;
					}
					else
					{
						rotationDirection = player.direction;
					}
				}
				break;
			}
			case 3:
				base.NPC.damage = 0;
				base.NPC.ForceNetUpdate();
				if (base.NPC.alpha > 0)
				{
					base.NPC.Center = player.Center + Utils.RotatedBy(new Vector2((float)teleportRadius, 0f), rotation, default(Vector2));
					rotation += rotationIncrement * (double)rotationDirection;
					phase2timer = ((lungesPerformed > 0) ? (lungeDelay / (lungesPerformed + 1)) : lungeDelay);
					base.NPC.alpha -= lungeFade;
					if (base.NPC.alpha < 0)
					{
						base.NPC.alpha = 0;
					}
					break;
				}
				phase2timer--;
				if (!dashStarted)
				{
					if (phase2timer <= 0)
					{
						base.NPC.damage = base.NPC.defDamage;
						phase2timer = lungeTime;
						base.NPC.velocity = player.Center - base.NPC.Center;
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC10 = base.NPC;
						nPC10.velocity *= (float)(teleportRadius / lungeTime);
						dashStarted = true;
						SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
						base.NPC.netUpdate = true;
					}
					else
					{
						base.NPC.Center = player.Center + Utils.RotatedBy(new Vector2((float)teleportRadius, 0f), rotation, default(Vector2));
						rotation += rotationIncrement * (double)rotationDirection * (double)phase2timer / (double)lungeDelay;
					}
				}
				else
				{
					base.NPC.damage = base.NPC.defDamage;
					if (phase2timer <= 0)
					{
						base.NPC.damage = 0;
						performingLungeCombo = death;
						state = 6;
						phase2timer = 0;
						deceleration = base.NPC.velocity / (float)decelerationTime;
					}
				}
				break;
			case 4:
				base.NPC.damage = 0;
				if (base.NPC.alpha > 0)
				{
					if (Main.netMode != 1)
					{
						base.NPC.Center = player.Center;
						base.NPC.position.Y += teleportRadius;
					}
					base.NPC.alpha -= 5;
					if (base.NPC.alpha < 0)
					{
						base.NPC.alpha = 0;
					}
					base.NPC.ForceNetUpdate();
					break;
				}
				if (!dashStarted)
				{
					base.NPC.damage = base.NPC.defDamage;
					dashStarted = true;
					SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
					base.NPC.velocity.X = (float)Math.PI * (float)teleportRadius / arcTime;
					NPC nPC4 = base.NPC;
					nPC4.velocity *= (float)rotationDirection;
					base.NPC.ForceNetUpdate();
					break;
				}
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.velocity = base.NPC.velocity.RotatedBy((float)Math.PI / arcTime * (float)(-rotationDirection));
				phase2timer++;
				if (phase2timer != (int)(arcTime / 6f))
				{
					break;
				}
				phase2timer = 0;
				base.NPC.ai[0]++;
				if (base.NPC.ai[0] == 6f)
				{
					base.NPC.velocity = base.NPC.velocity.RotatedBy((float)Math.PI / arcTime * (float)(-rotationDirection));
					if (base.NPC.Distance(Main.player[base.NPC.target].Center) > 80f)
					{
						SpawnStuff();
					}
					state = 6;
					base.NPC.ai[0] = 0f;
					deceleration = base.NPC.velocity / (float)decelerationTime;
				}
				break;
			case 5:
				base.NPC.damage = 0;
				if (base.NPC.alpha > 0)
				{
					if (Main.netMode != 1)
					{
						base.NPC.Center = player.Center;
						base.NPC.position.Y -= teleportRadius;
						base.NPC.position.X += (death ? ((float)teleportRadius * 1.5f) : ((float)teleportRadius)) * (float)((rainDashesPerformed == 0) ? rotationDirection : (-rotationDirection));
					}
					base.NPC.alpha -= 5;
					if (base.NPC.alpha < 0)
					{
						base.NPC.alpha = 0;
					}
					base.NPC.ForceNetUpdate();
					break;
				}
				if (!dashStarted)
				{
					base.NPC.damage = base.NPC.defDamage;
					dashStarted = true;
					SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
					base.NPC.velocity.X = (float)teleportRadius / arcTime * 3f;
					NPC nPC9 = base.NPC;
					nPC9.velocity *= (float)((rainDashesPerformed == 0) ? (-rotationDirection) : rotationDirection);
					base.NPC.ForceNetUpdate();
					break;
				}
				base.NPC.damage = base.NPC.defDamage;
				phase2timer++;
				if (phase2timer % 30 == (int)(arcTime / (death ? 15f : 20f)))
				{
					phase2timer = 0;
					base.NPC.ai[0]++;
					if (Main.netMode != 1)
					{
						int type = ModContent.ProjectileType<ShadeNimbusHostile>();
						Vector2 cloudSpawnPos = default(Vector2);
						((Vector2)(ref cloudSpawnPos))._002Ector(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width), base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height));
						Vector2 randomVelocity = (Main.getGoodWorld ? Main.rand.NextVector2CircularEdge(4f, 4f) : Vector2.Zero);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), cloudSpawnPos, randomVelocity, type, ShaderainDamage, 0f, Main.myPlayer, 11f);
					}
					if (base.NPC.ai[0] == 10f)
					{
						performingRainDashCombo = death;
						state = 6;
						base.NPC.ai[0] = 0f;
						deceleration = base.NPC.velocity / (float)decelerationTime;
					}
				}
				break;
			case 6:
			{
				base.NPC.damage = 0;
				NPC nPC2 = base.NPC;
				nPC2.velocity -= deceleration;
				phase2timer++;
				if (phase2timer != decelerationTime)
				{
					break;
				}
				if (performingRainDashCombo)
				{
					rainDashesPerformed++;
					if (rainDashesPerformed < rainDashAmount)
					{
						state = 2;
					}
					else
					{
						phase2timer = minimumDriftTime + Main.rand.Next(death ? 61 : 121);
						performingRainDashCombo = false;
						rainDashesPerformed = 0;
						state = 0;
					}
				}
				else if (performingLungeCombo)
				{
					lungesPerformed++;
					if (lungesPerformed < lungeAmount)
					{
						state = 2;
					}
					else
					{
						phase2timer = minimumDriftTime + Main.rand.Next(death ? 61 : 121);
						performingLungeCombo = false;
						lungesPerformed = 0;
						state = 0;
					}
				}
				else
				{
					phase2timer = minimumDriftTime + Main.rand.Next(death ? 61 : 121);
					state = 0;
				}
				base.NPC.ForceNetUpdate();
				break;
			}
			}
			if (!((base.NPC.life > 0) & expertMode) || !(lifeRatio < 0.5f))
			{
				return;
			}
			if (base.NPC.ai[3] > (float)base.NPC.lifeMax * 0.6f)
			{
				base.NPC.ai[3] = (float)base.NPC.lifeMax * 0.6f;
			}
			int tenPercentHP = (int)((double)base.NPC.lifeMax * 0.1);
			if (!((float)(base.NPC.life + tenPercentHP) < base.NPC.ai[3]))
			{
				return;
			}
			base.NPC.ai[3] = base.NPC.life;
			if (NPC.AnyNPCs(ModContent.NPCType<DarkHeart>()))
			{
				return;
			}
			SoundEngine.PlaySound(in SoundID.NPCDeath22, base.NPC.Center);
			for (int m = 0; m < 20; m++)
			{
				int dust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
				Dust obj4 = Main.dust[dust3];
				obj4.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[dust3].scale = 0.5f;
					Main.dust[dust3].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int n = 0; n < 35; n++)
			{
				int dust4 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dust4].noGravity = true;
				Dust obj5 = Main.dust[dust4];
				obj5.velocity *= 5f;
				dust4 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
				Dust obj6 = Main.dust[dust4];
				obj6.velocity *= 2f;
			}
			if (Main.netMode != 1)
			{
				int x = (int)(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width));
				int y = (int)(base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height));
				int type2 = ModContent.NPCType<DarkHeart>();
				int tenPercentMinions = NPC.NewNPC(base.NPC.GetSource_FromAI(), x, y, type2);
				Main.npc[tenPercentMinions].SetDefaults(type2);
				if (Main.dedServ && tenPercentMinions < Main.maxNPCs)
				{
					NetMessage.SendData(23, -1, -1, null, tenPercentMinions);
				}
			}
			return;
		}
		base.NPC.damage = 0;
		CalamityGlobalNPC.hiveMind = base.NPC.whoAmI;
		if (!player.active || player.dead || (!player.ZoneCorrupt && !BossRushEvent.BossRushActive))
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || (!player.ZoneCorrupt && !BossRushEvent.BossRushActive))
			{
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				if (base.NPC.localAI[3] < 120f)
				{
					base.NPC.localAI[3]++;
				}
				if (base.NPC.localAI[3] > 60f)
				{
					base.NPC.velocity.Y += (base.NPC.localAI[3] - 60f) * 0.5f;
					base.NPC.noGravity = true;
					base.NPC.noTileCollide = true;
					if (burrowTimer > 30)
					{
						burrowTimer = 30;
					}
				}
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (base.NPC.localAI[3] > 0f)
		{
			base.NPC.localAI[3]--;
			return;
		}
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		if (Main.netMode != 1 && base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			int maxBlobs = (death ? 15 : (revenge ? 7 : (expertMode ? 6 : 5)));
			if (Main.getGoodWorld)
			{
				maxBlobs *= 2;
			}
			if (Main.zenithWorld)
			{
				maxBlobs = 50;
			}
			for (int num = 0; num < maxBlobs; num++)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<HiveBlob>(), base.NPC.whoAmI, 0f, 0f, Main.rand.Next(2));
			}
		}
		if (base.NPC.ai[3] == 0f && base.NPC.life > 0)
		{
			base.NPC.ai[3] = base.NPC.lifeMax;
		}
		if (base.NPC.life > 0)
		{
			int fivePercentHP = (int)((double)base.NPC.lifeMax * 0.05);
			if ((float)(base.NPC.life + fivePercentHP) < base.NPC.ai[3])
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath22, base.NPC.Center);
				base.NPC.ai[3] = base.NPC.life;
				for (int num2 = 0; num2 < 20; num2++)
				{
					int dust5 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
					Dust obj7 = Main.dust[dust5];
					obj7.velocity *= 3f;
					if (Main.rand.NextBool())
					{
						Main.dust[dust5].scale = 0.5f;
						Main.dust[dust5].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num3 = 0; num3 < 35; num3++)
				{
					int dust6 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dust6].noGravity = true;
					Dust obj8 = Main.dust[dust6];
					obj8.velocity *= 5f;
					dust6 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
					Dust obj9 = Main.dust[dust6];
					obj9.velocity *= 2f;
				}
				if (Main.netMode != 1)
				{
					int maxSpawns = (death ? 5 : (revenge ? 4 : (expertMode ? Main.rand.Next(3, 5) : Main.rand.Next(2, 4))));
					int maxDankSpawns = (death ? Main.rand.Next(2, 4) : (revenge ? 2 : ((!expertMode) ? 1 : Main.rand.Next(1, 3))));
					for (int num4 = 0; num4 < maxSpawns; num4++)
					{
						int x2 = (int)(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width - 32));
						int y2 = (int)(base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height - 32));
						int type3 = ModContent.NPCType<HiveBlob>();
						if (NPC.CountNPCS(ModContent.NPCType<DankCreeper>()) < maxDankSpawns)
						{
							type3 = ModContent.NPCType<DankCreeper>();
						}
						int fivePercentMinions = NPC.NewNPC(base.NPC.GetSource_FromAI(), x2, y2, type3);
						Main.npc[fivePercentMinions].SetDefaults(type3);
						if (Main.dedServ && fivePercentMinions < Main.maxNPCs)
						{
							NetMessage.SendData(23, -1, -1, null, fivePercentMinions);
						}
					}
					return;
				}
			}
		}
		burrowTimer--;
		if (burrowTimer < -120)
		{
			burrowTimer = (death ? 180 : (revenge ? 300 : (expertMode ? 360 : 420)));
			if (burrowTimer < 30)
			{
				burrowTimer = 30;
			}
			base.NPC.scale = 1f;
			base.NPC.alpha = 0;
			base.NPC.dontTakeDamage = false;
		}
		else if (burrowTimer < -60)
		{
			base.NPC.scale += 0.0165f;
			base.NPC.alpha -= 4;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
			int burrowedDust = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.Center.Y), base.NPC.width, base.NPC.height / 2, 14, 0f, -3f, 100, default(Color), 2.5f * base.NPC.scale);
			Dust obj10 = Main.dust[burrowedDust];
			obj10.velocity *= 2f;
			if (Main.rand.NextBool())
			{
				Main.dust[burrowedDust].scale = 0.5f;
				Main.dust[burrowedDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
			for (int num5 = 0; num5 < 2; num5++)
			{
				int burrowedDust2 = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.Center.Y), base.NPC.width, base.NPC.height / 2, 14, 0f, -3f, 100, default(Color), 3.5f * base.NPC.scale);
				Main.dust[burrowedDust2].noGravity = true;
				Dust obj11 = Main.dust[burrowedDust2];
				obj11.velocity *= 3.5f;
				burrowedDust2 = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.Center.Y), base.NPC.width, base.NPC.height / 2, 14, 0f, -3f, 100, default(Color), 2.5f * base.NPC.scale);
				Dust obj12 = Main.dust[burrowedDust2];
				obj12.velocity *= 1f;
			}
		}
		else if (burrowTimer == -60)
		{
			base.NPC.scale = 0.01f;
			if (Main.netMode != 1)
			{
				base.NPC.Center = player.Center;
				base.NPC.position.Y = player.position.Y - (float)base.NPC.height;
				int tilePosX = (int)base.NPC.Center.X / 16;
				int tilePosY = (int)(base.NPC.position.Y + (float)base.NPC.height) / 16 + 1;
				while (!Main.tile[tilePosX, tilePosY].HasUnactuatedTile || !Main.tileSolid[Main.tile[tilePosX, tilePosY].TileType])
				{
					tilePosY++;
					base.NPC.position.Y += 16f;
				}
				for (int num6 = 0; num6 < Main.maxNPCs; num6++)
				{
					NPC hiveBlob = Main.npc[num6];
					if (hiveBlob.active && hiveBlob.type == ModContent.NPCType<HiveBlob>())
					{
						hiveBlob.position.X = base.NPC.position.X;
						hiveBlob.position.Y = base.NPC.position.Y;
					}
				}
			}
			base.NPC.ForceNetUpdate();
		}
		else if (burrowTimer < 0)
		{
			base.NPC.scale -= 0.0165f;
			base.NPC.alpha += 4;
			if (base.NPC.alpha > 255)
			{
				base.NPC.alpha = 255;
			}
			int burrowedDust3 = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.Center.Y), base.NPC.width, base.NPC.height / 2, 14, 0f, -3f, 100, default(Color), 2.5f * base.NPC.scale);
			Dust obj13 = Main.dust[burrowedDust3];
			obj13.velocity *= 2f;
			if (Main.rand.NextBool())
			{
				Main.dust[burrowedDust3].scale = 0.5f;
				Main.dust[burrowedDust3].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
			for (int num7 = 0; num7 < 2; num7++)
			{
				int burrowedDust4 = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.Center.Y), base.NPC.width, base.NPC.height / 2, 14, 0f, -3f, 100, default(Color), 3.5f * base.NPC.scale);
				Main.dust[burrowedDust4].noGravity = true;
				Dust obj14 = Main.dust[burrowedDust4];
				obj14.velocity *= 3.5f;
				burrowedDust4 = Dust.NewDust(new Vector2(base.NPC.position.X, base.NPC.Center.Y), base.NPC.width, base.NPC.height / 2, 14, 0f, -3f, 100, default(Color), 2.5f * base.NPC.scale);
				Dust obj15 = Main.dust[burrowedDust4];
				obj15.velocity *= 1f;
			}
		}
		else if (burrowTimer == 0)
		{
			if (!player.active || player.dead)
			{
				burrowTimer = 30;
				return;
			}
			base.NPC.TargetClosest();
			base.NPC.dontTakeDamage = true;
		}
	}

	public override bool CanHitNPC(NPC target)
	{
		return base.NPC.alpha == 0;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
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
		if (minDist <= 60f && base.NPC.alpha == 0)
		{
			return base.NPC.scale == 1f;
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage >= 0)
		{
			target.AddBuff(ModContent.BuffType<BrainRot>(), 240);
		}
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return base.NPC.scale == 1f;
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		if (phase2timer < 0)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= -4f;
			ReelBack();
			base.NPC.ForceNetUpdate();
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
		base.NPC.damage = (int)((float)base.NPC.damage * 0.8f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; (double)k < (double)(hit.Damage / base.NPC.lifeMax) * 100.0; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, hit.HitDirection, -1f);
		}
		if (!IsPhaseTwo && base.NPC.Distance(Main.player[base.NPC.target].Center) > 80f && Main.netMode != 1)
		{
			if (Main.rand.NextBool(30) && NPC.CountNPCS(6) < 2)
			{
				NPC.NewNPC(base.NPC.GetSource_FromThis(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 6);
			}
			if (Main.rand.NextBool(45) && !NPC.AnyNPCs(7))
			{
				NPC.NewNPC(base.NPC.GetSource_FromThis(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, 7);
			}
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		if (!Main.dedServ)
		{
			int goreAmount = 10;
			for (int i = 1; i <= goreAmount; i++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity, base.Mod.Find<ModGore>("HiveMindP2Gore" + i).Type);
			}
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 200;
		base.NPC.height = 150;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int j = 0; j < 40; j++)
		{
			int killDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[killDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[killDust].scale = 0.5f;
				Main.dust[killDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int l = 0; l < 70; l++)
		{
			int killDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 3f);
			Main.dust[killDust2].noGravity = true;
			Dust obj2 = Main.dust[killDust2];
			obj2.velocity *= 5f;
			killDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[killDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 188;
	}

	public override void OnKill()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			if (!DownedBossSystem.downedHiveMind && !DownedBossSystem.downedPerforator)
			{
				Color messageColor = Color.Cyan;
				AerialiteOreGen.Enchant();
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.SkyOreText", messageColor);
			}
			DownedBossSystem.downedHiveMind = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<HiveMindBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, new WeightedItemStack[5]
		{
			ModContent.ItemType<PerfectDark>(),
			ModContent.ItemType<Shadethrower>(),
			ModContent.ItemType<ShaderainStaff>(),
			ModContent.ItemType<DankStaff>(),
			ModContent.ItemType<RotBall>()
		}));
		normalOnly.Add(57, 1, 10, 15);
		normalOnly.Add(68, 1, 10, 15);
		normalOnly.Add(59, 1, 10, 15);
		normalOnly.Add(ItemDropRule.ByCondition(DropHelper.Hardmode(), 522, 1, 10, 20));
		normalOnly.Add(ModContent.ItemType<FilthyGlove>(), DropHelper.NormalWeaponDropRateFraction);
		normalOnly.Add(ModContent.ItemType<HiveMindMask>(), 7);
		normalOnly.Add(ModContent.ItemType<RottingEyeball>(), 10);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<HiveMindTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<HiveMindRelic>());
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(490), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(491), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(489), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(2998), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<RogueEmblem>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedHiveMind, ModContent.ItemType<LoreHiveMind>(), ui: true, DropHelper.FirstKillText);
	}
}
