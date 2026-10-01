using CalamityMod.Tiles.Abyss;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "Lumenite" })]
public class Lumenyl : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<LumenylCrystals>());
		base.Item.value = Item.sellPrice(0, 0, 12);
		base.Item.rare = 7;
	}
}
