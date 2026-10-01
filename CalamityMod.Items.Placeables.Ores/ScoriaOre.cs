using CalamityMod.Tiles.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Ores;

[LegacyName(new string[] { "ChaoticOre" })]
public class ScoriaOre : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 95;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Ores.ScoriaOre>());
		base.Item.value = Item.sellPrice(0, 0, 25);
		base.Item.rare = 8;
	}
}
