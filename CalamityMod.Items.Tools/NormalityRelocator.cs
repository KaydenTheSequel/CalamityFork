using System.Collections.Generic;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Plates;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class NormalityRelocator : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle TeleportSound = new SoundStyle("CalamityMod/Sounds/Item/NormalityRelocator", 3);

	public new string LocalizationCategory => "Items.Tools";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 7));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 38;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.NormalityRelocatorHotKey);
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override void UpdateInventory(Player player)
	{
		player.Calamity().normalityRelocator = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1326).AddIngredient(3459, 30).AddIngredient<Cinderplate>(5)
			.AddIngredient<ExodiumCluster>(10)
			.AddTile(134)
			.Register();
	}
}
