using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.GreatSandShark;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class SandstormsCore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 13;
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

	public override bool CanUseItem(Player player)
	{
		if (player.ZoneDesert && (!Main.zenithWorld || player.Calamity().ZoneAstral))
		{
			return !NPC.AnyNPCs(ModContent.NPCType<GreatSandShark>());
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<GreatSandShark>(player, new SoundStyle?(SoundID.Roar));
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[BIOME]", Main.zenithWorld ? CalamityUtils.GetTextValue("Biomes.AstralDesert.TownNPCDialogueName") : Language.GetTextValue("Bestiary_Biomes.Desert"));
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3783, 3).AddIngredient<CoreofCalamity>().AddTile(134)
			.Register();
	}
}
