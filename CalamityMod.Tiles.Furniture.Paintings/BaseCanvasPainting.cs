using System;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.TileEntities;
using CalamityMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture.Paintings;

public abstract class BaseCanvasPainting : ModTile
{
	public static Asset<Texture2D> border;

	public static Asset<Texture2D> corner;

	public virtual float Scale => 0.4f;

	public override void Load()
	{
		border = ModContent.Request<Texture2D>("CalamityMod/Tiles/Furniture/Paintings/CalamityCanvasBorder", (AssetRequestMode)2);
		corner = ModContent.Request<Texture2D>("CalamityMod/Tiles/Furniture/Paintings/CalamityCanvasCorner", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileSpelunker[base.Type] = true;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.Width = 5;
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.CoordinateHeights = new int[5] { 18, 18, 18, 18, 18 };
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TECanvasPainting>().Hook_AfterPlacement, -1, 0, processedCoordinates: false);
		TileObjectData.addTile(base.Type);
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		TileID.Sets.FramesOnKillWall[base.Type] = true;
		TileID.Sets.DisableSmartCursor[base.Type] = true;
		AddMapEntry(new Color(99, 50, 30), Language.GetText("MapObject.Painting"));
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Main.LocalPlayer.CancelSignsAndChests();
		TECanvasPainting cube = CalamityUtils.FindTileEntity<TECanvasPainting>(i, j, 5, 5);
		if (cube != null)
		{
			CanvasPaintingUIState.ResetVars();
			Main.LocalPlayer.Calamity().CurrentlyViewedCanvasID = cube.ID;
			Main.LocalPlayer.Calamity().CurrentlyViewedCanvasType = base.Type;
			SoundEngine.PlaySound(in SoundID.MenuOpen);
			Main.playerInventory = true;
			Main.recBigList = false;
		}
		Recipe.FindRecipes();
		return false;
	}

