using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class CrimulanBlightSlime : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 1;
		base.AIType = 71;
		base.NPC.damage = 30;
		base.NPC.width = 60;
		base.NPC.height = 42;
		base.NPC.defense = 8;
		base.NPC.lifeMax = 130;
		base.NPC.knockBackResist = 0.3f;
		base.AnimationType = 244;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.alpha = 105;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<CrimulanBlightSlimeBanner>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.BlightSlime")
		});
	}

	public override void AI()
	{
		base.NPC.damage = ((base.NPC.velocity.Y != 0f && !(((Vector2)(ref base.NPC.velocity)).Length() < 3f)) ? base.NPC.defDamage : 0);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().ZoneAbyss)
		{
			return 0f;
		}
		return SpawnCondition.Crimson.Chance * 0.15f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 40; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(22, 240);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(23, 1, 10, 14);
		npcLoot.Add(ItemDropRule.NormalvsExpert(888, 100, 50));
		npcLoot.Add(ModContent.ItemType<BlightedGel>(), 1, 15, 21);
	}
}
