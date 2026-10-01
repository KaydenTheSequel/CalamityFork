using CalamityMod.Items.Materials;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class DragonPow : ModItem, ILocalizedModType, IModType
{
	public static float Speed = 13f;

	public static float ReturnSpeed = 20f;

	public static float SparkSpeed = 0.6f;

	public static float MinPetalSpeed = 24f;

	public static float MaxPetalSpeed = 30f;

	public static float MinWaterfallSpeed = 12f;

	public static float MaxWaterfallSpeed = 15.5f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 76;
		base.Item.height = 82;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 660;
		base.Item.knockBack = 9f;
		base.Item.useAnimation = 20;
		base.Item.useTime = 20;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = Yharon.ShortRoarSound;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<DragonPowFlail>();
		base.Item.shootSpeed = Speed;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Mourningstar>().AddIngredient(389).AddIngredient(1259)
			.AddIngredient(2611)
			.AddIngredient<BallOFugu>()
			.AddIngredient<Tumbleweed>()
			.AddIngredient<AuricBar>(5)
			.AddIngredient<YharonSoulFragment>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
