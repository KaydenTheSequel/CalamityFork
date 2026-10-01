using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class Cryon : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 42;
		base.NPC.width = 50;
		base.NPC.height = 64;
		base.NPC.defense = 25;
		base.NPC.lifeMax = 300;
		base.NPC.knockBackResist = 0.4f;
		base.NPC.value = Item.buyPrice(0, 0, 3);
		base.NPC.HitSound = SoundID.NPCHit5;
		base.NPC.DeathSound = SoundID.NPCDeath7;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<CryonBanner>();
		base.NPC.coldDamage = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Cryon")
		});
	}

	public override void AI()
	{
		CalamityRegularEnemyAI.UnicornAI(base.NPC, base.Mod, spin: false, CalamityWorld.death ? 8f : (CalamityWorld.revenge ? 6f : 4f), 5f, CalamityWorld.death ? 0.2f : (CalamityWorld.revenge ? 0.15f : 0.1f));
	}

	public override void FindFrame(int frameHeight)
	{
		if ((base.NPC.velocity.Y > 0f || base.NPC.velocity.Y < 0f) && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.spriteDirection = base.NPC.direction;
			base.NPC.frame.Y = frameHeight * 5;
			base.NPC.frameCounter = 0.0;
			return;
		}
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter += 2.0;
		}
		else
		{
			base.NPC.frameCounter += ((Vector2)(ref base.NPC.velocity)).Length() / 2f;
		}
		base.NPC.spriteDirection = base.NPC.direction;
		if (base.NPC.frameCounter > 12.0)
		{
			base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y >= frameHeight * 4)
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.ZoneSnow || spawnInfo.Player.PillarZone() || spawnInfo.Player.ZoneDungeon || spawnInfo.Player.InSunkenSea() || !Main.hardMode || spawnInfo.PlayerInTown || spawnInfo.Player.ZoneOldOneArmy || Main.snowMoon || Main.pumpkinMoon)
		{
			return 0f;
		}
		return 0.045f;
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
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 92, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 92, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<EssenceofEleum>());
	}
}
