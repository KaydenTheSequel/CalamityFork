using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class AngelicAlliance : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/AngelicAllianceActivation");

	public static int MinionSlotBoost = 2;

	public static float TotalSummonDamageBoost = 0.15f;

	public static float DamageBoost = 0.08f;

	public static int RegenBoostDuringFlight = 4;

	public static int DivineBlessDuration = CalamityUtils.SecondsToFrames(15);

	public static int DivineBlessCooldown = CalamityUtils.SecondsToFrames(60);

	public static int HealPerAngelSpawned = 2;

	public static int BanishingFireDuration = CalamityUtils.SecondsToFrames(1);

	public static int DivineBlessFramesPerHeal = 15;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, TotalSummonDamageBoost.ToPercent(), DamageBoost.ToPercent(), RegenBoostDuringFlight.ToRegenPerSecond(), DivineBlessDuration.FramesToSeconds(), HealPerAngelSpawned, (60f / (float)DivineBlessFramesPerHeal).Round(), ((float)DivineBlessCooldown / 3600f).Round());

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 92;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.AngelicAllianceHotKey);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().angelicAlliance = true;
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetDamage<SummonDamageClass>() += TotalSummonDamageBoost - DamageBoost;
		player.maxMinions += MinionSlotBoost;
		if (player.wingTime < (float)player.wingTimeMax)
		{
			player.lifeRegen += RegenBoostDuringFlight;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyHallowedHelmet").AddRecipeGroup("AnyHallowedPlatemail").AddRecipeGroup("AnyHallowedGreaves")
			.AddIngredient(938)
			.AddIngredient(674)
			.AddIngredient(554)
			.AddIngredient<ShadowspecBar>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
