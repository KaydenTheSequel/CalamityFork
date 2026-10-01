using System;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Prismatic;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class PrismaticHelmet : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 80;

	public static float ManaCostReduction = 0.15f;

	public static float MagicDamageBoost = 0.15f;

	public static int MagicCritBoost = 12;

	public static float NonMagicDamageDecrease = 0.2f;

	public static int ManaRegenBonus = 8;

	public static int LaserDamage = 30;

	public static int LaserDuration = CalamityUtils.SecondsToFrames(5);

	public static int LaserCooldown = CalamityUtils.SecondsToFrames(30);

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	internal static string LaserEntitySourceContext => "SetBonus_Calamity_Prismatic";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), MagicDamageBoost.ToPercent(), MagicCritBoost, NonMagicDamageDecrease.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 18;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<PrismaticRegalia>())
		{
			return legs.type == ModContent.ItemType<PrismaticGreaves>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadowSubtle = true;
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().prismaticSet = true;
		player.manaRegenBonus += ManaRegenBonus;
		Color AbilityBriefColor = Color.Lerp(new Color(255, 106, 246), new Color(148, 145, 243), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
		player.setBonus = this.GetLocalization("SetBonus").Format(AbilityBriefColor.Hex3(), CalamityUtils.GetArmorSetBonusKey(), LaserDuration.FramesToSeconds(), LaserCooldown.FramesToSeconds());
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().prismaticHelmet = true;
		player.statManaMax2 += MaxManaBoost;
		player.manaCost -= ManaCostReduction;
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += MagicCritBoost;
		player.GetDamage<GenericDamageClass>() -= NonMagicDamageDecrease;
		player.GetDamage<MagicDamageClass>() += NonMagicDamageDecrease;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ArmoredShell>(3).AddIngredient<ExodiumCluster>(5).AddIngredient<DivineGeode>(4)
			.AddIngredient(1346, 300)
			.AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<PrismaticGreaves>())
			.Register();
	}
}
