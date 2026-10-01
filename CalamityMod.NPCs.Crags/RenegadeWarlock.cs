using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Melee;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Crags;

public class RenegadeWarlock : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.lavaImmune = true;
		base.NPC.aiStyle = 3;
		base.NPC.damage = 50;
		base.NPC.width = 18;
		base.NPC.height = 40;
		base.NPC.defense = 16;
		base.NPC.lifeMax = 300;
		base.NPC.knockBackResist = 0.5f;
		base.AnimationType = 331;
		base.AIType = 120;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath50;
		if (DownedBossSystem.downedProvidence)
		{
			base.NPC.damage = 100;
			base.NPC.defense = 30;
			base.NPC.lifeMax = 3000;
		}
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<RenegadeWarlockBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToWater = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<BrimstoneCragsBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.RenegadeWarlock")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if ((!spawnInfo.Player.Calamity().ZoneCalamity && !spawnInfo.Player.ZoneDungeon) || !Main.hardMode)
		{
			return 0f;
		}
		return 0.04f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
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
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 20; i++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (!Main.dedServ)
		{
			int count = ((!Main.zenithWorld) ? 1 : 20);
			for (int g = 0; g < count; g++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("RenegadeWarlock").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("RenegadeWarlock2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("RenegadeWarlock3").Type, base.NPC.scale);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<EssenceofHavoc>(), 2);
		npcLoot.AddIf(() => NPC.downedPlantBoss, ModContent.ItemType<Hellkite>(), 8);
		npcLoot.DefineConditionalDropSet(DropHelper.PostProv()).Add(ModContent.ItemType<Bloodstone>(), 4);
	}
}
