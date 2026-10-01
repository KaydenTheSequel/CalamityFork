using System.IO;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class CrawlerAmethyst : ModNPC
{
	private bool detected;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.NPC.npcSlots = 0.3f;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 15;
		base.NPC.width = 44;
		base.NPC.height = 34;
		base.NPC.defense = 4;
		base.NPC.lifeMax = 75;
		base.NPC.knockBackResist = 0.75f;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 0, 60);
		base.NPC.HitSound = SoundID.NPCHit33;
		base.NPC.DeathSound = SoundID.NPCDeath36;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AmethystCrawlerBanner>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.GemCrawler")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(detected);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		detected = reader.ReadBoolean();
	}

	public override void FindFrame(int frameHeight)
	{
		if (!detected)
		{
			base.NPC.frame.Y = frameHeight * 4;
			base.NPC.frameCounter = 0.0;
			return;
		}
		base.NPC.spriteDirection = -base.NPC.direction;
		base.NPC.frameCounter += ((Vector2)(ref base.NPC.velocity)).Length() / 8f;
		if (base.NPC.frameCounter > 2.0)
		{
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y >= frameHeight * 3)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override void AI()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (!detected)
		{
			base.NPC.TargetClosest();
		}
		Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
		if ((((Vector2)(ref val)).Length() < 100f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height)) || base.NPC.justHit)
		{
			detected = true;
		}
		if (detected)
		{
			CalamityRegularEnemyAI.GemCrawlerAI(base.NPC, base.Mod, 4.5f, 0.045f);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().InAnyCalamityBiome)
		{
			return 0f;
		}
		return SpawnCondition.Underground.Chance * 0.04f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 70, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 70, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("CrawlerAmethyst").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("CrawlerAmethyst2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Crawler").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Crawler2").Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(181, 1, 2, 4);
		npcLoot.Add(ModContent.ItemType<ScuttlersJewel>(), 6);
		npcLoot.Add(ModContent.ItemType<XyksBlessingBlue>(), 25);
	}
}
