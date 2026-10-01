using CalamityMod.CalPlayer;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Placeables.DraedonStructures;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.DraedonStructures;

public class PowerCellFactory : ModTile
{
	public const int Width = 4;

	public const int Height = 4;

	public const int OriginOffsetX = 1;

	public const int OriginOffsetY = 3;

	public const int SheetSquare = 18;

	public const int TotalFrames = 45;

	private const int FramesPerColumn = 15;

	public const int AnimationFramerate = 5;

	public const int BetweenCellDowntime = 675;

	public const int CellCreateFrame = 42;

	public const int MagicFrameDelay = 4;

	public override void SetStaticDefaults()
	{
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileID.Sets.HasOutlines[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Width = 4;
		TileObjectData.newTile.Height = 4;
		TileObjectData.newTile.Origin = new Point16(1, 3);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateHeights = new int[4] { 16, 16, 16, 16 };
		TileObjectData.newTile.LavaDeath = false;
		ModTileEntity te = ModContent.GetInstance<TEPowerCellFactory>();
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(te.Hook_AfterPlacement, -1, 0, processedCoordinates: true);
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(67, 72, 81), CalamityUtils.GetItemName<PowerCellFactoryItem>());
		base.AnimationFrameHeight = 68;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 226);
		return false;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Tile t = Main.tile[i, j];
		int left = i - t.TileFrameX % 72 / 18;
		int top = j - t.TileFrameY % 72 / 18;
		TEPowerCellFactory tEPowerCellFactory = CalamityUtils.FindTileEntity<TEPowerCellFactory>(i, j, 4, 4, 18);
		int numCells = tEPowerCellFactory?.CellStack ?? 0;
		if (numCells > 0)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), new Vector2((float)i, (float)j) * 16f, ModContent.ItemType<DraedonPowerCell>(), numCells);
		}
		tEPowerCellFactory?.Kill(left, top);
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		TEPowerCellFactory thisFactory = CalamityUtils.FindTileEntity<TEPowerCellFactory>(i, j, 4, 4, 18);
		Player localPlayer = Main.LocalPlayer;
		localPlayer.CancelSignsAndChests();
		CalamityPlayer mp = localPlayer.Calamity();
		if (thisFactory == null || thisFactory.ID == mp.CurrentlyViewedFactoryID)
		{
			mp.CurrentlyViewedFactoryID = -1;
			SoundEngine.PlaySound(in SoundID.MenuClose);
		}
		else if (thisFactory != null)
		{
			SoundEngine.PlaySound((mp.CurrentlyViewedFactoryID == -1) ? SoundID.MenuOpen : SoundID.MenuTick);
			mp.CurrentlyViewedFactoryID = thisFactory.ID;
			Main.playerInventory = true;
			Main.recBigList = false;
		}
		Recipe.FindRecipes();
		return true;
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		Tile t = Main.tile[i, j];
		if (t.IsTileActuallyInvisible())
		{
			return false;
		}
		int frameXPos = t.TileFrameX;
		int frameYPos = t.TileFrameY;
		int frameIndex = CalamityUtils.FindTileEntity<TEPowerCellFactory>(i, j, 4, 4, 18)?.AnimationFrame ?? 44;
		frameXPos += frameIndex / 15 * 72;
		frameYPos += frameIndex % 15 * 72;
		Texture2D tex = TextureAssets.Tile[base.Type].Value;
		Vector2 offset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + offset;
		Color drawColor = Lighting.GetColor(i, j);
		if (!t.IsHalfBlock && t.Slope == SlopeType.Solid)
		{
			spriteBatch.Draw(tex, drawOffset, (Rectangle?)new Rectangle(frameXPos, frameYPos, 16, 16), drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		else if (t.IsHalfBlock)
		{
			spriteBatch.Draw(tex, drawOffset + Vector2.UnitY * 8f, (Rectangle?)new Rectangle(frameXPos, frameYPos, 16, 16), drawColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (TileID.Sets.HasOutlines[base.Type] && Main.InSmartCursorHighlightArea(i, j, out var actuallySelected))
		{
			int avgBrightness = (((Color)(ref drawColor)).R + ((Color)(ref drawColor)).G + ((Color)(ref drawColor)).B) / 3;
			if (avgBrightness > 10)
			{
				Color highlightColor = Colors.GetSelectionGlowColor(actuallySelected, avgBrightness);
				spriteBatch.Draw(TextureAssets.HighlightMask[base.Type].Value, drawOffset, (Rectangle?)new Rectangle(frameXPos, frameYPos, 16, 16), highlightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
		return false;
	}
}
