using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Providence;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "ProfanedCoreUnlimited" })]
public class ProfanedCore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 4;
		base.Item.consumable = false;
		base.Item.rare = 11;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override bool CanUseItem(Player player)
	{
		int Prov = CalamityGlobalNPC.holyBoss;
		bool canPissOffProvi = Prov != -1 && (float)Main.npc[Prov].life >= (float)Main.npc[Prov].lifeMax * 0.95f && Main.npc[Prov].Calamity().newAI[3] >= 180f && !Main.npc[Prov].Calamity().CurrentlyEnraged;
		if ((!NPC.AnyNPCs(ModContent.NPCType<Providence>()) | canPissOffProvi) && (player.ZoneHallow || player.ZoneUnderworldHeight))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		int Prov = CalamityGlobalNPC.holyBoss;
		if (Prov != -1 && (float)Main.npc[Prov].life >= (float)Main.npc[Prov].lifeMax * 0.95f && Main.npc[Prov].Calamity().newAI[3] >= 180f && !Main.npc[Prov].Calamity().CurrentlyEnraged)
		{
			(Main.npc[Prov].ModNPC as Providence).hasBeenGivenFullPower = true;
			Projectile.NewProjectile(base.Item.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<HolyProfanedCore>(), 0, 0f);
		}
		else
		{
			int posX = (int)player.position.X;
			int posY = (int)(player.position.Y - 100f);
			int bossToSpawn = ModContent.NPCType<Providence>();
			CalamityUtils.SpawnBossOnPosUsingItem(player, bossToSpawn, posX, posY, new SoundStyle?(Providence.SpawnSound));
		}
		return true;
	}
}
