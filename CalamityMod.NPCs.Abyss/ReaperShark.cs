using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Potions;
using CalamityMod.Items.Weapons.Rogue;
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

namespace CalamityMod.NPCs.Abyss;

public class ReaperShark : ModNPC
{
	public static Asset<Texture2D> ManTexture;

	public static readonly SoundStyle SearchRoarSound = new SoundStyle("CalamityMod/Sounds/Custom/ReaperSearchRoar");

	public static readonly SoundStyle EnragedRoarSound = new SoundStyle("CalamityMod/Sounds/Custom/ReaperEnragedRoar");

	public bool hasBeenHit;

	public bool reset;

	public bool reset2;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		if (!Main.dedServ)
		{
			ManTexture = ModContent.Request<Texture2D>(Texture + "Man", (AssetRequestMode)1);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.npcSlots = 6f;
		base.NPC.noGravity = true;
		base.NPC.lavaImmune = true;
		base.NPC.damage = 160;
		base.NPC.width = 280;
		base.NPC.height = 150;
		base.NPC.defense = 70;
		base.NPC.lifeMax = 100000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.timeLeft = NPC.activeTime * 30;
		base.NPC.value = Item.buyPrice(0, 25);
		base.NPC.HitSound = SoundID.NPCHit56;
		base.NPC.DeathSound = SoundID.NPCDeath60;
		base.NPC.knockBackResist = 0f;
		base.NPC.rarity = 2;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ReaperSharkBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer4Biome>().Type };
		if (Main.zenithWorld)
		{
			base.NPC.height = (int)((float)base.NPC.height * 1.5f);
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ReaperShark")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(reset);
		writer.Write(reset2);
		writer.Write(hasBeenHit);
		writer.Write(base.NPC.localAI[0]);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		reset = reader.ReadBoolean();
		reset2 = reader.ReadBoolean();
		hasBeenHit = reader.ReadBoolean();
		base.NPC.localAI[0] = reader.ReadSingle();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e20: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1270: Unknown result type (might be due to invalid IL or missing references)
		//IL_127a: Unknown result type (might be due to invalid IL or missing references)
		//IL_127f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1286: Unknown result type (might be due to invalid IL or missing references)
		//IL_1290: Unknown result type (might be due to invalid IL or missing references)
		//IL_130c: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1604: Unknown result type (might be due to invalid IL or missing references)
		//IL_1609: Unknown result type (might be due to invalid IL or missing references)
		//IL_160e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1615: Unknown result type (might be due to invalid IL or missing references)
		//IL_161a: Unknown result type (might be due to invalid IL or missing references)
		//IL_162c: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_15bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1abf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1afa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b70: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b83: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b93: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_16bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_173f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1672: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_13cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13de: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1404: Unknown result type (might be due to invalid IL or missing references)
		//IL_140a: Unknown result type (might be due to invalid IL or missing references)
		//IL_140c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1417: Unknown result type (might be due to invalid IL or missing references)
		//IL_141c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1421: Unknown result type (might be due to invalid IL or missing references)
		//IL_1428: Unknown result type (might be due to invalid IL or missing references)
		//IL_142d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1432: Unknown result type (might be due to invalid IL or missing references)
		//IL_1434: Unknown result type (might be due to invalid IL or missing references)
		//IL_1436: Unknown result type (might be due to invalid IL or missing references)
		//IL_1442: Unknown result type (might be due to invalid IL or missing references)
		//IL_144f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1460: Unknown result type (might be due to invalid IL or missing references)
		//IL_1466: Unknown result type (might be due to invalid IL or missing references)
		//IL_1498: Unknown result type (might be due to invalid IL or missing references)
		//IL_149a: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1844: Unknown result type (might be due to invalid IL or missing references)
		//IL_184e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1785: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1811: Unknown result type (might be due to invalid IL or missing references)
		//IL_1977: Unknown result type (might be due to invalid IL or missing references)
		//IL_197c: Unknown result type (might be due to invalid IL or missing references)
		//IL_197e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1983: Unknown result type (might be due to invalid IL or missing references)
		//IL_198a: Unknown result type (might be due to invalid IL or missing references)
		//IL_198f: Unknown result type (might be due to invalid IL or missing references)
		bool phase1 = (double)base.NPC.life > (double)base.NPC.lifeMax * 0.5;
		bool phase2 = (double)base.NPC.life <= (double)base.NPC.lifeMax * 0.5;
		bool phase3 = (double)base.NPC.life <= (double)base.NPC.lifeMax * 0.1;
		base.NPC.chaseable = hasBeenHit;
		if (base.NPC.soundDelay <= 0)
		{
			base.NPC.soundDelay = 360;
			if (hasBeenHit)
			{
				float pitch = (phase3 ? (-0.4f) : 0f);
				SoundEngine.PlaySound(EnragedRoarSound with
				{
					Pitch = pitch
				}, base.NPC.Center);
			}
			else
			{
				SoundEngine.PlaySound(in SearchRoarSound, base.NPC.Center);
			}
		}
		Vector2 center;
		bool num;
		if (phase3 | phase1)
		{
			if (!reset2 & phase3)
			{
				base.NPC.damage = 0;
				base.NPC.noTileCollide = true;
				base.NPC.netAlways = true;
				base.NPC.localAI[0] = 0f;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = -16f;
				base.NPC.ai[3] = 0f;
				reset2 = true;
				base.NPC.netUpdate = true;
			}
			base.NPC.spriteDirection = ((base.NPC.direction <= 0) ? 1 : (-1));
			if (base.NPC.ai[2] == 0f)
			{
				base.NPC.damage = 0;
				base.NPC.TargetClosest();
				if (!Main.player[base.NPC.target].dead)
				{
					center = Main.player[base.NPC.target].Center - base.NPC.Center;
					if (((Vector2)(ref center)).Length() < 170f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.ai[2] = -16f;
					}
				}
				if (base.NPC.justHit || base.NPC.localAI[0] >= 420f)
				{
					base.NPC.ai[2] = -16f;
				}
				return;
			}
			if (base.NPC.ai[2] < 0f)
			{
				base.NPC.damage = 0;
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] == 0f)
				{
					base.NPC.ai[2] = 1f;
					base.NPC.velocity.X = base.NPC.direction * 2;
				}
				return;
			}
			if (base.NPC.ai[2] == 1f)
			{
				if (base.NPC.direction == 0)
				{
					base.NPC.TargetClosest();
				}
				if (base.NPC.wet || base.NPC.noTileCollide)
				{
					num = hasBeenHit;
					base.NPC.TargetClosest(faceTarget: false);
					if (!Main.player[base.NPC.target].dead && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						center = Main.player[base.NPC.target].Center - base.NPC.Center;
						if (((Vector2)(ref center)).Length() < Main.player[base.NPC.target].Calamity().GetAbyssAggro(360f))
						{
							goto IL_04b9;
						}
					}
					if (base.NPC.justHit)
					{
						goto IL_04b9;
					}
					goto IL_04c0;
				}
				base.NPC.damage = base.NPC.defDamage;
				if (base.NPC.velocity.Y == 0f && Main.netMode != 1)
				{
					base.NPC.velocity.Y = (float)Main.rand.Next(-250, -180) * 0.1f;
					base.NPC.velocity.X = (float)Main.rand.Next(-50, 50) * 0.1f;
					base.NPC.netUpdate = true;
				}
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.4f;
				if (base.NPC.velocity.Y > 16f)
				{
					base.NPC.velocity.Y = 16f;
				}
				base.NPC.ai[0] = 1f;
				goto IL_0cbd;
			}
		}
		else if (phase2)
		{
			if (!reset)
			{
				base.NPC.noTileCollide = true;
				base.NPC.netAlways = true;
				base.NPC.localAI[0] = 0f;
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
				reset = true;
				base.NPC.netUpdate = true;
			}
			bool expertMode = Main.expertMode;
			float chargeAcceleration = (expertMode ? 0.4f : 0.35f);
			float chargeThreshold = (expertMode ? 6f : 5.5f);
			int phase2Delay = (expertMode ? 28 : 30);
			float chargeVelocity = (expertMode ? 12f : 11f);
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
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.4f;
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				base.NPC.ai[0] = 0f;
				base.NPC.ai[2] = 0f;
			}
			if (base.NPC.localAI[0] == 0f)
			{
				base.NPC.localAI[0] = 1f;
				base.NPC.alpha = 255;
				base.NPC.rotation = 0f;
				if (Main.netMode != 1)
				{
					base.NPC.ai[0] = -1f;
					base.NPC.netUpdate = true;
				}
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
			if (base.NPC.ai[0] == -1f)
			{
				getRotatedIdiot = 0f;
			}
			float rotationSpeed = 0.04f;
			if (base.NPC.ai[0] == 1f)
			{
				rotationSpeed = 0f;
			}
			if (base.NPC.rotation < getRotatedIdiot)
			{
				if ((double)(getRotatedIdiot - base.NPC.rotation) > Math.PI)
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
				if ((double)(base.NPC.rotation - getRotatedIdiot) > Math.PI)
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
			if (base.NPC.ai[0] != -1f)
			{
				if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.alpha += 15;
				}
				else
				{
					base.NPC.alpha -= 15;
				}
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
				if (base.NPC.alpha > 150)
				{
					base.NPC.alpha = 150;
				}
			}
			if (base.NPC.ai[0] == -1f)
			{
				base.NPC.damage = 0;
				base.NPC.dontTakeDamage = true;
				base.NPC.chaseable = false;
				NPC nPC = base.NPC;
				nPC.velocity *= 0.98f;
				int spriteFaceDirection = Math.Sign(player.Center.X - shorkCenter.X);
				if (spriteFaceDirection != 0)
				{
					base.NPC.direction = spriteFaceDirection;
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				if (base.NPC.ai[2] > 20f)
				{
					base.NPC.velocity.Y = -2f;
					base.NPC.alpha -= 5;
					if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
					{
						base.NPC.alpha += 15;
					}
					if (base.NPC.alpha < 0)
					{
						base.NPC.alpha = 0;
					}
					if (base.NPC.alpha > 150)
					{
						base.NPC.alpha = 150;
					}
				}
				if (base.NPC.ai[2] == 60f)
				{
					int dustAmt = 36;
					for (int i = 0; i < dustAmt; i++)
					{
						Vector2 spinningpoint = Vector2.Normalize(base.NPC.velocity) * new Vector2((float)base.NPC.width / 2f, (float)base.NPC.height) * 0.75f * 0.5f;
						double radians = (float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt;
						center = default(Vector2);
						Vector2 val = spinningpoint.RotatedBy(radians, center) + base.NPC.Center;
						Vector2 dustDirection = val - base.NPC.Center;
						int chargeDust = Dust.NewDust(val + dustDirection, 0, 0, 172, dustDirection.X * 2f, dustDirection.Y * 2f, 100, default(Color), 1.4f);
						Main.dust[chargeDust].noGravity = true;
						Main.dust[chargeDust].noLight = true;
						Main.dust[chargeDust].velocity = Vector2.Normalize(dustDirection) * 3f;
					}
					SoundEngine.PlaySound(in EnragedRoarSound, base.NPC.Center);
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 75f)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.netUpdate = true;
					return;
				}
			}
			else if (base.NPC.ai[0] == 0f && !player.dead)
			{
				base.NPC.damage = 0;
				base.NPC.dontTakeDamage = false;
				base.NPC.chaseable = true;
				if (base.NPC.ai[1] == 0f)
				{
					base.NPC.ai[1] = 300 * Math.Sign((shorkCenter - player.Center).X);
				}
				Vector2 chargeDirection = Vector2.Normalize(player.Center + new Vector2(base.NPC.ai[1], -200f) - shorkCenter - base.NPC.velocity) * chargeThreshold;
				if (base.NPC.velocity.X < chargeDirection.X)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + chargeAcceleration;
					if (base.NPC.velocity.X < 0f && chargeDirection.X > 0f)
					{
						base.NPC.velocity.X = base.NPC.velocity.X + chargeAcceleration;
					}
				}
				else if (base.NPC.velocity.X > chargeDirection.X)
				{
					base.NPC.velocity.X = base.NPC.velocity.X - chargeAcceleration;
					if (base.NPC.velocity.X > 0f && chargeDirection.X < 0f)
					{
						base.NPC.velocity.X = base.NPC.velocity.X - chargeAcceleration;
					}
				}
				if (base.NPC.velocity.Y < chargeDirection.Y)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + chargeAcceleration;
					if (base.NPC.velocity.Y < 0f && chargeDirection.Y > 0f)
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y + chargeAcceleration;
					}
				}
				else if (base.NPC.velocity.Y > chargeDirection.Y)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - chargeAcceleration;
					if (base.NPC.velocity.Y > 0f && chargeDirection.Y < 0f)
					{
						base.NPC.velocity.Y = base.NPC.velocity.Y - chargeAcceleration;
					}
				}
				int shorkFacingSign = Math.Sign(player.Center.X - shorkCenter.X);
				if (shorkFacingSign != 0)
				{
					if (base.NPC.ai[2] == 0f && shorkFacingSign != base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.direction = shorkFacingSign;
					if (base.NPC.spriteDirection != -base.NPC.direction)
					{
						base.NPC.rotation += (float)Math.PI;
					}
					base.NPC.spriteDirection = -base.NPC.direction;
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= 30f)
				{
					base.NPC.damage = base.NPC.defDamage;
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.velocity = Vector2.Normalize(player.Center - shorkCenter) * chargeVelocity;
					base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
					if (shorkFacingSign != 0)
					{
						base.NPC.direction = shorkFacingSign;
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
				base.NPC.damage = base.NPC.defDamage;
				int phase2DustAmt = 7;
				for (int j = 0; j < phase2DustAmt; j++)
				{
					Vector2 spinningpoint2 = Vector2.Normalize(base.NPC.velocity) * new Vector2((float)(base.NPC.width + 50) / 2f, (float)base.NPC.height) * 0.75f;
					double radians2 = (double)(j - (phase2DustAmt / 2 - 1)) * Math.PI / (double)(float)phase2DustAmt;
					center = default(Vector2);
					Vector2 val2 = spinningpoint2.RotatedBy(radians2, center) + shorkCenter;
					Vector2 phase2DustRotation = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
					int phase2Dust = Dust.NewDust(val2 + phase2DustRotation, 0, 0, 172, phase2DustRotation.X * 2f, phase2DustRotation.Y * 2f, 100, default(Color), 1.4f);
					Main.dust[phase2Dust].noGravity = true;
					Main.dust[phase2Dust].noLight = true;
					Dust obj = Main.dust[phase2Dust];
					obj.velocity /= 4f;
					Dust obj2 = Main.dust[phase2Dust];
					obj2.velocity -= base.NPC.velocity;
				}
				base.NPC.ai[2]++;
				if (base.NPC.ai[2] >= (float)phase2Delay)
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
					base.NPC.netUpdate = true;
					return;
				}
				if (Main.zenithWorld && Main.netMode != 1 && base.NPC.ai[2] % 5f == 0f)
				{
					Vector2 direction = shorkCenter - player.Center;
					((Vector2)(ref direction)).Normalize();
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, direction * 10f, 44, 80, 0f, Main.myPlayer);
				}
			}
		}
		goto IL_1ca1;
		IL_1ca1:
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 6400f)
		{
			base.NPC.active = false;
		}
		return;
		IL_04c0:
		if (!num)
		{
			base.NPC.damage = 0;
			if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.noTileCollide = false;
			}
			if (base.NPC.collideX)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * -1f;
				base.NPC.direction *= -1;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.collideY)
			{
				base.NPC.netUpdate = true;
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
					base.NPC.directionY = -1;
					base.NPC.ai[0] = -1f;
				}
				else if (base.NPC.velocity.Y < 0f)
				{
					base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
					base.NPC.directionY = 1;
					base.NPC.ai[0] = 1f;
				}
			}
		}
		if (num)
		{
			base.NPC.damage = (phase3 ? ((int)Math.Round((double)base.NPC.defDamage * 0.5)) : base.NPC.defDamage);
			if (base.NPC.ai[3] > 0f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
				{
					base.NPC.ai[3] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.netUpdate = true;
				}
			}
			else if (base.NPC.ai[3] == 0f)
			{
				base.NPC.ai[1]++;
			}
			if (base.NPC.ai[1] >= 90f)
			{
				base.NPC.ai[3] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.netUpdate = true;
			}
			if (base.NPC.ai[3] == 0f)
			{
				base.NPC.noTileCollide = false;
			}
			else
			{
				base.NPC.noTileCollide = true;
			}
			base.NPC.TargetClosest();
			base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.3f;
			base.NPC.velocity.Y = base.NPC.velocity.Y + (float)base.NPC.directionY * 0.2f;
			float speedX = (phase3 ? 3f : 12f);
			float speedY = (phase3 ? 2.25f : 9f);
			if (base.NPC.velocity.X > speedX)
			{
				base.NPC.velocity.X = speedX;
			}
			if (base.NPC.velocity.X < 0f - speedX)
			{
				base.NPC.velocity.X = 0f - speedX;
			}
			if (base.NPC.velocity.Y > speedY)
			{
				base.NPC.velocity.Y = speedY;
			}
			if (base.NPC.velocity.Y < 0f - speedY)
			{
				base.NPC.velocity.Y = 0f - speedY;
			}
		}
		else
		{
			if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.noTileCollide = false;
			}
			base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.2f;
			if (base.NPC.velocity.X < -4f || base.NPC.velocity.X > 4f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
			}
			if (base.NPC.ai[0] == -1f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.01f;
				if ((double)base.NPC.velocity.Y < -0.3)
				{
					base.NPC.ai[0] = 1f;
				}
			}
			else
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.01f;
				if ((double)base.NPC.velocity.Y > 0.3)
				{
					base.NPC.ai[0] = -1f;
				}
			}
		}
		int npcTileX = (int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16;
		int npcTileY = (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16;
		if (Main.tile[npcTileX, npcTileY - 1].LiquidAmount > 128)
		{
			if (Main.tile[npcTileX, npcTileY + 1].HasTile)
			{
				base.NPC.ai[0] = -1f;
			}
			else if (Main.tile[npcTileX, npcTileY + 2].HasTile)
			{
				base.NPC.ai[0] = -1f;
			}
		}
		if ((double)base.NPC.velocity.Y > 0.4 || (double)base.NPC.velocity.Y < -0.4)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y * 0.95f;
		}
		goto IL_0cbd;
		IL_0cbd:
		base.NPC.rotation = base.NPC.velocity.Y * (float)base.NPC.direction * 0.1f;
		if ((double)base.NPC.rotation < -0.2)
		{
			base.NPC.rotation = -0.2f;
		}
		if ((double)base.NPC.rotation > 0.2)
		{
			base.NPC.rotation = 0.2f;
			return;
		}
		goto IL_1ca1;
		IL_04b9:
		hasBeenHit = true;
		goto IL_04c0;
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
		int newFrameHeight = (int)((float)frameHeight * (Main.zenithWorld ? 1.5f : 1f));
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.3f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 54f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -10f;
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 12f;
		value.Position.Y -= 50f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		base.NPC.frameCounter += ((hasBeenHit || base.NPC.IsABestiaryIconDummy) ? 0.15f : 0.075f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * newFrameHeight;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Asset<Texture2D> npcTexture = (Main.zenithWorld ? ManTexture : TextureAssets.Npc[base.Type]);
		Rectangle nframe = npcTexture.Frame(1, 4, 0, (int)base.NPC.frameCounter);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(npcTexture.Value.Width / 2), (float)(npcTexture.Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 npcOffset = base.NPC.Center - screenPos;
		npcOffset -= new Vector2((float)npcTexture.Value.Width, (float)(npcTexture.Value.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		npcOffset += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(npcTexture.Value, npcOffset, (Rectangle?)nframe, base.NPC.GetAlpha(drawColor), base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 300);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer4 && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<ReaperShark>()))
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 1.2f;
			}
			return 10.8f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<Voidstone>(), 1, 40, 50);
		npcLoot.Add(ModContent.ItemType<AnechoicCoating>(), 1, 2, 3);
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.PostPolter());
		mainRule.Add(ModContent.ItemType<ReaperTooth>(), 1, 6, 8);
		mainRule.Add(ModContent.ItemType<DeepSeaDumbbell>(), 3);
		mainRule.Add(ModContent.ItemType<Valediction>(), 3);
		npcLoot.DefineConditionalDropSet(DropHelper.PostLevi()).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 10, 17, 14, 22));
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}
}
