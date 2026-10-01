using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class ReaperTooth : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 1, 50);
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}
}
