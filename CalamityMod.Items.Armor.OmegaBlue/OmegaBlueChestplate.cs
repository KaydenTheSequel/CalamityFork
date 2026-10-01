using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.OmegaBlue;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class OmegaBlueChestplate : ModItem, ILocalizedModType, IModType
{
	public static float AmmoReduction = 0.75f;

	public static float DamageBoost = 0.18f;

	public static int CritBoost = 12;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost, (1f - AmmoReduction).ToPercent());

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Body);
			ArmorIDs.Body.Sets.HidesTopSkin[equipSlot] = true;
			ArmorIDs.Body.Sets.HidesArms[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 28;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void UpdateEquip(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		calamityPlayer.ammoCost *= AmmoReduction;
		calamityPlayer.omegaBlueChestplate = true;
		calamityPlayer.noLifeRegen = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ReaperTooth>(5).AddIngredient<DepthCells>(18).AddIngredient<RuinousSoul>(3)
			.AddTile(134)
			.Register();
	}
}
