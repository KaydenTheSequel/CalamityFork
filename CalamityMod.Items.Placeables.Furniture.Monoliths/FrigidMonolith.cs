using CalamityMod.Tiles.Furniture.Monoliths;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Monoliths;

public class FrigidMonolith : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<FrigidMonolithTile>());
		base.Item.value = Item.buyPrice(0, 25);
		base.Item.rare = 5;
		base.Item.accessory = true;
		base.Item.vanity = true;
	}

	public override void UpdateEquip(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().monolithCryogenShader = 30;
		}
	}

	public override void UpdateVanity(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().monolithCryogenShader = 30;
		}
	}
}
