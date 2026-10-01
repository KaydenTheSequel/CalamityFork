using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Banners;

public class GammaSlimeBanner : BaseBanner
{
	public override int BannerTileStyle => 122;

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<IrradiatedSlimeBanner>();
	}
}
