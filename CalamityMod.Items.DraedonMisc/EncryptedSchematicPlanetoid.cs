using System.Collections.Generic;
using System.Linq;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.PlaceableTurrets;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Rarities;
using CalamityMod.UI;
using CalamityMod.UI.DraedonLogs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.DraedonMisc;

public class EncryptedSchematicPlanetoid : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.DraedonItems";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 42;
		base.Item.rare = ModContent.RarityType<DarkOrange>();
		base.Item.maxStack = 1;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 4;
	}

	public override void UpdateInventory(Player player)
	{
		if (Main.netMode != 1 && !RecipeUnlockHandler.HasFoundPlanetoidSchematic)
		{
			RecipeUnlockHandler.HasFoundPlanetoidSchematic = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip0");
		if (RecipeUnlockHandler.HasUnlockedT2ArsenalRecipes)
		{
			if (line != null)
			{
				line.Text = CalamityUtils.GetTextValue(LocalizationCategory + ".SchematicUnlocked");
			}
			int insertIndex = list.FindIndex((TooltipLine x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
			if (insertIndex != -1)
			{
				int aureusItem = ModContent.ItemType<AstralChunk>();
				TooltipLine aureusDisplay = new TooltipLine(base.Mod, "CalamityMod:MeleeDisplay", $"[i:{aureusItem}] {CalamityUtils.GetItemName(aureusItem)}");
				aureusDisplay.OverrideColor = new Color(149, 169, 182);
				list.Insert(insertIndex + 1, aureusDisplay);
				int meleeItem = ModContent.ItemType<HydraulicVoltCrasher>();
				TooltipLine meleeDisplay = new TooltipLine(base.Mod, "CalamityMod:MeleeDisplay", $"[i:{meleeItem}] {CalamityUtils.GetItemName(meleeItem)}");
				meleeDisplay.OverrideColor = new Color(31, 242, 245);
				list.Insert(insertIndex + 1, meleeDisplay);
				int rangedItem = ModContent.ItemType<HolofibreImmolator>();
				TooltipLine rangedDisplay = new TooltipLine(base.Mod, "CalamityMod:RangedDisplay", $"[i:{rangedItem}] {CalamityUtils.GetItemName(rangedItem)}");
				rangedDisplay.OverrideColor = new Color(149, 243, 43);
				list.Insert(insertIndex + 2, rangedDisplay);
				int mageItem = ModContent.ItemType<Vulcan>();
				TooltipLine mageDisplay = new TooltipLine(base.Mod, "CalamityMod:MageDisplay", $"[i:{mageItem}] {CalamityUtils.GetItemName(mageItem)}");
				mageDisplay.OverrideColor = new Color(236, 255, 31);
				list.Insert(insertIndex + 3, mageDisplay);
				int summonItem = ModContent.ItemType<MountedScanner>();
				TooltipLine summonDisplay = new TooltipLine(base.Mod, "CalamityMod:SummonDisplay", $"[i:{summonItem}] {CalamityUtils.GetItemName(summonItem)}");
				summonDisplay.OverrideColor = new Color(255, 64, 31);
				list.Insert(insertIndex + 4, summonDisplay);
				int rogueItem = ModContent.ItemType<PulseGrenade>();
				TooltipLine rogueDisplay = new TooltipLine(base.Mod, "CalamityMod:RogueDisplay", $"[i:{rogueItem}] {CalamityUtils.GetItemName(rogueItem)}");
				rogueDisplay.OverrideColor = new Color(201, 41, 255);
				list.Insert(insertIndex + 5, rogueDisplay);
				int turretFireItem = ModContent.ItemType<FireTurret>();
				TooltipLine turretFireDisplay = new TooltipLine(base.Mod, "CalamityMod:CodeDisplay", $"[i:{turretFireItem}] {CalamityUtils.GetItemName(turretFireItem)}");
				turretFireDisplay.OverrideColor = new Color(165, 118, 104);
				list.Insert(insertIndex + 6, turretFireDisplay);
				int turretIceItem = ModContent.ItemType<IceTurret>();
				TooltipLine turretIceDisplay = new TooltipLine(base.Mod, "CalamityMod:CodeDisplay", $"[i:{turretIceItem}] {CalamityUtils.GetItemName(turretIceItem)}");
				turretIceDisplay.OverrideColor = new Color(165, 118, 104);
				list.Insert(insertIndex + 7, turretIceDisplay);
				int turretLaserItem = ModContent.ItemType<LaserTurret>();
				TooltipLine turretLaserDisplay = new TooltipLine(base.Mod, "CalamityMod:CodeDisplay", $"[i:{turretLaserItem}] {CalamityUtils.GetItemName(turretLaserItem)}");
				turretLaserDisplay.OverrideColor = new Color(165, 118, 104);
				list.Insert(insertIndex + 8, turretLaserDisplay);
				int codeItem = ModContent.ItemType<LongRangedSensorArray>();
				TooltipLine machineDisplay = new TooltipLine(base.Mod, "CalamityMod:CodeDisplay", $"[i:{codeItem}] {CalamityUtils.GetItemName(codeItem)}");
				machineDisplay.OverrideColor = new Color(165, 118, 104);
				list.Insert(insertIndex + 9, machineDisplay);
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(10).AddIngredient<DubiousPlating>(10).AddIngredient(170, 50)
			.AddCondition(SchematicRecipe.ConstructRecipeCondition("Planetoid", out var condition), condition)
			.AddTile(16)
			.Register();
	}

	public override bool? UseItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI && RecipeUnlockHandler.HasUnlockedT2ArsenalRecipes)
		{
			PopupGUIManager.FlipActivityOfGUIWithType(typeof(DraedonSchematicPlanetoidGUI));
		}
		return true;
	}
}
