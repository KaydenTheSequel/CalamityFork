using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class LivingShard : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 14;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = 7;
	}
}
