using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Yoyos;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "Azathoth" })]
public class Ozzathoth : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 880f;

	public static float Speed = 60f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Reach.ToTiles(), Speed);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[base.Type] = true;
		ItemID.Sets.GamepadExtraRange[base.Type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 54;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 90;
		base.Item.knockBack = 6f;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<OzzathothYoyo>();
		base.Item.shootSpeed = 16f;
		base.Item.autoReuse = true;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3389).AddIngredient<ShadowspecBar>(5).AddIngredient<CoreofCalamity>(2)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
