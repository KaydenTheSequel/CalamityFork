using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.TownNPCs;
using CalamityMod.Projectiles.Enemy;
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
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.SunkenSea;

[AutoloadBossHead]
public class GiantClam : ModNPC
{
	public static readonly SoundStyle SlamSound = new SoundStyle("CalamityMod/Sounds/Item/ClamImpact");

	private int hitAmount;

	private int attack = -1;

	private bool attackAnim;

	private bool hasBeenHit;

	private bool hide;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 12;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.4f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 40f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.lavaImmune = true;
		base.NPC.npcSlots = 5f;
		base.NPC.damage = (Main.hardMode ? 100 : 50);
		base.NPC.defense = (Main.hardMode ? 35 : 10);
		base.NPC.width = 160;
		base.NPC.height = 120;
		base.NPC.defense = 9999;
		base.NPC.lifeMax = (Main.hardMode ? 7500 : 1250);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = (Main.hardMode ? Item.buyPrice(0, 5) : Item.buyPrice(0, 1));
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.knockBackResist = 0f;
		base.NPC.rarity = 2;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<ClamDenBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.GiantClam")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hitAmount);
		writer.Write(attack);
		writer.Write(attackAnim);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.chaseable);
		writer.Write(hasBeenHit);
		writer.Write(hide);
		for (int i = 0; i < 2; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hitAmount = reader.ReadInt32();
		attack = reader.ReadInt32();
		attackAnim = reader.ReadBoolean();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
		hasBeenHit = reader.ReadBoolean();
		hide = reader.ReadBoolean();
		for (int i = 0; i < 2; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_1024: Unknown result type (might be due to invalid IL or missing references)
		//IL_102f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1173: Unknown result type (might be due to invalid IL or missing references)
		//IL_117e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_1073: Unknown result type (might be due to invalid IL or missing references)
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_109e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_110d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1112: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0982: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0991: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest();
		Player player = Main.player[base.NPC.target];
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		if (base.NPC.justHit && hitAmount < 5)
		{
			hitAmount++;
			hasBeenHit = true;
		}
		if (!hasBeenHit)
		{
			base.NPC.damage = 0;
		}
		base.NPC.chaseable = hasBeenHit;
		if (hitAmount != 5)
		{
			return;
		}
		if (!Main.dedServ && !Main.player[base.NPC.target].dead && Main.player[base.NPC.target].active)
		{
			player.AddBuff(ModContent.BuffType<Clamity>(), 2);
		}
		if (!hide)
		{
			Lighting.AddLight(base.NPC.Center, 0f, (float)(255 - base.NPC.alpha) * 2.5f / 255f, (float)(255 - base.NPC.alpha) * 2.5f / 255f);
		}
		if (base.NPC.ai[0] < 240f)
		{
			base.NPC.damage = 0;
			base.NPC.defense = (Main.hardMode ? 35 : 10);
			base.NPC.ai[0]++;
			hide = false;
		}
		else if (attack == -1)
		{
			base.NPC.damage = 0;
			attack = Main.rand.Next(2);
			if (attack == 0)
			{
				attack = Main.rand.Next(2);
			}
		}
		else if (attack == 0)
		{
			base.NPC.damage = 0;
			hide = true;
			base.NPC.defense = 9999;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 90f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				hide = false;
				attack = -1;
				base.NPC.defense = (Main.hardMode ? 35 : 10);
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X + 5f), (int)base.NPC.Center.Y, ModContent.NPCType<Clam>());
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<Clam>());
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)(base.NPC.Center.X - 5f), (int)base.NPC.Center.Y, ModContent.NPCType<Clam>());
			}
		}
		else if (attack == 1)
		{
			if (base.NPC.ai[2] == 0f)
			{
				if (Main.netMode != 1)
				{
					base.NPC.TargetClosest();
					base.NPC.ai[2] = 1f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 1f)
			{
				base.NPC.damage = 0;
				base.NPC.chaseable = false;
				base.NPC.dontTakeDamage = true;
				base.NPC.noGravity = true;
				base.NPC.noTileCollide = true;
				base.NPC.alpha += (Main.hardMode ? 8 : 5);
				if (base.NPC.alpha >= 255)
				{
					base.NPC.alpha = 255;
					base.NPC.position.X = player.Center.X - (float)(base.NPC.width / 2);
					base.NPC.position.Y = player.Center.Y - (float)(base.NPC.height / 2) + player.gfxOffY - 200f;
					base.NPC.position.X = base.NPC.position.X - 15f;
					base.NPC.position.Y = base.NPC.position.Y - 100f;
					base.NPC.ai[2] = 2f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 2f)
			{
				if (Main.rand.NextBool())
				{
					int attackDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 226, 0f, 0f, 200, default(Color), 1.5f);
					Main.dust[attackDust].noGravity = true;
					Dust obj = Main.dust[attackDust];
					obj.velocity *= 0.75f;
					Main.dust[attackDust].fadeIn = 1.3f;
					Vector2 vector = default(Vector2);
					((Vector2)(ref vector))._002Ector((float)Main.rand.Next(-200, 201), (float)Main.rand.Next(-200, 201));
					((Vector2)(ref vector)).Normalize();
					vector *= (float)Main.rand.Next(100, 200) * 0.04f;
					Main.dust[attackDust].velocity = vector;
					((Vector2)(ref vector)).Normalize();
					vector *= 34f;
					Main.dust[attackDust].position = base.NPC.Center - vector;
				}
				base.NPC.alpha -= (Main.hardMode ? 7 : 4);
				if (base.NPC.alpha <= 0)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.chaseable = true;
					base.NPC.dontTakeDamage = false;
					base.NPC.alpha = 0;
					base.NPC.ai[2] = 3f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 3f)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.velocity.Y += 0.8f;
				attackAnim = true;
				if (base.NPC.Center.Y > player.Center.Y - (float)(base.NPC.height / 2) + player.gfxOffY - 15f)
				{
					base.NPC.noTileCollide = false;
					base.NPC.ai[2] = 4f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[2] == 4f)
			{
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.damage = 0;
					base.NPC.ai[2] = 0f;
					base.NPC.ai[0] = 0f;
					base.NPC.netUpdate = true;
					base.NPC.noGravity = false;
					attack = -1;
					SoundEngine.PlaySound(in SlamSound, base.NPC.Center);
					if (!Main.dedServ)
					{
						for (int stompDustArea = (int)base.NPC.position.X - 30; stompDustArea < (int)base.NPC.position.X + base.NPC.width + 60; stompDustArea += 30)
						{
							for (int stompDustAmount = 0; stompDustAmount < 5; stompDustAmount++)
							{
								int stompDust = Dust.NewDust(new Vector2(base.NPC.position.X - 30f, base.NPC.position.Y + (float)base.NPC.height), base.NPC.width + 30, 4, 33, 0f, 0f, 100, default(Color), 1.5f);
								Dust obj2 = Main.dust[stompDust];
								obj2.velocity *= 0.2f;
							}
							int stompGore = Gore.NewGore(base.NPC.GetSource_FromAI(), new Vector2((float)(stompDustArea - 30), base.NPC.position.Y + (float)base.NPC.height - 12f), default(Vector2), Main.rand.Next(61, 64));
							Gore obj3 = Main.gore[stompGore];
							obj3.velocity *= 0.4f;
						}
					}
				}
				base.NPC.velocity.Y += 0.8f;
			}
		}
		if (Main.zenithWorld)
		{
			calamityGlobalNPC.newAI[0]++;
			if (Main.netMode != 1)
			{
				calamityGlobalNPC.newAI[0]++;
				if (Main.hardMode)
				{
					int type = ModContent.ProjectileType<PearlBurst>();
					int damage = (Main.expertMode ? 28 : 35);
					float speedPearlFrequency = 180f;
					float projSpeed = 3f;
					if (calamityGlobalNPC.newAI[0] <= 300f)
					{
						if (calamityGlobalNPC.newAI[0] % speedPearlFrequency == 0f)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed, type, damage, 0f, Main.myPlayer, 1f);
						}
					}
					else if (calamityGlobalNPC.newAI[0] <= 600f && calamityGlobalNPC.newAI[0] > 300f)
					{
						if (calamityGlobalNPC.newAI[0] % speedPearlFrequency == 0f)
						{
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 0f - projSpeed, 0f, type, damage, 0f, Main.myPlayer, 1f);
						}
					}
					else if (calamityGlobalNPC.newAI[0] > 600f && calamityGlobalNPC.newAI[0] % speedPearlFrequency == 0f)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed, type, damage, 0f, Main.myPlayer, 1f);
					}
				}
				calamityGlobalNPC.newAI[1]++;
				float pearlGateValue = 60f;
				if (calamityGlobalNPC.newAI[1] >= pearlGateValue)
				{
					calamityGlobalNPC.newAI[1] = 0f;
					int type2 = ModContent.ProjectileType<PearlRain>();
					int damage2 = (Main.expertMode ? 28 : 35);
					float projSpeed2 = 4f;
					if (calamityGlobalNPC.newAI[0] % (pearlGateValue * 6f) == 0f)
					{
						float distance = (Main.rand.NextBool() ? (-1000f) : 1000f);
						float velocity = ((distance == -1000f) ? projSpeed2 : (0f - projSpeed2));
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + distance, player.position.Y, velocity, 0f, type2, damage2, 0f, Main.myPlayer, 1f);
					}
					if (calamityGlobalNPC.newAI[0] < 300f)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed2, type2, damage2, 0f, Main.myPlayer, 1f);
					}
					else if (calamityGlobalNPC.newAI[0] < 600f)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 0f - (projSpeed2 - 0.5f), 0f, type2, damage2, 0f, Main.myPlayer, 1f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), projSpeed2 - 0.5f, 0f, type2, damage2, 0f, Main.myPlayer, 1f);
					}
					else
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + (float)Main.rand.Next(-1000, 1001), player.position.Y - 1000f, 0f, projSpeed2 - 1f, type2, damage2, 0f, Main.myPlayer, 1f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X + 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), 0f - (projSpeed2 - 1f), 0f, type2, damage2, 0f, Main.myPlayer, 1f);
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.position.X - 1000f, player.position.Y + (float)Main.rand.Next(-1000, 1001), projSpeed2 - 1f, 0f, type2, damage2, 0f, Main.myPlayer, 1f);
					}
				}
			}
		}
		if (base.NPC.ai[3] < 180f && Main.hardMode)
		{
			base.NPC.ai[3]++;
		}
		else
		{
			if (!Main.hardMode)
			{
				return;
			}
			if (attack == -1)
			{
				attack = Main.rand.Next(2, 4);
			}
			else if (attack == 2)
			{
				SoundEngine.PlaySound(in SoundID.Item67, base.NPC.Center);
				if (Main.netMode != 1)
				{
					int projectileShot = ModContent.ProjectileType<PearlBurst>();
					int damage3 = (Main.masterMode ? 23 : (Main.expertMode ? 28 : 35));
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * 5f, projectileShot, damage3, 0f, Main.myPlayer);
					for (int i = 0; i < 8; i++)
					{
						Vector2 velocity2 = ((float)Math.PI * 2f * (float)i / 8f - (float)Math.PI / 8f).ToRotationVector2() * 3f;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity2, projectileShot, damage3, 0f, Main.myPlayer);
					}
				}
				attack = -1;
				base.NPC.ai[3] = 0f;
			}
			else
			{
				if (attack != 3)
				{
					return;
				}
				SoundEngine.PlaySound(in SoundID.Item68, base.NPC.Center);
				if (Main.netMode != 1)
				{
					int damage4 = (Main.masterMode ? 23 : (Main.expertMode ? 28 : 35));
					float shotSpacing = 750f;
					for (int j = 0; j < 11; j++)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center.X + shotSpacing, player.Center.Y - 750f, 0f, 8f, ModContent.ProjectileType<PearlRain>(), damage4, 0f, Main.myPlayer);
						shotSpacing -= 150f;
					}
				}
				attack = -1;
				base.NPC.ai[3] = 0f;
			}
		}
	}

	public override bool CheckActive()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 5600f;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return hasBeenHit;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > (attackAnim ? 2.0 : 5.0))
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
		}
		if ((hitAmount < 5 || hide) && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frame.Y = frameHeight * 11;
		}
		else if (attackAnim)
		{
			if (base.NPC.frame.Y < frameHeight * 3)
			{
				base.NPC.frame.Y = frameHeight * 3;
			}
			if (base.NPC.frame.Y > frameHeight * 10)
			{
				hide = true;
				attackAnim = false;
			}
		}
		else if (base.NPC.frame.Y > frameHeight * 3)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.Water && DownedBossSystem.downedDesertScourge && !NPC.AnyNPCs(ModContent.NPCType<GiantClam>()))
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.24f;
		}
		return 0f;
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (Main.zenithWorld)
		{
			if (Main.hardMode)
			{
				typeName = CalamityUtils.GetTextValue("NPCs.SupremeClamitas");
			}
			else
			{
				typeName = CalamityUtils.GetTextValue("NPCs.Clamitas");
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 37, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 37, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantClam1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantClam2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantClam3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantClam4").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantClam5").Type);
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		Main.EntitySpriteDraw(TextureAssets.Npc[base.Type].Value, base.NPC.Center - screenPos, base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0);
		return false;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Texture2D glowmask = GlowTexture.Value;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		Vector2 val = new Vector2(base.NPC.Center.X, base.NPC.Center.Y);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(texture.Width / 2), (float)(texture.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 vector = val - screenPos;
		vector -= new Vector2((float)glowmask.Width, (float)(glowmask.Height / Main.npcFrameCount[base.Type])) * 1f / 2f;
		vector += halfSizeTexture * 1f + new Vector2(0f, 4f + base.NPC.gfxOffY);
		Color color = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), Color.LightBlue);
		Main.EntitySpriteDraw(glowmask, vector, base.NPC.frame, color, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects);
	}

	public override void OnKill()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (NPC.FindFirstNPC(ModContent.NPCType<SeaKing>()) == -1 && Main.netMode != 1)
		{
			NPC.NewNPC(base.NPC.GetSource_Death(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<SeaKing>());
		}
		DownedBossSystem.downedCLAM = true;
		DownedBossSystem.downedCLAMHardMode = Main.hardMode || DownedBossSystem.downedCLAMHardMode;
		CalamityNetcode.SyncWorld();
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule hardmode = npcLoot.DefineConditionalDropSet(DropHelper.Hardmode());
		npcLoot.Add(ModContent.ItemType<Navystone>(), 1, 30, 40);
		hardmode.Add(ModContent.ItemType<MolluskHusk>(), 1, 25, 30);
		int[] weapons = new int[4]
		{
			ModContent.ItemType<ClamCrusher>(),
			ModContent.ItemType<ClamorRifle>(),
			ModContent.ItemType<Poseidon>(),
			ModContent.ItemType<ShellfishStaff>()
		};
		hardmode.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		npcLoot.Add(4412, 2);
		npcLoot.Add(4413, 4);
		npcLoot.Add(4414, 10);
		npcLoot.Add(ModContent.ItemType<GiantPearl>(), 3);
		npcLoot.Add(ModContent.ItemType<AmidiasPendant>(), 3);
		npcLoot.Add(ModContent.ItemType<GiantClamTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<GiantClamRelic>());
	}
}
