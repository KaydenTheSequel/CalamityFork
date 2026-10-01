using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Placeables.Furniture.Trophies;
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
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Leviathan;

[AutoloadBossHead]
public class Anahita : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/AnahitaHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/AnahitaDeath");

	private int biomeEnrageTimer = 300;

	private bool spawnedLevi;

	private bool forceChargeFrames;

	private int frameUsed;

	public bool HasBegunSummoningLeviathan;

	private int DrawProjectileTelegraphTimer;

	private int GetGoodAttackChosen;

	public static Asset<Texture2D> ChargeTexture;

	public static float DashDamageMult = 1.5f;

	public static int SpearDamage = 27;

	public static int MistDamage = 24;

	public static int SongDamage = 30;

	public bool WaitingForLeviathan
	{
		get
		{
			if (Main.npc.IndexInRange(CalamityGlobalNPC.leviathan) && (float)Main.npc[CalamityGlobalNPC.leviathan].life / (float)Main.npc[CalamityGlobalNPC.leviathan].lifeMax >= ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 0.7f : 0.4f))
			{
				return true;
			}
			return CalamityUtils.FindFirstProjectile(ModContent.ProjectileType<LeviathanSpawner>()) != -1;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.5f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			ChargeTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Leviathan/AnahitaStabbing", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 50;
		base.NPC.npcSlots = 16f;
		base.NPC.width = 100;
		base.NPC.height = 100;
		base.NPC.defense = 20;
		base.NPC.LifeMaxNERB(25000, 42000, 260000);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<LeviathanAnahitaBossBar>();
		base.NPC.value = Item.buyPrice(0, 7, 50);
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.8f;
		}
		if (Main.zenithWorld)
		{
			base.NPC.scale *= 4f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Anahita")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(biomeEnrageTimer);
		writer.Write(spawnedLevi);
		writer.Write(forceChargeFrames);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		writer.Write(frameUsed);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.Calamity().newAI[0]);
		writer.Write(HasBegunSummoningLeviathan);
		writer.Write(DrawProjectileTelegraphTimer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		biomeEnrageTimer = reader.ReadInt32();
		spawnedLevi = reader.ReadBoolean();
		forceChargeFrames = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		frameUsed = reader.ReadInt32();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.Calamity().newAI[0] = reader.ReadSingle();
		HasBegunSummoningLeviathan = reader.ReadBoolean();
		DrawProjectileTelegraphTimer = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a78: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0904: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ded: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e33: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1908: Unknown result type (might be due to invalid IL or missing references)
		//IL_190d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1912: Unknown result type (might be due to invalid IL or missing references)
		//IL_13db: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1802: Unknown result type (might be due to invalid IL or missing references)
		//IL_180c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1811: Unknown result type (might be due to invalid IL or missing references)
		//IL_1403: Unknown result type (might be due to invalid IL or missing references)
		//IL_140b: Unknown result type (might be due to invalid IL or missing references)
		//IL_145a: Unknown result type (might be due to invalid IL or missing references)
		//IL_145f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1461: Unknown result type (might be due to invalid IL or missing references)
		//IL_1466: Unknown result type (might be due to invalid IL or missing references)
		//IL_1486: Unknown result type (might be due to invalid IL or missing references)
		//IL_148d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1492: Unknown result type (might be due to invalid IL or missing references)
		//IL_238f: Unknown result type (might be due to invalid IL or missing references)
		//IL_239a: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_251d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2522: Unknown result type (might be due to invalid IL or missing references)
		//IL_252c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2548: Unknown result type (might be due to invalid IL or missing references)
		//IL_254e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2550: Unknown result type (might be due to invalid IL or missing references)
		//IL_255b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2560: Unknown result type (might be due to invalid IL or missing references)
		//IL_2580: Unknown result type (might be due to invalid IL or missing references)
		//IL_2592: Unknown result type (might be due to invalid IL or missing references)
		//IL_2597: Unknown result type (might be due to invalid IL or missing references)
		//IL_2599: Unknown result type (might be due to invalid IL or missing references)
		//IL_259b: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_25cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_25fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_2608: Unknown result type (might be due to invalid IL or missing references)
		//IL_260d: Unknown result type (might be due to invalid IL or missing references)
		//IL_261b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2626: Unknown result type (might be due to invalid IL or missing references)
		//IL_262b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2630: Unknown result type (might be due to invalid IL or missing references)
		//IL_241b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2423: Unknown result type (might be due to invalid IL or missing references)
		//IL_2472: Unknown result type (might be due to invalid IL or missing references)
		//IL_2477: Unknown result type (might be due to invalid IL or missing references)
		//IL_2479: Unknown result type (might be due to invalid IL or missing references)
		//IL_247e: Unknown result type (might be due to invalid IL or missing references)
		//IL_249e: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1943: Unknown result type (might be due to invalid IL or missing references)
		//IL_194b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1950: Unknown result type (might be due to invalid IL or missing references)
		//IL_195b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1960: Unknown result type (might be due to invalid IL or missing references)
		//IL_1965: Unknown result type (might be due to invalid IL or missing references)
		//IL_196c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1971: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_201b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2026: Unknown result type (might be due to invalid IL or missing references)
		//IL_2043: Unknown result type (might be due to invalid IL or missing references)
		//IL_204e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2255: Unknown result type (might be due to invalid IL or missing references)
		//IL_2260: Unknown result type (might be due to invalid IL or missing references)
		//IL_2265: Unknown result type (might be due to invalid IL or missing references)
		//IL_226a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2271: Unknown result type (might be due to invalid IL or missing references)
		//IL_2276: Unknown result type (might be due to invalid IL or missing references)
		//IL_22af: Unknown result type (might be due to invalid IL or missing references)
		//IL_22bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b36: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ec9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c18: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c20: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c41: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d49: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d65: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d70: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dda: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ddc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dee: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e09: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fac: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe1: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.siren = base.NPC.whoAmI;
		if (HasBegunSummoningLeviathan && CalamityGlobalNPC.LeviAndAna == -1)
		{
			CalamityGlobalNPC.LeviAndAna = base.NPC.whoAmI;
		}
		Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0f, 0.5f, 0.3f);
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		bool leviAlive = false;
		if (CalamityGlobalNPC.leviathan != -1)
		{
			leviAlive = Main.npc[CalamityGlobalNPC.leviathan].active;
		}
		Player player = Main.player[base.NPC.target];
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		if ((player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400))) && !BossRushEvent.BossRushActive)
		{
			if (biomeEnrageTimer > 0)
			{
				biomeEnrageTimer--;
			}
		}
		else
		{
			biomeEnrageTimer = 300;
		}
		bool num = biomeEnrageTimer <= 0;
		float enrageScale = 0f;
		if (num)
		{
			base.NPC.Calamity().CurrentlyEnraged = true;
			enrageScale += 1.5f;
		}
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		float bubbleVelocity = (death ? 9f : (revenge ? 7f : (expertMode ? 6f : 5f)));
		bubbleVelocity += 4f * enrageScale;
		if (!leviAlive)
		{
			bubbleVelocity += 2f * (1f - lifeRatio);
		}
		if (Main.getGoodWorld)
		{
			bubbleVelocity *= 2f;
		}
		bool phase2 = lifeRatio < 0.7f;
		bool phase3 = lifeRatio < 0.4f;
		bool phase4 = lifeRatio < (death ? 0.4f : 0.2f);
		if ((phase3 | death) && !HasBegunSummoningLeviathan && !Main.zenithWorld)
		{
			base.NPC.damage = 0;
			DrawProjectileTelegraphTimer = 0;
			base.NPC.ai[0] = 3f;
			base.NPC.direction = (base.NPC.Center.X < (float)Main.maxTilesX * 8f).ToDirectionInt();
			base.NPC.position.X = MathHelper.Clamp(base.NPC.position.X, 150f, (float)Main.maxTilesX * 16f - 150f);
			if (base.NPC.alpha <= 0)
			{
				float moveDirection = 1f;
				if (Math.Abs(base.NPC.Center.X - (float)Main.maxTilesX * 16f) > Math.Abs(base.NPC.Center.X))
				{
					moveDirection = -1f;
				}
				base.NPC.velocity.X = moveDirection * 6f;
				base.NPC.spriteDirection = (int)(0f - moveDirection);
				base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y + 0.2f, -3f, 16f);
			}
			float idealRotation = base.NPC.velocity.ToRotation();
			if (base.NPC.spriteDirection == 1)
			{
				idealRotation += (float)Math.PI;
			}
			base.NPC.rotation = base.NPC.rotation.AngleTowards(idealRotation, 0.08f);
			if (BossRushEvent.BossRushActive || Collision.WetCollision(base.NPC.position, base.NPC.width, base.NPC.height) || (double)base.NPC.position.Y > (Main.worldSurface - 125.0) * 16.0)
			{
				int oldAlpha = base.NPC.alpha;
				base.NPC.alpha = Utils.Clamp(base.NPC.alpha + 9, 0, 255);
				if (base.NPC.alpha >= 255 && oldAlpha < 255)
				{
					if (Main.netMode != 1)
					{
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, Vector2.Zero, ModContent.ProjectileType<LeviathanSpawner>(), 0, 0f);
					}
					HasBegunSummoningLeviathan = true;
					base.NPC.netUpdate = true;
				}
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
			}
			else
			{
				base.NPC.alpha -= 5;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
			}
			base.NPC.dontTakeDamage = true;
			return;
		}
		if (base.NPC.localAI[2] < 3f)
		{
			if (base.NPC.ai[3] == 0f && base.NPC.localAI[1] == 0f && Main.netMode != 1)
			{
				int iceShieldSpawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<AnahitasIceShield>(), base.NPC.whoAmI);
				base.NPC.ai[3] = iceShieldSpawn + 1;
				base.NPC.localAI[1] = -1f;
				base.NPC.localAI[2]++;
				base.NPC.netUpdate = true;
				Main.npc[iceShieldSpawn].ai[0] = base.NPC.whoAmI;
				Main.npc[iceShieldSpawn].netUpdate = true;
			}
			int iceShieldCheck = (int)base.NPC.ai[3] - 1;
			if (iceShieldCheck != -1 && Main.npc[iceShieldCheck].active && Main.npc[iceShieldCheck].type == ModContent.NPCType<AnahitasIceShield>())
			{
				base.NPC.dontTakeDamage = true;
			}
			else
			{
				base.NPC.dontTakeDamage = false;
				base.NPC.ai[3] = 0f;
				if (base.NPC.localAI[1] == -1f)
				{
					base.NPC.localAI[1] = 1f;
				}
				else
				{
					switch ((int)base.NPC.localAI[2])
					{
					case 1:
						if (phase2)
						{
							base.NPC.localAI[1] = 0f;
						}
						break;
					case 2:
						if (phase3)
						{
							base.NPC.localAI[1] = 0f;
						}
						break;
					}
				}
			}
		}
		else
		{
			base.NPC.dontTakeDamage = false;
			int iceShieldCheck2 = (int)base.NPC.ai[3] - 1;
			if (iceShieldCheck2 != -1 && Main.npc[iceShieldCheck2].active && Main.npc[iceShieldCheck2].type == ModContent.NPCType<AnahitasIceShield>())
			{
				base.NPC.dontTakeDamage = true;
			}
		}
		base.NPC.canDisplayBuffs = true;
		if ((phase3 | death) && WaitingForLeviathan && !Main.zenithWorld)
		{
			base.NPC.damage = 0;
			ChargeRotation(player);
			ChargeLocation(player, leviAlive: false, revenge: true);
			if (base.NPC.alpha < 255)
			{
				base.NPC.alpha += 3;
			}
			if (base.NPC.alpha > 255)
			{
				base.NPC.alpha = 255;
			}
			else if (base.NPC.alpha < 255)
			{
				for (int k = 0; k < 3; k++)
				{
					int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 172, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
				}
			}
			base.NPC.dontTakeDamage = true;
			base.NPC.canDisplayBuffs = false;
			if (base.NPC.ai[0] != -1f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.localAI[0] = 0f;
				base.NPC.netUpdate = true;
			}
			return;
		}
		base.NPC.alpha -= 5;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		float extrapitch = (Main.zenithWorld ? (-0.5f) : 0f);
		if (Main.rand.NextBool(300))
		{
			SoundStyle style = SoundID.Zombie35 with
			{
				Pitch = SoundID.Zombie35.Pitch + extrapitch
			};
			SoundEngine.PlaySound(in style, base.NPC.Center);
		}
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f)
			{
				base.NPC.rotation = base.NPC.velocity.X * 0.02f;
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
					for (int x = 0; x < Main.maxNPCs; x++)
					{
						if (Main.npc[x].type == ModContent.NPCType<Leviathan>())
						{
							Main.npc[x].active = false;
							Main.npc[x].netUpdate = true;
						}
					}
					base.NPC.active = false;
					base.NPC.netUpdate = true;
				}
				if (base.NPC.ai[0] != -1f)
				{
					base.NPC.ai[0] = -1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.localAI[0] = 0f;
					base.NPC.netUpdate = true;
				}
				return;
			}
		}
		if (base.NPC.ai[0] > 2f)
		{
			ChargeRotation(player);
		}
		if (base.NPC.ai[0] == -1f)
		{
			int random = ((((phase2 & expertMode) && !leviAlive) | phase4 | death) ? 4 : 3);
			int nextAttack;
			do
			{
				nextAttack = Main.rand.Next(random);
			}
			while ((float)nextAttack == base.NPC.ai[1] || nextAttack == 1);
			base.NPC.ai[0] = nextAttack;
			if (base.NPC.ai[0] != 3f)
			{
				forceChargeFrames = false;
				float playerLocation = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			else
			{
				ChargeRotation(player);
			}
			base.NPC.TargetClosest();
			base.NPC.ai[1] = 0f;
			base.NPC.ai[2] = 0f;
		}
		else if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.rotation = base.NPC.velocity.X * 0.02f;
			base.NPC.spriteDirection = base.NPC.direction;
			DrawProjectileTelegraphTimer = 0;
			Vector2 anahitaPos = base.NPC.Center;
			float num2 = player.position.X + (float)(player.width / 2) - anahitaPos.X;
			float playerYDist = player.position.Y + (float)(player.height / 2) - 200f * base.NPC.scale - anahitaPos.Y;
			float num3 = (float)Math.Sqrt(num2 * num2 + playerYDist * playerYDist);
			base.NPC.Calamity().newAI[0]++;
			if (num3 < 600f * base.NPC.scale || base.NPC.Calamity().newAI[0] >= 180f)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.Calamity().newAI[0] = 0f;
				base.NPC.netUpdate = true;
				return;
			}
			float maxVelocityY = (death ? 3f : 4f);
			float maxVelocityX = (death ? 7f : 8f);
			maxVelocityY -= enrageScale;
			maxVelocityX -= 2f * enrageScale;
			if (base.NPC.position.Y > player.position.Y - 350f * base.NPC.scale)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y *= 0.98f;
				}
				base.NPC.velocity.Y -= (death ? 0.12f : 0.1f);
				if (base.NPC.velocity.Y > maxVelocityY)
				{
					base.NPC.velocity.Y = maxVelocityY;
				}
			}
			else if (base.NPC.position.Y < player.position.Y - 450f * base.NPC.scale)
			{
				if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y *= 0.98f;
				}
				base.NPC.velocity.Y += (death ? 0.12f : 0.1f);
				if (base.NPC.velocity.Y < 0f - maxVelocityY)
				{
					base.NPC.velocity.Y = 0f - maxVelocityY;
				}
			}
			if (base.NPC.position.X + (float)(base.NPC.width / 2) > player.position.X + (float)(player.width / 2) + 100f * base.NPC.scale)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				base.NPC.velocity.X -= (death ? 0.12f : 0.1f);
				if (base.NPC.velocity.X > maxVelocityX)
				{
					base.NPC.velocity.X = maxVelocityX;
				}
			}
			if (base.NPC.position.X + (float)(base.NPC.width / 2) < player.position.X + (float)(player.width / 2) - 100f * base.NPC.scale)
			{
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.velocity.X *= 0.98f;
				}
				base.NPC.velocity.X += (death ? 0.12f : 0.1f);
				if (base.NPC.velocity.X < 0f - maxVelocityX)
				{
					base.NPC.velocity.X = 0f - maxVelocityX;
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			base.NPC.rotation = base.NPC.velocity.X * 0.02f;
			Vector2 bubbleSpawnPos = default(Vector2);
			((Vector2)(ref bubbleSpawnPos))._002Ector(base.NPC.position.X + (float)(base.NPC.width / 2) + (float)(15 * base.NPC.direction) * base.NPC.scale, base.NPC.position.Y + 30f * base.NPC.scale);
			Vector2 restingPos = base.NPC.Center;
			float num4 = player.position.X + (float)(player.width / 2) - restingPos.X;
			float restingPlayerYDist = player.position.Y + (float)(player.height / 2) - restingPos.Y;
			float num5 = (float)Math.Sqrt(num4 * num4 + restingPlayerYDist * restingPlayerYDist);
			base.NPC.ai[1]++;
			base.NPC.ai[1] += Main.CurrentFrameFlags.ActivePlayersCount / 2;
			bool spawnedBubble = false;
			float bubbleDelay = 20f;
			if (!leviAlive | phase4)
			{
				bubbleDelay -= (death ? (15f * (1f - lifeRatio)) : (12f * (1f - lifeRatio)));
			}
			if (base.NPC.ai[1] > bubbleDelay)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2]++;
				spawnedBubble = true;
			}
			if (Collision.CanHit(bubbleSpawnPos, 1, 1, player.position, player.width, player.height) & spawnedBubble)
			{
				SoundEngine.PlaySound(in SoundID.Item85, base.NPC.Center);
				if (Main.netMode != 1)
				{
					int spawn = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)bubbleSpawnPos.X, (int)bubbleSpawnPos.Y, 371);
					Main.npc[spawn].target = base.NPC.target;
					Main.npc[spawn].velocity = player.Center - bubbleSpawnPos;
					((Vector2)(ref Main.npc[spawn].velocity)).Normalize();
					NPC obj = Main.npc[spawn];
					obj.velocity *= bubbleVelocity;
					Main.npc[spawn].netUpdate = true;
					Main.npc[spawn].ai[3] = (float)Main.rand.Next(80, 121) / 100f;
				}
			}
			if (num5 > 600f * base.NPC.scale)
			{
				float maxVelocityY2 = (death ? 3f : 4f);
				float maxVelocityX2 = (death ? 7f : 8f);
				maxVelocityY2 -= enrageScale;
				maxVelocityX2 -= 2f * enrageScale;
				if (base.NPC.position.Y > player.position.Y - 350f * base.NPC.scale)
				{
					if (base.NPC.velocity.Y > 0f)
					{
						base.NPC.velocity.Y *= 0.98f;
					}
					base.NPC.velocity.Y -= (death ? 0.12f : 0.1f);
					if (base.NPC.velocity.Y > maxVelocityY2)
					{
						base.NPC.velocity.Y = maxVelocityY2;
					}
				}
				else if (base.NPC.position.Y < player.position.Y - 450f * base.NPC.scale)
				{
					if (base.NPC.velocity.Y < 0f)
					{
						base.NPC.velocity.Y *= 0.98f;
					}
					base.NPC.velocity.Y += (death ? 0.12f : 0.1f);
					if (base.NPC.velocity.Y < 0f - maxVelocityY2)
					{
						base.NPC.velocity.Y = 0f - maxVelocityY2;
					}
				}
				if (base.NPC.position.X + (float)(base.NPC.width / 2) > player.position.X + (float)(player.width / 2) + 100f * base.NPC.scale)
				{
					if (base.NPC.velocity.X > 0f)
					{
						base.NPC.velocity.X *= 0.98f;
					}
					base.NPC.velocity.X -= (death ? 0.12f : 0.1f);
					if (base.NPC.velocity.X > maxVelocityX2)
					{
						base.NPC.velocity.X = maxVelocityX2;
					}
				}
				if (base.NPC.position.X + (float)(base.NPC.width / 2) < player.position.X + (float)(player.width / 2) - 100f * base.NPC.scale)
				{
					if (base.NPC.velocity.X < 0f)
					{
						base.NPC.velocity.X *= 0.98f;
					}
					base.NPC.velocity.X += (death ? 0.12f : 0.1f);
					if (base.NPC.velocity.X < 0f - maxVelocityX2)
					{
						base.NPC.velocity.X = 0f - maxVelocityX2;
					}
				}
			}
			else
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.9f;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			float maxBubbleSpawn = (death ? 3f : 4f);
			if (base.NPC.ai[2] > maxBubbleSpawn)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			base.NPC.rotation = base.NPC.velocity.X * 0.02f;
			float basey = (Main.zenithWorld ? (-100) : (-350));
			Vector2 targetVector = player.Center + new Vector2(0f, basey) * base.NPC.scale;
			float velocity = (death ? 13.5f : 12f);
			velocity += 6f * enrageScale;
			if (Main.getGoodWorld)
			{
				velocity *= 1.15f;
			}
			Vector2 chargeSetupLocation = Vector2.Normalize(targetVector - base.NPC.Center - base.NPC.velocity) * velocity;
			float acceleration = (death ? 0.28f : 0.25f);
			acceleration += 0.2f * enrageScale;
			if (Main.getGoodWorld)
			{
				acceleration *= 1.15f;
			}
			if (Math.Abs(base.NPC.Center.Y - targetVector.Y) > 50f * base.NPC.scale || Math.Abs(base.NPC.Center.X - player.Center.X) > 350f * base.NPC.scale)
			{
				base.NPC.SimpleFlyMovement(chargeSetupLocation, acceleration);
			}
			base.NPC.ai[1]++;
			float attackDivisor = 140f - (float)(int)(30f * enrageScale);
			float telegraphOffset = (death ? 45f : 60f);
			if (!leviAlive | phase4)
			{
				attackDivisor -= (float)Math.Ceiling(50f * (1f - lifeRatio));
				telegraphOffset = (float)Math.Ceiling(telegraphOffset * 0.66f);
			}
			bool telegraphProjectiles = base.NPC.ai[1] % attackDivisor == attackDivisor - telegraphOffset;
			bool shootProjectiles = base.NPC.ai[1] % attackDivisor == 0f;
			if (Main.netMode != 1)
			{
				if (telegraphProjectiles)
				{
					DrawProjectileTelegraphTimer = (int)telegraphOffset;
					int totalTelegraphs = 8;
					float telegraphDist = 500f;
					if (Main.getGoodWorld)
					{
						GetGoodAttackChosen = Main.rand.Next(3);
						if (GetGoodAttackChosen == 2)
						{
							telegraphDist -= 50f;
						}
						for (int i = 0; i < totalTelegraphs; i++)
						{
							Vector2 spawnVector = player.Center - Vector2.UnitY.RotatedBy((float)Math.PI * 2f / (float)totalTelegraphs * (float)i) * telegraphDist;
							int tele = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector, Vector2.Zero, ModContent.ProjectileType<AnahitaTelegraph>(), 0, 0f, Main.myPlayer, player.whoAmI, GetGoodAttackChosen);
							Main.projectile[tele].netUpdate = true;
						}
					}
					else
					{
						switch ((int)base.NPC.localAI[3])
						{
						case 1:
							totalTelegraphs = 3;
							break;
						case 2:
							totalTelegraphs = 6;
							telegraphDist -= 50f;
							break;
						}
						if ((phase2 && !leviAlive) | phase4)
						{
							totalTelegraphs += totalTelegraphs / 2;
						}
						for (int j = 0; j < totalTelegraphs; j++)
						{
							Vector2 spawnVector2 = player.Center - Vector2.UnitY.RotatedBy((float)Math.PI * 2f / (float)totalTelegraphs * (float)j) * telegraphDist;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector2, Vector2.Zero, ModContent.ProjectileType<AnahitaTelegraph>(), 0, 0f, Main.myPlayer, player.whoAmI, base.NPC.localAI[3]);
						}
					}
				}
				else
				{
					DrawProjectileTelegraphTimer--;
				}
				if (shootProjectiles)
				{
					float projectileVelocity = (expertMode ? 3f : 2f);
					projectileVelocity += enrageScale;
					if (!leviAlive | phase4)
					{
						projectileVelocity += (death ? (3f * (1f - lifeRatio)) : (2f * (1f - lifeRatio)));
					}
					int totalProjectiles = 8;
					int projectileDistance = 600;
					int type = ModContent.ProjectileType<WaterSpear>();
					int damage = SpearDamage;
					if (Main.getGoodWorld)
					{
						float radians = (float)Math.PI * 2f / (float)totalProjectiles;
						for (int l = 0; l < totalProjectiles; l++)
						{
							switch (GetGoodAttackChosen)
							{
							case 0:
								SoundEngine.PlaySound(in SoundID.Item21, player.Center);
								break;
							case 1:
								type = ModContent.ProjectileType<FrostMist>();
								damage = MistDamage;
								SoundEngine.PlaySound(in SoundID.Item30, player.Center);
								break;
							case 2:
								type = ModContent.ProjectileType<SirenSong>();
								damage = SongDamage;
								Main.musicPitch = (Main.rand.NextFloat() - 0.5f) * 0.5f;
								SoundEngine.PlaySound(in SoundID.Item26, player.Center);
								break;
							}
							Vector2 spawnVector3 = player.Center + Vector2.Normalize(Utils.RotatedBy(new Vector2(0f, 0f - projectileVelocity), (double)(radians * (float)l), default(Vector2))) * (float)projectileDistance;
							Vector2 projVelocity = Vector2.Normalize(player.Center - spawnVector3) * projectileVelocity;
							int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector3, projVelocity, type, damage, 0f, Main.myPlayer);
							Main.projectile[proj].netUpdate = true;
						}
					}
					else
					{
						switch ((int)base.NPC.localAI[3])
						{
						case 0:
							SoundEngine.PlaySound(in SoundID.Item21, player.Center);
							break;
						case 1:
							totalProjectiles = 3;
							type = ModContent.ProjectileType<FrostMist>();
							damage = MistDamage;
							SoundEngine.PlaySound(in SoundID.Item30, player.Center);
							break;
						case 2:
						{
							totalProjectiles = 6;
							type = ModContent.ProjectileType<SirenSong>();
							damage = SongDamage;
							SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HarpEnd");
							style.Volume = 0.6f;
							SoundEngine.PlaySound(in style, player.Center);
							break;
						}
						}
						base.NPC.localAI[3]++;
						if (base.NPC.localAI[3] > 2f)
						{
							base.NPC.localAI[3] = 0f;
						}
						if ((phase2 && !leviAlive) | phase4)
						{
							totalProjectiles += totalProjectiles / 2;
						}
						float radians2 = (float)Math.PI * 2f / (float)totalProjectiles;
						for (int m = 0; m < totalProjectiles; m++)
						{
							Vector2 spawnVector4 = player.Center + Vector2.Normalize(Utils.RotatedBy(new Vector2(0f, 0f - projectileVelocity), (double)(radians2 * (float)m), default(Vector2))) * (float)projectileDistance;
							Vector2 projVelocity2 = Vector2.Normalize(player.Center - spawnVector4) * projectileVelocity;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector4, projVelocity2, type, damage, 0f, Main.myPlayer);
						}
					}
				}
			}
			if (Math.Abs(base.NPC.Center.X - player.Center.X) > 10f)
			{
				float playerLocation2 = base.NPC.Center.X - player.Center.X;
				base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
			}
			if (base.NPC.ai[1] > attackDivisor * 2f)
			{
				base.NPC.ai[0] = -1f;
				base.NPC.ai[1] = 2f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			base.NPC.damage = 0;
			DrawProjectileTelegraphTimer = 0;
			ChargeLocation(player, leviAlive && !phase4, revenge);
			base.NPC.ai[1]++;
			if (!(base.NPC.ai[1] >= (revenge ? 45f : 60f)))
			{
				return;
			}
			forceChargeFrames = true;
			float aiInterval = ((leviAlive && !phase4) ? 2f : 3f);
			base.NPC.ai[0] = ((base.NPC.ai[2] >= aiInterval) ? (-1f) : 4f);
			base.NPC.ai[1] = ((base.NPC.ai[2] >= aiInterval) ? 3f : 0f);
			base.NPC.localAI[0] = 0f;
			float chargeVelocity = ((leviAlive && !phase4) ? 21f : 26f);
			chargeVelocity += 8f * enrageScale;
			if (revenge)
			{
				chargeVelocity += 2f + (death ? (6f * (1f - lifeRatio)) : (4f * (1f - lifeRatio)));
			}
			if (Main.getGoodWorld)
			{
				chargeVelocity *= 1.15f;
			}
			if (base.NPC.ai[0] == -1f)
			{
				chargeVelocity *= 0.3f;
			}
			base.NPC.velocity = Vector2.Normalize(player.Center - base.NPC.Center) * chargeVelocity;
			base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
			int chargeDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
			if (chargeDirection != 0)
			{
				base.NPC.direction = chargeDirection;
				if (base.NPC.spriteDirection == 1)
				{
					base.NPC.rotation += (float)Math.PI;
				}
				base.NPC.spriteDirection = -base.NPC.direction;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 4f)
			{
				return;
			}
			base.NPC.damage = (int)Math.Round((float)base.NPC.defDamage * DashDamageMult);
			if (Main.zenithWorld && base.NPC.ai[1] % 5f == 0f)
			{
				SoundEngine.PlaySound(in SoundID.Item85, base.NPC.Center);
				Vector2 bubbleSpawnPos2 = default(Vector2);
				((Vector2)(ref bubbleSpawnPos2))._002Ector(base.NPC.position.X + (float)(base.NPC.width / 2) + (float)(15 * base.NPC.direction) * base.NPC.scale, base.NPC.position.Y + 30f * base.NPC.scale);
				if (Main.netMode != 1)
				{
					int spawn2 = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)bubbleSpawnPos2.X, (int)bubbleSpawnPos2.Y, 371);
					Main.npc[spawn2].target = base.NPC.target;
					Main.npc[spawn2].velocity = player.Center - bubbleSpawnPos2;
					((Vector2)(ref Main.npc[spawn2].velocity)).Normalize();
					NPC obj2 = Main.npc[spawn2];
					obj2.velocity *= bubbleVelocity;
					Main.npc[spawn2].netUpdate = true;
					Main.npc[spawn2].ai[3] = (float)Main.rand.Next(80, 121) / 100f;
				}
			}
			int dustAmt = 7;
			for (int n = 0; n < dustAmt; n++)
			{
				Vector2 val = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)(base.NPC.width + 50) / 2f, (float)base.NPC.height) * 0.75f).RotatedBy((float)(n - (dustAmt / 2 - 1)) * (float)Math.PI / (float)dustAmt) + base.NPC.Center;
				Vector2 vector4 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
				int waterDust = Dust.NewDust(val + vector4, 0, 0, 172, vector4.X * 2f, vector4.Y * 2f, 100, default(Color), 1.4f);
				Main.dust[waterDust].noGravity = true;
				Main.dust[waterDust].noLight = true;
				Dust obj3 = Main.dust[waterDust];
				obj3.velocity /= 4f;
				Dust obj4 = Main.dust[waterDust];
				obj4.velocity -= base.NPC.velocity;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= ((leviAlive && !phase4) ? 50f : 35f))
			{
				base.NPC.ai[0] = 3f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2]++;
				base.NPC.netUpdate = true;
			}
		}
	}

	private void ChargeRotation(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		float playerDirection = (float)Math.Atan2(player.Center.Y - base.NPC.Center.Y, player.Center.X - base.NPC.Center.X);
		if (base.NPC.spriteDirection == 1)
		{
			playerDirection += (float)Math.PI;
		}
		if (playerDirection < 0f)
		{
			playerDirection += (float)Math.PI * 2f;
		}
		if (playerDirection > (float)Math.PI * 2f)
		{
			playerDirection -= (float)Math.PI * 2f;
		}
		float rotationSpeed = 0.04f;
		if (base.NPC.ai[0] == 4f)
		{
			rotationSpeed = 0f;
		}
		if (rotationSpeed != 0f)
		{
			base.NPC.rotation = base.NPC.rotation.AngleTowards(playerDirection, rotationSpeed);
		}
	}

	private void ChargeLocation(Player player, bool leviAlive, bool revenge)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		float distance = (leviAlive ? 600f : 500f) * base.NPC.scale;
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = (int)distance * Math.Sign((base.NPC.Center - player.Center).X);
		}
		Vector2 chargeSetupLocation = Vector2.Normalize(player.Center + new Vector2(base.NPC.localAI[0], 0f - distance) - base.NPC.Center - base.NPC.velocity) * 12f;
		float acceleration = (revenge ? 0.75f : 0.5f);
		if (Main.getGoodWorld)
		{
			acceleration *= 1.15f;
		}
		base.NPC.SimpleFlyMovement(chargeSetupLocation, acceleration);
		int chargeDirection = Math.Sign(player.Center.X - base.NPC.Center.X);
		if (chargeDirection != 0)
		{
			if (base.NPC.ai[1] == 0f && chargeDirection != base.NPC.direction)
			{
				base.NPC.rotation += (float)Math.PI;
			}
			base.NPC.direction = chargeDirection;
			if (base.NPC.spriteDirection != -base.NPC.direction)
			{
				base.NPC.rotation += (float)Math.PI;
			}
			base.NPC.spriteDirection = -base.NPC.direction;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 187, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 187, hit.HitDirection, -1f);
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		switch (frameUsed)
		{
		case 0:
			texture = TextureAssets.Npc[base.Type].Value;
			break;
		case 1:
			texture = ChargeTexture.Value;
			break;
		}
		bool charging = base.NPC.ai[0] > 2f || forceChargeFrames;
		int height = texture.Height / Main.npcFrameCount[base.Type];
		int width = texture.Width;
		SpriteEffects spriteEffects = (SpriteEffects)(!charging);
		if (base.NPC.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)(charging ? 1 : 0);
		}
		if (DrawProjectileTelegraphTimer > 0)
		{
			Color telegraphColor = Color.Black;
			switch (Main.getGoodWorld ? GetGoodAttackChosen : ((int)base.NPC.localAI[3]))
			{
			case 0:
				((Color)(ref telegraphColor))._002Ector(55, 70, 240);
				break;
			case 1:
				telegraphColor = Color.White * 0.9f;
				break;
			case 2:
				((Color)(ref telegraphColor))._002Ector(199, 90, 67);
				break;
			}
			Lighting.AddLight(base.NPC.Center, ((Color)(ref telegraphColor)).ToVector3() * 0.75f);
			base.NPC.DrawBackglow(telegraphColor * 0.35f, 9f, spriteEffects, base.NPC.frame, screenPos, texture);
		}
		Main.spriteBatch.Draw(texture, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY), (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, new Vector2((float)width / 2f, (float)height / 2f), base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.ai[0] > 2f || forceChargeFrames)
		{
			frameUsed = 1;
		}
		else
		{
			frameUsed = 0;
		}
		int timeBetweenFrames = 8;
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > (double)(timeBetweenFrames * Main.npcFrameCount[base.Type]))
		{
			base.NPC.frameCounter = 0.0;
		}
		base.NPC.frame.Y = frameHeight * (int)(base.NPC.frameCounter / (double)timeBetweenFrames);
		if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type])
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}

	public override void OnKill()
	{
		if (Leviathan.LastAnLStanding())
		{
			Leviathan.RealOnKill(base.NPC);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		Leviathan.DefineAnahitaLeviathanLoot(npcLoot);
		npcLoot.Add(ModContent.ItemType<AnahitaTrophy>(), 10);
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
