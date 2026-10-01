using CalamityMod.Items.Placeables.MusicBoxes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace CalamityMod.Tiles.MusicBoxes;

public class DraedonTalkMusicBox : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileObsidianKill[base.Type] = true;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.newTile.StyleLineSkip = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(191, 142, 111), CalamityUtils.GetItemName(576));
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<global::CalamityMod.Items.Placeables.MusicBoxes.DraedonTalkMusicBox>();
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		return false;
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		if (Lighting.UpdateEveryFrame && new FastRandom(Main.TileFrameSeed).WithModifier(i, j).Next(4) != 0)
		{
			return;
		}
		Tile tile = Main.tile[i, j];
		if (TileDrawing.IsVisible(tile) && tile.TileFrameX == 36 && tile.TileFrameY % 36 == 0 && (int)Main.timeForVisualEffects % 7 == 0 && Main.rand.NextBool(3))
		{
			int goreType = Main.rand.Next(570, 573);
			Vector2 position = default(Vector2);
			((Vector2)(ref position))._002Ector((float)(i * 16 + 8), (float)(j * 16 - 8));
			Vector2 velocity = default(Vector2);
			((Vector2)(ref velocity))._002Ector(Main.WindForVisuals * 2f, -0.5f);
			velocity.X *= Main.rand.NextFloat(0.5f, 1.5f);
			velocity.Y *= Main.rand.NextFloat(0.5f, 1.5f);
			switch (goreType)
			{
			case 572:
				position.X -= 8f;
				break;
			case 571:
				position.X -= 4f;
				break;
			}
			Gore.NewGore(new EntitySource_TileUpdate(i, j), position, velocity, goreType, 0.8f);
		}
	}
}
