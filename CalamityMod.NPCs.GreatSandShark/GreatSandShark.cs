using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.NPCs.Astral;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.GreatSandShark;

[AutoloadBossHead]
public class GreatSandShark : ModNPC
{
	private bool resetAI;

	public static readonly SoundStyle RoarSound = new SoundStyle("CalamityMod/Sounds/Custom/GreatSandSharkRoar");

	public static readonly SoundStyle HurtSound = new SoundStyle("CalamityMod/Sounds/NPCHit/GreatSandSharkHit");

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/GreatSandSharkDeath");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.6f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 70f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 60f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.MPAllowedEnemies[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.npcSlots = 15f;
		base.NPC.damage = 100;
		base.NPC.width = 300;
		base.NPC.height = 120;
		base.NPC.defense = 40;
		base.NPC.LifeMaxNERB(9200, 11000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 5);
		NPCID.Sets.TrailCacheLength[base.Type] = 8;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		base.NPC.behindTiles = true;
		base.NPC.netAlways = true;
		base.NPC.DeathSound = DeathSound;
		base.NPC.timeLeft = NPC.activeTime * 30;
		base.NPC.rarity = 2;
		if (Main.zenithWorld)
		{
			base.NPC.Calamity().VulnerableToHeat = true;
			base.NPC.Calamity().VulnerableToSickness = false;
		}
		else
		{
			base.NPC.Calamity().VulnerableToCold = true;
			base.NPC.Calamity().VulnerableToSickness = true;
			base.NPC.Calamity().VulnerableToWater = true;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Sandstorm,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.GreatSandShark")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(resetAI);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.localAI[1]);
		writer.Write(base.NPC.localAI[2]);
		writer.Write(base.NPC.localAI[3]);
		writer.Write(base.NPC.Calamity().newAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		resetAI = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.localAI[1] = reader.ReadSingle();
		base.NPC.localAI[2] = reader.ReadSingle();
		base.NPC.localAI[3] = reader.ReadSingle();
		base.NPC.Calamity().newAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1710: Unknown result type (might be due to invalid IL or missing references)
		//IL_171b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0edd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f36: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_1771: Unknown result type (might be due to invalid IL or missing references)
		//IL_178c: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_12bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe4: Unknown result type (might be due to invalid IL or missing references)
		//IL_100a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Unknown result type (might be due to invalid IL or missing references)
		//IL_103b: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_110b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1111: Unknown result type (might be due to invalid IL or missing references)
		//IL_114e: Unknown result type (might be due to invalid IL or missing references)
		//IL_117c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_121d: Unknown result type (might be due to invalid IL or missing references)
		//IL_123e: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		bool expertMode = Main.expertMode;
		bool revenge = CalamityWorld.revenge;
		bool death = CalamityWorld.death;
		bool lowLife = (double)base.NPC.life <= (double)base.NPC.lifeMax * (expertMode ? 0.75 : 0.5);
		bool lowerLife = (double)base.NPC.life <= (double)base.NPC.lifeMax * (expertMode ? 0.35 : 0.2);
		bool youMustDie = !Main.player[base.NPC.target].ZoneDesert;
		if (!Sandstorm.Happening)
		{
			CalamityWorld.StartSandstorm();
			CalamityNetcode.SyncWorld();
		}
		if (base.NPC.soundDelay <= 0)
		{
			base.NPC.soundDelay = 480;
			SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
		}
		if (base.NPC.localAI[3] >= 1f || Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 1000f)
		{
			if (!resetAI)
			{
				base.NPC.localAI[0] = 0f;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				resetAI = true;
				base.NPC.netUpdate = true;
			}
			int chargeTime = (expertMode ? 35 : 50);
			float chargeAcceleration = (expertMode ? 0.5f : 0.42f);
			float chargeThreshold = (expertMode ? 7.5f : 6.7f);
			int chargeDelay = (expertMode ? 28 : 30);
			float chargeVelocity = (expertMode ? 15.5f : 14f);
			if (revenge | lowerLife)
			{
				chargeAcceleration *= 1.1f;
				chargeThreshold *= 1.1f;
				chargeVelocity *= 1.1f;
			}
			if (death)
			{
				chargeAcceleration *= 1.1f;
				chargeThreshold *= 1.1f;
				chargeVelocity *= 1.1f;
				chargeDelay = 25;
			}
			if (youMustDie)
			{
				chargeAcceleration *= 1.5f;
				chargeThreshold *= 1.5f;
				chargeVelocity *= 1.5f;
				chargeDelay = 20;
			}
			Vector2 shorkCenter = base.NPC.Center;
			Player player = Main.player[base.NPC.target];
			if (base.NPC.target < 0 || base.NPC.target == 255 || player.dead || !player.active)
			{
				base.NPC.TargetClosest();
				player = Main.player[base.NPC.target];
				base.NPC.netUpdate = true;
			}
			if (player.dead || Vector2.Distance(player.Center, shorkCenter) > 5600f)
			{
				base.NPC.velocity.Y += 0.4f;
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				base.NPC.ai[0] = 0f;
				base.NPC.ai[2] = 0f;
			}
			float getRotatedIdiot = (float)Math.Atan2(player.Center.Y - shorkCenter.Y, player.Center.X - shorkCenter.X);
			if (base.NPC.spriteDirection == 1)
			{
				getRotatedIdiot += (float)Math.PI;
			}
			if (getRotatedIdiot < 0f)
			{
				getRotatedIdiot += (float)Math.PI * 2f;
			}
			if (getRotatedIdiot > (float)Math.PI * 2f)
			{
				getRotatedIdiot -= (float)Math.PI * 2f;
			}
			float rotationSpeed = 0.04f;
			if (base.NPC.ai[0] == 1f)
			{
				rotationSpeed = 0f;
			}
			if (base.NPC.rotation < getRotatedIdiot)
			{
				if ((double)(getRotatedIdiot - base.NPC.rotation) > 3.1415927410125732)
				{
					base.NPC.rotation -= rotationSpeed;
				}
				else
				{
					base.NPC.rotation += rotationSpeed;
				}
			}
			if (base.NPC.rotation > getRotatedIdiot)
			{
				if ((double)(base.NPC.rotation - getRotatedIdiot) > 3.1415927410125732)
				{
					base.NPC.rotation += rotationSpeed;
				}
				else
				{
					base.NPC.rotation -= rotationSpeed;
				}
			}
			if (base.NPC.rotation > getRotatedIdiot - rotationSpeed && base.NPC.rotation < getRotatedIdiot + rotationSpeed)
			{
				base.NPC.rotation = getRotatedIdiot;
			}
			if (base.NPC.rotation < 0f)
			{
				base.NPC.rotation += (float)Math.PI * 2f;
			}
			if (base.NPC.rotation > (float)Math.PI * 2f)
			{
				base.NPC.rotation -= (float)Math.PI * 2f;
			}
			if (base.NPC.rotation > getRotatedIdiot - rotationSpeed && base.NPC.rotation < getRotatedIdiot + rotationSpeed)
			{
				base.NPC.rotation = getRotatedIdiot;
			}
			if (base.NPC.ai[0] == 0f && !player.dead)
			{
				if (base.NPC.ai[1] == 0f)
				{
					base.NPC.ai[1] = 300 * Math.Sign((shorkCenter - player.Center).X);
				}
				Vector2 chargeDirection = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -200f) - shorkCenter - base.NPC.velocity) * chargeThreshold;
				if (base.NPC.velocity.X < chargeDirection.X)
				{
					base.NPC.velocity.X += chargeAcceleration;
					if (base.NPC.velocity.X < 0f && chargeDirection.X > 0f)
					{
						base.NPC.velocity.X += chargeAcceleration;
					}
				}
				else if (base.NPC.velocity.X > chargeDirection.X)
				{
					base.NPC.velocity.X -= chargeAcceleration;
					if (base.NPC.velocity.X > 0f && chargeDirection.X < 0f)
					{
						base.NPC.velocity.X -= chargeAcceleration;
					}
				}
				if (base.NPC.velocity.Y < chargeDirection.Y)
				{
					base.NPC.velocity.Y += chargeAcceleration;
					if (base.NPC.velocity.Y < 0f && chargeDirection.Y > 0f)
					{
						base.NPC.velocity.Y += chargeAcceleration;
					}
				}
				else if (base.NPC.velocity.Y > chargeDirection.Y)
				{
					base.NPC.velocity.Y -= chargeAcceleration;
					if (base.NPC.velocity.Y > 0f && chargeDirection.Y < 0f)
					{
						base.NPC.velocity.Y -= chargeAcceleration;
					}
				}
				int shorkFaceDirection = Math.Sign(player.Center.X - shorkCenter.X);
				if (shorkFaceDirection != 0)
				{
					if (base.NPC.ai[2] == 0f && shorkFaceDirection != base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.direction = shorkFaceDirection;
					if (base.NPC.spriteDirection != -base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= (float)chargeTime)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.velocity = Vector2.Normalize(player.Center - shorkCenter) * chargeVelocity;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (shorkFaceDirection != 0)
					{
						base.NPC.direction = shorkFaceDirection;
						if (base.NPC.spriteDirection == 1)
						{
							base.NPC.rotation += (float)Math.PI;
						}
						base.NPC.spriteDirection = -base.NPC.direction;
					}
					base.NPC.netUpdate = true;
					return;
				}
			}
			else if (base.NPC.ai[0] == 1f)
			{
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= (float)chargeDelay)
				{
					base.NPC.localAI[3]++;
					if (base.NPC.localAI[3] >= 2f)
					{
						base.NPC.localAI[3] = 0f;
					}
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.netUpdate = true;
					return;
				}
			}
		}
		else
		{
			resetAI = false;
			if (base.NPC.direction == 0)
			{
				base.NPC.TargetClosest();
			}
			Point shorkTileCenter = base.NPC.Center.ToTileCoordinates();
			Tile tileSafely = Framing.GetTileSafely(shorkTileCenter);
			bool isInSolidTile = tileSafely.HasUnactuatedTile || tileSafely.LiquidAmount > 0;
			bool shouldDoLunge = false;
			base.NPC.TargetClosest(faceTarget: false);
			Vector2 targetLungeDirection = ((Rectangle)(ref base.NPC.targetRect)).Center.ToVector2();
			if (Main.player[base.NPC.target].velocity.Y > -0.1f && !Main.player[base.NPC.target].dead && base.NPC.Distance(targetLungeDirection) > 150f)
			{
				shouldDoLunge = true;
			}
			base.NPC.localAI[1]++;
			if (lowLife)
			{
				bool spawnFlag = base.NPC.localAI[1] == 150f;
				if (NPC.CountNPCS(542) > 2)
				{
					spawnFlag = false;
				}
				if (spawnFlag && Main.netMode != 1)
				{
					int npcType = (Main.zenithWorld ? ModContent.NPCType<FusionFeeder>() : 542);
					NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y + 50, npcType);
					SoundEngine.PlaySound(in RoarSound, base.NPC.Center);
				}
			}
			if (base.NPC.localAI[1] >= 300f)
			{
				base.NPC.localAI[1] = 0f;
				if (base.NPC.localAI[2] > 0f)
				{
					base.NPC.localAI[2] = 0f;
				}
				switch (Main.rand.Next(3))
				{
				case 0:
					base.NPC.ai[3] = 0f;
					break;
				case 1:
					base.NPC.ai[3] = 1f;
					break;
				case 2:
					base.NPC.ai[3] = 2f;
					break;
				}
				int random = (lowerLife ? 5 : 9);
				if (lowLife && Main.rand.NextBool(random))
				{
					base.NPC.localAI[3] = 1f;
				}
				base.NPC.netUpdate = true;
			}
			if (base.NPC.localAI[0] == -1f && !isInSolidTile)
			{
				base.NPC.localAI[0] = 20f;
			}
			if (base.NPC.localAI[0] > 0f)
			{
				base.NPC.localAI[0]--;
			}
			if (isInSolidTile)
			{
				bool lungeShouldDecelerate = false;
				shorkTileCenter = (base.NPC.Center + new Vector2(0f, 24f)).ToTileCoordinates();
				if (Framing.GetTileSafely(shorkTileCenter.X, shorkTileCenter.Y - 2).HasUnactuatedTile)
				{
					lungeShouldDecelerate = true;
				}
				base.NPC.ai[1] = lungeShouldDecelerate.ToInt();
				if (base.NPC.ai[2] < 30f)
				{
					base.NPC.ai[2]++;
				}
				if (shouldDoLunge)
				{
					base.NPC.TargetClosest();
					base.NPC.velocity.X += (float)base.NPC.direction * 0.15f;
					base.NPC.velocity.Y += (float)base.NPC.directionY * 0.15f;
					float velocityX = 8f;
					float velocityY = 6f;
					switch ((int)base.NPC.ai[3])
					{
					case 0:
						velocityX = 10f;
						velocityY = 9f;
						break;
					case 1:
						velocityX = 14f;
						velocityY = 7f;
						break;
					case 2:
						velocityX = 8f;
						velocityY = 11f;
						break;
					}
					if (revenge | lowerLife)
					{
						velocityX *= 1.1f;
						velocityY *= 1.1f;
					}
					if (youMustDie)
					{
						velocityX *= 1.5f;
						velocityY *= 1.5f;
					}
					base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X, 0f - velocityX, velocityX);
					base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y, 0f - velocityY, velocityY);
					Vector2 center = base.NPC.Center;
					Vector2 val = base.NPC.velocity.SafeNormalize(Vector2.Zero);
					Vector2 size = base.NPC.Size;
					shorkTileCenter = (center + val * ((Vector2)(ref size)).Length() / 2f + base.NPC.velocity).ToTileCoordinates();
					if (!Framing.GetTileSafely(shorkTileCenter).HasUnactuatedTile && Math.Sign(base.NPC.velocity.X) == base.NPC.direction && ((base.NPC.Distance(targetLungeDirection) < 600f) | youMustDie) && (base.NPC.ai[2] >= 30f || base.NPC.ai[2] < 0f))
					{
						if (base.NPC.localAI[0] == 0f)
						{
							SoundEngine.PlaySound(in SoundID.NPCDeath15, base.NPC.Center);
							base.NPC.localAI[0] = -1f;
							for (int i = 0; i < 25; i++)
							{
								int burrowDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 32, 0f, 0f, 100, default(Color), 2f);
								Main.dust[burrowDust].velocity.Y *= 6f;
								Main.dust[burrowDust].velocity.X *= 3f;
								if (Main.rand.NextBool())
								{
									Main.dust[burrowDust].scale = 0.5f;
									Main.dust[burrowDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
								}
							}
							for (int j = 0; j < 50; j++)
							{
								int burrowDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 85, 0f, 0f, 100, default(Color), 3f);
								Main.dust[burrowDust2].noGravity = true;
								Main.dust[burrowDust2].velocity.Y *= 10f;
								burrowDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 268, 0f, 0f, 100, default(Color), 2f);
								Main.dust[burrowDust2].velocity.X *= 2f;
							}
							if (Main.netMode != 1)
							{
								int spawnX = base.NPC.width / 2;
								int projType = (Main.zenithWorld ? ModContent.ProjectileType<AstralMeteorProj>() : ModContent.ProjectileType<GreatSandBlast>());
								int damage = (Main.masterMode ? 25 : (Main.expertMode ? 30 : 40));
								for (int sand = 0; sand < 5; sand++)
								{
									Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X + (float)Main.rand.Next(-spawnX, spawnX), base.NPC.Center.Y, Main.rand.Next(-3, 4), Main.rand.Next(-12, -6), projType, damage, 0f, Main.myPlayer, 0f, 0f, 0f);
								}
							}
						}
						base.NPC.ai[2] = -30f;
						Vector2 upwardChargeDirection = base.NPC.SafeDirectionTo(targetLungeDirection + new Vector2(0f, -80f), -Vector2.UnitY);
						base.NPC.velocity = upwardChargeDirection * 18f;
					}
				}
				else
				{
					float decelerationXThreshold = 6f;
					base.NPC.velocity.X += (float)base.NPC.direction * 0.1f;
					if (base.NPC.velocity.X < 0f - decelerationXThreshold || base.NPC.velocity.X > decelerationXThreshold)
					{
						base.NPC.velocity.X *= 0.95f;
					}
					if (lungeShouldDecelerate)
					{
						base.NPC.ai[0] = -1f;
					}
					else
					{
						base.NPC.ai[0] = 1f;
					}
					float decelerationYThreshold = 0.06f;
					float decelerationAmt = 0.01f;
					if (base.NPC.ai[0] == -1f)
					{
						base.NPC.velocity.Y -= decelerationAmt;
						if (base.NPC.velocity.Y < 0f - decelerationYThreshold)
						{
							base.NPC.ai[0] = 1f;
						}
					}
					else
					{
						base.NPC.velocity.Y += decelerationAmt;
						if (base.NPC.velocity.Y > decelerationYThreshold)
						{
							base.NPC.ai[0] = -1f;
						}
					}
					if (base.NPC.velocity.Y > 0.4f || base.NPC.velocity.Y < -0.4f)
					{
						base.NPC.velocity.Y *= 0.95f;
					}
				}
			}
			else
			{
				if (base.NPC.velocity.Y == 0f)
				{
					if (shouldDoLunge)
					{
						base.NPC.TargetClosest();
					}
					float smallDecelerationXThreshold = 1f;
					base.NPC.velocity.X += (float)base.NPC.direction * 0.1f;
					if (base.NPC.velocity.X < 0f - smallDecelerationXThreshold || base.NPC.velocity.X > smallDecelerationXThreshold)
					{
						base.NPC.velocity.X *= 0.95f;
					}
				}
				if (base.NPC.localAI[2] == 0f)
				{
					base.NPC.localAI[2] = 1f;
					float velocityX2 = 12f;
					float velocityY2 = 12f;
					switch ((int)base.NPC.ai[3])
					{
					case 0:
						velocityX2 = 12f;
						velocityY2 = 12f;
						break;
					case 1:
						velocityX2 = 14f;
						velocityY2 = 14f;
						break;
					case 2:
						velocityX2 = 16f;
						velocityY2 = 16f;
						break;
					}
					if (revenge | lowerLife)
					{
						velocityX2 *= 1.1f;
						velocityY2 *= 1.1f;
					}
					if (youMustDie)
					{
						velocityX2 *= 1.5f;
						velocityY2 *= 1.5f;
					}
					base.NPC.velocity.Y = 0f - velocityY2;
					base.NPC.velocity.X = velocityX2 * (float)base.NPC.direction;
					base.NPC.netUpdate = true;
				}
				base.NPC.velocity.Y += 0.4f;
				if (base.NPC.velocity.Y > 10f)
				{
					base.NPC.velocity.Y = 10f;
				}
				base.NPC.ai[0] = 1f;
			}
			base.NPC.rotation = base.NPC.velocity.Y * (float)base.NPC.direction * 0.1f;
			base.NPC.rotation = MathHelper.Clamp(base.NPC.rotation, -0.1f, 0.1f);
		}
		if (!Main.zenithWorld)
		{
			return;
		}
		base.NPC.Calamity().newAI[0]++;
		if (!(base.NPC.Calamity().newAI[0] >= 120f))
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item105, Main.player[base.NPC.target].Center);
		if (Main.netMode != 1)
		{
			for (int k = 0; k < 5; k++)
			{
				float speedX = 2f + (float)Main.rand.Next(-8, 5);
				float speedY = 2f + (float)Main.rand.Next(1, 6);
				int p = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, Main.player[base.NPC.target].Center.Y - 800f, speedX, speedY, ModContent.ProjectileType<AstralFlame>(), 40, 0f, Main.myPlayer);
				if (p.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[p].timeLeft = 180;
				}
			}
		}
		base.NPC.Calamity().newAI[0] = 0f;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.localAI[3] == 0f)
		{
			base.NPC.spriteDirection = -base.NPC.direction;
		}
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Color mainAfterimageColor = base.NPC.GetAlpha(drawColor);
		Color extraAfterimageColor = Lighting.GetColor((int)((double)base.NPC.position.X + (double)base.NPC.width * 0.5) / 16, (int)(((double)base.NPC.position.Y + (double)base.NPC.height * 0.5) / 16.0));
		if (Main.zenithWorld)
		{
			mainAfterimageColor = Color.Silver;
			extraAfterimageColor = Color.Orange;
		}
		Texture2D texture2D3 = TextureAssets.Npc[base.Type].Value;
		int currentFrame = TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type];
		int y3 = currentFrame * (int)base.NPC.frameCounter;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, y3, texture2D3.Width, currentFrame);
		Vector2 halfRectSize = rectangle.Size() / 2f;
		int eightConst = 8;
		int afterimageInc = 2;
		for (int afterimageCounter = 1; (afterimageInc > 0 && afterimageCounter < eightConst) || (afterimageInc < 0 && afterimageCounter > eightConst); afterimageCounter += afterimageInc)
		{
			if (!CalamityClientConfig.Instance.Afterimages)
			{
				break;
			}
			Color alphaAfterimageColor = base.NPC.GetAlpha(extraAfterimageColor);
			float afterimagesRemaining = eightConst - afterimageCounter;
			if (afterimageInc < 0)
			{
				afterimagesRemaining = 1 - afterimageCounter;
			}
			alphaAfterimageColor *= afterimagesRemaining / ((float)NPCID.Sets.TrailCacheLength[base.Type] * 1.5f);
			Vector2 afterimagePos = base.NPC.oldPos[afterimageCounter];
			float afterimageRotation = base.NPC.rotation;
			Main.spriteBatch.Draw(texture2D3, afterimagePos + base.NPC.Size / 2f - screenPos + new Vector2(0f, base.NPC.gfxOffY), (Rectangle?)rectangle, alphaAfterimageColor, afterimageRotation + base.NPC.rotation * 0f * (float)(afterimageCounter - 1) * (0f - (float)((Enum)spriteEffects).HasFlag((Enum)(object)(SpriteEffects)1).ToDirectionInt()), halfRectSize, base.NPC.scale, spriteEffects, 0f);
		}
		SpriteEffects something = (SpriteEffects)(base.NPC.direction != 1);
		spriteBatch.Draw(texture2D3, base.NPC.Center - screenPos + new Vector2(0f, base.NPC.gfxOffY), (Rectangle?)base.NPC.frame, mainAfterimageColor, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, something, 0f);
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in HurtSound, base.NPC.Center);
		}
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 50; i++)
		{
			int burrowDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[burrowDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[burrowDust].scale = 0.5f;
				Main.dust[burrowDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 100; j++)
		{
			int burrowDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 3f);
			Main.dust[burrowDust2].noGravity = true;
			Dust obj2 = Main.dust[burrowDust2];
			obj2.velocity *= 5f;
			burrowDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[burrowDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(3783);
		npcLoot.Add(ModContent.ItemType<GrandScale>());
		npcLoot.AddIf(() => Main.expertMode, ModContent.ItemType<GrandScale>(), 3);
		npcLoot.Add(528, 2);
		npcLoot.Add(527, 2);
		npcLoot.Add(ModContent.ItemType<GreatSandSharkTrophy>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.RevAndMaster).Add(ModContent.ItemType<GreatSandSharkRelic>());
	}

	public override void OnKill()
	{
		DownedBossSystem.downedGSS = true;
		CalamityNetcode.SyncWorld();
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.8f * balance);
		base.NPC.damage = (int)((float)base.NPC.damage * 0.8f);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
		}
	}
}
