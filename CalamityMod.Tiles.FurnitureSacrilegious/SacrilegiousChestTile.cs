using CalamityMod.Items.Placeables.FurnitureSacrilegious;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureSacrilegious;

public class SacrilegiousChestTile : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpChest(ModContent.ItemType<SacrilegiousChest>(), offset: true);
		AddMapEntry(new Color(43, 19, 42), CalamityUtils.GetItemName<SacrilegiousChest>(), FurnitureCommon.GetMapChestName);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 8, 0f, 0f, 1, new Color(255, 255, 255));
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

	public override LocalizedText DefaultContainerName(int frameX, int frameY)
	{
		return CalamityUtils.GetItemName<SacrilegiousChest>();
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.ChestMouseOver<SacrilegiousChest>(i, j);
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.ChestMouseFar<SacrilegiousChest>(i, j);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Chest.DestroyChest(i, j);
	}

	public override bool RightClick(int i, int j)
	{
		return FurnitureCommon.ChestRightClick(i, j);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		int seed = 1;
		int itemAmt = 0;
		int index = FindChestIndex(i, j, ref seed);
		if (index >= 0)
		{
			itemAmt = CountItems(Main.chest[index]);
		}
		Tile tile = Main.tile[i, j];
		if (!tile.IsTileActuallyInvisible())
		{
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureSacrilegious/SacrilegiousChestTileGlow", (AssetRequestMode)2);
			}
			Texture2D texture = GlowTexture.Value;
			int x = i;
			int y = j;
			switch (seed)
			{
			case 2:
				y = j - 1;
				break;
			case 3:
				x = i - 1;
				break;
			case 4:
				x = i - 1;
				y = j - 1;
				break;
			}
			ulong seeding = Main.TileFrameSeed ^ (ulong)(((long)y << 32) | (uint)x);
			int yOffset = TileObjectData.GetTileData(tile).DrawYOffset;
			Vector2 drawOffset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
			Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y + (float)yOffset) + drawOffset;
			int alpha = 255 - (int)(MathHelper.Lerp(0f, 7f, (float)itemAmt / 40f) * 15f);
			Color color = default(Color);
			((Color)(ref color))._002Ector(120, 100, 100, alpha);
			int loopAmt = ((itemAmt > 0) ? ((itemAmt - 1) / 10 + 4) : 4);
			float shakeAmt = MathHelper.Lerp(0f, 0.3f, (float)itemAmt / 40f);
			if (itemAmt == 0)
			{
				loopAmt = 0;
			}
			Vector2 shake = default(Vector2);
			for (int c = 0; c < loopAmt; c++)
			{
				float shakeX = (float)Utils.RandomInt(ref seeding, -5, 5) * shakeAmt;
				float shakeY = (float)Utils.RandomInt(ref seeding, -5, 5) * shakeAmt;
				((Vector2)(ref shake))._002Ector(shakeX, shakeY);
				spriteBatch.Draw(texture, drawPosition + shake, (Rectangle?)new Rectangle((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16), color, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	private int FindChestIndex(int i, int j, ref int seed)
	{
		int index = Chest.FindChest(i, j);
		if (index < 0 && Main.tile[i, j - 1].TileType == ModContent.TileType<SacrilegiousChestTile>())
		{
			index = Chest.FindChest(i, j - 1);
			seed = 2;
		}
		if (index < 0 && Main.tile[i - 1, j].TileType == ModContent.TileType<SacrilegiousChestTile>())
		{
			index = Chest.FindChest(i - 1, j);
			seed = 3;
		}
		if (index < 0 && Main.tile[i - 1, j - 1].TileType == ModContent.TileType<SacrilegiousChestTile>())
		{
			index = Chest.FindChest(i - 1, j - 1);
			seed = 4;
		}
		return index;
	}

	private int CountItems(Chest chest)
	{
		if (chest == null)
		{
			return -1;
		}
		int amt = 0;
		for (int i = 0; i < 40; i++)
		{
			if (chest.item[i] != null && !chest.item[i].IsAir && chest.item[i].stack > 0)
			{
				amt++;
			}
		}
		return amt;
	}

	private byte Average(byte a, byte b)
	{
		return (byte)((a + b) / 2);
	}
}
