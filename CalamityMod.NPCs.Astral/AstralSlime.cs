using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class AstralSlime : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 40;
		base.NPC.width = 36;
		base.NPC.height = 31;
		base.NPC.aiStyle = 1;
		base.NPC.defense = 18;
		base.NPC.lifeMax = 240;
		base.NPC.knockBackResist = 0.6f;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.alpha = 60;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.AnimationType = 1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AstralSlimeBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 65;
			base.NPC.defense = 28;
			base.NPC.lifeMax = 360;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AstralSlime")
		});
	}

	public override void AI()
	{
		base.NPC.damage = ((base.NPC.velocity.Y != 0f && !(((Vector2)(ref base.NPC.velocity)).Length() < 3f)) ? base.NPC.defDamage : 0);
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 44, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(4, 4, 36, 24), Vector2.Zero, 0.15f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, ModContent.DustType<AstralOrange>(), 1f, 4, 24);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(1))
		{
			return 0.21f;
		}
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 2, 1, 2, 1, 3));
		npcLoot.AddIf(() => DownedBossSystem.downedAstrumAureus, ModContent.ItemType<AbandonedSlimeStaff>(), 10);
		npcLoot.DefineConditionalDropSet(() => DownedBossSystem.downedAstrumDeus).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<AstralOre>(), 1, 8, 12, 11, 16));
	}
}
