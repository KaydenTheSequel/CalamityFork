using System;
using CalamityMod.Items.Placeables.FurnitureSacrilegious;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureSacrilegious;

public class MonolithOfTheAccursedTile : ModTile
{
	public Asset<Texture2D> IconRightTexture;

	public override void SetStaticDefaults()
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 3;
		TileObjectData.addTile(base.Type);
		TileID.Sets.HasOutlines[base.Type] = true;
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		AddMapEntry(new Color(43, 19, 42), CalamityUtils.GetItemName<MonolithOfTheAccursed>());
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 60, 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 8, 0f, 0f, 1, new Color(100, 100, 100));
		return false;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX > 36)
		{
			r = 1.2f;
			g = 0.2f;
			b = 0.2f;
		}
		else
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}
	}

	private void ToggleMode(int i, int j)
	{
		int tileX = 2;
		int tileY = 3;
		Tile tile = Main.tile[i, j];
		int x = i - tile.TileFrameX / 18 % tileX;
		tile = Main.tile[i, j];
		int y = j - tile.TileFrameY / 18 % tileY;
		for (int l = x; l < x + tileX; l++)
		{
			for (int m = y; m < y + tileY; m++)
			{
				tile = Main.tile[l, m];
				if (!tile.HasTile)
				{
					continue;
				}
				tile = Main.tile[l, m];
				if (tile.TileType == base.Type)
				{
					tile = Main.tile[l, m];
					if (tile.TileFrameX < 36 * tileX)
					{
						tile = Main.tile[l, m];
						tile.TileFrameX += (short)(18 * tileX);
					}
					else
					{
						tile = Main.tile[l, m];
						tile.TileFrameX -= (short)(36 * tileX);
					}
				}
			}
		}
		if (Wiring.running)
		{
			for (int k = 0; k < tileX; k++)
			{
				for (int n = 0; n < tileY; n++)
				{
					Wiring.SkipWire(x + k, y + n);
				}
			}
		}
		if (Main.netMode != 0)
		{
			NetMessage.SendTileSquare(-1, x, y, tileX, tileY);
		}
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		ToggleMode(i, j);
		SoundEngine.PlaySound(in SoundID.MenuTick);
		return true;
	}

	public override void HitWire(int i, int j)
	{
		ToggleMode(i, j);
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (Main.tile[i, j].TileFrameX >= 36)
		{
			Player player = Main.LocalPlayer;
			if (player != null && player.active)
			{
				int resetAmt = ((Main.tile[i, j].TileFrameX < 72) ? 20 : 40);
				player.Calamity().monolithAccursedShader = resetAmt;
			}
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.MouseOver(i, j, ModContent.ItemType<MonolithOfTheAccursed>());
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].TileFrameX >= 36 && !Main.tile[i, j].IsTileActuallyInvisible())
		{
			if (IconRightTexture == null)
			{
				IconRightTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureSacrilegious/MonolithOfTheAccursedTile_IconRight", (AssetRequestMode)2);
			}
			Texture2D texture = IconRightTexture.Value;
			Tile tile = Main.tile[i, j];
			int xPos = tile.TileFrameX;
			int yPos = tile.TileFrameY;
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			float xOffset = ((Main.tile[i, j].TileFrameX > 70) ? 52f : 16f);
			Vector2 correction = default(Vector2);
			((Vector2)(ref correction))._002Ector(xOffset, -10f);
			float yOffset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f) / 5f) * 2f;
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y + yOffset) + zero + correction;
			Rectangle rect = default(Rectangle);
			((Rectangle)(ref rect))._002Ector(xPos, yPos, texture.Width, texture.Height);
			Color color = default(Color);
			((Color)(ref color))._002Ector(100, 100, 100, 0);
			Vector2 origin = rect.Size() / 2f;
			for (int c = 0; c < 5; c++)
			{
				spriteBatch.Draw(texture, drawOffset, (Rectangle?)rect, color, 0f, origin, 1f, (SpriteEffects)0, 0f);
			}
		}
	}
}
