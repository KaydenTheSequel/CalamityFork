using CalamityMod.Items.Placeables.FurnitureAbyss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureAbyss;

public class AbyssBookcase : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpBookcase(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureAbyss.AbyssBookcase>(), lavaImmune: true);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(100, 130, 150));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
