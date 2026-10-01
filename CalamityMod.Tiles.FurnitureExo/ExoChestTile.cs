using CalamityMod.Items.Placeables.FurnitureExo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureExo;

public class ExoChestTile : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpChest(ModContent.ItemType<ExoChest>(), offset: true, 2);
		AddMapEntry(new Color(71, 95, 114), CalamityUtils.GetItemName<ExoChest>(), FurnitureCommon.GetMapChestName);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 107, 0f, 0f, 1, new Color(255, 255, 255));
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
		return CalamityUtils.GetItemName<ExoChest>();
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.ChestMouseOver<ExoChest>(i, j);
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.ChestMouseFar<ExoChest>(i, j);
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
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (tile.IsTileActuallyInvisible())
		{
			return;
		}
		int xPos = tile.TileFrameX;
		int yPos = tile.TileFrameY;
		int chestIndex = Chest.FindChest(i - xPos / 18, j - yPos / 18);
		if (chestIndex != -1)
		{
			int yOffset = TileObjectData.GetTileData(tile).DrawYOffset;
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureExo/ExoChestGlow", (AssetRequestMode)2);
			}
			Texture2D glowmask = GlowTexture.Value;
			Color drawColour = Color.White;
			Vector2 drawPosition = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y + (float)yOffset) + (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Rectangle frame = default(Rectangle);
			((Rectangle)(ref frame))._002Ector(xPos, yPos + Main.chest[chestIndex].frame * 38, 18, 18);
			Main.spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)frame, drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}
}
