using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class EffulgentFeather : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(3, 11));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 102;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 24;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 1, 30);
		base.Item.rare = 11;
	}
}
