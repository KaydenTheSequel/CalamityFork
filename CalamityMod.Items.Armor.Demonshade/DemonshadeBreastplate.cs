using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Demonshade;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class DemonshadeBreastplate : ModItem, IDrawArmOverShoulderpad, ILocalizedModType, IModType
{
	public static int MaxManaBoost = 200;

	public static float AmmoReduction = 0.7f;

	public static float DamageBoost = 0.15f;

	public static int CritBoost = 15;

	public static float MeleeSpeedBoost = 0.25f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public string FrontArmTexture => "CalamityMod/Items/Armor/Demonshade/DemonshadeBreastplate_Arms";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaBoost, DamageBoost.ToPercent(), MeleeSpeedBoost.ToPercent(), (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 50;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override void UpdateEquip(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.shadeRegen = true;
		calamityPlayer.ammoCost *= AmmoReduction;
		player.statManaMax2 += MaxManaBoost;
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.GetAttackSpeed<MeleeDamageClass>() += MeleeSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ShadowspecBar>(18).AddTile<DraedonsForge>().Register();
	}
}
