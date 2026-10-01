using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Auric;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AuricTeslaHoodedFacemask" })]
public class AuricTeslaHeadRanged : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.22f;

	public static int RangedCritBoost = 20;

	public static float AmmoReduction = 0.7f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), RangedCritBoost, (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.defense = 39;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<AuricTeslaBodyArmor>())
		{
			return legs.type == ModContent.ItemType<AuricTeslaCuisses>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalizedValue("SetBonus");
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.tarraSet = true;
		modPlayer.tarraRanged = true;
		modPlayer.bloodflareSet = true;
		modPlayer.bloodflareRanged = true;
		modPlayer.godSlayer = true;
		modPlayer.godSlayerRanged = true;
		modPlayer.auricSet = true;
		player.crimsonRegen = true;
		if (modPlayer.godSlayerDashHotKeyPressed || (player.dashDelay != 0 && modPlayer.LastUsedDashID == GodslayerArmorDash.ID))
		{
			modPlayer.DeferredDashID = GodslayerArmorDash.ID;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		if (Main.keyState.PressingShift())
		{
			if (!AuricTeslaBodyArmor.holdingShift)
			{
				AuricTeslaBodyArmor.holdingShift = true;
				AuricTeslaBodyArmor.setBonusTooltipNumber++;
				if (AuricTeslaBodyArmor.setBonusTooltipNumber > 3)
				{
					AuricTeslaBodyArmor.setBonusTooltipNumber = 1;
				}
			}
			{
				foreach (TooltipLine line in tooltips)
				{
					if (line.Name == "SetBonus")
					{
						Color[] armorColors = (Color[])(object)new Color[3]
						{
							AuricTeslaBodyArmor.tooltipTarragonColor,
							AuricTeslaBodyArmor.tooltipBloodflareColor,
							AuricTeslaBodyArmor.tooltipGodslayerColor
						};
						LocalizedText LocalizedText = CalamityUtils.GetTextFromModItem(base.Type, $"SetBonus{AuricTeslaBodyArmor.setBonusTooltipNumber}");
						line.Text = ((AuricTeslaBodyArmor.setBonusTooltipNumber == 3) ? LocalizedText.Format(GodSlayerHeadRanged.ShrapnelRoundCooldown.FramesToSeconds(), CalamityKeybinds.GodSlayerDashHotKey.TooltipHotkeyString(), GodSlayerChestplate.DashCooldown.FramesToSeconds()) : ((AuricTeslaBodyArmor.setBonusTooltipNumber == 2) ? LocalizedText.Format(CalamityUtils.GetArmorSetBonusKey(), BloodflareHeadRanged.SoulCooldown.FramesToSeconds(), BloodflareHeadRanged.BloodBombCooldown.FramesToSeconds()) : LocalizedText.Format()));
						line.OverrideColor = armorColors[AuricTeslaBodyArmor.setBonusTooltipNumber - 1];
					}
				}
				return;
			}
		}
		AuricTeslaBodyArmor.holdingShift = false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GodSlayerHeadRanged>().AddIngredient<BloodflareHeadRanged>().AddIngredient<TarragonHeadRanged>()
			.AddIngredient<AuricBar>(12)
			.AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<AuricTeslaHeadMagic>())
			.Register();
	}
}
