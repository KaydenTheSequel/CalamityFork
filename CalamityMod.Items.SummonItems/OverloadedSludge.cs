using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.SlimeGod;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class OverloadedSludge : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 4;
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
		if (!NPC.AnyNPCs(ModContent.NPCType<SlimeGodCore>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<SlimeGodCore>(player, new SoundStyle?(SoundID.Roar));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlightedGel>(40).AddRecipeGroup("AnyEvilBlock", 40).AddTile(26)
			.Register();
	}
}
