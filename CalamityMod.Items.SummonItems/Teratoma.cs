using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.HiveMind;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class Teratoma : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 18;
		base.Item.rare = 3;
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
		if (player.ZoneCorrupt && !NPC.AnyNPCs(ModContent.NPCType<HiveMind>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<HiveMind>(player, new SoundStyle?(SoundID.Roar));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(57, 3).AddIngredient<AerialiteBar>(7).AddIngredient(68, 13)
			.AddTile(26)
			.AddDecraftCondition(CalamityConditions.DownedHiveMind)
			.Register();
	}
}
