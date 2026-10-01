using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.AcidRain;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.Systems.Collections;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AquaticScourge;

[AutoloadBossHead]
[HasPierceResist(false)]
[LongDistanceNetSync]
public class AquaticScourgeHead : ModNPC
{
	public static int MistDamage = 23;

	public static int CloudDamage = 26;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.6f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.6f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 40f;
		value.Position.Y += 20f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 85;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.width = 90;
		base.NPC.height = 90;
		base.NPC.defense = 10;
		base.NPC.DR_NERD(0.05f);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.LifeMaxNERB(80000, 96000, 1000000);
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 12);
		base.NPC.behindTiles = true;
		base.NPC.chaseable = false;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
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
			base.NPC.scale *= 1.25f;
		}
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SulphurousSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[base.Type], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			new BossBestiaryInfoElement(),
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AquaticScourge")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		writer.Write(base.NPC.npcSlots);
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
		base.NPC.npcSlots = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a98: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a38: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_1237: Unknown result type (might be due to invalid IL or missing references)
		//IL_1250: Unknown result type (might be due to invalid IL or missing references)
		//IL_1269: Unknown result type (might be due to invalid IL or missing references)
		//IL_1275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1106: Unknown result type (might be due to invalid IL or missing references)
		//IL_1112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1158: Unknown result type (might be due to invalid IL or missing references)
		//IL_1142: Unknown result type (might be due to invalid IL or missing references)
		//IL_11eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1206: Unknown result type (might be due to invalid IL or missing references)
		//IL_120d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_1049: Unknown result type (might be due to invalid IL or missing references)
		//IL_1054: Unknown result type (might be due to invalid IL or missing references)
		//IL_106b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1076: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool getFuckedAI = Main.zenithWorld;
		CalamityGlobalNPC.aquaticScourge = base.NPC.whoAmI;
		bool nonHostile = calamityGlobalNPC.newAI[0] == 0f;
		if (base.NPC.justHit || (double)base.NPC.life <= (double)base.NPC.lifeMax * 0.999 || BossRushEvent.BossRushActive || Main.zenithWorld)
		{
			if (nonHostile)
			{
				base.NPC.timeLeft *= 20;
				base.NPC.npcSlots = 16f;
				base.NPC.damage = base.NPC.defDamage;
				CalamityGlobalNPC.BossKillTimes.TryGetValue(base.NPC.type, out var revKillTime);
				calamityGlobalNPC.KillTime = revKillTime;
				calamityGlobalNPC.newAI[0] = 1f;
				nonHostile = false;
				base.NPC.boss = true;
				base.NPC.chaseable = true;
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			base.NPC.damage = 0;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = lifeRatio < 0.75f;
		bool phase3 = lifeRatio < 0.5f;
		bool phase4 = lifeRatio < 0.25f;
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		bool notOcean = player.position.Y < 300f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 7680f && player.position.X < (float)(Main.maxTilesX * 16 - 7680));
		if (Main.remixWorld)
		{
			notOcean = player.position.Y < (float)Main.UnderworldLayer * 0.8f || player.position.Y > (float)Main.UnderworldLayer || (player.position.X > 7680f && player.position.X < (float)(Main.maxTilesX * 16 - 7680));
		}
		if (notOcean && !player.Calamity().ZoneSulphur && !BossRushEvent.BossRushActive)
		{
			if (base.NPC.localAI[2] > 0f)
			{
				base.NPC.localAI[2]--;
			}
		}
		else
		{
			base.NPC.localAI[2] = 300f;
		}
		bool biomeEnraged = base.NPC.localAI[2] <= 0f;
		float enrageScale = 0f;
		if (biomeEnraged)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale += 2f;
		}
		float colorFadeTimeAfterSpiral = 90f;
		float spiralGateValue = 480f;
		bool doSpiral = false;
		if (calamityGlobalNPC.newAI[0] == 1f && calamityGlobalNPC.newAI[2] == 1f && (revenge | getFuckedAI))
		{
			doSpiral = calamityGlobalNPC.newAI[1] == 0f && calamityGlobalNPC.newAI[3] >= spiralGateValue;
			if ((Vector2.Distance(base.NPC.Center, player.Center) < (getFuckedAI ? 1600f : 1000f)) | doSpiral)
			{
				calamityGlobalNPC.newAI[3]++;
			}
			if (doSpiral)
			{
				base.NPC.localAI[3] = colorFadeTimeAfterSpiral;
				float acidMistBarfDivisor = (getFuckedAI ? 2f : ((float)Math.Floor(death ? 5f : 6f) * (phase3 ? 1.5f : 1f)));
				if (calamityGlobalNPC.newAI[3] % acidMistBarfDivisor == 0f && Main.netMode != 1)
				{
					float mistVelocity = (death ? 10f : 8f);
					Vector2 projectileVelocity = (base.NPC.Center + base.NPC.velocity * 10f - base.NPC.Center).SafeNormalize(Vector2.UnitY);
					int type = ModContent.ProjectileType<SulphuricAcidMist>();
					int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + projectileVelocity * 5f, projectileVelocity * mistVelocity, type, MistDamage, 0f, Main.myPlayer);
					Main.projectile[proj].tileCollide = false;
					Main.projectile[proj].timeLeft = (getFuckedAI ? 240 : 600);
				}
				float toxicCloudBarfDivisor = (death ? 30f : 40f);
				if (((calamityGlobalNPC.newAI[3] % toxicCloudBarfDivisor == 0f) & phase3) && Main.netMode != 1)
				{
					int type2 = ModContent.ProjectileType<ToxicCloud>();
					int totalProjectiles = (phase4 ? 6 : 9) + (getFuckedAI ? Main.rand.Next(-2, 3) : ((int)((calamityGlobalNPC.newAI[3] - spiralGateValue) / toxicCloudBarfDivisor) * (phase4 ? 2 : 3)));
					float radians = (float)Math.PI * 2f / (float)totalProjectiles;
					float cloudVelocity = 1f + enrageScale;
					Vector2 spinningPoint = default(Vector2);
					((Vector2)(ref spinningPoint))._002Ector(0f, 0f - cloudVelocity);
					for (int k = 0; k < totalProjectiles; k++)
					{
						Vector2 vector255 = spinningPoint.RotatedBy(radians * (float)k);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + vector255.SafeNormalize(Vector2.UnitY) * 5f, vector255, type2, CloudDamage, 0f, Main.myPlayer);
					}
				}
				if (calamityGlobalNPC.newAI[3] == spiralGateValue)
				{
					base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.UnitY);
					NPC nPC = base.NPC;
					nPC.velocity *= 24f;
				}
				float velocity = (float)Math.PI / 60f;
				if (getFuckedAI)
				{
					velocity *= (phase3 ? 1.5f : (phase2 ? 1.25f : 1f));
				}
				base.NPC.velocity = base.NPC.velocity.RotatedBy((0.0 - (double)velocity) * (double)base.NPC.localAI[1]);
				if (getFuckedAI && ((Vector2)(ref base.NPC.velocity)).Length() <= 32f)
				{
					NPC nPC2 = base.NPC;
					nPC2.velocity *= 1.1f;
				}
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
				if (!getFuckedAI && calamityGlobalNPC.newAI[3] >= spiralGateValue + 120f)
				{
					calamityGlobalNPC.newAI[3] = 0f;
					base.NPC.TargetClosest();
				}
			}
			else
			{
				if (!Collision.CanHit(base.NPC.Center, 1, 1, player.position, player.width, player.height) && calamityGlobalNPC.newAI[3] > 300f)
				{
					calamityGlobalNPC.newAI[3] -= 2f;
				}
				if (base.NPC.localAI[3] > 0f)
				{
					base.NPC.localAI[3]--;
				}
				base.NPC.localAI[1] = ((base.NPC.Center.X - player.Center.X < 0f) ? 1f : (-1f));
			}
		}
		bool immuneToSlowingDebuffs = doSpiral | getFuckedAI;
		base.NPC.buffImmune[ModContent.BuffType<GlacialState>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TemporalSadness>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Eutrophication>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TimeDistortion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<GalvanicCorrosion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Vaporfied>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[149] = immuneToSlowingDebuffs;
		if (calamityGlobalNPC.newAI[2] == 0f && base.NPC.ai[0] == 0f)
		{
			if (Main.netMode != 1)
			{
				int maxLength = (getFuckedAI ? 24 : (death ? 80 : (revenge ? 40 : (expertMode ? 35 : 30))));
				int Previous = base.NPC.whoAmI;
				for (int segments = 0; segments < maxLength; segments++)
				{
					int lol = ((segments < 0 || segments >= maxLength - 1) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<AquaticScourgeTail>(), base.NPC.whoAmI) : ((segments % 2 != 0) ? NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<AquaticScourgeBody>(), base.NPC.whoAmI) : NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<AquaticScourgeBodyAlt>(), base.NPC.whoAmI)));
					Main.npc[lol].realLife = base.NPC.whoAmI;
					Main.npc[lol].ai[2] = base.NPC.whoAmI;
					Main.npc[lol].ai[1] = Previous;
					Main.npc[Previous].ai[0] = lol;
					NetMessage.SendData(23, -1, -1, null, lol);
					Previous = lol;
				}
			}
			calamityGlobalNPC.newAI[2] = 1f;
		}
		if ((calamityGlobalNPC.newAI[0] == 1f && (!doSpiral & phase2)) || (getFuckedAI && !phase3))
		{
			base.NPC.localAI[0]++;
			if (base.NPC.localAI[0] >= (revenge ? 360f : 420f) && Vector2.Distance(player.Center, base.NPC.Center) > 320f)
			{
				base.NPC.localAI[0] = 0f;
				base.NPC.netUpdate = true;
				SoundEngine.PlaySound(in SoundID.NPCDeath13, base.NPC.Center);
				if (Main.netMode != 1)
				{
					int totalProjectiles2 = (expertMode ? 8 : 6);
					if (phase3)
					{
						totalProjectiles2 *= 2;
					}
					int type3 = ModContent.ProjectileType<SandPoisonCloud>();
					Vector2 velocity2 = default(Vector2);
					for (int i = 0; i < totalProjectiles2; i++)
					{
						((Vector2)(ref velocity2))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						velocity2 = velocity2.SafeNormalize(Vector2.UnitY);
						velocity2 *= (float)Main.rand.Next(phase3 ? 300 : 100, 401) * 0.01f;
						float maximumVelocityMult = (death ? 0.75f : 0.5f);
						if (expertMode)
						{
							velocity2 *= 1f + maximumVelocityMult * (0.5f - lifeRatio);
						}
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + velocity2.SafeNormalize(Vector2.UnitY) * 5f, velocity2, type3, CloudDamage, 0f, Main.myPlayer);
					}
				}
			}
		}
		if (base.NPC.life > Main.npc[(int)base.NPC.ai[0]].life)
		{
			base.NPC.life = Main.npc[(int)base.NPC.ai[0]].life;
		}
		float maxDistance = ((calamityGlobalNPC.newAI[0] == 1f) ? 12800f : 6400f);
		if (player.dead || Vector2.Distance(base.NPC.Center, player.Center) > maxDistance || (nonHostile & biomeEnraged))
		{
			calamityGlobalNPC.newAI[1] = 1f;
			base.NPC.TargetClosest(faceTarget: false);
			base.NPC.velocity.Y += 2f;
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				base.NPC.velocity.Y += 2f;
			}
			if ((double)base.NPC.position.Y > Main.worldSurface * 16.0)
			{
				for (int a = 0; a < Main.npc.Length; a++)
				{
					int type4 = Main.npc[a].type;
					if (CalamityNPCTypeSets.AquaticScourge.Contains(type4))
					{
						Main.npc[a].active = false;
					}
				}
			}
		}
		else
		{
			calamityGlobalNPC.newAI[1] = 0f;
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		base.NPC.alpha -= 42;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		Vector2 scourgePosition = base.NPC.Center;
		Vector2 predictionVector = (Main.getGoodWorld ? (Main.player[base.NPC.target].velocity * 20f) : Vector2.Zero);
		float scourgeTargetX = player.Center.X + predictionVector.X;
		float scourgeTargetY = player.Center.Y + predictionVector.Y;
		float scourgeMaxSpeed = 5f;
		float scourgeAcceleration = 0.08f;
		if (calamityGlobalNPC.newAI[0] == 1f)
		{
			scourgeMaxSpeed = (revenge ? 14.4f : 12f);
			scourgeAcceleration = (revenge ? 0.18f : 0.15f);
			if (expertMode)
			{
				scourgeMaxSpeed += 2.4f * (1f - lifeRatio);
				scourgeAcceleration += 0.03f * (1f - lifeRatio);
			}
			scourgeMaxSpeed += 3f * enrageScale;
			scourgeAcceleration += 0.06f * enrageScale;
			if (death | getFuckedAI)
			{
				scourgeMaxSpeed += 5f;
				scourgeAcceleration -= (getFuckedAI ? 0f : 0.03f);
				scourgeMaxSpeed += Vector2.Distance(player.Center, base.NPC.Center) * 0.001f;
				scourgeAcceleration += Vector2.Distance(player.Center, base.NPC.Center) * 4.5E-05f;
			}
			if (base.NPC.localAI[3] > 0f)
			{
				float accelerationMultiplier = MathHelper.Lerp(1f, 2f, base.NPC.localAI[3] / colorFadeTimeAfterSpiral);
				scourgeAcceleration *= accelerationMultiplier;
			}
			if (Main.getGoodWorld)
			{
				scourgeMaxSpeed *= 1.15f;
				scourgeAcceleration *= 1.15f;
			}
		}
		if (!doSpiral)
		{
			if (calamityGlobalNPC.newAI[0] != 1f)
			{
				scourgeTargetY += 400f;
				if (Math.Abs(base.NPC.Center.X - player.Center.X) < 500f)
				{
					scourgeTargetX = ((!(base.NPC.velocity.X > 0f)) ? (player.Center.X - 600f) : (player.Center.X + 600f));
				}
			}
			float scourgeHigherSpeed = scourgeMaxSpeed * 1.3f;
			float scourgeLowerSpeed = scourgeMaxSpeed * 0.7f;
			float scourgeSpeed = ((Vector2)(ref base.NPC.velocity)).Length();
			if (scourgeSpeed > 0f)
			{
				if (scourgeSpeed > scourgeHigherSpeed)
				{
					base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.UnitY);
					NPC nPC3 = base.NPC;
					nPC3.velocity *= scourgeHigherSpeed;
				}
				else if (scourgeSpeed < scourgeLowerSpeed)
				{
					base.NPC.velocity = base.NPC.velocity.SafeNormalize(Vector2.UnitY);
					NPC nPC4 = base.NPC;
					nPC4.velocity *= scourgeLowerSpeed;
				}
			}
		}
		scourgeTargetX = (int)(scourgeTargetX / 16f) * 16;
		scourgeTargetY = (int)(scourgeTargetY / 16f) * 16;
		scourgePosition.X = (int)(scourgePosition.X / 16f) * 16;
		scourgePosition.Y = (int)(scourgePosition.Y / 16f) * 16;
		scourgeTargetX -= scourgePosition.X;
		scourgeTargetY -= scourgePosition.Y;
		float scourgeTargetDist = (float)Math.Sqrt(scourgeTargetX * scourgeTargetX + scourgeTargetY * scourgeTargetY);
		if (doSpiral)
		{
			return;
		}
		float scourgeAbsoluteTargetX = Math.Abs(scourgeTargetX);
		float scourgeAbsoluteTargetY = Math.Abs(scourgeTargetY);
		float scourgeTimeToReachTarget = scourgeMaxSpeed / scourgeTargetDist;
		scourgeTargetX *= scourgeTimeToReachTarget;
		scourgeTargetY *= scourgeTimeToReachTarget;
		if ((base.NPC.velocity.X > 0f && scourgeTargetX > 0f) || (base.NPC.velocity.X < 0f && scourgeTargetX < 0f) || (base.NPC.velocity.Y > 0f && scourgeTargetY > 0f) || (base.NPC.velocity.Y < 0f && scourgeTargetY < 0f))
		{
			if (base.NPC.velocity.X < scourgeTargetX)
			{
				base.NPC.velocity.X += scourgeAcceleration;
			}
			else if (base.NPC.velocity.X > scourgeTargetX)
			{
				base.NPC.velocity.X -= scourgeAcceleration;
			}
			if (base.NPC.velocity.Y < scourgeTargetY)
			{
				base.NPC.velocity.Y += scourgeAcceleration;
			}
			else if (base.NPC.velocity.Y > scourgeTargetY)
			{
				base.NPC.velocity.Y -= scourgeAcceleration;
			}
			if ((double)Math.Abs(scourgeTargetY) < (double)scourgeMaxSpeed * 0.2 && ((base.NPC.velocity.X > 0f && scourgeTargetX < 0f) || (base.NPC.velocity.X < 0f && scourgeTargetX > 0f)))
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y += scourgeAcceleration * 2f;
				}
				else
				{
					base.NPC.velocity.Y -= scourgeAcceleration * 2f;
				}
			}
			if ((double)Math.Abs(scourgeTargetX) < (double)scourgeMaxSpeed * 0.2 && ((base.NPC.velocity.Y > 0f && scourgeTargetY < 0f) || (base.NPC.velocity.Y < 0f && scourgeTargetY > 0f)))
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X += scourgeAcceleration * 2f;
				}
				else
				{
					base.NPC.velocity.X -= scourgeAcceleration * 2f;
				}
			}
		}
		else if (scourgeAbsoluteTargetX > scourgeAbsoluteTargetY)
		{
			if (base.NPC.velocity.X < scourgeTargetX)
			{
				base.NPC.velocity.X += scourgeAcceleration * 1.1f;
			}
			else if (base.NPC.velocity.X > scourgeTargetX)
			{
				base.NPC.velocity.X -= scourgeAcceleration * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)scourgeMaxSpeed * 0.5)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y += scourgeAcceleration;
				}
				else
				{
					base.NPC.velocity.Y -= scourgeAcceleration;
				}
			}
		}
		else
		{
			if (base.NPC.velocity.Y < scourgeTargetY)
			{
				base.NPC.velocity.Y += scourgeAcceleration * 1.1f;
			}
			else if (base.NPC.velocity.Y > scourgeTargetY)
			{
				base.NPC.velocity.Y -= scourgeAcceleration * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)scourgeMaxSpeed * 0.5)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X += scourgeAcceleration;
				}
				else
				{
					base.NPC.velocity.X -= scourgeAcceleration;
				}
			}
		}
		base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI / 2f;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return CalamityUtils.DrawAnimatedBestiaryWorm(spriteBatch, base.NPC, drawColor, TextureAssets.Npc[base.Type].Value, TextureAssets.Npc[ModContent.NPCType<AquaticScourgeBody>()].Value, TextureAssets.Npc[ModContent.NPCType<AquaticScourgeBodyAlt>()].Value, 10, 12, 0.6f, new Vector2(20f, 30f), 3, 10f);
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 scaledDraw = default(Vector2);
		((Vector2)(ref scaledDraw))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += scaledDraw * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Color color = base.NPC.GetAlpha(drawColor);
		if (CalamityWorld.revenge || BossRushEvent.BossRushActive || Main.zenithWorld)
		{
			if (base.NPC.Calamity().newAI[3] > 300f)
			{
				color = Color.Lerp(color, Color.SandyBrown, MathHelper.Clamp((base.NPC.Calamity().newAI[3] - 300f) / 180f, 0f, 1f));
			}
			else if (base.NPC.localAI[3] > 0f)
			{
				color = Color.Lerp(color, Color.SandyBrown, MathHelper.Clamp(base.NPC.localAI[3] / 90f, 0f, 1f));
			}
		}
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, scaledDraw, base.NPC.scale, spriteEffects, 0f);
		return false;
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
		float topRightHitbox = Vector2.Distance(base.NPC.Center, targetHitbox.TopRight());
		float bottomLeftHitbox = Vector2.Distance(base.NPC.Center, targetHitbox.BottomLeft());
		float bottomRightHitbox = Vector2.Distance(base.NPC.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (topRightHitbox < minDist)
		{
			minDist = topRightHitbox;
		}
		if (bottomLeftHitbox < minDist)
		{
			minDist = bottomLeftHitbox;
		}
		if (bottomRightHitbox < minDist)
		{
			minDist = bottomRightHitbox;
		}
		return minDist <= 50f * base.NPC.scale;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return base.NPC.Calamity().newAI[0] == 1f;
		}
		return null;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().disableNaturalScourgeSpawns)
		{
			return 0f;
		}
		if (spawnInfo.PlayerSafe)
		{
			return 0f;
		}
		if (spawnInfo.Player.Calamity().ZoneSulphur && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<AquaticScourgeHead>()))
		{
			if (!Main.zenithWorld)
			{
				return 0.01f;
			}
			return 0.1f;
		}
		return 0f;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<SulphurousSand>();
	}

	public override bool SpecialOnKill()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		int closestSegmentID = DropHelper.FindClosestWormSegment(base.NPC, ModContent.NPCType<AquaticScourgeHead>(), ModContent.NPCType<AquaticScourgeBody>(), ModContent.NPCType<AquaticScourgeBodyAlt>(), ModContent.NPCType<AquaticScourgeTail>());
		base.NPC.position = Main.npc[closestSegmentID].position;
		return false;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<AquaticScourgeBag>()));
		LeadingConditionRule normalOnly = npcLoot.DefineNormalOnlyDropSet();
		int[] weapons = new int[5]
		{
			ModContent.ItemType<SubmarineShocker>(),
			ModContent.ItemType<Barinautical>(),
			ModContent.ItemType<Downpour>(),
			ModContent.ItemType<DeepseaStaff>(),
			ModContent.ItemType<ScourgeoftheSeas>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(ModContent.ItemType<AquaticScourgeMask>(), 7);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		normalOnly.Add(ModContent.ItemType<CorrosiveSpine>(), DropHelper.NormalWeaponDropRateFraction);
		normalOnly.Add(ModContent.ItemType<SeasSearing>(), 10);
		npcLoot.DefineConditionalDropSet(() => true).Add(DropHelper.PerPlayer(499, 1, 5, 15), hideLootReport: true);
		npcLoot.Add(ModContent.ItemType<AquaticScourgeTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<AquaticScourgeRelic>());
		npcLoot.DefineConditionalDropSet(DropHelper.GFB).Add(DropHelper.PerPlayer(ModContent.ItemType<SupremeBaitTackleBoxFishingStation>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(firstASKill, ModContent.ItemType<LoreAquaticScourge>(), ui: true, DropHelper.FirstKillText);
		npcLoot.AddConditionalPerPlayer(firstASKill, ModContent.ItemType<LoreSulphurSea>(), ui: true, DropHelper.FirstKillText);
		static bool firstASKill()
		{
			return !DownedBossSystem.downedAquaticScourge;
		}
	}

	public override void OnKill()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			return;
		}
		CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
		if (!DownedBossSystem.downedAquaticScourge)
		{
			if (!Main.LocalPlayer.dead && Main.LocalPlayer.active)
			{
				SoundEngine.PlaySound(in Mauler.RoarSound, Main.LocalPlayer.Center);
			}
			Color sulfSeaBoostColor = AcidRainEvent.TextColor;
			CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Progression.WetWormBossText", sulfSeaBoostColor);
			AcidRainEvent.CountdownUntilForcedAcidRain = 601;
		}
		DownedBossSystem.downedAquaticScourge = true;
		CalamityNetcode.SyncWorld();
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ASHead").Type, base.NPC.scale);
			}
		}
	}

	public override bool CheckActive()
	{
		if (base.NPC.Calamity().newAI[0] == 1f && !Main.player[base.NPC.target].dead && base.NPC.Calamity().newAI[1] != 1f)
		{
			return false;
		}
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 480);
		}
	}
}
