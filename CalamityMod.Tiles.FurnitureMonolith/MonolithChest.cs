using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureMonolith;

public class MonolithChest : ModTile
{
	public Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpChest(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureMonolith.MonolithChest>());
		AddMapEntry(new Color(191, 142, 111), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureMonolith.MonolithChest>(), FurnitureCommon.GetMapChestName);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<AstralBasic>(), 0f, 0f, 1, new Color(255, 255, 255));
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
		return CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureMonolith.MonolithChest>();
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.ChestMouseOver<global::CalamityMod.Items.Placeables.FurnitureMonolith.MonolithChest>(i, j);
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.ChestMouseFar<global::CalamityMod.Items.Placeables.FurnitureMonolith.MonolithChest>(i, j);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Chest.DestroyChest(i, j);
	}

	public override bool RightClick(int i, int j)
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		Main.mouseRightRelease = false;
		int left = i;
		int top = j;
		if (tile.TileFrameX % 36 != 0)
		{
			left--;
		}
		if (tile.TileFrameY != 0)
		{
			top--;
		}
		if (Main.netMode != 1 && Chest.FindChest(left, top) >= 0 && player.chest < 0)
		{
			SoundStyle style = SoundID.NPCDeath22 with
			{
				Volume = SoundID.NPCDeath22.Volume * 0.5f
			};
			SoundEngine.PlaySound(in style);
		}
		return FurnitureCommon.ChestRightClick(i, j);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
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
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/FurnitureMonolith/MonolithChestGlow", (AssetRequestMode)2);
			}
			Color drawColour = CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, new Color(100, 100, 100, 100));
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y + (float)yOffset) + (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Rectangle frame = default(Rectangle);
			((Rectangle)(ref frame))._002Ector(xPos, yPos + Main.chest[chestIndex].frame * 38, 18, 18);
			Main.spriteBatch.Draw(GlowTexture.Value, drawOffset, (Rectangle?)frame, drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}
}
