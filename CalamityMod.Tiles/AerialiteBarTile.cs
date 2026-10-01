using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class AerialiteBarTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpBar(ModContent.ItemType<AerialiteBar>(), new Color(141, 232, 246));
		base.DustType = 187;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = (Main.rand.NextBool() ? 187 : 16);
		return true;
	}
}
