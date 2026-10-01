using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Tiles.Crags;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.BrimstoneCragCatches;

public class SlagCrate : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.IsFishingCrate[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<SlagCrateTile>());
		base.Item.width = (base.Item.height = 32);
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.rare = 2;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Crates;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		itemLoot.Add(ModContent.ItemType<global::CalamityMod.Items.Placeables.Crags.ScorchedBone>(), 3, 20, 50);
		itemLoot.Add(ModContent.ItemType<SlagfireDouser>(), 10);
		itemLoot.AddBiomeCrateLootRules(hardMode: false);
	}
}
