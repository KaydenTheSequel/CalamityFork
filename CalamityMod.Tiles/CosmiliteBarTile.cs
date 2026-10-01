using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class CosmiliteBarTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpBar(ModContent.ItemType<CosmiliteBar>(), new Color(229, 141, 246));
		base.DustType = ModContent.DustType<CosmiliteBarDust>();
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = ModContent.DustType<CosmiliteBarDust>();
		return true;
	}
}
