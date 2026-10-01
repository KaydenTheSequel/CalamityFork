using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Tarragon;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "TarragonMask" })]
public class TarragonHeadMagic : ModItem, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 100;

	public static float ManaCostReduction = 0.15f;

	public static float MagicDamageBoost = 0.15f;

	public static int MagicCritBoost = 10;

	public static int CritsToSpawnLeaves = 5;

	public static float LeafDamageRatio = 0.2f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, ManaCostReduction.ToPercent(), MagicDamageBoost.ToPercent(), MagicCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.defense = 16;
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
		calamityPlayer.tarraMage = true;
		player.setBonus = this.GetLocalizedValue("SetBonus");
	}

	public override void UpdateEquip(Player player)
	{
		player.statManaMax2 += MaxManaBoost;
		player.manaCost -= ManaCostReduction;
		player.GetDamage<MagicDamageClass>() += MagicDamageBoost;
		player.GetCritChance<MagicDamageClass>() += MagicCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(7).AddIngredient<DivineGeode>(6).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<TarragonBreastplate>())
			.Register();
	}
}
