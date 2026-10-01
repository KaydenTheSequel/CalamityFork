using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.ProfanedGuardians;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class ProfanedShard : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianSpawn");

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
		if (!NPC.AnyNPCs(ModContent.NPCType<ProfanedGuardianCommander>()) && (player.ZoneHallow || player.ZoneUnderworldHeight))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<ProfanedGuardianCommander>(player, new SoundStyle?(UseSound));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3467, 5).AddIngredient<UnholyEssence>(25).AddTile(134)
			.Register();
	}
}
