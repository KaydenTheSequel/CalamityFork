using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.GemTech;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class GemTechHeadgear : ModItem, ILocalizedModType, IModType
{
	public const int GemBreakDamageLowerBound = 100;

	public const int GemDamage = 40000;

	public const int GemDamageSoftcapThreshold = 100000;

	public const int GemRegenTime = 1800;

	public const int MeleeShardBaseDamage = 825;

	public const int MeleeShardDelay = 330;

	public const float MeleeDamageBoost = 0.45f;

	public const float MeleeCritBoost = 0.12f;

	public const float MeleeSpeedBoost = 0.26f;

	public const int MaxFlechettes = 8;

	public static float RangedAmmoReduction = 0.7f;

	public const float RangedDamageBoost = 0.5f;

	public const float RangedCritBoost = 0.16f;

	public const int MagicManaBoost = 100;

	public const int NonMagicItemManaRegenBoost = 8;

	public const float MagicDamageBoost = 0.5f;

	public const float MagicCritBoost = 0.16f;

	public const int SummonMinionCountBoost = 4;

	public const float SummonDamageBoost = 0.72f;

	public const int RogueStealthBoost = 130;

	public const float RogueDamageBoost = 0.5f;

	public const float RogueCritBoost = 0.16f;

	public const int BaseGemDefenseBoost = 75;

	public const int BaseGemLifeRegenBoost = 2;

	public const float BaseGemDRBoost = 0.06f;

	public const float BaseGemMovementSpeedBoost = 0.4f;

	public const float BaseGemJumpSpeedBoost = 0.4f;

	public const int AllGemsWeaponUseLifeRegenBoost = 2;

	public const int AllGemsMultiWeaponUseLifeRegenBoost = 3;

	public const int AllGemsLifeRegenBoostTime = 480;

	public const int AllGemsMultiWeaponLifeRegenBoostTime = 150;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 32;
		base.Item.defense = 14;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<GemTechBodyArmor>())
		{
			return legs.type == ModContent.ItemType<GemTechSchynbaulds>();
		}
		return false;
	}

	public static bool HasArmorSet(Player player)
	{
		if (player.armor[0].type == ModContent.ItemType<GemTechHeadgear>() && player.armor[1].type == ModContent.ItemType<GemTechBodyArmor>())
		{
			return player.armor[2].type == ModContent.ItemType<GemTechSchynbaulds>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().GemTechSet = true;
		player.Calamity().wearingRogueArmor = true;
		if (player.Calamity().GemTechState.IsRedGemActive)
		{
			player.Calamity().rogueStealthMax += 1.3f;
		}
		if (player.Calamity().GemTechState.IsYellowGemActive)
		{
			player.GetAttackSpeed<MeleeDamageClass>() += 0.26f;
		}
		Color AbilityBriefColor = Color.Lerp(Color.White, Main.DiscoColor, 0.3f);
		player.setBonus = this.GetLocalization("AbilityBrief").Format(AbilityBriefColor.Hex3());
	}

	public static void ModifySetTooltips(ModItem item, List<TooltipLine> tooltips)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		if (!HasArmorSet(Main.LocalPlayer))
		{
			return;
		}
		int setBonusIndex = tooltips.FindIndex((TooltipLine x) => x.Name == "SetBonus" && x.Mod == "Terraria");
		if (setBonusIndex != -1)
		{
			if (!Main.keyState.PressingShift())
			{
				setBonusIndex++;
				TooltipLine briefDescription = new TooltipLine(item.Mod, "CalamityMod:SetBonus1", CalamityUtils.GetTextValueFromModItem<GemTechHeadgear>("AbilityDescription"));
				briefDescription.OverrideColor = Color.Lerp(Color.White, Main.DiscoColor, 0.5f);
				tooltips.Insert(setBonusIndex, briefDescription);
				setBonusIndex++;
				TooltipLine holdShiftIndicator = new TooltipLine(item.Mod, "CalamityMod:HoldShiftExtensionIndicator", CalamityUtils.GetTextValue("Misc.ShiftToExpand"));
				holdShiftIndicator.OverrideColor = IHoldShiftTooltipItem.DefaultExtensionIndicatorColor;
				tooltips.Insert(setBonusIndex, holdShiftIndicator);
				return;
			}
			setBonusIndex++;
			TooltipLine largerDescription = new TooltipLine(item.Mod, "CalamityMod:SetBonus1", CalamityUtils.GetTextFromModItem<GemTechHeadgear>("GeneralGemInfo").Format(100, 40000, 1800.FramesToSeconds()));
			largerDescription.OverrideColor = Color.Lerp(Color.White, Main.DiscoColor, 0.5f);
			tooltips.Insert(setBonusIndex, largerDescription);
			setBonusIndex++;
			TooltipLine redGemTooltip = new TooltipLine(item.Mod, "CalamityMod:SetBonus2", CalamityUtils.GetTextFromModItem<GemTechHeadgear>("RedGemInfo").Format(130));
			redGemTooltip.OverrideColor = new Color(224, 24, 0);
			tooltips.Insert(setBonusIndex, redGemTooltip);
			setBonusIndex++;
			TooltipLine yellowGemTooltip = new TooltipLine(item.Mod, "CalamityMod:SetBonus3", CalamityUtils.GetTextValueFromModItem<GemTechHeadgear>("YellowGemInfo"));
			yellowGemTooltip.OverrideColor = new Color(237, 170, 43);
			tooltips.Insert(setBonusIndex, yellowGemTooltip);
			setBonusIndex++;
			TooltipLine greenGemTooltip = new TooltipLine(item.Mod, "CalamityMod:SetBonus4", CalamityUtils.GetTextValueFromModItem<GemTechHeadgear>("GreenGemInfo"));
			greenGemTooltip.OverrideColor = new Color(37, 188, 108);
			tooltips.Insert(setBonusIndex, greenGemTooltip);
			setBonusIndex++;
			TooltipLine blueGemTooltip = new TooltipLine(item.Mod, "CalamityMod:SetBonus5", CalamityUtils.GetTextFromModItem<GemTechHeadgear>("BlueGemInfo").Format(4));
			blueGemTooltip.OverrideColor = new Color(37, 119, 206);
			tooltips.Insert(setBonusIndex, blueGemTooltip);
			setBonusIndex++;
			TooltipLine purpleGemTooltip = new TooltipLine(item.Mod, "CalamityMod:SetBonus6", CalamityUtils.GetTextFromModItem<GemTechHeadgear>("PurpleGemInfo").Format(100));
			purpleGemTooltip.OverrideColor = new Color(200, 58, 209);
			tooltips.Insert(setBonusIndex, purpleGemTooltip);
			setBonusIndex++;
			TooltipLine pinkGemTooltip = new TooltipLine(item.Mod, "CalamityMod:SetBonus7", CalamityUtils.GetTextFromModItem<GemTechHeadgear>("PinkGemInfo").Format(75, 2.ToRegenPerSecond()));
			pinkGemTooltip.OverrideColor = new Color(255, 115, 206);
			tooltips.Insert(setBonusIndex, pinkGemTooltip);
			setBonusIndex++;
			TooltipLine liferegenTooltip = new TooltipLine(item.Mod, "CalamityMod:SetBonus8", CalamityUtils.GetTextFromModItem<GemTechHeadgear>("GemBonusInfo").Format(2.ToRegenPerSecond(), 480.FramesToSeconds(), 3.ToRegenPerSecond(), 150.FramesToSeconds()));
			liferegenTooltip.OverrideColor = new Color(230, 230, 230);
			tooltips.Insert(setBonusIndex, liferegenTooltip);
		}
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		ModifySetTooltips(this, tooltips);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ExoPrism>(10).AddIngredient<GalacticaSingularity>(3).AddIngredient<CoreofCalamity>(2)
			.AddTile<DraedonsForge>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<GemTechBodyArmor>())
			.Register();
	}
}
