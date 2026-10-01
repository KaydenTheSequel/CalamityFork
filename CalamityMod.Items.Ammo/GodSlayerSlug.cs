using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class GodSlayerSlug : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
	}

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 22;
		base.Item.damage = 18;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.knockBack = 3f;
		base.Item.value = Item.sellPrice(0, 0, 0, 28);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.shoot = ModContent.ProjectileType<GodSlayerSlugProj>();
		base.Item.shootSpeed = 6f;
		base.Item.ammo = 97;
	}

	public override void AddRecipes()
	{
		CreateRecipe(999).AddIngredient(3567, 999).AddIngredient<CosmiliteBar>().AddTile<CosmicAnvil>()
			.Register();
	}
}
