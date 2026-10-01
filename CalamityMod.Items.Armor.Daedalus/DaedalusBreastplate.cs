using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Daedalus;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class DaedalusBreastplate : ModItem, ILocalizedModType, IModType
{
	public static float DamageBoost = 0.08f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent());

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Daedalus/DaedalusBreastplate_Waist", EquipType.Waist, this);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 19;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(15).AddIngredient<EssenceofEleum>(3).AddTile(134)
			.Register();
	}
}
