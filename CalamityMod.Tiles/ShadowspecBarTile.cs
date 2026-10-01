using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class ShadowspecBarTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpBar(ModContent.ItemType<ShadowspecBar>(), new Color(128, 41, 149));
		base.DustType = ModContent.DustType<ShadowspecBarDust>();
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = ModContent.DustType<ShadowspecBarDust>();
		return true;
	}
}
