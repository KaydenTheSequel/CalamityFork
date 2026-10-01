using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class GrandScale : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.rare = 7;
	}
}
