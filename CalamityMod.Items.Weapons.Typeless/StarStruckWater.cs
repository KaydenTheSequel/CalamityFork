using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

public class StarStruckWater : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
		ItemID.Sets.SortingPriorityTerraforming[base.Type] = 88;
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 20;
		base.Item.useStyle = 1;
		base.Item.shootSpeed = 14f;
		base.Item.rare = 3;
		base.Item.damage = 20;
		base.Item.shoot = ModContent.ProjectileType<StarStruckWaterBottle>();
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.useAnimation = 15;
		base.Item.useTime = 15;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.value = Item.sellPrice(0, 0, 0, 40);
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(126, 10).AddIngredient<StarblightSoot>(2).AddIngredient<AstralGrassSeeds>()
			.Register();
	}
}
