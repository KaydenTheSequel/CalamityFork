using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing;

public class GrandMarquisBait : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.width = 12;
		base.Item.height = 12;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.rare = 4;
		base.Item.value = Item.sellPrice(0, 0, 30);
		base.Item.bait = 75;
		base.Item.consumable = true;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.FishingBait;
	}
}
