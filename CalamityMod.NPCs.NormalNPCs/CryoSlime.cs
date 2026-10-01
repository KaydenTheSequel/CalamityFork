using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.Ores;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class CryoSlime : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 1;
		base.AIType = 71;
		base.NPC.damage = 30;
		base.NPC.width = 40;
		base.NPC.height = 30;
		base.NPC.defense = 24;
		base.NPC.lifeMax = 280;
		base.NPC.knockBackResist = 0.6f;
		base.AnimationType = 81;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.alpha = 50;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.coldDamage = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<CryoSlimeBanner>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.CryoSlime")
		});
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !spawnInfo.Player.ZoneSnow || !DownedBossSystem.downedCryogen || (!NPC.downedMechBoss1 && !NPC.downedMechBoss2) || (!NPC.downedMechBoss2 && !NPC.downedMechBoss3) || (!NPC.downedMechBoss3 && !NPC.downedMechBoss1) || spawnInfo.Player.Calamity().ZoneAbyss || spawnInfo.Player.Calamity().ZoneSunkenSea)
		{
			return 0f;
		}
		return SpawnCondition.Cavern.Chance * 0.08f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(44, 180);
			target.AddBuff(46, 90);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		int dustType = 91;
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dustType, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<CryonicOre>(), 1, 10, 26);
	}
}
