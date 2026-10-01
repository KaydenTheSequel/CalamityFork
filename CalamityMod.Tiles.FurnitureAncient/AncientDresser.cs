using CalamityMod.Items.Placeables.FurnitureAncient;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureAncient;

public class AncientDresser : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpDresser(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureAncient.AncientDresser>());
		AddMapEntry(new Color(191, 142, 111), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureAncient.AncientDresser>(), FurnitureCommon.GetMapChestName);
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
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 100, 100));
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
		return CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureAncient.AncientDresser>();
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.DresserMouseOver<global::CalamityMod.Items.Placeables.FurnitureAncient.AncientDresser>();
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.DresserMouseFar<global::CalamityMod.Items.Placeables.FurnitureAncient.AncientDresser>();
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
