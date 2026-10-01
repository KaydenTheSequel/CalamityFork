using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.NPCs.Providence;
using CalamityMod.Projectiles.Boss;
using CalamityMod.UI.VanillaBossBars;
using CalamityMod.Utilities;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ProfanedGuardians;

[AutoloadBossHead]
public class ProfanedGuardianCommander : ModNPC
{
	private int spearType;

	private int healTimer;

	private const float TimeForShieldDespawn = 120f;

	public static readonly SoundStyle HolyRaySound = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianRay")
	{
		Volume = 1.25f
	};

	public static readonly SoundStyle DashSound = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianDash");

	public static readonly SoundStyle ShieldDeathSound = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianShieldDeactivate");

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> TextureNight_Glow;

	public static int FireDamage = 36;

	public static int SpearDamage = 42;

	public static int HolyBlastDamage = 60;

	public static int RayDamage = 80;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 10;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		nPCBestiaryDrawModifiers.Scale = 0.75f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 25f;
		value.Position.Y += 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
			TextureNight_Glow = ModContent.Request<Texture2D>(Texture + "GlowNight", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 120;
		base.NPC.npcSlots = 20f;
		base.NPC.aiStyle = -1;
		base.NPC.width = 228;
		base.NPC.height = 186;
		base.NPC.defense = 40;
		base.NPC.DR_NERD(0.3f);
		base.NPC.LifeMaxNERB(80000, 120000, 200000);
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.AIType = -1;
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<ProfanedGuardianBossBar>();
		base.NPC.value = Item.buyPrice(0, 50);
		base.NPC.HitSound = SoundID.NPCHit52;
		base.NPC.DeathSound = SoundID.NPCDeath55;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ProfanedGuardianCommander")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(spearType);
		writer.Write(healTimer);
		writer.Write(base.NPC.chaseable);
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
		spearType = reader.ReadInt32();
		healTimer = reader.ReadInt32();
		base.NPC.chaseable = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.12f + ((Vector2)(ref base.NPC.velocity)).Length() / 120f;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1651: Unknown result type (might be due to invalid IL or missing references)
		//IL_165c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1661: Unknown result type (might be due to invalid IL or missing references)
		//IL_1677: Unknown result type (might be due to invalid IL or missing references)
		//IL_167c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1681: Unknown result type (might be due to invalid IL or missing references)
		//IL_1683: Unknown result type (might be due to invalid IL or missing references)
		//IL_1687: Unknown result type (might be due to invalid IL or missing references)
		//IL_168c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1694: Unknown result type (might be due to invalid IL or missing references)
		//IL_1698: Unknown result type (might be due to invalid IL or missing references)
		//IL_169d: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16da: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_170a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1710: Unknown result type (might be due to invalid IL or missing references)
		//IL_1712: Unknown result type (might be due to invalid IL or missing references)
		//IL_1717: Unknown result type (might be due to invalid IL or missing references)
		//IL_1718: Unknown result type (might be due to invalid IL or missing references)
		//IL_171d: Unknown result type (might be due to invalid IL or missing references)
		//IL_171e: Unknown result type (might be due to invalid IL or missing references)
		//IL_171f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1724: Unknown result type (might be due to invalid IL or missing references)
		//IL_1726: Unknown result type (might be due to invalid IL or missing references)
		//IL_1728: Unknown result type (might be due to invalid IL or missing references)
		//IL_1734: Unknown result type (might be due to invalid IL or missing references)
		//IL_173b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1745: Unknown result type (might be due to invalid IL or missing references)
		//IL_174b: Unknown result type (might be due to invalid IL or missing references)
		//IL_178f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1796: Unknown result type (might be due to invalid IL or missing references)
		//IL_179b: Unknown result type (might be due to invalid IL or missing references)
		//IL_23cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2402: Unknown result type (might be due to invalid IL or missing references)
		//IL_1900: Unknown result type (might be due to invalid IL or missing references)
		//IL_190b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1910: Unknown result type (might be due to invalid IL or missing references)
		//IL_1926: Unknown result type (might be due to invalid IL or missing references)
		//IL_192b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1930: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a51: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a71: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_18dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_24d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_24d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_24da: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_24e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_24fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_2502: Unknown result type (might be due to invalid IL or missing references)
		//IL_2509: Unknown result type (might be due to invalid IL or missing references)
		//IL_250e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2516: Unknown result type (might be due to invalid IL or missing references)
		//IL_251b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f43: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b33: Unknown result type (might be due to invalid IL or missing references)
		//IL_198d: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2563: Unknown result type (might be due to invalid IL or missing references)
		//IL_256d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2572: Unknown result type (might be due to invalid IL or missing references)
		//IL_2535: Unknown result type (might be due to invalid IL or missing references)
		//IL_2542: Unknown result type (might be due to invalid IL or missing references)
		//IL_2547: Unknown result type (might be due to invalid IL or missing references)
		//IL_2549: Unknown result type (might be due to invalid IL or missing references)
		//IL_2550: Unknown result type (might be due to invalid IL or missing references)
		//IL_2555: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_2007: Unknown result type (might be due to invalid IL or missing references)
		//IL_200c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_20de: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_208f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2099: Unknown result type (might be due to invalid IL or missing references)
		//IL_209e: Unknown result type (might be due to invalid IL or missing references)
		//IL_203b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2048: Unknown result type (might be due to invalid IL or missing references)
		//IL_204d: Unknown result type (might be due to invalid IL or missing references)
		//IL_204f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2056: Unknown result type (might be due to invalid IL or missing references)
		//IL_205b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2678: Unknown result type (might be due to invalid IL or missing references)
		//IL_268a: Unknown result type (might be due to invalid IL or missing references)
		//IL_268f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2691: Unknown result type (might be due to invalid IL or missing references)
		//IL_269b: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_26a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_26b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_26f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_26fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2717: Unknown result type (might be due to invalid IL or missing references)
		//IL_271c: Unknown result type (might be due to invalid IL or missing references)
		//IL_271e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2723: Unknown result type (might be due to invalid IL or missing references)
		//IL_272d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2741: Unknown result type (might be due to invalid IL or missing references)
		//IL_274b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2750: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21be: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_21cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_21df: Unknown result type (might be due to invalid IL or missing references)
		//IL_2846: Unknown result type (might be due to invalid IL or missing references)
		//IL_2851: Unknown result type (might be due to invalid IL or missing references)
		//IL_2856: Unknown result type (might be due to invalid IL or missing references)
		//IL_285b: Unknown result type (might be due to invalid IL or missing references)
		//IL_286b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2802: Unknown result type (might be due to invalid IL or missing references)
		//IL_280d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2880: Unknown result type (might be due to invalid IL or missing references)
		//IL_289c: Unknown result type (might be due to invalid IL or missing references)
		//IL_28a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_28a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_28a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_28b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_28b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2828: Unknown result type (might be due to invalid IL or missing references)
		//IL_2833: Unknown result type (might be due to invalid IL or missing references)
		//IL_21ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_21fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2205: Unknown result type (might be due to invalid IL or missing references)
		//IL_220b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2900: Unknown result type (might be due to invalid IL or missing references)
		//IL_2901: Unknown result type (might be due to invalid IL or missing references)
		//IL_2903: Unknown result type (might be due to invalid IL or missing references)
		//IL_2246: Unknown result type (might be due to invalid IL or missing references)
		//IL_224d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2252: Unknown result type (might be due to invalid IL or missing references)
		//IL_2953: Unknown result type (might be due to invalid IL or missing references)
		//IL_296f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2975: Unknown result type (might be due to invalid IL or missing references)
		//IL_2977: Unknown result type (might be due to invalid IL or missing references)
		//IL_297c: Unknown result type (might be due to invalid IL or missing references)
		//IL_298a: Unknown result type (might be due to invalid IL or missing references)
		//IL_298b: Unknown result type (might be due to invalid IL or missing references)
		//IL_29cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_29d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_29d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2354: Unknown result type (might be due to invalid IL or missing references)
		//IL_2355: Unknown result type (might be due to invalid IL or missing references)
		//IL_235c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2397: Unknown result type (might be due to invalid IL or missing references)
		//IL_2399: Unknown result type (might be due to invalid IL or missing references)
		//IL_239e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2276: Unknown result type (might be due to invalid IL or missing references)
		//IL_228e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2294: Unknown result type (might be due to invalid IL or missing references)
		//IL_2296: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1002: Unknown result type (might be due to invalid IL or missing references)
		//IL_1018: Unknown result type (might be due to invalid IL or missing references)
		//IL_101d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1024: Unknown result type (might be due to invalid IL or missing references)
		//IL_1029: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_22b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_22bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_22c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_22cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_150b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1515: Unknown result type (might be due to invalid IL or missing references)
		//IL_151a: Unknown result type (might be due to invalid IL or missing references)
		//IL_107b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1080: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_137d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1382: Unknown result type (might be due to invalid IL or missing references)
		//IL_1383: Unknown result type (might be due to invalid IL or missing references)
		//IL_1388: Unknown result type (might be due to invalid IL or missing references)
		//IL_138f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1394: Unknown result type (might be due to invalid IL or missing references)
		//IL_13aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_11af: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_14f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_143e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1443: Unknown result type (might be due to invalid IL or missing references)
		//IL_144d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1453: Unknown result type (might be due to invalid IL or missing references)
		//IL_1455: Unknown result type (might be due to invalid IL or missing references)
		//IL_145a: Unknown result type (might be due to invalid IL or missing references)
		//IL_145c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1464: Unknown result type (might be due to invalid IL or missing references)
		//IL_146a: Unknown result type (might be due to invalid IL or missing references)
		//IL_146c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1471: Unknown result type (might be due to invalid IL or missing references)
		//IL_1473: Unknown result type (might be due to invalid IL or missing references)
		//IL_147e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1483: Unknown result type (might be due to invalid IL or missing references)
		//IL_149b: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14af: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1202: Unknown result type (might be due to invalid IL or missing references)
		//IL_120c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1213: Unknown result type (might be due to invalid IL or missing references)
		//IL_121d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1223: Unknown result type (might be due to invalid IL or missing references)
		//IL_1322: Unknown result type (might be due to invalid IL or missing references)
		//IL_1323: Unknown result type (might be due to invalid IL or missing references)
		//IL_125b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1273: Unknown result type (might be due to invalid IL or missing references)
		//IL_1279: Unknown result type (might be due to invalid IL or missing references)
		//IL_127b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1280: Unknown result type (might be due to invalid IL or missing references)
		//IL_1287: Unknown result type (might be due to invalid IL or missing references)
		//IL_1291: Unknown result type (might be due to invalid IL or missing references)
		//IL_1298: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d6: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.doughnutBoss = base.NPC.whoAmI;
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 1.1f, 0.9f, 0f);
		Vector2 dustAndProjectileOffset = default(Vector2);
		((Vector2)(ref dustAndProjectileOffset))._002Ector(40f * (float)base.NPC.direction, 20f);
		Vector2 shootFrom = base.NPC.Center + dustAndProjectileOffset;
		base.NPC.rotation = base.NPC.velocity.X * 0.005f;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		if (Main.netMode != 1 && base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<ProfanedGuardianDefender>());
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<ProfanedGuardianHealer>());
		}
		bool defenderAlive = false;
		bool healerAlive = false;
		if (CalamityGlobalNPC.doughnutBossDefender != -1 && Main.npc[CalamityGlobalNPC.doughnutBossDefender].active)
		{
			defenderAlive = true;
		}
		if (CalamityGlobalNPC.doughnutBossHealer != -1 && Main.npc[CalamityGlobalNPC.doughnutBossHealer].active)
		{
			healerAlive = true;
		}
		if (defenderAlive)
		{
			base.NPC.Calamity().DR = 0.9f;
			base.NPC.Calamity().unbreakableDR = true;
			base.NPC.Calamity().CurrentlyIncreasingDefenseOrDR = true;
		}
		else
		{
			base.NPC.Calamity().DR = 0.3f;
			base.NPC.Calamity().unbreakableDR = false;
			base.NPC.Calamity().CurrentlyIncreasingDefenseOrDR = false;
		}
		bool hasDoneLaser = calamityGlobalNPC.newAI[2] == 1f;
		if (healerAlive)
		{
			bool shouldDoLaser = (float)Main.npc[CalamityGlobalNPC.doughnutBossHealer].life / (float)Main.npc[CalamityGlobalNPC.doughnutBossHealer].lifeMax <= 0.5f;
			if ((!hasDoneLaser & shouldDoLaser) && base.NPC.ai[0] != 5f)
			{
				base.NPC.ai[0] = 5f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.localAI[3] = 0f;
				base.NPC.netUpdate = true;
			}
			float distanceFromHealer = Vector2.Distance(Main.npc[CalamityGlobalNPC.doughnutBossHealer].Center, base.NPC.Center);
			if (distanceFromHealer > 2000f || Main.npc[CalamityGlobalNPC.doughnutBossHealer].justHit || base.NPC.life == base.NPC.lifeMax)
			{
				healTimer = 0;
			}
			else
			{
				float healGateValue = 60f;
				healTimer++;
				if ((float)healTimer >= healGateValue)
				{
					SoundEngine.PlaySound(in SoundID.Item8, shootFrom);
					int maxHealDustIterations = (int)distanceFromHealer;
					int maxDust = 100;
					int dustDivisor = maxHealDustIterations / maxDust;
					if (dustDivisor < 2)
					{
						dustDivisor = 2;
					}
					Vector2 healDustOffset = default(Vector2);
					((Vector2)(ref healDustOffset))._002Ector(40f * (float)Main.npc[CalamityGlobalNPC.doughnutBossHealer].direction, 20f);
					Vector2 dustLineStart = Main.npc[CalamityGlobalNPC.doughnutBossHealer].Center + healDustOffset;
					Vector2 dustLineEnd = shootFrom;
					Vector2 currentDustPos = default(Vector2);
					Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), 3.1415927410125732);
					Vector2 dustVelocityMult = default(Vector2);
					((Vector2)(ref dustVelocityMult))._002Ector(2.1f, 2f);
					int dustSpawned = 0;
					for (int i = 0; i < maxHealDustIterations; i++)
					{
						if (i % dustDivisor == 0)
						{
							currentDustPos = Vector2.Lerp(dustLineStart, dustLineEnd, (float)i / (float)maxHealDustIterations);
							Color dustColor = Main.hslToRgb(Main.rgbToHsl(new Color(255, 200, Math.Abs(Main.DiscoB - (int)((float)dustSpawned * 2.55f)))).X, 1f, 0.5f);
							((Color)(ref dustColor)).A = byte.MaxValue;
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
							dustSpawned++;
						}
					}
					healTimer = 0;
					if (Main.netMode != 1)
					{
						int healAmt = base.NPC.lifeMax / 10;
						if (healAmt > base.NPC.lifeMax - base.NPC.life)
						{
							healAmt = base.NPC.lifeMax - base.NPC.life;
						}
						if (healAmt > 0)
						{
							base.NPC.life += healAmt;
							base.NPC.HealEffect(healAmt);
							base.NPC.netUpdate = true;
						}
					}
				}
			}
			if (Main.npc[CalamityGlobalNPC.doughnutBossHealer].ai[0] == 599f && Main.zenithWorld && Main.netMode != 1)
			{
				base.NPC.lifeMax += 7500;
				base.NPC.life += base.NPC.lifeMax - base.NPC.life;
				base.NPC.HealEffect(base.NPC.lifeMax - base.NPC.life);
				base.NPC.netUpdate = true;
			}
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (!player.active || player.dead || Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
			{
				if (base.NPC.velocity.Y > 3f)
				{
					base.NPC.velocity.Y = 3f;
				}
				base.NPC.velocity.Y -= 0.1f;
				if (base.NPC.velocity.Y < -12f)
				{
					base.NPC.velocity.Y = -12f;
				}
				if (base.NPC.timeLeft > 60)
				{
					base.NPC.timeLeft = 60;
				}
				if (base.NPC.ai[0] != 0f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3] = 0f;
					base.NPC.netUpdate = true;
				}
				base.NPC.ai[3] = -1f;
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (base.NPC.ai[3] < 0f)
		{
			base.NPC.ai[3] = 0f;
		}
		bool phase1 = healerAlive | defenderAlive;
		base.NPC.chaseable = !phase1;
		if (!phase1 && base.NPC.localAI[1] < 120f)
		{
			if (base.NPC.localAI[1] == 0f)
			{
				SoundEngine.PlaySound(in ShieldDeathSound, base.NPC.Center);
			}
			base.NPC.localAI[1]++;
		}
		float moveToOtherSideInPhase1GateValue = 900f;
		float timeBeforeMoveToOtherSideInPhase1Reset = moveToOtherSideInPhase1GateValue * 2f;
		float goLowDuration = 240f * 0.5f;
		float goLowOrHighDistance = 540f;
		float moveToOtherSideInPhase2GateValue = (death ? 480f : (revenge ? 510f : (expertMode ? 540f : 600f))) - 120f;
		float timeBeforeMoveToOtherSideInPhase2Reset = moveToOtherSideInPhase2GateValue * 2f;
		float goLowDurationPhase2 = 210f * 0.5f;
		float chargeVelocityMult = 0.25f;
		float maxChargeVelocity = (death ? 28f : (revenge ? 26f : (expertMode ? 24f : 20f)));
		if (Main.getGoodWorld)
		{
			maxChargeVelocity *= 2f;
		}
		float inertia = (death ? 45f : (revenge ? 47f : (expertMode ? 50f : 55f)));
		if (lifeRatio < 0.5f)
		{
			inertia *= 0.8f;
		}
		if (!phase1)
		{
			inertia *= 0.75f;
		}
		if (Main.getGoodWorld)
		{
			inertia *= 0.8f;
		}
		bool speedUp = Vector2.Distance(base.NPC.Center, player.Center) > 960f;
		int totalDustPerProjectile = 15;
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
			{
				float playerLocation = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			if (healerAlive)
			{
				base.NPC.localAI[3]++;
			}
			else
			{
				base.NPC.localAI[3] = 0f;
			}
			float roundedGoLowCheck = (float)Math.Round((double)goLowDuration * 0.5);
			bool goLow = (base.NPC.localAI[3] > moveToOtherSideInPhase1GateValue - goLowDuration && base.NPC.localAI[3] <= moveToOtherSideInPhase1GateValue + roundedGoLowCheck) || base.NPC.localAI[3] > timeBeforeMoveToOtherSideInPhase1Reset - goLowDuration || base.NPC.localAI[3] <= 0f - roundedGoLowCheck;
			if (base.NPC.localAI[3] == moveToOtherSideInPhase1GateValue - roundedGoLowCheck || base.NPC.localAI[3] == timeBeforeMoveToOtherSideInPhase1Reset - roundedGoLowCheck)
			{
				calamityGlobalNPC.newAI[0] *= -1f;
			}
			if (base.NPC.localAI[3] > timeBeforeMoveToOtherSideInPhase1Reset)
			{
				base.NPC.localAI[3] = 0f - goLowDuration;
			}
			bool canSwapPlacesWithDefender = false;
			bool defenderCharging = false;
			if (defenderAlive)
			{
				if (Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[0] == 1f && Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[1] < -120f)
				{
					canSwapPlacesWithDefender = true;
				}
				if ((Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[0] == 0f && Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[3] > 0f) || (Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[0] == 1f && Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[1] >= -60f) || Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[0] == 2f || Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[0] == 3f)
				{
					defenderCharging = true;
				}
			}
			if (!healerAlive & defenderAlive)
			{
				if (canSwapPlacesWithDefender)
				{
					calamityGlobalNPC.newAI[1]++;
				}
			}
			else
			{
				calamityGlobalNPC.newAI[1] = 0f;
			}
			float roundedGoLowPhase2Check = (float)Math.Round((double)goLowDurationPhase2 * 0.5);
			bool goLowPhase2 = calamityGlobalNPC.newAI[1] > moveToOtherSideInPhase2GateValue - goLowDurationPhase2 && calamityGlobalNPC.newAI[1] <= moveToOtherSideInPhase2GateValue + roundedGoLowPhase2Check;
			bool goHigh = calamityGlobalNPC.newAI[1] > timeBeforeMoveToOtherSideInPhase2Reset - goLowDurationPhase2 || calamityGlobalNPC.newAI[1] <= 0f - roundedGoLowPhase2Check;
			if (calamityGlobalNPC.newAI[1] == moveToOtherSideInPhase2GateValue - roundedGoLowPhase2Check || calamityGlobalNPC.newAI[1] == timeBeforeMoveToOtherSideInPhase2Reset - roundedGoLowPhase2Check)
			{
				calamityGlobalNPC.newAI[0] *= -1f;
			}
			if (calamityGlobalNPC.newAI[1] > timeBeforeMoveToOtherSideInPhase2Reset)
			{
				calamityGlobalNPC.newAI[1] = 0f - goLowDurationPhase2;
			}
			if (!goLow && !goLowPhase2 && !goHigh)
			{
				calamityGlobalNPC.newAI[0] = -base.NPC.direction;
			}
			float velocity = (death ? 16f : (revenge ? 15f : (expertMode ? 14f : 12f)));
			if (Main.getGoodWorld)
			{
				velocity *= 1.25f;
			}
			if (healerAlive)
			{
				velocity *= 0.8f;
			}
			if (speedUp)
			{
				inertia *= 0.5f;
				velocity *= 2f;
			}
			if (goLowPhase2 | goHigh)
			{
				inertia *= 0.5f;
				velocity *= 2f;
			}
			else if (goLow)
			{
				inertia *= 0.66f;
				velocity *= 1.66f;
			}
			if (defenderCharging && !speedUp)
			{
				inertia *= 1.5f;
				velocity *= 0.75f;
			}
			float distanceToStayAwayFromTarget = (defenderAlive ? 800f : 600f);
			Vector2 destination = player.Center + Vector2.UnitX * distanceToStayAwayFromTarget * calamityGlobalNPC.newAI[0];
			if (goLow | goLowPhase2 | goHigh)
			{
				destination.Y += (goHigh ? (0f - goLowOrHighDistance) : goLowOrHighDistance);
			}
			Vector2 desiredVelocity = (destination - base.NPC.Center).SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * velocity;
			float phaseGateValue = (death ? 66f : (revenge ? 75f : (expertMode ? 83f : 100f)));
			if (phase1 || base.NPC.ai[2] < phaseGateValue * 5f)
			{
				if (Vector2.Distance(base.NPC.Center, destination) > 80f)
				{
					base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + desiredVelocity) / inertia;
				}
				else
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.98f;
				}
				base.NPC.ai[2]++;
				float projectileShootGateValue = (death ? 60f : (revenge ? 90f : (expertMode ? 100f : 150f)));
				if (base.NPC.ai[2] % projectileShootGateValue != 0f)
				{
					return;
				}
				if (phase1)
				{
					bool fireExtraProjectiles = false;
					if (defenderAlive && Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[0] == 2f)
					{
						fireExtraProjectiles = true;
					}
					bool shootSpear = base.NPC.ai[2] % (projectileShootGateValue * 2f) == 0f;
					float projectileVelocity = (death ? 16f : (revenge ? 15f : (expertMode ? 14f : 12f)));
					Vector2 finalProjectileVelocity = Vector2.Normalize(player.Center - shootFrom) * projectileVelocity;
					int type = (shootSpear ? ModContent.ProjectileType<ProfanedSpear>() : ModContent.ProjectileType<HolyFire2>());
					int damage = (shootSpear ? SpearDamage : FireDamage);
					if (type == ModContent.ProjectileType<HolyFire2>())
					{
						finalProjectileVelocity *= 0.5f;
					}
					for (int k = 0; k < totalDustPerProjectile; k++)
					{
						Dust.NewDust(shootFrom, 30, 30, 244, finalProjectileVelocity.X, finalProjectileVelocity.Y);
					}
					if (fireExtraProjectiles & shootSpear)
					{
						int baseProjectileAmt = 2;
						float rotation = MathHelper.ToRadians(10f);
						for (int j = 0; j < baseProjectileAmt; j++)
						{
							Vector2 perturbedSpeed = finalProjectileVelocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)j / (float)(baseProjectileAmt - 1)));
							for (int l = 0; l < totalDustPerProjectile; l++)
							{
								Dust.NewDust(shootFrom, 30, 30, 244, perturbedSpeed.X, perturbedSpeed.Y);
							}
							if (Main.netMode != 1)
							{
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, perturbedSpeed, type, damage, 0f, Main.myPlayer);
							}
						}
					}
					else if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, finalProjectileVelocity, type, damage, 0f, Main.myPlayer);
					}
				}
				else
				{
					float holyBlastVelocity = (death ? 18f : (revenge ? 17f : (expertMode ? 16f : 14f)));
					int projTimeLeft = (int)(2000f / holyBlastVelocity);
					Vector2 finalHolyBlastVelocity = Vector2.Normalize(player.Center - shootFrom) * holyBlastVelocity;
					if (Main.netMode != 1)
					{
						int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, finalHolyBlastVelocity, ModContent.ProjectileType<HolyBlast>(), HolyBlastDamage, 0f, Main.myPlayer, player.position.X, player.position.Y, 1f);
						Main.projectile[proj].timeLeft = projTimeLeft;
					}
					for (int m = 0; m < 100; m++)
					{
						int num = Main.rand.Next(6);
						int dustID = (((uint)num > 3u) ? 158 : 244);
						float num2 = Main.rand.NextFloat(holyBlastVelocity * 0.5f, holyBlastVelocity);
						float angleRandom = 0.06f;
						Vector2 dustVel = Utils.RotatedBy(new Vector2(num2, 0f), (double)finalHolyBlastVelocity.ToRotation(), default(Vector2));
						dustVel = dustVel.RotatedBy(0f - angleRandom);
						dustVel = dustVel.RotatedByRandom(2f * angleRandom);
						float scale = Main.rand.NextFloat(1f, 2f);
						int idx = Dust.NewDust(shootFrom, 180, 180, dustID, dustVel.X, dustVel.Y, 0, default(Color), scale);
						Main.dust[idx].noGravity = true;
					}
					base.NPC.velocity = -finalHolyBlastVelocity * 0.5f;
				}
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.98f;
				if (base.NPC.ai[1] >= phaseGateValue)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.netUpdate = true;
				}
				else
				{
					base.NPC.ai[1]++;
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = base.NPC.defDamage;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
			{
				float playerLocation2 = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			base.NPC.ai[0] = 2f;
			base.NPC.netUpdate = true;
			Vector2 velocity2 = (player.Center - base.NPC.Center).SafeNormalize(new Vector2((float)base.NPC.direction, 0f));
			velocity2 *= maxChargeVelocity;
			base.NPC.velocity = velocity2 * chargeVelocityMult;
			SoundEngine.PlaySound(in DashSound, base.NPC.Center);
			int totalDust = 36;
			for (int n = 0; n < totalDust; n++)
			{
				Vector2 val = (base.NPC.velocity.SafeNormalize(Vector2.UnitY) * new Vector2(160f, 160f)).RotatedBy((float)(n - (totalDust / 2 - 1)) * ((float)Math.PI * 2f) / (float)totalDust) + shootFrom;
				Vector2 dustVelocity = val - shootFrom;
				int dust3 = Dust.NewDust(val + dustVelocity, 0, 0, 244, dustVelocity.X, dustVelocity.Y);
				Main.dust[dust3].noGravity = true;
				Main.dust[dust3].noLight = true;
				Main.dust[dust3].scale = 3f;
				Main.dust[dust3].velocity = dustVelocity * 0.3f;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = base.NPC.defDamage;
			if (Math.Sign(base.NPC.velocity.X) != 0)
			{
				base.NPC.spriteDirection = -Math.Sign(base.NPC.velocity.X);
			}
			base.NPC.spriteDirection = Math.Sign(base.NPC.velocity.X);
			base.NPC.ai[1]++;
			float phaseGateValue2 = (death ? 100f : (revenge ? 110f : (expertMode ? 120f : 135f)));
			if (base.NPC.ai[1] >= phaseGateValue2)
			{
				base.NPC.ai[0] = 3f;
				float slowDownDurationAfterCharge = (revenge ? ((float)Main.rand.Next(20, 41)) : 40f);
				base.NPC.ai[1] = slowDownDurationAfterCharge;
				base.NPC.localAI[2] = 0f;
				NPC nPC3 = base.NPC;
				nPC3.velocity /= 2f;
				base.NPC.netUpdate = true;
				return;
			}
			Vector2 targetVector = (player.Center - base.NPC.Center).SafeNormalize(new Vector2((float)base.NPC.direction, 0f));
			if (base.NPC.localAI[2] == 0f)
			{
				if (((Vector2)(ref base.NPC.velocity)).Length() < maxChargeVelocity)
				{
					float velocityMult = (death ? 1.05037f : (revenge ? 1.047407f : (expertMode ? 1.044444f : 1.04f)));
					base.NPC.velocity = targetVector * (((Vector2)(ref base.NPC.velocity)).Length() * velocityMult);
					if (((Vector2)(ref base.NPC.velocity)).Length() > maxChargeVelocity)
					{
						base.NPC.localAI[2] = 1f;
						base.NPC.velocity = base.NPC.velocity.SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * maxChargeVelocity;
					}
				}
			}
			else if (base.NPC.localAI[2] == 1f)
			{
				inertia *= 1.5f;
				base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + targetVector * (((Vector2)(ref base.NPC.velocity)).Length() + 0.11111112f * inertia)) / inertia;
				if (base.NPC.Distance(player.Center) < 160f * base.NPC.scale)
				{
					base.NPC.localAI[2] = 2f;
				}
			}
			else if (base.NPC.Distance(player.Center) >= 240f * base.NPC.scale || base.NPC.localAI[2] == 3f)
			{
				if (base.NPC.localAI[2] != 3f)
				{
					base.NPC.localAI[2] = 3f;
				}
				NPC nPC4 = base.NPC;
				nPC4.velocity *= 0.98f;
			}
			int projectileGateValue = (int)(phaseGateValue2 * 0.4f);
			if (base.NPC.ai[1] % (float)projectileGateValue == 0f)
			{
				float projectileVelocityY = base.NPC.velocity.Y;
				if (projectileVelocityY < 0f)
				{
					projectileVelocityY = 0f;
				}
				projectileVelocityY += (expertMode ? 3f : 2f);
				Vector2 projectileVelocity2 = default(Vector2);
				((Vector2)(ref projectileVelocity2))._002Ector(base.NPC.velocity.X * 0.2f, projectileVelocityY);
				for (int num3 = 0; num3 < totalDustPerProjectile; num3++)
				{
					Dust.NewDust(shootFrom, 30, 30, 244, projectileVelocity2.X, projectileVelocity2.Y);
				}
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, projectileVelocity2, ModContent.ProjectileType<HolyFire>(), FireDamage, 0f, Main.myPlayer);
				}
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			if (Math.Sign(base.NPC.velocity.X) != 0)
			{
				base.NPC.spriteDirection = -Math.Sign(base.NPC.velocity.X);
			}
			base.NPC.spriteDirection = Math.Sign(base.NPC.velocity.X);
			base.NPC.ai[1]--;
			if (base.NPC.ai[1] <= 0f)
			{
				base.NPC.ai[2]++;
				float totalCharges = (revenge ? ((float)Main.rand.Next(3) + 1f) : 2f);
				bool dontCharge = base.NPC.ai[2] >= totalCharges;
				bool useSpears = base.NPC.ai[3] % 2f == 0f;
				base.NPC.ai[0] = ((!dontCharge) ? 1f : (useSpears ? 4f : 0f));
				if (dontCharge)
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[3]++;
				}
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
			NPC nPC5 = base.NPC;
			nPC5.velocity *= 0.95f;
		}
		else if (base.NPC.ai[0] == 4f)
		{
			base.NPC.damage = 0;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
			{
				float playerLocation3 = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation3 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			if (Vector2.Distance(base.NPC.Center, player.Center) > 1600f)
			{
				base.NPC.ai[2] = 1f;
			}
			bool targetRanAwayAndWillNowBeFucked = base.NPC.ai[2] == 1f;
			bool boostVelocityToCatchUp = (base.NPC.ai[1] == 0f) | targetRanAwayAndWillNowBeFucked;
			float velocity3 = (death ? 16f : (revenge ? 15f : (expertMode ? 14f : 12f)));
			if (Main.getGoodWorld)
			{
				velocity3 *= 1.25f;
			}
			if (boostVelocityToCatchUp)
			{
				velocity3 *= 2f;
			}
			float distanceToStayAwayFromTargetForSpears = 640f;
			Vector2 destination2 = player.Center + Vector2.UnitX * distanceToStayAwayFromTargetForSpears * (float)(-base.NPC.direction);
			Vector2 desiredVelocity2 = (destination2 - base.NPC.Center).SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * velocity3;
			float totalSpears = 12f;
			float shootDuration = (death ? 280f : (revenge ? 300f : (expertMode ? 320f : 360f)));
			float dontShootTime = shootDuration * 0.3f;
			float phaseGateValue3 = dontShootTime + shootDuration;
			if (base.NPC.ai[1] > 0f)
			{
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] < phaseGateValue3)
			{
				if (Vector2.Distance(base.NPC.Center, destination2) > 80f)
				{
					inertia *= (targetRanAwayAndWillNowBeFucked ? 0.25f : 0.5f);
					base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + desiredVelocity2) / inertia;
				}
				else
				{
					if (base.NPC.ai[1] == 0f)
					{
						base.NPC.ai[1] = 1f;
					}
					NPC nPC6 = base.NPC;
					nPC6.velocity *= 0.96f;
				}
			}
			int spearShootDivisor = (int)(shootDuration / totalSpears);
			float totalPhaseDuration = phaseGateValue3 + dontShootTime;
			if (!(base.NPC.ai[1] >= dontShootTime))
			{
				return;
			}
			if (base.NPC.ai[1] >= phaseGateValue3)
			{
				NPC nPC7 = base.NPC;
				nPC7.velocity *= 0.98f;
				if (base.NPC.ai[1] >= totalPhaseDuration)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else
			{
				if (base.NPC.ai[1] % (float)spearShootDivisor != 0f)
				{
					return;
				}
				float spearVelocity = (death ? 16f : (revenge ? 15f : (expertMode ? 14f : 12f)));
				if (Main.getGoodWorld)
				{
					spearVelocity *= 1.25f;
				}
				if (boostVelocityToCatchUp)
				{
					spearVelocity *= 1.5f;
				}
				Vector2 velocity4 = Vector2.Normalize(player.Center - shootFrom) * spearVelocity;
				Vector2 knockbackVelocity = velocity4 * 0.1f;
				SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballShot, shootFrom);
				for (int num4 = 0; num4 < totalDustPerProjectile; num4++)
				{
					Dust.NewDust(shootFrom, 30, 30, 244, velocity4.X, velocity4.Y);
				}
				if ((base.NPC.ai[1] % (float)(spearShootDivisor * 3) == 0f) | targetRanAwayAndWillNowBeFucked)
				{
					knockbackVelocity *= 2f;
					int baseProjectileAmt2 = (expertMode ? 6 : 4);
					float rotation2 = MathHelper.ToRadians((float)(expertMode ? 50 : 40));
					for (int num5 = 0; num5 < baseProjectileAmt2; num5++)
					{
						Vector2 perturbedSpeed2 = velocity4.RotatedBy(MathHelper.Lerp(0f - rotation2, rotation2, (float)num5 / (float)(baseProjectileAmt2 - 1))) * 0.3f;
						for (int num6 = 0; num6 < totalDustPerProjectile; num6++)
						{
							Dust.NewDust(shootFrom, 30, 30, 244, perturbedSpeed2.X, perturbedSpeed2.Y);
						}
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, perturbedSpeed2, ModContent.ProjectileType<HolySpear>(), SpearDamage, 0f, Main.myPlayer, targetRanAwayAndWillNowBeFucked ? (-2f) : (-1f), -30f);
						}
					}
				}
				if (Main.netMode != 1)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, velocity4 * 0.85f, ModContent.ProjectileType<HolySpear>(), SpearDamage, 0f, Main.myPlayer, 1f);
				}
				if (!targetRanAwayAndWillNowBeFucked)
				{
					base.NPC.velocity = -knockbackVelocity;
				}
			}
		}
		else
		{
			if (base.NPC.ai[0] != 5f)
			{
				return;
			}
			base.NPC.damage = 0;
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
			{
				float playerLocation4 = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation4 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			float laserGateValue = 120f;
			float velocity5 = (death ? 4f : (revenge ? 3.75f : (expertMode ? 3.5f : 3f)));
			if (base.NPC.ai[1] < laserGateValue)
			{
				velocity5 *= 6f;
			}
			if (Main.getGoodWorld)
			{
				velocity5 *= 1.25f;
			}
			calamityGlobalNPC.newAI[0] = -base.NPC.direction;
			float distanceToStayAwayFromTargetForLaser = 720f;
			Vector2 destination3 = player.Center + Vector2.UnitX * distanceToStayAwayFromTargetForLaser * calamityGlobalNPC.newAI[0];
			Vector2 desiredVelocity3 = (destination3 - base.NPC.Center).SafeNormalize(new Vector2((float)base.NPC.direction, 0f)) * velocity5;
			if (Vector2.Distance(base.NPC.Center, destination3) > 80f)
			{
				base.NPC.velocity = (base.NPC.velocity * (inertia - 1f) + desiredVelocity3) / inertia;
			}
			else
			{
				NPC nPC8 = base.NPC;
				nPC8.velocity *= 0.98f;
			}
			if (base.NPC.ai[1] >= laserGateValue)
			{
				float speedCap = (Main.getGoodWorld ? 4f : 2f);
				if (base.NPC.velocity.Y > speedCap)
				{
					base.NPC.velocity.Y = speedCap;
				}
				if (base.NPC.velocity.Y < 0f - speedCap)
				{
					base.NPC.velocity.Y = 0f - speedCap;
				}
			}
			base.NPC.ai[2]++;
			if (base.NPC.ai[2] < laserGateValue)
			{
				Vector2 dustPosOffset = default(Vector2);
				((Vector2)(ref dustPosOffset))._002Ector(27f, 59f);
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
						Vector2 dustPos = shootFrom + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * dustPosOffset / 2f;
						int index = Dust.NewDust(dustPos - Vector2.One * 8f, 16, 16, 244, base.NPC.velocity.X / 2f, base.NPC.velocity.Y / 2f);
						Main.dust[index].velocity = Vector2.Normalize(base.NPC.Center - dustPos) * 3.5f * (10f - (float)extraDustAmt * 2f) / 10f;
						Main.dust[index].noGravity = true;
						Main.dust[index].scale = scalar;
					}
				}
			}
			else if (base.NPC.ai[2] < (revenge ? 220f : 300f) && base.NPC.ai[2] == laserGateValue)
			{
				float rotation3 = (death ? 445f : (revenge ? 450f : (expertMode ? 455f : 465f)));
				if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && Vector2.Distance(Main.LocalPlayer.Center, base.NPC.Center) < 2800f)
				{
					SoundEngine.PlaySound(in HolyRaySound, Main.LocalPlayer.Center);
				}
				if (Main.netMode != 1)
				{
					Vector2 laserVelocity = player.Center - base.NPC.Center;
					((Vector2)(ref laserVelocity)).Normalize();
					float beamDirection = -1f;
					if (laserVelocity.X < 0f)
					{
						beamDirection = 1f;
					}
					laserVelocity = laserVelocity.RotatedBy((0.0 - (double)beamDirection) * 6.2831854820251465 / 6.0);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, laserVelocity, ModContent.ProjectileType<ProvidenceHolyRay>(), RayDamage, 0f, Main.myPlayer, beamDirection * ((float)Math.PI * 2f) / rotation3, base.NPC.whoAmI);
					if (revenge)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, -laserVelocity, ModContent.ProjectileType<ProvidenceHolyRay>(), RayDamage, 0f, Main.myPlayer, (0f - beamDirection) * ((float)Math.PI * 2f) / rotation3, base.NPC.whoAmI);
					}
					if (CalamityWorld.death)
					{
						rotation3 *= 0.33f;
						laserVelocity = laserVelocity.RotatedBy((0.0 - (double)beamDirection) * 6.2831854820251465 / 2.0);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, laserVelocity, ModContent.ProjectileType<ProvidenceHolyRay>(), RayDamage, 0f, Main.myPlayer, beamDirection * ((float)Math.PI * 2f) / rotation3, base.NPC.whoAmI);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shootFrom, -laserVelocity, ModContent.ProjectileType<ProvidenceHolyRay>(), RayDamage, 0f, Main.myPlayer, (0f - beamDirection) * ((float)Math.PI * 2f) / rotation3, base.NPC.whoAmI);
					}
					base.NPC.netUpdate = true;
				}
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= (revenge ? 235f : 315f))
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				calamityGlobalNPC.newAI[2] = 1f;
				base.NPC.netUpdate = true;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		float useLaserGateValue = 120f;
		float num = ((CalamityWorld.revenge || BossRushEvent.BossRushActive) ? 235f : 315f);
		float maxIntensity = 45f;
		float increaseIntensityGateValue = useLaserGateValue - maxIntensity;
		float decreaseIntensityGateValue = num - maxIntensity;
		bool num2 = base.NPC.ai[0] == 5f;
		_ = base.NPC.ai[1];
		bool decreaseIntensity = base.NPC.ai[1] > decreaseIntensityGateValue;
		if (num2)
		{
			float burnIntensity = (decreaseIntensity ? Utils.GetLerpValue(0f, maxIntensity, maxIntensity - (base.NPC.ai[1] - decreaseIntensityGateValue), clamped: true) : Utils.GetLerpValue(0f, maxIntensity, base.NPC.ai[1], clamped: true));
			int totalGuardiansToDraw = (int)MathHelper.Lerp(1f, 30f, burnIntensity);
			for (int i = 0; i < totalGuardiansToDraw; i++)
			{
				float num3 = (float)Math.PI * 2f * (float)i * 2f / (float)totalGuardiansToDraw;
				float drawOffsetFactor = (float)Math.Sin(num3 * 6f + Main.GlobalTimeWrappedHourly * (float)Math.PI);
				drawOffsetFactor *= (float)Math.Pow(burnIntensity, 3.0) * 50f;
				Vector2 drawOffset = num3.ToRotationVector2() * drawOffsetFactor;
				Color baseColor = Color.White * (MathHelper.Lerp(0.4f, 0.8f, burnIntensity) / (float)totalGuardiansToDraw * 1.5f);
				((Color)(ref baseColor)).A = 0;
				baseColor = Color.Lerp(Color.White, baseColor, burnIntensity);
				drawGuardianInstance(drawOffset, (totalGuardiansToDraw == 1) ? ((Color?)null) : new Color?(baseColor));
			}
		}
		else
		{
			drawGuardianInstance(Vector2.Zero, null);
		}
		if (base.NPC.IsABestiaryIconDummy)
		{
			return false;
		}
		bool defenderAlive = false;
		bool healerAlive = false;
		if (CalamityGlobalNPC.doughnutBossDefender != -1 && Main.npc[CalamityGlobalNPC.doughnutBossDefender].active)
		{
			defenderAlive = true;
		}
		if (CalamityGlobalNPC.doughnutBossHealer != -1 && Main.npc[CalamityGlobalNPC.doughnutBossHealer].active)
		{
			healerAlive = true;
		}
		if (base.NPC.localAI[1] < 120f)
		{
			float maxOscillation = 60f;
			float minScale = 0.8f;
			float maxPulseScale = 1f - minScale;
			float minOpacity = 0.5f;
			float maxOpacityScale = 1f - minOpacity;
			float currentOscillation = MathHelper.Lerp(0f, maxOscillation, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * (float)Math.PI) + 1f) * 0.5f);
			float shieldOpacity = minOpacity + maxOpacityScale * Utils.Remap(currentOscillation, 0f, maxOscillation, 1f, 0f);
			float oscillationRatio = currentOscillation / maxOscillation;
			float invertedOscillationRatio = 1f - (1f - oscillationRatio) * (1f - oscillationRatio);
			float oscillationScale = 1f - (1f - invertedOscillationRatio) * (1f - invertedOscillationRatio);
			float num4 = Utils.Remap(currentOscillation, maxOscillation - 15f, maxOscillation, 0f, 1f);
			float twoOscillationsMultipliedTogetherForScaleCalculation = num4 * num4;
			float invertedOscillationUsedForScale = MathHelper.Lerp(minScale, 1f, 1f - twoOscillationsMultipliedTogetherForScaleCalculation);
			float shieldScale = (minScale + maxPulseScale * oscillationScale) * invertedOscillationUsedForScale;
			float smallerRemappedOscillation = Utils.Remap(currentOscillation, 20f, maxOscillation, 0f, 1f);
			float invertedSmallerOscillationRatio = 1f - (1f - smallerRemappedOscillation) * (1f - smallerRemappedOscillation);
			float smallerOscillationScale = 1f - (1f - invertedSmallerOscillationRatio) * (1f - invertedSmallerOscillationRatio);
			float shieldScale2 = (minScale + maxPulseScale * smallerOscillationScale) * invertedOscillationUsedForScale;
			Texture2D shieldTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleOpenCircle", (AssetRequestMode)2).Value;
			Rectangle shieldFrame = shieldTexture.Frame();
			Vector2 origin = shieldFrame.Size() * 0.5f;
			Vector2 shieldDrawPos = base.NPC.Center - screenPos;
			shieldDrawPos -= new Vector2((float)shieldTexture.Width, (float)shieldTexture.Height) * base.NPC.scale / 2f;
			shieldDrawPos += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
			float minHue = ((defenderAlive & healerAlive) ? 0.18f : 0.06f);
			float maxHue = minHue + 0.12f;
			float opacityScaleDuringShieldDespawn = (120f - base.NPC.localAI[1]) / 120f;
			float scaleDuringShieldDespawnScale = 1.8f;
			float scaleDuringShieldDespawn = (1f - opacityScaleDuringShieldDespawn) * scaleDuringShieldDespawnScale;
			float colorScale = MathHelper.Lerp(0f, shieldOpacity, opacityScaleDuringShieldDespawn);
			Color color = Main.hslToRgb(MathHelper.Lerp(maxHue - minHue, maxHue, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f)) + 1f) * 0.5f), 1f, 0.5f) * colorScale;
			Color color2 = Main.hslToRgb(MathHelper.Lerp(minHue, maxHue - minHue, ((float)Math.Sin(Main.GlobalTimeWrappedHourly * (float)Math.PI * 3f) + 1f) * 0.5f), 1f, 0.5f) * colorScale;
			((Color)(ref color2)).A = 0;
			color *= 0.6f;
			color2 *= 0.6f;
			float scaleMult = 1.2f + scaleDuringShieldDespawn;
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
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color, base.NPC.rotation, origin, shieldScale2 * scaleMult, (SpriteEffects)0, 0f);
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color2, base.NPC.rotation, origin, shieldScale2 * scaleMult * 0.95f, (SpriteEffects)0, 0f);
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color, base.NPC.rotation, origin, shieldScale * scaleMult, (SpriteEffects)0, 0f);
			spriteBatch.Draw(shieldTexture, shieldDrawPos, (Rectangle?)shieldFrame, color2, base.NPC.rotation, origin, shieldScale * scaleMult * 0.95f, (SpriteEffects)0, 0f);
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
		void drawGuardianInstance(Vector2 val, Color? colorOverride)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0218: Unknown result type (might be due to invalid IL or missing references)
			//IL_0219: Unknown result type (might be due to invalid IL or missing references)
			//IL_021b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0238: Unknown result type (might be due to invalid IL or missing references)
			//IL_0248: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Unknown result type (might be due to invalid IL or missing references)
			//IL_0257: Unknown result type (might be due to invalid IL or missing references)
			//IL_025c: Unknown result type (might be due to invalid IL or missing references)
			//IL_025e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0260: Unknown result type (might be due to invalid IL or missing references)
			//IL_026c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0281: Unknown result type (might be due to invalid IL or missing references)
			//IL_0286: Unknown result type (might be due to invalid IL or missing references)
			//IL_028b: Unknown result type (might be due to invalid IL or missing references)
			//IL_028c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0291: Unknown result type (might be due to invalid IL or missing references)
			//IL_0296: Unknown result type (might be due to invalid IL or missing references)
			//IL_029f: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0304: Unknown result type (might be due to invalid IL or missing references)
			//IL_0309: Unknown result type (might be due to invalid IL or missing references)
			//IL_0313: Unknown result type (might be due to invalid IL or missing references)
			//IL_0318: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_032c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0331: Unknown result type (might be due to invalid IL or missing references)
			//IL_0111: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0138: Unknown result type (might be due to invalid IL or missing references)
			//IL_013d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0143: Unknown result type (might be due to invalid IL or missing references)
			//IL_0148: Unknown result type (might be due to invalid IL or missing references)
			//IL_014d: Unknown result type (might be due to invalid IL or missing references)
			//IL_014f: Unknown result type (might be due to invalid IL or missing references)
			//IL_016c: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_018b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0190: Unknown result type (might be due to invalid IL or missing references)
			//IL_0192: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01db: Unknown result type (might be due to invalid IL or missing references)
			//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0102: Unknown result type (might be due to invalid IL or missing references)
			//IL_033e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0343: Unknown result type (might be due to invalid IL or missing references)
			//IL_04da: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0502: Unknown result type (might be due to invalid IL or missing references)
			//IL_050a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0517: Unknown result type (might be due to invalid IL or missing references)
			//IL_0527: Unknown result type (might be due to invalid IL or missing references)
			//IL_0533: Unknown result type (might be due to invalid IL or missing references)
			//IL_035c: Unknown result type (might be due to invalid IL or missing references)
			//IL_035e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0360: Unknown result type (might be due to invalid IL or missing references)
			//IL_0362: Unknown result type (might be due to invalid IL or missing references)
			//IL_036c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0371: Unknown result type (might be due to invalid IL or missing references)
			//IL_0379: Unknown result type (might be due to invalid IL or missing references)
			//IL_037b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0380: Unknown result type (might be due to invalid IL or missing references)
			//IL_0382: Unknown result type (might be due to invalid IL or missing references)
			//IL_0390: Unknown result type (might be due to invalid IL or missing references)
			//IL_0395: Unknown result type (might be due to invalid IL or missing references)
			//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0411: Unknown result type (might be due to invalid IL or missing references)
			//IL_0421: Unknown result type (might be due to invalid IL or missing references)
			//IL_042b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0430: Unknown result type (might be due to invalid IL or missing references)
			//IL_0435: Unknown result type (might be due to invalid IL or missing references)
			//IL_0437: Unknown result type (might be due to invalid IL or missing references)
			//IL_0439: Unknown result type (might be due to invalid IL or missing references)
			//IL_0445: Unknown result type (might be due to invalid IL or missing references)
			//IL_045a: Unknown result type (might be due to invalid IL or missing references)
			//IL_045f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0464: Unknown result type (might be due to invalid IL or missing references)
			//IL_0465: Unknown result type (might be due to invalid IL or missing references)
			//IL_046a: Unknown result type (might be due to invalid IL or missing references)
			//IL_046f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0478: Unknown result type (might be due to invalid IL or missing references)
			//IL_0480: Unknown result type (might be due to invalid IL or missing references)
			//IL_048a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0497: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
			SpriteEffects spriteEffects = (SpriteEffects)0;
			if (base.NPC.spriteDirection == 1)
			{
				spriteEffects = (SpriteEffects)1;
			}
			Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
			Vector2 drawPos = base.NPC.Center - screenPos;
			Vector2 halfSizeTexture = default(Vector2);
			((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
			int afterimageAmt = 5;
			if (base.NPC.ai[0] == 2f)
			{
				afterimageAmt = 10;
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int j = 1; j < afterimageAmt; j += 2)
				{
					Color afterimageColor = drawColor;
					afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
					afterimageColor = base.NPC.GetAlpha(afterimageColor);
					afterimageColor *= (float)(afterimageAmt - j) / 15f;
					if (colorOverride.HasValue)
					{
						afterimageColor = colorOverride.Value;
					}
					Vector2 afterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
					spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
			}
			Vector2 drawLocation = drawPos;
			drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
			drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, (Color)(((_003F?)colorOverride) ?? base.NPC.GetAlpha(drawColor)), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			texture2D15 = Texture_Glow.Value;
			Color timeBasedColorLerp = Color.Lerp(Color.White, Color.Yellow, 0.5f);
			if (Main.zenithWorld)
			{
				texture2D15 = TextureNight_Glow.Value;
				timeBasedColorLerp = Main.DiscoColor;
			}
			if (colorOverride.HasValue)
			{
				timeBasedColorLerp = colorOverride.Value;
			}
			if (CalamityClientConfig.Instance.Afterimages)
			{
				for (int k = 1; k < afterimageAmt; k++)
				{
					Color timeBasedAfterimageColor = timeBasedColorLerp;
					timeBasedAfterimageColor = Color.Lerp(timeBasedAfterimageColor, Color.White, 0.5f);
					timeBasedAfterimageColor = base.NPC.GetAlpha(timeBasedAfterimageColor);
					timeBasedAfterimageColor *= (float)(afterimageAmt - k) / 15f;
					if (colorOverride.HasValue)
					{
						timeBasedAfterimageColor = colorOverride.Value;
					}
					Vector2 timeBasedAfterimagePos = base.NPC.oldPos[k] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
					timeBasedAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
					timeBasedAfterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY) + val;
					spriteBatch.Draw(texture2D15, timeBasedAfterimagePos, (Rectangle?)base.NPC.frame, timeBasedAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
				}
			}
			base.NPC.DrawBackglow((Color)(Main.zenithWorld ? Main.DiscoColor : new Color(255, 64, 0, 0)), 4f, spriteEffects, base.NPC.frame, Main.screenPosition, texture2D15);
			spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, ProvUtils.GetColorBasedOnEnrage(Night: false, 0), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 3544;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<RelicOfDeliverance>(), 4);
		npcLoot.Add(ModContent.ItemType<ProfanedGuardianMask>(), 7);
		LeadingConditionRule expert = new LeadingConditionRule(new Conditions.IsExpert());
		expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WarbanneroftheRighteous>()));
		npcLoot.Add(expert);
		npcLoot.Add(ModContent.ItemType<ProfanedGuardianTrophy>(), 10);
		npcLoot.Add(ModContent.ItemType<ProfanedCore>());
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<UnholyEssence>(), 1, 15, 20, 20, 25));
		npcLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<ProfanedGuardiansRelic>());
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(4016, 1, 1, 9999), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<DivineGeode>(), 1, 25, 30), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedGuardians, ModContent.ItemType<LoreProfanedGuardians>(), ui: true, DropHelper.FirstKillText);
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			DownedBossSystem.downedGuardians = true;
			CalamityNetcode.SyncWorld();
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
		return minDist <= 80f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossA").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossA2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossA3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedGuardianBossA4").Type);
			}
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
			}
		}
	}
}
