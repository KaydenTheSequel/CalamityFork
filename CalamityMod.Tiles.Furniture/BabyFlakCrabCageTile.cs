using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Furniture;

public class BabyFlakCrabCageTile : ModTile
{
	public static Asset<Texture2D> topTexture;

	public override void SetStaticDefaults()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			topTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Furniture/TransparentCageTile_Top", (AssetRequestMode)2);
		}
		Main.tileFrameImportant[base.Type] = true;
		Main.tileLavaDeath[base.Type] = true;
		Main.tileSolidTop[base.Type] = true;
		Main.tileTable[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
		TileObjectData.addTile(base.Type);
		base.AnimationFrameHeight = 54;
		AddMapEntry(new Color(122, 217, 232), CalamityUtils.GetItemName<BabyFlakCrabCage>());
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 13);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		int frameAmt = 34;
		int timeNeeded = 6;
		if (frame == 0 || frame == 16)
		{
			timeNeeded = 90;
		}
		if (frame == 28)
		{
			timeNeeded = 60;
		}
		frameCounter++;
		if (frameCounter >= timeNeeded)
		{
			frame++;
			frameCounter = 0;
		}
		if (frame >= frameAmt)
		{
			frame = 0;
		}
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].IsTileActuallyInvisible())
		{
			return false;
		}
		Tile tile = Main.tile[i, j];
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
		Vector2 drawPos = new Vector2((float)(i * 16), (float)(j * 16)) - Main.screenPosition + zero + CalamityUtils.TileDrawOffset + Vector2.UnitY * 2f;
		int animateFrameOffset = Main.tileFrame[base.Type] * base.AnimationFrameHeight;
		int height = 16;
		Color finalColor = CalamityUtils.ApplyPaint(tile.TileColor, Lighting.GetColor(i, j), deepPaintOnly: false);
		Rectangle rect = default(Rectangle);
		((Rectangle)(ref rect))._002Ector((int)tile.TileFrameX, tile.TileFrameY + animateFrameOffset, 16, height);
		if (rect.Y % 54 == 0)
		{
			Vector2 position = drawPos;
			position.Y += 8f;
			Rectangle drawRectangle = rect;
			drawRectangle.Y += 8;
			drawRectangle.Height -= 8;
			Main.spriteBatch.Draw(TextureAssets.Tile[base.Type].Value, position, (Rectangle?)drawRectangle, finalColor, 0f, zero, 1f, (SpriteEffects)0, 0f);
			position = drawPos;
			position.Y -= 2f;
			drawRectangle = rect;
			drawRectangle.Y = 0;
			drawRectangle.Height = 10;
			spriteBatch.Draw(topTexture.Value, position, (Rectangle?)drawRectangle, finalColor, 0f, zero, 1f, (SpriteEffects)0, 0f);
		}
		else
		{
			spriteBatch.Draw(TextureAssets.Tile[base.Type].Value, drawPos, (Rectangle?)rect, finalColor, 0f, zero, 1f, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
