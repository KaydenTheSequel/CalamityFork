using CalamityMod.Items.Placeables.FurnitureStratus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureStratus;

public class StratusDresser : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpDresser(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureStratus.StratusDresser>());
		AddMapEntry(new Color(191, 142, 111), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureStratus.StratusDresser>(), FurnitureCommon.GetMapChestName);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 130, 150));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 132, 0f, 0f, 1, new Color(255, 255, 255));
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
		return CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureStratus.StratusDresser>();
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.DresserMouseOver<global::CalamityMod.Items.Placeables.FurnitureStratus.StratusDresser>();
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.DresserMouseFar<global::CalamityMod.Items.Placeables.FurnitureStratus.StratusDresser>();
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Chest.DestroyChest(i, j);
	}

	public override bool RightClick(int i, int j)
	{
		return FurnitureCommon.DresserRightClick();
	}
}
