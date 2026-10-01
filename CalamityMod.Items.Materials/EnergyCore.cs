using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class EnergyCore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 22);
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 0, 40);
		base.Item.rare = 1;
	}
}
