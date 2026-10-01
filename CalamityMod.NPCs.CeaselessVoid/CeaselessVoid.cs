using System;
using System.IO;
using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Melee;
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

namespace CalamityMod.NPCs.CeaselessVoid;

[AutoloadBossHead]
public class CeaselessVoid : ModNPC
{
	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/CeaselessVoidDeath");

	public static readonly SoundStyle BuildupSound = new SoundStyle("CalamityMod/Sounds/Custom/CeaselessVoidDeathBuild");

	public static Asset<Texture2D> GlowTexture;

	public bool playedbuildsound;

	public bool madeItToLocation = true;

	public static int BeamPortalDamage = 60;

	public static int DarkEnergyProjectileDamage = 60;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.55f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 180;
		base.NPC.npcSlots = 36f;
		base.NPC.width = 100;
		base.NPC.height = 100;
		base.NPC.defense = 80;
		base.NPC.Calamity().DR = 0.5f;
		base.NPC.LifeMaxNERB(50000, 78000, 72000);
		base.NPC.value = Item.buyPrice(0, 50);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<CeaselessVoidBossBar>();
		base.NPC.DeathSound = DeathSound;
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CeaselessVoid")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(playedbuildsound);
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
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		playedbuildsound = reader.ReadBoolean();
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
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1102: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0890: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_150b: Unknown result type (might be due to invalid IL or missing references)
		//IL_151c: Unknown result type (might be due to invalid IL or missing references)
		//IL_156c: Unknown result type (might be due to invalid IL or missing references)
		//IL_157d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_1809: Unknown result type (might be due to invalid IL or missing references)
		//IL_181a: Unknown result type (might be due to invalid IL or missing references)
		//IL_186a: Unknown result type (might be due to invalid IL or missing references)
		//IL_187b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1690: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1702: Unknown result type (might be due to invalid IL or missing references)
		//IL_1261: Unknown result type (might be due to invalid IL or missing references)
		//IL_1263: Unknown result type (might be due to invalid IL or missing references)
		//IL_126b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1275: Unknown result type (might be due to invalid IL or missing references)
		//IL_127a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1282: Unknown result type (might be due to invalid IL or missing references)
		//IL_1296: Unknown result type (might be due to invalid IL or missing references)
		//IL_129b: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_146a: Unknown result type (might be due to invalid IL or missing references)
		//IL_148c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_176e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1790: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1611: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1001: Unknown result type (might be due to invalid IL or missing references)
		//IL_100f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1015: Unknown result type (might be due to invalid IL or missing references)
		//IL_1017: Unknown result type (might be due to invalid IL or missing references)
		//IL_101c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1023: Unknown result type (might be due to invalid IL or missing references)
		//IL_1028: Unknown result type (might be due to invalid IL or missing references)
		//IL_102d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_103a: Unknown result type (might be due to invalid IL or missing references)
		//IL_103c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1041: Unknown result type (might be due to invalid IL or missing references)
		//IL_1048: Unknown result type (might be due to invalid IL or missing references)
		//IL_104d: Unknown result type (might be due to invalid IL or missing references)
		//IL_105b: Unknown result type (might be due to invalid IL or missing references)
		//IL_105d: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.voidBoss = base.NPC.whoAmI;
		double lifeRatio = (double)base.NPC.life / (double)base.NPC.lifeMax;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool phase2 = lifeRatio <= 0.7;
		bool phase3 = lifeRatio <= 0.4;
		bool phase4 = lifeRatio <= 0.1;
		bool theBigSucc = (double)base.NPC.life / (double)base.NPC.lifeMax <= 0.1;
		bool succSoHardThatYouDie = (double)base.NPC.life / (double)base.NPC.lifeMax <= 0.005;
		int darkEnergyAmt = (death ? 6 : (revenge ? 5 : (expertMode ? 4 : 3)));
		if (phase2)
		{
			darkEnergyAmt++;
		}
		if (phase3)
		{
			darkEnergyAmt++;
		}
		if (phase4)
		{
			darkEnergyAmt++;
		}
		if (Main.getGoodWorld)
		{
			darkEnergyAmt *= 2;
		}
		int spacing = 360 / darkEnergyAmt;
		int distance2 = 10;
		if (base.NPC.ai[2] == 0f)
		{
			base.NPC.ai[2] = 1f;
			for (int i = 0; i < darkEnergyAmt; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(i * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(i * spacing) * (double)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, i * spacing, j);
				}
			}
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + (float)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, 0f, 0.5f);
			NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + (float)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, 0f, 1.5f);
		}
		bool anyDarkEnergies = NPC.AnyNPCs(ModContent.NPCType<DarkEnergy>());
		bool movingDuringSuccPhase = base.NPC.ai[3] == 0f;
		base.NPC.dontTakeDamage = anyDarkEnergies | theBigSucc | movingDuringSuccPhase;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
		if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f || ((double)player.position.Y < Main.worldSurface * 16.0 && !BossRushEvent.BossRushActive))
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead || Vector2.Distance(player.Center, base.NPC.Center) > 5600f || ((double)player.position.Y < Main.worldSurface * 16.0 && !BossRushEvent.BossRushActive))
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
				return;
			}
		}
		else if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		float projectileFireRateMultiplier = (Main.getGoodWorld ? 0.5f : 1.5f);
		float distanceRequiredToMove = (Main.getGoodWorld ? 300f : 720f);
		bool move = Vector2.Distance(base.NPC.Center, player.Center) > distanceRequiredToMove || !Collision.CanHit(base.NPC.Center, 1, 1, player.Center, 1, 1);
		if (!anyDarkEnergies)
		{
			float suckDistance = (death ? 1600f : (revenge ? 1440f : (expertMode ? 1280f : 1040f)));
			if (movingDuringSuccPhase)
			{
				base.NPC.damage = 0;
				if (move)
				{
					Movement(succ: true);
				}
				else
				{
					base.NPC.ai[3] = 1f;
				}
			}
			else
			{
				base.NPC.damage = base.NPC.defDamage;
				float finalPhaseDustRatio = 1f;
				if (succSoHardThatYouDie)
				{
					finalPhaseDustRatio = 5f;
				}
				else if (theBigSucc)
				{
					float amount = (10f - (float)((double)base.NPC.life / (double)base.NPC.lifeMax) * 100f) / 10f;
					finalPhaseDustRatio += MathHelper.Lerp(0f, 2f, amount);
				}
				if (((Vector2)(ref base.NPC.velocity)).Length() > 0.5f)
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.8f;
				}
				else
				{
					base.NPC.velocity = Vector2.Zero;
				}
				float moveCloserGateValue = suckDistance * 0.8f;
				if (Vector2.Distance(base.NPC.Center, player.Center) > moveCloserGateValue)
				{
					base.NPC.ai[3] = 0f;
				}
				for (int h = 0; h < 3; h++)
				{
					float distanceDivisor = (float)h + 1f;
					float dustDistance = suckDistance / distanceDivisor;
					int numDust = (int)((float)Math.PI / 5f * dustDistance);
					Vector2 dustOffset = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * dustDistance;
					int var = (int)(dustDistance / finalPhaseDustRatio);
					float dustVelocity = 24f / distanceDivisor * finalPhaseDustRatio;
					for (int k = 0; k < numDust; k++)
					{
						if (Main.rand.NextBool(var))
						{
							dustOffset = dustOffset.RotatedBy((float)Math.PI * 2f / (float)numDust);
							Vector2 dustSpawn = base.NPC.Center + dustOffset;
							int type = ModContent.DustType<CeaselessDust>();
							Vector2? velocity = dustSpawn.DirectionTo(base.NPC.Center) * dustVelocity;
							float scale = 3 - h;
							Dust.NewDustPerfect(dustSpawn, type, velocity, 0, default(Color), scale).fadeIn = 1f;
						}
					}
				}
				float succPower = 0.125f + finalPhaseDustRatio * 0.125f;
				for (int l = 0; l < 255; l++)
				{
					float distance3 = Vector2.Distance(Main.player[l].Center, base.NPC.Center);
					if (distance3 < suckDistance && Main.player[l].grappling[0] == -1 && Collision.CanHit(base.NPC.Center, 1, 1, Main.player[l].Center, 1, 1))
					{
						float distanceRatio = distance3 / suckDistance;
						float multiplier = 1f - distanceRatio;
						if (Main.player[l].Center.X < base.NPC.Center.X)
						{
							Main.player[l].velocity.X += succPower * multiplier;
						}
						else
						{
							Main.player[l].velocity.X -= succPower * multiplier;
						}
					}
				}
				if (theBigSucc && calamityGlobalNPC.newAI[1] % 60f == 0f)
				{
					int damageIncrement = base.NPC.lifeMax / (Main.zenithWorld ? 600 : 200);
					if (Main.netMode != 1)
					{
						base.NPC.life -= damageIncrement;
						base.NPC.DamageEffect(damageIncrement);
					}
					if (base.NPC.life <= damageIncrement * 5 && !playedbuildsound)
					{
						SoundEngine.PlaySound(in BuildupSound, base.NPC.Center);
						playedbuildsound = true;
					}
					if (base.NPC.life <= 0)
					{
						base.NPC.life = 0;
						base.NPC.HitEffect();
						base.NPC.checkDead();
					}
					base.NPC.netUpdate = true;
				}
				if (Main.netMode != 1 && calamityGlobalNPC.newAI[1] == 0f)
				{
					int numBeamPortals = (revenge ? 3 : 2);
					float degrees = 360 / numBeamPortals;
					float beamPortalDistance = (death ? 400f : (revenge ? 420f : (expertMode ? 440f : 480f)));
					int type2 = ModContent.ProjectileType<DoGBeamPortal>();
					for (int m = 0; m < numBeamPortals; m++)
					{
						float ai1 = (float)m * degrees;
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), player.Center.X + (float)(Math.Sin((float)m * degrees) * (double)beamPortalDistance), player.Center.Y + (float)(Math.Cos((float)m * degrees) * (double)beamPortalDistance), 0f, 0f, type2, BeamPortalDamage, 0f, Main.myPlayer, ai1);
					}
				}
				float beamPortalTimeLeft = 600f;
				bool summonLessDarkEnergies = false;
				if (calamityGlobalNPC.newAI[1] < beamPortalTimeLeft)
				{
					calamityGlobalNPC.newAI[1]++;
					summonLessDarkEnergies = true;
				}
				else if (theBigSucc)
				{
					calamityGlobalNPC.newAI[1]++;
				}
				calamityGlobalNPC.newAI[3]++;
				float darkEnergySpiralGateValue = (summonLessDarkEnergies ? 24f : 12f) * projectileFireRateMultiplier;
				if (calamityGlobalNPC.newAI[3] >= darkEnergySpiralGateValue)
				{
					calamityGlobalNPC.newAI[3] = 0f;
					if (Main.netMode != 1)
					{
						int type3 = ModContent.ProjectileType<DarkEnergyBall>();
						bool normalSpread = base.NPC.localAI[0] % 2f == 0f;
						float speed = 0.5f;
						int totalProjectiles = 4;
						Vector2 spinningPoint = default(Vector2);
						((Vector2)(ref spinningPoint))._002Ector(normalSpread ? 0f : (0f - speed), 0f - speed);
						float radialOffset = MathHelper.ToRadians(base.NPC.localAI[1]);
						for (int n = 0; n < totalProjectiles; n++)
						{
							Vector2 spawnVector = base.NPC.Center + Vector2.Normalize(spinningPoint.RotatedBy((float)Math.PI * 2f / (float)totalProjectiles * (float)n + radialOffset)) * suckDistance;
							Vector2 velocity2 = Vector2.Normalize(base.NPC.Center - spawnVector) * speed;
							Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector, velocity2, type3, DarkEnergyProjectileDamage, 0f, Main.myPlayer);
						}
					}
					base.NPC.localAI[1] += 10f;
				}
				if (phase2 & expertMode)
				{
					base.NPC.localAI[2]++;
					if (base.NPC.localAI[2] >= 60f * projectileFireRateMultiplier)
					{
						base.NPC.localAI[2] = 0f;
						if (Main.netMode != 1)
						{
							int type4 = ModContent.ProjectileType<DarkEnergyBall2>();
							bool normalSpread2 = base.NPC.localAI[0] % 2f != 0f;
							float speed2 = 2f;
							int totalProjectiles2 = 2;
							float radians = (float)Math.PI * 2f / (float)totalProjectiles2;
							double angleA = (double)radians * 0.5;
							double angleB = (double)MathHelper.ToRadians(90f) - angleA;
							float velocityX = (float)((double)speed2 * Math.Sin(angleA) / Math.Sin(angleB));
							Vector2 spinningPoint2 = default(Vector2);
							((Vector2)(ref spinningPoint2))._002Ector(normalSpread2 ? 0f : (0f - velocityX), 0f - speed2);
							float radialOffset2 = MathHelper.ToRadians(base.NPC.localAI[1] * 0.25f);
							for (int num = 0; num < totalProjectiles2; num++)
							{
								Vector2 spawnVector2 = base.NPC.Center + Vector2.Normalize(spinningPoint2.RotatedBy(radians * (float)num + radialOffset2)) * suckDistance;
								Vector2 velocity3 = Vector2.Normalize(base.NPC.Center - spawnVector2) * speed2;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector2, velocity3, type4, DarkEnergyProjectileDamage, 0f, Main.myPlayer);
							}
						}
					}
				}
				if (phase4 & revenge)
				{
					base.NPC.localAI[3]++;
					if (base.NPC.localAI[3] >= 90f * projectileFireRateMultiplier)
					{
						base.NPC.localAI[3] = 0f;
						if (Main.netMode != 1)
						{
							int type5 = ModContent.ProjectileType<DarkEnergyBall2>();
							bool normalSpread3 = base.NPC.localAI[0] % 2f == 0f;
							float speed3 = 4f;
							int totalProjectiles3 = 2;
							float radians2 = (float)Math.PI * 2f / (float)totalProjectiles3;
							double angleA2 = (double)radians2 * 0.5;
							double angleB2 = (double)MathHelper.ToRadians(90f) - angleA2;
							float velocityX2 = (float)((double)speed3 * Math.Sin(angleA2) / Math.Sin(angleB2));
							Vector2 spinningPoint3 = default(Vector2);
							((Vector2)(ref spinningPoint3))._002Ector(normalSpread3 ? 0f : (0f - velocityX2), 0f - speed3);
							float radialOffset3 = MathHelper.ToRadians(base.NPC.localAI[1] * 0.25f);
							for (int num2 = 0; num2 < totalProjectiles3; num2++)
							{
								Vector2 spawnVector3 = base.NPC.Center + Vector2.Normalize(spinningPoint3.RotatedBy(radians2 * (float)num2 + radialOffset3)) * suckDistance;
								Vector2 velocity4 = Vector2.Normalize(base.NPC.Center - spawnVector3) * speed3;
								Projectile.NewProjectile(base.NPC.GetSource_FromAI(), spawnVector3, velocity4, type5, DarkEnergyProjectileDamage, 0f, Main.myPlayer);
							}
						}
					}
				}
			}
		}
		else
		{
			base.NPC.damage = 0;
			if (move)
			{
				madeItToLocation = false;
			}
			if (!madeItToLocation)
			{
				Movement(succ: false);
			}
			else if (((Vector2)(ref base.NPC.velocity)).Length() > 0.5f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.8f;
			}
			else
			{
				base.NPC.velocity = Vector2.Zero;
			}
			int totalDarkEnergyHP = 0;
			for (int num3 = 0; num3 < Main.maxNPCs; num3++)
			{
				NPC darkEnergy = Main.npc[num3];
				if (darkEnergy.active && darkEnergy.type == ModContent.NPCType<DarkEnergy>())
				{
					totalDarkEnergyHP += darkEnergy.life;
				}
			}
			int num4 = (BossRushEvent.BossRushActive ? 20000 : 12000);
			double HPBoost = (double)CalamityServerConfig.Instance.BossHealthBoost * 0.01;
			int num5 = num4 + (int)((double)num4 * HPBoost);
			int totalDarkEnergiesSpawned = darkEnergyAmt * 3 + 2;
			int succPhaseGateValue = (int)((double)(num5 * totalDarkEnergiesSpawned) * 0.2);
			if (totalDarkEnergyHP < succPhaseGateValue)
			{
				SoundEngine.PlaySound(in SoundID.NPCDeath44, base.NPC.Center);
				for (int num6 = 0; num6 < Main.maxNPCs; num6++)
				{
					NPC darkEnergy2 = Main.npc[num6];
					if (darkEnergy2.active && darkEnergy2.type == ModContent.NPCType<DarkEnergy>())
					{
						darkEnergy2.HitEffect();
						darkEnergy2.active = false;
						darkEnergy2.netUpdate = true;
					}
				}
				int dustAmt = 30;
				int random = 3;
				Vector2 dustVelocity2 = default(Vector2);
				for (int num7 = 0; num7 < 10; num7++)
				{
					random += num7 * 2;
					for (int d = 0; d < dustAmt; d++)
					{
						((Vector2)(ref dustVelocity2))._002Ector((float)Main.rand.Next(-random, random), (float)Main.rand.Next(-random, random));
						dustVelocity2 = Vector2.Normalize(dustVelocity2) * (float)random * 2f;
						Dust.NewDustPerfect(base.NPC.Center + Main.rand.NextVector2CircularEdge(10f, 10f), 173, dustVelocity2, 100, default(Color), 5f).noGravity = true;
					}
				}
			}
		}
		if (calamityGlobalNPC.newAI[0] == 0f && base.NPC.life > 0)
		{
			calamityGlobalNPC.newAI[0] = 1f;
		}
		if (base.NPC.life <= 0)
		{
			return;
		}
		int healthGateValue = (int)((double)base.NPC.lifeMax * 0.3);
		if (!((float)(base.NPC.life + healthGateValue) / (float)base.NPC.lifeMax < calamityGlobalNPC.newAI[0]))
		{
			return;
		}
		base.NPC.TargetClosest();
		calamityGlobalNPC.newAI[0] -= 0.3f;
		calamityGlobalNPC.newAI[1] = 0f;
		calamityGlobalNPC.newAI[2] = 0f;
		calamityGlobalNPC.newAI[3] = 0f;
		base.NPC.ai[3] = 0f;
		base.NPC.localAI[0]++;
		base.NPC.localAI[1] = 0f;
		base.NPC.localAI[2] = 0f;
		base.NPC.localAI[3] = 0f;
		if (Main.netMode != 1)
		{
			if (phase4)
			{
				madeItToLocation = false;
				for (int num8 = 0; num8 < darkEnergyAmt; num8++)
				{
					for (int num9 = 0; num9 < 3; num9++)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(num8 * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(num8 * spacing) * (double)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, num8 * spacing, (float)num9 * 2f);
					}
				}
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + (float)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, 0f, 1f);
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + (float)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, 0f, 3f);
			}
			else if (phase3)
			{
				madeItToLocation = false;
				for (int num10 = 0; num10 < darkEnergyAmt; num10++)
				{
					for (int num11 = 0; num11 < 3; num11++)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(num10 * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(num10 * spacing) * (double)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, num10 * spacing, (float)num11 * 1.5f);
					}
				}
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + (float)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, 0f, 0.5f);
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + (float)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, 0f, 2f);
			}
			else
			{
				madeItToLocation = false;
				for (int num12 = 0; num12 < darkEnergyAmt; num12++)
				{
					for (int num13 = 0; num13 < 3; num13++)
					{
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)((double)base.NPC.Center.X + Math.Sin(num12 * spacing) * (double)distance2), (int)((double)base.NPC.Center.Y + Math.Cos(num12 * spacing) * (double)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, num12 * spacing, num13);
					}
				}
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + (float)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, 0f, 1.5f);
				NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)(base.NPC.Center.Y + (float)distance2), ModContent.NPCType<DarkEnergy>(), base.NPC.whoAmI, 0f, 2.5f);
			}
		}
		for (int num14 = 0; num14 < Main.maxProjectiles; num14++)
		{
			Projectile projectile = Main.projectile[num14];
			if (projectile.active && (projectile.type == ModContent.ProjectileType<DoGBeamPortal>() || projectile.type == ModContent.ProjectileType<DoGBeam>() || projectile.type == ModContent.ProjectileType<DarkEnergyBall>() || projectile.type == ModContent.ProjectileType<DarkEnergyBall2>()) && projectile.timeLeft > 30)
			{
				projectile.timeLeft = 30;
			}
		}
		void Movement(bool succ)
		{
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0103: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0214: Unknown result type (might be due to invalid IL or missing references)
			//IL_0215: Unknown result type (might be due to invalid IL or missing references)
			//IL_0217: Unknown result type (might be due to invalid IL or missing references)
			//IL_021c: Unknown result type (might be due to invalid IL or missing references)
			//IL_021d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0224: Unknown result type (might be due to invalid IL or missing references)
			//IL_0229: Unknown result type (might be due to invalid IL or missing references)
			//IL_022e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_025d: Unknown result type (might be due to invalid IL or missing references)
			//IL_026d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01df: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Unknown result type (might be due to invalid IL or missing references)
			//IL_0206: Unknown result type (might be due to invalid IL or missing references)
			//IL_0208: Unknown result type (might be due to invalid IL or missing references)
			//IL_020d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0212: Unknown result type (might be due to invalid IL or missing references)
			float velocity5 = (expertMode ? 7.5f : 6f) + (float)(death ? (2.0 * (1.0 - lifeRatio)) : 0.0);
			float acceleration = (death ? 0.2f : (expertMode ? 0.16f : 0.12f)) + (float)(death ? (0.03999999910593033 * (1.0 - lifeRatio)) : 0.0);
			if (succ)
			{
				velocity5 *= 2f;
				acceleration *= 2f;
			}
			if (!madeItToLocation)
			{
				velocity5 *= 2f;
				acceleration *= 5f;
			}
			if (Main.getGoodWorld)
			{
				velocity5 *= 1.15f;
				acceleration *= 1.15f;
			}
			Vector2 destination = player.Center;
			float maxDistance = 320f;
			Vector2 moveToOffset = (Vector2)(succ ? Vector2.Zero : (Main.getGoodWorld ? new Vector2(0f, 0f - maxDistance) : Vector2.Zero));
			if ((!succ && Main.getGoodWorld) || !madeItToLocation)
			{
				calamityGlobalNPC.newAI[2]++;
				float newPositionGateValue = (death ? 180f : (revenge ? 210f : (expertMode ? 240f : 300f)));
				if (calamityGlobalNPC.newAI[2] > newPositionGateValue)
				{
					calamityGlobalNPC.newAI[2] = 0f;
					base.NPC.ai[0]++;
					if (base.NPC.ai[0] > 7f)
					{
						base.NPC.ai[0] = 0f;
					}
				}
				moveToOffset += Utils.RotatedBy(new Vector2(maxDistance, 0f), (double)(base.NPC.ai[0] / 8f * ((float)Math.PI * 2f)), default(Vector2));
			}
			destination += moveToOffset;
			Vector2 distanceFromDestination = destination - base.NPC.Center;
			if (((base.NPC.Distance(destination) > maxDistance) | succ) || (!Main.getGoodWorld && !madeItToLocation))
			{
				CalamityUtils.SmoothMovement(base.NPC, 0f, distanceFromDestination, velocity5, acceleration, useSimpleFlyMovement: true);
			}
			if (base.NPC.Distance(destination) < 80f)
			{
				madeItToLocation = true;
			}
		}
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
		return minDist <= 50f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(164, 60);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		int afterimageAmt = 7;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.White, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimageDrawPos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimageDrawPos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimageDrawPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimageDrawPos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		Color cyanLerp = Color.Lerp(Color.White, Color.Cyan, 0.5f);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageAmt; j++)
			{
				Color extraAfterimageColor = cyanLerp;
				extraAfterimageColor = Color.Lerp(extraAfterimageColor, Color.White, 0.5f);
				extraAfterimageColor *= (float)(afterimageAmt - j) / 15f;
				Vector2 extraAfterimageDrawPos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				extraAfterimageDrawPos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				extraAfterimageDrawPos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, extraAfterimageDrawPos, (Rectangle?)base.NPC.frame, extraAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, cyanLerp, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void OnKill()
	{
		if (!BossRushEvent.BossRushActive)
		{
			CalamityGlobalNPC.SetNewBossJustDowned(base.NPC);
			DownedBossSystem.downedCeaselessVoid = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<CeaselessVoidBag>()));
		LeadingConditionRule normalOnly = new LeadingConditionRule(new Conditions.NotExpert());
		npcLoot.Add(normalOnly);
		int[] weapons = new int[2]
		{
			ModContent.ItemType<MirrorBlade>(),
			ModContent.ItemType<VoidConcentrationStaff>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, weapons));
		normalOnly.Add(DropHelper.PerPlayer(ModContent.ItemType<DarkPlasma>(), 1, 10, 12));
		normalOnly.Add(ModContent.ItemType<CeaselessVoidMask>(), 7);
		IItemDropRule godSlayerVanity = ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerHelm>(), 20);
		godSlayerVanity.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerChestplate>()));
		godSlayerVanity.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AncientGodSlayerLeggings>()));
		normalOnly.Add(godSlayerVanity);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		npcLoot.Add(ModContent.ItemType<CeaselessVoidTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<CeaselessVoidRelic>());
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<EclipseMirror>()), hideLootReport: true);
		mainRule.Add(DropHelper.PerPlayer(ModContent.ItemType<Nucleogenesis>()), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(() => !DownedBossSystem.downedCeaselessVoid, ModContent.ItemType<LoreCeaselessVoid>(), ui: true, DropHelper.FirstKillText);
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = ModContent.ItemType<SupremeHealingPotion>();
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0 && (float)base.NPC.life >= (float)base.NPC.lifeMax * 0.05f)
		{
			base.NPC.soundDelay = 8;
			float pitchVar = (Main.zenithWorld ? 0.4f : 0f);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/OtherworldlyHit");
			style.PitchVariance = pitchVar;
			SoundEngine.PlaySound(in style, base.NPC.Center);
		}
		for (int k = 0; k < 5; k++)
		{
			int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, hit.HitDirection, -1f);
			Main.dust[dust].noGravity = true;
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = 100;
		base.NPC.height = 100;
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int purpleDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[purpleDust];
			obj.velocity *= 3f;
			Main.dust[purpleDust].noGravity = true;
			if (Main.rand.NextBool())
			{
				Main.dust[purpleDust].scale = 0.5f;
				Main.dust[purpleDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int purpleDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
			Main.dust[purpleDust2].noGravity = true;
			Dust obj2 = Main.dust[purpleDust2];
			obj2.velocity *= 5f;
			purpleDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Main.dust[purpleDust2].noGravity = true;
			Dust obj3 = Main.dust[purpleDust2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("CeaselessVoid").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("CeaselessVoid2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("CeaselessVoid2").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("CeaselessVoid3").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("CeaselessVoid3").Type);
		}
	}
}
