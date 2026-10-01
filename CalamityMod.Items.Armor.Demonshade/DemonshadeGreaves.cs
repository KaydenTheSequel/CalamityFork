using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Demonshade;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
public class DemonshadeGreaves : ModItem, ILocalizedModType, IModType
{
	public static float MoveSpeedBoost = 0.3f;

	public static float AccelerationBoost = 0.5f;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 50;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().shadowSpeed = true;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ShadowspecBar>(15).AddTile<DraedonsForge>().Register();
	}
}
