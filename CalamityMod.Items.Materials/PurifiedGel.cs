using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class PurifiedGel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 71;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 36;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 5);
		base.Item.rare = 4;
	}
}
