using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "SulfuricScale" })]
public class SulphuricScale : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 0, 60);
		base.Item.rare = 2;
	}
}
