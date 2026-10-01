using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Tarragon;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "TarragonHornedHelm" })]
public class TarragonHeadSummon : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.25f;

	public static int SetBonusMinionSlotBoost = 2;

	public static float SetBonusSummonDamageBoost = 0.3f;

	public static int AuraDamage = 120;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	internal static string LifeAuraEntitySourceContext => "SetBonus_Calamity_Tarragon";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.defense = 10;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<TarragonBreastplate>())
		{
			return legs.type == ModContent.ItemType<TarragonLeggings>();
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
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.tarraSet = true;
		calamityPlayer.tarraSummon = true;
		calamityPlayer.WearingPostMLSummonerSet = true;
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent());
		player.maxMinions += SetBonusMinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(7).AddIngredient<DivineGeode>(6).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<TarragonHeadRogue>())
			.Register();
	}
}
