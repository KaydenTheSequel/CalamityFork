using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.BrimstoneElemental;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class CharredIdol : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Custom/BrimstoneElemental/BrimstoneSpawn");

	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 18;
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
		if (player.Calamity().ZoneCalamity && !NPC.AnyNPCs(ModContent.NPCType<BrimstoneElemental>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<BrimstoneElemental>(player, new SoundStyle?(UseSound));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(521, 5).AddIngredient<EssenceofHavoc>(7).AddIngredient<UnholyCore>(2)
			.AddTile(77)
			.Register();
	}
}
