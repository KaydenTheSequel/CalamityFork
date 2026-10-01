using System;
using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class WulfrumAmplifier : ModNPC
{
	public static readonly SoundStyle Hit = new SoundStyle("CalamityMod/Sounds/NPCHit/WulfrumHit", 3);

	public const float ChargeRadiusMax = 495f;

	public const float SuperchargeTime = 720f;

	public int laserDelay = 150;

	public bool Charging
	{
		get
		{
			return base.NPC.ai[0] != 0f;
		}
		set
		{
			base.NPC.ai[0] = value.ToInt();
		}
	}

	public float ChargeRadius
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

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
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
		base.NPC.damage = 0;
		base.NPC.width = 44;
		base.NPC.height = 44;
		base.NPC.defense = 4;
		base.NPC.lifeMax = (Main.zenithWorld ? 200 : 100);
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = Hit;
		base.NPC.DeathSound = CommonCalamitySounds.WulfrumNPCDeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<WulfrumAmplifierBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		if (Main.zenithWorld)
		{
			base.NPC.scale = 1.5f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.WulfrumAmplifier")
		});
	}

	public override void AI()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		List<int> SuperchargableEnemies = new List<int>
		{
			ModContent.NPCType<WulfrumDrone>(),
			ModContent.NPCType<WulfrumGyrator>(),
			ModContent.NPCType<WulfrumHovercraft>(),
			ModContent.NPCType<WulfrumRover>()
		};
		base.NPC.TargetClosest(faceTarget: false);
		Player player = Main.player[base.NPC.target];
		if (!Charging && base.NPC.Distance(player.Center) < 330.165f)
		{
			if (Main.netMode != 1)
			{
				int enemiesToSpawn = (Main.getGoodWorld ? 4 : (CalamityWorld.death ? 3 : ((!CalamityWorld.revenge) ? 1 : 2)));
				for (int i = 0; i < enemiesToSpawn; i++)
				{
					int tries = 0;
					Vector2 spawnPosition;
					do
					{
						spawnPosition = player.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(600f, 1015f) * new Vector2(1.5f, 1f);
						if (spawnPosition.Y > player.Center.Y)
						{
							spawnPosition.Y = player.Center.Y;
						}
						if (tries > 500)
						{
							break;
						}
						tries++;
					}
					while (WorldGen.SolidTile(CalamityUtils.ParanoidTileRetrieval((int)spawnPosition.X / 16, (int)spawnPosition.Y / 16)));
					if (tries < 500 && !Main.zenithWorld)
					{
						int npcToSpawn = (Main.rand.NextBool() ? ModContent.NPCType<WulfrumDrone>() : ModContent.NPCType<WulfrumHovercraft>());
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnPosition.X, (int)spawnPosition.Y, npcToSpawn);
					}
					else if (tries < 500 && Main.zenithWorld)
					{
						int npcToSpawn2 = Main.rand.Next(0, 4);
						switch (enemiesToSpawn)
						{
						case 0:
							npcToSpawn2 = ModContent.NPCType<WulfrumDrone>();
							break;
						case 1:
							npcToSpawn2 = ModContent.NPCType<WulfrumHovercraft>();
							break;
						case 2:
							npcToSpawn2 = ModContent.NPCType<WulfrumGyrator>();
							break;
						case 3:
							npcToSpawn2 = ModContent.NPCType<WulfrumRover>();
							break;
						}
						NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnPosition.X, (int)spawnPosition.Y, npcToSpawn2);
					}
				}
			}
			Charging = true;
			base.NPC.netUpdate = true;
		}
		else
		{
			if (!Charging)
			{
				return;
			}
			ChargeRadius = (int)MathHelper.Lerp(ChargeRadius, 495f, 0.1f);
			if (Main.rand.NextBool(4))
			{
				float dustCount = (float)Math.PI * 2f * ChargeRadius / 8f;
				for (int j = 0; (float)j < dustCount; j++)
				{
					float angle = (float)Math.PI * 2f * (float)j / dustCount;
					Dust dust = Dust.NewDustPerfect(base.NPC.Center, 229);
					dust.position = base.NPC.Center + angle.ToRotationVector2() * ChargeRadius;
					dust.scale = 0.7f;
					dust.noGravity = true;
					dust.velocity = base.NPC.velocity;
				}
			}
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npcAtIndex = enumerator.Current;
				if ((!SuperchargableEnemies.Contains(npcAtIndex.type) && npcAtIndex.type != ModContent.NPCType<WulfrumRover>()) || npcAtIndex.ai[3] > 0f || base.NPC.Distance(npcAtIndex.Center) > ChargeRadius)
				{
					continue;
				}
				npcAtIndex.ai[3] = 720f;
				npcAtIndex.netUpdate = true;
				if (!Main.dedServ)
				{
					for (int k = 0; k < 10; k++)
					{
						Dust.NewDust(npcAtIndex.position, npcAtIndex.width, npcAtIndex.height, 226);
					}
				}
			}
			if (Main.getGoodWorld)
			{
				laserDelay--;
				base.NPC.spriteDirection = (player.Center.X - base.NPC.Center.X < 0f).ToDirectionInt();
				int times = 3;
				while (times > 0 && laserDelay == 0)
				{
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center + Vector2.UnitX * 6f * (float)base.NPC.spriteDirection, base.NPC.SafeDirectionTo(player.Center, Vector2.UnitY) * 4.5f, 448, 10, 0f);
					times--;
				}
				if (laserDelay <= 0)
				{
					laserDelay = 150;
				}
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		int frame = (int)(base.NPC.frameCounter / 8.0) % Main.npcFrameCount[base.Type];
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().ZoneSulphur || (!spawnInfo.Player.ZoneOverworldHeight && !Main.remixWorld) || (!spawnInfo.Player.ZoneNormalCaverns && spawnInfo.Player.ZoneGlowshroom && Main.remixWorld))
		{
			return 0f;
		}
		if ((float)spawnInfo.PlayerFloorX > (float)Main.maxTilesX * 0.333f && (float)spawnInfo.PlayerFloorX < (float)Main.maxTilesX - (float)Main.maxTilesX * 0.333f)
		{
			return (Main.remixWorld ? SpawnCondition.Cavern.Chance : SpawnCondition.OverworldDaySlime.Chance) * (Main.hardMode ? 0.015f : 0.06f) * ((!NPC.AnyNPCs(base.NPC.type)) ? 1.3f : 1f);
		}
		return (Main.remixWorld ? SpawnCondition.Cavern.Chance : SpawnCondition.OverworldDaySlime.Chance) * (Main.hardMode ? 0.0375f : 0.15f) * ((!NPC.AnyNPCs(base.NPC.type)) ? 1.3f : 1f);
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
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
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
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumAmplifierGore").Type);
			int randomGoreCount = Main.rand.Next(1, 4);
			for (int j = 0; j < randomGoreCount; j++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumEnemyGore" + Main.rand.Next(1, 11)).Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<WulfrumMetalScrap>(), 1, 2, 3);
		npcLoot.Add(ModContent.ItemType<WulfrumBattery>(), new Fraction(7, 100));
		npcLoot.Add(ModContent.ItemType<AbandonedWulfrumHelmet>(), new Fraction(5, 100));
		npcLoot.Add(ModContent.ItemType<EnergyCore>());
	}
}
