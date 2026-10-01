using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Tiles.Crags;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.BrimstoneCragCatches;

public class BrimstoneCrate : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
		ItemID.Sets.IsFishingCrate[base.Type] = true;
		ItemID.Sets.IsFishingCrateHardmode[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<SlagCrate>();
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 32;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.rare = 2;
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.createTile = ModContent.TileType<BrimstoneCrateTile>();
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.useAnimation = 15;
		base.Item.useTime = 10;
		base.Item.useStyle = 1;
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
		itemLoot.Add(ModContent.ItemType<EssenceofHavoc>(), 2, 2, 5);
		itemLoot.Add(ModContent.ItemType<SlagfireDouser>(), 10);
		itemLoot.AddBiomeCrateLootRules();
	}
}
