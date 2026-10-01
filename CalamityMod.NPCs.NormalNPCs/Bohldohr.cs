using CalamityMod.Items.Placeables;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs.Other;
using CalamityMod.World;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class Bohldohr : ModNPC
{
	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 80;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.defense = 32;
		base.NPC.lifeMax = 600;
		base.NPC.knockBackResist = 0.95f;
		base.NPC.value = Item.buyPrice(0, 0, 10);
		base.NPC.HitSound = SoundID.NPCHit7;
		base.NPC.DeathSound = SoundID.NPCDeath35;
		base.NPC.behindTiles = true;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<BohldohrBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheTemple,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Bohldohr")
		});
	}

	public override void AI()
	{
		CalamityRegularEnemyAI.UnicornAI(base.NPC, base.Mod, spin: true, CalamityWorld.death ? 8f : (CalamityWorld.revenge ? 6f : 4f), 5f, 0.2f);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe)
		{
			return 0f;
		}
		return SpawnCondition.JungleTemple.Chance * 0.1f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 155, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 155, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bohldohr").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bohldohr2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Bohldohr3").Type);
			}
		}
	}

	public override void OnKill()
	{
		if (Main.zenithWorld && Main.netMode != 1 && Main.rand.NextBool(42))
		{
			NPC.SpawnOnPlayer(Main.myPlayer, ModContent.NPCType<THELORDE>());
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<Stohne>(), 1, 10, 26);
		npcLoot.Add(2766, 7, 10, 26);
		npcLoot.Add(1293, 50);
		npcLoot.AddIf(() => DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs && Main.zenithWorld, ModContent.ItemType<NO>(), 2, 1, 1, ui: false);
	}
}
