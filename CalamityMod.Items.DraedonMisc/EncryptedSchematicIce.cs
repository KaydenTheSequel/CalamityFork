using System.Collections.Generic;
using System.Linq;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Rarities;
using CalamityMod.UI;
using CalamityMod.UI.DraedonLogs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.DraedonMisc;

public class EncryptedSchematicIce : ModItem, ILocalizedModType, IModType
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
		if (Main.netMode != 1 && !RecipeUnlockHandler.HasFoundIceSchematic)
		{
			RecipeUnlockHandler.HasFoundIceSchematic = true;
			CalamityNetcode.SyncWorld();
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip0");
		if (RecipeUnlockHandler.HasUnlockedT5ArsenalRecipes)
		{
			if (line != null)
			{
				line.Text = CalamityUtils.GetTextValue(LocalizationCategory + ".SchematicUnlocked");
			}
			int insertIndex = list.FindIndex((TooltipLine x) => x.Name == "Tooltip0" && x.Mod == "Terraria");
			if (insertIndex != -1)
			{
				int meleeItem = ModContent.ItemType<Phaseslayer>();
				TooltipLine meleeDisplay = new TooltipLine(base.Mod, "CalamityMod:MeleeDisplay", $"[i:{meleeItem}] {CalamityUtils.GetItemName(meleeItem)}");
				meleeDisplay.OverrideColor = new Color(255, 64, 31);
				list.Insert(insertIndex + 1, meleeDisplay);
				int rangedItem = ModContent.ItemType<PulseRifle>();
				TooltipLine rangedDisplay = new TooltipLine(base.Mod, "CalamityMod:RangedDisplay", $"[i:{rangedItem}] {CalamityUtils.GetItemName(rangedItem)}");
				rangedDisplay.OverrideColor = new Color(201, 41, 255);
				list.Insert(insertIndex + 2, rangedDisplay);
				int rangedItem2 = ModContent.ItemType<TheAnomalysNanogun>();
				TooltipLine rangedDisplay2 = new TooltipLine(base.Mod, "CalamityMod:RangedDisplay2", $"[i:{rangedItem2}] {CalamityUtils.GetItemName(rangedItem2)}");
				rangedDisplay2.OverrideColor = new Color(255, 64, 31);
				list.Insert(insertIndex + 3, rangedDisplay2);
				int mageItem = ModContent.ItemType<TeslaCannon>();
				TooltipLine mageDisplay = new TooltipLine(base.Mod, "CalamityMod:MageDisplay", $"[i:{mageItem}] {CalamityUtils.GetItemName(mageItem)}");
				mageDisplay.OverrideColor = new Color(31, 242, 245);
				list.Insert(insertIndex + 4, mageDisplay);
				int summonItem = ModContent.ItemType<PoleWarper>();
				TooltipLine summonDisplay = new TooltipLine(base.Mod, "CalamityMod:SummonDisplay", $"[i:{summonItem}] {CalamityUtils.GetItemName(summonItem)}");
				summonDisplay.OverrideColor = new Color(236, 255, 31);
				list.Insert(insertIndex + 5, summonDisplay);
				int rogueItem = ModContent.ItemType<PlasmaGrenade>();
				TooltipLine rogueDisplay = new TooltipLine(base.Mod, "CalamityMod:RogueDisplay", $"[i:{rogueItem}] {CalamityUtils.GetItemName(rogueItem)}");
				rogueDisplay.OverrideColor = new Color(149, 243, 43);
				list.Insert(insertIndex + 6, rogueDisplay);
				int codeItem = ModContent.ItemType<AuricQuantumCoolingCell>();
				TooltipLine machineDisplay = new TooltipLine(base.Mod, "CalamityMod:CodeDisplay", $"[i:{codeItem}] {CalamityUtils.GetItemName(codeItem)}");
				machineDisplay.OverrideColor = new Color(255, 215, 0);
				list.Insert(insertIndex + 7, machineDisplay);
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(10).AddIngredient<DubiousPlating>(10).AddIngredient(170, 50)
			.AddCondition(SchematicRecipe.ConstructRecipeCondition("Ice", out var condition), condition)
			.AddTile(16)
			.Register();
	}

	public override bool? UseItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI && RecipeUnlockHandler.HasUnlockedT5ArsenalRecipes)
		{
			PopupGUIManager.FlipActivityOfGUIWithType(typeof(DraedonSchematicIceGUI));
		}
		return true;
	}
}
