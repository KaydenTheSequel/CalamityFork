using CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;

[LegacyName(new string[] { "EutrophicTable" })]
public class AncientNavystoneTable : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpTable(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone.AncientNavystoneTable>());
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
}
