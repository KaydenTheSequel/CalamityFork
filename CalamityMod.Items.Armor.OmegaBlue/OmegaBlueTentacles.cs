using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.OmegaBlue;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
[LegacyName(new string[] { "OmegaBlueLeggings" })]
public class OmegaBlueTentacles : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.16f;

	public static int CritBoost = 12;

	public static float MoveSpeedBoost = 0.12f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost, MoveSpeedBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Legs);
			ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.defense = 22;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ReaperTooth>(4).AddIngredient<DepthCells>(15).AddIngredient<RuinousSoul>(2)
			.AddTile(134)
			.Register();
	}
}
