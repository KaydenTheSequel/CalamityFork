using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Spears;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Nadir : ModItem, ILocalizedModType, IModType
{
	public static float ProjShootSpeed = 20f;

	public static int FadeoutSpeed = 20;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 144;
		base.Item.height = 144;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.damage = 365;
		base.Item.knockBack = 8f;
		base.Item.useAnimation = 18;
		base.Item.useTime = 18;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<NadirSpear>();
		base.Item.shootSpeed = 12f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<VanishingPoint>().AddIngredient<AuricBar>(5).AddIngredient<DarksunFragment>(8)
			.AddIngredient<TwistingNether>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
