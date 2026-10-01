using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "BlushieStaff" })]
public class StaffofBlushie : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 48);
		base.Item.useStyle = 4;
		base.Item.useAnimation = 30;
		base.Item.useTime = 30;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.damage = 1;
		base.Item.knockBack = 1f;
		base.Item.autoReuse = false;
		base.Item.useTurn = false;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shoot = ModContent.ProjectileType<BlushieStaffProj>();
		base.Item.mana = 200;
		base.Item.shootSpeed = 0f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(741).AddIngredient<ShadowspecBar>(5).AddIngredient<Necroplasm>(10)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
