using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Starmageddon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 166;
		base.Item.height = 62;
		base.Item.damage = 138;
		base.Item.knockBack = 4f;
		base.Item.shootSpeed = 16f;
		base.Item.useStyle = 5;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.UseSound = null;
		base.Item.shoot = ModContent.ProjectileType<StarmageddonHeld>();
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.channel = true;
		base.Item.useTurn = false;
		base.Item.autoReuse = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/StarmageddonGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<StarmageddonHeld>(), 0, 0f, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Starfleet>().AddIngredient<BarracudaGun>().AddIngredient<CosmiliteBar>(8)
			.AddIngredient<DarksunFragment>(8)
			.AddIngredient<ExodiumCluster>(15)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
