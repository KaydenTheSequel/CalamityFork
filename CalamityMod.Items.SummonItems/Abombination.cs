using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.PlaguebringerGoliath;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "Abomination" })]
public class Abombination : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/PBGSummon");

	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 17;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 18;
		base.Item.rare = 8;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 4;
		base.Item.consumable = false;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ZoneJungle && !NPC.AnyNPCs(ModContent.NPCType<PlaguebringerGoliath>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 3);
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<PlaguebringerGoliath>(player, new SoundStyle?(UseSound));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PlagueCellCanister>(20).AddIngredient<MysteriousCircuitry>(12).AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(3, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
