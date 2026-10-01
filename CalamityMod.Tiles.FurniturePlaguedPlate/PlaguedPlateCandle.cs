using CalamityMod.Items.Placeables.FurniturePlagued;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurniturePlaguedPlate;

public class PlaguedPlateCandle : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpCandle(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurniturePlagued.PlaguedPlateCandle>(), lavaImmune: true);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 178, 0f, 0f, 1, new Color(255, 255, 255));
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
			r = 0.4f;
			g = 1f;
			b = 0.3f;
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
		FurnitureCommon.LightHitWire(base.Type, i, j, 1, 1);
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<global::CalamityMod.Items.Placeables.FurniturePlagued.PlaguedPlateCandle>();
	}

	public override bool RightClick(int i, int j)
	{
		FurnitureCommon.RightClickBreak(i, j);
		return true;
	}
}
