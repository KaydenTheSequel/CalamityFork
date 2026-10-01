using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Crags;

public class InfernalCongealment : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = 1;
		base.AIType = 59;
		base.NPC.damage = 40;
		base.NPC.width = 40;
		base.NPC.height = 30;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 250;
		base.NPC.knockBackResist = 0.5f;
		base.AnimationType = 81;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.alpha = 50;
		base.NPC.lavaImmune = true;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		if (DownedBossSystem.downedProvidence)
		{
			base.NPC.damage = 80;
			base.NPC.defense = 20;
			base.NPC.lifeMax = 3500;
		}
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<InfernalCongealmentBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToWater = true;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<BrimstoneCragsBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.InfernalCongealment")
		});
	}

	public override void AI()
	{
		base.NPC.damage = ((base.NPC.velocity.Y != 0f && !(((Vector2)(ref base.NPC.velocity)).Length() < 3f)) ? base.NPC.defDamage : 0);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!DownedBossSystem.downedBrimstoneElemental)
		{
			return 0f;
		}
		if (!spawnInfo.Player.Calamity().ZoneCalamity)
		{
			return 0f;
		}
		return 0.08f;
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 235, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<InfernalSuevite>(), 1, 10, 26);
		npcLoot.Add(ModContent.ItemType<EssenceofHavoc>(), 2);
		npcLoot.DefineConditionalDropSet(DropHelper.PostProv()).Add(ModContent.ItemType<Bloodstone>(), 4);
	}
}
