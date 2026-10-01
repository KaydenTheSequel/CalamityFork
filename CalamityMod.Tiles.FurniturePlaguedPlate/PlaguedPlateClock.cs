using CalamityMod.Items.Placeables.FurniturePlagued;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurniturePlaguedPlate;

public class PlaguedPlateClock : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpClock(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurniturePlagued.PlaguedPlateClock>(), lavaImmune: true);
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override bool RightClick(int x, int y)
	{
		return FurnitureCommon.ClockRightClick();
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

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer)
		{
			Main.SceneMetrics.HasClock = true;
		}
	}

	public override void MouseOver(int i, int j)
	{
		FurnitureCommon.MouseOver(i, j, ModContent.ItemType<global::CalamityMod.Items.Placeables.FurniturePlagued.PlaguedPlateClock>());
	}
}
