using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Shield })]
public class OrnateShield : ModItem, ILocalizedModType, IModType
{
	public const int ShieldSlamDamage = 50;

	public const float ShieldSlamKnockback = 3f;

	public const int ShieldSlamIFrames = 12;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.defense = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().DashID = OrnateShieldDash.ID;
		player.dashType = 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(5).AddIngredient(502, 10).AddTile(134)
			.Register();
	}
}
