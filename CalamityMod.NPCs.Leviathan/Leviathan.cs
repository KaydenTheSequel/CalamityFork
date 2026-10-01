using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Monoliths;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
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

namespace CalamityMod.NPCs.Leviathan;

[AutoloadBossHead]
[HasPierceResist(true)]
public class Leviathan : ModNPC
{
	private int biomeEnrageTimer = 300;

	private int counter;

	private bool initialised;

	private bool gfbAnaSummoned;

	private int soundDelay;

	private float extrapitch;

	public static Asset<Texture2D> AttackTexture = null;

	public static readonly SoundStyle RoarMeteorSound = new SoundStyle("CalamityMod/Sounds/Custom/LeviathanRoarMeteor");

	public static readonly SoundStyle RoarChargeSound = new SoundStyle("CalamityMod/Sounds/Custom/LeviathanRoarCharge");

	public static readonly SoundStyle EmergeSound = new SoundStyle("CalamityMod/Sounds/Custom/LeviathanEmerge");

	public static int BoulderDamage = 40;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 3;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		if (!Main.dedServ)
		{
			AttackTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Leviathan/LeviathanAttack", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.npcSlots = 20f;
		base.NPC.damage = 100;
		base.NPC.width = 900;
		base.NPC.height = 450;
		base.NPC.defense = 40;
		base.NPC.DR_NERD(0.1f);
		base.NPC.LifeMaxNERB(50000, 67500, 600000);
		base.NPC.knockBackResist = 0f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.Opacity = 0f;
		base.NPC.value = Item.buyPrice(0, 7, 50);
		base.NPC.HitSound = SoundID.NPCHit56;
		base.NPC.DeathSound = SoundID.NPCDeath60;
		base.NPC.noTileCollide = true;
		base.NPC.noGravity = true;
		base.NPC.boss = true;
		base.NPC.BossBar = ModContent.GetInstance<LeviathanAnahitaBossBar>();
		base.NPC.netAlways = true;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 1.3f;
		}
		if (Main.zenithWorld)
		{
			base.NPC.scale *= 0.3f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Leviathan")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(biomeEnrageTimer);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(soundDelay);
		writer.Write(base.NPC.Calamity().newAI[3]);
		writer.Write(gfbAnaSummoned);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		biomeEnrageTimer = reader.ReadInt32();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		soundDelay = reader.ReadInt32();
		base.NPC.Calamity().newAI[3] = reader.ReadSingle();
		gfbAnaSummoned = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_083a: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1003: Unknown result type (might be due to invalid IL or missing references)
		//IL_100f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1add: Unknown result type (might be due to invalid IL or missing references)
		//IL_1415: Unknown result type (might be due to invalid IL or missing references)
		//IL_141a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1440: Unknown result type (might be due to invalid IL or missing references)
		//IL_1445: Unknown result type (might be due to invalid IL or missing references)
		//IL_144f: Unknown result type (might be due to invalid IL or missing references)
		//IL_146b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1471: Unknown result type (might be due to invalid IL or missing references)
		//IL_1473: Unknown result type (might be due to invalid IL or missing references)
		//IL_1478: Unknown result type (might be due to invalid IL or missing references)
		//IL_147a: Unknown result type (might be due to invalid IL or missing references)
		//IL_149a: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_14df: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1518: Unknown result type (might be due to invalid IL or missing references)
		//IL_1522: Unknown result type (might be due to invalid IL or missing references)
		//IL_1527: Unknown result type (might be due to invalid IL or missing references)
		//IL_1535: Unknown result type (might be due to invalid IL or missing references)
		//IL_1540: Unknown result type (might be due to invalid IL or missing references)
		//IL_1545: Unknown result type (might be due to invalid IL or missing references)
		//IL_154a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1564: Unknown result type (might be due to invalid IL or missing references)
		//IL_1570: Unknown result type (might be due to invalid IL or missing references)
		//IL_1588: Unknown result type (might be due to invalid IL or missing references)
		//IL_1594: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bde: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1658: Unknown result type (might be due to invalid IL or missing references)
		//IL_165a: Unknown result type (might be due to invalid IL or missing references)
		//IL_165e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1668: Unknown result type (might be due to invalid IL or missing references)
		//IL_1674: Unknown result type (might be due to invalid IL or missing references)
		//IL_167e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1851: Unknown result type (might be due to invalid IL or missing references)
		//IL_185d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1736: Unknown result type (might be due to invalid IL or missing references)
		//IL_173e: Unknown result type (might be due to invalid IL or missing references)
		//IL_188a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1896: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c46: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1932: Unknown result type (might be due to invalid IL or missing references)
		//IL_193b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1977: Unknown result type (might be due to invalid IL or missing references)
		//IL_1980: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a2c: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC calamityGlobalNPC = base.NPC.Calamity();
		CalamityGlobalNPC.leviathan = base.NPC.whoAmI;
		if (CalamityGlobalNPC.LeviAndAna == -1)
		{
			CalamityGlobalNPC.LeviAndAna = base.NPC.whoAmI;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		Vector2 npcCenter = base.NPC.Center;
		float spawnAnimationTime = 180f;
		bool spawnAnimation = calamityGlobalNPC.newAI[3] < spawnAnimationTime;
		float lifeRatio = (float)base.NPC.life / (float)base.NPC.lifeMax;
		bool phase2 = ((lifeRatio < 0.7f) | death) & expertMode;
		bool phase3 = lifeRatio < (death ? 0.7f : 0.4f);
		bool phase4 = lifeRatio < (death ? 0.4f : 0.2f);
		bool sirenAlive = false;
		if (CalamityGlobalNPC.siren != -1)
		{
			sirenAlive = Main.npc[CalamityGlobalNPC.siren].active;
		}
		if (CalamityGlobalNPC.siren != -1 && Main.npc[CalamityGlobalNPC.siren].active && !phase3 && Main.npc[CalamityGlobalNPC.siren].damage == 0)
		{
			sirenAlive = false;
		}
		if (phase2 && !sirenAlive && Main.zenithWorld && !gfbAnaSummoned)
		{
			if (Main.netMode != 1)
			{
				CalamityUtils.BossAwakenMessage(NPC.NewNPC(base.NPC.GetSource_Death(), (int)base.NPC.Center.X, (int)base.NPC.position.Y + base.NPC.height - 1000, ModContent.NPCType<Anahita>(), base.NPC.whoAmI));
			}
			gfbAnaSummoned = true;
		}
		SoundStyle soundChoiceRage = SoundID.Zombie92;
		SoundStyle soundChoice = Utils.SelectRandom<SoundStyle>(Main.rand, SoundID.Zombie38, SoundID.Zombie39, SoundID.Zombie40);
		if (soundDelay > 0)
		{
			soundDelay--;
		}
		extrapitch = (Main.zenithWorld ? 0.3f : 0f);
		if (Main.rand.NextBool(600) && !spawnAnimation)
		{
			SoundStyle obj = ((sirenAlive && !death) ? soundChoice : soundChoiceRage);
			SoundEngine.PlaySound(obj with
			{
				Pitch = soundChoice.Pitch + extrapitch
			}, npcCenter);
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, npcCenter) > 3200f)
		{
			base.NPC.TargetClosest();
		}
		Player player = Main.player[base.NPC.target];
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
			enrageScale += 2f;
		}
		base.NPC.dontTakeDamage = spawnAnimation;
		bool immuneToSlowingDebuffs = base.NPC.ai[0] == 2f;
		base.NPC.buffImmune[ModContent.BuffType<GlacialState>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TemporalSadness>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Eutrophication>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<TimeDistortion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<GalvanicCorrosion>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[ModContent.BuffType<Vaporfied>()] = immuneToSlowingDebuffs;
		base.NPC.buffImmune[149] = immuneToSlowingDebuffs;
		if (!player.active || player.dead || Vector2.Distance(player.Center, npcCenter) > 5600f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (player.active && !player.dead && !(Vector2.Distance(player.Center, npcCenter) > 5600f))
			{
				return;
			}
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
					if (Main.npc[x].type == ModContent.NPCType<Anahita>())
					{
						Main.npc[x].active = false;
						Main.npc[x].netUpdate = true;
					}
				}
				base.NPC.active = false;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.ai[0] != 0f)
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.netUpdate = true;
			}
			return;
		}
		bool canCharge = ((phase2 | phase3) && !sirenAlive) | phase4 | death;
		if (spawnAnimation)
		{
			base.NPC.damage = 0;
			float minSpawnVelocity = 0.4f;
			float maxSpawnVelocity = 4f;
			float velocityY = maxSpawnVelocity - MathHelper.Lerp(minSpawnVelocity, maxSpawnVelocity, calamityGlobalNPC.newAI[3] / spawnAnimationTime);
			base.NPC.velocity = new Vector2(0f, 0f - velocityY);
			if (calamityGlobalNPC.newAI[3] == 10f)
			{
				SoundEngine.PlaySound(in EmergeSound, npcCenter);
				SoundStyle style = soundChoiceRage with
				{
					Pitch = soundChoiceRage.Pitch + extrapitch
				};
				SoundEngine.PlaySound(in style, npcCenter);
			}
			base.NPC.Opacity = MathHelper.Clamp(calamityGlobalNPC.newAI[3] / spawnAnimationTime, 0f, 1f);
			calamityGlobalNPC.newAI[3]++;
		}
		else if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			float hoverSpeed = ((sirenAlive && !phase4) ? 3.5f : 7f);
			float hoverAcceleration = ((sirenAlive && !phase4) ? 0.1f : 0.2f);
			hoverSpeed += 2f * enrageScale;
			hoverAcceleration += 0.05f * enrageScale;
			if (expertMode && (!sirenAlive | phase4))
			{
				hoverSpeed += (death ? (6f * (1f - lifeRatio)) : (3.5f * (1f - lifeRatio)));
				hoverAcceleration += (death ? (0.15f * (1f - lifeRatio)) : (0.1f * (1f - lifeRatio)));
			}
			if (Main.getGoodWorld)
			{
				hoverSpeed *= 1.15f;
				hoverAcceleration *= 1.15f;
			}
			int posXSign = 1;
			if (npcCenter.X < player.position.X + (float)player.width)
			{
				posXSign = -1;
			}
			Vector2 leviCenter = npcCenter;
			float xDestination = player.Center.X + (float)posXSign * ((sirenAlive && !phase4) ? 1000f : 800f) * base.NPC.scale - leviCenter.X;
			float yDestination = player.Center.Y - leviCenter.Y;
			float destinationDist = (float)Math.Sqrt(xDestination * xDestination + yDestination * yDestination);
			destinationDist = hoverSpeed / destinationDist;
			xDestination *= destinationDist;
			yDestination *= destinationDist;
			if (base.NPC.velocity.X < xDestination)
			{
				base.NPC.velocity.X += hoverAcceleration;
				if (base.NPC.velocity.X < 0f && xDestination > 0f)
				{
					base.NPC.velocity.X += hoverAcceleration;
				}
			}
			else if (base.NPC.velocity.X > xDestination)
			{
				base.NPC.velocity.X -= hoverAcceleration;
				if (base.NPC.velocity.X > 0f && xDestination < 0f)
				{
					base.NPC.velocity.X -= hoverAcceleration;
				}
			}
			if (base.NPC.velocity.Y < yDestination)
			{
				base.NPC.velocity.Y += hoverAcceleration;
				if (base.NPC.velocity.Y < 0f && yDestination > 0f)
				{
					base.NPC.velocity.Y += hoverAcceleration;
				}
			}
			else if (base.NPC.velocity.Y > yDestination)
			{
				base.NPC.velocity.Y -= hoverAcceleration;
				if (base.NPC.velocity.Y > 0f && yDestination < 0f)
				{
					base.NPC.velocity.Y -= hoverAcceleration;
				}
			}
			float playerLocation = npcCenter.X - player.Center.X;
			base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.ai[1]++;
			float phaseTimer = 360f;
			if (!sirenAlive | phase4)
			{
				phaseTimer -= 120f * (1f - lifeRatio);
			}
			if (base.NPC.ai[1] >= phaseTimer)
			{
				base.NPC.ai[0] = ((!canCharge) ? 1f : (Main.rand.NextBool() ? 2f : 1f));
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
				return;
			}
			if (!player.dead)
			{
				base.NPC.ai[2]++;
				if (!sirenAlive | phase4)
				{
					base.NPC.ai[2]++;
				}
			}
			if (!(base.NPC.ai[2] >= 75f))
			{
				return;
			}
			base.NPC.ai[2] = 0f;
			((Vector2)(ref leviCenter))._002Ector(npcCenter.X, npcCenter.Y + 20f);
			xDestination = leviCenter.X + 1000f * (float)base.NPC.direction - leviCenter.X;
			yDestination = player.Center.Y - leviCenter.Y;
			if (Main.netMode != 1)
			{
				float speed = ((sirenAlive && !phase4 && !death) ? 13.5f : 16f);
				int type = ModContent.ProjectileType<LeviathanBomb>();
				if (expertMode)
				{
					speed = ((sirenAlive && !phase4 && !death) ? 14f : 17f);
				}
				speed += 2f * enrageScale;
				if (!sirenAlive | phase4)
				{
					speed += 3f * (1f - lifeRatio);
				}
				destinationDist = (float)Math.Sqrt(xDestination * xDestination + yDestination * yDestination);
				destinationDist = speed / destinationDist;
				xDestination *= destinationDist;
				yDestination *= destinationDist;
				leviCenter.X += xDestination * 4f;
				leviCenter.Y += yDestination * 4f;
				if (Main.zenithWorld)
				{
					type = (Main.rand.NextBool() ? 1013 : 99);
					leviCenter.Y -= 5f;
				}
				int proj = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), leviCenter.X, leviCenter.Y, xDestination, yDestination, type, BoulderDamage, 0f, Main.myPlayer);
				if (Main.zenithWorld)
				{
					Main.projectile[proj].scale *= 5f;
				}
				if (soundDelay <= 0)
				{
					soundDelay = 120;
					SoundStyle style = RoarMeteorSound with
					{
						Pitch = RoarMeteorSound.Pitch + extrapitch
					};
					SoundEngine.PlaySound(in style, npcCenter);
				}
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			Vector2 aberrationSpawn = default(Vector2);
			((Vector2)(ref aberrationSpawn))._002Ector(npcCenter.X, base.NPC.position.Y + (float)base.NPC.height * 0.8f);
			Vector2 leviCenterAberration = npcCenter;
			float playerXDist = player.Center.X - leviCenterAberration.X;
			float playerYDist = player.Center.Y - leviCenterAberration.Y;
			float num2 = (float)Math.Sqrt(playerXDist * playerXDist + playerYDist * playerYDist);
			base.NPC.ai[1]++;
			bool spawnedAberration = false;
			float aberrationSpawnDelay = (Main.zenithWorld ? 20f : ((!sirenAlive | phase4) ? 60f : 50f));
			if (base.NPC.ai[1] > aberrationSpawnDelay)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2]++;
				spawnedAberration = true;
			}
			int spawnLimit = (Main.zenithWorld ? 20 : (Main.getGoodWorld ? 6 : ((!sirenAlive || phase4) ? 2 : 0)));
			if (spawnedAberration && NPC.CountNPCS(ModContent.NPCType<AquaticAberration>()) < spawnLimit && base.NPC.ai[2] <= (float)spawnLimit)
			{
				SoundStyle style = soundChoice with
				{
					Pitch = soundChoice.Pitch + extrapitch
				};
				SoundEngine.PlaySound(in style, npcCenter);
				if (Main.netMode != 1)
				{
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)aberrationSpawn.X, (int)aberrationSpawn.Y, ModContent.NPCType<AquaticAberration>());
				}
			}
			if (num2 > ((sirenAlive && !phase4) ? 1000f : 800f) * base.NPC.scale)
			{
				float aberrationAccel = ((sirenAlive && !phase4) ? 0.05f : 0.065f);
				aberrationAccel += 0.04f * enrageScale;
				if (expertMode && (!sirenAlive | phase4))
				{
					aberrationAccel += (death ? (0.05f * (1f - lifeRatio)) : (0.03f * (1f - lifeRatio)));
				}
				leviCenterAberration = aberrationSpawn;
				playerXDist = player.Center.X - leviCenterAberration.X;
				playerYDist = player.Center.Y - leviCenterAberration.Y;
				if (base.NPC.velocity.X < playerXDist)
				{
					base.NPC.velocity.X += aberrationAccel;
					if (base.NPC.velocity.X < 0f && playerXDist > 0f)
					{
						base.NPC.velocity.X += aberrationAccel;
					}
				}
				else if (base.NPC.velocity.X > playerXDist)
				{
					base.NPC.velocity.X -= aberrationAccel;
					if (base.NPC.velocity.X > 0f && playerXDist < 0f)
					{
						base.NPC.velocity.X -= aberrationAccel;
					}
				}
				if (base.NPC.velocity.Y < playerYDist)
				{
					base.NPC.velocity.Y += aberrationAccel;
					if (base.NPC.velocity.Y < 0f && playerYDist > 0f)
					{
						base.NPC.velocity.Y += aberrationAccel;
					}
				}
				else if (base.NPC.velocity.Y > playerYDist)
				{
					base.NPC.velocity.Y -= aberrationAccel;
					if (base.NPC.velocity.Y > 0f && playerYDist < 0f)
					{
						base.NPC.velocity.Y -= aberrationAccel;
					}
				}
			}
			else
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.9f;
			}
			float playerLocation2 = npcCenter.X - player.Center.X;
			base.NPC.direction = ((playerLocation2 < 0f) ? 1 : (-1));
			base.NPC.spriteDirection = base.NPC.direction;
			if (base.NPC.ai[2] > (float)spawnLimit)
			{
				base.NPC.ai[0] = ((!canCharge) ? 0f : (Main.rand.NextBool() ? 2f : 0f));
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 2f)
			{
				return;
			}
			Vector2 distFromPlayer = player.Center - npcCenter;
			float chargeAmt = ((!death) ? 1f : (phase4 ? 3f : (phase3 ? 2f : 1f)));
			if (base.NPC.ai[1] >= chargeAmt * 2f || ((Vector2)(ref distFromPlayer)).Length() > 2400f)
			{
				base.NPC.ai[0] = ((!canCharge) ? 0f : (Main.rand.NextBool() ? 1f : 0f));
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.TargetClosest();
				base.NPC.netUpdate = true;
				return;
			}
			float gfbchargeboost = (Main.zenithWorld ? 1100 : 0);
			float chargeDistanceY = 20f;
			float chargeDistanceX = ((sirenAlive && !phase4) ? 1100f : 900f) * base.NPC.scale + gfbchargeboost;
			chargeDistanceX -= 50f * enrageScale;
			if (!sirenAlive | phase4)
			{
				chargeDistanceX -= 250f * (1f - lifeRatio);
			}
			if (base.NPC.ai[1] % 2f == 0f)
			{
				base.NPC.damage = 0;
				int dustAmt = 7;
				for (int j = 0; j < dustAmt; j++)
				{
					Vector2 val = (Vector2.Normalize(base.NPC.velocity) * new Vector2((float)(base.NPC.width + 50) / 2f, (float)base.NPC.height) * 0.75f).RotatedBy((float)(j - (dustAmt / 2 - 1)) * (float)Math.PI / (float)dustAmt) + npcCenter;
					Vector2 dustRotation = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int waterDust = Dust.NewDust(val + dustRotation, 0, 0, 172, dustRotation.X * 2f, dustRotation.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[waterDust].noGravity = true;
					Main.dust[waterDust].noLight = true;
					Dust obj2 = Main.dust[waterDust];
					obj2.velocity /= 4f;
					Dust obj3 = Main.dust[waterDust];
					obj3.velocity -= base.NPC.velocity;
				}
				float distanceFromTargetX = Math.Abs(base.NPC.Center.X - player.Center.X);
				if (Math.Abs(base.NPC.Center.Y - player.Center.Y) < chargeDistanceY && distanceFromTargetX >= chargeDistanceX)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.ai[1]++;
					base.NPC.ai[2] = 0f;
					float lineupSpeed = (revenge ? 20f : 18f);
					lineupSpeed += 2f * enrageScale;
					if (revenge && (!sirenAlive | phase4))
					{
						lineupSpeed += (death ? (9f * (1f - lifeRatio)) : (6f * (1f - lifeRatio)));
					}
					if (Main.getGoodWorld)
					{
						lineupSpeed *= 1.15f;
					}
					Vector2 leviChargeCenter = npcCenter;
					float playerXDistCharge = player.Center.X - leviChargeCenter.X;
					float playerYDistCharge = player.Center.Y - leviChargeCenter.Y;
					float playerDistanceCharge = (float)Math.Sqrt(playerXDistCharge * playerXDistCharge + playerYDistCharge * playerYDistCharge);
					playerDistanceCharge = lineupSpeed / playerDistanceCharge;
					base.NPC.velocity.X = playerXDistCharge * playerDistanceCharge;
					base.NPC.velocity.Y = playerYDistCharge * playerDistanceCharge;
					float playerLocation3 = npcCenter.X - player.Center.X;
					base.NPC.direction = ((playerLocation3 < 0f) ? 1 : (-1));
					base.NPC.spriteDirection = base.NPC.direction;
					SoundEngine.PlaySound(RoarChargeSound with
					{
						Pitch = RoarChargeSound.Pitch + extrapitch
					}, npcCenter);
					return;
				}
				if (base.NPC.ai[2] < 300f)
				{
					base.NPC.ai[2]++;
				}
				float chargeSpeed = (revenge ? 7.5f : 6.5f) + base.NPC.ai[2] * 0.0083f;
				float chargeAcceleration = (revenge ? 0.12f : 0.11f) + base.NPC.ai[2] * 0.001f;
				chargeSpeed += 2f * enrageScale;
				chargeAcceleration += 0.04f * enrageScale;
				if (revenge && (!sirenAlive | phase4))
				{
					chargeSpeed += (death ? (9f * (1f - lifeRatio)) : (6f * (1f - lifeRatio)));
					chargeAcceleration += (death ? (0.15f * (1f - lifeRatio)) : (0.1f * (1f - lifeRatio)));
				}
				if (Main.getGoodWorld)
				{
					chargeSpeed *= 1.15f;
					chargeAcceleration *= 1.15f;
				}
				if (base.NPC.Center.Y < player.Center.Y - chargeDistanceY)
				{
					base.NPC.velocity.Y += chargeAcceleration;
				}
				else if (base.NPC.Center.Y > player.Center.Y + chargeDistanceY)
				{
					base.NPC.velocity.Y -= chargeAcceleration;
				}
				else
				{
					base.NPC.velocity.Y *= 0.7f;
				}
				if (base.NPC.velocity.Y < 0f - chargeSpeed)
				{
					base.NPC.velocity.Y = 0f - chargeSpeed;
				}
				if (base.NPC.velocity.Y > chargeSpeed)
				{
					base.NPC.velocity.Y = chargeSpeed;
				}
				float distanceXMax = 200f;
				float distanceXMin = 20f;
				if (Math.Abs(npcCenter.X - player.Center.X) > chargeDistanceX + distanceXMax)
				{
					base.NPC.velocity.X += chargeAcceleration * (float)base.NPC.direction;
				}
				else if (Math.Abs(npcCenter.X - player.Center.X) < chargeDistanceX + distanceXMin)
				{
					base.NPC.velocity.X -= chargeAcceleration * (float)base.NPC.direction;
				}
				else
				{
					base.NPC.velocity.X *= 0.7f;
				}
				if (base.NPC.velocity.X < 0f - chargeSpeed)
				{
					base.NPC.velocity.X = 0f - chargeSpeed;
				}
				if (base.NPC.velocity.X > chargeSpeed)
				{
					base.NPC.velocity.X = chargeSpeed;
				}
				float playerLocation4 = npcCenter.X - player.Center.X;
				base.NPC.direction = ((playerLocation4 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
				return;
			}
			base.NPC.damage = base.NPC.defDamage;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			int chargeXDirectSign = 1;
			if (npcCenter.X < player.Center.X)
			{
				chargeXDirectSign = -1;
			}
			if (base.NPC.direction == chargeXDirectSign && Math.Abs(npcCenter.X - player.Center.X) > chargeDistanceX)
			{
				base.NPC.ai[2] = 1f;
			}
			if (Math.Abs(base.NPC.Center.Y - player.Center.Y) > chargeDistanceX * 1.5f)
			{
				base.NPC.ai[2] = 1f;
			}
			if (base.NPC.ai[2] == 1f)
			{
				base.NPC.damage = 0;
				float playerLocation5 = npcCenter.X - player.Center.X;
				base.NPC.direction = ((playerLocation5 < 0f) ? 1 : (-1));
				base.NPC.spriteDirection = base.NPC.direction;
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.9f;
				float chargeStopSpeed = (revenge ? 0.11f : 0.1f);
				chargeStopSpeed += 0.02f * enrageScale;
				if (revenge && (!sirenAlive | phase4))
				{
					NPC nPC3 = base.NPC;
					nPC3.velocity *= (death ? MathHelper.Lerp(0.75f, 1f, lifeRatio) : MathHelper.Lerp(0.81f, 1f, lifeRatio));
					chargeStopSpeed += (death ? (0.15f * (1f - lifeRatio)) : (0.1f * (1f - lifeRatio)));
				}
				if (Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y) < chargeStopSpeed)
				{
					base.NPC.ai[2] = 0f;
					base.NPC.ai[1]++;
					base.NPC.TargetClosest();
				}
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcCenter = base.NPC.Center;
		Rectangle mouthHitbox = default(Rectangle);
		((Rectangle)(ref mouthHitbox))._002Ector((int)(npcCenter.X - (float)base.NPC.width / 2f), (int)(npcCenter.Y - (float)base.NPC.height / 4f), base.NPC.width / 4, base.NPC.height / 2);
		Rectangle bodyHitbox = default(Rectangle);
		((Rectangle)(ref bodyHitbox))._002Ector((int)(npcCenter.X - (float)base.NPC.width / 4f), (int)(npcCenter.Y - (float)base.NPC.height / 2f), base.NPC.width / 2, base.NPC.height);
		Rectangle tailHitbox = default(Rectangle);
		((Rectangle)(ref tailHitbox))._002Ector((int)(npcCenter.X + (float)base.NPC.width / 4f), (int)(npcCenter.Y - (float)base.NPC.height / 4f), base.NPC.width / 4, base.NPC.height / 2);
		Vector2 val = new Vector2((float)(mouthHitbox.X + mouthHitbox.Width / 2), (float)(mouthHitbox.Y + mouthHitbox.Height / 2));
		Vector2 bodyHitboxCenter = default(Vector2);
		((Vector2)(ref bodyHitboxCenter))._002Ector((float)(bodyHitbox.X + bodyHitbox.Width / 2), (float)(bodyHitbox.Y + bodyHitbox.Height / 2));
		Vector2 tailHitboxCenter = default(Vector2);
		((Vector2)(ref tailHitboxCenter))._002Ector((float)(tailHitbox.X + tailHitbox.Width / 2), (float)(tailHitbox.Y + tailHitbox.Height / 2));
		Rectangle targetHitbox = target.Hitbox;
		float mouthDist1 = Vector2.Distance(val, targetHitbox.TopLeft());
		float mouthDist2 = Vector2.Distance(val, targetHitbox.TopRight());
		float mouthDist3 = Vector2.Distance(val, targetHitbox.BottomLeft());
		float mouthDist4 = Vector2.Distance(val, targetHitbox.BottomRight());
		float minMouthDist = mouthDist1;
		if (mouthDist2 < minMouthDist)
		{
			minMouthDist = mouthDist2;
		}
		if (mouthDist3 < minMouthDist)
		{
			minMouthDist = mouthDist3;
		}
		if (mouthDist4 < minMouthDist)
		{
			minMouthDist = mouthDist4;
		}
		bool num = minMouthDist <= 115f * base.NPC.scale;
		float bodyDist1 = Vector2.Distance(bodyHitboxCenter, targetHitbox.TopLeft());
		float bodyDist2 = Vector2.Distance(bodyHitboxCenter, targetHitbox.TopRight());
		float bodyDist3 = Vector2.Distance(bodyHitboxCenter, targetHitbox.BottomLeft());
		float bodyDist4 = Vector2.Distance(bodyHitboxCenter, targetHitbox.BottomRight());
		float minBodyDist = bodyDist1;
		if (bodyDist2 < minBodyDist)
		{
			minBodyDist = bodyDist2;
		}
		if (bodyDist3 < minBodyDist)
		{
			minBodyDist = bodyDist3;
		}
		if (bodyDist4 < minBodyDist)
		{
			minBodyDist = bodyDist4;
		}
		bool insideBodyHitbox = minBodyDist <= 230f * base.NPC.scale;
		float tailDist1 = Vector2.Distance(tailHitboxCenter, targetHitbox.TopLeft());
		float tailDist2 = Vector2.Distance(tailHitboxCenter, targetHitbox.TopRight());
		float tailDist3 = Vector2.Distance(tailHitboxCenter, targetHitbox.BottomLeft());
		float tailDist4 = Vector2.Distance(tailHitboxCenter, targetHitbox.BottomRight());
		float minTailDist = tailDist1;
		if (tailDist2 < minTailDist)
		{
			minTailDist = tailDist2;
		}
		if (tailDist3 < minTailDist)
		{
			minTailDist = tailDist3;
		}
		if (tailDist4 < minTailDist)
		{
			minTailDist = tailDist4;
		}
		bool insideTailHitbox = minTailDist <= 115f * base.NPC.scale;
		return num | insideBodyHitbox | insideTailHitbox;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 40; i++)
		{
			int bloody = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[bloody];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[bloody].scale = 0.5f;
				Main.dust[bloody].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int bloody2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 3f);
			Main.dust[bloody2].noGravity = true;
			Dust obj2 = Main.dust[bloody2];
			obj2.velocity *= 5f;
			bloody2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[bloody2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("LeviGore").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("LeviGore2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("LeviGore3").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity * randomSpread, base.Mod.Find<ModGore>("LeviGore4").Type, base.NPC.scale);
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 499;
	}

	public static void RealOnKill(NPC npc)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (BossRushEvent.BossRushActive)
		{
			return;
		}
		CalamityGlobalNPC.SetNewBossJustDowned(npc);
		string key = "Mods.CalamityMod.Status.Progression.AbyssDropsText";
		Color messageColor = Color.RoyalBlue;
		if (!DownedBossSystem.downedLeviathan)
		{
			if (!Main.LocalPlayer.dead && Main.LocalPlayer.active)
			{
				SoundEngine.PlaySound(in CommonCalamitySounds.WyrmScreamSound, Main.LocalPlayer.Center);
			}
			CalamityUtils.BroadcastLocalizedText(key, messageColor);
		}
		DownedBossSystem.downedLeviathan = true;
		CalamityNetcode.SyncWorld();
	}

	public override void OnKill()
	{
		if (LastAnLStanding())
		{
			RealOnKill(base.NPC);
		}
	}

	public static bool LastAnLStanding()
	{
		return NPC.CountNPCS(ModContent.NPCType<Anahita>()) + NPC.CountNPCS(ModContent.NPCType<Leviathan>()) <= 1;
	}

	public static void DefineAnahitaLeviathanLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(LastAnLStanding);
		mainRule.Add(ItemDropRule.BossBag(ModContent.ItemType<LeviathanBag>()));
		LeadingConditionRule normalOnly = new LeadingConditionRule(new Conditions.NotExpert());
		mainRule.Add(normalOnly);
		int[] items = new int[7]
		{
			ModContent.ItemType<Greentide>(),
			ModContent.ItemType<Leviatitan>(),
			ModContent.ItemType<AnahitasArpeggio>(),
			ModContent.ItemType<Atlantis>(),
			ModContent.ItemType<GastricBelcherStaff>(),
			ModContent.ItemType<Whitewater>(),
			ModContent.ItemType<LeviathanTeeth>()
		};
		normalOnly.Add(DropHelper.CalamityStyle(DropHelper.NormalWeaponDropRateFraction, items));
		normalOnly.Add(ModContent.ItemType<LeviathanMask>(), 7);
		normalOnly.Add(ModContent.ItemType<AnahitaMask>(), 7);
		normalOnly.Add(ModContent.ItemType<DeepSeaAnchor>(), 10);
		normalOnly.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		normalOnly.Add(ModContent.ItemType<PearlofEnthrallment>(), DropHelper.NormalWeaponDropRateFraction);
		normalOnly.Add(ModContent.ItemType<TheCommunity>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).AddIf((DropAttemptInfo info) => LastAnLStanding(), ModContent.ItemType<LeviathanAnahitaRelic>());
		LeadingConditionRule mainRule2 = npcLoot.DefineConditionalDropSet(DropHelper.GFB);
		mainRule2.Add(DropHelper.PerPlayer(5383, 1, 1, 9999), hideLootReport: true);
		mainRule2.Add(DropHelper.PerPlayer(540, 1, 1, 9999), hideLootReport: true);
		mainRule2.Add(DropHelper.PerPlayer(4029, 1, 1, 9999), hideLootReport: true);
		mainRule2.Add(DropHelper.PerPlayer(3035), hideLootReport: true);
		npcLoot.AddConditionalPerPlayer(shouldDropLore, ModContent.ItemType<LoreAbyss>(), ui: true, DropHelper.FirstKillText);
		npcLoot.AddConditionalPerPlayer(shouldDropLore, ModContent.ItemType<LoreLeviathanAnahita>(), ui: true, DropHelper.FirstKillText);
		static bool shouldDropLore(DropAttemptInfo info)
		{
			if (!DownedBossSystem.downedLeviathan)
			{
				return LastAnLStanding();
			}
			return false;
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		DefineAnahitaLeviathanLoot(npcLoot);
		npcLoot.Add(ModContent.ItemType<LeviathanTrophy>(), 10);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = AttackTexture.Value;
		if (base.NPC.ai[0] == 1f || base.NPC.Calamity().newAI[3] < 180f)
		{
			texture = TextureAssets.Npc[base.Type].Value;
		}
		SpriteEffects spriteEffects = (SpriteEffects)1;
		float xOffset = -50f;
		if (base.NPC.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)0;
			xOffset *= -1f;
		}
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(base.NPC.frame.X, base.NPC.frame.Y, texture.Width / 2, texture.Height / 3);
		Vector2 origin = rectangle.Size() / 2f;
		spriteBatch.Draw(texture, base.NPC.Center - screenPos + new Vector2(xOffset, base.NPC.gfxOffY), (Rectangle?)rectangle, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		int width = 1011;
		int height = 486;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.2f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.3f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		if (!initialised)
		{
			counter = 3;
			base.NPC.frameCounter = 8.0;
			initialised = true;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 8.0)
		{
			base.NPC.frameCounter = 0.0;
			counter++;
			base.NPC.frame.X = ((counter >= 3) ? (width + 3) : 0);
			if (counter == 3)
			{
				base.NPC.frame.Y = 0;
			}
			else
			{
				base.NPC.frame.Y += height;
			}
		}
		if (counter == 6)
		{
			counter = 1;
			base.NPC.frame.Y = 0;
			base.NPC.frame.X = 0;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}
}
