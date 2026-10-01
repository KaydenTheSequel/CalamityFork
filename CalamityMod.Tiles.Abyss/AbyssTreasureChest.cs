using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class AbyssTreasureChest : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpChest(ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.AbyssTreasureChest>());
		AddMapEntry(new Color(71, 49, 41), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.Furniture.AbyssTreasureChest>(), FurnitureCommon.GetMapChestName);
		base.DustType = 33;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = 1;
	}

	public override LocalizedText DefaultContainerName(int frameX, int frameY)
	{
		return CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.Furniture.AbyssTreasureChest>();
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.ChestMouseOver<global::CalamityMod.Items.Placeables.Furniture.AbyssTreasureChest>(i, j);
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.ChestMouseFar<global::CalamityMod.Items.Placeables.Furniture.AbyssTreasureChest>(i, j);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Chest.DestroyChest(i, j);
	}

	public override bool IsLockedChest(int i, int j)
	{
		return Main.tile[i, j].TileFrameX / 36 == 1;
	}

	public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
	{
		if (!global::CalamityMod.World.Abyss.UnlockChests)
		{
			return NPC.downedBoss3;
		}
		return true;
	}

	public override bool RightClick(int i, int j)
	{
		Tile tile = Main.tile[i, j];
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
		return FurnitureCommon.LockedChestRightClick(IsLockedChest(left, top), left, top, i, j);
	}
}
