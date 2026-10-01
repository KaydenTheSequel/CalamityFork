using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class WildBumblebirb : ModNPC
{
	public override string Texture => "CalamityMod/NPCs/Bumblebirb/BumbleFolly";

	public override void SetStaticDefaults()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Position = Vector2.UnitX * 36f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 1f;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 90;
		base.NPC.width = 120;
		base.NPC.height = 80;
		base.NPC.defense = 20;
		base.NPC.LifeMaxNERB(9375, 11250, 5000);
		base.NPC.knockBackResist = 0.15f;
		base.NPC.lavaImmune = true;
		base.NPC.noTileCollide = true;
		base.NPC.noGravity = true;
		base.NPC.HitSound = SoundID.NPCHit51;
		base.NPC.DeathSound = SoundID.NPCDeath46;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<DraconicSwarmerBanner>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.WildBumblefuck")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !NPC.downedMoonlord || spawnInfo.Player.Calamity().ZoneSunkenSea || !spawnInfo.Player.ZoneJungle)
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		return SpawnCondition.SurfaceJungle.Chance * 0.14f;
	}

	public override void AI()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_0867: Unknown result type (might be due to invalid IL or missing references)
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.NPC.target];
		float rotationMult = 4f;
		float rotationAmt = 0.04f;
		if (Vector2.Distance(obj.Center, base.NPC.Center) > 5600f && base.NPC.timeLeft > 5)
		{
			base.NPC.timeLeft = 5;
		}
		base.NPC.rotation = (base.NPC.rotation * rotationMult + base.NPC.velocity.X * rotationAmt * 1.25f) / 10f;
		if (base.NPC.ai[0] == 0f || base.NPC.ai[0] == 1f)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (i != base.NPC.whoAmI && Main.npc[i].active && Main.npc[i].type == base.NPC.type)
				{
					Vector2 otherSwarmerDirection = Main.npc[i].Center - base.NPC.Center;
					if (((Vector2)(ref otherSwarmerDirection)).Length() < (float)(base.NPC.width + base.NPC.height))
					{
						((Vector2)(ref otherSwarmerDirection)).Normalize();
						otherSwarmerDirection *= -0.1f;
						NPC nPC = base.NPC;
						nPC.velocity += otherSwarmerDirection;
						NPC obj2 = Main.npc[i];
						obj2.velocity -= otherSwarmerDirection;
					}
				}
			}
		}
		if (base.NPC.target < 0 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
			Vector2 swarmerTargetDist = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (Main.player[base.NPC.target].dead || ((Vector2)(ref swarmerTargetDist)).Length() > 2800f)
			{
				base.NPC.ai[0] = -1f;
			}
		}
		else
		{
			Vector2 swarmerCatchUpTargetDist = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (base.NPC.ai[0] > 1f && ((Vector2)(ref swarmerCatchUpTargetDist)).Length() > 3600f)
			{
				base.NPC.ai[0] = 1f;
			}
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.damage = 0;
			Vector2 swarmerDespawnVelMult = default(Vector2);
			((Vector2)(ref swarmerDespawnVelMult))._002Ector(0f, -8f);
			base.NPC.velocity = (base.NPC.velocity * 21f + swarmerDespawnVelMult) / 10f;
		}
		else if (base.NPC.ai[0] == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.TargetClosest();
			base.NPC.spriteDirection = base.NPC.direction;
			Vector2 swarmerIdleTargetDist = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (((Vector2)(ref swarmerIdleTargetDist)).Length() > 2800f)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
			}
			else if (((Vector2)(ref swarmerIdleTargetDist)).Length() > 400f)
			{
				float swarmerIdleSpeed = 7f + ((Vector2)(ref swarmerIdleTargetDist)).Length() / 100f + base.NPC.ai[1] / 15f;
				((Vector2)(ref swarmerIdleTargetDist)).Normalize();
				swarmerIdleTargetDist *= swarmerIdleSpeed;
				base.NPC.velocity = (base.NPC.velocity * 29f + swarmerIdleTargetDist) / 30f;
			}
			else if (((Vector2)(ref base.NPC.velocity)).Length() > 2f)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.95f;
			}
			else if (((Vector2)(ref base.NPC.velocity)).Length() < 1f)
			{
				NPC nPC3 = base.NPC;
				nPC3.velocity *= 1.05f;
			}
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 105f)
			{
				base.NPC.ai[1] = 0f;
				base.NPC.ai[0] = 2f;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			if (base.NPC.target < 0 || !Main.player[base.NPC.target].active || Main.player[base.NPC.target].dead)
			{
				base.NPC.TargetClosest();
			}
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = (base.NPC.rotation * rotationMult + base.NPC.velocity.X * rotationAmt) / 10f;
			Vector2 swarmerChargeTargetDist = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (((Vector2)(ref swarmerChargeTargetDist)).Length() < 800f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.ai[0] = 0f;
				base.NPC.ai[1] = 0f;
				base.NPC.ai[2] = 0f;
				base.NPC.ai[3] = 0f;
			}
			base.NPC.ai[2] += 1f / 60f;
			float swarmerChargeSpeed = 9f + base.NPC.ai[2] + ((Vector2)(ref swarmerChargeTargetDist)).Length() / 150f;
			float swarmerChargeVelMult = 25f;
			((Vector2)(ref swarmerChargeTargetDist)).Normalize();
			swarmerChargeTargetDist *= swarmerChargeSpeed;
			base.NPC.velocity = (base.NPC.velocity * (swarmerChargeVelMult - 1f) + swarmerChargeTargetDist) / swarmerChargeVelMult;
			base.NPC.ForceNetUpdate();
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.damage = 0;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.rotation = (base.NPC.rotation * rotationMult * 0.75f + base.NPC.velocity.X * rotationAmt * 1.25f) / 8f;
			Vector2 swarmerDecelerateTargetDist = Main.player[base.NPC.target].Center - base.NPC.Center;
			swarmerDecelerateTargetDist.Y -= 8f;
			float swarmerDecelerateSpeed = 14f;
			float swarmerDecelerateVelMult = 8f;
			((Vector2)(ref swarmerDecelerateTargetDist)).Normalize();
			swarmerDecelerateTargetDist *= swarmerDecelerateSpeed;
			base.NPC.velocity = (base.NPC.velocity * (swarmerDecelerateVelMult - 1f) + swarmerDecelerateTargetDist) / swarmerDecelerateVelMult;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] > 10f)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.velocity = swarmerDecelerateTargetDist;
				if (base.NPC.velocity.X < 0f)
				{
					base.NPC.direction = -1;
				}
				else
				{
					base.NPC.direction = 1;
				}
				base.NPC.ai[0] = 2.1f;
				base.NPC.ai[1] = 0f;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 2.1f)
			{
				return;
			}
			base.NPC.damage = base.NPC.defDamage;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.direction = -1;
			}
			else if (base.NPC.velocity.X > 0f)
			{
				base.NPC.direction = 1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			NPC nPC4 = base.NPC;
			nPC4.velocity *= 1.01f;
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] > 30f)
			{
				if (!Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					base.NPC.ai[0] = 0f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
				}
				else if (base.NPC.ai[1] > 60f)
				{
					base.NPC.ai[0] = 1f;
					base.NPC.ai[1] = 0f;
					base.NPC.ai[2] = 0f;
				}
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<EffulgentFeather>(), 1, 5, 7);
	}

	public override void OnKill()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.zenithWorld)
		{
			return;
		}
		SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, base.NPC.Center - Vector2.UnitY * 300f);
		if (Main.netMode != 1)
		{
			Vector2 fireFrom = default(Vector2);
			for (int i = 0; i < 5; i++)
			{
				((Vector2)(ref fireFrom))._002Ector(base.NPC.Center.X + (float)(40 * i) - 120f, base.NPC.Center.Y - 900f);
				Vector2 ai0 = base.NPC.Center - fireFrom;
				float ai1 = Main.rand.Next(100);
				Vector2 velocity = Vector2.Normalize(ai0.RotatedByRandom(0.7853981852531433)) * 7f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), fireFrom.X, fireFrom.Y, velocity.X, velocity.Y, ModContent.ProjectileType<RedLightning>(), 45, 0f, Main.myPlayer, ai0.ToRotation(), ai1);
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += ((base.NPC.ai[0] == 2.1f) ? 1.5 : 1.0);
		if (Main.zenithWorld)
		{
			base.NPC.frameCounter += 2.0;
		}
		if (base.NPC.frameCounter > 4.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
		}
		if (base.NPC.frame.Y >= frameHeight * 4)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		int afterimageAmt = ((base.NPC.ai[0] == 2.1f) ? 7 : 0);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, Color.Gold, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
			}
		}
	}
}
