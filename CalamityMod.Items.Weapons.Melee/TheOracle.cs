using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Yoyos;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "Oracle" })]
public class TheOracle : ModItem, ILocalizedModType, IModType
{
	public const float AuraBaseDamageMult = 0.35f;

	public const float AuraMaxDamageMult = 0.8f;

	public static float Reach = 800f;

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
		base.Item.width = 58;
		base.Item.height = 50;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 201;
		base.Item.knockBack = 4f;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<OracleYoyo>();
		base.Item.shootSpeed = 16f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BurningRevelation>().AddIngredient<TheObliterator>().AddIngredient<AuricBar>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
