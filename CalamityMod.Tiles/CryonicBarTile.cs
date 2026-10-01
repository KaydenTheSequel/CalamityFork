using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class CryonicBarTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpBar(ModContent.ItemType<CryonicBar>(), new Color(138, 43, 226));
		base.DustType = 44;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = (Main.rand.NextBool() ? 56 : 73);
		return true;
	}
}
