using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class PrismaticBreaker : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.damage = 425;
		base.Item.useStyle = 5;
		base.Item.useAnimation = (base.Item.useTime = 13);
		base.Item.reuseDelay = 30;
		base.Item.DamageType = MeleeRangedHybridDamageClass.Instance;
		base.Item.knockBack = 7f;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<PrismaticBreakerHoldout>();
		base.Item.shootSpeed = 14f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/PrismaticBreakerGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmicRainbow>().AddIngredient<SolsticeClaymore>().AddIngredient<CosmiliteBar>(8)
			.AddIngredient<EndothermicEnergy>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
