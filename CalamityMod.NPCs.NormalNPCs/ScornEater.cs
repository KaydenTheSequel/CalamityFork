using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class ScornEater : ModNPC
{
	public static readonly SoundStyle JumpSound = new SoundStyle("CalamityMod/Sounds/Custom/ScornJump");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/ScornHurt");

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/ScornDeath");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.4f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.67f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 4f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.npcSlots = 3f;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 90;
		base.NPC.width = 160;
		base.NPC.height = 160;
		base.NPC.defense = 38;
		base.NPC.lifeMax = 9000;
		base.NPC.knockBackResist = 0f;
		base.AIType = -1;
		base.NPC.lavaImmune = true;
		base.NPC.value = Item.buyPrice(0, 0, 75);
		base.NPC.DeathSound = DeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ScornEaterBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ScornEater")
		});
	}

	public override void AI()
	{
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest();
		if ((Main.player[base.NPC.target].position.Y > base.NPC.position.Y + (float)base.NPC.height && base.NPC.velocity.Y > 0f) || (Main.player[base.NPC.target].position.Y < base.NPC.position.Y + (float)base.NPC.height && base.NPC.velocity.Y < 0f))
		{
			base.NPC.noTileCollide = true;
		}
		else
		{
			base.NPC.noTileCollide = false;
		}
		if (base.NPC.velocity.Y == 0f)
		{
			base.NPC.damage = 0;
			base.NPC.ai[2]++;
			int decelerationDelay = (CalamityWorld.death ? 6 : (CalamityWorld.revenge ? 12 : 20));
			if (base.NPC.ai[1] == 0f)
			{
				decelerationDelay = (CalamityWorld.death ? 3 : (CalamityWorld.revenge ? 6 : 12));
			}
			if (base.NPC.ai[2] < (float)decelerationDelay)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
				return;
			}
			base.NPC.ai[2] = 0f;
			if (base.NPC.direction == 0)
			{
				base.NPC.direction = -1;
			}
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.ai[1]++;
			base.NPC.ai[3]++;
			if (base.NPC.ai[3] >= 4f)
			{
				base.NPC.damage = base.NPC.defDamage;
				base.NPC.ai[3] = 0f;
				base.NPC.noTileCollide = true;
				if (base.NPC.ai[1] == 2f)
				{
					base.NPC.velocity.X = (float)base.NPC.direction * 15f;
					if (Main.player[base.NPC.target].position.Y < base.NPC.position.Y + (float)base.NPC.height)
					{
						base.NPC.velocity.Y = -12f;
					}
					else
					{
						base.NPC.velocity.Y = 12f;
					}
					base.NPC.ai[1] = 0f;
				}
				else
				{
					base.NPC.velocity.X = (float)base.NPC.direction * 21f;
					if (Main.player[base.NPC.target].position.Y < base.NPC.position.Y + (float)base.NPC.height)
					{
						base.NPC.velocity.Y = -6f;
					}
					else
					{
						base.NPC.velocity.Y = 12f;
					}
				}
				if (!Main.zenithWorld)
				{
					SoundEngine.PlaySound(in JumpSound, base.NPC.Center);
				}
			}
			base.NPC.netUpdate = true;
		}
		else if (base.NPC.direction == 1 && base.NPC.velocity.X < 1f)
		{
			base.NPC.velocity.X += 0.1f;
		}
		else if (base.NPC.direction == -1 && base.NPC.velocity.X > -1f)
		{
			base.NPC.velocity.X -= 0.1f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (Math.Abs(base.NPC.velocity.X) <= 1f)
		{
			if (base.NPC.frameCounter > 9.0)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
				base.NPC.frameCounter = 0.0;
			}
			if (base.NPC.frame.Y >= frameHeight * 5)
			{
				base.NPC.frame.Y = 0;
			}
			return;
		}
		if (base.NPC.frameCounter > 9.0)
		{
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y < frameHeight * 5)
		{
			base.NPC.frame.Y = frameHeight * 5;
		}
		if (base.NPC.frame.Y >= frameHeight * 7)
		{
			base.NPC.frame.Y = frameHeight * 5;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !NPC.downedMoonlord || spawnInfo.Player.Calamity().ZoneCalamity || Main.pumpkinMoon || Main.snowMoon || Main.eclipse)
		{
			return 0f;
		}
		if (SpawnCondition.Underworld.Chance > 0f)
		{
			return SpawnCondition.Underworld.Chance / 4f;
		}
		return SpawnCondition.OverworldHallow.Chance / 4f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 7;
			SoundEngine.PlaySound(in HitSound, base.NPC.Center);
		}
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
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScornEater").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScornEater2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScornEater3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScornEater4").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScornEater5").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ScornEater6").Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<UnholyEssence>(), 1, 2, 4);
	}
}
