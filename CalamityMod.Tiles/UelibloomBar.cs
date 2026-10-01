using CalamityMod.Dusts.Furniture;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class UelibloomBar : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpBar(ModContent.ItemType<global::CalamityMod.Items.Materials.UelibloomBar>(), new Color(134, 209, 102));
		base.DustType = ModContent.DustType<BloomTileLeaves>();
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = (Main.rand.NextBool() ? ModContent.DustType<BloomTileLeaves>() : ModContent.DustType<BloomTileGold>());
		return true;
	}
}
