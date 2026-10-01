using CalamityMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables;

public abstract class BaseBanner : ModItem, ILocalizedModType, IModType
{
	public virtual int BannerTileID => ModContent.TileType<MonsterBanner>();

	public virtual int BannerTileStyle => 0;

	public virtual int BannerKillRequirement => ItemID.Sets.DefaultKillsForBannerNeeded;

	public virtual int BonusNPCID => MonsterBanner.GetBannerNPC(BannerTileStyle);

	public new string LocalizationCategory => "Items.Placeables";

	public virtual LocalizedText NPCName => NPCLoader.GetNPC(BonusNPCID).DisplayName;

	public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs(NPCName.ToString());

	public override LocalizedText Tooltip => CalamityUtils.GetText(LocalizationCategory + ".FormattedBannerTooltip").WithFormatArgs(NPCName.ToString());

	public override void SetStaticDefaults()
	{
		ItemID.Sets.KillsToBanner[base.Type] = BannerKillRequirement;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(BannerTileID, BannerTileStyle);
		base.Item.width = 10;
		base.Item.height = 24;
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}
}
