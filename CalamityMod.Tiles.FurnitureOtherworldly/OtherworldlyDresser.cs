using CalamityMod.Dusts.Furniture;
using CalamityMod.Items.Placeables.FurnitureOtherworldly;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureOtherworldly;

[LegacyName(new string[] { "OccultDresser" })]
public class OtherworldlyDresser : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpDresser(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureOtherworldly.OtherworldlyDresser>());
		AddMapEntry(new Color(191, 142, 111), CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureOtherworldly.OtherworldlyDresser>(), FurnitureCommon.GetMapChestName);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(125, 94, 128));
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, ModContent.DustType<OtherworldlyTileCloth>(), 0f, 0f, 1, new Color(255, 255, 255));
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
		return CalamityUtils.GetItemName<global::CalamityMod.Items.Placeables.FurnitureOtherworldly.OtherworldlyDresser>();
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.DresserMouseOver<global::CalamityMod.Items.Placeables.FurnitureOtherworldly.OtherworldlyDresser>();
	}

	public override void MouseOverFar(int i, int j)
	{
		FurnitureCommon.DresserMouseFar<global::CalamityMod.Items.Placeables.FurnitureOtherworldly.OtherworldlyDresser>();
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
