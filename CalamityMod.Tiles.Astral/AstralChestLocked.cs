using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

public class AstralChestLocked : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpChest(ModContent.ItemType<AstralChest>());
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry0"), MapChestName);
		AddMapEntry(new Color(174, 129, 92), this.GetLocalization("MapEntry1"), MapChestName);
		base.DustType = ModContent.DustType<AstralBasic>();
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = 1;
	}

	public string MapChestName(string name, int i, int j)
	{
		return FurnitureCommon.GetMapChestName(name, i, j);
	}

	public override ushort GetMapOption(int i, int j)
	{
		return (ushort)(Main.tile[i, j].TileFrameX / 36);
	}

	public override LocalizedText DefaultContainerName(int frameX, int frameY)
	{
		return this.GetLocalization("MapEntry" + frameX / 36);
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.ChestMouseOver<AstralChest>(i, j);
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.ChestMouseFar<AstralChest>(i, j);
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
		if (!DownedBossSystem.downedAstrumAureus)
		{
			return false;
		}
		dustType = base.DustType;
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
