using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Events;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Boss;
using CalamityMod.UI.VanillaBossBars;
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

namespace CalamityMod.NPCs.SlimeGod;

[AutoloadBossHead]
public class SlimeGodCore : ModNPC
{
	private bool slimesSpawned;

	private int buffedSlime;

	public static readonly SoundStyle PossessionSound = new SoundStyle("CalamityMod/Sounds/Custom/SlimeGodPossession");

	public static readonly SoundStyle ExitSound = new SoundStyle("CalamityMod/Sounds/Custom/SlimeGodExit");

	public static readonly SoundStyle ShotSound = new SoundStyle("CalamityMod/Sounds/Custom/SlimeGodShot", 2);

	public static readonly SoundStyle BigShotSound = new SoundStyle("CalamityMod/Sounds/Custom/SlimeGodBigShot", 2);

	public static Asset<Texture2D> EyeTexture;

	public static int GlobDamage = 15;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		NPCID.Sets.TrailCacheLength[base.Type] = 8;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		if (!Main.dedServ)
		{
			EyeTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/SlimeGod/SlimeGodEyes", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 40;
		base.NPC.npcSlots = 10f;
		base.NPC.width = 44;
		base.NPC.height = 44;
		if (Main.getGoodWorld)
		{
			base.NPC.scale = 2f;
		}
		base.NPC.defense = 6;
		base.NPC.LifeMaxNERB(420);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 8);
		base.NPC.Opacity = 0.8f;
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<SlimeGodBossBar>();
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SlimeGodCore")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(slimesSpawned);
		writer.Write(buffedSlime);
		writer.Write(base.NPC.Opacity);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		slimesSpawned = reader.ReadBoolean();
		buffedSlime = reader.ReadInt32();
		base.NPC.Opacity = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1188: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_109c: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1024: Unknown result type (might be due to invalid IL or missing references)
		//IL_102f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1034: Unknown result type (might be due to invalid IL or missing references)
		//IL_1039: Unknown result type (might be due to invalid IL or missing references)
		//IL_1040: Unknown result type (might be due to invalid IL or missing references)
		//IL_1045: Unknown result type (might be due to invalid IL or missing references)
		//IL_1059: Unknown result type (might be due to invalid IL or missing references)
		//IL_105e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d90: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1303: Unknown result type (might be due to invalid IL or missing references)
		//IL_1305: Unknown result type (might be due to invalid IL or missing references)
		//IL_1312: Unknown result type (might be due to invalid IL or missing references)
		//IL_1317: Unknown result type (might be due to invalid IL or missing references)
		//IL_1331: Unknown result type (might be due to invalid IL or missing references)
		//IL_133b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1340: Unknown result type (might be due to invalid IL or missing references)
		//IL_1342: Unknown result type (might be due to invalid IL or missing references)
		//IL_134c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1351: Unknown result type (might be due to invalid IL or missing references)
		//IL_136b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1375: Unknown result type (might be due to invalid IL or missing references)
		//IL_137a: Unknown result type (might be due to invalid IL or missing references)
		//IL_137c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1386: Unknown result type (might be due to invalid IL or missing references)
		//IL_138b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ead: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ede: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f65: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f74: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.slimeGod = base.NPC.whoAmI;
		bool bossRush = BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode | bossRush;
		bool revenge = CalamityWorld.revenge | bossRush;
		bool death = CalamityWorld.death | bossRush;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (!slimesSpawned)
		{
			slimesSpawned = true;
			if (Main.netMode != 1)
			{
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<EbonianPaladin>());
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<CrimulanPaladin>());
			}
		}
		bool purpleSlimeAlive = false;
		bool redSlimeAlive = false;
		if (CalamityGlobalNPC.slimeGodPurple != -1 && Main.npc[CalamityGlobalNPC.slimeGodPurple].active)
		{
			if (buffedSlime == 1)
			{
				Main.npc[CalamityGlobalNPC.slimeGodPurple].localAI[1] = 1f;
			}
			else
			{
				Main.npc[CalamityGlobalNPC.slimeGodPurple].localAI[1] = 0f;
			}
			calamityGlobalNPC.newAI[0] = Main.npc[CalamityGlobalNPC.slimeGodPurple].Center.X;
			calamityGlobalNPC.newAI[1] = Main.npc[CalamityGlobalNPC.slimeGodPurple].Center.Y;
			calamityGlobalNPC.newAI[3] = ((Main.npc[CalamityGlobalNPC.slimeGodPurple].ai[0] == 4f) ? 1f : 0f);
			purpleSlimeAlive = true;
		}
		if (CalamityGlobalNPC.slimeGodRed != -1 && Main.npc[CalamityGlobalNPC.slimeGodRed].active)
		{
			if (buffedSlime == 2)
			{
				Main.npc[CalamityGlobalNPC.slimeGodRed].localAI[1] = 1f;
			}
			else
			{
				Main.npc[CalamityGlobalNPC.slimeGodRed].localAI[1] = 0f;
			}
			base.NPC.ai[1] = Main.npc[CalamityGlobalNPC.slimeGodRed].Center.X;
			base.NPC.ai[2] = Main.npc[CalamityGlobalNPC.slimeGodRed].Center.Y;
			calamityGlobalNPC.newAI[3] = ((Main.npc[CalamityGlobalNPC.slimeGodRed].ai[0] == 3f) ? 1f : 0f);
			redSlimeAlive = true;
		}
		bool phase2 = !purpleSlimeAlive || !redSlimeAlive;
		if ((!purpleSlimeAlive && !redSlimeAlive) || calamityGlobalNPC.newAI[3] == 1f || base.NPC.ai[3] == 1f)
		{
			base.NPC.damage = 0;
			if (base.NPC.ai[3] == 0f && !NPC.AnyNPCs(ModContent.NPCType<EbonianPaladin>()) && !NPC.AnyNPCs(ModContent.NPCType<CrimulanPaladin>()))
			{
				base.NPC.ai[3] = 1f;
				base.NPC.Opacity = 0.8f;
			}
			if (!Main.zenithWorld)
			{
				for (int k = 0; k < 5; k++)
				{
					Color color = (Main.rand.NextBool() ? Color.Lavender : Color.Crimson);
					((Color)(ref color)).A = 150;
					Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, color);
				}
			}
			NPC nPC = base.NPC;
			nPC.velocity *= 0.97f;
			base.NPC.rotation += (float)base.NPC.direction * 0.3f;
			base.NPC.Opacity -= 0.005f;
			if (!(base.NPC.Opacity <= 0f))
			{
				return;
			}
			base.NPC.Opacity = 0f;
			SoundEngine.PlaySound(in PossessionSound, base.NPC.Center);
			base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
			base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
			base.NPC.width = 40;
			base.NPC.height = 40;
			base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
			base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
			for (int i = 0; i < 40; i++)
			{
				Color color2 = (Main.rand.NextBool() ? Color.Lavender : Color.Crimson);
				((Color)(ref color2)).A = 150;
				int slimyDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, color2, 2f);
				Dust obj = Main.dust[slimyDust];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[slimyDust].scale = 0.5f;
					Main.dust[slimyDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 70; j++)
			{
				Color color3 = (Main.rand.NextBool() ? Color.Lavender : Color.Crimson);
				((Color)(ref color3)).A = 150;
				int slimyDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, color3, 3f);
				Main.dust[slimyDust2].noGravity = true;
				Dust obj2 = Main.dust[slimyDust2];
				obj2.velocity *= 5f;
				slimyDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, color3, 2f);
				Dust obj3 = Main.dust[slimyDust2];
				obj3.velocity *= 2f;
			}
			if (calamityGlobalNPC.newAI[3] != 1f)
			{
				if (!DownedBossSystem.downedSlimeGod)
				{
					Color messageColor = Color.Magenta;
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.SlimeGodRun", messageColor);
				}
				for (int i2 = 254; i2 >= 0; i2--)
				{
					base.NPC.ApplyInteraction(i2);
				}
				base.NPC.active = false;
				base.NPC.HitEffect();
				base.NPC.NPCLoot();
				base.NPC.netUpdate = true;
				return;
			}
			for (int x = 0; x < Main.maxNPCs; x++)
			{
				if (Main.npc[x].type == ModContent.NPCType<EbonianPaladin>() || Main.npc[x].type == ModContent.NPCType<SplitEbonianPaladin>() || Main.npc[x].type == ModContent.NPCType<CrimulanPaladin>() || Main.npc[x].type == ModContent.NPCType<SplitCrimulanPaladin>())
				{
					Main.npc[x].active = false;
					Main.npc[x].netUpdate = true;
				}
			}
			base.NPC.active = false;
			base.NPC.HitEffect();
			base.NPC.netUpdate = true;
			return;
		}
		if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 3200f)
			{
				if (base.NPC.velocity.Y < -3f)
				{
					base.NPC.velocity.Y = -3f;
				}
				base.NPC.velocity.Y += 0.2f;
				if (base.NPC.velocity.Y > 16f)
				{
					base.NPC.velocity.Y = 16f;
				}
				if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
				{
					for (int l = 0; l < Main.maxNPCs; l++)
					{
						if (Main.npc[l].type == ModContent.NPCType<EbonianPaladin>() || Main.npc[l].type == ModContent.NPCType<SplitEbonianPaladin>() || Main.npc[l].type == ModContent.NPCType<CrimulanPaladin>() || Main.npc[l].type == ModContent.NPCType<SplitCrimulanPaladin>())
						{
							Main.npc[l].active = false;
							Main.npc[l].netUpdate = true;
						}
					}
					base.NPC.active = false;
					base.NPC.netUpdate = true;
				}
				base.NPC.Opacity = 0.8f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				calamityGlobalNPC.newAI[0] = 0f;
				calamityGlobalNPC.newAI[1] = 0f;
				calamityGlobalNPC.newAI[2] = 0f;
				base.NPC.netUpdate = true;
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		float hideInsideLargeSlimePhaseGateValue = (phase2 ? 300f : 900f);
		float hideInsideLargeSlimePhaseDuration = 600f;
		float exitLargeSlimeGateValue = hideInsideLargeSlimePhaseGateValue + hideInsideLargeSlimePhaseDuration;
		calamityGlobalNPC.newAI[2]++;
		if (calamityGlobalNPC.newAI[2] >= hideInsideLargeSlimePhaseGateValue)
		{
			base.NPC.damage = 0;
			base.NPC.rotation += (float)base.NPC.direction * 0.3f;
			if (buffedSlime == 0)
			{
				SoundEngine.PlaySound(in PossessionSound, base.NPC.Center);
				if (purpleSlimeAlive & redSlimeAlive)
				{
					buffedSlime = Main.rand.Next(2) + 1;
				}
				else if (purpleSlimeAlive)
				{
					buffedSlime = 1;
				}
				else if (redSlimeAlive)
				{
					buffedSlime = 2;
				}
			}
			Vector2 purpleSlimeVector = default(Vector2);
			((Vector2)(ref purpleSlimeVector))._002Ector(calamityGlobalNPC.newAI[0], calamityGlobalNPC.newAI[1]);
			Vector2 redSlimeVector = default(Vector2);
			((Vector2)(ref redSlimeVector))._002Ector(base.NPC.ai[1], base.NPC.ai[2]);
			Vector2 goToVector = ((buffedSlime == 1) ? purpleSlimeVector : redSlimeVector);
			Vector2 goToPosition = goToVector - base.NPC.Center;
			base.NPC.velocity = Vector2.Normalize(goToPosition) * 24f;
			if (Vector2.Distance(base.NPC.Center, goToVector) < 24f)
			{
				base.NPC.velocity = Vector2.Zero;
				base.NPC.Opacity -= 0.2f;
				if (base.NPC.Opacity < 0f)
				{
					base.NPC.Opacity = 0f;
				}
			}
			bool slimeDead = ((!(goToVector == purpleSlimeVector)) ? (CalamityGlobalNPC.slimeGodRed < 0 || !Main.npc[CalamityGlobalNPC.slimeGodRed].active) : (CalamityGlobalNPC.slimeGodPurple < 0 || !Main.npc[CalamityGlobalNPC.slimeGodPurple].active));
			if (!((calamityGlobalNPC.newAI[2] >= exitLargeSlimeGateValue) | slimeDead))
			{
				return;
			}
			base.NPC.TargetClosest();
			calamityGlobalNPC.newAI[2] = 0f;
			base.NPC.velocity = Vector2.UnitY * -12f;
			SoundEngine.PlaySound(in ExitSound, base.NPC.Center);
			for (int m = 0; m < 20; m++)
			{
				Color color4 = (Main.rand.NextBool() ? Color.Lavender : Color.Crimson);
				((Color)(ref color4)).A = 150;
				int dust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, color4, 2f);
				Dust obj4 = Main.dust[dust2];
				obj4.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[dust2].scale = 0.5f;
					Main.dust[dust2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int n = 0; n < 30; n++)
			{
				Color color5 = (Main.rand.NextBool() ? Color.Lavender : Color.Crimson);
				((Color)(ref color5)).A = 150;
				int dust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, color5, 3f);
				Main.dust[dust3].noGravity = true;
				Dust obj5 = Main.dust[dust3];
				obj5.velocity *= 5f;
				dust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, 0f, 0f, base.NPC.alpha, color5, 2f);
				Dust obj6 = Main.dust[dust3];
				obj6.velocity *= 2f;
			}
			return;
		}
		base.NPC.damage = 0;
		if (expertMode)
		{
			float divisor = (death ? 180f : (revenge ? 240f : 300f));
			if (phase2)
			{
				divisor /= 2f;
			}
			if (calamityGlobalNPC.newAI[2] % divisor == 0f)
			{
				SoundEngine.PlaySound(in ShotSound, base.NPC.Center);
				if (Main.netMode != 1)
				{
					if (Main.rand.NextBool())
					{
						float projectileVelocity = 4f;
						int type = ModContent.ProjectileType<UnstableEbonianGlob>();
						int damage = GlobDamage;
						Vector2 velocity = Vector2.Normalize(player.Center - base.NPC.Center) * projectileVelocity;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity, type, damage, 0f, Main.myPlayer);
					}
					else
					{
						float projectileVelocity2 = 8f;
						int type2 = ModContent.ProjectileType<UnstableCrimulanGlob>();
						int damage2 = GlobDamage;
						Vector2 velocity2 = Vector2.Normalize(player.Center - base.NPC.Center) * projectileVelocity2;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity2, type2, damage2, 0f, Main.myPlayer);
					}
				}
			}
		}
		base.NPC.Opacity += 0.2f;
		if (base.NPC.Opacity > 0.8f)
		{
			base.NPC.Opacity = 0.8f;
		}
		buffedSlime = 0;
		float flySpeed = (death ? 15f : (revenge ? 13.5f : (expertMode ? 12f : 9f)));
		if (phase2)
		{
			flySpeed *= 1.25f;
		}
		if (Main.getGoodWorld)
		{
			flySpeed *= 1.25f;
		}
		Vector2 flyDirection = default(Vector2);
		((Vector2)(ref flyDirection))._002Ector(base.NPC.Center.X + (float)(base.NPC.direction * 20), base.NPC.Center.Y + 6f);
		Vector2 flyDestination = GetFlyDestination(player);
		Vector2 idealVelocity = (flyDestination - flyDirection).SafeNormalize(Vector2.UnitY) * flySpeed;
		float distanceFromFlyDestination = base.NPC.Distance(flyDestination);
		base.NPC.ai[0]--;
		if (distanceFromFlyDestination < 200f || base.NPC.ai[0] > 0f)
		{
			base.NPC.damage = base.NPC.defDamage;
			if (distanceFromFlyDestination < 200f)
			{
				base.NPC.ai[0] = 20f;
			}
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.rotation += (float)base.NPC.direction * 0.3f;
			return;
		}
		float inertia = 50f;
		if (Main.getGoodWorld)
		{
			inertia *= 0.8f;
		}
		if (CalamityWorld.LegendaryMode && CalamityWorld.revenge)
		{
			inertia -= (float)Main.rand.Next(31);
		}
		base.NPC.velocity = (base.NPC.velocity * inertia + idealVelocity) / (inertia + 1f);
		if (distanceFromFlyDestination < 350f)
		{
			base.NPC.velocity = (base.NPC.velocity * 10f + idealVelocity) / 11f;
		}
		if (distanceFromFlyDestination < 300f)
		{
			base.NPC.velocity = (base.NPC.velocity * 7f + idealVelocity) / 8f;
		}
		base.NPC.rotation = base.NPC.velocity.X * 0.1f;
	}

	public Vector2 GetFlyDestination(Player target)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		int largeCrimulanPaladin = ModContent.NPCType<CrimulanPaladin>();
		int splitCrimulanPaladin = ModContent.NPCType<SplitCrimulanPaladin>();
		int largeEbonianPaladin = ModContent.NPCType<EbonianPaladin>();
		int splitEbonianPaladin = ModContent.NPCType<SplitEbonianPaladin>();
		List<NPC> largeSlimes = new List<NPC>();
		float ignoreGeneralAreaDistanceThreshold = 750f;
		float ignoreAllSlimesDistanceThreshold = 3200f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			int npcType = n.type;
			if ((npcType == largeCrimulanPaladin || npcType == splitCrimulanPaladin || npcType == largeEbonianPaladin || npcType == splitEbonianPaladin) && base.NPC.WithinRange(n.Center, ignoreAllSlimesDistanceThreshold))
			{
				largeSlimes.Add(n);
			}
		}
		if (largeSlimes.Count <= 0)
		{
			return target.Center;
		}
		NPC closestSlime = largeSlimes.OrderBy(delegate(NPC nPC)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return nPC.Distance(base.NPC.Center);
		}).First();
		Vector2 generalSlimeArea = Vector2.Zero;
		for (int i = 0; i < largeSlimes.Count; i++)
		{
			generalSlimeArea += largeSlimes[i].Center;
		}
		generalSlimeArea /= (float)largeSlimes.Count;
		if (largeSlimes.Average(delegate(NPC s)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			return s.Distance(generalSlimeArea);
		}) > ignoreGeneralAreaDistanceThreshold)
		{
			return closestSlime.Center;
		}
		return generalSlimeArea;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		return (Color)((buffedSlime == 0) ? new Color(255, 255, 255, (int)((Color)(ref drawColor)).A) : drawColor) * base.NPC.Opacity;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Color drawColorAlpha = base.NPC.GetAlpha(drawColor);
		Color colorLightingArea = Lighting.GetColor((int)((double)base.NPC.position.X + (double)base.NPC.width * 0.5) / 16, (int)(((double)base.NPC.position.Y + (double)base.NPC.height * 0.5) / 16.0));
		Texture2D texture2D3 = TextureAssets.Npc[base.NPC.type].Value;
		Texture2D pog = EyeTexture.Value;
		int frameTexture = TextureAssets.Npc[base.NPC.type].Value.Height / Main.npcFrameCount[base.NPC.type];
		int y3 = frameTexture * (int)base.NPC.frameCounter;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, y3, texture2D3.Width, frameTexture);
		Vector2 halfRect = rectangle.Size() / 2f;
		int twoConst = 2;
		int coreID = 1;
		spriteBatch.Draw(texture2D3, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY), (Rectangle?)base.NPC.frame, drawColorAlpha, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, spriteEffects, 0f);
		if (Main.zenithWorld)
		{
			spriteBatch.Draw(pog, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY), (Rectangle?)base.NPC.frame, drawColorAlpha, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, spriteEffects, 0f);
		}
		if (!Main.zenithWorld)
		{
			for (; (twoConst > 0 && coreID < 8) || (twoConst < 0 && coreID > 8); coreID += twoConst)
			{
				if (!CalamityClientConfig.Instance.Afterimages)
				{
					break;
				}
				Color colorLightingAlpha = base.NPC.GetAlpha(colorLightingArea);
				float trailLengthMult = 8 - coreID;
				if (twoConst < 0)
				{
					trailLengthMult = 1 - coreID;
				}
				colorLightingAlpha *= trailLengthMult / ((float)NPCID.Sets.TrailCacheLength[base.NPC.type] * 1.5f);
				Vector2 drawPosition = base.NPC.oldPos[coreID];
				float coreRotate = base.NPC.rotation;
				Main.spriteBatch.Draw(texture2D3, drawPosition + base.NPC.Size / 2f - screenPos + new Vector2(0f, base.NPC.gfxOffY), (Rectangle?)rectangle, colorLightingAlpha, coreRotate + base.NPC.rotation * 0f * (float)(coreID - 1) * (0f - (float)((Enum)spriteEffects).HasFlag((Enum)(object)(SpriteEffects)1).ToDirectionInt()), halfRect, base.NPC.scale, spriteEffects, 0f);
			}
		}
		return false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 188;
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			DownedBossSystem.downedSlimeGod = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(23, 1, 32, 48);
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SlimeGodBag>()));
		LeadingConditionRule normalOnly = new LeadingConditionRule(new Conditions.NotExpert());
		npcLoot.Add(normalOnly);
		int[] weapons = new int[5]
		{
			ModContent.ItemType<OverloadedBlaster>(),
			ModContent.ItemType<AbyssalTome>(),
			ModContent.ItemType<EldritchTome>(),
			ModContent.ItemType<CorroslimeStaff>(),
			ModContent.ItemType<CrimslimeStaff>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<PurifiedGel>(), 1, 30, 45));
		normalOnly.Add(ModContent.ItemType<SlimeGodMask>(), 7);
		normalOnly.Add(ModContent.ItemType<SlimeGodMask2>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<SlimeGodTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<SlimeGodRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(4988), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedSlimeGod, ModContent.ItemType<LoreSlimeGod>(), ui: true, DropHelper.FirstKillText);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Color color = (Main.rand.NextBool() ? Color.Lavender : Color.Crimson);
			((Color)(ref color)).A = 150;
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 4, hit.HitDirection, -1f, base.NPC.alpha, color);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			int debufftype = (Main.zenithWorld ? 164 : 32);
			target.AddBuff(debufftype, 180);
		}
	}
}
