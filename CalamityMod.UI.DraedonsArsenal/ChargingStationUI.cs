using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityMod.UI.DraedonsArsenal;

public class ChargingStationUI
{
	public const float MaxPlayerDistance = 160f;

	private const float IconScale = 0.7f;

	private const int GuiWidth = 36;

	private const int GuiHeight = 36;

	private const int SlotSpacing = 8;

	private const float SlotDrawOffsetX = 24f;

	private const float CellDrawOffsetY = -20f;

	private const float PluggedDrawOffsetY = -64f;

	public static void Draw(SpriteBatch spriteBatch)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		Player p = Main.LocalPlayer;
		CalamityPlayer mp = p.Calamity();
		int chargerID = mp.CurrentlyViewedChargerID;
		if (chargerID == -1)
		{
			return;
		}
		if (TileEntity.ByID.TryGetValue(chargerID, out var te) && te is TEChargingStation cast)
		{
			TEChargingStation charger = cast;
			if (!Main.playerInventory || p.chest != -1 || p.channel)
			{
				mp.CurrentlyViewedChargerID = -1;
				return;
			}
			Vector2 chargerWorldCenter = charger.Center;
			if (p.DistanceSQ(chargerWorldCenter) > 25600f)
			{
				SoundEngine.PlaySound(in SoundID.MenuClose);
				mp.CurrentlyViewedChargerID = -1;
				return;
			}
			int powercellID = ModContent.ItemType<DraedonPowerCell>();
			ref Item pluggedItem = ref charger.PluggedItem;
			Item powercell = new Item();
			powercell.TurnToAir();
			if (charger.CellStack > 0 || powercell.maxStack == 0)
			{
				powercell.SetDefaults(powercellID);
				powercell.stack = charger.CellStack;
			}
			Vector2 val = charger.Position.ToWorldCoordinates(0f, 0f);
			DrawWeaponSlot(drawPosition: val + new Vector2(24f, -64f) - Main.screenPosition, spriteBatch: spriteBatch, item: pluggedItem);
			Vector2 powercellDrawPos = val + new Vector2(24f, -20f) - Main.screenPosition;
			CalamityUtils.DrawPowercellSlot(spriteBatch, powercell, powercellDrawPos);
			Rectangle mouseRect = CalamityUtils.MouseHitbox;
			int slotRectX = (int)(val.X - 1f);
			int pluggedSlotRectY = (int)(val.Y + -64f - 18f);
			Rectangle pluggedSlotRect = default(Rectangle);
			((Rectangle)(ref pluggedSlotRect))._002Ector(slotRectX, pluggedSlotRectY, 36, 36);
			int cellSlotRectY = (int)(val.Y + -20f - 18f);
			Rectangle powercellSlotRect = default(Rectangle);
			((Rectangle)(ref powercellSlotRect))._002Ector(slotRectX, cellSlotRectY, 36, 36);
			if (((Rectangle)(ref mouseRect)).Intersects(pluggedSlotRect))
			{
				p.mouseInterface = (Main.blockMouse = true);
				if (!pluggedItem.IsAir)
				{
					Main.HoverItem = pluggedItem.Clone();
				}
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					bool syncRequired = false;
					if (Main.keyState.PressingShift() && p.ItemSpace(pluggedItem).CanTakeItemToPersonalInventory)
					{
						p.QuickSpawnItem(new EntitySource_TileEntity(charger), pluggedItem, pluggedItem.stack);
						pluggedItem.TurnToAir();
						if (!Main.mouseItem.IsAir && Main.mouseItem.Calamity().UsesCharge)
						{
							Utils.Swap(ref Main.mouseItem, ref pluggedItem);
						}
						syncRequired = true;
					}
					else if ((Main.mouseItem.IsAir && !pluggedItem.IsAir) || (!Main.mouseItem.IsAir && Main.mouseItem.Calamity().UsesCharge))
					{
						Utils.Swap(ref Main.mouseItem, ref pluggedItem);
						SoundEngine.PlaySound(in SoundID.Grab);
						syncRequired = true;
					}
					if (syncRequired)
					{
						charger.SendItemSyncPacket();
					}
				}
				Main.instance.MouseTextHackZoom("");
			}
			else
			{
				if (!((Rectangle)(ref mouseRect)).Intersects(powercellSlotRect))
				{
					return;
				}
				if (!powercell.IsAir)
				{
					Main.HoverItem = powercell;
				}
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					short chargerStackDiff = 0;
					bool shiftClicked = false;
					if (Main.keyState.PressingShift() && p.ItemSpace(powercell).CanTakeItemToPersonalInventory)
					{
						p.QuickSpawnItem(p.GetSource_TileInteraction(te.Position.X, te.Position.Y), powercellID, powercell.stack);
						chargerStackDiff = (short)(-powercell.stack);
						shiftClicked = true;
					}
					else if (Main.mouseItem.type == powercellID && powercell.stack < powercell.maxStack)
					{
						int spaceLeft = powercell.maxStack - powercell.stack;
						int cellsToInsert = Math.Min(Main.mouseItem.stack, spaceLeft);
						chargerStackDiff = (short)cellsToInsert;
						Main.mouseItem.stack -= cellsToInsert;
						if (Main.mouseItem.stack == 0)
						{
							Main.mouseItem.TurnToAir();
						}
					}
					else if (Main.mouseItem.IsAir && powercell.stack > 0)
					{
						chargerStackDiff = (short)(-powercell.stack);
						Main.mouseItem.SetDefaults(powercell.type);
						Main.mouseItem.stack = powercell.stack;
						powercell.TurnToAir();
					}
					if (chargerStackDiff != 0)
					{
						if (!shiftClicked)
						{
							SoundEngine.PlaySound(in SoundID.Grab);
						}
						charger.CellStack += chargerStackDiff;
					}
				}
				Main.instance.MouseTextHackZoom("");
				Main.blockMouse = Main.LocalPlayer.HeldItem.pick <= 0;
			}
		}
		else
		{
			mp.CurrentlyViewedChargerID = -1;
		}
	}

	public static void DrawWeaponSlot(SpriteBatch spriteBatch, Item item, Vector2 drawPosition)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D slotBackgroundTex = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/ChargerWeaponSlot", (AssetRequestMode)2).Value;
		spriteBatch.Draw(slotBackgroundTex, drawPosition, (Rectangle?)null, Color.White, 0f, slotBackgroundTex.Size() * 0.5f, 0.7f, (SpriteEffects)0, 0f);
		if (!item.IsAir)
		{
			float inventoryScale = Main.inventoryScale;
			Texture2D itemTexture = TextureAssets.Item[item.type].Value;
			Rectangle itemFrame = ((Main.itemAnimations[item.type] == null) ? itemTexture.Frame() : Main.itemAnimations[item.type].GetFrame(itemTexture));
			float baseScale = 1f;
			Color _ = Color.White;
			ItemSlot.GetItemLight(ref _, ref baseScale, item);
			float scaleRestrictor = 1f;
			if (itemFrame.Width > 46 || itemFrame.Height > 46)
			{
				int restrictingDim = Math.Max(itemFrame.Width, itemFrame.Height);
				scaleRestrictor = 46f / (float)restrictingDim;
			}
			scaleRestrictor *= inventoryScale;
			if (ItemLoader.PreDrawInInventory(item, spriteBatch, drawPosition, itemFrame, item.GetAlpha(Color.White), item.GetColor(Color.White), itemTexture.Size() * 0.5f, scaleRestrictor * baseScale))
			{
				spriteBatch.Draw(itemTexture, drawPosition, (Rectangle?)itemFrame, item.GetAlpha(Color.White), 0f, itemTexture.Size() * 0.5f, scaleRestrictor * baseScale, (SpriteEffects)0, 0f);
				spriteBatch.Draw(itemTexture, drawPosition, (Rectangle?)itemFrame, item.GetColor(Color.White), 0f, itemTexture.Size() * 0.5f, scaleRestrictor * baseScale, (SpriteEffects)0, 0f);
			}
		}
	}
}
