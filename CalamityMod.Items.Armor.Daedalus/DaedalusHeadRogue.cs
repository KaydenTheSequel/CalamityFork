using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Daedalus;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "DaedalusVisor" })]
public class DaedalusHeadRogue : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.13f;

	public static int RogueCritBoost = 5;

	public static float RogueVelocityBoost = 0.15f;

	public static float MoveSpeedBoost = 0.05f;

	public static float SetBonusRogueStealth = 1.05f;

	public static int ShardCountLimit = 15;

	public static double ShardDamageRatio = 0.25;

	public static int ShardDamageSoftcap = 30;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, RogueVelocityBoost.ToPercent(), MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 7;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<DaedalusBreastplate>())
		{
			return legs.type == ModContent.ItemType<DaedalusLeggings>();
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
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth());
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.daedalusSplit = true;
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		calamityPlayer.wearingRogueArmor = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.Calamity().rogueVelocity += RogueVelocityBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(7).AddIngredient<EssenceofEleum>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<DaedalusBreastplate>())
			.Register();
	}
}
