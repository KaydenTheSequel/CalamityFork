using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.DesertScourge;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "DriedSeafood" })]
public class DesertMedallion : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle SummonSound = new SoundStyle("CalamityMod/Sounds/Custom/DesertScourge/DesertScourgeSummon");

	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 28;
		base.Item.rare = 2;
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
		if (player.ZoneDesert && !NPC.AnyNPCs(ModContent.NPCType<DesertScourgeHead>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<DesertScourgeHead>(player, new SoundStyle?(SummonSound));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(169, 40).AddIngredient(323, 4).AddIngredient<StormlionMandible>(2)
			.AddTile(26)
			.Register();
	}
}
