using CalamityMod.Dusts.Furniture;
using CalamityMod.Items.Placeables.FurnitureBotanic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureBotanic;

public class BotanicChest : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpChest(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureBotanic.BotanicChest>(), offset: true);
		AddMapEntry(new Color(191, 142, 111), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureBotanic.BotanicChest>(), FurnitureCommon.GetMapChestName);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<BloomTileGold>(), 0f, 0f, 1, new Color(255, 255, 255));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<BloomTileLeaves>(), 0f, 0f, 1, new Color(255, 255, 255));
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
		return CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureBotanic.BotanicChest>();
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.ChestMouseOver<global::CalamityMod.Items.Placeables.FurnitureBotanic.BotanicChest>(i, j);
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.ChestMouseFar<global::CalamityMod.Items.Placeables.FurnitureBotanic.BotanicChest>(i, j);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Chest.DestroyChest(i, j);
	}

	public override bool RightClick(int i, int j)
	{
		return FurnitureCommon.ChestRightClick(i, j);
	}
}
