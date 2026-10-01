using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Tarragon;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "TarragonHelm" })]
public class TarragonHeadMelee : ModItem, ILocalizedModType, IModType
{
	public static float MeleeDamageBoost = 0.1f;

	public static int MeleeCritBoost = 5;

	public static float MeleeSpeedBoost = 0.15f;

	public static int SetBonusAggroBoost = 800;

	public static int TarraLifeDuration = CalamityUtils.SecondsToFrames(5);

	public static int TarraLifeRegenBoost = 3;

	public static float CloakContactDamageReduction = 0.5f;

	public static int CloakDuration = CalamityUtils.SecondsToFrames(10);

	public static int CloakCooldown = CalamityUtils.SecondsToFrames(30);

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBoost.ToPercent(), MeleeCritBoost, MeleeSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.defense = 40;
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
		calamityPlayer.tarraMelee = true;
		player.aggro += SetBonusAggroBoost;
		player.setBonus = this.GetLocalization("SetBonus").Format(TarraLifeRegenBoost.ToRegenPerSecond(), CalamityUtils.GetArmorSetBonusKey(), CloakDuration.FramesToSeconds(), CloakCooldown.FramesToSeconds());
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MeleeDamageClass>() += MeleeDamageBoost;
		player.GetCritChance<MeleeDamageClass>() += MeleeCritBoost;
		player.GetAttackSpeed<MeleeDamageClass>() += MeleeSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(7).AddIngredient<DivineGeode>(6).AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<TarragonHeadMagic>())
			.Register();
	}
}
