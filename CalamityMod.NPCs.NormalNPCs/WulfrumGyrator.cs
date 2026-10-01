using System;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class WulfrumGyrator : ModNPC
{
	public const float PlayerTargetingThreshold = 90f;

	public const float PlayerSearchDistance = 500f;

	public const float StuckJumpPromptTime = 45f;

	public const float MaxMovementSpeedX = 6f;

	public const float JumpSpeed = -8f;

	public const float NPCGravity = 0.3f;

	public float TimeSpentStuck
	{
		get
		{
			return base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = value;
		}
	}

	public float SuperchargeTimer
	{
		get
		{
			return base.NPC.ai[3];
		}
		set
		{
			base.NPC.ai[3] = value;
		}
	}

	public bool Supercharged => SuperchargeTimer > 0f;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 10;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.AIType = -1;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 15;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.defense = 5;
		base.NPC.lifeMax = 25;
		base.NPC.knockBackResist = 0.15f;
		base.NPC.value = Item.buyPrice(0, 0, 0, 75);
		base.NPC.HitSound = WulfrumAmplifier.Hit;
		base.NPC.DeathSound = CommonCalamitySounds.WulfrumNPCDeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<WulfrumGyratorBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.WulfrumGyrator")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (Main.zenithWorld)
		{
			base.NPC.frameCounter++;
		}
		int frame = (int)(base.NPC.frameCounter / 5.0) % (Main.npcFrameCount[base.Type] / 2);
		if (Supercharged)
		{
			frame += Main.npcFrameCount[base.Type] / 2;
		}
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(Main.zenithWorld);
		Player player = Main.player[base.NPC.target];
		if (Supercharged)
		{
			float chargeJumpSpeed = -12f;
			float maxHeight = chargeJumpSpeed * chargeJumpSpeed * (float)Math.Pow(Math.Sin(base.NPC.AngleTo(player.Center)), 2.0) / 1.2f;
			bool jumpWouldHitPlayer = maxHeight > Math.Abs(player.Center.Y - base.NPC.Center.Y) && maxHeight < Math.Abs(player.Center.Y - base.NPC.Center.Y) + (float)player.height;
			if (((Main.netMode != 1) & jumpWouldHitPlayer) && base.NPC.collideY && base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.Y = chargeJumpSpeed;
				base.NPC.ForceNetUpdate();
			}
			SuperchargeTimer--;
		}
		if (Main.netMode != 1 && HoleAtPosition(base.NPC.Center.X + base.NPC.velocity.X * 4f) && base.NPC.collideY && base.NPC.velocity.Y == 0f)
		{
			base.NPC.velocity.Y = -8f;
			base.NPC.ForceNetUpdate();
		}
		if (Collision.CanHitLine(player.position, player.width, player.height, base.NPC.position, base.NPC.width, base.NPC.height) && Math.Abs(player.Center.X - base.NPC.Center.X) < 500f && Math.Abs(player.Center.X - base.NPC.Center.X) > 90f)
		{
			int direction = Math.Sign(player.Center.X - base.NPC.Center.X) * ((!base.NPC.confused) ? 1 : (-1));
			if (base.NPC.direction != direction)
			{
				base.NPC.direction = direction;
				base.NPC.ForceNetUpdate();
			}
			if (Main.getGoodWorld)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 1.01f;
				if (Supercharged && Main.rand.NextBool(3))
				{
					int spark = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitX * 6f * (float)base.NPC.spriteDirection, Vector2.Zero, ModContent.ProjectileType<EGloveSpark>(), 10, 0f);
					if (spark.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[spark].friendly = false;
						Main.projectile[spark].hostile = true;
						Main.projectile[spark].timeLeft = 60;
					}
				}
			}
		}
		else if (Main.netMode != 1 && base.NPC.collideX && base.NPC.collideY && base.NPC.velocity.Y == 0f)
		{
			base.NPC.velocity.Y = -8f;
			base.NPC.ForceNetUpdate();
		}
		if (base.NPC.oldPosition == base.NPC.position)
		{
			TimeSpentStuck++;
			if (Main.netMode != 1 && TimeSpentStuck > 45f)
			{
				base.NPC.velocity.Y = -8f;
				TimeSpentStuck = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			TimeSpentStuck = 0f;
		}
		base.NPC.velocity.X = MathHelper.Lerp(base.NPC.velocity.X, 6f * (float)base.NPC.direction * (Supercharged ? 1.25f : 1f), Supercharged ? 0.02f : 0.0125f);
		Vector4 adjustedVectors = Collision.WalkDownSlope(base.NPC.position, base.NPC.velocity, base.NPC.width, base.NPC.height, 0.3f);
		base.NPC.position = adjustedVectors.XY();
		base.NPC.velocity = adjustedVectors.ZW();
	}

	private bool HoleAtPosition(float xPosition)
	{
		int tileWidth = base.NPC.width / 16;
		xPosition = (int)(xPosition / 16f) - tileWidth;
		if (base.NPC.velocity.X > 0f)
		{
			xPosition += (float)tileWidth;
		}
		int tileY = (int)((base.NPC.position.Y + (float)base.NPC.height) / 16f);
		for (int y = tileY; y < tileY + 2; y++)
		{
			for (int x = (int)xPosition; (float)x < xPosition + (float)tileWidth; x++)
			{
				if (Main.tile[x, y].HasTile)
				{
					return false;
				}
			}
		}
		return true;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().ZoneSulphur || (!spawnInfo.Player.ZoneOverworldHeight && !Main.remixWorld) || (!spawnInfo.Player.ZoneNormalCaverns && spawnInfo.Player.ZoneGlowshroom && Main.remixWorld))
		{
			return 0f;
		}
		return (Main.remixWorld ? SpawnCondition.Cavern.Chance : SpawnCondition.OverworldDaySlime.Chance) * (Main.hardMode ? 0.055f : 0.135f) * (NPC.AnyNPCs(ModContent.NPCType<WulfrumAmplifier>()) ? 5.5f : 1f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 3, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 20; i++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 3, hit.HitDirection, -1f);
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumGyratorGore").Type);
			int randomGoreCount = Main.rand.Next(1, 4);
			for (int j = 0; j < randomGoreCount; j++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumEnemyGore" + Main.rand.Next(1, 11)).Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<WulfrumMetalScrap>(), 1, 1, 2);
		npcLoot.Add(ModContent.ItemType<WulfrumBattery>(), new Fraction(7, 100));
		npcLoot.AddIf((DropAttemptInfo info) => info.npc.ModNPC<WulfrumGyrator>().Supercharged, ModContent.ItemType<EnergyCore>());
	}
}
