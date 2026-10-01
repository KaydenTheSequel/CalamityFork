using System.IO;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class WulfrumDrone : ModNPC
{
	public const float TotalHorizontalChargeTime = 75f;

	internal DroneAIState AIState
	{
		get
		{
			return (DroneAIState)base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = (float)value;
		}
	}

	public float HorizontalChargeTime
	{
		get
		{
			return base.NPC.ai[1];
		}
		set
		{
			base.NPC.ai[1] = value;
		}
	}

	public float Time
	{
		get
		{
			return base.NPC.ai[2];
		}
		set
		{
			base.NPC.ai[2] = value;
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

	public ref float FlyAwayTimer => ref base.NPC.localAI[0];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.AIType = -1;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 16;
		base.NPC.width = 32;
		base.NPC.height = 32;
		base.NPC.defense = 4;
		base.NPC.lifeMax = 25;
		base.NPC.knockBackResist = 0.35f;
		base.NPC.value = Item.buyPrice(0, 0, 0, 80);
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = WulfrumAmplifier.Hit;
		base.NPC.DeathSound = CommonCalamitySounds.WulfrumNPCDeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<WulfrumDroneBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.WulfrumDrone")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(FlyAwayTimer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		FlyAwayTimer = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		Player player = Main.player[base.NPC.target];
		bool farFromPlayer = base.NPC.Distance(player.Center) > 960f;
		if (((base.NPC.target < 0 || base.NPC.target >= 255) | farFromPlayer) || player.dead || !player.active)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			farFromPlayer = base.NPC.Distance(player.Center) > 960f;
			if ((player.dead || !player.active) | farFromPlayer)
			{
				if (FlyAwayTimer > 420f)
				{
					base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, Vector2.UnitY * -8f, 0.1f);
					base.NPC.rotation = base.NPC.rotation.AngleTowards(0f, MathHelper.ToRadians(15f));
					base.NPC.noTileCollide = true;
				}
				else
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 0.96f;
					base.NPC.rotation = base.NPC.rotation.AngleTowards(0f, MathHelper.ToRadians(15f));
					FlyAwayTimer++;
				}
				return;
			}
		}
		FlyAwayTimer = Utils.Clamp(FlyAwayTimer - 3f, 0f, 180f);
		base.NPC.noTileCollide = !farFromPlayer;
		if (Supercharged)
		{
			SuperchargeTimer--;
		}
		if (AIState == DroneAIState.Searching)
		{
			base.NPC.damage = 0;
			if (base.NPC.direction == 0)
			{
				base.NPC.direction = 1;
			}
			float searchVelocity = (CalamityWorld.death ? 10f : (CalamityWorld.revenge ? 9f : (Main.expertMode ? 8f : 6f)));
			Vector2 destination = player.Center + new Vector2(300f * (float)base.NPC.direction, -90f);
			base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, base.NPC.SafeDirectionTo(destination) * searchVelocity, searchVelocity / 60f);
			if (base.NPC.Distance(destination) < 40f)
			{
				Time++;
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.95f;
				float chargeDelay = (CalamityWorld.death ? 10f : (CalamityWorld.revenge ? 15f : (Main.expertMode ? 20f : 40f)));
				if (Time >= chargeDelay)
				{
					AIState = DroneAIState.Charging;
					base.NPC.netUpdate = true;
				}
			}
		}
		else
		{
			base.NPC.damage = base.NPC.defDamage;
			float chargeVelocity = (CalamityWorld.death ? 10f : (CalamityWorld.revenge ? 9f : (Main.expertMode ? 8f : 6f)));
			if (HorizontalChargeTime < 25f)
			{
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, base.NPC.SafeDirectionTo(player.Center) * chargeVelocity, chargeVelocity / 60f);
			}
			if (Supercharged && Main.netMode != 1 && HorizontalChargeTime % 30f == 29f)
			{
				int damage = (Main.masterMode ? 8 : (Main.expertMode ? 9 : 12));
				if (Main.zenithWorld)
				{
					int spread = 15;
					for (int times = 3; times > 0; times--)
					{
						Vector2 velocity = base.NPC.SafeDirectionTo(player.Center, Vector2.UnitY) * 6f;
						Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X + (float)Main.rand.Next(-2, 3), velocity.Y + (float)Main.rand.Next(-2, 3)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitX * 6f * (float)base.NPC.spriteDirection, perturbedspeed, 449, damage, 0f);
						spread -= Main.rand.Next(5, 8);
					}
				}
				else
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitX * 6f * (float)base.NPC.spriteDirection, base.NPC.SafeDirectionTo(player.Center, Vector2.UnitY) * 6f, 449, damage, 0f);
				}
				SoundEngine.PlaySound(in SoundID.Item12);
			}
			float totalChargeTime = 75f - (CalamityWorld.death ? 25f : (CalamityWorld.revenge ? 20f : (Main.expertMode ? 15f : 0f)));
			HorizontalChargeTime++;
			if (HorizontalChargeTime > totalChargeTime)
			{
				AIState = DroneAIState.Searching;
				HorizontalChargeTime = 0f;
				base.NPC.direction = (player.Center.X - base.NPC.Center.X < 0f).ToDirectionInt();
				base.NPC.netUpdate = true;
			}
		}
		base.NPC.spriteDirection = (base.NPC.velocity.X < 0f).ToDirectionInt();
		base.NPC.rotation = base.NPC.velocity.X / 25f;
		if (!Main.dedServ)
		{
			Dust dust = Dust.NewDustPerfect(base.NPC.Bottom, 229);
			dust.color = Color.Green;
			dust.scale = 0.675f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		int frame = (int)(base.NPC.frameCounter / 5.0) % (Main.npcFrameCount[base.Type] / 2);
		if (Supercharged)
		{
			frame += Main.npcFrameCount[base.Type] / 2;
		}
		base.NPC.frame.Y = frame * frameHeight;
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
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 3, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 15; i++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 3, hit.HitDirection, -1f);
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumDroneGore1").Type);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumDroneGore2").Type);
			int randomGoreCount = Main.rand.Next(1, 4);
			for (int j = 0; j < randomGoreCount; j++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumEnemyGore" + Main.rand.Next(1, 11)).Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<WulfrumMetalScrap>(), 1, 1, 3);
		npcLoot.Add(ModContent.ItemType<WulfrumBattery>(), new Fraction(7, 100));
		npcLoot.AddIf((DropAttemptInfo info) => info.npc.ModNPC<WulfrumDrone>().Supercharged, ModContent.ItemType<EnergyCore>());
	}
}
