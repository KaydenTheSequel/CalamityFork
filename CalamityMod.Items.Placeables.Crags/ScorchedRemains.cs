using CalamityMod.Tiles.Crags;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Crags;

public class ScorchedRemains : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Crags.ScorchedRemains>());
	}
}
