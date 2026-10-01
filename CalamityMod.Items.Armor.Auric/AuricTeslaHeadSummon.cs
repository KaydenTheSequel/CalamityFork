using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Armor.Silva;
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
[LegacyName(new string[] { "AuricTeslaSpaceHelmet" })]
public class AuricTeslaHeadSummon : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 2;

	public static float SummonDamageBoost = 0.32f;

	public static int SetBonusMinionSlotBoost = 4;

	public static float SetBonusSummonDamageBoost = 0.55f;

	public static int CrystalDamage = 500;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.defense = 18;
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
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.tarraSet = true;
		calamityPlayer.tarraSummon = true;
		calamityPlayer.bloodflareSet = true;
		calamityPlayer.bloodflareSummon = true;
		calamityPlayer.silvaSet = true;
		calamityPlayer.silvaSummon = true;
		calamityPlayer.auricSet = true;
		calamityPlayer.WearingPostMLSummonerSet = true;
		player.crimsonRegen = true;
		player.maxMinions += SetBonusMinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
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
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
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
							AuricTeslaBodyArmor.tooltipSilvaColor
						};
						LocalizedText LocalizedText = this.GetLocalization($"SetBonus{AuricTeslaBodyArmor.setBonusTooltipNumber}");
						line.Text = ((AuricTeslaBodyArmor.setBonusTooltipNumber == 3) ? LocalizedText.Format(SilvaArmor.SetBonusRegenBoost.ToRegenPerSecond(), SilvaArmor.AccelerationBoost.ToPercent(), SilvaArmor.ReviveDuration.FramesToSeconds(), (SilvaArmor.ReviveCooldown / 60).FramesToSeconds()) : ((AuricTeslaBodyArmor.setBonusTooltipNumber == 2) ? LocalizedText.Format(BloodflareHeadSummon.DefenseBoostBelowHealthThreshold, BloodflareHeadSummon.DefenseBoostHealthThreshold.ToPercent()) : LocalizedText.Format()));
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
		CreateRecipe().AddIngredient<SilvaHeadSummon>().AddIngredient<BloodflareHeadSummon>().AddIngredient<TarragonHeadSummon>()
			.AddIngredient<AuricBar>(12)
			.AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<AuricTeslaHeadRogue>())
			.Register();
	}
}
