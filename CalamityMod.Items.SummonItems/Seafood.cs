using CalamityMod.Events;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.NPCs.AquaticScourge;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class Seafood : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 9;
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 24;
		base.Item.rare = 5;
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
		if (player.Calamity().ZoneSulphur && !NPC.AnyNPCs(ModContent.NPCType<AquaticScourgeHead>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<AquaticScourgeHead>(player, new SoundStyle?(SoundID.Roar));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SulphurousSand>(20).AddIngredient(2626, 10).AddIngredient(319, 5)
			.AddTile(16)
			.Register();
	}
}
