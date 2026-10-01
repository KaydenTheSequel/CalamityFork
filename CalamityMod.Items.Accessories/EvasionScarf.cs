using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Neck })]
public class EvasionScarf : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.dodgeScarf = true;
		calamityPlayer.evasionScarf = true;
		calamityPlayer.DashID = CounterScarfDash.ID;
		player.dashType = 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CounterScarf>().AddIngredient(520, 5).AddIngredient(521, 5)
			.AddIngredient(225, 15)
			.AddTile(134)
			.Register();
	}
}
