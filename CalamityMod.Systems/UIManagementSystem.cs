using System.Collections.Generic;
using CalamityMod.UI;
using CalamityMod.UI.CalamitasEnchants;
using CalamityMod.UI.DraedonsArsenal;
using CalamityMod.UI.DraedonSummoning;
using CalamityMod.UI.ModeIndicator;
using CalamityMod.UI.Rippers;
using CalamityMod.UI.SulphurousWaterMeter;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityMod.Systems;

public class UIManagementSystem : ModSystem
{
	public static Vector2 PreviousMouseWorld;

	public static Vector2 PreviousZoom;

	public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
	{
		int buffDisplayIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Resource Bars");
		if (buffDisplayIndex != -1)
		{
			layers.Insert(buffDisplayIndex, new LegacyGameInterfaceLayer("Cooldown Rack UI", delegate
			{
				CooldownRackUI.Draw(Main.spriteBatch);
				return true;
			}, InterfaceScaleType.UI));
		}
		int mouseIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Mouse Text");
		if (mouseIndex != -1)
		{
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Draedon Hologram", delegate
			{
				LabHologramProjectorUI.Draw(Main.spriteBatch);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Charging Station UI", delegate
			{
				ChargingStationUI.Draw(Main.spriteBatch);
				return true;
			}));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Power Cell Factory UI", delegate
			{
				PowerCellFactoryUI.Draw(Main.spriteBatch);
				return true;
			}));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Mode Indicator UI", delegate
			{
				ModeIndicatorUI.Draw(Main.spriteBatch);
				return true;
			}, InterfaceScaleType.UI));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Speedrun Timer", delegate
			{
				SpeedrunTimerUI.Draw(Main.LocalPlayer);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Rage and Adrenaline UI", delegate
			{
				RipperUI.Draw(Main.spriteBatch, Main.LocalPlayer);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Stealth UI", delegate
			{
				StealthUI.Draw(Main.spriteBatch, Main.LocalPlayer);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Sulphuric Water Poisoning UI", delegate
			{
				SulphurousWaterMeterUI.Draw(Main.spriteBatch, Main.LocalPlayer);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Flight UI", delegate
			{
				FlightBar.Draw(Main.spriteBatch, Main.LocalPlayer);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Charge UI", delegate
			{
				ChargeMeterUI.Draw(Main.spriteBatch, Main.LocalPlayer);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Vis UI", delegate
			{
				VisUI.Draw(Main.spriteBatch, Main.LocalPlayer);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Enchantment Meters", delegate
			{
				EnchantmentMetersUI.Draw(Main.spriteBatch, Main.LocalPlayer);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Calamitas Enchantment", delegate
			{
				CalamitasEnchantUI.Draw(Main.spriteBatch);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Codebreaker Decryption GUI", delegate
			{
				CodebreakerUI.Draw(Main.spriteBatch);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Popup GUIs", delegate
			{
				PopupGUIManager.UpdateAndDraw(Main.spriteBatch);
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Exo Mech Selection", delegate
			{
				if (Main.LocalPlayer.Calamity().AbleToSelectExoMech)
				{
					ExoMechSelectionUI.Draw();
				}
				else
				{
					ExoMechSelectionUI.HoverSoundMechType = null;
				}
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Defense Damage Indicator", delegate
			{
				if (Main.EquipPage != 1 && Main.EquipPage != 2)
				{
					DefenseDamageDisplayUI.Draw(Main.spriteBatch);
				}
				return true;
			}, InterfaceScaleType.None));
			layers.Insert(mouseIndex, new LegacyGameInterfaceLayer("Canvas Painting", delegate
			{
				if (Main.LocalPlayer.Calamity().CurrentlyViewedCanvasID != -1)
				{
					CanvasPaintingUIState.DrawCanvasUI(Main.spriteBatch);
				}
				return true;
			}, InterfaceScaleType.None));
		}
		int invasionIndex = layers.FindIndex((GameInterfaceLayer layer) => layer.Name == "Vanilla: Diagnose Net");
		if (invasionIndex != -1)
		{
			layers.Insert(invasionIndex, new LegacyGameInterfaceLayer("Calamity Invasion UIs", delegate
			{
				InvasionProgressUIManager.UpdateAndDraw(Main.spriteBatch);
				return true;
			}, InterfaceScaleType.UI));
		}
	}
}
