using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles;

public class RoxTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileSpelunker[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		Main.tileObsidianKill[base.Type] = false;
		Main.tileOreFinderPriority[base.Type] = 910;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 18 };
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(240, 77, 7), CalamityUtils.GetItemName<Roxcalibur>());
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		RegisterItemDrop(ModContent.ItemType<Roxcalibur>());
		FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<Roxcalibur>(), base.Type, default(int));
		base.DustType = 35;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 1.64f;
		g = 0.25f;
		b = 1.89f;
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		Tile tile = Main.tile[i, j];
		if (tile.TileFrameY == 18 && tile.TileFrameX < 54)
		{
			CalamityUtils.DrawFlameSparks(173, 5, i, j);
		}
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = ((!WorldGen.genRand.NextBool(3)) ? 173 : 35);
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 5 : 50);
	}
}
