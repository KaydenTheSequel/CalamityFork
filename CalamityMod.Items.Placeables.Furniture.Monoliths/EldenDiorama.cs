using CalamityMod.ForegroundDrawing.LoopingTextures;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.Monoliths;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Monoliths;

[LegacyName(new string[] { "OldDukeMonolith" })]
public class EldenDiorama : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<OldDukeMonolithTile>());
		base.Item.value = Item.sellPrice(0, 20);
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.accessory = true;
		base.Item.vanity = true;
	}

	public override void UpdateEquip(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.GetModPlayer<NuclearTorrentPlayer>().ShouldDisplayTorrentMonolith = true;
		}
	}

	public override void UpdateVanity(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.GetModPlayer<NuclearTorrentPlayer>().ShouldDisplayTorrentMonolith = true;
		}
	}
}
