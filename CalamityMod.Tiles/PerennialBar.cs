using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class PerennialBar : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpBar(ModContent.ItemType<global::CalamityMod.Items.Materials.PerennialBar>(), new Color(157, 255, 0));
		base.DustType = 44;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = (Main.rand.NextBool() ? 44 : 157);
		return true;
	}
}
