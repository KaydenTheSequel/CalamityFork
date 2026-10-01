using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.SunkenSeaCatches;

public class PrismaticGuppy : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Item.type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 28;
		base.Item.maxStack = 9999;
		base.Item.value = Item.sellPrice(0, 0, 5);
		base.Item.rare = 2;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Fish;
	}
}
