using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Neck })]
public class BloodyWormScarf : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 42;
		base.Item.defense = 3;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().bloodyWormTooth = true;
		player.endurance += 0.1f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodyWormTooth>().AddIngredient(3224).AddIngredient(521, 3)
			.AddTile(16)
			.Register();
	}
}
