using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Plaguebringer;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class PlaguebringerPistons : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.15f;

	public static float MoveSpeedBoost = 0.15f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent(), MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 10;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateEquip(Player player)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
		player.moveSpeed += MoveSpeedBoost;
		if (player.whoAmI != Main.myPlayer || player.velocity.Y != 0f || player.grappling[0] != -1)
		{
			return;
		}
		int x = (int)player.Center.X / 16;
		int y = (int)(player.position.Y + (float)player.height - 1f) / 16;
		Tile tile = Main.tile[x, y];
		if (tile == null)
		{
			tile = default(Tile);
		}
		if (tile.HasTile || tile.LiquidAmount != 0 || !(Main.tile[x, y + 1] != null) || !WorldGen.SolidTile(x, y + 1))
		{
			return;
		}
		tile.TileFrameY = 0;
		tile.Get<TileWallWireStateData>().Slope = SlopeType.Solid;
		tile.Get<TileWallWireStateData>().IsHalfBlock = false;
		if (Main.tile[x, y + 1].TileType == 0)
		{
			if (Main.rand.NextBool(1000))
			{
				tile.Get<TileWallWireStateData>().HasTile = true;
				tile.TileType = 227;
				tile.TileFrameX = (short)(34 * Main.rand.Next(0, 13));
				while (tile.TileFrameX == 144)
				{
					tile.TileFrameX = (short)(34 * Main.rand.Next(0, 13));
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
		if (Main.tile[x, y + 1].TileType == 2)
		{
			if (Main.rand.NextBool())
			{
				tile.Get<TileWallWireStateData>().HasTile = true;
				tile.TileType = 3;
				tile.TileFrameX = (short)(18 * Main.rand.Next(6, 11));
				while (tile.TileFrameX == 144)
				{
					tile.TileFrameX = (short)(18 * Main.rand.Next(6, 11));
				}
			}
			else
			{
				tile.Get<TileWallWireStateData>().HasTile = true;
				tile.TileType = 73;
				tile.TileFrameX = (short)(18 * Main.rand.Next(6, 21));
				while (tile.TileFrameX == 144)
				{
					tile.TileFrameX = (short)(18 * Main.rand.Next(6, 21));
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
		else if (Main.tile[x, y + 1].TileType == 109)
		{
			if (Main.rand.NextBool())
			{
				tile.Get<TileWallWireStateData>().HasTile = true;
				tile.TileType = 110;
				tile.TileFrameX = (short)(18 * Main.rand.Next(4, 7));
				while (tile.TileFrameX == 90)
				{
					tile.TileFrameX = (short)(18 * Main.rand.Next(4, 7));
				}
			}
			else
			{
				tile.Get<TileWallWireStateData>().HasTile = true;
				tile.TileType = 113;
				tile.TileFrameX = (short)(18 * Main.rand.Next(2, 8));
				while (tile.TileFrameX == 90)
				{
					tile.TileFrameX = (short)(18 * Main.rand.Next(2, 8));
				}
			}
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
		else if (Main.tile[x, y + 1].TileType == 60)
		{
			tile.Get<TileWallWireStateData>().HasTile = true;
			tile.TileType = 74;
			tile.TileFrameX = (short)(18 * Main.rand.Next(9, 17));
			if (Main.netMode == 1)
			{
				NetMessage.SendTileSquare(-1, x, y, 1);
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2363).AddIngredient(3017).AddIngredient<InfectedArmorPlating>(5)
			.AddIngredient<PlagueCellCanister>(5)
			.AddTile(134)
			.Register();
	}
}
