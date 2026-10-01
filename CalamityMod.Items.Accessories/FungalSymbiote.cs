using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class FungalSymbiote : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().fungalSymbiote = true;
		int x = (int)player.Center.X / 16;
		int y = (int)(player.Bottom.Y - 1f) / 16;
		Tile groundTile = CalamityUtils.ParanoidTileRetrieval(x, y + 1);
		if (player.whoAmI != Main.myPlayer || player.velocity.Y != 0f || player.grappling[0] != -1)
		{
			return;
		}
		Tile walkTile = CalamityUtils.ParanoidTileRetrieval(x, y);
		if (walkTile.HasTile || walkTile.LiquidAmount != 0 || !(groundTile != null) || !WorldGen.SolidTile(groundTile))
		{
			return;
		}
		walkTile.TileFrameY = 0;
		walkTile.Get<TileWallWireStateData>().Slope = SlopeType.Solid;
		walkTile.Get<TileWallWireStateData>().IsHalfBlock = false;
		if (groundTile.TileType == 0)
		{
			if (Main.rand.NextBool(1000))
			{
				walkTile.Get<TileWallWireStateData>().HasTile = true;
				walkTile.TileType = 227;
				walkTile.TileFrameX = (short)((!Main.rand.NextBool()) ? 34 : 0);
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
		else if (groundTile.TileType == 2)
		{
			walkTile.Get<TileWallWireStateData>().HasTile = true;
			walkTile.TileType = 3;
			walkTile.TileFrameX = 144;
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
		else if (groundTile.TileType == 109)
		{
			walkTile.Get<TileWallWireStateData>().HasTile = true;
			walkTile.TileType = 110;
			walkTile.TileFrameX = 144;
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
		else if (groundTile.TileType == 23)
		{
			walkTile.Get<TileWallWireStateData>().HasTile = true;
			walkTile.TileType = 24;
			walkTile.TileFrameX = 144;
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
		else if (groundTile.TileType == 199)
		{
			walkTile.Get<TileWallWireStateData>().HasTile = true;
			walkTile.TileType = 201;
			walkTile.TileFrameX = 270;
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
		else if (groundTile.TileType == 70)
		{
			walkTile.Get<TileWallWireStateData>().HasTile = true;
			walkTile.TileType = 71;
			walkTile.TileFrameX = (short)(Main.rand.Next(5) * 18);
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
	}
}
