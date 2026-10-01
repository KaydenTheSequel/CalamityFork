using CalamityMod.Tiles.Astral;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.AstralCatches;

public class MonolithCrate : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.IsFishingCrate[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<MonolithCrateTile>());
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
		itemLoot.Add(ModContent.ItemType<AstrophageItem>(), 20);
		itemLoot.AddBiomeCrateLootRules(hardMode: false);
	}
}