	public override void MouseOver(int i, int j)
	{
		Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<CalamityCanvas2023>();
		Main.LocalPlayer.noThrow = 2;
		Main.LocalPlayer.cursorItemIconEnabled = true;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		Tile t = Main.tile[i, j];
		int left = i - t.TileFrameX % 90 / 18;
		int top = j - t.TileFrameY % 90 / 18;
		CalamityUtils.FindTileEntity<TECanvasPainting>(i, j, 5, 5, 18)?.Kill(left, top);
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		Tile t = Main.tile[i, j];
		Texture2D texture = TextureAssets.Tile[base.Type].Value;
		TECanvasPainting cube = CalamityUtils.FindTileEntity<TECanvasPainting>(i, j, 1, 1);
		Vector2 pos = new Vector2((float)(i * 16), (float)(j * 16)) + CalamityUtils.TileDrawOffset;
		if (cube != null && t.TileFrameX == 0)
		{
			int fPX = (int)cube.framePosition.X;
			int fPY = (int)cube.framePosition.Y;
			int scale = (int)((float)texture.Width * 0.1f * cube.scale);
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null);
			spriteBatch.Draw(texture, pos - Main.screenPosition, (Rectangle?)new Rectangle(fPX, fPY, scale, scale), Lighting.GetColor(i, j), 0f, new Vector2(0f, 0f), 1f / cube.scale * Scale, (SpriteEffects)0, 0f);
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, (Effect)null);
		}
		if (t.TileFrameX == 0 && t.TileFrameY == 0)
		{
			DrawBorders(spriteBatch, pos - Main.screenPosition, new Point(i, j));
		}
		return false;
	}

	public static void DrawBorders(SpriteBatch spriteBatch, Vector2 pos, Point cords)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = border.Value;
		Texture2D cornerTex = corner.Value;
		ushort canvasID = (ushort)Main.LocalPlayer.Calamity().CurrentlyViewedCanvasType;
		int commonDim = 8;
		int finalCord = 72;
		int size = 80;
		Color light = Lighting.GetColor(cords.X, cords.Y);
		bool drawTop = true;
		bool drawLeft = true;
		bool drawRight = true;
		bool drawBottom = true;
		bool drawTopLeft = false;
		bool drawTopRight = false;
		bool drawBottomLeft = false;
		bool drawBottomRight = false;
		Point bottomRight = default(Point);
		((Point)(ref bottomRight))._002Ector(cords.X + 4, cords.Y + 4);
		Tile left = CalamityUtils.ParanoidTileRetrieval(cords.X - 1, cords.Y);
		Tile top = CalamityUtils.ParanoidTileRetrieval(cords.X, cords.Y - 1);
		Tile t = CalamityUtils.ParanoidTileRetrieval(bottomRight.X + 1, bottomRight.Y);
		Tile bottom = CalamityUtils.ParanoidTileRetrieval(bottomRight.X, bottomRight.Y + 1);
		bool validTop = ValidCanvasFrame(top, 0, size, canvasID);
		bool num = ValidCanvasFrame(t, 0, size, canvasID);
		bool validLeft = ValidCanvasFrame(left, finalCord, 0, canvasID);
		bool num2 = ValidCanvasFrame(bottom, finalCord, 0, canvasID);
		if (validTop)
		{
			drawTop = false;
		}
		if (num2)
		{
			drawBottom = false;
		}
		if (num)
		{
			drawRight = false;
			Tile topright = CalamityUtils.ParanoidTileRetrieval(bottomRight.X + 1, cords.Y - 1);
			Tile bottomright = CalamityUtils.ParanoidTileRetrieval(bottomRight.X + 1, bottomRight.Y + 1);
			if (!drawTop && !ValidCanvasFrame(topright, 0, size, canvasID))
			{
				drawTopRight = true;
			}
			if (!drawBottom && !ValidCanvasFrame(bottomright, 0, 0, canvasID))
			{
				drawBottomRight = true;
			}
		}
		if (validLeft)
		{
			drawLeft = false;
			Tile topleft = CalamityUtils.ParanoidTileRetrieval(cords.X - 1, cords.Y - 1);
			Tile bottomleft = CalamityUtils.ParanoidTileRetrieval(cords.X - 1, bottomRight.Y + 1);
			if (!drawTop && !ValidCanvasFrame(topleft, finalCord, size, canvasID))
			{
				drawTopLeft = true;
			}
			if (!drawBottom && !ValidCanvasFrame(bottomleft, finalCord, 0, canvasID))
			{
				drawBottomLeft = true;
			}
		}
		if (drawBottom)
		{
			spriteBatch.Draw(texture, pos + Vector2.UnitY * (float)finalCord, (Rectangle?)new Rectangle(0, texture.Height - commonDim, texture.Width, commonDim), light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
		}
		if (drawTop)
		{
			spriteBatch.Draw(texture, pos, (Rectangle?)new Rectangle(0, 0, texture.Width, commonDim), light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
		}
		if (drawRight)
		{
			spriteBatch.Draw(texture, pos + Vector2.UnitX * (float)finalCord, (Rectangle?)new Rectangle(texture.Width - commonDim, commonDim, commonDim, texture.Height - 2 * commonDim), light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
		}
		if (drawLeft)
		{
			spriteBatch.Draw(texture, pos, (Rectangle?)new Rectangle(0, commonDim, commonDim, texture.Height - 2 * commonDim), light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
		}
		if (drawTop & drawLeft)
		{
			spriteBatch.Draw(cornerTex, pos, (Rectangle?)null, light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
		}
		if (drawTop & drawRight)
		{
			spriteBatch.Draw(cornerTex, pos + Vector2.UnitX * (float)finalCord, (Rectangle?)null, light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)1, 0f);
		}
		if (drawBottom & drawLeft)
		{
			spriteBatch.Draw(cornerTex, pos + Vector2.UnitY * (float)finalCord, (Rectangle?)null, light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)2, 0f);
		}
		if (drawBottom & drawRight)
		{
			spriteBatch.Draw(cornerTex, pos + Vector2.One * (float)size, (Rectangle?)null, light, (float)Math.PI, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
		}
		if (drawTopLeft)
		{
			spriteBatch.Draw(cornerTex, pos + corner.Size(), (Rectangle?)null, light, (float)Math.PI, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
		}
		if (drawTopRight)
		{
			spriteBatch.Draw(cornerTex, pos + Vector2.UnitX * (float)finalCord, (Rectangle?)null, light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)2, 0f);
		}
		if (drawBottomLeft)
		{
			spriteBatch.Draw(cornerTex, pos + Vector2.UnitY * (float)finalCord, (Rectangle?)null, light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)1, 0f);
		}
		if (drawBottomRight)
		{
			spriteBatch.Draw(cornerTex, pos - corner.Size() + Vector2.One * (float)size, (Rectangle?)null, light, 0f, new Vector2(0f, 0f), 1f, (SpriteEffects)0, 0f);
		}
	}

	public static bool ValidCanvasFrame(Tile t, int frameX, int frameY, ushort canvasID)
	{
		if (t.HasTile && t.TileType == canvasID && t.TileFrameX == frameX)
		{
			return t.TileFrameY == frameY;
		}
		return false;
	}
}
