using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class TitanHeart : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SetDefaults()
	{
		base.Item.width = 10;
		base.Item.height = 10;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 30);
		base.Item.rare = 4;
	}
}
