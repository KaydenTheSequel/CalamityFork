using CalamityMod.Tiles.Astral;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Astral;

public class AstralClay : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(base.Type, 1, 133, 1);
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Astral.AstralClay>());
	}
}
