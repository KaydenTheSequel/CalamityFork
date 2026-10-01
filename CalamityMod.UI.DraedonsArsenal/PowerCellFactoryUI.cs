using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonsArsenal;

public class PowerCellFactoryUI
{
	public const float MaxPlayerDistance = 160f;

	private const int GuiWidth = 36;

	private const int GuiHeight = 36;

	private const float SlotDrawOffsetX = 32f;

	private const float SlotDrawOffsetY = -14f;

	public static void Draw(SpriteBatch spriteBatch)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		Player p = Main.LocalPlayer;
		CalamityPlayer mp = p.Calamity();
		int factoryID = mp.CurrentlyViewedFactoryID;
		if (factoryID == -1)
		{
			return;
		}
		if (TileEntity.ByID.TryGetValue(factoryID, out var te) && te is TEPowerCellFactory cast)
		{
			TEPowerCellFactory factory = cast;
			if (!Main.playerInventory || p.chest != -1 || p.channel)
			{
				mp.CurrentlyViewedFactoryID = -1;
				return;
			}
			Vector2 factoryWorldCenter = factory.Center;
			if (p.DistanceSQ(factoryWorldCenter) > 25600f)
			{
				SoundEngine.PlaySound(in SoundID.MenuClose);
				mp.CurrentlyViewedFactoryID = -1;
				return;
			}
			int powercellID = ModContent.ItemType<DraedonPowerCell>();
			Item powercell = new Item();
			powercell.TurnToAir();
			if (factory.CellStack > 0)
			{
				powercell.SetDefaults(powercellID);
				powercell.stack = factory.CellStack;
			}
			Vector2 val = factory.Position.ToWorldCoordinates(0f, 0f);
			CalamityUtils.DrawPowercellSlot(drawPosition: val + new Vector2(32f, -14f) - Main.screenPosition, spriteBatch: spriteBatch, item: powercell);
			Rectangle mouseRect = CalamityUtils.MouseHitbox;
			int slotRectX = (int)(val.X - 1f);
			int cellSlotRectY = (int)(val.Y + -14f - 18f);
			Rectangle powercellSlotRect = default(Rectangle);
			((Rectangle)(ref powercellSlotRect))._002Ector(slotRectX, cellSlotRectY, 36, 36);
			if (!((Rectangle)(ref mouseRect)).Intersects(powercellSlotRect) || powercell.stack <= 0)
			{
				return;
			}
			p.mouseInterface = (Main.blockMouse = true);
			if (!powercell.IsAir)
			{
				Main.HoverItem = powercell;
			}
			int cellsGrabbed = 0;
			bool shiftClicked = false;
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				if (Main.keyState.PressingShift() && p.ItemSpace(powercell).CanTakeItemToPersonalInventory)
				{
					cellsGrabbed = powercell.stack;
					shiftClicked = true;
					p.QuickSpawnItem(p.GetSource_TileInteraction(te.Position.X, te.Position.Y), powercellID, cellsGrabbed);
				}
				else
				{
					cellsGrabbed = TryGrabCell(ref Main.mouseItem, ref powercell);
					if (cellsGrabbed == 0)
					{
						cellsGrabbed = TryGrabCell(ref p.inventory[Main.LocalPlayer.selectedItem], ref powercell);
					}
				}
			}
			if (cellsGrabbed > 0)
			{
				factory.CellStack -= (short)cellsGrabbed;
				if (!shiftClicked)
				{
					SoundEngine.PlaySound(in SoundID.Grab);
				}
			}
			Main.instance.MouseTextHackZoom("");
		}
		else
		{
			mp.CurrentlyViewedFactoryID = -1;
		}
	}

	public static int TryGrabCell(ref Item playerHandItem, ref Item cell)
	{
		Main.playerInventory = true;
		Main.recBigList = false;
		if (playerHandItem.IsAir)
		{
			playerHandItem.SetDefaults(cell.type);
			playerHandItem.stack = cell.stack;
			cell.TurnToAir();
			return playerHandItem.stack;
		}
		if (playerHandItem.type == cell.type)
		{
			int spaceLeft = playerHandItem.maxStack - playerHandItem.stack;
			int cellsToTake = Math.Min(cell.stack, spaceLeft);
			if (cellsToTake <= 0)
			{
				return 0;
			}
			playerHandItem.stack += cellsToTake;
			cell.stack -= cellsToTake;
			if (cell.stack == 0)
			{
				cell.TurnToAir();
			}
			return cellsToTake;
		}
		return 0;
	}
}
