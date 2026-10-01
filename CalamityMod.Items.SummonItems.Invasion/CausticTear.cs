using CalamityMod.Events;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems.Invasion;

public class CausticTear : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 28;
		base.Item.consumable = true;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.useStyle = 4;
		base.Item.rare = 2;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.EventItem;
	}

	public override bool CanUseItem(Player player)
	{
		return !AcidRainEvent.AcidRainEventIsOngoing;
	}

	public override bool? UseItem(Player player)
	{
		if (Main.netMode != 1)
		{
			AcidRainEvent.TryStartEvent(forceRain: true);
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SulphuricScale>().AddCondition(Condition.NearWater).Register();
	}
}
