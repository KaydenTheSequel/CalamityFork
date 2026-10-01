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
[LegacyName(new string[] { "AuricTeslaHelm" })]
[LegacyName(new string[] { "AuricTeslaRoyalHelm" })]
public class AuricTeslaHeadMelee : ModItem, ILocalizedModType, IModType
{
	public static float MeleeDamageBoost = 0.12f;

	public static float MeleeSpeedBoost = 0.28f;

	public static int SetBonusAggroBoost = 1200;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBoost.ToPercent(), MeleeSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.defense = 54;
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
		modPlayer.tarraMelee = true;
		modPlayer.bloodflareSet = true;
		modPlayer.bloodflareMelee = true;
		modPlayer.godSlayer = true;
		modPlayer.godSlayerDamage = true;
		modPlayer.auricSet = true;
		modPlayer.auricSetMelee = true;
		player.crimsonRegen = true;
		player.aggro += SetBonusAggroBoost;
		if (modPlayer.godSlayerDashHotKeyPressed || (player.dashDelay != 0 && modPlayer.LastUsedDashID == GodslayerArmorDash.ID))
		{
			modPlayer.DeferredDashID = GodslayerArmorDash.ID;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MeleeDamageClass>() += MeleeDamageBoost;
		player.GetAttackSpeed<MeleeDamageClass>() += MeleeSpeedBoost;
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
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
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
						line.Text = ((AuricTeslaBodyArmor.setBonusTooltipNumber == 3) ? LocalizedText.Format(GodSlayerHeadMelee.SetBonusHurtDamageThreshold, CalamityKeybinds.GodSlayerDashHotKey.TooltipHotkeyString(), GodSlayerChestplate.DashCooldown.FramesToSeconds()) : ((AuricTeslaBodyArmor.setBonusTooltipNumber == 2) ? LocalizedText.Format(BloodflareHeadMelee.HitsToActivateFrenzy, BloodflareHeadMelee.FrenzyDuration.FramesToSeconds(), BloodflareHeadMelee.FrenzyMeleeDamageBoost.ToPercent(), BloodflareHeadMelee.FrenzyContactDamageReduction.ToPercent(), BloodflareHeadMelee.FrenzyCooldown.FramesToSeconds()) : LocalizedText.Format(TarragonHeadMelee.TarraLifeRegenBoost.ToRegenPerSecond(), CalamityUtils.GetArmorSetBonusKey(), TarragonHeadMelee.CloakDuration.FramesToSeconds(), TarragonHeadMelee.CloakCooldown.FramesToSeconds())));
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
		CreateRecipe().AddIngredient<GodSlayerHeadMelee>().AddIngredient<BloodflareHeadMelee>().AddIngredient<TarragonHeadMelee>()
			.AddIngredient<AuricBar>(12)
			.AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<AuricTeslaHeadMagic>())
			.Register();
	}
}
