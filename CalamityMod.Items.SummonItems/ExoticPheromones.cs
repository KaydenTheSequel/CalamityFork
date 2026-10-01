using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.Bumblebirb;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "BirbPheromones" })]
public class ExoticPheromones : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 18;
		base.Item.rare = 10;
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
		if (player.ZoneJungle && !NPC.AnyNPCs(ModContent.NPCType<Dragonfolly>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<Dragonfolly>(player, new SoundStyle?(SoundID.Roar));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LifeAlloy>(5).AddIngredient(3458, 20).AddTile(412)
			.Register();
	}
}
