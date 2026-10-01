using CalamityMod.Items.Placeables.FurnitureWulfrum.FurnitureAnodizedWulfrum;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureWulfrum.FurnitureAnodizedWulfrum;

public class AnodizedWulfrumDoorOpen : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpDoorOpen(ModContent.ItemType<AnodizedWulfrumDoor>(), lavaImmune: true);
		TileID.Sets.CloseDoorID[base.Type] = ModContent.TileType<AnodizedWulfrumDoorClosed>();
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 83, 0f, 0f, 1, new Color(255, 255, 255));
		return false;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<AnodizedWulfrumDoor>();
	}
}
