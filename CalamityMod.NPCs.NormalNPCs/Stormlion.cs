using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Summon;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class Stormlion : ModNPC
{
	public static readonly SoundStyle IdleSound = new SoundStyle("CalamityMod/Sounds/Custom/StormlionIdle");

	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/StormlionHit");

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/StormlionDeath");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 20;
		base.NPC.aiStyle = 3;
		base.NPC.width = 33;
		base.NPC.height = 31;
		base.NPC.defense = 8;
		base.NPC.lifeMax = 100;
		base.NPC.knockBackResist = 0.2f;
		base.AnimationType = 580;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<StormlionBanner>();
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Stormlion")
		});
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(800))
		{
			SoundEngine.PlaySound(in IdleSound, base.NPC.Center);
		}
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
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Stormlion").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Stormlion2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Stormlion3").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Stormlion4").Type, base.NPC.scale);
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().ZoneSunkenSea || spawnInfo.Player.PillarZone() || spawnInfo.Player.InAstral() || spawnInfo.Player.ZoneCorrupt || spawnInfo.Player.ZoneCrimson || spawnInfo.Player.ZoneOldOneArmy || Main.eclipse || Main.snowMoon || Main.pumpkinMoon || Main.invasionType != 0)
		{
			return 0f;
		}
		if (Main.IsItStorming && spawnInfo.Player.ZoneDesert)
		{
			return SpawnCondition.OverworldDayDesert.Chance;
		}
		return SpawnCondition.DesertCave.Chance * 0.3f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<StaticDischarge>(), 120);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<StormlionMandible>());
		npcLoot.Add(ModContent.ItemType<StormjawStaff>(), 5);
		npcLoot.Add(4061, 25);
		npcLoot.Add(4062, 25);
	}
}
