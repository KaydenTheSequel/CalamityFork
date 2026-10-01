using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "Stardust" })]
public class StarblightSoot : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 26;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 4;
	}
}
