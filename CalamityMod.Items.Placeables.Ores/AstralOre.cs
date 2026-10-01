using CalamityMod.Tiles.Ores;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Ores;

public class AstralOre : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 99;
		ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(base.Type, 1, 116, 1);
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Ores.AstralOre>());
		base.Item.value = Item.sellPrice(0, 0, 36);
		base.Item.rare = 9;
	}
}
