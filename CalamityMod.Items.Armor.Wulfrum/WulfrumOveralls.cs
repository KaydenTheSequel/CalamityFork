using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Wulfrum;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
[LegacyName(new string[] { "WulfrumLeggings" })]
public class WulfrumOveralls : ModItem, ILocalizedModType, IModType
{
	public static float SummonDamageBoost = 0.05f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 1;
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Legs);
			ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlot] = true;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(8).AddIngredient<EnergyCore>().AddTile(16)
			.Register();
	}
}
