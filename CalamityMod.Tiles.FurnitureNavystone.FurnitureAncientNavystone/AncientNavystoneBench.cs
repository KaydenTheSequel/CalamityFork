using CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;

[LegacyName(new string[] { "EutrophicBench" })]
public class AncientNavystoneBench : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpSofa(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone.AncientNavystoneBench>(), lavaImmune: false, bench: true);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 51, 0f, 0f, 1, new Color(54, 69, 72));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info)
	{
		FurnitureCommon.BenchSitInfo(i, j, ref info);
	}

	public override bool RightClick(int i, int j)
	{
		return FurnitureCommon.ChairRightClick(i, j);
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.BenchMouseOver(i, j, ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone.AncientNavystoneBench>());
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return settings.player.IsWithinSnappngRangeToTile(i, j, 40);
	}
}
