using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Tiles.Astral;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityMod.World;

public class AstralChestGeneration
{
	public static void PlaceAstralChest()
	{
		int left = GenVars.dMinX + 25;
		int right = GenVars.dMaxX - 25;
		int top = (int)Main.worldSurface;
		int bottom = GenVars.dMaxY - 25;
		bool num = left >= right;
		bool invalidHeightData = top >= bottom;
		if (num | invalidHeightData)
		{
			CalamityMod.Log.Warn((object)"The generated dungeon was found to have unusable area bounds. As a result, the astral chest could not be generated.");
			return;
		}
		int astralChestItemID = ModContent.ItemType<HeavenfallenStardisk>();
		ushort astralChestID = (ushort)ModContent.TileType<AstralChestLocked>();
		int chestStyle = 1;
		Chest chest = null;
		int attempts = 0;
		while (chest == null && attempts < 1000)
		{
			attempts++;
			int x = WorldGen.genRand.Next(left, right);
			int y = WorldGen.genRand.Next(top, bottom);
			Tile randomTile = Main.tile[x, y];
			if (Main.wallDungeon[randomTile.WallType] && !randomTile.HasTile)
			{
				chest = MiscWorldgenRoutines.AddChestWithLoot(x, y, astralChestID, 1u, chestStyle);
			}
		}
		if (chest != null)
		{
			chest.item[0].SetDefaults(astralChestItemID);
			chest.item[0].Prefix(-1);
		}
		else
		{
			CalamityMod.Log.Warn((object)"The astral chest loop could not find a valid generation location. As a result, it has not generated.");
		}
	}
}
