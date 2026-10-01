using CalamityMod.CalPlayer;
using CalamityMod.ExtraJumps;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Sulphurous;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "SulfurHelmet" })]
public class SulphurousHelmet : ModItem, ILocalizedModType, IModType
{
	public static int RogueCritBoost = 6;

	public static int SetBonusPoisonDuration = CalamityUtils.SecondsToFrames(1);

	public static float SetBonusRogueStealth = 0.65f;

	public static int BubbleDamage = 20;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueCritBoost);

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.defense = 5;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<SulphurousBreastplate>())
		{
			return legs.type == ModContent.ItemType<SulphurousLeggings>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), SetBonusPoisonDuration.FramesToSeconds());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.sulphurSet = true;
		player.GetJumpState<SulphurJump>().Enable();
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		calamityPlayer.wearingRogueArmor = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		if (player.Calamity().countsAsAnyWet)
		{
			player.gills = true;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(10).AddIngredient<SulphuricScale>(10).AddTile(16)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<SulphurousBreastplate>())
			.Register();
	}
}
