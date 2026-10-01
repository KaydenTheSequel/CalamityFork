using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "Minigun" })]
public class Kingsbane : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 95;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 92;
		base.Item.height = 44;
		base.Item.damage = 249;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 3;
		base.Item.useAnimation = 3;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.5f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<KingsbaneHoldout>();
		base.Item.shootSpeed = 2f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			return Main.rand.Next(100) >= AmmoSavedPercent;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, position, velocity, base.Item.shoot, damage, knockback, player.whoAmI).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1929).AddIngredient<P90>().AddIngredient<AuricBar>(5)
			.AddIngredient<LifeAlloy>(3)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
