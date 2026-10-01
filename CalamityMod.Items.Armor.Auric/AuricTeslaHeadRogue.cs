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
[LegacyName(new string[] { "AuricTeslaPlumedHelm" })]
public class AuricTeslaHeadRogue : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.17f;

	public static int RogueCritBoost = 10;

	public static float MoveSpeedBoost = 0.05f;

	public static float SetBonusRogueStealth = 1.3f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.defense = 34;
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
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth());
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.tarraSet = true;
		modPlayer.tarraThrowing = true;
		modPlayer.bloodflareSet = true;
		modPlayer.bloodflareThrowing = true;
		modPlayer.godSlayer = true;
		modPlayer.godSlayerThrowing = true;
		modPlayer.auricSet = true;
		modPlayer.rogueStealthMax += SetBonusRogueStealth;
		modPlayer.wearingRogueArmor = true;
		player.crimsonRegen = true;
		if (modPlayer.godSlayerDashHotKeyPressed || (player.dashDelay != 0 && modPlayer.LastUsedDashID == GodslayerArmorDash.ID))
		{
			modPlayer.DeferredDashID = GodslayerArmorDash.ID;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.moveSpeed += MoveSpeedBoost;
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
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
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
						line.Text = ((AuricTeslaBodyArmor.setBonusTooltipNumber == 3) ? LocalizedText.Format(GodSlayerHeadRogue.RogueDamageBoostAtFullHealth.ToPercent(), GodSlayerHeadRogue.SetBonusHurtDamageThreshold, CalamityKeybinds.GodSlayerDashHotKey.TooltipHotkeyString(), GodSlayerChestplate.DashCooldown.FramesToSeconds()) : ((AuricTeslaBodyArmor.setBonusTooltipNumber == 2) ? LocalizedText.Format(BloodflareHeadRogue.DefenseBoostAboveHealthThreshold, BloodflareHeadRogue.DefenseBoostHealthThreshold.ToPercent()) : LocalizedText.Format(TarragonHeadRogue.CritsToActivateImmunity, TarragonHeadRogue.ImmunityDuration.FramesToSeconds(), TarragonHeadRogue.ImmunityCooldown.FramesToSeconds(), TarragonHeadRogue.RogueDamageBoostWhileDebuffed.ToPercent())));
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
		CreateRecipe().AddIngredient<GodSlayerHeadRogue>().AddIngredient<BloodflareHeadRogue>().AddIngredient<TarragonHeadRogue>()
			.AddIngredient<AuricBar>(12)
			.AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<AuricTeslaBodyArmor>())
			.Register();
	}
}
