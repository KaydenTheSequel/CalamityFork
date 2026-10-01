using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Aerospec;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "AerospecHeadgear" })]
public class AerospecHeadRogue : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.1f;

	public static float MoveSpeedBoost = 0.05f;

	public static float SetBonusRogueStealth = 0.8f;

	public static float SetBonusMoveSpeedBoost = 0.05f;

	public static int SetBonusRogueCritBoost = 5;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), MoveSpeedBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			ArmorIDs.Head.Sets.DrawFullHair[base.Item.headSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.defense = 4;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<AerospecBreastplate>())
		{
			return legs.type == ModContent.ItemType<AerospecLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), SetBonusMoveSpeedBoost.ToPercent(), AerospecBreastplate.SetBonusHurtDamageThreshold);
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.aeroSet = true;
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		player.noFallDmg = true;
		player.moveSpeed += SetBonusMoveSpeedBoost;
		player.GetCritChance<ThrowingDamageClass>() += SetBonusRogueCritBoost;
		player.Calamity().wearingRogueArmor = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(5).AddIngredient(824, 3).AddIngredient(320)
			.AddTile(16)
			.SortAfterFirstRecipesOf(ModContent.ItemType<AerospecBreastplate>())
			.Register();
	}
}
