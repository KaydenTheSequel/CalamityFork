using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.CraftingStations;

public class SCalAltarLarge : ModTile
{
	public const int Width = 5;

	public const int Height = 3;

	public override void SetStaticDefaults()
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileID.Sets.PreventsTileRemovalIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileHammeringIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsSandfall[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Width = 5;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(1, 2);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(43, 19, 42), CalamityUtils.GetItemName<AltarOfTheAccursedItem>());
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		base.AdjTiles = new int[1] { ModContent.TileType<SCalAltar>() };
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = 60;
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		return SCalAltar.AttemptToSummonSCal(i, j);
	}

	public override void MouseOver(int i, int j)
	{
		SCalAltar.HoverItemIcon(i, j);
	}

	public override void MouseOverFar(int i, int j)
	{
		SCalAltar.HoverItemIcon(i, j);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == ModContent.ProjectileType<SCalAltarArenaVisual>())
			{
				p.Kill();
				break;
			}
		}
	}
}
