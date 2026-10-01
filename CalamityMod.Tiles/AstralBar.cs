using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class AstralBar : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpBar(ModContent.ItemType<global::CalamityMod.Items.Materials.AstralBar>(), new Color(47, 66, 90));
		base.DustType = ModContent.DustType<AstralBlue>();
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = (Main.rand.NextBool() ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>());
		return true;
	}
}
