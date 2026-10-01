using CalamityMod.Items.Placeables.FurnitureNavystone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureNavystone;

public class NavystonePlatform : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpPlatform(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureNavystone.NavystonePlatform>());
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 51, 0f, 0f, 1, new Color(54, 69, 72));
		return false;
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[base.Type] = false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
