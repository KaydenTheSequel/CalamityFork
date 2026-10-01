using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.AstrumAureus;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class AstralChunk : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumAureus/AstrumAureusSpawn");

	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 12;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 7;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 4;
		base.Item.consumable = false;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 2);
	}

	public override bool CanUseItem(Player player)
	{
		if (player.Calamity().ZoneAstral && !NPC.AnyNPCs(ModContent.NPCType<AstrumAureus>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		int posX = (int)(player.position.X + (float)Main.rand.Next(-250, 251));
		int posY = (int)(player.position.Y - 500f);
		int bossToSpawn = ModContent.NPCType<AstrumAureus>();
		CalamityUtils.SpawnBossOnPosUsingItem(player, bossToSpawn, posX, posY, new SoundStyle?(UseSound));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StarblightSoot>(30).AddIngredient(75, 20).AddIngredient<DubiousPlating>(8)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(2, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
