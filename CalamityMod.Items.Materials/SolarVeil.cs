using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class SolarVeil : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 32;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 15);
		base.Item.rare = 7;
	}
}
