using CalamityMod.Items.Placeables.FurnitureNavystone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureNavystone;

public class NavystoneLamp : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpLamp(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureNavystone.NavystoneLamp>());
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 51, 0f, 0f, 1, new Color(54, 69, 72));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 0.5f;
			g = 1f;
			b = 1f;
		}
		else
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 1, 3);
	}
}
