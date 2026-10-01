using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class TempTilesManagerSystem : ModSystem
{
	public static int[] TemporaryTileIDs;

	public static List<TemporaryTile> ManagedTiles = new List<TemporaryTile>();

	public static List<TemporaryTile> DeletableTiles = new List<TemporaryTile>();

	public override void Load()
	{
		ManagedTiles = new List<TemporaryTile>();
		DeletableTiles = new List<TemporaryTile>();
	}

	public override void Unload()
	{
		ManagedTiles = null;
		DeletableTiles = null;
	}

	public override void PostAddRecipes()
	{
		TemporaryTileIDs = new int[1] { WulfrumScaffoldKit.PlacedTileType };
	}

	public static void AddTemporaryTile(Point position, TemporaryTileManager manager)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		TemporaryTile tile = manager.Setup(position);
		ManagedTiles.Add(tile);
	}

	public static int GetTemporaryTileTime(Point position)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		return ManagedTiles.Find(delegate(TemporaryTile t)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return t.position == position;
		}).timeleft;
	}

	public override void PostWorldLoad()
	{
		for (int i = 0; i < Main.maxTilesX; i++)
		{
			for (int j = 0; j < Main.maxTilesY; j++)
			{
				if (TemporaryTileIDs.Contains(Main.tile[i, j].TileType))
				{
					WorldGen.KillTile(i, j);
				}
			}
		}
	}

	public override void OnWorldUnload()
	{
		ManagedTiles = new List<TemporaryTile>();
		DeletableTiles = new List<TemporaryTile>();
	}

	public override void PostDrawTiles()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (ManagedTiles.Count <= 0)
		{
			return;
		}
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		foreach (TemporaryTile tile in ManagedTiles)
		{
			if (TileLoader.GetTile(Main.tile[tile.position].TileType) is ISpecialTempTileDraw coolTile)
			{
				coolTile.CoolDraw(tile.position.X, tile.position.Y, Main.spriteBatch);
			}
		}
		Main.spriteBatch.End();
	}

	public override void PostUpdateEverything()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < ManagedTiles.Count; i++)
		{
			TemporaryTile tile = ManagedTiles[i];
			TemporaryTileManager manager = tile.manager;
			if (!manager.ManagedTypes.Contains(Main.tile[tile.position].TileType))
			{
				DeletableTiles.Add(tile);
				continue;
			}
			manager.UpdateEffect(tile);
			tile.timeleft--;
			if (tile.timeleft < 0)
			{
				manager.EndEffect(tile);
				WorldGen.KillTile(tile.position.X, tile.position.Y);
				NetMessage.SendTileSquare(-1, tile.position.X, tile.position.Y);
				DeletableTiles.Add(tile);
			}
			ManagedTiles[i] = tile;
		}
		foreach (TemporaryTile deletableTile in DeletableTiles)
		{
			ManagedTiles.Remove(deletableTile);
		}
		DeletableTiles.Clear();
	}
}
