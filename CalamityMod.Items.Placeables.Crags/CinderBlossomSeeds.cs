using CalamityMod.Tiles.Crags;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Crags;

public class CinderBlossomSeeds : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.consumable = true;
		base.Item.useTime = 10;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 1;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.buyPrice(0, 0, 1, 50);
	}

	public override bool? UseItem(Player player)
	{
		return true;
	}

	public override bool ConsumeItem(Player player)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		int tileX = Player.tileTargetX;
		int tileY = Player.tileTargetY;
		Tile tile = Framing.GetTileSafely(tileX, tileY);
		Tile tileAbove = Framing.GetTileSafely(tileX, tileY - 1);
		if (tile.HasTile && !tileAbove.HasTile && tileAbove.LiquidAmount == 0 && tile.TileType == ModContent.TileType<global::CalamityMod.Tiles.Crags.ScorchedRemains>() && player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
		{
			tile.TileType = (ushort)ModContent.TileType<ScorchedRemainsGrass>();
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(player.whoAmI, tileX, tileY);
			}
			SoundEngine.PlaySound(in SoundID.Dig, player.Center);
			return true;
		}
		return false;
	}
}
