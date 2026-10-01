using CalamityMod.Items.Placeables.FurnitureDriftwood;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureDriftwood;

public class DriftwoodBookcase : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpBookcase(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureDriftwood.DriftwoodBookcase>(), lavaImmune: true);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 235, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
