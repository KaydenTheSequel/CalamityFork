using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

[LegacyName(new string[] { "ChaoticBarPlaced" })]
public class ScoriaBarPlaced : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		this.SetUpBar(ModContent.ItemType<ScoriaBar>(), new Color(255, 165, 0));
		base.DustType = 87;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = (Main.rand.NextBool() ? 87 : 6);
		return true;
	}
}
