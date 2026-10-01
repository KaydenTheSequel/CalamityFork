namespace CalamityMod.Items.Placeables.Banners;

public class FearlessGoldfishWarriorBanner : BaseBanner
{
	public override int BannerTileStyle => 109;

	public override void SetDefaults()
	{
		base.Item.Calamity().donorItem = true;
		base.SetDefaults();
	}
}
