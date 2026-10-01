using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.FathomSwarmer;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class FathomSwarmerVisage : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.08f;

	public static int SetBonusMinionSlotBoost = 2;

	public static float SetBonusSummonDamageBoost = 0.1f;

	public static float SetBonusSubmergedSummonDamageBoost = 0.2f;

	public static int SetBonusSubmergedDefenseBoost = 10;

	public static int SetBonusSubmergedRegenBoost = 5;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 8;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<FathomSwarmerBreastplate>())
		{
			return legs.type == ModContent.ItemType<FathomSwarmerBoots>();
		}
		return false;
	}

	public override void PreUpdateVanitySet(Player player)
	{
		player.Calamity().fathomSwarmerTail = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent(), SetBonusSubmergedSummonDamageBoost.ToPercent(), SetBonusSubmergedDefenseBoost, SetBonusSubmergedRegenBoost.ToRegenPerSecond());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.fathomSwarmer = true;
		player.spikedBoots = 2;
		player.maxMinions += SetBonusMinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
		if (calamityPlayer.countsAsAnyWet)
		{
			player.GetDamage<SummonDamageClass>() += SetBonusSubmergedSummonDamageBoost;
			player.statDefense += SetBonusSubmergedDefenseBoost;
			player.lifeRegen += SetBonusSubmergedRegenBoost;
		}
	}

	public override void UpdateEquip(Player player)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
		if (player.breath <= player.breathMax + 2 && !modPlayer.ZoneAbyss)
		{
			player.breath = player.breathMax + 3;
		}
		modPlayer.fathomSwarmerVisage = true;
		if (player.Calamity().countsAsAnyWet)
		{
			Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.3f, 0.9f, 1.35f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(5).AddIngredient<PlantyMush>(6).AddIngredient<DepthCells>(3)
			.AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<FathomSwarmerBreastplate>())
			.Register();
	}
}
